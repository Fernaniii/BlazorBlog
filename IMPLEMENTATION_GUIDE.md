# Clean Architecture Implementation Guide for Blazor Projects

## 📋 Complete Step-by-Step Implementation Plan

This guide provides a comprehensive roadmap to recreate the **BlazorBlog** project structure using **Clean Architecture** principles with .NET 10 and Blazor.

---

## **PHASE 1: PROJECT SETUP & STRUCTURE**

### **Step 1.1: Create Solution**

#### ✅ Actions:
```powershell
# Create new folder for your solution
mkdir MyCleanArchitectureApp
cd MyCleanArchitectureApp

# Create blank solution
dotnet new sln -n MyCleanArchitectureApp
```

#### 📝 Notes:
- Use PascalCase for solution names (e.g., `MyCleanArchitectureApp`)
- Keep solution root clean - only `.sln` and `.gitignore` files

#### ⚠️ Warnings:
- ❌ **DO NOT** put solution files inside the project folders
- ❌ **DO NOT** use spaces in solution names
- ❌ **DO NOT** create nested solution folders unnecessarily

---

### **Step 1.2: Create Project Layers**

#### ✅ Create Projects in This Order:

```powershell
# 1. Domain Layer (Core Business Logic)
dotnet new classlib -n MyCleanArchitectureApp.Domain -f net10.0
dotnet sln MyCleanArchitectureApp.sln add MyCleanArchitectureApp.Domain/MyCleanArchitectureApp.Domain.csproj

# 2. Application Layer (Services & Business Rules)
dotnet new classlib -n MyCleanArchitectureApp.Application -f net10.0
dotnet sln MyCleanArchitectureApp.sln add MyCleanArchitectureApp.Application/MyCleanArchitectureApp.Application.csproj

# 3. Infrastructure Layer (Database & External Services)
dotnet new classlib -n MyCleanArchitectureApp.Infrastructure -f net10.0
dotnet sln MyCleanArchitectureApp.sln add MyCleanArchitectureApp.Infrastructure/MyCleanArchitectureApp.Infrastructure.csproj

# 4. Presentation Layer (Blazor WebUI)
dotnet new blazor -n MyCleanArchitectureApp -f net10.0 --interactivity server
dotnet sln MyCleanArchitectureApp.sln add MyCleanArchitectureApp/MyCleanArchitectureApp.csproj

# 5. (Optional) Test Projects
dotnet new nunit -n MyCleanArchitectureApp.Application.Tests -f net10.0
dotnet sln MyCleanArchitectureApp.sln add MyCleanArchitectureApp.Application.Tests/MyCleanArchitectureApp.Application.Tests.csproj
```

#### 📊 Final Solution Structure:
```
MyCleanArchitectureApp/
├── MyCleanArchitectureApp.sln
├── MyCleanArchitectureApp.Domain/              # Layer 1: Business Logic
│   └── MyCleanArchitectureApp.Domain.csproj
├── MyCleanArchitectureApp.Application/         # Layer 2: Business Rules
│   └── MyCleanArchitectureApp.Application.csproj
├── MyCleanArchitectureApp.Infrastructure/      # Layer 3: Data Access
│   └── MyCleanArchitectureApp.Infrastructure.csproj
├── MyCleanArchitectureApp/                     # Layer 4: UI (Blazor)
│   └── MyCleanArchitectureApp.csproj
└── MyCleanArchitectureApp.Application.Tests/   # Tests
    └── MyCleanArchitectureApp.Application.Tests.csproj
```

#### 📝 Notes:
- Order matters! Create domain first, then application, then infrastructure
- This ensures proper dependency flow
- Blazor project uses `--interactivity server` for server-side rendering

#### ⚠️ Warnings:
- ❌ **DO NOT** create infrastructure project before domain
- ❌ **DO NOT** use `.sln` projects inside project folders
- ❌ **DO NOT** skip creating base project structure - add it later

---

## **PHASE 2: PROJECT REFERENCES & DEPENDENCIES**

### **Step 2.1: Add Project References**

#### ✅ Reference Flow (Dependency Direction):

```
Domain Layer (no dependencies on other projects)
    ↑
Application Layer (references Domain)
    ↑
Infrastructure Layer (references Application & Domain)
    ↑
UI/Blazor Layer (references Application & Infrastructure)
```

#### ✅ Commands:

