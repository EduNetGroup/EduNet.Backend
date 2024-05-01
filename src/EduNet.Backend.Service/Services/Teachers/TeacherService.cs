using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Service.Helpers;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Domain.Entities.Teachers;
using EduNet.Backend.Domain.Entities.Branches;
using EduNet.Backend.Service.Interfaces.Teachers;
using EduNet.Backend.Service.DTOs.Teachers.Teachers;

namespace EduNet.Backend.Service.Services.Teachers;

public class TeacherService : ITeacherService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<Teacher> _teacherRepository;
    private readonly IRepository<TeacherProfilePhoto> _teacherProfilePhotoRepository;

    public TeacherService(
        IMapper mapper,
        IRepository<User> userRepository,
        IRepository<Branch> branchRepository,
        IRepository<Teacher> teacherRepository,
        IRepository<TeacherProfilePhoto> teacherProfilePhotoRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _branchRepository = branchRepository;
        _teacherRepository = teacherRepository;
        _teacherProfilePhotoRepository = teacherProfilePhotoRepository;
    }
    public async Task<TeacherForResultDto> AddAsync(TeacherForCreationDto dto)
    {
        var userData = await _userRepository
            .SelectAsync(u => u.Id == dto.UserId);
        if (userData is null)
            throw new EduNetException(404, "User is not found");

        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new EduNetException(404, "Branch is not found");
        
        userData.BranchId = dto.BranchId;
        await _userRepository.UpdateAsync(userData);

        var teacherData = await _teacherRepository
            .SelectAsync(t => t.FirstName.ToLower() == dto.FirstName.ToLower()
                                    && t.LastName.ToLower() == dto.LastName.ToLower()
                                    && t.PhoneNumber == dto.PhoneNumber);
        if (teacherData is not null)
            throw new EduNetException(409, "Teacher is already exist");

        var mappedData = _mapper.Map<Teacher>(dto);

        return _mapper.Map<TeacherForResultDto>(await _teacherRepository.InsertAsync(mappedData));
    }

    public async Task<TeacherProfilePhotoForResultDto> AddProfilePhotoAsync(long teacherId, IFormFile formFile)
    {
        var teacherData = await _teacherRepository
            .SelectAsync(t => t.Id == teacherId);
        if (teacherData is null)
            throw new EduNetException(404, "Teacher is not found");

        if (formFile.Length > 5000000)
            throw new EduNetException(400, "Size of photo must be less than 5 mb");

        var extensions = new string[] { ".jpg", ".png", ".jpeg", ".heic", ".heif" };
        var extensionOfPhoto = Path.GetExtension(formFile.FileName);
        if (!extensions.Contains(extensionOfPhoto))
        {
            throw new EduNetException(400, "Extension of photo must be .jpg, .png, .jpeg, .heic or .heif");
        }

        var teacherProfilePhotoData = await _teacherProfilePhotoRepository
            .SelectAsync(tp => tp.TeacherId == teacherId);
        if (teacherProfilePhotoData is not null)
        {
            await RemoveProfilePhotoAsync(teacherId);
        }

        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(formFile.FileName);
        var rootPath = Path.Combine(EnvironmentHelper.WebRootPath, "Teachers", "ProfilePhotos", fileName);
        using (var stream = new FileStream(rootPath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }

        var mappedAsset = new TeacherProfilePhoto()
        {
            TeacherId = teacherId,
            Name = fileName,
            Path = Path.Combine("Teachers", "ProfilePhotos", formFile.FileName),
            Extension = Path.GetExtension(formFile.FileName),
            Size = formFile.Length,
            Type = formFile.ContentType,
            CreatedAt = DateTime.UtcNow,
        };

        var result = await this._teacherProfilePhotoRepository.InsertAsync(mappedAsset);

        return this._mapper.Map<TeacherProfilePhotoForResultDto>(result);
    }

    public async Task<TeacherForResultDto> ModifyAsync(long id, TeacherForUpdateDto dto)
    {
        var userData = await _userRepository
            .SelectAsync(u => u.Id == dto.UserId);
        if (userData is null)
            throw new EduNetException(404, "User is not found");

        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new EduNetException(404, "Branch is not found");

        userData.BranchId = dto.BranchId;
        await _userRepository.UpdateAsync(userData);

        var teacherData = await _teacherRepository
            .SelectAsync(t => t.Id == id);
        if (teacherData is null)
            throw new EduNetException(404, "Teacher not found");

        var mappedData = _mapper.Map(dto, teacherData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _teacherRepository.UpdateAsync(mappedData);

        return _mapper.Map<TeacherForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var studentData = await _teacherRepository
            .SelectAsync(t => t.Id == id);
        if (studentData is null)
            throw new EduNetException(404, "Teacher is not found");

        return await _teacherRepository.DeleteAsync(id);
    }

    public async Task<bool> RemoveProfilePhotoAsync(long teacherId)
    {
        var teacherData = await _teacherRepository
            .SelectAsync(t => t.Id == teacherId);
        if (teacherData is null)
            throw new EduNetException(404, "Teacher is not found");

        var teacherProfilePhotoData = await _teacherProfilePhotoRepository
            .SelectAsync(tp => tp.TeacherId == teacherId);
        if (teacherProfilePhotoData is null)
            throw new EduNetException(404, "TeacherProfilePhoto is not found");

        var teacherProfilePhotoId = (await _teacherProfilePhotoRepository
            .SelectAsync(tp => tp.TeacherId == teacherId))
            .Id;

        return await _teacherProfilePhotoRepository.DeleteAsync(teacherProfilePhotoId);
    }

    public async Task<IEnumerable<TeacherForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var teacherData = await _teacherRepository
            .SelectAll(t => !t.IsDeleted)
            .Include(t => t.TeacherProfilePhoto)
            .Include(t => t.Courses.Where(c => !c.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TeacherForResultDto>>(teacherData);
    }

    public async Task<TeacherForResultDto> RetrieveByIdAsync(long id)
    {
        var teacherData = await _teacherRepository
            .SelectAll(t => !t.IsDeleted)
            .Where(t => t.Id == id)
            .Include(t => t.TeacherProfilePhoto)
            .Include(t => t.Courses.Where(c => !c.IsDeleted))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (teacherData is null)
            throw new EduNetException(404, "Teacher is not found");

        return _mapper.Map<TeacherForResultDto>(teacherData);
    }

    public async Task<TeacherProfilePhotoForResultDto> RetrieveProfilePhotoAsync(long teacherId)
    {
        var teacherData = await _teacherRepository
            .SelectAsync(t => t.Id == teacherId);
        if (teacherData is null)
            throw new EduNetException(404, "Teacher is not found");

        var teacherProfilePhotoData = await _teacherProfilePhotoRepository
            .SelectAsync(tp => tp.TeacherId == teacherId);
        if (teacherProfilePhotoData is null)
            throw new EduNetException(404, "TeacherProfilePhoto is not found");

        return _mapper.Map<TeacherProfilePhotoForResultDto>(teacherProfilePhotoData);
    }

    public async Task<IEnumerable<TeacherForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var studentData = await _teacherRepository
            .SelectAll(t => !t.IsDeleted)
            .Where(t => t.FirstName.ToLower().Contains(search.ToLower())
                                || t.LastName.ToLower().Contains(search.ToLower())
                                || t.DateOfBirth.ToString("dd/MM/yyyy") == search
                                || t.PhoneNumber.Contains(search)
                                || t.TelegramUserName.ToLower().Contains(search.ToLower()))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TeacherForResultDto>>(studentData);
    }
}
