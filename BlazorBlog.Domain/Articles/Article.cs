using BlazorBlog.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Domain.Articles
{
    public class Article : Entity
    {

        public required string Title { get; set; }

        public string? Content { get; set; }
        public DateTime PublishedOn { get; set; } = DateTime.Now;

        public bool IsPublished { get; set; }   = false;

    }
}
