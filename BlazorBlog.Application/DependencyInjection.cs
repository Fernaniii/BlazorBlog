using BlazorBlog.Application.Abstractions;
using BlazorBlog.Application.Articles;
using BlazorBlog.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorBlog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();

        services.AddScoped<AuditService>();

        return services;
    }

}
