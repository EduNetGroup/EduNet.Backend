using EduNet.Backend.Domain.Commons;

namespace EduNet.Backend.Domain.Entities.Courses;

public class Lesson : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; }
}
