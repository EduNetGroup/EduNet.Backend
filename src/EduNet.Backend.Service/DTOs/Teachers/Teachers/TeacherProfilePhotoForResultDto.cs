namespace EduNet.Backend.Service.DTOs.Teachers.Teachers;

public class TeacherProfilePhotoForResultDto
{
    public long Id { get; set; }
    public long TeacherId { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public long Size { get; set; }
    public string Type { get; set; }
}
