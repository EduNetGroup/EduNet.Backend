namespace EduNet.Backend.Domain.Commons;

public abstract class Auditable
{
    public long Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
