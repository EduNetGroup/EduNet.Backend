using AutoMapper;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Domain.Entities.Roles;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Roles.RolePermissions;
using EduNet.Backend.Service.DTOs.Users.UserRoles;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Service.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace EduNet.Backend.Service.Services.Users;

public class UserRoleService : IUserRoleService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<UserRole> _userRoleRepository;

    public UserRoleService(
        IMapper mapper,
        IRepository<User> userRepository,
        IRepository<Role> roleRepository,
        IRepository<UserRole> userRoleRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<UserRoleForResultDto> AddAsync(UserRoleForCreationDto dto)
    {
        var roleData = await _roleRepository
            .SelectAsync(r => r.Id == dto.RoleId);
        if (roleData is null)
            throw new EduNetException(404, "Role is not found");

        var userData = await _userRepository
            .SelectAsync(u => u.Id == dto.UserId);
        if (userData is null)
            throw new EduNetException(404, "User is not found");

        var userRoleData = await _userRoleRepository
            .SelectAsync(ur => ur.UserId == dto.UserId && ur.RoleId == dto.RoleId);
        if (userRoleData is not null)
            throw new EduNetException(409, "UserRole is already exist");

        var mappedData = _mapper.Map<UserRole>(dto);

        return _mapper.Map<UserRoleForResultDto>(await _userRoleRepository.InsertAsync(mappedData));
    }

    public async Task<UserRoleForResultDto> ModifyAsync(long id, UserRoleForUpdateDto dto)
    {
        var roleData = await _roleRepository
            .SelectAsync(r => r.Id == dto.RoleId);
        if (roleData is null)
            throw new EduNetException(404, "Role is not found");

        var userData = await _userRepository
            .SelectAsync(u => u.Id == dto.UserId);
        if (userData is null)
            throw new EduNetException(404, "User is not found");

        var userRoleData = await _userRoleRepository
            .SelectAsync(ur => ur.Id == id);
        if (userRoleData is null)
            throw new EduNetException(404, "UserRole is not found");

        var mappedData = _mapper.Map(dto, userRoleData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _userRoleRepository.UpdateAsync(mappedData);

        return _mapper.Map<UserRoleForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var userRoleData = await _userRoleRepository
            .SelectAsync(ur => ur.Id == id);
        if (userRoleData is null)
            throw new EduNetException(404, "UserRole is not found");

        return await _userRoleRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserRoleForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var userRoleData = await _userRoleRepository
            .SelectAll(ur => !ur.IsDeleted)
            .Include(ur => ur.Role)
            .Include(rp => rp.User)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserRoleForResultDto>>(userRoleData);
    }

    public async Task<UserRoleForResultDto> RetrieveByIdAsync(long id)
    {
        var userRoleData = await _userRoleRepository
            .SelectAll(ur => !ur.IsDeleted)
            .Include(ur => ur.Role)
            .Include(ur => ur.User)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (userRoleData is null)
            throw new EduNetException(404, "UserRole is not found");

        return _mapper.Map<UserRoleForResultDto>(userRoleData);
    }

    public Task<IEnumerable<UserRoleForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }
}
