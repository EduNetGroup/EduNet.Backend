using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Domain.Entities.Courses;
using EduNet.Backend.Service.Interfaces.Courses;
using EduNet.Backend.Service.DTOs.Courses.Lessons;

namespace EduNet.Backend.Service.Services.Courses;

public class LessonService : ILessonService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Course> _courseRepository;
    private readonly IRepository<Lesson> _lessonRepository;
    public LessonService(
        IMapper mapper,
        IRepository<Course> courseRepository,
        IRepository<Lesson> lessonRepository)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<LessonForResultDto> AddAsync(LessonForCreationDto dto)
    {
        var courseData = await _courseRepository
            .SelectAsync(c => c.Id == dto.CourseId && !c.IsDeleted);
        if (courseData is null)
            throw new EduNetException(404, "Course is not found");

        var lessonData = await _courseRepository
            .SelectAsync(l => l.Name.ToLower() == dto.Name.ToLower());
        if (lessonData is not null)
            throw new EduNetException(404, "Lesson is already exist");

        var mappedData = _mapper.Map<Lesson>(dto);

        return _mapper.Map<LessonForResultDto>(await _lessonRepository.InsertAsync(mappedData));
    }

    public async Task<LessonForResultDto> ModifyAsync(long id, LessonForUpdateDto dto)
    {
        var courseData = await _courseRepository
            .SelectAsync(c => c.Id == dto.CourseId && !c.IsDeleted);
        if (courseData is null)
            throw new EduNetException(404, "Course is not found");

        var lessonData = await _lessonRepository
            .SelectAsync(c => c.Id == id && !c.IsDeleted);
        if (lessonData is null)
            throw new EduNetException(404, "Lesson is not found");

        var mappedData = _mapper.Map(dto, lessonData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _lessonRepository.UpdateAsync(mappedData);

        return _mapper.Map<LessonForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var lessonData = await _lessonRepository
            .SelectAsync(l => l.Id == id && !l.IsDeleted);
        if (lessonData is null)
            throw new EduNetException(404, "Lesson is not found");

        return await _lessonRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<LessonForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var lessonData = await _lessonRepository
            .SelectAll(l => !l.IsDeleted)
            .Include(l => l.Course)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<LessonForResultDto>>(lessonData);
    }

    public async Task<LessonForResultDto> RetrieveByIdAsync(long id)
    {
        var lessonData = await _lessonRepository
            .SelectAll(l => !l.IsDeleted)
            .Where(l => l.Id == id && l.Course.IsDeleted == false)
            .Include(c => c.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (lessonData is null)
            throw new EduNetException(404, "Lesson is not found");

        return _mapper.Map<LessonForResultDto>(lessonData);
    }

    public async Task<IEnumerable<LessonForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var lessonData = await _lessonRepository
           .SelectAll(l => !l.IsDeleted)
           .Where(l => l.Name.ToLower().Contains(search.ToLower())
               || l.Description.ToLower().Contains(search.ToLower())
               || l.Date.ToString().Contains(search))
           .AsNoTracking()
           .ToPagedList(@params)
           .ToListAsync();

        return _mapper.Map<IEnumerable<LessonForResultDto>>(lessonData);
    }
}
