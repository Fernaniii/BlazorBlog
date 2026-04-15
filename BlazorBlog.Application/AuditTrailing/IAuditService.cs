using BlazorBlog.Domain.AuditTrailing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Application.AuditTrailing;

public interface IAuditService
{
    Task<List<AuditLog>> GetLogsAsync();
}
