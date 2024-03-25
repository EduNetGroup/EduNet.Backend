using Serilog;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using EduNet.Backend.Api.Extensions;
using EduNet.Backend.Data.DbContexts;
using EduNet.Backend.Service.Helpers;
using EduNet.Backend.Api.MiddleWares;

namespace EduNet.Backend.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //// ServiceExtension
        builder.Services.AddCustomService();

        //// Fix the Cycle
        builder.Services.AddControllers()
             .AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             });

        //// Logger 
        var logger = new LoggerConfiguration()
          .ReadFrom.Configuration(builder.Configuration)
          .Enrich.FromLogContext()
          .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        //// Db Connection
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        //// EnvironmentHelper
        EnvironmentHelper.WebRootPath = Path.GetFullPath("wwwroot");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();
        app.UseHttpsRedirection();

        //// MiddleWare
        app.UseMiddleware<ExceptionHandlerMiddleWare>();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
