using EduNet.Backend.Service.DTOs.Users.UserCodes;
using EduNet.Backend.Service.DTOs.Users.UserRoles;
using EduNet.Backend.Service.DTOs.Students.Students;
using EduNet.Backend.Service.DTOs.Teachers.Teachers;

namespace EduNet.Backend.Service.DTOs.Users.Users;

public class UserForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long BranchId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsVerified { get; set; } = false;
    public string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }
    public StudentForResultDto Student { get; set; }
    public TeacherForResultDto Teacher { get; set; }
    public ICollection<UserRoleForResultDto> Roles { get; set; }
    public ICollection<UserCodeForResultDto> UserCodes { get; set; }
}
