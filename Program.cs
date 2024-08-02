using Microsoft.EntityFrameworkCore;
using Npgsql;
using SimpleApiExample.Database;
using SimpleApiExample.Factories;
using SimpleApiExample.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContextFactory<AppDbContext, AppDbContextFactory>();
builder.Services.AddScoped(
    p => p.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext()
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseTenantMiddleware();

app.Run();
