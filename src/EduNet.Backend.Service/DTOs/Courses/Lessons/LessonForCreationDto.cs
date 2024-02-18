namespace EduNet.Backend.Service.DTOs.Courses.Lessons;

public class LessonForCreationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public long CourseId { get; set; }
    public string Date { get; set; }
}
