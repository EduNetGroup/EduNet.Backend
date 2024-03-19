using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Service.DTOs.Payments;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Domain.Entities.Students;
using EduNet.Backend.Domain.Entities.Payments;
using EduNet.Backend.Service.Interfaces.Payments;

namespace EduNet.Backend.Service.Services.Payments;

public class PaymentService : IPaymentService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<Payment> _paymentRepository;
    private readonly IRepository<Student> _studentRepository;

    public PaymentService(
        IMapper mapper,
        IRepository<Branch> branchRepository,
        IRepository<Payment> paymentRepository,
        IRepository<Student> studentRepository)
    {
        _mapper = mapper;
        _branchRepository = branchRepository;
        _paymentRepository = paymentRepository;
        _studentRepository = studentRepository;
    }

    public async Task<PaymentForResultDto> AddAsync(PaymentForCreationDto dto)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new EduNetException(404, "Branch is not found");

        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new EduNetException(404, "Student is not found");

        var paymentData = await _paymentRepository
            .SelectAsync(p => p.StudentId == dto.StudentId && p.BranchId == dto.BranchId);
        if (paymentData is not null)
            throw new EduNetException(404, "Payment is already exist");

        var mappedData = _mapper.Map<Payment>(dto);

        return _mapper.Map<PaymentForResultDto>(await _paymentRepository.InsertAsync(mappedData));
    }

    public async Task<PaymentForResultDto> ModifyAsync(long id, PaymentForUpdateDto dto)
    {
        var branchData = await _branchRepository
           .SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new EduNetException(404, "Branch is not found");

        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new EduNetException(404, "Student is not found");

        var paymentData = await _paymentRepository
            .SelectAsync(p => p.Id == id);
        if (paymentData is null)
            throw new EduNetException(404, "Payment is not found");

        var mappedData = _mapper.Map(dto, paymentData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _paymentRepository.UpdateAsync(mappedData);

        return _mapper.Map<PaymentForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var paymentData = await _paymentRepository
            .SelectAsync(p => p.Id == id);
        if (paymentData is null)
            throw new EduNetException(404, "Payment is not found");

        return await _paymentRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<PaymentForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var paymentData = await _paymentRepository
            .SelectAll(p => !p.IsDeleted)
            //.Include(p => p.Student)
            //.Include(p => p.Branch)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PaymentForResultDto>>(paymentData);
    }

    public async Task<IEnumerable<PaymentForResultDto>> RetrieveAllByStudentIdAsync(long studentId, PaginationParams @params)
    {
        var paymentData = await _paymentRepository
            .SelectAll(p => !p.IsDeleted)
            .Where(p => p.StudentId == studentId)
            .Include(p => p.Student)
            .Include(p => p.Branch)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PaymentForResultDto>>(paymentData);
    }

    public async Task<PaymentForResultDto> RetrieveByIdAsync(long id)
    {
        var paymentData = await _paymentRepository
            .SelectAll(p => !p.IsDeleted)
            //.Include(p => p.Student)
            //.Include(p => p.Branch)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (paymentData is null)
            throw new EduNetException(404, "Payment is not found");

        return _mapper.Map<PaymentForResultDto>(paymentData);
    }

    public async Task<IEnumerable<PaymentForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var paymentData = await _paymentRepository
            .SelectAll(p => !p.IsDeleted)
            .Where(p => p.Date.ToString().Contains(search.ToString()))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PaymentForResultDto>>(paymentData);
    }
}