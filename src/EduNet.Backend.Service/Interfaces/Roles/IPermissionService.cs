using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Roles.Permissions;

namespace EduNet.Backend.Service.Interfaces.Roles;

public interface IPermissionService
{
    Task<bool> RemoveAsync(long id);
    Task<PermissionForResultDto> RetrieveByIdAsync(long id);
    Task<PermissionForResultDto> AddAsync(PermissionForCreationDto dto);
    Task<PermissionForResultDto> ModifyAsync(long id, PermissionForUpdateDto dto);
    Task<IEnumerable<PermissionForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<PermissionForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
