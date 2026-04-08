using BlazorBlog.Application.Articles;
using BlazorBlog.Domain.Articles;
using Moq;
using FluentAssertions;


namespace BlazorBlog.Application.Tests
{
    public class ArticleServiceTests
    {

        private Mock<IArticleRepository> _repositoryMock;
        private ArticleService _service;


        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IArticleRepository>();
            _service = new ArticleService(_repositoryMock.Object);
        }

        [Test]
        public async Task GetAllArticlesAsync_ShouldReturnOnlyNonDeletedArticles()
        {
            // Arrange
        var articles = new List<Article>
        {
            CreateArticle(isDeleted: false),
            CreateArticle(isDeleted: true),
            CreateArticle(isDeleted: false)
        };

            _repositoryMock
                .Setup(r => r.GetAllArticlesAsync())
                .ReturnsAsync(articles);

            // Act
            var result = await _service.GetAllArticlesAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().OnlyContain(a => !a.IsDeleted);
        }


        // So you can Add Helper for the tests
        private Article CreateArticle(bool isDeleted)
        {
            var article = Article.Create("Title", "Content");

            if (isDeleted)
            {
                // assuming you have a domain method
                article.SoftDelete();
            }

            return article;
        }

        [Test]
        public async Task GetAllArticlesAsync_ShouldReturnEmptyList_WhenNoArticlesExist()
        {
            // Arrange
            var articles = new List<Article>(); // empty list

            _repositoryMock
                .Setup(r => r.GetAllArticlesAsync())
                .ReturnsAsync(articles);

            // Act
            var result = await _service.GetAllArticlesAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

    }
}
