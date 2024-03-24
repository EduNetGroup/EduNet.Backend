using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Domain.Entities.Courses;
using EduNet.Backend.Domain.Entities.Teachers;
using EduNet.Backend.Service.Interfaces.Teachers;
using EduNet.Backend.Service.DTOs.Teachers.TeacherCourses;

namespace EduNet.Backend.Service.Services.Teachers;

public class TeacherCourseService : ITeacherCourseService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Course> _courseRepository;
    private readonly IRepository<Teacher> _teacherRepository;
    private readonly IRepository<TeacherCourse> _teacherCourseRepository;

    public TeacherCourseService(
        IMapper mapper,
        IRepository<Course> courseRepository,
        IRepository<Teacher> teacherRepository,
        IRepository<TeacherCourse> teacherCourseRepository)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
        _teacherRepository = teacherRepository;
        _teacherCourseRepository = teacherCourseRepository;
    }

    public async Task<TeacherCourseForResultDto> AddAsync(TeacherCourseForCreationDto dto)
    {
        var teacherData = await _teacherRepository
            .SelectAsync(t => t.Id == dto.TeacherId);
        if (teacherData is null)
            throw new EduNetException(404, "Teacher is not found");

        var courseData = await _courseRepository
            .SelectAsync(c => c.Id == dto.CourseId);
        if (courseData is null)
            throw new EduNetException(404, "Course is not found");

        var teacherCourseData = await _teacherCourseRepository
            .SelectAsync(tch => tch.TeacherId == dto.TeacherId && tch.CourseId == dto.CourseId);
        if (teacherCourseData is not null)
            throw new EduNetException(409, "TeacherCourse is already exist");

        var mappedData = _mapper.Map<TeacherCourse>(dto);

        return _mapper.Map<TeacherCourseForResultDto>(await _teacherCourseRepository.InsertAsync(mappedData));
    }

    public async Task<TeacherCourseForResultDto> ModifyAsync(long id, TeacherCourseForUpdateDto dto)
    {
        var teacherData = await _teacherRepository
             .SelectAsync(t => t.Id == dto.TeacherId);
        if (teacherData is null)
            throw new EduNetException(404, "Teacher is not found");

        var courseData = await _courseRepository
            .SelectAsync(c => c.Id == dto.CourseId);
        if (courseData is null)
            throw new EduNetException(404, "Course is not found");

        var teacherCourseData = await _teacherCourseRepository
            .SelectAsync(tch => tch.Id == id);
        if (teacherCourseData is not null)
            throw new EduNetException(404, "TeacherCourse is not found");

        var mappedData = _mapper.Map(dto, teacherCourseData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _teacherCourseRepository.UpdateAsync(mappedData);

        return _mapper.Map<TeacherCourseForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var teacherCourseData = await _teacherCourseRepository
            .SelectAsync(tch => tch.Id == id);
        if (teacherCourseData is null)
            throw new EduNetException(404, "TeacherCourse is not found");

        return await _teacherCourseRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<TeacherCourseForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var teacherCourseData = await _teacherCourseRepository
            .SelectAll(tch => !tch.IsDeleted)
            //.Include(tch => tch.Teacher)
            //.Include(tch => tch.Course)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TeacherCourseForResultDto>>(teacherCourseData);
    }

    public async Task<TeacherCourseForResultDto> RetrieveByIdAsync(long id)
    {
        var teacherCourseData = await _teacherCourseRepository
            .SelectAll(tch => !tch.IsDeleted)
            //.Include(tch => tch.Teacher)
            //.Include(tch => tch.Course)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<TeacherCourseForResultDto>(teacherCourseData);
    }

    public async Task<IEnumerable<TeacherCourseForResultDto>> SearchAllByDateAsync(string search, PaginationParams @params)
    {
        var teacherCourseData = await _teacherCourseRepository
            .SelectAll(tch => !tch.IsDeleted)
            .Where(tch => tch.Date.ToString("dd/MM/yyyy") == search)
            .Include(tch => tch.Teacher)
            .Include(tch => tch.Course)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TeacherCourseForResultDto>>(teacherCourseData);
    }
}
