using EduNet.Backend.Domain.Commons;

namespace EduNet.Backend.Domain.Entities.Messages;

public class Message : Auditable
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string To { get; set; }
}
