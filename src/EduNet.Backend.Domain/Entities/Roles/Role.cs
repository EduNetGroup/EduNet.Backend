using EduNet.Backend.Domain.Commons;
using EduNet.Backend.Domain.Entities.Users;

namespace EduNet.Backend.Domain.Entities.Roles;

public class Role : Auditable
{
    public string Name { get; set; }
    public ICollection<UserRole> Users { get; set; }
    public ICollection<RolePermission> Permissions { get; set; }
}
