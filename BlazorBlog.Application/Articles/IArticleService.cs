using BlazorBlog.Domain.Articles;

namespace BlazorBlog.Application.Articles
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticlesAsync();
        Task<Article> GetByIdAsync(int id);

        Task<Article> AddAsync(Article article);
        Task<Article> UpdateAsync(Article article);
    }
}
