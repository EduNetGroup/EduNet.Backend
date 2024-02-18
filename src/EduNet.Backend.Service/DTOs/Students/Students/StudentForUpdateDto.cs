namespace EduNet.Backend.Service.DTOs.Students.Students;

public class StudentForUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelegramUserName { get; set; }
    public long UserId { get; set; }
    public long EnrollmentId { get; set; }
    public string PhoneNumber { get; set; }
    public string DateOfBirth { get; set; }
}
