using EduNet.Backend.Service.DTOs.Users.Users;
using EduNet.Backend.Service.DTOs.Courses.Courses;

namespace EduNet.Backend.Service.DTOs.Branches;

public class BranchForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public ICollection<UserForResultDto> Users { get; set; }
    public ICollection<CourseForResultDto> Courses { get; set; }
}
