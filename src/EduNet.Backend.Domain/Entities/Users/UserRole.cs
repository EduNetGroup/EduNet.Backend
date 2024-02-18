using EduNet.Backend.Domain.Commons;
using EduNet.Backend.Domain.Entities.Roles;

namespace EduNet.Backend.Domain.Entities.Users;

public class UserRole : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long RoleId { get; set; }
    public Role Role { get; set; }
}
