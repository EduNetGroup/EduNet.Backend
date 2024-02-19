using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Users.UserRoles;

namespace EduNet.Backend.Service.Interfaces.Users;

public interface IUserRoleService
{
    Task<bool> RemoveAsync(long id);
    Task<UserRoleForResultDto> RetrieveByIdAsync(long id);
    Task<UserRoleForResultDto> AddAsync(UserRoleForCreationDto dto);
    Task<UserRoleForResultDto> ModifyAsync(long id, UserRoleForUpdateDto dto);
    Task<IEnumerable<UserRoleForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<UserRoleForResultDto>> SearchAllAsync(string search, PaginationParams @params);

}
