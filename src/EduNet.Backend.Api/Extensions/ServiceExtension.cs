using EduNet.Backend.Service.Mappers;
using EduNet.Backend.Data.Repositories;
using EduNet.Backend.Data.IRepositories;

namespace EduNet.Backend.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        // MappingProfile
        services.AddScoped(typeof(MappingProfile));
        
        // Repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Services
    }
}
