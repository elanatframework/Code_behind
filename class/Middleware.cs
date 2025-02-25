using CodeBehind;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net.WebSockets;
using System.Text;
using System.Web;

public class UseCodeBehindMiddleware
{
    private readonly RequestDelegate _next;

    public UseCodeBehindMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehindExecute execute = new CodeBehindExecute();
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
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = execute.Run(context);

        if (execute.FoundPage)
            await context.Response.WriteAsync(PageResult);
        else
            await context.Response.WriteAsync(execute.RunErrorPage(404, context));

        await _next(context);
    }
}

public class UseCodeBehindNextNotFoundMiddleware
{
    private readonly RequestDelegate _next;

    public UseCodeBehindNextNotFoundMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = execute.Run(context);

        if (execute.FoundPage)
            await context.Response.WriteAsync(PageResult);
        else if (execute.IsAspxExtension)
            return;
        else
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
        CodeBehindExecute execute = new CodeBehindExecute();
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
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = execute.RunRoute(context, 0);

        if (execute.FoundController)
            await context.Response.WriteAsync(PageResult);
        else
            await context.Response.WriteAsync(execute.RunErrorPage(404, context));

        await _next(context);
    }
}

public class UseCodeBehindRouteNextNotFoundMiddleware
{
    private readonly RequestDelegate _next;

    public UseCodeBehindRouteNextNotFoundMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = execute.RunRoute(context, 0);

        if (execute.FoundController)
            await context.Response.WriteAsync(PageResult);
        else
        {
            string path = context.Request.Path.ToString();
            path = System.Net.WebUtility.UrlDecode(path);
            string extension = Path.GetExtension(path);

            if (extension == ".aspx")
                return;
            else
                await _next(context);
        }
    }
}

public class UseRoleAccessMiddleware
{
    private readonly RequestDelegate _next;

    public UseRoleAccessMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        RoleAccess access = new RoleAccess(context.Session);

        if (!access.HasAccess(context.Request))
            return;

        await _next(context);
    }
}

public class UseRoleAccessMiddlewareWithErrorHandling
{
    private readonly RequestDelegate _next;

    public UseRoleAccessMiddlewareWithErrorHandling(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        RoleAccess access = new RoleAccess(context.Session);

        if (!access.HasAccess(context.Request))
        {
            CodeBehindExecute execute = new CodeBehindExecute();
            await context.Response.WriteAsync(execute.RunErrorPage(403, context));

            return;
        }

        await _next(context);
    }
}

public class UseCodeBehindWebSocketsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly WebSocketHandler _webSocketHandler;

    public UseCodeBehindWebSocketsMiddleware(RequestDelegate next)
    {
        _next = next;
        _webSocketHandler = new WebSocketHandler(_next);
    }

    public async Task Invoke(HttpContext context)
    {
        await _webSocketHandler.HandleWebSocketRequest(context, "UseCodeBehind");
        await _next(context);
    }
}

public class UseCodeBehindWebSocketsMiddlewareWithErrorHandling
{
    private readonly RequestDelegate _next;
    private readonly WebSocketHandler _webSocketHandler;

    public UseCodeBehindWebSocketsMiddlewareWithErrorHandling(RequestDelegate next)
    {
        _next = next;
        _webSocketHandler = new WebSocketHandler(_next);
    }

    public async Task Invoke(HttpContext context)
    {
        await _webSocketHandler.HandleWebSocketRequest(context, "UseCodeBehindWithErrorHandling");
        await _next(context);
    }
}

public class UseCodeBehindWebSocketsNextNotFoundMiddleware
{
    private readonly RequestDelegate _next;
    private readonly WebSocketHandler _webSocketHandler;

    public UseCodeBehindWebSocketsNextNotFoundMiddleware(RequestDelegate next)
    {
        _next = next;
        _webSocketHandler = new WebSocketHandler(_next);
    }

    public async Task Invoke(HttpContext context)
    {
        await _webSocketHandler.HandleWebSocketRequest(context, "UseCodeBehindNextNotFound");
        await _next(context);
    }
}

public class UseCodeBehindWebSocketsRouteMiddleware
{
    private readonly RequestDelegate _next;
    private readonly WebSocketHandler _webSocketHandler;

    public UseCodeBehindWebSocketsRouteMiddleware(RequestDelegate next)
    {
        _next = next;
        _webSocketHandler = new WebSocketHandler(_next);
    }

    public async Task Invoke(HttpContext context)
    {
        await _webSocketHandler.HandleWebSocketRequest(context, "UseCodeBehindRoute");
        await _next(context);
    }
}

public class UseCodeBehindWebSocketsRouteMiddlewareWithErrorHandling
{
    private readonly RequestDelegate _next;
    private readonly WebSocketHandler _webSocketHandler;

    public UseCodeBehindWebSocketsRouteMiddlewareWithErrorHandling(RequestDelegate next)
    {
        _next = next;
        _webSocketHandler = new WebSocketHandler(_next);
    }

    public async Task Invoke(HttpContext context)
    {
        await _webSocketHandler.HandleWebSocketRequest(context, "UseCodeBehindRouteWithErrorHandling");
        await _next(context);
    }
}

public class UseCodeBehindWebSocketsRouteNextNotFoundMiddleware
{
    private readonly RequestDelegate _next;
    private readonly WebSocketHandler _webSocketHandler;

