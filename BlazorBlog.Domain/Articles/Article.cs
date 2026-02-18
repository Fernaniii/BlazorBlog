using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Domain.Articles
{
    public class Article
    {

        public int Id { get; set; }
        public required string Title { get; set; }

        public string? Content { get; set; }
        public DateTime PublishedOn { get; set; } = DateTime.Now;

        public bool IsPublished { get; set; }   = false;

    }
}
