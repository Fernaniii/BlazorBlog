using BlazorBlog.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }
    }
}
