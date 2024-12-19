using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Service.Interfaces.Users;
using EduNet.Backend.Service.DTOs.Users.Users;

namespace EduNet.Backend.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    public UserService(
        IMapper mapper,
        IRepository<User> userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
    {
        var userData = await _userRepository
            .SelectAsync(u => u.PhoneNumber == dto.PhoneNumber);
        if (userData is not null)
            throw new EduNetException(409, "User is already exist");

        var mappedData = _mapper.Map<User>(dto);

        return _mapper.Map<UserForResultDto>(_userRepository.InsertAsync(mappedData));
    }

    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        var userData = await _userRepository
            .SelectAsync(u => u.Id == id);
        if (userData is null)
            throw new EduNetException(404, "User is not found");

        var mappedData = _mapper.Map(dto, userData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(mappedData);

        return _mapper.Map<UserForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var userData = await _userRepository
            .SelectAsync(u => u.Id == id);
        if (userData is null)
            throw new EduNetException(404, "User is not found");

        return await _userRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var userData = await _userRepository
            .SelectAll(u => !u.IsDeleted)
            .Include(u => u.Teacher)
            .Include(u => u.Student)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(userData);
    }

    public async Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        var userData = await _userRepository
            .SelectAll(u => !u.IsDeleted)
            .Include(u => u.Teacher)
            .Include(u => u.Student)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (userData is null)
            throw new EduNetException(404, "User is not found");

        return _mapper.Map<UserForResultDto>(userData);
    }

    public async Task<IEnumerable<UserForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var userData = await _userRepository
            .SelectAll(u => !u.IsDeleted)
            .Where(u => u.FirstName.ToLower().Contains(search.ToLower())
                            || u.LastName.ToLower().Contains(search.ToLower())
                            || u.PhoneNumber.Contains(search)
                            || u.Email.ToLower().Contains(search.ToLower()))
            .Include(u => u.Teacher)
            .Include(u => u.Student)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(userData);    
    }
}
