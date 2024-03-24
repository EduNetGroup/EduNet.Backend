using EduNet.Backend.Domain.Entities.Assets;

namespace EduNet.Backend.Domain.Entities.Students;

public class StudentProfilePhoto : Asset
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
}
