using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Roles.RolePermissions;

namespace EduNet.Backend.Service.Interfaces.Roles;

public interface IRolePermissionService
{
    Task<bool> RemoveAsync(long id);
    Task<RolePermissionForResultDto> RetrieveByIdAsync(long id);
    Task<RolePermissionForResultDto> AddAsync(RolePermissionForCreationDto dto);
    Task<RolePermissionForResultDto> ModifyAsync(long id, RolePermissionForUpdateDto dto);
    Task<IEnumerable<RolePermissionForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<RolePermissionForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
