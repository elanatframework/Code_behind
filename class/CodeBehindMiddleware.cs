using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public class UseCodeBehindMiddleware
{
    private readonly RequestDelegate _next;

    public UseCodeBehindMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehind.CodeBehindExecute execute = new CodeBehind.CodeBehindExecute();
        await context.Response.WriteAsync(execute.Run(context));

        await _next(context);
    }
}

public class UseCodeBehindMiddlewareWithErrorHandling
{
    private readonly RequestDelegate _next;

    public UseCodeBehindMiddlewareWithErrorHandling(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehind.CodeBehindExecute execute = new CodeBehind.CodeBehindExecute();

        string PageResult = execute.Run(context);

        if (execute.FoundPage)
            await context.Response.WriteAsync(PageResult);
        else
            await context.Response.WriteAsync(execute.RunErrorPage(404, context));

        await _next(context);
    }
}

public class UseCodeBehindRouteMiddleware
{
    private readonly RequestDelegate _next;

    public UseCodeBehindRouteMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehind.CodeBehindExecute execute = new CodeBehind.CodeBehindExecute();
        await context.Response.WriteAsync(execute.RunRoute(context, 0));

        await _next(context);
    }
}

public class UseCodeBehindRouteMiddlewareWithErrorHandling
{
    private readonly RequestDelegate _next;

    public UseCodeBehindRouteMiddlewareWithErrorHandling(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehind.CodeBehindExecute execute = new CodeBehind.CodeBehindExecute();

        string PageResult = execute.Run(context);

        if (execute.FoundPage)
            await context.Response.WriteAsync(execute.RunRoute(context, 0));
        else
            await context.Response.WriteAsync(execute.RunErrorPage(404, context));

        await _next(context);
    }
}

public class UseRollAccessMiddleware
{
    private readonly RequestDelegate _next;

    public UseRollAccessMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehind.RoleAccess access = new CodeBehind.RoleAccess(context.Session);

        if (!access.HasAccess(context.Request))
            return;

        await _next(context);
    }
}

public class UseRollAccessMiddlewareWithErrorHandling
{
    private readonly RequestDelegate _next;

    public UseRollAccessMiddlewareWithErrorHandling(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehind.RoleAccess access = new CodeBehind.RoleAccess(context.Session);

        if (!access.HasAccess(context.Request))
        {
            CodeBehind.CodeBehindExecute execute = new CodeBehind.CodeBehindExecute();
            await context.Response.WriteAsync(execute.RunErrorPage(403, context));

            return;
        }

        await _next(context);
    }
}

public static class CodeBehindMiddlewareExtensions
{
    public static IApplicationBuilder UseCodeBehind(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseCodeBehindMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehind(this IApplicationBuilder builder, bool ErrorHandling)
    {
        if (ErrorHandling)
            return builder.UseMiddleware<UseCodeBehindMiddlewareWithErrorHandling>();
        else
            return builder.UseMiddleware<UseCodeBehindMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindRoute(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseCodeBehindRouteMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindRoute(this IApplicationBuilder builder, bool ErrorHandling)
    {
        if (ErrorHandling)
            return builder.UseMiddleware<UseCodeBehindRouteMiddlewareWithErrorHandling>();
        else
            return builder.UseMiddleware<UseCodeBehindRouteMiddleware>();
    }

    /// <summary>
    /// Session Must Be Activated
    /// </summary>
    public static IApplicationBuilder UseRollAccess(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseRollAccessMiddleware>();
    }

    /// <summary>
    /// Session Must Be Activated
    /// </summary>
    public static IApplicationBuilder UseRollAccess(this IApplicationBuilder builder, bool ErrorHandling)
    {
        if (ErrorHandling)
            return builder.UseMiddleware<UseRollAccessMiddlewareWithErrorHandling>();
        else
            return builder.UseMiddleware<UseRollAccessMiddleware>();
    }
}
