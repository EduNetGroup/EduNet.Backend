using EduNet.Backend.Domain.Commons;
using EduNet.Backend.Domain.Entities.Courses;

namespace EduNet.Backend.Domain.Entities.Students;

public class Enrollment : Auditable
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public string EnrollmentDate { get; set; }
}
