using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.API.Middleware;

public class BasicAuthenticationMiddleware {
    private const string ValidEmail = "admin@expense.com";
    private const string ValidPassword = "password";

    private readonly RequestDelegate _next;

    public BasicAuthenticationMiddleware(RequestDelegate next) {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context) {
        //  Bypass authenticationn for [AllowAnonnymous]
        if (context.GetEndpoint()?.Metadata.GetMetadata<IAllowAnonymous>() != null) {
            await _next(context);
            return;
        }

        // Try to retrieve the Request Header containing secret value
        string authHeader = context.Request.Headers["Authorization"];

        // If not found, then return with Unauthorized
        if (authHeader == null) {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync ("Authorization header not found.");
            return;
        }

        // Extract the credentials from the header  by splitting
        var auth = authHeader.Split([' ']) [1];

        // Convert from Base64 to string
        var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(auth));
        
        // Extract the email and password from the credentials
        var providedEmail = credentials.Split(':')[0];
        var providedPassword = credentials.Split(':')[1];

        // Validate credentials
        if (providedEmail == ValidEmail && providedPassword == ValidPassword) {
            await _next(context);
        } 
        // If invalid, then return with Unauthorized
        else {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid credentials.");
        }
    }
    
}

public static class BasicAuthenticationMiddlewareExtensions {
    public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder builder) {
        return builder.UseMiddleware<BasicAuthenticationMiddleware>();
    }
}