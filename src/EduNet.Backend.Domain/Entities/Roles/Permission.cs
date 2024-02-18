using EduNet.Backend.Domain.Commons;

namespace EduNet.Backend.Domain.Entities.Roles;

public class Permission : Auditable
{
    public string Name { get; set; }
    public ICollection<RolePermission> Roles { get; set; }
}
