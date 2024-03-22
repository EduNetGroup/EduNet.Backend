using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Students.Enrollments;

namespace EduNet.Backend.Service.Interfaces.Students;

public interface IEnrollmentService
{
    Task<bool> RemoveAsync(long id);
    Task<EnrollmentForResultDto> RetrieveByIdAsync(long id);
    Task<EnrollmentForResultDto> AddAsync(EnrollmentForCreationDto dto);
    Task<EnrollmentForResultDto> ModifyAsync(long id, EnrollmentForUpdateDto dto);
    Task<IEnumerable<EnrollmentForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<EnrollmentForResultDto>> SearchAllByDateAsync(string search, PaginationParams @params);
}
