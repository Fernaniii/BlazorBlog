using BlazorBlog.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Infrastructure
{
    public  class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options)
            :base(options) 
        {
            
        }

        public DbSet<Article> Articles { get; set; }
    }
}
