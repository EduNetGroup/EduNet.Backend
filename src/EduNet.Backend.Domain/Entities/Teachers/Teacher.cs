using EduNet.Backend.Domain.Commons;
using EduNet.Backend.Domain.Entities.Users;

namespace EduNet.Backend.Domain.Entities.Teachers;

public class Teacher : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Specialization { get; set; }
    public string PhoneNumber { get; set; }
    public string TelegramUserName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public ICollection<TeacherCourse> Courses { get; set; }
    public TeacherProfilePhoto TeacherProfilePhoto { get; set; }
}
