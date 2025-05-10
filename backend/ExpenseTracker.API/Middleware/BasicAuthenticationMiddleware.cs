using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.API.Middleware;

public class BasicAuthenticationMiddleware {
    private const string ValidPassword = "password";
    private readonly RequestDelegate _next;

    public BasicAuthenticationMiddleware(RequestDelegate next) {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context) {
       // Try to retrieve the Request Header containing our secret value
       string? authHeaderValue = context.Request.Headers["X-My-Request-Header"];

       // If not found, then return with Unauthorized
       if (string.IsNullOrWhiteSpace(authHeaderValue)) {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Auth Header value not provided.");
        return;
       }

       // If value is not correct, then return with Unauthorized
       if (!string.Equals(authHeaderValue, ValidPassword)) {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Invalid Auth Header value.");
        return;
       }

       // Continue with request
       await _next(context);
    }
}

public static class BasicAuthenticationMiddlewareExtensions {
    public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder builder) {
        return builder.UseMiddleware<BasicAuthenticationMiddleware>();
    }
}

    
