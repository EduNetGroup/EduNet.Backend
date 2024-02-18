using EduNet.Backend.Domain.Entities.Courses;
using EduNet.Backend.Domain.Entities.Teachers;

namespace EduNet.Backend.Service.DTOs.Teachers.TeacherCourses;

public class TeacherCourseForResultDto
{
    public long Id { get; set; }
    public long TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
}
