using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Domain.Entities.Courses;
using EduNet.Backend.Domain.Entities.Students;
using EduNet.Backend.Service.Interfaces.Students;
using EduNet.Backend.Service.DTOs.Students.Enrollments;

namespace EduNet.Backend.Service.Services.Students;

public class EnrollmentService : IEnrollmentService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Course> _coursesRepository;
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<Enrollment> _enrollmentRepository;

    public EnrollmentService(
        IMapper mapper,
        IRepository<Course> coursesRepository,
        IRepository<Student> studentRepository,
        IRepository<Enrollment> enrollmentRepository)
    {
        _mapper = mapper;
        _coursesRepository = coursesRepository;
        _studentRepository = studentRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<EnrollmentForResultDto> AddAsync(EnrollmentForCreationDto dto)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new EduNetException(404, "Student is not found");

        var courseData = await _coursesRepository
            .SelectAsync(c => c.Id == dto.CourseId);
        if (courseData is null)
            throw new EduNetException(404, "Course is not found");

        var enrollmentData = await _enrollmentRepository
            .SelectAsync(e => e.StudentId ==  dto.StudentId && e.CourseId == dto.CourseId);
        if (enrollmentData is not null)
            throw new EduNetException(409, "Enrollment is already exist");

        var mappedData = _mapper.Map<Enrollment>(dto);

        return _mapper.Map<EnrollmentForResultDto>(await _enrollmentRepository.InsertAsync(mappedData));
    }

    public async Task<EnrollmentForResultDto> ModifyAsync(long id, EnrollmentForUpdateDto dto)
    {
        var studentData = await _studentRepository
             .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new EduNetException(404, "Student is not found");

        var courseData = await _coursesRepository
            .SelectAsync(c => c.Id == dto.CourseId);
        if (courseData is null)
            throw new EduNetException(404, "Course is not found");

        var enrollmentData = await _enrollmentRepository
            .SelectAsync(e => e.Id == id);
        if (enrollmentData is not null)
            throw new EduNetException(404, "Enrollment is not found");

        var mappedData = _mapper.Map(dto, enrollmentData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _enrollmentRepository.UpdateAsync(mappedData);

        return _mapper.Map<EnrollmentForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var enrollmentData = await _enrollmentRepository
            .SelectAsync(e => e.Id == id);
        if (enrollmentData is null)
            throw new EduNetException(404, "Enrollment is not found");

        return await _enrollmentRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EnrollmentForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var enrollmentData = await _enrollmentRepository
            .SelectAll(e => !e.IsDeleted)
            //.Include(e => e.Student)
            //.Include(e => e.Course)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<EnrollmentForResultDto>>(enrollmentData);
    }

    public async Task<EnrollmentForResultDto> RetrieveByIdAsync(long id)
    {
        var enrollmentData = await _enrollmentRepository
            .SelectAll(e => !e.IsDeleted)
            //.Include(e => e.Student)
            //.Include(e => e.Course)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<EnrollmentForResultDto>(enrollmentData);
    }

    public async Task<IEnumerable<EnrollmentForResultDto>> SearchAllByDateAsync(string search, PaginationParams @params)
    {
        var enrollmentData = await _enrollmentRepository
            .SelectAll(e => !e.IsDeleted)
            .Where(e => e.EnrollmentDate.ToString("dd/MM/yyyy") == search)
            .Include(e => e.Student)
            .Include(e => e.Course)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<EnrollmentForResultDto>>(enrollmentData);
    }
}
