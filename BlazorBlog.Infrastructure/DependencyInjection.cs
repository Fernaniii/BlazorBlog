using BlazorBlog.Application.Abstractions;
using BlazorBlog.Domain.Articles;
using BlazorBlog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorBlog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Register the Repoisitory in Here
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IAuditRepository, AuditRepository>();

        return services;
    }
}
