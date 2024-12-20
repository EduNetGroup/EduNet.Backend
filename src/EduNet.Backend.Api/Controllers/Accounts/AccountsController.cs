﻿using Microsoft.AspNetCore.Mvc;
using EduNet.Backend.Api.Models;
using EduNet.Backend.Service.DTOs.Logins;
using EduNet.Backend.Api.Controllers.Commons;
using EduNet.Backend.Service.DTOs.Users.Users;
using EduNet.Backend.Service.Interfaces.Accounts;

namespace EduNet.Backend.Api.Controllers.Accounts;

public class AccountsController : BaseController
{
    private readonly IAccountService _accountService;
    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(LoginForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _accountService.LoginAsync(dto)
        });

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePasswordAsync(string email, string password)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _accountService.ChangePassword(email, password)
        });

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserForCreationDto userModel)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _accountService.CreateAsync(userModel)
        });
}
