using BlazorBlog.Domain.Articles;
using BlazorBlog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace BlazorBlog.Infrastructure.Tests.Repositories
{
    public class ArticleRepositoryTests
    {
        private ApplicationDbContext _context;
        private ArticleRepository _repository;

        [SetUp]
        public void Setup()
        {
            // For now Use in Memory Database, later we can switch to Sqlite for more realistic testing
            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseSqlite("TestDb")
            //    .Options;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            
            // Use This only when using sqlite and sqlSever
            //_context.Database.OpenConnection();
            //_context.Database.EnsureCreated();

            _repository = new ArticleRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {

            //_context.Database.CloseConnection();
            _context.Dispose();
        }

        [Test]
        public void Constructor_ShouldInitialize_WithApplicationDbContext()
        {
            // Assert
            Assert.That(_repository, Is.Not.Null);
        }

        [Test]
        public async Task Repository_ShouldUse_Context_ForDatabaseOperations()
        {
            // Arrange
            var article = Article.Create("Test", "Test Content");

            // Act
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();

            var result = await _context.Articles
               .IgnoreQueryFilters()
               .FirstOrDefaultAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("Test"));
        }
    }
}
