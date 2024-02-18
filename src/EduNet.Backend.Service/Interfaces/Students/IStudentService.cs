using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Students.Students;

namespace EduNet.Backend.Service.Interfaces.Students;

public interface IStudentService
{
    Task<bool> RemoveAsync(long id);
    Task<StudentForResultDto> RetrieveByIdAsync(long id);
    Task<StudentForResultDto> AddAsync(StudentForCreationDto dto);
    Task<StudentForResultDto> ModifyAsync(long id, StudentForUpdateDto dto);
    Task<IEnumerable<StudentForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<StudentForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
