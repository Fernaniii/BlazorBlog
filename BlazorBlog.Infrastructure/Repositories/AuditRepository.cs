using BlazorBlog.Application.Abstractions;
using BlazorBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBlog.Infrastructure.Repositories;

public class AuditRepository : IAuditRepository
{

    private readonly ApplicationDbContext _context;

    public AuditRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AuditLog>> GetLogsAsync()
    {
        return await _context.AuditLogs
             .AsNoTracking()
             .OrderByDescending(x => x.Timestamp)
             .ToListAsync();
    }
}
