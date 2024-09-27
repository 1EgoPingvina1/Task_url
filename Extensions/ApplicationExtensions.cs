using WebApplication1.Interfaces;
using WebApplication1.Repositories;
using WebApplication1.Services;
using WebApplication1.Data;

namespace WebApplication1.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddDbContext<UrlDataContext>();
        services.AddScoped<IUrlRepository, UrlRepository>();
        services.AddScoped<UrlService>();
        return services;
    }
}