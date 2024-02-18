using EduNet.Backend.Domain.Entities.Roles;
using EduNet.Backend.Domain.Entities.Users;

namespace EduNet.Backend.Service.DTOs.Users.UserRoles;

public class UserRoleForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public long RoleId { get; set; }
    public Role Role { get; set; }
}