```powershell
# From Project Root: Add references
cd MyCleanArchitectureApp.Application
dotnet add reference ../MyCleanArchitectureApp.Domain/MyCleanArchitectureApp.Domain.csproj

cd ../MyCleanArchitectureApp.Infrastructure
dotnet add reference ../MyCleanArchitectureApp.Domain/MyCleanArchitectureApp.Domain.csproj
dotnet add reference ../MyCleanArchitectureApp.Application/MyCleanArchitectureApp.Application.csproj

cd ../MyCleanArchitectureApp
dotnet add reference ../MyCleanArchitectureApp.Domain/MyCleanArchitectureApp.Domain.csproj
dotnet add reference ../MyCleanArchitectureApp.Application/MyCleanArchitectureApp.Application.csproj
dotnet add reference ../MyCleanArchitectureApp.Infrastructure/MyCleanArchitectureApp.Infrastructure.csproj

cd ../MyCleanArchitectureApp.Application.Tests
dotnet add reference ../MyCleanArchitectureApp.Application/MyCleanArchitectureApp.Application.csproj
dotnet add reference ../MyCleanArchitectureApp.Domain/MyCleanArchitectureApp.Domain.csproj
```

#### 📝 Notes:
- References must follow the dependency inversion principle
- Domain should NEVER reference other layers
- Infrastructure should NEVER reference UI layer

#### ⚠️ Warnings:
- ❌ **DO NOT** create circular references (e.g., Domain → Application → Domain)
- ❌ **DO NOT** have Infrastructure reference UI directly
- ❌ **DO NOT** reference projects from lower layers in higher layers backwards

---

### **Step 2.2: Add NuGet Packages**

#### ✅ Domain Layer Packages:
```powershell
cd MyCleanArchitectureApp.Domain

# No external dependencies needed - keep it pure!
# Domain only references .NET Base Class Library
```

#### 📝 Notes:
- Domain layer should have ZERO external NuGet dependencies
- This keeps business logic pure and testable

#### ✅ Application Layer Packages:
```powershell
cd ../MyCleanArchitectureApp.Application

# Extension methods for dependency injection
dotnet add package Microsoft.Extensions.DependencyInjection.Abstractions
```

#### ✅ Infrastructure Layer Packages:
```powershell
cd ../MyCleanArchitectureApp.Infrastructure

# Entity Framework Core for data access
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design

# Dependency Injection
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Configuration

# Logging
dotnet add package Microsoft.Extensions.Logging
```

#### ✅ UI/Blazor Layer Packages:
```powershell
cd ../MyCleanArchitectureApp

# Blazor packages (already included in template)
# If needed for additional functionality:
# dotnet add package Microsoft.AspNetCore.Components.Web
```

#### ✅ Test Project Packages:
```powershell
cd ../MyCleanArchitectureApp.Application.Tests

dotnet add package NUnit
dotnet add package NUnit3TestAdapter
dotnet add package Moq
dotnet add package Microsoft.NET.Test.Sdk
```

#### 📝 Notes:
- Install packages in the CORRECT layer only
- Never install Entity Framework in Domain or Application
- Domain layer stays dependency-free

#### ⚠️ Warnings:
- ❌ **DO NOT** install EntityFramework in Application layer
- ❌ **DO NOT** install UI packages in business logic layers
- ❌ **DO NOT** add unnecessary packages - keep layers lean

---

## **PHASE 3: DOMAIN LAYER IMPLEMENTATION**

### **Step 3.1: Create Base Abstractions**

#### ✅ Create Folder Structure:
```powershell
# Inside MyCleanArchitectureApp.Domain/
mkdir Abstractions
mkdir Articles
mkdir Common
```

#### ✅ Create Entity Base Class:

**File: `Abstractions/Entity.cs`**
```csharp
namespace MyCleanArchitectureApp.Domain.Abstractions
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; }
    }
}
```

#### ✅ Create Soft Deletable Entity:

**File: `Abstractions/SoftDeletableEntity.cs`**
```csharp
namespace MyCleanArchitectureApp.Domain.Abstractions
{
    public abstract class SoftDeletableEntity : Entity
    {
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedOn { get; private set; }

        public void SoftDelete()
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedOn = DateTime.Now;
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletedOn = null;
        }
    }
}
```

#### 📝 Notes:
- Use abstract classes for entities - force inheritance
- Keep entity methods focused on business logic
- Use private setters for audit fields

