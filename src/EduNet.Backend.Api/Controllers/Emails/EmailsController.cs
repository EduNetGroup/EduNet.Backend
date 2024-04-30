using Microsoft.AspNetCore.Mvc;
using EduNet.Backend.Api.Controllers.Commons;
using EduNet.Backend.Service.Interfaces.Emails;

namespace EduNet.Backend.Api.Controllers.Emails;

public class EmailsController : BaseController
{
    private readonly IEmailService _emailService;
    public EmailsController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("Check")]
    public async Task<IActionResult> CheckEmail(string email)
    {
        var result = await _emailService.EmailExistence(email);
        return Ok(new
        {
            result = result.ToString(),
            resultCode = (int)result
        });
    }

    [HttpPost("Verify/Code")]
    public async Task<IActionResult> VerifyCode(string email, long code)
    {
        var res = await _emailService.VerifyCodeAsync(email, code);
        return Ok(new
        {
            verified = res
        });
    }
}
