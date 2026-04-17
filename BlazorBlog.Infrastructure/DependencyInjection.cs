using BlazorBlog.Application.AuditTrailing;
using BlazorBlog.Domain.Articles;
using BlazorBlog.Domain.AuditTrailing;
using BlazorBlog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.Customizer;
using TickerQ.EntityFrameworkCore.DependencyInjection;

namespace BlazorBlog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddTickerQ();

        // Register the Repoisitory in Here
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IAuditRepository, AuditRepository>();

        services.AddTickerQ(opt =>
        {
            opt.AddDashboard();
            opt.AddDashboard(dashboard =>
            {
                dashboard.SetBasePath("/admin/tickerq");
                dashboard.WithBasicAuth("admin", "secret");
            });
            opt.AddOperationalStore(ef =>
            {
                ef.UseApplicationDbContext<ApplicationDbContext>(ConfigurationType.UseModelCustomizer);
            });
        });

        return services;
    }
}
