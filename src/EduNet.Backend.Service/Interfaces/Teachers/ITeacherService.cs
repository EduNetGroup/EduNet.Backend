using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Teachers.Teachers;

namespace EduNet.Backend.Service.Interfaces.Teachers;

public interface ITeacherService
{
    Task<bool> RemoveAsync(long id);
    Task<TeacherForResultDto> RetrieveByIdAsync(long id);
    Task<TeacherForResultDto> AddAsync(TeacherForCreationDto dto);
    Task<TeacherForResultDto> ModifyAsync(long id, TeacherForUpdateDto dto);
    Task<IEnumerable<TeacherForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<TeacherForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
