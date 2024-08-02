using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SimpleApiExample.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // In the real version of this, we are deriving the TenantId from
            // the JWT token. Here we will randomly set it between "1" and "2".

            var random = new Random();
            var tenantId = random.Next(1, 3);

            context.Items["TenantId"] = $"{tenantId}";

            await _next(context);
        }
    }

    public static class TenantMiddlewareExtensions
    {
        public static IApplicationBuilder UseTenantMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}