#### ⚠️ Warnings:
- ❌ **DO NOT** add data access logic (SaveChanges, etc.) to entities
- ❌ **DO NOT** use public setters for sensitive properties
- ❌ **DO NOT** create concrete entity instances directly - use factory methods

---

### **Step 3.2: Create Domain Entities**

#### ✅ Create Article Entity:

**File: `Articles/Article.cs`**
```csharp
using MyCleanArchitectureApp.Domain.Abstractions;

namespace MyCleanArchitectureApp.Domain.Articles
{
    public class Article : SoftDeletableEntity
    {
        public string Title { get; set; }
        public string? Content { get; set; }
        public DateTime PublishedOn { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } = false;

        // Private constructor for Factory Pattern
        private Article() { }

        private Article(string title, string? content)
        {
            SetTitle(title);
            SetContent(content);
            PublishedOn = DateTime.Now;
        }

        // Factory method - use this to create instances
        public static Article Create(string title, string? content)
        {
            return new Article(title, content);
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");
            Title = title;
        }

        public void SetContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be empty.");
            Content = content;
        }

        public void Update(string title, string? content)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");
            Title = title;
            Content = content;
        }

        public void Publish()
        {
            IsPublished = true;
            PublishedOn = DateTime.Now;
        }

        public void Unpublish()
        {
            IsPublished = false;
        }
    }
}
```

#### 📝 Notes:
- Use **Factory Pattern** with private constructor
- Entity validates its own state (Domain-Driven Design)
- Business logic lives in entities, not in services
- Use meaningful method names (Publish, Unpublish, etc.)

#### ⚠️ Warnings:
- ❌ **DO NOT** accept repository/database objects in entities
- ❌ **DO NOT** use public constructors - use factory methods
- ❌ **DO NOT** have entities know about infrastructure concerns

---

### **Step 3.3: Create Repository Interfaces**

#### ✅ Create Article Repository Interface:

**File: `Articles/IArticleRepository.cs`**
```csharp
namespace MyCleanArchitectureApp.Domain.Articles
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllArticlesAsync();
        Task<Article> GetByIdAsync(int id);
        Task<Article> AddAsync(Article article);
        Task<Article> UpdateAsync(Article article);
        Task<bool> DeleteAsync(int id);
    }
}
```

#### 📝 Notes:
- Interfaces define the contract, not implementation
- Repository pattern abstracts data access
- Put interfaces in Domain for Dependency Inversion
- Use async/await for all data operations

#### ⚠️ Warnings:
- ❌ **DO NOT** implement repository in Domain layer
- ❌ **DO NOT** add database-specific methods to interface
- ❌ **DO NOT** use synchronous methods - always async

---

## **PHASE 4: APPLICATION LAYER IMPLEMENTATION**

### **Step 4.1: Create Application Services**

#### ✅ Create Article Service:

**File: `Articles/IArticleService.cs`**
```csharp
using MyCleanArchitectureApp.Domain.Articles;

namespace MyCleanArchitectureApp.Application.Articles
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticlesAsync();
        Task<Article> GetByIdAsync(int id);
        Task<Article> AddAsync(Article article);
        Task<Article> UpdateAsync(Article article);
        Task<bool> DeleteAsync(int id);
    }
}
```

**File: `Articles/ArticleService.cs`**
```csharp
using MyCleanArchitectureApp.Domain.Articles;

namespace MyCleanArchitectureApp.Application.Articles
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

        public async Task<bool> DeleteAsync(int id)
        {
            return await _articleRepository.DeleteAsync(id);
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            return await _articleRepository.GetAllArticlesAsync();
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
```

#### 📝 Notes:
- Services orchestrate business logic
- Services use dependency injection
- Services delegate to repositories for data access
- Keep services focused - single responsibility

#### ⚠️ Warnings:
- ❌ **DO NOT** put business logic in services - put it in entities
- ❌ **DO NOT** access database directly from services
- ❌ **DO NOT** make services know about UI concerns

---

### **Step 4.2: Create Dependency Injection Extension**

#### ✅ Create DependencyInjection Class:

**File: `DependencyInjection.cs`**
```csharp
using MyCleanArchitectureApp.Application.Articles;
using Microsoft.Extensions.DependencyInjection;

namespace MyCleanArchitectureApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register all application services here
        services.AddScoped<IArticleService, ArticleService>();
        
        return services;
    }
}
```

