using EduNet.Backend.Domain.Enums;
using EduNet.Backend.Domain.Entities.Messages;

namespace EduNet.Backend.Service.Interfaces.Emails;

public interface IEmailService
{
    public Task SendMessage(Message message);
    public Task<bool> ResendCodeAsync(string email);
    public Task<ExistEmailEnum> EmailExistence(string email);
    public Task<bool> VerifyCodeAsync(string email, long code);
}
