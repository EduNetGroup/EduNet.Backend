namespace EduNet.Backend.Service.DTOs.Courses.Courses;

public class CourseForUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string StartTime { get; set; }
    public string DurationTime { get; set; }
    public long BranchId { get; set; }
    public short Duration { get; set; }
}
