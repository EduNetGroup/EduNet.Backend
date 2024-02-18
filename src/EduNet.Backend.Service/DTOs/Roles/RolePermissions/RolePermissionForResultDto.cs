using EduNet.Backend.Domain.Entities.Roles;

namespace EduNet.Backend.Service.DTOs.Roles.RolePermissions;

public class RolePermissionForResultDto
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public Role Role { get; set; }
    public long PermissionId { get; set; }
    public Permission Permission { get; set; }
}
