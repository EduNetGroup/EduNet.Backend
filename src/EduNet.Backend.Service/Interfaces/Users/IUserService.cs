using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Users.Users;

namespace EduNet.Backend.Service.Interfaces.Users;

public interface IUserService
{
    Task<bool> RemoveAsync(long id);
    Task<UserForResultDto> RetrieveByIdAsync(long id);
    Task<UserForResultDto> AddAsync(UserForCreationDto dto);
    Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<UserForResultDto>> SearchAllAsync(string search, PaginationParams @params);

}
