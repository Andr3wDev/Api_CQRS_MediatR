using MediatR;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Behaviors;
using Application.Abstractions;
using Application.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Infrastructure.Mapping;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using System.Reflection;

namespace API.Extensions;

public static class DIExtension
{    
    public static void ConfigureDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<LibraryDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("PostgresDb") ??
                throw new ApplicationException("PostgresDb connection string is invalid.");
            options.UseNpgsql(connectionString);
        });
    }

    public static void ConfigureMapping(this IServiceCollection services)
    {
        services.AddSingleton<MapsterMapper.IMapper, MapsterMapper.Mapper>();
        services.AddSingleton<ICustomMapper, CustomMapper>();
    }

    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers();        
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            /*var file = $"swagger_web.xml";
            var filePath = Path.Combine(AppContext.BaseDirectory, file);
            c.IncludeXmlComments(filePath);*/
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        });
    }

    public static void ConfigureValidation(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
    
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
    }

    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}