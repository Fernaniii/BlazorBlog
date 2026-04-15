using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Domain.AuditTrailing
{
    public interface IAuditRepository
    {
        Task<List<AuditLog>> GetLogsAsync();
    }
}
