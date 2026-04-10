using BlazorBlog.Application.Abstractions;
using BlazorBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Application.Services;

public class AuditService : IAuditRepository
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
