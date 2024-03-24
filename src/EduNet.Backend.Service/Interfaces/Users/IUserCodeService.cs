using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Users.UserCodes;

namespace EduNet.Backend.Service.Interfaces.Users;

public interface IUserCodeService
{
    Task<bool> RemoveAsync(long id);
    Task<UserCodeForResultDto> RetrieveByIdAsync(long id);
    Task<UserCodeForResultDto> AddAsync(UserCodeForCreationDto dto);
    Task<IEnumerable<UserCodeForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
