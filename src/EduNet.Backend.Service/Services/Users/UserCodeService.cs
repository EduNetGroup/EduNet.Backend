using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.Interfaces.Users;
using EduNet.Backend.Service.DTOs.Users.UserCodes;

namespace EduNet.Backend.Service.Services.Users;

public class UserCodeService : IUserCodeService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserCode> _userCodeRepository;

    public UserCodeService(
        IMapper mapper,
        IRepository<User> userRepository,
        IRepository<UserCode> userCodeRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userCodeRepository = userCodeRepository;
    }

    public async Task<UserCodeForResultDto> AddAsync(UserCodeForCreationDto dto)
    {
        var userData = await _userRepository
            .SelectAsync(u => u.Id == dto.UserId);
        if (userData is null)
            throw new EduNetException(404, "User is not found");

        var model = _mapper.Map<UserCode>(dto);
        model.ExpireDate = DateTime.UtcNow.AddMinutes(3);

        var result = await _userCodeRepository.InsertAsync(model);

        return _mapper.Map<UserCodeForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var userCodeData = await _userCodeRepository
            .SelectAsync(u => u.Id == id);
        if (userCodeData is null)
            throw new EduNetException(404, "UserCode is not found");

        return await _userCodeRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserCodeForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var userCodeData = await _userCodeRepository
            .SelectAll(uc => !uc.IsDeleted)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserCodeForResultDto>>(userCodeData);
    }

    public async Task<UserCodeForResultDto> RetrieveByIdAsync(long id)
    {
        var userCodeData = await _userCodeRepository
            .SelectAsync(uc => uc.Id == id);
        if (userCodeData is null)
            throw new EduNetException(404, "UserCode is not found");

        return _mapper.Map<UserCodeForResultDto>(userCodeData);
    }
}
