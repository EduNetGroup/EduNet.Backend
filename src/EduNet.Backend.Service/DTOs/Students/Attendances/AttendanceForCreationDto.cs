using EduNet.Backend.Domain.Enums;

namespace EduNet.Backend.Service.DTOs.Students.Attendances;

public class AttendanceForCreationDto
{
    public long StudentId { get; set; }
    public long CourseId { get; set; }
    public DateTime Date { get; set; }
    public Status Status { get; set; }
}
