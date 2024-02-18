using System.ComponentModel.DataAnnotations;

namespace EduNet.Backend.Service.DTOs.Students.Enrollments;

public class EnrollmentForCreationDto
{
    [Required]
    public long StudentId { get; set; }
    [Required]
    public long CourseId { get; set; }
    [Required]
    public string EnrollmentDate { get; set; }
}
