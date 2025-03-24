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

public static class CodeBehindMiddlewareExtensions
{
    public static IApplicationBuilder UseCodeBehind(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseCodeBehindMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehind(this IApplicationBuilder app, bool ErrorHandling)
    {
        if (ErrorHandling)
            return app.UseMiddleware<UseCodeBehindMiddlewareWithErrorHandling>();
        else
            return app.UseMiddleware<UseCodeBehindMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindNextNotFound(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseCodeBehindNextNotFoundMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindRoute(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseCodeBehindRouteMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindRoute(this IApplicationBuilder app, bool ErrorHandling)
    {
        if (ErrorHandling)
            return app.UseMiddleware<UseCodeBehindRouteMiddlewareWithErrorHandling>();
        else
            return app.UseMiddleware<UseCodeBehindRouteMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindRouteNextNotFound(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseCodeBehindRouteNextNotFoundMiddleware>();
    }

    /// <summary>
    /// Session Must Be Activated
    /// </summary>
    public static IApplicationBuilder UseRoleAccess(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseRoleAccessMiddleware>();
    }

    /// <summary>
    /// Session Must Be Activated
    /// </summary>
    public static IApplicationBuilder UseRoleAccess(this IApplicationBuilder app, bool ErrorHandling)
    {
        if (ErrorHandling)
            return app.UseMiddleware<UseRoleAccessMiddlewareWithErrorHandling>();
        else
            return app.UseMiddleware<UseRoleAccessMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindWebSockets(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehind");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }


    public static IApplicationBuilder UseCodeBehindWebSocketsByRole(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehind");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSockets(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehind");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    /// <summary>
    /// Session Must Be Activated
    /// </summary>
    public static IApplicationBuilder UseCodeBehindWebSocketsByRole(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehind");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsWithErrorHandling(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindWithErrorHandling");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsWithErrorHandlingByRole(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindWithErrorHandling");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsWithErrorHandling(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindWithErrorHandling");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsWithErrorHandlingByRole(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindWithErrorHandling");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsNextNotFound(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindNextNotFound");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsNextNotFoundByRole(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindNextNotFound");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsNextNotFound(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindNextNotFound");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsNextNotFoundByRole(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindNextNotFound");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSockets(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRoute");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsByRole(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRoute");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSockets(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRoute");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsByRole(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRoute");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsWithErrorHandling(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteWithErrorHandling");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsWithErrorHandlingByRole(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteWithErrorHandling");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsWithErrorHandling(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteWithErrorHandling");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsWithErrorHandlingByRole(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteWithErrorHandling");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsNextNotFound(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteNextNotFound");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsNextNotFoundByRole(this IApplicationBuilder app)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteNextNotFound");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsNextNotFound(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteNextNotFound");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsNextNotFoundByRole(this IApplicationBuilder app, WebSocketOptions options)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                string clientId = "";
                if (context.Request.Cookies.ContainsKey("SessionId"))
                    clientId = context.Request.Cookies["SessionId"];

                if (WebSocketManager.AddWebSocket(webSocket, clientId, role.GetUserRole()))
                    await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteNextNotFound");
                else
                    context.Response.StatusCode = 503;
            }
            else
            {
                await next();
            }
        });
    }

    private static async Task HandleWebSocketConnection(HttpContext context, WebSocket webSocket, string middleware)
    {
        var buffer = new byte[StaticObject.WebSocketBufferSize];

        try
        {
            WebSocketReceiveResult receiveData;
            while (webSocket.State == WebSocketState.Open)
            {
                receiveData = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (receiveData.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                }
                else
                {
                    string formData = Encoding.UTF8.GetString(buffer, 0, receiveData.Count);

                    if (!string.IsNullOrEmpty(formData))
                    {
                        try
                        {
                            var formDictionary = new Dictionary<string, StringValues>();
                            var parsedQuery = HttpUtility.ParseQueryString(formData);

                            foreach (string key in parsedQuery)
                                if (!formDictionary.ContainsKey(key))
                                    formDictionary[key] = new StringValues(parsedQuery.GetValues(key));

                            context.Request.Form = new FormCollection(formDictionary);
                        }
                        catch (Exception ex) { }
                    }

                    string responseData = "";
                    CodeBehindExecute execute = new CodeBehindExecute();
                    switch (middleware)
                    {
                        case "UseCodeBehind":
                            responseData = execute.Run(context);
                            if (execute.WebSocketId != null)
                                WebSocketManager.UpdateWebSocketInfoByWebSocketId(webSocket, execute.WebSocketId);
                            break;

                        case "UseCodeBehindWithErrorHandling":
                            string pageResult1 = execute.Run(context);

                            if (execute.FoundPage)
                                responseData = pageResult1;
                            else
                                responseData = execute.RunErrorPage(404, context);

                            if (execute.WebSocketId != null)
                                WebSocketManager.UpdateWebSocketInfoByWebSocketId(webSocket, execute.WebSocketId);
                            break;

                        case "UseCodeBehindNextNotFound":
                            responseData = execute.Run(context);

                            if (!execute.FoundPage)
                            {
                                if (execute.IsAspxExtension)
                                    return;
                            }

                            if (execute.WebSocketId != null)
                                WebSocketManager.UpdateWebSocketInfoByWebSocketId(webSocket, execute.WebSocketId);
                            break;

                        case "UseCodeBehindRoute":
                            responseData = execute.RunRoute(context, 0);

                            if (execute.WebSocketId != null)
                                WebSocketManager.UpdateWebSocketInfoByWebSocketId(webSocket, execute.WebSocketId);
                            break;

                        case "UseCodeBehindRouteWithErrorHandling":
                            responseData = execute.RunRoute(context, 0);

                            if (!execute.FoundController)
                                responseData = execute.RunErrorPage(404, context);

                            if (execute.WebSocketId != null)
                                WebSocketManager.UpdateWebSocketInfoByWebSocketId(webSocket, execute.WebSocketId);
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
                            }

                            if (execute.WebSocketId != null)
                                WebSocketManager.UpdateWebSocketInfoByWebSocketId(webSocket, execute.WebSocketId);
                            break;
                    }

                    if (!responseData.Has())
                        continue;

                    buffer = Encoding.UTF8.GetBytes(responseData);
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
        finally
        {
            WebSocketManager.RemoveWebSocket(webSocket);
        }
    }

    public static async Task WebSocketsBroadcastAsync(HttpContext context, string broadcastMessage, string broadcastRoleName, string broadcastWebSocketId, string broadcastClientId, bool IgnoreThis)
    {
        var buffer = new byte[StaticObject.WebSocketBufferSize];
        buffer = Encoding.UTF8.GetBytes(broadcastMessage);

        string userSessionId = "";
        if (context.Request.Cookies.ContainsKey("SessionId"))
            userSessionId = context.Request.Cookies["SessionId"];

        foreach (var client in WebSocketManager.GetAllWebSockets())
        {
            bool sendIt = false;
            if (client.WebSocket.State == WebSocketState.Open)
            {
                if (IgnoreThis && userSessionId.Has() && (userSessionId == client.ClientId))
                    continue;

                if (broadcastRoleName.Has() && broadcastWebSocketId.Has() && broadcastClientId.Has())
                {
                    if ((broadcastRoleName == client.RoleName) && (broadcastWebSocketId == client.WebSocketId) && (broadcastClientId == client.ClientId))
                        sendIt = true;
                }
                else if (broadcastRoleName.Has() && broadcastClientId.Has())
                {
                    if ((broadcastRoleName == client.RoleName) && (broadcastClientId == client.ClientId))
                        sendIt = true;
                }
                else if (broadcastWebSocketId.Has() && broadcastClientId.Has())
                {
                    if ((broadcastWebSocketId == client.WebSocketId) && (broadcastClientId == client.ClientId))
                        sendIt = true;
                }
                else if (broadcastRoleName.Has() && broadcastWebSocketId.Has())
                {
                    if ((broadcastRoleName == client.RoleName) && (broadcastWebSocketId == client.WebSocketId))
                        sendIt = true;
                }
                else if (broadcastRoleName.Has())
                {
                    if (broadcastRoleName == client.RoleName)
                        sendIt = true;
                }
                else if (broadcastWebSocketId.Has())
                {
                    if (broadcastWebSocketId == client.WebSocketId)
                        sendIt = true;
                }
                else if (broadcastClientId.Has())
                {
                    if (broadcastClientId == client.ClientId)
                        sendIt = true;
                }
                else
                    sendIt = true;
            }
            if (sendIt)
                await client.WebSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
            sendIt = false;
        }
    }

    public static void WebSocketsBroadcast(HttpContext context, string broadcastMessage, string broadcastRoleName, string broadcastWebSocketId, string broadcastClientId, bool IgnoreThis)
    {
        WebSocketsBroadcastAsync(context, broadcastMessage, broadcastRoleName, broadcastWebSocketId, broadcastClientId, IgnoreThis).GetAwaiter().GetResult();
    }

    public static class WebSocketManager
    {
        private static readonly List<WebSocketInfo> WebSockets = new List<WebSocketInfo>();

        public static bool AddWebSocket(WebSocket webSocket, string clientId, string roleName = "", string webSocketId = "")
        {
            if (clientId.Has())
                CheckMaxWebSocketConnections(clientId);

            WebSockets.Add(new WebSocketInfo(webSocket, webSocketId, roleName, clientId));
            return true;
        }

        public static void RemoveWebSocket(WebSocket webSocket)
        {
            for (int i = 0; i < WebSockets.Count; i++)
                if (WebSockets[i].WebSocket == webSocket)
                {
                    WebSockets.RemoveAt(i);
                    return;
                }
        }

        public static void CheckMaxWebSocketConnections(string clientId)
        {
            int numberOfConnections = 0;

            for (int i = WebSockets.Count - 1; i >= 0; i--)
                if (WebSockets[i].ClientId == clientId)
                {
                    numberOfConnections++;

                    if (numberOfConnections >= StaticObject.MaxWebSocketConnectionsPerClient)
                    {
                        WebSockets.RemoveAt(i);
                        return;
                    }
                }
        }

        public static List<WebSocketInfo> GetAllWebSockets()
        {
            return WebSockets;
        }

        public static void UpdateWebSocketInfo(WebSocket webSocket, string newRoleName, string newWebSocketId)
        {
            for (int i = 0; i < WebSockets.Count; i++)
                if (WebSockets[i].WebSocket == webSocket)
                {
                    WebSockets[i].RoleName = newRoleName;
                    WebSockets[i].WebSocketId = newWebSocketId;
                    return;
                }
        }

        public static void UpdateWebSocketInfoByRoleName(WebSocket webSocket, string newRoleName)
        {
            for (int i = 0; i < WebSockets.Count; i++)
                if (WebSockets[i].WebSocket == webSocket)
                {
                    WebSockets[i].RoleName = newRoleName;
                    return;
                }
        }

        public static void UpdateWebSocketInfoByWebSocketId(WebSocket webSocket, string newWebSocketId)
        {
            for (int i = 0; i < WebSockets.Count; i++)
                if (WebSockets[i].WebSocket == webSocket)
                {
                    WebSockets[i].WebSocketId = newWebSocketId;
                    return;
                }
        }
    }

    public class WebSocketInfo
    {
        public WebSocketInfo()
        {

        }

        public WebSocketInfo(WebSocket webSocket, string webSocketId, string roleName, string clientId)
        {
            WebSocket = webSocket;
            WebSocketId = webSocketId;
            RoleName = roleName;
            ClientId = clientId;
        }

        public WebSocket WebSocket { get; set; }
        public string WebSocketId { get; set; }
        public string RoleName { get; set; }
        public string ClientId { get; set; }
    }
}
