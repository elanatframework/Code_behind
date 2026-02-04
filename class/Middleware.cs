using CodeBehind;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Text;
using System.Web;

public static class CodeBehindServiceExtensions
{
    public static IServiceCollection AddCodeBehind(this IServiceCollection services)
    {
        SetCodeBehind.CodeBehindCompiler.Initialization();
        return services;
    }
}

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

        string PageResult = execute.Run(context);

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

        await context.Response.WriteAsync(PageResult);

        await _next(context);
    }
}

public class UseCodeBehindAsyncMiddleware
{
    private readonly RequestDelegate _next;

    public UseCodeBehindAsyncMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = await execute.RunAsync(context);

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

        await context.Response.WriteAsync(PageResult);

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

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

        if (execute.FoundPage)
            await context.Response.WriteAsync(PageResult);
        else
            await context.Response.WriteAsync(execute.RunErrorPage(404, context));

        await _next(context);
    }
}

public class UseCodeBehindAsyncMiddlewareWithErrorHandling
{
    private readonly RequestDelegate _next;

    public UseCodeBehindAsyncMiddlewareWithErrorHandling(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = await execute.RunAsync(context);

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

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

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

        if (execute.FoundPage)
            await context.Response.WriteAsync(PageResult);
        else if (execute.IsAspxExtension)
            return;
        else
            await _next(context);
    }
}

public class UseCodeBehindNextNotFoundAsyncMiddleware
{
    private readonly RequestDelegate _next;

    public UseCodeBehindNextNotFoundAsyncMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = await execute.RunAsync(context);

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

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
    private readonly int _routeIndex;

    public UseCodeBehindRouteMiddleware(RequestDelegate next, int routeIndex)
    {
        _next = next;
        _routeIndex = routeIndex;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = execute.RunRoute(context, _routeIndex);

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

        await context.Response.WriteAsync(PageResult);

        await _next(context);
    }
}

public class UseCodeBehindRouteAsyncMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _routeIndex;

    public UseCodeBehindRouteAsyncMiddleware(RequestDelegate next, int routeIndex)
    {
        _next = next;
        _routeIndex = routeIndex;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = await execute.RunRouteAsync(context, _routeIndex);

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

        await context.Response.WriteAsync(PageResult);

        await _next(context);
    }
}

public class UseCodeBehindRouteMiddlewareWithErrorHandling
{
    private readonly RequestDelegate _next;
    private readonly int _routeIndex;

    public UseCodeBehindRouteMiddlewareWithErrorHandling(RequestDelegate next, int routeIndex)
    {
        _next = next;
        _routeIndex = routeIndex;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = execute.RunRoute(context, _routeIndex);

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

        if (execute.FoundController)
            await context.Response.WriteAsync(PageResult);
        else
            await context.Response.WriteAsync(execute.RunErrorPage(404, context));

        await _next(context);
    }
}

public class UseCodeBehindRouteAsyncMiddlewareWithErrorHandling
{
    private readonly RequestDelegate _next;
    private readonly int _routeIndex;

    public UseCodeBehindRouteAsyncMiddlewareWithErrorHandling(RequestDelegate next, int routeIndex)
    {
        _next = next;
        _routeIndex = routeIndex;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = await execute.RunRouteAsync(context, _routeIndex);

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

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

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

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

public class UseCodeBehindRouteNextNotFoundAsyncMiddleware
{
    private readonly RequestDelegate _next;

    public UseCodeBehindRouteNextNotFoundAsyncMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        CodeBehindExecute execute = new CodeBehindExecute();

        string PageResult = await execute.RunRouteAsync(context, 0);

        if (execute.UseSSE == true)
        {
            await new CodeBehindMiddlewareExtensions.UseCodeBehindSSEMiddleware(_next).Invoke(context, execute.SSEId);
            return;
        }

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

public class GzipDecompressionMiddleware
{
    private readonly RequestDelegate _next;

    public GzipDecompressionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("Content-Encoding", out var encoding) && encoding.ToString().Equals("gzip", StringComparison.OrdinalIgnoreCase))
        {
            var decompressedStream = new MemoryStream();
            using (var gzipStream = new GZipStream(context.Request.Body, CompressionMode.Decompress))
            {
                await gzipStream.CopyToAsync(decompressedStream);
            }

            decompressedStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = decompressedStream;

            context.Request.Headers.Remove("Content-Encoding");
        }

        await _next(context);
    }
}

public class GzipFileDecompressionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HashSet<string> _allowedExtensions;
    private readonly long _maxFileSize;

