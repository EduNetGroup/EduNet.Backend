namespace EduNet.Backend.Service.DTOs.Teachers.Teachers;

public class TeacherForUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Specialization { get; set; }
    public string PhoneNumber { get; set; }
    public string TelegramUserName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public long UserId { get; set; }
}
