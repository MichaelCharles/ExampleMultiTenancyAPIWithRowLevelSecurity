using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleApiExample.Models;

public class Tenant
{
    public int TenantId { get; set; }

    public Guid TenantUuid { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    public static void Configure(EntityTypeBuilder<Tenant> entity)
    {
        entity.ToTable("Tenant");
    }
}
