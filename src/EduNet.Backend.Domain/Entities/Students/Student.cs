using EduNet.Backend.Domain.Commons;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Domain.Entities.Payments;

namespace EduNet.Backend.Domain.Entities.Students;

public class Student : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelegramUserName { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsGraduated { get; set; } = false;
    public ICollection<Payment> Payments { get; set; }
    public ICollection<Enrollment> Courses { get; set; }
    public ICollection<Attendance> Attendances { get; set; }
    public StudentProfilePhoto StudentProfilePhoto { get; set; }
}
