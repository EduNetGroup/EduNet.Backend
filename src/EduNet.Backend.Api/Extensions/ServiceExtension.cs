using EduNet.Backend.Service.Mappers;
using EduNet.Backend.Data.Repositories;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Services.Courses;
using EduNet.Backend.Service.Services.Branches;
using EduNet.Backend.Service.Interfaces.Courses;
using EduNet.Backend.Service.Interfaces.Branches;

namespace EduNet.Backend.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        // MappingProfile
        services.AddAutoMapper(typeof(MappingProfile));
        
        // Repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Services
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ILessonService, LessonService>();
    }
}
