using BlazorBlog.Application.Articles;
using BlazorBlog.WebUI.Server.Components;
using BlazorBlog.Application;
using BlazorBlog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

// To Register a service for dependency injection, you can use the following methods:
// Firstly you Can add in Program.cs file directly
//builder.Services.AddScoped<IArticleService, ArticleService>();


// Or you can Use Depenndency Injection class in ano9ther folder and call it in Program.cs file like below
builder.Services.AddApplication();

// On Adding Connection string you can use this

//builder.Configuration.GetConnectionString("DefaultConnection");

// But this is not an option but instead use the same approach like the Service
// And Create a Dependency Injection class in the Infrastructure folder and call it in Program.cs file like below
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>();

app.Run();
