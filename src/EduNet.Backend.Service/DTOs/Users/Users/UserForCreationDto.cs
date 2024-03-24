namespace EduNet.Backend.Service.DTOs.Users.Users;

public class UserForCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long BranchId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
