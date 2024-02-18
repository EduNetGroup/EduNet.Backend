using EduNet.Backend.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduNet.Backend.Domain.Entities.Users;

public class UserCode : Auditable
{
    [ForeignKey(nameof(User))]
    public long UserId { get; set; }
    public User User { get; set; }
    public long Code { get; set; }
    public DateTime ExpireDate { get; set; }
}
