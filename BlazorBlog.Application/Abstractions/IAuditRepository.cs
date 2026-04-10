using BlazorBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Application.Abstractions;

public interface IAuditRepository
{
    Task<List<AuditLog>> GetLogsAsync();
}
