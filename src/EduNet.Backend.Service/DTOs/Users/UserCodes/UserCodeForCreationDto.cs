namespace EduNet.Backend.Service.DTOs.Users.UserCodes;

public class UserCodeForCreationDto
{
    public long UserId { get; set; }
    public long Code { get; set; }
    public DateTime ExpireDate { get; set; }
}
