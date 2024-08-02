using Microsoft.EntityFrameworkCore;
using Npgsql;
using SimpleApiExample.Database;

namespace SimpleApiExample.Factories;

public class AppDbContextFactory : IDbContextFactory<AppDbContext>
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppDbContextFactory(
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public AppDbContext CreateDbContext()
    {
        var connectionString = GetConnectionString();
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.EnableDynamicJson();
        using var dataSource = dataSourceBuilder.Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(dataSource);

        return new AppDbContext(optionsBuilder.Options, _httpContextAccessor, _configuration);
    }

    private string GetConnectionString()
    {
        var host = _configuration.GetValue<string>("Database:Host");
        var database = _configuration.GetValue<string>("Database:Name");
        string? user = _configuration.GetValue<string>("Database:Username");
        string? password = _configuration.GetValue<string>("Database:Password");

        string? tenantId = _httpContextAccessor.HttpContext?.Items["TenantId"] as string;
        if (tenantId != null)
        {
            user = $"user{tenantId}";
        }

        return $"Host={host}; Database={database}; Username={user}; Password={password};";
    }
}
