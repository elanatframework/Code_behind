using CodeBehind;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Concurrent;
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

    public static WebSocketsBroadcastCollection WebSocketsBroadcastQueue = new WebSocketsBroadcastCollection();

    public static IApplicationBuilder UseCodeBehindWebSockets(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehind", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsByRole(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehind", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSockets(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehind", bufferSize);
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
    public static IApplicationBuilder UseCodeBehindWebSocketsByRole(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehind", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsWithErrorHandling(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindWithErrorHandling", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsWithErrorHandlingByRole(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindWithErrorHandling", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsWithErrorHandling(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindWithErrorHandling", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsWithErrorHandlingByRole(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindWithErrorHandling", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsNextNotFound(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindNextNotFound", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsNextNotFoundByRole(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindNextNotFound", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsNextNotFound(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindNextNotFound", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindWebSocketsNextNotFoundByRole(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindNextNotFound", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSockets(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRoute", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsByRole(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRoute", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSockets(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRoute", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsByRole(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRoute", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsWithErrorHandling(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteWithErrorHandling", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsWithErrorHandlingByRole(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteWithErrorHandling", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsWithErrorHandling(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteWithErrorHandling", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsWithErrorHandlingByRole(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteWithErrorHandling", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsNextNotFound(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteNextNotFound", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsNextNotFoundByRole(this IApplicationBuilder app, int bufferSize = 4096)
    {
        app.UseWebSockets();

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteNextNotFound", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsNextNotFound(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket);
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteNextNotFound", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    public static IApplicationBuilder UseCodeBehindRouteWebSocketsNextNotFoundByRole(this IApplicationBuilder app, WebSocketOptions options, int bufferSize = 4096)
    {
        app.UseWebSockets(options);

        return app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                RoleAccess role = new RoleAccess(context.Session);

                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketManager.AddWebSocket(webSocket, role.GetUserRole());
                await HandleWebSocketConnection(context, webSocket, "UseCodeBehindRouteNextNotFound", bufferSize);
            }
            else
            {
                await next();
            }
        });
    }

    private static async Task HandleWebSocketConnection(HttpContext context, WebSocket webSocket, string middleware, int bufferSize, int broadcastDelay = 1000)
    {
        var buffer = new byte[bufferSize];
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var broadcastTask = Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (WebSocketsBroadcastQueue.Count() > 0)
                {
                    string message = WebSocketsBroadcastQueue.GetMessageByIndex(0);
                    string roleName = WebSocketsBroadcastQueue.GetRoleNameByIndex(0);
                    string webSocketId = WebSocketsBroadcastQueue.GetWebSocketIdByIndex(0);
                    WebSocketsBroadcastQueue.DeleteByIndex(0);
                    buffer = Encoding.UTF8.GetBytes(message);

                    foreach (var client in WebSocketManager.GetAllWebSockets())
                        if (client.Key.State == WebSocketState.Open)
                        {
                            if (roleName.Has() && webSocketId.Has())
                            {
                                if ((roleName == client.Value.Item1) && (webSocketId == client.Value.Item2))
                                    await client.Key.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                            }
                            else if (roleName.Has())
                            {
                                if (roleName == client.Value.Item1)
                                    await client.Key.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                            }
                            else if (webSocketId.Has())
                            {
                                if (webSocketId == client.Value.Item2)
                                    await client.Key.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                            }
                            else
                                await client.Key.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                }

                // Prevent Loop
                await Task.Delay(broadcastDelay);
            }
        }, cancellationToken);

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

                    buffer = Encoding.UTF8.GetBytes(responseData);
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
        finally
        {
            WebSocketManager.RemoveWebSocket(webSocket);
            cancellationTokenSource.Cancel();
            await broadcastTask;
        }
    }

    public static class WebSocketManager
    {
        // Item1 Used For Role Name And Item2 Used For WebSocket Id
        private static readonly ConcurrentDictionary<WebSocket, Tuple<string, string>> WebSockets = new ConcurrentDictionary<WebSocket, Tuple<string, string>>();

        public static void AddWebSocket(WebSocket webSocket, string roleName = "", string additionalInfo = "")
        {
            WebSockets.TryAdd(webSocket, new Tuple<string, string>(roleName, additionalInfo));
        }

        public static void RemoveWebSocket(WebSocket webSocket)
        {
            WebSockets.TryRemove(webSocket, out _);
        }

        public static IEnumerable<KeyValuePair<WebSocket, Tuple<string, string>>> GetAllWebSockets()
        {
            return WebSockets;
        }

        public static void UpdateWebSocketInfo(WebSocket webSocket, string newRoleName, string newId)
        {
            if (WebSockets.ContainsKey(webSocket))
            {
                WebSockets[webSocket] = new Tuple<string, string>(newRoleName, newId);
            }
        }

        public static void UpdateWebSocketInfoByRoleName(WebSocket webSocket, string newRoleName)
        {
            if (WebSockets.TryGetValue(webSocket, out var currentInfo))
            {
                var updatedInfo = new Tuple<string, string>(newRoleName, currentInfo.Item1);
                WebSockets[webSocket] = updatedInfo;
            }
        }

        public static void UpdateWebSocketInfoByWebSocketId(WebSocket webSocket, string newId)
        {
            if (WebSockets.TryGetValue(webSocket, out var currentInfo))
            {
                var updatedInfo = new Tuple<string, string>(currentInfo.Item2, newId);
                WebSockets[webSocket] = updatedInfo;
            }
        }
    }
}
