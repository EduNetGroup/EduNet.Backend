using EduNet.Backend.Domain.Commons;
using EduNet.Backend.Domain.Entities.Courses;

namespace EduNet.Backend.Domain.Entities.Teachers;

public class TeacherCourse : Auditable
{
    public long TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; }
}