#### 📝 Notes:
- Use extension methods for clean registration
- Register services in Application layer DI
- Use `AddScoped` for services (one instance per request)
- Makes Program.cs cleaner and more maintainable

#### ⚠️ Warnings:
- ❌ **DO NOT** register infrastructure services here - do it in Infrastructure DI
- ❌ **DO NOT** use `AddSingleton` for stateful services
- ❌ **DO NOT** register UI components here

---

## **PHASE 5: INFRASTRUCTURE LAYER IMPLEMENTATION**

### **Step 5.1: Create Database Context**

#### ✅ Create ApplicationDbContext:

**File: `ApplicationDbContext.cs`**
```csharp
using MyCleanArchitectureApp.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace MyCleanArchitectureApp.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Add entity configurations here
        // Example:
        // modelBuilder.ApplyConfiguration(new ArticleConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update DateUpdated before saving
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Domain.Abstractions.Entity);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Modified)
            {
                ((Domain.Abstractions.Entity)entry.Entity).DateUpdated = DateTime.Now;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
```

#### 📝 Notes:
- DbContext belongs in Infrastructure layer
- Use OnModelCreating for entity configurations
- Override SaveChangesAsync for audit trails
- Keep context focused on database operations

#### ⚠️ Warnings:
- ❌ **DO NOT** reference DbContext in Domain or Application layers directly
- ❌ **DO NOT** put business logic in SaveChangesAsync
- ❌ **DO NOT** use DbContext without dependency injection

---

### **Step 5.2: Implement Repository Pattern**

#### ✅ Create Folder Structure:
```powershell
# Inside MyCleanArchitectureApp.Infrastructure/
mkdir Repositories
```

#### ✅ Create Article Repository:

**File: `Repositories/ArticleRepository.cs`**
```csharp
using MyCleanArchitectureApp.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace MyCleanArchitectureApp.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly ApplicationDbContext _context;

    public ArticleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Article> AddAsync(Article article)
    {
        await _context.Articles.AddAsync(article);
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        
        if (article is null)
            return false;

        article.SoftDelete();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Article>> GetAllArticlesAsync()
    {
        return await _context.Articles
            .Where(a => !a.IsDeleted)
            .ToListAsync();
    }

    public async Task<Article> GetByIdAsync(int id)
    {
        var article = await _context.Articles
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        return article ?? throw new KeyNotFoundException("Article not found.");
    }

    public async Task<Article> UpdateAsync(Article article)
    {
        var existingArticle = await _context.Articles.FindAsync(article.Id);
        
        if (existingArticle is null)
            throw new Exception("Article not found");

        existingArticle.Update(article.Title, article.Content);
        
        _context.Articles.Update(existingArticle);
        await _context.SaveChangesAsync();
        
        return existingArticle;
    }
}
```

#### 📝 Notes:
- Repository implements the interface defined in Domain
- Handles all database operations
- Implements soft delete pattern
- Always filter out deleted items in queries

#### ⚠️ Warnings:
- ❌ **DO NOT** put business logic in repository - just data access
- ❌ **DO NOT** return IQueryable - return concrete types (List, single item)
- ❌ **DO NOT** use synchronous database operations - always use async

---

### **Step 5.3: Create Infrastructure Dependency Injection**

#### ✅ Create DependencyInjection Class:

**File: `DependencyInjection.cs`**
```csharp
using MyCleanArchitectureApp.Domain.Articles;
using MyCleanArchitectureApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyCleanArchitectureApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Register Repositories
        services.AddScoped<IArticleRepository, ArticleRepository>();

        return services;
    }
}
```

#### 📝 Notes:
- Register DbContext in Infrastructure DI
- Register all repositories here
- Pass Configuration for connection string
- Use MigrationsAssembly for proper migration discovery

#### ⚠️ Warnings:
- ❌ **DO NOT** register repositories in Application layer
- ❌ **DO NOT** hardcode connection strings
- ❌ **DO NOT** use UseInMemoryDatabase in production

---

### **Step 5.4: Setup Database Migrations**

#### ✅ Create Initial Migration:

```powershell
# From solution root
cd MyCleanArchitectureApp.Infrastructure

# Create initial migration
dotnet ef migrations add InitialCreate --startup-project ../MyCleanArchitectureApp

# Update database
dotnet ef database update --startup-project ../MyCleanArchitectureApp
```

