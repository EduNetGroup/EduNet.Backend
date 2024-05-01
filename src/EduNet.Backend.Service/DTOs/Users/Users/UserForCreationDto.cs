using System.ComponentModel.DataAnnotations;
using EduNet.Backend.Service.Commons.Attributes;

namespace EduNet.Backend.Service.DTOs.Users.Users;

public class UserForCreationDto
{
    [MinLength(1), MaxLength(64)]
    public string FirstName { get; set; }

    [MinLength(1), MaxLength(64)]
    public string LastName { get; set; }

    [Required]
    public long BranchId { get; set; }

    [PhoneNumberAttribute]
    public string PhoneNumber { get; set; }

    [CustomEmailAddressAttribute]
    public string Email { get; set; }

    [StrongPasswordAttribute]
    public string Password { get; set; }
}
