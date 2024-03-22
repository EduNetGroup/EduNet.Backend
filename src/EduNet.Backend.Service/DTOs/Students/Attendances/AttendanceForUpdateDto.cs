using EduNet.Backend.Domain.Enums;

namespace EduNet.Backend.Service.DTOs.Students.Attendances;

public class AttendanceForUpdateDto
{
    public long StudentId { get; set; }
    public long CourseId { get; set; }
    public Status Status { get; set; }
}
