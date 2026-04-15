using BlazorBlog.Domain.AuditTrailing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Application.AuditTrailing;

public class AuditService : IAuditService
{
    private readonly IAuditRepository _repo;

    public AuditService(IAuditRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<AuditLog>> GetLogsAsync()
    {
        return await _repo.GetLogsAsync();
    }
}
