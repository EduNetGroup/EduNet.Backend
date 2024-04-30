using EduNet.Backend.Service.Mappers;
using EduNet.Backend.Data.Repositories;
using EduNet.Backend.Data.IRepositories;
using EduNet.Backend.Service.Services.Roles;
using EduNet.Backend.Service.Services.Users;
using EduNet.Backend.Service.Interfaces.Users;
using EduNet.Backend.Service.Interfaces.Roles;
using EduNet.Backend.Service.Services.Courses;
using EduNet.Backend.Service.Services.Accounts;
using EduNet.Backend.Service.Services.Teachers;
using EduNet.Backend.Service.Services.Payments;
using EduNet.Backend.Service.Services.Students;
using EduNet.Backend.Service.Services.Branches;
using EduNet.Backend.Service.Interfaces.Courses;
using EduNet.Backend.Service.Interfaces.Branches;
using EduNet.Backend.Service.Interfaces.Payments;
using EduNet.Backend.Service.Interfaces.Students;
using EduNet.Backend.Service.Interfaces.Teachers;
using EduNet.Backend.Service.Interfaces.Accounts;

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
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IAttendanceService, AttendanceService>();
        services.AddScoped<ITeacherCourseService, TeacherCourseService>();
        services.AddScoped<IRolePermissionService, RolePermissionService>();
    }
}
