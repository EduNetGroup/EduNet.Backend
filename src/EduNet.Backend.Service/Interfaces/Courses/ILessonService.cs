using EduNet.Backend.Service.Configurations;
using EduNet.Backend.Service.DTOs.Courses.Lessons;

namespace EduNet.Backend.Service.Interfaces.Courses;

public interface ILessonService
{
    Task<bool> RemoveAsync(long id);
    Task<LessonForResultDto> RetrieveByIdAsync(long id);
    Task<LessonForResultDto> AddAsync(LessonForCreationDto dto);
    Task<LessonForResultDto> ModifyAsync(long id, LessonForUpdateDto dto);
    Task<IEnumerable<LessonForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<LessonForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
