using Microsoft.AspNetCore.Mvc;
using EduNet.Backend.Api.Models;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Api.Controllers.Commons;
using EduNet.Backend.Service.Interfaces.Teachers;
using EduNet.Backend.Service.DTOs.Teachers.TeacherCourses;

namespace EduNet.Backend.Api.Controllers.Teachers;

public class TeacherCoursesController : BaseController
{
    private readonly ITeacherCourseService _teacherCourseService;

    public TeacherCoursesController(ITeacherCourseService teacherCourseService)
    {
        _teacherCourseService = teacherCourseService;
    }

    /// <summary>
    /// To Create teacherCourse
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] TeacherCourseForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherCourseService.AddAsync(dto)
        });

    /// <summary>
    /// To Get all teacherCourses
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherCourseService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To Get teacherCourse by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherCourseService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To update teacherCourse by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] TeacherCourseForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherCourseService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To delete teacherCourse by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _teacherCourseService.RemoveAsync(id)
        });

    /// <summary>
    /// To get all teacherCourses by searching
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
            Data = await _teacherCourseService.SearchAllByDateAsync(search, @params)
        });
}
