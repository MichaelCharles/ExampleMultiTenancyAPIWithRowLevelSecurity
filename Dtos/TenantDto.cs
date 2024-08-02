using System.Text.Json;
using SimpleApiExample.Models;
using SimpleApiExample.Models.DataModels;

namespace SimpleApiExample.Dtos;

public record TenantDto(Guid TenantUuid, string Name);

public static class TenantExtensions
{
    public static TenantDto ToDto(this Tenant tenant) =>
        new TenantDto(tenant.TenantUuid, tenant.Name);
}
