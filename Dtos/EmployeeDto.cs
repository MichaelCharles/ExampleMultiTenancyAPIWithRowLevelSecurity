using System.Text.Json;
using SimpleApiExample.Models;
using SimpleApiExample.Models.DataModels;

namespace SimpleApiExample.Dtos;

public record EmployeeDto(
    Guid EmployeeUuid,
    string Name,
    EmployeeSettings Settings,
    TenantDto Tenant
);

public static class EmployeeExtensions
{
    public static EmployeeDto ToDto(this Employee employee) =>
        new EmployeeDto(
            employee.EmployeeUuid,
            employee.Name,
            employee.Settings,
            employee.Tenant.ToDto()
        );
}
