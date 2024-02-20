using EduNet.Backend.Domain.Entities.Assets;

namespace EduNet.Backend.Domain.Entities.Teachers;

public class TeacherProfilePhoto : Asset
{
    public long TeacherId { get; set; }
    public Teacher Teacher { get; set; }
}
