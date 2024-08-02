using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using SimpleApiExample.Models;

namespace SimpleApiExample.Database;

public partial class AppDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IConfiguration _configuration;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration
    ) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Employee>(Employee.Configure);
        modelBuilder.Entity<Tenant>(Tenant.Configure);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
