using System.ComponentModel.DataAnnotations;

namespace BlazorBlog.Application.DTOs
{
    public class ArticleEditModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string? Content { get; set; }

        public bool IsPublished { get; set; }
    }
}
