using EduNet.Backend.Data.Repositories;
using EduNet.Backend.Data.IRepositories;

namespace EduNet.Backend.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        // Repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
