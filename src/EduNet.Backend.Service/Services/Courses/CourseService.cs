﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Domain.Entities.Courses;
using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Service.Interfaces.Courses;
using EduNet.Backend.Service.DTOs.Courses.Courses;

namespace EduNet.Backend.Service.Services.Courses;

public class CourseService : ICourseService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<Course> _courseRepository;

    public CourseService(
        IMapper mapper,
        IRepository<Branch> branchRepository,
        IRepository<Course> courseRepository)
    {
        _mapper = mapper;
        _branchRepository = branchRepository;
        _courseRepository = courseRepository;
    }

    public async Task<CourseForResultDto> AddAsync(CourseForCreationDto dto)
    {
        var branchData = await _branchRepository
           .SelectAsync(b => b.Id == dto.BranchId && !b.IsDeleted);
        if (branchData is null)
            throw new EduNetException(404, "Branch is not found");

        var courseData = await _courseRepository
            .SelectAsync(c => !c.IsDeleted && c.Name.ToLower() == dto.Name.ToLower());
        if (courseData is not null)
            throw new EduNetException(409, "Course is already exist");

        var mappedData = _mapper.Map<Course>(dto);

        return _mapper.Map<CourseForResultDto>(await _courseRepository.InsertAsync(mappedData));
    }

    public async Task<CourseForResultDto> ModifyAsync(long id, CourseForUpdateDto dto)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == dto.BranchId && !b.IsDeleted);
        if (branchData is null)
            throw new EduNetException(404, "Branch is not found");

        var courseData = await _courseRepository
            .SelectAsync(c => c.Id == id && !c.IsDeleted);
        if (courseData is null)
            throw new EduNetException(404, "Course is not found");

        var mappedData = _mapper.Map(dto, courseData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _courseRepository.UpdateAsync(mappedData);

        return _mapper.Map<CourseForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var courseData = await _courseRepository
            .SelectAsync(c => c.Id == id && !c.IsDeleted);
        if (courseData is null)
            throw new EduNetException(404, "Course is not found");

        return await _courseRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<CourseForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var courseData = await _courseRepository
            .SelectAll(c => !c.IsDeleted)
            .Include(c => c.Teachers.Where(t => !t.IsDeleted))
            .Include(c => c.Students.Where(s => !s.IsDeleted))
            .Include(c => c.Lessons.Where(l => !l.IsDeleted))
            .Include(c => c.Attendances.Where(s => !s.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CourseForResultDto>>(courseData);
    }

    public async Task<CourseForResultDto> RetrieveByIdAsync(long id)
    {
        var courseData = await _courseRepository
            .SelectAll(c => !c.IsDeleted)
            .Where(c => c.Id == id)
            .Include(c => c.Teachers.Where(t => !t.IsDeleted))
            .Include(c => c.Students.Where(s => !s.IsDeleted))
            .Include(c => c.Lessons.Where(l => !l.IsDeleted))
            .Include(c => c.Attendances.Where(s => !s.IsDeleted))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (courseData is null)
            throw new EduNetException(404, "Course is not found");

        return _mapper.Map<CourseForResultDto>(courseData);
    }

    public async Task<IEnumerable<CourseForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var courseData = await _courseRepository
            .SelectAll(c => !c.IsDeleted)
            .Where(c => c.Name.ToLower().Contains(search.ToLower())
                || c.Description.ToLower().Contains(search.ToLower()))
            .Include(c => c.Teachers.Where(t => !t.IsDeleted))
            .Include(c => c.Students.Where(s => !s.IsDeleted))
            .Include(c => c.Lessons.Where(l => !l.IsDeleted))
            .Include(c => c.Attendances.Where(s => !s.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CourseForResultDto>>(courseData);
    }
}
