using EduNet.Backend.Service.DTOs.Logins;
using EduNet.Backend.Service.DTOs.Users.Users;

namespace EduNet.Backend.Service.Interfaces.Accounts;

public interface IAccountService
{
    public Task<LoginForResultDto> LoginAsync(LoginForCreationDto login);
    public Task<LoginForResultDto> CreateAsync(UserForCreationDto user);
    public Task<bool> ChangePassword(string email, string password);
}
