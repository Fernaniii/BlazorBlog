using BlazorBlog.Domain.Articles;

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
            return await _articleRepository.AddAsync(article);
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            return await _articleRepository.GetAllArticlesAsync();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _articleRepository.GetByIdAsync(id);
        }
    }
}
