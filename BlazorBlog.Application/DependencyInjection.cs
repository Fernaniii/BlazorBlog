using BlazorBlog.Application.Articles;
using BlazorBlog.Application.AuditTrailing;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorBlog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();

        services.AddScoped<IAuditService, AuditService>();

        return services;
    }

}
