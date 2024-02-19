using EduNet.Backend.Service.DTOs.Users.Users;

namespace EduNet.Backend.Service.Interfaces.Auth;

public interface IAuthService
{
    Task<UserForResultDto?> GetUserByAccessTokenAsync(string accessToken);
    Task<(string refreshToken, DateTime tokenValidityTime)> GenerateRefreshTokenAsync();
    Task<(string token, DateTime tokenExpiryTime)> GenerateTokenAsync(UserForResultDto user);

}
