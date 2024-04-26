using AutoMapper;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Users.Users;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Service.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace EduNet.Backend.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Branch> _branchRepository;

    public UserService(
        IMapper mapper,
        IRepository<User> userRepository,
        IRepository<Branch> branchRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _branchRepository = branchRepository;
    }

    public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new EduNetException(404, "Branch is not found");

        var userData = await _userRepository
            .SelectAsync(u => u.PhoneNumber == dto.PhoneNumber);
        if (userData is not null)
            throw new EduNetException(409, "User is already exist");

        var mappedData = _mapper.Map<User>(dto);

        return _mapper.Map<UserForResultDto>(_userRepository.InsertAsync(mappedData));
    }

    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new EduNetException(404, "Branch is not found");

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
            .Include(u => u.Teachers);
    }

    public Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }
}
