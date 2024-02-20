namespace EduNet.Backend.Service.DTOs.Students.Students;

public class StudentProfilePhotoForResultDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public long Size { get; set; }
    public string Type { get; set; }
}
