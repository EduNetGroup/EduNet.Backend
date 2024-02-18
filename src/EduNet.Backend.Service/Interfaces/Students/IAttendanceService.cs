using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Students.Attendances;

namespace EduNet.Backend.Service.Interfaces.Students;

public interface IAttendanceService
{
    Task<bool> RemoveAsync(long id);
    Task<AttendanceForResultDto> RetrieveByIdAsync(long id);
    Task<AttendanceForResultDto> AddAsync(AttendanceForCreationDto dto);
    Task<AttendanceForResultDto> ModifyAsync(long id, AttendanceForUpdateDto dto);
    Task<IEnumerable<AttendanceForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<AttendanceForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
