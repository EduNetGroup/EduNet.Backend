namespace EduNet.Backend.Service.DTOs.Students.Enrollments;

public class EnrollmentForUpdateDto
{
    public long StudentId { get; set; }
    public long CourseId { get; set; }
    public string EnrollmentDate { get; set; }
}
