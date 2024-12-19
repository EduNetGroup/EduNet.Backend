using Microsoft.AspNetCore.Mvc;
using EduNet.Backend.Api.Models;
using System.ComponentModel.DataAnnotations;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Api.Controllers.Commons;
using EduNet.Backend.Service.Interfaces.Students;
using EduNet.Backend.Service.DTOs.Students.Students;

namespace EduNet.Backend.Api.Controllers.Students;

public class StudentsController : BaseController
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// To Create student
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] StudentForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.AddAsync(dto)
        });

    /// <summary>
    /// To Get all students
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To Get student by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To update student by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] StudentForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To delete student by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RemoveAsync(id)
        });

    /// <summary>
    /// To get all students by searching
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
            Data = await _studentService.SearchAllAsync(search, @params)
        });

    /// <summary>
    /// To Add profile photo 
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="formFile"></param>
    /// <returns></returns>
    [HttpPost("{studentId}")]
    public async Task<IActionResult> AddProfilePhotoAsync([FromRoute(Name = "studentId")] long studentId, [Required] FormFile formFile)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.AddProfilePhotoAsync(studentId, formFile)
        });

    /// <summary>
    /// To Delete profile photo
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    [HttpDelete("student/{studentId}")]
    public async Task<IActionResult> DeleteProfilePhotoAsync([FromRoute(Name = "studentId")] long studentId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RemoveProfilePhotoAsync(studentId)
        });

    /// <summary>
    /// To Get profile photo by studentId
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    [HttpGet("student/{studentId}/photo")]
    public async Task<IActionResult> GetProfilePhotoAsync([FromRoute(Name = "studentId")] long studentId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RetrieveProfilePhotoAsync(studentId)
        });
}
