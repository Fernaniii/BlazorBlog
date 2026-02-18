using BlazorBlog.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Application.Articles
{
    public interface IArticleService
    {

        List<Article> GetArticles();
    }
}
