using BlazorBlog.Domain.Articles;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;

namespace BlazorBlog.Domain.ArticleTests
{
    public class ArticleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Create_WithValidData_ShouldInitializedCorrectly()
        {
            // Arrange
            var title = "Test Article";
            var content = "Test Content";


            // Act
            var article = Article.Create(title, content);


            // Assert
            article.Should().NotBeNull();
            article.Title.Should().Be(title);
            article.Content.Should().Be(content);
            article.IsPublished.Should().BeFalse();

            article.PublishedOn.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
        }

        [Test]
        public void Create_WithEmptyTitle_ShouldThrowArgumentException()
        {
            // Arrange
            var emptyTitle = "";
            var content = "Test Content";

            // Act
            var ex = Assert.Throws<ValidationException>(() => Article.Create(emptyTitle, content));

            // When using Argument Exception, we can check the message directly without needing to catch the exception
            //Action act = () => Article.Create(emptyTitle, content);

            // Assert
            Assert.That(ex.Message, Does.Contain("Title cannot be empty"));


            //When using argument exception, we can check the message directly without needing to catch the exception
            //act.Should().Throw<ArgumentException>()
            //    .WithMessage("*Title*");
        }


        [Test]
        public void Create_WithNullTitle_ShouldThrowArgumentException()
        {
            // Arrange
            string nullTitle = null!;
            var content = "Test Content";

            // Act
            var ex = Assert.Throws<ValidationException>(() => Article.Create(nullTitle, content));
            //Action act = () => Article.Create(nullTitle, content);

            Assert.That(ex.Message, Does.Contain("Title cannot be empty"));

            // Assert
            //act.Should().Throw<ArgumentException>()
            //    .WithMessage("*Title*");
        }




        }
}
