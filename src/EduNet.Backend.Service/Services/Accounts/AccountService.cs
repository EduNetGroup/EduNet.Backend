using AutoMapper;
using EduNet.Backend.Service.Helpers;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.DTOs.Logins;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.Interfaces.Auth;
using EduNet.Backend.Service.Interfaces.Users;
using EduNet.Backend.Service.DTOs.Users.Users;
using EduNet.Backend.Service.Interfaces.Emails;
using EduNet.Backend.Service.Interfaces.Accounts;

namespace EduNet.Backend.Service.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IAuthService  _authService;
    private readonly IEmailService _emailService;
    private readonly IRepository<User> _userRepository;

    public AccountService(
        IMapper mapper,
        IUserService userService,
        IAuthService authService,
        IEmailService emailService,
        IRepository<User> userRepository)
    {
        _mapper = mapper;
        _userService = userService;
        _authService = authService;
        _emailService = emailService;
        _userRepository = userRepository;
    }

    public async Task<bool> ChangePassword(string email, string password)
    {
        var user = await _userRepository
            .SelectAsync(u => u.Email == email);

        if (user is null || PasswordHasherHelper.IsEqual(Constants.PASSWORD_SALT, user.Password))
            return false;

        user.Password = PasswordHasherHelper.PasswordHasher(password);
        return true;
    }
    public async Task<LoginForResultDto> CreateAsync(UserForCreationDto userModel)
    {
        var user = await _userRepository
            .SelectAsync(u => u.Email == userModel.Email);

        if (user is not null && !user.IsVerified)
            throw new EduNetException(409, "Siz avval ro'yhatdan o'tgansiz, iltimos pochtangizni tasdiqlang va tizimga kiring!");

        if (user is not null)
            throw new EduNetException(409, "Siz avval ro'yhatdan o'tgansiz, iltimos pochta va parol orqali tizimga kiring!");

        var mapped = _mapper.Map<User>(userModel);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.Password = PasswordHasherHelper.PasswordHasher(userModel.Password);
        //mapped.Role = Role.Student;
        (mapped.RefreshToken, mapped.ExpireDate) = await _authService.GenerateRefreshTokenAsync();

        var result = await _userRepository.InsertAsync(mapped);

        await _emailService.ResendCodeAsync(userModel.Email);

        var userView = _mapper.Map<UserForResultDto>(result);
        (string token, DateTime expireDate) = await _authService.GenerateTokenAsync(userView);
        return new LoginForResultDto
        {
            Token = token,
            AccessTokenExpireDate = expireDate,
            RefreshToken = mapped.RefreshToken,
            User = userView
        };
    }
    public async Task<LoginForResultDto> LoginAsync(LoginForCreationDto login)
    {
        var user = await _userRepository
             .SelectAsync(u => u.Email == login.Email);

        var log = PasswordHasherHelper.PasswordHasher(login.Password);
        var log2 = PasswordHasherHelper.PasswordHasher("string");
        if (user is null || !PasswordHasherHelper.IsEqual(login.Password, user.Password))
            throw new EduNetException(404, "Email yoki parol xato!");

        if (!user.IsVerified)
            throw new EduNetException(403, "Iltimos avval pochtangizni tasdiqlang!");

        if (user.ExpireDate > DateTime.UtcNow)
        {
            (user.RefreshToken, user.ExpireDate) = await _authService.GenerateRefreshTokenAsync();
            await _userService.ModifyAsync(user.Id, _mapper.Map<UserForUpdateDto>(user));
        }

        var userView = _mapper.Map<UserForResultDto>(user);
        (string token, DateTime expireDate) = await _authService.GenerateTokenAsync(userView);
        return new LoginForResultDto
        {
            Token = token,
            AccessTokenExpireDate = expireDate,
            RefreshToken = user.RefreshToken,
            User = userView
        };
    }
}
