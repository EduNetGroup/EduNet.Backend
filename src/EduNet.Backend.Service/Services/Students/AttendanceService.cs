using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Domain.Entities.Courses;
using EduNet.Backend.Domain.Entities.Students;
using EduNet.Backend.Service.Interfaces.Students;
using EduNet.Backend.Service.DTOs.Students.Attendances;

namespace EduNet.Backend.Service.Services.Students;

public class AttendanceService : IAttendanceService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Course> _courseRepository;
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<Attendance> _attendanceRepository;

    public AttendanceService(
        IMapper mapper,
        IRepository<Course> courseRepository,
        IRepository<Student> studentRepository,
        IRepository<Attendance> attendanceRepository)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
        _attendanceRepository = attendanceRepository;
    }

    public async Task<AttendanceForResultDto> AddAsync(AttendanceForCreationDto dto)
    {
        var courseData = await _courseRepository
            .SelectAsync(c => c.Id == dto.CourseId);
        if (courseData is null)
            throw new EduNetException(404, "Course is not found");

        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new EduNetException(404, "Student is not found");

        var attendanceData = await _attendanceRepository
            .SelectAsync(a => a.StudentId == dto.StudentId && a.CourseId == dto.CourseId);
        if (attendanceData is not null)
            throw new EduNetException(409, "Attendance is already exist");

        var mappedData = _mapper.Map<Attendance>(dto);

        return _mapper.Map<AttendanceForResultDto>(await _attendanceRepository.InsertAsync(mappedData));
    }

    public async Task<AttendanceForResultDto> ModifyAsync(long id, AttendanceForUpdateDto dto)
    {
        var courseData = await _courseRepository
           .SelectAsync(c => c.Id == dto.CourseId);
        if (courseData is null)
            throw new EduNetException(404, "Course is not found");

        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new EduNetException(404, "Student is not found");

        var attendanceData = await _attendanceRepository
            .SelectAsync(a => a.Id == id);
        if (attendanceData is null)
            throw new EduNetException(404, "Attendance is not found");

        var mappedData = _mapper.Map(dto, attendanceData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _attendanceRepository.UpdateAsync(mappedData);

        return _mapper.Map<AttendanceForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var attendanceData = await _attendanceRepository
            .SelectAsync(a => a.Id == id);
        if (attendanceData is null)
            throw new EduNetException(404, "Attendance is not found");

        return await _attendanceRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AttendanceForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var attendanceData = await _attendanceRepository
             .SelectAll(a => !a.IsDeleted)
             .Include(a => a.Student)
             .Include(a => a.Course)
             .AsNoTracking()
             .ToPagedList(@params)
             .ToListAsync();

        return _mapper.Map<IEnumerable<AttendanceForResultDto>>(attendanceData);
    }

    public async Task<AttendanceForResultDto> RetrieveByIdAsync(long id)
    {
        var attendanceData = await _attendanceRepository
            .SelectAll(a => !a.IsDeleted)
            .Where(a => a.Id == id)
            .Include(a => a.Student)
            .Include(a => a.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (attendanceData is null)
            throw new EduNetException(404, "Attendance is not found");
    }

    public async Task<IEnumerable<AttendanceForResultDto>> SearchAllByDateAsync(string search, PaginationParams @params)
    {
        var attendanceData = await _attendanceRepository
            .SelectAll(a => !a.IsDeleted)
            .Where(a => a.Date.ToString("dd/MM/yyyy") == search)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AttendanceForResultDto>>(attendanceData);
    }
}
