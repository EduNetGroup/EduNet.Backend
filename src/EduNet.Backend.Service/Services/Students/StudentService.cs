using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Service.Helpers;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Extensions;
using EduNet.Backend.Service.Exceptions;
using EduNet.Backend.Domain.Entities.Users;
using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Domain.Entities.Students;
using EduNet.Backend.Service.Interfaces.Students;
using EduNet.Backend.Service.DTOs.Students.Students;

namespace EduNet.Backend.Service.Services.Students;

public class StudentService : IStudentService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<StudentProfilePhoto> _studentProfilePhotoRepository;

    public StudentService(
        IMapper mapper,
        IRepository<User> userRepository,
        IRepository<Student> studentRepository,
        IRepository<StudentProfilePhoto> studentProfilePhotoRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _studentRepository = studentRepository;
        _studentProfilePhotoRepository = studentProfilePhotoRepository;
    }

    public async Task<StudentForResultDto> AddAsync(StudentForCreationDto dto)
    {
        var userData = await _userRepository
            .SelectAsync(u => u.Id == dto.UserId);
        if (userData is null)
            throw new EduNetException(404, "User is not found");

        var studentData = await _studentRepository
            .SelectAsync(s => s.FirstName.ToLower() == dto.FirstName.ToLower()
                                    && s.LastName.ToLower() == dto.LastName.ToLower()
                                    && s.PhoneNumber == dto.PhoneNumber);
        if (studentData is not null)
            throw new EduNetException(409, "User is already exist");

        var mappedData = _mapper.Map<Student>(dto);

        return _mapper.Map<StudentForResultDto>(await _studentRepository.InsertAsync(mappedData));
    }

    public async Task<StudentProfilePhotoForResultDto> AddProfilePhotoAsync(long studentId, IFormFile formFile)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == studentId);
        if (studentData is null)
            throw new EduNetException(404, "Student is not found");

        if (formFile.Length > 5000000)
            throw new EduNetException(400, "Size of photo must be less than 5 mb");

        var extensions = new string[] { ".jpg", ".png", ".jpeg", ".heic", ".heif" };
        var extensionOfPhoto = Path.GetExtension(formFile.FileName);
        if (!extensions.Contains(extensionOfPhoto))
        {
            throw new EduNetException(400, "Extension of photo must be .jpg, .png, .jpeg, .heic or .heif");
        }

        var studentProfilePhotoData = await _studentProfilePhotoRepository
            .SelectAsync(sp => sp.StudentId == studentId);
        if(studentProfilePhotoData is not null)
        {
            await RemoveProfilePhotoAsync(studentId);
        }

        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(formFile.FileName);
        var rootPath = Path.Combine(EnvironmentHelper.WebRootPath, "Students", "ProfilePhotos", fileName);
        using (var stream = new FileStream(rootPath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }

        var mappedAsset = new StudentProfilePhoto()
        {
            StudentId = studentId,
            Name = fileName,
            Path = Path.Combine("Students", "ProfilePhotos", formFile.FileName),
            Extension = Path.GetExtension(formFile.FileName),
            Size = formFile.Length,
            Type = formFile.ContentType,
            CreatedAt = DateTime.UtcNow,
        };

        var result = await this._studentProfilePhotoRepository.InsertAsync(mappedAsset);

        return this._mapper.Map<StudentProfilePhotoForResultDto>(result);
    }

    public async Task<StudentForResultDto> ModifyAsync(long id, StudentForUpdateDto dto)
    {
        var userData = await _userRepository
            .SelectAsync(u => u.Id == dto.UserId);
        if (userData is null)
            throw new EduNetException(404, "User is not found");

        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == id);
        if (studentData is null)
            throw new EduNetException(404, "Student not found");

        var mappedData = _mapper.Map(dto, studentData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _studentRepository.UpdateAsync(mappedData);

        return _mapper.Map<StudentForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == id);
        if (studentData is null)
            throw new EduNetException(404, "Student is not found");

        return await _studentRepository.DeleteAsync(id);
    }

    public async Task<bool> RemoveProfilePhotoAsync(long studentId)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == studentId);
        if (studentData is null)
            throw new EduNetException(404, "Student is not found");

        var studentProfilePhotoData = await _studentProfilePhotoRepository
            .SelectAsync(sp => sp.StudentId == studentId);
        if(studentProfilePhotoData is null)
            throw new EduNetException(404,"StudentProfilePhoto is not found");

        var studentProfilePhotoId = (await _studentProfilePhotoRepository
            .SelectAsync(sp => sp.StudentId == studentId))
            .Id;

        return await _studentProfilePhotoRepository.DeleteAsync(studentProfilePhotoId);
    }

    public async Task<IEnumerable<StudentForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var studentData = await _studentRepository
            .SelectAll(s => !s.IsDeleted)
            .Include(s => s.Courses.Where(c => !c.IsDeleted))
            .Include(s => s.Payments.Where(p => !p.IsDeleted))
            .Include(s => s.Attendances.Where(a => !a.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<StudentForResultDto>>(studentData);
    }

    public async Task<StudentForResultDto> RetrieveByIdAsync(long id)
    {
        var studentData = await _studentRepository
            .SelectAll(s => !s.IsDeleted)
            .Where(s => s.Id == id) 
            .Include(s => s.Courses.Where(c => !c.IsDeleted))
            .Include(s => s.Payments.Where(p => !p.IsDeleted))
            .Include(s => s.Attendances.Where(a => !a.IsDeleted))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (studentData is null)
            throw new EduNetException(404, "Student is not found");

        return _mapper.Map<StudentForResultDto>(studentData);
    }

    public async Task<StudentProfilePhotoForResultDto> RetrieveProfilePhotoAsync(long studentId)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == studentId);
        if (studentData is null)
            throw new EduNetException(404, "Student is not found");

        var studentProfilePhotoData = await _studentProfilePhotoRepository
            .SelectAsync(sp => sp.StudentId == studentId);
        if (studentProfilePhotoData is null)
            throw new EduNetException(404, "StudentProfilePhoto is not found");

        return _mapper.Map<StudentProfilePhotoForResultDto>(studentProfilePhotoData);
    }

    public async Task<IEnumerable<StudentForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var studentData = await _studentRepository
            .SelectAll(s => !s.IsDeleted)
            .Where(s => s.FirstName.ToLower().Contains(search.ToLower())
                                || s.LastName.ToLower().Contains(search.ToLower())
                                || s.DateOfBirth.ToString("dd/MM/yyyy") == search
                                || s.PhoneNumber.Contains(search)
                                || s.TelegramUserName.ToLower().Contains(search.ToLower()))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<StudentForResultDto>>(studentData);
    }
}
