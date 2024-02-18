using EduNet.Backend.Service.DTOs.Users.UserRoles;
using EduNet.Backend.Service.DTOs.Roles.RolePermissions;

namespace EduNet.Backend.Service.DTOs.Roles.Roles;

public class RoleForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<UserRoleForResultDto> Users { get; set; }
    public ICollection<RolePermissionForResultDto> Permissions { get; set; }
}
