using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Roles.Roles;

namespace EduNet.Backend.Service.Interfaces.Roles;

public interface IRoleService
{
    Task<bool> RemoveAsync(long id);
    Task<RoleForResultDto> RetrieveByIdAsync(long id);
    Task<RoleForResultDto> AddAsync(RoleForCreationDto dto);
    Task<RoleForResultDto> ModifyAsync(long id, RoleForUpdateDto dto);
    Task<IEnumerable<RoleForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<RoleForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
