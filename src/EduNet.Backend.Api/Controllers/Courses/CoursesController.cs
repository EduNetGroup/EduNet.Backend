using Microsoft.AspNetCore.Mvc;
using EduNet.Backend.Api.Models;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Api.Controllers.Commons;
using EduNet.Backend.Service.Interfaces.Courses;
using EduNet.Backend.Service.DTOs.Courses.Courses;

namespace EduNet.Backend.Api.Controllers.Courses;

public class CoursesController : BaseController
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    /// <summary>
    /// To Create course
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CourseForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.AddAsync(dto)
        });

    /// <summary>
    /// To Get all courses
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To Get course by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To update course by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] CourseForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To delete course by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.RemoveAsync(id)
        });

    /// <summary>
    /// To get all courses by searching
    /// </summary>
    /// <param name="search"></param>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet("search")]
    public async Task<IActionResult> GetAllBySearchAsync([FromQuery(Name = "search")] string search, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.SearchAllAsync(search, @params)
        });
}
