using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleApiExample.Models.DataModels;

namespace SimpleApiExample.Models;

public class Employee
{
    public int TenantId { get; set; }
    public int EmployeeId { get; set; }
    public Guid EmployeeUuid { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "jsonb")]
    public EmployeeSettings Settings { get; set; } = new EmployeeSettings();

    public Tenant Tenant { get; set; } = null!;

    public static void Configure(EntityTypeBuilder<Employee> entity)
    {
        entity.ToTable("Employee");

        entity.HasOne(e => e.Tenant).WithMany().HasForeignKey(e => e.TenantId);
    }
}
