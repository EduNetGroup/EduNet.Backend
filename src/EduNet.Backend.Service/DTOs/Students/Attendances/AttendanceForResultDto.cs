using EduNet.Backend.Domain.Enums;
using EduNet.Backend.Domain.Entities.Courses;
using EduNet.Backend.Domain.Entities.Students;

namespace EduNet.Backend.Service.DTOs.Students.Attendances;

public class AttendanceForResultDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public string Date { get; set; }
    public Status Status { get; set; }
}
