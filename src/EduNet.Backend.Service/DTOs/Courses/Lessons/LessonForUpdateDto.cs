namespace EduNet.Backend.Service.DTOs.Courses.Lessons;

public class LessonForUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public long CourseId { get; set; }
    public DateTime Date { get; set; }
}