    public GzipFileDecompressionMiddleware(RequestDelegate next, IEnumerable<string> allowedExtensions, long maxFileSize)
    {
        _next = next;
        _allowedExtensions = new HashSet<string>(allowedExtensions, StringComparer.OrdinalIgnoreCase);
        _maxFileSize = maxFileSize;
    }

    public async Task Invoke(HttpContext context)
    {
        var blockedFiles = new List<string>();

        if (context.Request.Headers.TryGetValue("X-Files-Gzip", out var val) && val == "true" && context.Request.HasFormContentType)
        {
            context.Request.EnableBuffering();

            var form = await context.Request.ReadFormAsync();

            var formFields = form.ToDictionary(f => f.Key, f => f.Value);
            var newFiles = new FormFileCollection();

            foreach (var file in form.Files)
            {
                if (!file.FileName.EndsWith(".gz", StringComparison.OrdinalIgnoreCase))
                {
                    newFiles.Add(file);
                    continue;
                }

                var originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                var innerExtension = Path.GetExtension(originalFileName);

                if (!_allowedExtensions.Contains(innerExtension))
                {
                    blockedFiles.Add(file.FileName);
                    continue;
                }

                using var gzipStream = new GZipStream(file.OpenReadStream(), CompressionMode.Decompress);

                var memoryStream = new MemoryStream();
                var buffer = new byte[81920];
                int read;
                long totalRead = 0;

                while ((read = await gzipStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    totalRead += read;

                    if (totalRead > _maxFileSize)
                    {
                        blockedFiles.Add(file.FileName);
                        memoryStream.Dispose();
                        goto SkipFile;
                    }

                    await memoryStream.WriteAsync(buffer, 0, read);
                }

                memoryStream.Position = 0;

                var newFile = new FormFile(memoryStream, 0, memoryStream.Length, file.Name, originalFileName)
                {
                    Headers = file.Headers,
                    ContentType = "application/octet-stream"
                };

                newFiles.Add(newFile);

            SkipFile:
                continue;
            }

            context.Request.Form = new FormCollection(formFields, newFiles);
        }

        context.Items["BlockedFiles"] = blockedFiles;

        await _next(context);
    }
}

public static class CodeBehindMiddlewareExtensions
{
    public static IApplicationBuilder UseCodeBehind(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseCodeBehindMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindAsync(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseCodeBehindAsyncMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehind(this IApplicationBuilder app, bool ErrorHandling)
    {
        if (ErrorHandling)
            return app.UseMiddleware<UseCodeBehindMiddlewareWithErrorHandling>();
        else
            return app.UseMiddleware<UseCodeBehindMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindAsync(this IApplicationBuilder app, bool ErrorHandling)
    {
        if (ErrorHandling)
            return app.UseMiddleware<UseCodeBehindAsyncMiddlewareWithErrorHandling>();
        else
            return app.UseMiddleware<UseCodeBehindAsyncMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindNextNotFound(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseCodeBehindNextNotFoundMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindNextNotFoundAsync(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseCodeBehindNextNotFoundAsyncMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindRoute(this IApplicationBuilder app, int RouteIndex = 0)
    {
        return app.UseMiddleware<UseCodeBehindRouteMiddleware>(RouteIndex);
    }

    public static IApplicationBuilder UseCodeBehindRouteAsync(this IApplicationBuilder app, int RouteIndex = 0)
    {
        return app.UseMiddleware<UseCodeBehindRouteAsyncMiddleware>(RouteIndex);
    }

    public static IApplicationBuilder UseCodeBehindRoute(this IApplicationBuilder app, bool ErrorHandling, int RouteIndex = 0)
    {
        if (ErrorHandling)
            return app.UseMiddleware<UseCodeBehindRouteMiddlewareWithErrorHandling>(RouteIndex);
        else
            return app.UseMiddleware<UseCodeBehindRouteMiddleware>(RouteIndex);
    }

    public static IApplicationBuilder UseCodeBehindRouteAsync(this IApplicationBuilder app, bool ErrorHandling, int RouteIndex = 0)
    {
        if (ErrorHandling)
            return app.UseMiddleware<UseCodeBehindRouteAsyncMiddlewareWithErrorHandling>(RouteIndex);
        else
            return app.UseMiddleware<UseCodeBehindRouteAsyncMiddleware>(RouteIndex);
    }

    public static IApplicationBuilder UseCodeBehindRouteNextNotFound(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseCodeBehindRouteNextNotFoundMiddleware>();
    }

    public static IApplicationBuilder UseCodeBehindRouteNextNotFoundAsync(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseCodeBehindRouteNextNotFoundAsyncMiddleware>();
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

    // Gzip Decompression
    public static IApplicationBuilder UseGzipDecompression(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GzipDecompressionMiddleware>();
    }

    // Gzip File Decompression
    // Note: If You Using Gzip Decompression For Request Body, Using GzipDecompression Middleware Before UseGzipFileDecompression Middleware
    // Example For Allowed Files: var allowedFiles = new[] { ".jpg", ".png", ".pdf", ".txt" }; app.UseGzipFileDecompression(allowedFiles);
    public static IApplicationBuilder UseGzipFileDecompression(this IApplicationBuilder app, IEnumerable<string> AllowedExtensions, long MaxFileSize)
    {
        return app.UseMiddleware<GzipFileDecompressionMiddleware>(AllowedExtensions, MaxFileSize);
    }

    // WebSocket
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
                        if (formData.StartsWith("form=true&"))
                        {
                            formData = formData.Remove(0,10);
                            try
                            {
                                var formDictionary = new Dictionary<string, StringValues>();
                                var parsedQuery = HttpUtility.ParseQueryString(formData);

                                foreach (string key in parsedQuery)
                                    if (!formDictionary.ContainsKey(key))
                                        formDictionary[key] = new StringValues(parsedQuery.GetValues(key));

                                context.Request.Form = new FormCollection(formDictionary);
                            }
                            catch (Exception) { }
                        }
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

        public static bool WebSocketExists(WebSocket webSocket)
        {
            return WebSockets.Any(ws => ws.WebSocket == webSocket);
        }

        public static bool WebSocketExistsById(string webSocketId)
        {
            return WebSockets.Any(ws => ws.WebSocketId == webSocketId);
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

    // SSE
    public static IApplicationBuilder UseCodeBehindSSE(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseCodeBehindSSEMiddleware>();
    }

    public class UseCodeBehindSSEMiddleware
    {
        private readonly RequestDelegate _next;

        public UseCodeBehindSSEMiddleware()
        {

        }

        public UseCodeBehindSSEMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, string sseId)
        {
            context.Response.Headers.Add("Content-Type", "text/event-stream");
            context.Response.Headers.Add("Cache-Control", "no-cache");
            context.Response.Headers.Add("Connection", "keep-alive");

            if (!sseId.Has())
                sseId = new Random().Next(1, 1000000000).ToString();

            string roleName = "";
            var sessionFeature = context.Features.Get<ISessionFeature>();
            if (sessionFeature?.Session != null)
            {
                RoleAccess role = new RoleAccess(context.Session);
                roleName = role.GetUserRole();
            }

            string clientId = "";
            if (context.Request.Cookies.ContainsKey("SessionId"))
                clientId = context.Request.Cookies["SessionId"];

            string path = context.Request.Path;

            SSEManager.AddSSE(sseId, path, clientId, roleName);

            try
            {
                while (!context.RequestAborted.IsCancellationRequested)
                {
                    foreach (var client in SSEManager.GetAllSSEs())
                    {
                        if (client.SSEId == sseId)
                        {
                            foreach (string message in client.Message)
                            {
                                byte[] buffer = Encoding.UTF8.GetBytes("data: " + message + "\n\n");

                                await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                                await context.Response.Body.FlushAsync();
                            }
                            client.Message.Clear();
                        }
                    }

                    await Task.Delay(StaticObject.SseInterval);
                }
            }
            finally
            {
                SSEManager.RemoveSSE(sseId);
            }
        }
    }

    public static void SSEsBroadcast(HttpContext context, string broadcastMessage, string broadcastRoleName, string broadcastSSEId, string broadcastClientId, bool IgnoreThis)
    {
        string userSessionId = "";
        if (context.Request.Cookies.ContainsKey("SessionId"))
            userSessionId = context.Request.Cookies["SessionId"];

        foreach (var client in SSEManager.GetAllSSEs())
        {
            bool sendIt = false;
            if (IgnoreThis && userSessionId.Has() && (userSessionId == client.ClientId))
                continue;

            if (broadcastRoleName.Has() && broadcastSSEId.Has() && broadcastClientId.Has())
            {
                if ((broadcastRoleName == client.RoleName) && (broadcastSSEId == client.SSEId) && (broadcastClientId == client.ClientId))
                    sendIt = true;
            }
            else if (broadcastRoleName.Has() && broadcastClientId.Has())
            {
                if ((broadcastRoleName == client.RoleName) && (broadcastClientId == client.ClientId))
                    sendIt = true;
            }
            else if (broadcastSSEId.Has() && broadcastClientId.Has())
            {
                if ((broadcastSSEId == client.SSEId) && (broadcastClientId == client.ClientId))
                    sendIt = true;
            }
            else if (broadcastRoleName.Has() && broadcastSSEId.Has())
            {
                if ((broadcastRoleName == client.RoleName) && (broadcastSSEId == client.SSEId))
                    sendIt = true;
            }
            else if (broadcastRoleName.Has())
            {
                if (broadcastRoleName == client.RoleName)
                    sendIt = true;
            }
            else if (broadcastSSEId.Has())
            {
                if (broadcastSSEId == client.SSEId)
                    sendIt = true;
            }
            else if (broadcastClientId.Has())
            {
                if (broadcastClientId == client.ClientId)
                    sendIt = true;
            }
            else
                sendIt = true;

            if (sendIt)
                client.Message.Add(broadcastMessage);
            sendIt = false;
        }
    }

    public static class SSEManager
    {
        private static readonly List<SSEInfo> SSEs = new List<SSEInfo>();

        public static bool AddSSE(string sseId, string path, string clientId, string roleName = "")
        {
            if (clientId.Has())
                CheckMaxSSEConnections(clientId);

            SSEs.Add(new SSEInfo(sseId, path, roleName, clientId));
            return true;
        }

        public static void RemoveSSE(string sseId)
        {
            for (int i = 0; i < SSEs.Count; i++)
                if (SSEs[i].SSEId == sseId)
                {
                    SSEs.RemoveAt(i);
                    return;
                }
        }

        public static bool SSEExist(string sseId)
        {
            for (int i = 0; i < SSEs.Count; i++)
                if (SSEs[i].SSEId == sseId)
                    return true;

            return false;
        }

        public static void CheckMaxSSEConnections(string clientId)
        {
            int numberOfConnections = 0;

            for (int i = SSEs.Count - 1; i >= 0; i--)
                if (SSEs[i].ClientId == clientId)
                {
                    numberOfConnections++;

                    if (numberOfConnections >= StaticObject.MaxSSEConnectionsPerClient)
                    {
                        SSEs.RemoveAt(i);
                        return;
                    }
                }
        }

        public static List<SSEInfo> GetAllSSEs()
        {
            return SSEs;
        }
    }

    public class SSEInfo
    {
        public SSEInfo()
        {

        }

        public SSEInfo(string sseId, string ssePath, string roleName, string clientId)
        {
            SSEId = sseId;
            SSEPath = ssePath;
            RoleName = roleName;
            ClientId = clientId;
        }

        public string SSEId { get; set; }
        public string SSEPath { get; set; }
        public string RoleName { get; set; }
        public string ClientId { get; set; }
        public List<string> Message { get; set; } = new List<string>();
    }
}
