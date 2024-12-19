using Microsoft.AspNetCore.Mvc;
using EduNet.Backend.Api.Models;
using System.ComponentModel.DataAnnotations;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Api.Controllers.Commons;
using EduNet.Backend.Service.Interfaces.Teachers;
using EduNet.Backend.Service.DTOs.Teachers.Teachers;

namespace EduNet.Backend.Api.Controllers.Teachers;

public class TeachersController : BaseController
{
    private readonly ITeacherService _teacherService;

    public TeachersController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    /// <summary>
    /// To Create teacher
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] TeacherForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherService.AddAsync(dto)
        });

    /// <summary>
    /// To Get all teachers
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To Get teacher by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To update teacher by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] TeacherForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To delete teacher by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherService.RemoveAsync(id)
        });

    /// <summary>
    /// To get all teachers by searching
    /// </summary>
    /// <param name="search"></param>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet("search")]
    public async Task<IActionResult> SearchAllAsync([FromQuery(Name = "search")] string search, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherService.SearchAllAsync(search, @params)
        });

    /// <summary>
    /// To Add profile photo by teacherId
    /// </summary>
    /// <param name="teacherId"></param>
    /// <param name="formFile"></param>
    /// <returns></returns>
    [HttpPost("{teacherId}")]
    public async Task<IActionResult> AddProfilePhotoAsync([FromRoute(Name = "teacherId")] long teacherId, [Required] FormFile formFile)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherService.AddProfilePhotoAsync(teacherId, formFile)
        });

    /// <summary>
    /// To Delete profile photo by teacherId
    /// </summary>
    /// <param name="teacherId"></param>
    /// <returns></returns>
    [HttpDelete("teacher/{teacherId}/photo")]
    public async Task<IActionResult> DeleteProfilePhotoAsync([FromRoute(Name = "teacherId")] long teacherId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherService.RemoveProfilePhotoAsync(teacherId)
        });

    /// <summary>
    /// To Get profile photo by teacherId
    /// </summary>
    /// <param name="teacherId"></param>
    /// <returns></returns>
    [HttpGet("teacher/{teacherId}/photo")]
    public async Task<IActionResult> GetProfilePhotoAsync([FromRoute(Name = "teacherId")] long teacherId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherService.RetrieveProfilePhotoAsync(teacherId)
        });
}
