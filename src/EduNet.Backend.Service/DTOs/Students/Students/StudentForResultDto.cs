using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.DTOs.Payments;
using EduNet.Backend.Domain.Entities.Students;
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
    public long EnrollmentId { get; set; }
    public Enrollment Enrollment { get; set; }
    public string PhoneNumber { get; set; }
    public string DateOfBirth { get; set; }
    public ICollection<PaymentForResultDto> Payments { get; set; }
    public ICollection<AttendanceForResultDto> Attendances { get; set; }
    public ICollection<EnrollmentForResultDto> Enrollments { get; set; }
}
