using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Domain.Articles
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllArticlesAsync();
    }
}
