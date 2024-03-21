using EduNet.Backend.Domain.Enums;
using EduNet.Backend.Domain.Commons;
using EduNet.Backend.Domain.Entities.Courses;

namespace EduNet.Backend.Domain.Entities.Students;

public class Attendance : Auditable
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; }
    public Status Status { get; set; }

}
