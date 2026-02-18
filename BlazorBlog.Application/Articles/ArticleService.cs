using BlazorBlog.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Application.Articles
{
    public class ArticleService : IArticleService
    {
        public List<Article> GetArticles()
        {
            return new List<Article>
            {
                new Article { Id = 1, Title = "First Article", Content = "This is the content of the first article.", IsPublished = true },
                new Article { Id = 2, Title = "Second Article", Content = "This is the content of the second article.", IsPublished = false },
                new Article { Id = 3, Title = "Third Article", Content = "This is the content of the third article.", IsPublished = true }
            };
        }
    }
}
