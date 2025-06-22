using ContactsManager.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ContactsManager.Core.Builders;

public static class Services
{
    /// <summary>
    /// Adds services to the services collection
    /// </summary>
    /// <param name="services">builder.Services</param>
    /// <param name="configurationManager">builder.Configuration</param>
    public static void AddServices(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        // Determine XML comments path
        string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        // Add services
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Contacts Manager API", Version = "v1" });
            options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        });

        // Load DB configuration from appsettings.json
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddDbContext<ContactDbContext>(options =>
            options.UseSqlServer(configurationManager.GetConnectionString("ContactManager")));

        // Add CORS
        services.AddCors(options =>
        {
            // Allow Local Development Only
            options.AddPolicy("AllowLocalDev",
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .WithMethods("GET", "POST", "PATCH", "DELETE")
                    .WithExposedHeaders("Sort", "Descending", "Page", "TotalPages", "Search")
                    .AllowAnyHeader();
                });
        });
    }
}