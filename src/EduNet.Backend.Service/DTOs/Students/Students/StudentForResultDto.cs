using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.DTOs.Payments;
using EduNet.Backend.Service.DTOs.Students.Attendances;
using EduNet.Backend.Service.DTOs.Students.Enrollments;

namespace EduNet.Backend.Service.DTOs.Students.Students;

public class StudentForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelegramUserName { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public string PhoneNumber { get; set; }
    public string DateOfBirth { get; set; }
    public string IsGraduated { get; set; }
    public ICollection<PaymentForResultDto> Payments { get; set; }
    public ICollection<EnrollmentForResultDto> Courses { get; set; }
    public StudentProfilePhotoForResultDto ProfilePhoto { get; set; }
    public ICollection<AttendanceForResultDto> Attendances { get; set; }
}
