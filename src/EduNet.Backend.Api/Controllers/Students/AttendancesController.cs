using Microsoft.AspNetCore.Mvc;
using EduNet.Backend.Api.Models;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Api.Controllers.Commons;
using EduNet.Backend.Service.Interfaces.Students;
using EduNet.Backend.Service.DTOs.Students.Attendances;

namespace EduNet.Backend.Api.Controllers.Students;

public class AttendancesController : BaseController
{
    private readonly IAttendanceService _attendanceService;

    public AttendancesController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    /// <summary>
    /// To Create attendance
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AttendanceForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _attendanceService.AddAsync(dto)
        });

    /// <summary>
    /// To Get all attendances
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _attendanceService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To Get attendance by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _attendanceService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To update attendance by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] AttendanceForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _attendanceService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To delete attendance by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _attendanceService.RemoveAsync(id)
        });

    /// <summary>
    /// To get all attendances by searching
    /// </summary>
    /// <param name="search"></param>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet("search")]
    public async Task<IActionResult> SearchAllByDateAsync([FromQuery(Name = "search")] string search, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _attendanceService.SearchAllByDateAsync(search, @params)
        });
}
