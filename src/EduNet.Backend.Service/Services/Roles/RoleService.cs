using AutoMapper;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Domain.Entities.Roles;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Roles.Roles;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Service.Interfaces.Roles;
using Microsoft.EntityFrameworkCore;

namespace EduNet.Backend.Service.Services.Roles;

public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Role> _roleRepository;

    public RoleService(IMapper mapper, IRepository<Role> roleRepository)
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    public async Task<RoleForResultDto> AddAsync(RoleForCreationDto dto)
    {
        var roleData = await _roleRepository
            .SelectAsync(r => r.Name.ToLower() == dto.Name.ToLower());
        if (roleData is not null)
            throw new EduNetException(409, "Role is already exist");

        var mappedData = _mapper.Map<RoleForResultDto>(roleData);
    }

    public async Task<RoleForResultDto> ModifyAsync(long id, RoleForUpdateDto dto)
    {
        var roleData = await _roleRepository
            .SelectAsync(r => r.Id == id);
        if (roleData is null)
            throw new EduNetException(404, "Role is not found");

        var mappedData = _mapper.Map<Role>(roleData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _roleRepository.UpdateAsync(mappedData);

        return _mapper.Map<RoleForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var roleData = await _roleRepository
            .SelectAsync(r => r.Id == id);
        if (roleData is null)
            throw new EduNetException(404, "Role is not found");

        return await _roleRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<RoleForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var roleData = await _roleRepository
            .SelectAll(r => !r.IsDeleted)
            .Include(r => r.Permissions.Where(p => p.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<RoleForResultDto>>(roleData);
    }

    public async Task<RoleForResultDto> RetrieveByIdAsync(long id)
    {
        var roleData = await _roleRepository
            .SelectAll(r => !r.IsDeleted && r.Id == id)
            .Include(r => r.Permissions.Where(r => !r.IsDeleted))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (roleData is null)
            throw new EduNetException(404, "Role is not found");
    }

    public async Task<IEnumerable<RoleForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var roleData = await _roleRepository
            .SelectAll(r => !r.IsDeleted)
            .Where(r => r.Name.ToLower().Contains(search.ToLower()))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<RoleForResultDto>>(roleData);
    }
}
