namespace BlazorBlog.Domain.Articles
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllArticlesAsync();
        Task<Article> GetByIdAsync(int id);
        Task<Article> AddAsync(Article article);
        Task<Article> UpdateAsync(Article article);
    }
}
