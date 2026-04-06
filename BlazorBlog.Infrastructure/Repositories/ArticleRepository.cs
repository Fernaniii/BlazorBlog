using BlazorBlog.Domain.Articles;
using Microsoft.EntityFrameworkCore;


namespace BlazorBlog.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {

        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Article> AddAsync(Article article)
        {
            await _context.Articles
                .AddAsync(article);

            await _context.SaveChangesAsync();

            return article;
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            var article = await _context.Articles
                .FirstOrDefaultAsync(x => x.Id == id);

            return article ?? throw new KeyNotFoundException("Article not found.");
        }

        public async Task<Article> UpdateAsync(Article article)
        {
            var _article = await _context.Articles.FindAsync(article.Id);

            if (_article is null)
                throw new Exception("Article not found");

            article.Update(article.Title, article.Content);

            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
            return article;
        }
    }
}
