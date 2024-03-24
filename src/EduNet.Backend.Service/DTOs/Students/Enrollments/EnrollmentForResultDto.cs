using EduNet.Backend.Domain.Entities.Courses;
using EduNet.Backend.Domain.Entities.Students;

namespace EduNet.Backend.Service.DTOs.Students.Enrollments;

public class EnrollmentForResultDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
