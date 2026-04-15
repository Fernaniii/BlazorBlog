using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BlazorBlog.Domain.AuditTrailing;

public class AuditLog
{
    public int Id { get; private set; }

    public string TableName { get; private set; } = default!;
    public string Action { get; private set; } = default!;

    public string? UserId { get; private set; }

    public string? OldValues { get; private set; }
    public string? NewValues { get; private set; }

    public DateTime Timestamp { get; private set; }

    private AuditLog() { } // EF Core

    public static AuditLog Create(
        string tableName,
        string action,
        string? userId,
        object? oldValues,
        object? newValues)
    {
        return new AuditLog
        {
            TableName = tableName,
            Action = action,
            UserId = userId,
            OldValues = Serialize(oldValues),
            NewValues = Serialize(newValues),
            Timestamp = DateTime.UtcNow
        };
    }

    private static string? Serialize(object? value)
    {
        return value == null ? null : JsonSerializer.Serialize(value);
    }
}
