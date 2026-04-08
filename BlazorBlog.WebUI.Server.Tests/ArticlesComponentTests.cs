using BlazorBlog.Application.Articles;
using BlazorBlog.Domain.Articles;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TestContext = Bunit.TestContext;

namespace BlazorBlog.WebUI.Server.Tests
{
    [TestFixture]
    public class ArticlesComponentTests : TestContext
    {
        [Test]
        public void Articles_ShouldRenderSuccessfully()
        {
            // Arrange
            var mockService = new Mock<IArticleService>();
            mockService.Setup(x => x.GetAllArticlesAsync())
                       .ReturnsAsync(new List<Article>());

            Services.AddSingleton(mockService.Object);

            // Act
            var cut = RenderComponent<Articles>();

            // Assert
            cut.Markup.Should().NotBeNullOrEmpty();

            // Optional (better verification)
            mockService.Verify(x => x.GetAllArticlesAsync(), Times.Once);
        }
    }
}