    public UseCodeBehindWebSocketsRouteNextNotFoundMiddleware(RequestDelegate next)
    {
        _next = next;
        _webSocketHandler = new WebSocketHandler(_next);
    }

    public async Task Invoke(HttpContext context)
    {
        await _webSocketHandler.HandleWebSocketRequest(context, "UseCodeBehindRouteNextNotFound");
        await _next(context);
    }
}

public class WebSocketHandler
{
    private readonly RequestDelegate _next;

    public WebSocketHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task HandleWebSocketRequest(HttpContext context, string Middleware)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult receiveData;

            while (true)
            {
                receiveData = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string formData = Encoding.UTF8.GetString(buffer, 0, receiveData.Count);

                if (formData.Has())
                {
                    var formDictionary = new Dictionary<string, StringValues>();
                    var parsedQuery = HttpUtility.ParseQueryString(formData);

                    foreach (string key in parsedQuery)
                        if (!formDictionary.ContainsKey(key))
                            formDictionary[key] = new StringValues(parsedQuery.GetValues(key));

                    context.Request.Form = new FormCollection(formDictionary);
                }

                if (receiveData.CloseStatus.HasValue)
                    break;

                string responseData = "";
                bool useNext = false;
                CodeBehindExecute execute = new CodeBehindExecute();
                switch (Middleware)
                {
                    case "UseCodeBehind":
                        responseData = execute.Run(context);
                        break;

                    case "UseCodeBehindWithErrorHandling":
                        string pageResult1 = execute.Run(context);

                        if (execute.FoundPage)
                            responseData = pageResult1;
                        else
                            responseData = execute.RunErrorPage(404, context);
                        break;

                    case "UseCodeBehindNextNotFound":
                        responseData = execute.Run(context);

                        if (!execute.FoundPage)
                        {
                            if (execute.IsAspxExtension)
                                return;
                            else
                                await _next(context);
                        }
                        break;

                    case "UseCodeBehindRoute":
                        responseData = execute.RunRoute(context, 0);
                        break;

                    case "UseCodeBehindRouteWithErrorHandling":
                        responseData = execute.RunRoute(context, 0);

                        if (!execute.FoundController)
                            responseData = execute.RunErrorPage(404, context);
                        break;

                    case "UseCodeBehindRouteNextNotFound":
                        responseData = execute.RunRoute(context, 0);

                        if (!execute.FoundController)
                        {
                            string path = context.Request.Path.ToString();
                            path = System.Net.WebUtility.UrlDecode(path);
                            string extension = Path.GetExtension(path);

                            if (extension == ".aspx")
                                return;
                            else
                                await _next(context);
                        }
                        break;
                }

                buffer = Encoding.UTF8.GetBytes(responseData);

                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), receiveData.MessageType, receiveData.EndOfMessage, CancellationToken.None);
            }

            await webSocket.CloseAsync(receiveData.CloseStatus.Value, receiveData.CloseStatusDescription, CancellationToken.None);
        }
    }
}

public static class WebSocketMiddlewareExtensions
{
    public static IApplicationBuilder UseWebSocketsIf(this IApplicationBuilder app, WebSocketOptions options, string matchingType, string matching)
    {
        return app.Use(async (context, next) =>
        {
            if (context.Request.Path.HasMatching(matchingType, matching))
            {
                app.UseWebSockets(options);
                await next();
            }
            else
            {
                await next();
            }
        });
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

    public static IApplicationBuilder UseCodeBehindNextNotFound(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseCodeBehindNextNotFoundMiddleware>();
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

    public static IApplicationBuilder UseCodeBehindRouteNextNotFound(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseCodeBehindRouteNextNotFoundMiddleware>();
    }

    /// <summary>
    /// Session Must Be Activated
    /// </summary>
    public static IApplicationBuilder UseRoleAccess(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseRoleAccessMiddleware>();
    }

    /// <summary>
    /// Session Must Be Activated
    /// </summary>
    public static IApplicationBuilder UseRoleAccess(this IApplicationBuilder builder, bool ErrorHandling)
    {
        if (ErrorHandling)
            return builder.UseMiddleware<UseRoleAccessMiddlewareWithErrorHandling>();
        else
            return builder.UseMiddleware<UseRoleAccessMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindWebSockets(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseCodeBehindWebSocketsMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindWebSockets(this IApplicationBuilder builder, bool ErrorHandling)
    {
        if (ErrorHandling)
            return builder.UseMiddleware<UseCodeBehindWebSocketsMiddlewareWithErrorHandling>();
        else
            return builder.UseMiddleware<UseCodeBehindWebSocketsMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsNextNotFound(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseCodeBehindWebSocketsNextNotFoundMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsRoute(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseCodeBehindWebSocketsRouteMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsRoute(this IApplicationBuilder builder, bool ErrorHandling)
    {
        if (ErrorHandling)
            return builder.UseMiddleware<UseCodeBehindWebSocketsRouteMiddlewareWithErrorHandling>();
        else
            return builder.UseMiddleware<UseCodeBehindWebSocketsRouteMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsRouteNextNotFound(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseCodeBehindWebSocketsRouteNextNotFoundMiddleware>();
    }
}
