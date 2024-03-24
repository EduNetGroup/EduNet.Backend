using EduNet.Backend.Domain.Entities.Courses;

namespace EduNet.Backend.Service.DTOs.Courses.Lessons;

public class LessonForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; }
}
