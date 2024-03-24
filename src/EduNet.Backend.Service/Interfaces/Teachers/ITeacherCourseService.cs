using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Teachers.TeacherCourses;

namespace EduNet.Backend.Service.Interfaces.Teachers;

public interface ITeacherCourseService
{
    Task<bool> RemoveAsync(long id);
    Task<TeacherCourseForResultDto> RetrieveByIdAsync(long id);
    Task<TeacherCourseForResultDto> AddAsync(TeacherCourseForCreationDto dto);
    Task<TeacherCourseForResultDto> ModifyAsync(long id, TeacherCourseForUpdateDto dto);
    Task<IEnumerable<TeacherCourseForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<TeacherCourseForResultDto>> SearchAllByDateAsync(string search, PaginationParams @params);
}