#### 📝 Notes:
- Migrations belong in Infrastructure layer
- Always specify startup project (Blazor project)
- Add meaningful migration names
- Commit migrations to version control

#### ⚠️ Warnings:
- ❌ **DO NOT** create migrations from Domain or Application
- ❌ **DO NOT** use Database.EnsureCreated() in production
- ❌ **DO NOT** delete migration files manually

---

## **PHASE 6: BLAZOR UI LAYER IMPLEMENTATION**

### **Step 6.1: Configure Program.cs**

#### ✅ Update Program.cs:

**File: `Program.cs`**
```csharp
using MyCleanArchitectureApp.Application;
using MyCleanArchitectureApp.Infrastructure;
using MyCleanArchitectureApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register application services (from Application layer)
builder.Services.AddApplication();

// Register infrastructure services (from Infrastructure layer)
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
```

#### 📝 Notes:
- Call AddApplication() and AddInfrastructure() from Program.cs
- Blazor only references Application and Infrastructure layers
- UI doesn't reference Domain directly - use through services

#### ⚠️ Warnings:
- ❌ **DO NOT** register services in Program.cs - use DI extensions
- ❌ **DO NOT** mix concerns - keep DI setup organized
- ❌ **DO NOT** import Domain namespace directly in components

---

### **Step 6.2: Create Blazor Components**

#### ✅ Create Articles Component:

**File: `Components/Pages/Articles.razor`**
```razor
@page "/articles"
@inject IArticleService ArticleService
@using MyCleanArchitectureApp.Application.Articles
@using MyCleanArchitectureApp.Domain.Articles

<h3>Articles</h3>

@if (isLoading)
{
    <p>Loading...</p>
}
else if (articles == null || articles.Count == 0)
{
    <p>No articles found.</p>
}
else
{
    <div class="articles-list">
        @foreach (var article in articles)
        {
            <article class="article-item">
                <h4>@article.Title</h4>
                <p>@article.Content</p>
                <small>Published: @article.PublishedOn.ToShortDateString()</small>
            </article>
        }
    </div>
}

@code {
    private List<Article>? articles;
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            articles = await ArticleService.GetAllArticlesAsync();
        }
        catch (Exception ex)
        {
            // Log error
            articles = new List<Article>();
        }
        finally
        {
            isLoading = false;
        }
    }
}
```

#### 📝 Notes:
- Components inject services via @inject directive
- Services return domain entities to components
- Handle loading and error states
- Use OnInitializedAsync for initialization

#### ⚠️ Warnings:
- ❌ **DO NOT** put business logic in components
- ❌ **DO NOT** create new service instances - use DI
- ❌ **DO NOT** make direct database queries from components
- ❌ **DO NOT** use synchronous operations

---

### **Step 6.3: Configure appsettings.json**

#### ✅ Update appsettings.json:

**File: `appsettings.json`**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MyCleanArchitectureApp;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

#### 📝 Notes:
- Store connection strings in appsettings
- Use user secrets in development
- Different configurations for dev/prod
- TrustServerCertificate=true for local development only

#### ⚠️ Warnings:
- ❌ **DO NOT** commit production connection strings
- ❌ **DO NOT** hardcode secrets in code
- ❌ **DO NOT** use TrustServerCertificate=true in production

---

## **PHASE 7: DATABASE & MIGRATIONS**

### **Step 7.1: Add Entity Configuration**

#### ✅ Create Configuration Class:

**File: `Infrastructure/Configuration/ArticleConfiguration.cs`**
```csharp
using MyCleanArchitectureApp.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyCleanArchitectureApp.Infrastructure.Configuration;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Content)
            .HasMaxLength(5000);

        builder.Property(x => x.IsPublished)
            .HasDefaultValue(false);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}
```

#### ✅ Apply Configuration in DbContext:

Update `ApplicationDbContext.cs`:
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfiguration(new ArticleConfiguration());
}
```

#### 📝 Notes:
- Use Fluent API for configuration
- Define constraints and defaults
- Separate configuration from context for clarity
- ApplyConfiguration applies all configurations

#### ⚠️ Warnings:
- ❌ **DO NOT** use Data Annotations in domain entities
- ❌ **DO NOT** mix configuration concerns
- ❌ **DO NOT** set required fields in entity - define in configuration

---

### **Step 7.2: Create & Run Migrations**

#### ✅ Commands:

```powershell
# Navigate to Infrastructure folder
cd MyCleanArchitectureApp.Infrastructure

