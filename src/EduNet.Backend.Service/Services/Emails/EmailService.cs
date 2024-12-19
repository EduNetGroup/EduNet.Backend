using System.Net;
using System.Net.Mail;
using EduNet.Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Exceptions;
using Microsoft.Extensions.Configuration;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.Interfaces.Users;
using EduNet.Backend.Domain.Entities.Messages;
using EduNet.Backend.Service.Interfaces.Emails;
using EduNet.Backend.Service.DTOs.Users.UserCodes;

namespace EduNet.Backend.Service.Services.Emails;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository<User> _userRepository;
    private readonly IUserCodeService _userCodeService;
    private readonly IRepository<UserCode> _userCodeRepository;

    public EmailService(
        IConfiguration configuration,
        IUserCodeService userCodeService,
        IRepository<User> userRepository,
        IRepository<UserCode> userCodeRepository)
    {
        _userRepository = userRepository;
        _userCodeService = userCodeService;
        _userCodeRepository = userCodeRepository;
        _configuration = configuration.GetSection("Email");
    }
    public async Task<ExistEmailEnum> EmailExistence(string email)
    {
        var userData = await _userRepository
           .SelectAsync(u => u.Email == email);

        if (userData is null)
            return ExistEmailEnum.EmailNotFound;

        if (userData.IsVerified)
            return ExistEmailEnum.EmailFound;

        var resend = await ResendCodeAsync(email);

        if (!resend)
            throw new EduNetException(403, "Birozdan keyinroq qayta urinib ko'ring!");

        return ExistEmailEnum.EmailNotChecked;
    }

    public async Task<bool> ResendCodeAsync(string email)
    {
        var userCodeAny = await _userCodeRepository
            .SelectAll(u => !u.IsDeleted)
            .AnyAsync(c => c.User.Email == email && c.ExpireDate > DateTime.UtcNow);

        if (userCodeAny)
            return false;

        var user = await _userRepository
            .SelectAll(u => !u.IsDeleted)
            .Where(u => u.Email == email)
            .Select(u => new { u.Id })
            .FirstOrDefaultAsync();

        if (user is null)
            return false;

        var randomNumber = new Random().Next(100000, 999999);

        var message = new Message()
        {
            Subject = "Bu kodni boshqalarga bermang!",
            To = email,
            Body = $"Sizning tasdiqlash kodingiz: {randomNumber}"
        };


        var userCode = new UserCodeForCreationDto()
        {
            Code = randomNumber,
            UserId = user.Id,
            ExpireDate = DateTime.UtcNow.AddMinutes(3)
        };

        await _userCodeService.AddAsync(userCode);
        await this.SendMessage(message);

        return true;
    }

    public Task SendMessage(Message message)
    {
        var _smtpModel = new
        {
            Host = _configuration["Host"],
            Email = (string)_configuration["EmailAddress"],
            Port = 587,
            AppPassword = _configuration["Password"]
        };

        using (MailMessage mm = new MailMessage(_smtpModel.Email, message.To))
        {
            mm.Subject = message.Subject;
            mm.Body = message.Body;
            mm.IsBodyHtml = false;
            using (System.Net.Mail.SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = _smtpModel.Host;
                smtp.Port = _smtpModel.Port;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new NetworkCredential(_smtpModel.Email, _smtpModel.AppPassword);
                smtp.Credentials = NetworkCred;
                smtp.Send(mm);
            }
        }

        return Task.CompletedTask;
    }

    public async Task<bool> VerifyCodeAsync(string email, long code)
    {
        var userCodeAny = await _userCodeRepository
            .SelectAll(u => !u.IsDeleted)
            .Include(u => u.User)
            .AnyAsync(c => c.User.Email == email && c.ExpireDate > DateTime.UtcNow && c.Code == code);

        if (userCodeAny)
        {
            var user = await _userRepository
                .SelectAll(u => !u.IsDeleted)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
                return false;

            if (!user.IsVerified)
            {
                user.IsVerified = true;
                await _userRepository.UpdateAsync(user);
            }
            return true;
        }

        return false;
    }
}
