using EduNet.Backend.Domain.Commons;

namespace EduNet.Backend.Domain.Entities.Assets;

public abstract class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public long Size { get; set; }
    public string Type { get; set; }
}