# Add migration
dotnet ef migrations add InitialCreate --startup-project ../MyCleanArchitectureApp

# View pending migrations
dotnet ef migrations list

# Update database
dotnet ef database update --startup-project ../MyCleanArchitectureApp

# Remove last migration (if needed)
dotnet ef migrations remove
```

#### 📝 Notes:
- Create meaningful migration names
- Review migration files before updating
- Keep migrations in Infrastructure layer
- Commit migrations to version control

#### ⚠️ Warnings:
- ❌ **DO NOT** modify migration files manually
- ❌ **DO NOT** use Database.EnsureCreated() in production
- ❌ **DO NOT** rollback production databases without backup

---

## **PHASE 8: TESTING SETUP**

### **Step 8.1: Create Unit Tests**

#### ✅ Create Test Class:

**File: `ArticleServiceTests.cs`**
```csharp
using MyCleanArchitectureApp.Application.Articles;
using MyCleanArchitectureApp.Domain.Articles;
using Moq;
using NUnit.Framework;

namespace MyCleanArchitectureApp.Application.Tests;

[TestFixture]
public class ArticleServiceTests
{
    private Mock<IArticleRepository> _mockRepository;
    private IArticleService _service;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IArticleRepository>();
        _service = new ArticleService(_mockRepository.Object);
    }

    [Test]
    public async Task GetAllArticlesAsync_ShouldReturnArticles()
    {
        // Arrange
        var articles = new List<Article>
        {
            Article.Create("Title 1", "Content 1"),
            Article.Create("Title 2", "Content 2")
        };
        _mockRepository.Setup(r => r.GetAllArticlesAsync()).ReturnsAsync(articles);

        // Act
        var result = await _service.GetAllArticlesAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        _mockRepository.Verify(r => r.GetAllArticlesAsync(), Times.Once);
    }

    [Test]
    public void AddAsync_WithInvalidTitle_ShouldThrowException()
    {
        // Arrange
        var article = Article.Create("", "Content");

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => _service.AddAsync(article));
    }
}
```

#### 📝 Notes:
- Use NUnit for testing framework
- Mock repository dependencies
- Follow Arrange-Act-Assert pattern
- Test one behavior per test

#### ⚠️ Warnings:
- ❌ **DO NOT** test multiple behaviors in one test
- ❌ **DO NOT** use real database in unit tests
- ❌ **DO NOT** create circular test dependencies

---

## **PHASE 9: RUNNING THE APPLICATION**

### **Step 9.1: Build Solution**

```powershell
# From solution root
dotnet build

# Restore packages
dotnet restore
```

#### 📝 Notes:
- Always build after adding new projects
- Check for build warnings
- Ensure no circular references

#### ⚠️ Warnings:
- ❌ **DO NOT** ignore build warnings
- ❌ **DO NOT** proceed if build fails

---

### **Step 9.2: Run Database Migrations**

```powershell
# Update database with migrations
dotnet ef database update --startup-project MyCleanArchitectureApp --project MyCleanArchitectureApp.Infrastructure
```

#### 📝 Notes:
- Ensure SQL Server is running
- Check connection string
- Verify database created

#### ⚠️ Warnings:
- ❌ **DO NOT** use Database.EnsureCreated() for production
- ❌ **DO NOT** proceed without successful migration

---

### **Step 9.3: Run Application**

```powershell
# From solution root or Blazor project folder
cd MyCleanArchitectureApp
dotnet run

