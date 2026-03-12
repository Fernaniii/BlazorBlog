using System.ComponentModel.DataAnnotations;

namespace BlazorBlog.WebUI.Server.ViewModels
{
    public class ArticleFormModel
    {

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string? Content { get; set; }
    }
}
