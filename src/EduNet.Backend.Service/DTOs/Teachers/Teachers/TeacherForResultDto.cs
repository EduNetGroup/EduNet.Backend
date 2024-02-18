using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.DTOs.Teachers.TeacherCourses;

namespace EduNet.Backend.Service.DTOs.Teachers.Teachers;

public class TeacherForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Specialization { get; set; }
    public string PhoneNumber { get; set; }
    public string DateOfBirth { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public ICollection<TeacherCourseForResultDto> Courses { get; set; }
}
