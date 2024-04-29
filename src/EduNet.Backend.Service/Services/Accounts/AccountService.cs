using AutoMapper;
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

    public Task<bool> ChangePassword(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<LoginForResultDto> CreateAsync(UserForCreationDto user)
    {
        throw new NotImplementedException();
    }

    public Task<LoginForResultDto> LoginAsync(LoginForCreationDto login)
    {
        throw new NotImplementedException();
    }
}
