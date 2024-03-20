using Microsoft.AspNetCore.Mvc;
using EduNet.Backend.Api.Models;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Api.Controllers.Commons;
using EduNet.Backend.Service.Interfaces.Roles;
using EduNet.Backend.Service.DTOs.Roles.RolePermissions;

namespace EduNet.Backend.Api.Controllers.Roles;

public class RolePermissionsController : BaseController
{
    private readonly IRolePermissionService _rolePermissionService;
    public RolePermissionsController(IRolePermissionService rolePermissionService)
    {
        _rolePermissionService = rolePermissionService;
    }


    /// <summary>
    /// To Add rolePermission 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] RolePermissionForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _rolePermissionService.AddAsync(dto)
        });

    /// <summary>
    /// To Get all rolePermissions 
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _rolePermissionService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To Get rolePermission by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _rolePermissionService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To Update rolePermission by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] RolePermissionForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _rolePermissionService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To Delete rolePermission by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _rolePermissionService.RemoveAsync(id)
        });

    /// <summary>
    /// To Get rolePermissions by searching
    /// </summary>
    /// <param name="search"></param>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet("{search}")]
    public async Task<IActionResult> GetAllBySearchAsync([FromQuery(Name = "search")] string search, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _rolePermissionService.SearchAllAsync(search, @params)
        });
}
