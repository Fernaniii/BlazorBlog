using BlazorBlog.Domain.Abstractions;

namespace BlazorBlog.Domain.Articles
{
    public class Article : Entity
    {

        public string Title { get; set; }

        public string? Content { get; set; }
        public DateTime PublishedOn { get; set; } = DateTime.Now;

        public bool IsPublished { get; set; } = false;

        private Article()
        {

        }

        private Article(string title, string? content)
        {
            SetTitle(title);
            SetContent(content);
            PublishedOn = DateTime.UtcNow;
        }

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
                throw new ArgumentException("Title cannot be empty");

            Title = title;
            Content = content;
        }

    }
}