# Or use Visual Studio: Press F5 or Ctrl+F5
```

#### ✅ Verify:
- Application runs without errors
- Blazor pages load
- Components render
- Services inject properly
- Database operations work

#### 📝 Notes:
- Application will run on https://localhost:7095
- Check browser console for JavaScript errors
- Use browser Developer Tools to debug

#### ⚠️ Warnings:
- ❌ **DO NOT** ignore startup errors
- ❌ **DO NOT** close database connections prematurely

---

## **PHASE 10: COMMON SETUP CHECKLIST**

### ✅ Final Verification Checklist

- [ ] Solution created with correct name
- [ ] All 4+ project layers created
- [ ] Project references follow clean architecture dependency flow
- [ ] NuGet packages installed in correct layers
- [ ] Domain layer has NO external dependencies
- [ ] Base entities (Entity, SoftDeletableEntity) created
- [ ] Domain entities created with factory pattern
- [ ] Repository interfaces defined in Domain
- [ ] Application services implement service interfaces
- [ ] Application DependencyInjection.cs created and exports AddApplication()
- [ ] Infrastructure DbContext created
- [ ] Repository implementations created
- [ ] Infrastructure DependencyInjection.cs created and exports AddInfrastructure()
- [ ] Entity configurations created (Fluent API)
- [ ] Migrations created successfully
- [ ] Database created
- [ ] Program.cs configured with DI
- [ ] Blazor components created and inject services
- [ ] appsettings.json configured with connection string
- [ ] Application builds successfully
- [ ] Application runs without errors
- [ ] Components render correctly
- [ ] Database operations work (CRUD)
- [ ] Unit tests created and passing

---

## **CRITICAL DO's AND DON'Ts SUMMARY**

### ✅ **DO**

✅ Create projects in order: Domain → Application → Infrastructure → UI  
✅ Keep Domain layer dependency-free  
✅ Use factory pattern for entity creation  
✅ Put business logic in domain entities  
✅ Use repository pattern for data access  
✅ Use dependency injection for all services  
✅ Use async/await for all database operations  
✅ Keep each layer focused on single responsibility  
✅ Use extension methods for DI registration  
✅ Apply migrations before running app  
✅ Use Entity Configurations (Fluent API)  
✅ Implement soft delete pattern  
✅ Use interfaces for abstraction  
✅ Store connection strings in appsettings  
✅ Test services with mocked repositories  

### ❌ **DON'T**

❌ Create circular references between projects  
❌ Add external dependencies to Domain  
❌ Use public constructors for entities - use factory pattern  
❌ Put business logic in services - put in entities  
❌ Access database directly from components  
❌ Use synchronous database operations  
❌ Return IQueryable from repositories  
❌ Hardcode connection strings  
❌ Register Infrastructure services in Application DI  
❌ Use public setters for audit fields  
❌ Reference DbContext in Domain or Application  
❌ Create entities without factory methods  
❌ Mix Business logic and data access concerns  
❌ Skip migration setup  
❌ Use Database.EnsureCreated() in production  
❌ Ignore build warnings  
❌ Put validation attributes in domain entities  
❌ Create test instances directly - use builders/factories  

---

## **TROUBLESHOOTING GUIDE**

### **Problem: Project won't build**
**Solution:**
- Check for circular references: `dotnet build --no-incremental`
- Remove/readd project references
- Clean bin and obj folders: `dotnet clean`
- Rebuild solution: `dotnet build`

### **Problem: Migration fails**
**Solution:**
- Verify connection string in appsettings.json
- Check SQL Server is running
- Delete Migrations folder and start fresh
- Remove and recreate DbContext configuration

### **Problem: Services not injecting**
**Solution:**
- Verify AddApplication() called in Program.cs
- Verify AddInfrastructure() called in Program.cs
- Check service is registered in DependencyInjection.cs
- Restart application

### **Problem: Components not rendering**
**Solution:**
- Check @inject syntax is correct
- Verify service method returns data
- Check browser console for JavaScript errors
- Add try-catch in OnInitializedAsync

### **Problem: Database operations fail**
**Solution:**
- Verify database created: `dotnet ef database update`
- Check connection string
- Verify SQL Server running
- Check migrations applied
- View pending migrations: `dotnet ef migrations list`

---

## **NEXT STEPS AFTER SETUP**

1. **Add more entities** following the same pattern
2. **Create additional services** as needed
3. **Write comprehensive tests** for all services
4. **Add validation** using FluentValidation (optional)
5. **Implement error handling** consistently
6. **Add logging** throughout layers
7. **Setup authentication/authorization** (if needed)
8. **Create DTOs** for data transfer (if needed)
9. **Setup CI/CD pipeline**
10. **Deploy to production**

---

## **USEFUL RESOURCES**

- [Clean Architecture - Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Microsoft Dependency Injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [NUnit Documentation](https://docs.nunit.org/)

---

**Last Updated:** .NET 10  
**Framework:** Blazor Server  
**Architecture:** Clean Architecture (4 Layers)  
**Target Audience:** Intermediate to Advanced .NET Developers
