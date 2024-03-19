using Microsoft.AspNetCore.Mvc;
using EduNet.Backend.Api.Models;
using EduNet.Backend.Service.DTOs.Payments;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Api.Controllers.Commons;
using EduNet.Backend.Service.Interfaces.Payments;

namespace EduNet.Backend.Api.Controllers.Payments;

public class PaymentsController : BaseController
{
    private readonly IPaymentService _paymentsService;

    public PaymentsController(IPaymentService paymentsService)
    {
        _paymentsService = paymentsService;
    }

    /// <summary>
    /// To Add payment
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] PaymentForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentsService.AddAsync(dto)
        });

    /// <summary>
    /// To Get all payments
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentsService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To Get All payments by studentId 
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet("{studentId")]
    public async Task<IActionResult> GetAllByStudentIdAsync([FromRoute(Name = "studentId")] long studentId, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentsService.RetrieveAllByStudentIdAsync(studentId, @params)
        });

    /// <summary>
    /// To Get by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentsService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To Update by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] PaymentForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentsService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To Delete by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentsService.RemoveAsync(id)
        });

    /// <summary>
    /// To Get payments by searching payment date
    /// </summary>
    /// <param name="search"></param>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet("search")]
    public async Task<IActionResult> SearchByPaymentDateAsync([FromQuery(Name = "search")] string search, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentsService.SearchAllAsync(search, @params)
        });
}
