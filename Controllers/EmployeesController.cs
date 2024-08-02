using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApiExample.Database;
using SimpleApiExample.Dtos;
using SimpleApiExample.Models;

namespace SimpleApiExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
    {
        return (await _context.Employees.Include(e => e.Tenant).ToListAsync())
            .Select(e => e.ToDto())
            .ToList();
    }
}
