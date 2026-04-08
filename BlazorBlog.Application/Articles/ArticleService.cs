using BlazorBlog.Domain.Articles;
using System.ComponentModel.DataAnnotations;

namespace BlazorBlog.Application.Articles
{
    public class ArticleService : IArticleService
    {

        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<Article> AddAsync(Article article)
        {
            if (string.IsNullOrWhiteSpace(article.Title))
                throw new ValidationException("Title cannot be empty");

            return await _articleRepository.AddAsync(article);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var resuilt = _articleRepository.DeleteAsync(id);
            return resuilt;
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            var articles =  await _articleRepository.GetAllArticlesAsync();

            return articles.Where(a => !a.IsDeleted).ToList();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _articleRepository.GetByIdAsync(id);
        }

        public async Task<Article> UpdateAsync(Article article)
        {
            return await _articleRepository.UpdateAsync(article);
        }
    }
}
