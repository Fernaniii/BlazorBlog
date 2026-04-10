using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Application.DTOs
{
    public class AuditChanges
    {
        public Dictionary<string, object?> Old { get; set; } = new();
        public Dictionary<string, object?> New { get; set; } = new();
    }
}
