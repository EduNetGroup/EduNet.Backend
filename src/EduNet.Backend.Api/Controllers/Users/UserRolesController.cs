using Microsoft.AspNetCore.Mvc;
using EduNet.Backend.Api.Models;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Api.Controllers.Commons;
using EduNet.Backend.Service.Interfaces.Users;
using EduNet.Backend.Service.DTOs.Users.UserRoles;

namespace EduNet.Backend.Api.Controllers.Users;

public class UserRolesController : BaseController
{
    private readonly IUserRoleService _userRoleService;

    public UserRolesController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    /// <summary>
    /// To Add userRole
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] UserRoleForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userRoleService.AddAsync(dto)
        });

    /// <summary>
    /// To Get all userRole 
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userRoleService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To Get userRole by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userRoleService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To Update userRole by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] UserRoleForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userRoleService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To Delete userRole by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userRoleService.RemoveAsync(id)
        });

    /// <summary>
    /// To Get userRole by searching
    /// </summary>
    /// <param name="search"></param>
    /// <param name="params"></param>
    /// <returns></returns>
    //[HttpGet("{search}")]
    //public async Task<IActionResult> GetAllBySearchAsync([FromQuery(Name = "search")] string search, [FromQuery] PaginationParams @params)
    //    => Ok(new Response
    //    {
    //        StatusCode = 200,
    //        Message = "Success",
    //        Data = await _userRoleService.SearchAllAsync(search, @params)
    //    });
}
