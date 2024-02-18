using EduNet.Backend.Domain.Entities.Users;

namespace EduNet.Backend.Service.DTOs.Users.UserCodes;

public class UserCodeForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public long Code { get; set; }
    public DateTime ExpireDate { get; set; }
}
