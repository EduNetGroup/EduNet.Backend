using EduNet.Backend.Api.Controllers.Commons;
using EduNet.Backend.Api.Models;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Branches;
using EduNet.Backend.Service.DTOs.Students.Enrollments;
using EduNet.Backend.Service.Interfaces.Students;
using Microsoft.AspNetCore.Mvc;

namespace EduNet.Backend.Api.Controllers.Students;

public class EnrollmentsController : BaseController
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    /// <summary>
    /// To Create enrollment
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] EnrollmentForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _enrollmentService.AddAsync(dto)
        });

    /// <summary>
    /// To Get all enrollments
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _enrollmentService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To Get enrollment by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _enrollmentService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To update enrollment by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] EnrollmentForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _enrollmentService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To delete enrollment by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _enrollmentService.RemoveAsync(id)
        });

    /// <summary>
    /// To get all enrollments by searching
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
            Data = await _enrollmentService.SearchAllByDateAsync(search, @params)
        });
}
