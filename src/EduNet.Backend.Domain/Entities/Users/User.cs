using EduNet.Backend.Domain.Commons;
using EduNet.Backend.Domain.Entities.Students;
using EduNet.Backend.Domain.Entities.Teachers;

namespace EduNet.Backend.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long BranchId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsVerified { get; set; } = false;
    public string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }
    public Student Student { get; set; }
    public Teacher Teacher { get; set; }
    public ICollection<UserRole> Roles { get; set; }
    public ICollection<UserCode> UserCodes { get; set; }
}
