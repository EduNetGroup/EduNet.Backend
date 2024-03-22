using EduNet.Backend.Domain.Entities.Branches;

namespace EduNet.Backend.Service.DTOs.Courses.Courses;

public class CourseForUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public DateTime EndDate { get; set; }
}
