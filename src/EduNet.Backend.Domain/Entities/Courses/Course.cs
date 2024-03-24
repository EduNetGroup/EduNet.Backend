using EduNet.Backend.Domain.Commons;
using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Domain.Entities.Students;
using EduNet.Backend.Domain.Entities.Teachers;

namespace EduNet.Backend.Domain.Entities.Courses;

public class Course : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
    public ICollection<Enrollment> Students { get; set; }
    public ICollection<Attendance> Attendances { get; set; }
    public ICollection<TeacherCourse> Teachers { get; set; }
}
