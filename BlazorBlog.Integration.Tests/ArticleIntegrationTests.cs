using BlazorBlog.Application.Articles;
using BlazorBlog.Domain.Articles;
using BlazorBlog.Infrastructure;
using BlazorBlog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorBlog.Integration.Tests
{
    public class ArticleIntegrationTests
    {
        private ApplicationDbContext _context;
        private ArticleRepository _repository;
        private ArticleService _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("IntegrationTestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new ArticleRepository(_context);
            _service = new ArticleService(_repository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        // Domain to Application Layer Integration Test
        [Test]
        public void Creating_InvalidArticle_ShouldThrowValidationException()
        {
          
            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => Article.Create("", "Content"));

            Assert.That(ex.Message, Does.Contain("Title cannot be empty"));
        }

        [Test]
        public async Task Creating_ValidArticle_ShouldPersistThroughService()
        {
            // Arrange
            var article = Article.Create("Valid Title", "Content");

            // Act
            await _service.AddAsync(article);

            var persisted = await _context.Articles
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(a => a.Title == "Valid Title");

            // Assert
            Assert.That(persisted, Is.Not.Null);
            Assert.That(persisted.Title, Is.EqualTo("Valid Title"));
        }
    }
}
