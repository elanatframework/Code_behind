using SetCodeBehind;
using CodeBehind.HtmlData;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace CodeBehind
{
    public class CodeBehindExecute
    {
        public bool FoundPage { get; private set; } = true;
        public bool FoundController { get; private set; } = true;
        public bool IsAspxExtension { get; private set; } = false;
        public string? WebSocketId { get; private set; }
        public string? SSEId { get; private set; }
        public bool? UseSSE { get; private set; }

        private async Task<string> RunByContextAsync(HttpContext context, string MethodName, string QueryString = "")
        {
            string path = context.Request.Path.ToString();
            path = System.Net.WebUtility.UrlDecode(path);
            string extension = Path.GetExtension(path);

            if (StaticObject.PreventAccessDefaultAspx && MethodName == "SetPageLoadByPath")
                if (path.EndsWith("/Default.aspx") || path.Contains("/Default.aspx/"))
                {
                    FoundPage = false;
                    return "";
                }

            bool HasSegment = path.Contains(".aspx/");

            if (string.IsNullOrEmpty(extension) && !HasSegment)
            {
                bool AddSlash = true;

                if (path.Length > 0)
                    AddSlash = (path[path.Length - 1] != '/');

                if (!string.IsNullOrEmpty(QueryString))
                    path = path + (AddSlash ? "/" : "") + "Default.aspx?" + QueryString;
                else
                    path = path + (AddSlash ? "/" : "") + "Default.aspx";

                extension = ".aspx";
            }

            IsAspxExtension = extension == ".aspx";

            if (extension == ".aspx" || HasSegment)
            {
                // Add QueryString Value
                new RequestQuery().AddQueryString(context, QueryString);

                if (context.Request.ContentType == null)
                    context.Request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

                Type type = CodeBehindCompiler.CompileAspxAndReturnType();
                object obj = Activator.CreateInstance(type);
                MethodInfo method;
                object[] Arguments;
                if (MethodName == "SetPageLoadByFullPath")
                {
                    method = CodeBehindCompiler.CompileAspxStaticMethodSetPageLoadByFullPath();
                    Arguments = new object[] { path, context, "" };
                }
                else
                {
                    method = CodeBehindCompiler.CompileAspxStaticMethodSetPageLoadByPath();
                    Arguments = new object[] { path, context };
                }
                string ReturnResult = await (Task<string>)method.Invoke(obj, Arguments);

                method = CodeBehindCompiler.CompileAspxStaticMethodPageHasFound();
                FoundPage = (bool)method.Invoke(obj, null);

                method = CodeBehindCompiler.CompileAspxStaticMethodGetWebSocketId();
                WebSocketId = (string)method.Invoke(obj, null);

                method = CodeBehindCompiler.CompileAspxStaticMethodGetSSEId();
                SSEId = (string)method.Invoke(obj, null);

                method = CodeBehindCompiler.CompileAspxStaticMethodGetUseSSE();
                UseSSE = (bool)method.Invoke(obj, null);

                // Set Web-Forms Control
                method = CodeBehindCompiler.CompileAspxStaticMethodGetWebFormsValue();
                string WebFormsValue = (string)method.Invoke(obj, null);

                if (!string.IsNullOrEmpty(WebFormsValue))
                {
                    bool HasPostBack = false;

                    if (context.Request.Headers.TryGetValue("Post-Back", out var value))
                    {
                        if (value == "true")
                        {
                            HasPostBack = true;
                            context.Response.Headers.Add("Content-Type", "text/plain");
                        }
                    }
                    else if (context.Request.Headers.TryGetValue("Upgrade", out var value2))
                    {
                        if (value2 == "websocket")
                            HasPostBack = true;
                    }

                    if (HasPostBack)
                        ReturnResult = SetWebFormsCombinate(ReturnResult, WebFormsValue);
                    else
                        ReturnResult = SetWebFormsCombinateFirstResponse(ReturnResult, WebFormsValue);
                }

                return ReturnResult;
            }

            FoundPage = false;

            return "";
        }

        private string RunByContext(HttpContext context, string MethodName, string QueryString = "")
        {
            return RunByContextAsync(context, MethodName, QueryString).GetAwaiter().GetResult();
        }

        /// <summary>
        /// It Works Based On Rewriting The Option File
        /// </summary>
        public string Run(HttpContext context)
        {
            return RunByContext(context, "SetPageLoadByPath");
        }

        /// <summary>
        /// Load All Page By Full Path, This Method Load Break Page And Does Not Apply Rewrite
        /// </summary>
        public string RunFullPath(HttpContext context)
        {
            return RunByContext(context, "SetPageLoadByFullPath");
        }

        public async Task<string> RunAsync(HttpContext context)
        {
            return await RunByContextAsync(context, "SetPageLoadByPathAsync");
        }

        public async Task<string> RunFullPathAsync(HttpContext context)
        {
            return await RunByContextAsync(context, "SetPageLoadByFullPathAsync");
        }

        // Overload
        private async Task<string> PrivateRun(HttpContext context, string Path, string MethodName)
        {
            string SavedPath = context.Request.Path;
            string QueryString = "";

            if (Path.Contains("?"))
            {
                context.Request.Path = Path.GetTextBeforeValue("?");
                QueryString = Path.GetTextAfterValue("?");
            }
            else
                context.Request.Path = Path;

            string ReturnValue = await RunByContextAsync(context, MethodName, QueryString);

            context.Request.Path = SavedPath;

            return ReturnValue;
        }

        // Overload
        /// <summary>
        /// It Works Based On Rewriting The Option File
        /// </summary>
        public string Run(HttpContext context, string Path)
        {
            return PrivateRun(context, Path, "SetPageLoadByPath").GetAwaiter().GetResult();
        }

        // Overload
        /// <summary>
        /// Load All Page By Full Path, This Method Load Break Page And Does Not Apply Rewrite
        /// </summary>
        public string RunFullPath(HttpContext context, string Path)
        {
            return PrivateRun(context, Path, "SetPageLoadByFullPath").GetAwaiter().GetResult();
        }

        public async Task<string> RunAsync(HttpContext context, string Path)
        {
            return await PrivateRun(context, Path, "SetPageLoadByPath");
        }

        public async Task<string> RunFullPathAsync(HttpContext context, string Path)
        {
            return await PrivateRun(context, Path, "SetPageLoadByFullPath");
        }

        // Overload
        /// <summary>
        /// This Overload Method Does Not Support HttpContext And Sends null Value Instead Of HttpContext.
        /// This Overload Method Does Not Support Query String
        /// This Overload Method Does Not Support Web-Forms Control
        /// </summary>
        private async Task<string> RunByPathAsync(string path, string MethodName)
        {
            string extension = Path.GetExtension(path);
            path = System.Net.WebUtility.UrlDecode(path);
            path = path.GetTextBeforeValue("?");

            if (StaticObject.PreventAccessDefaultAspx && MethodName == "SetPageLoadByPath")
                if (path.EndsWith("/Default.aspx") || path.Contains("/Default.aspx/"))
                {
                    FoundPage = false;
                    return "";
                }

            bool HasSegment = path.Contains(".aspx/");

            if (string.IsNullOrEmpty(extension) && !HasSegment)
            {
                bool AddSlash = true;

                if (path.Length > 0)
                    AddSlash = (path[path.Length - 1] != '/');

                path = path + (AddSlash ? "/" : "") + "Default.aspx";

                extension = ".aspx";
            }

            IsAspxExtension = extension == ".aspx";

            if (extension == ".aspx" || HasSegment)
            {
                Type type = CodeBehindCompiler.CompileAspxAndReturnType();
                object obj = Activator.CreateInstance(type);
                MethodInfo method;
                object[] Arguments;
                if (MethodName == "SetPageLoadByFullPath")
                {
                    method = CodeBehindCompiler.CompileAspxStaticMethodSetPageLoadByFullPath();
                    Arguments = new object[] { path, null, "" };
                }
                else
                {
                    method = CodeBehindCompiler.CompileAspxStaticMethodSetPageLoadByPath();
                    Arguments = new object[] { path, null };
                }

                string ReturnResult = await (Task<string>)method.Invoke(obj, Arguments);

                method = CodeBehindCompiler.CompileAspxStaticMethodPageHasFound();
                FoundPage = (bool)method.Invoke(obj, null);

                method = CodeBehindCompiler.CompileAspxStaticMethodGetWebSocketId();
                WebSocketId = (string)method.Invoke(obj, null);

                method = CodeBehindCompiler.CompileAspxStaticMethodGetSSEId();
                SSEId = (string)method.Invoke(obj, null);

                method = CodeBehindCompiler.CompileAspxStaticMethodGetUseSSE();
                UseSSE = (bool)method.Invoke(obj, null);

                return ReturnResult;
            }

            FoundPage = false;

            return "";
        }

        private string RunByPath(string path, string MethodName)
        {
            return RunByPathAsync(path, MethodName).GetAwaiter().GetResult();
        }

        // Overload
        /// <summary>
        /// It Works Based On Rewriting The Option File
        /// This Overload Method Does Not Support HttpContext And Sends null Value Instead Of HttpContext. This Overload Method Does Not Support Query String
        /// </summary>
        public string Run(string path)
        {
            return RunByPath(path, "SetPageLoadByPath");
        }

        // Overload
        /// <summary>
        /// Load All Page By Full Path, This Method Load Break Page And Does Not Apply Rewrite
        /// This Overload Method Does Not Support HttpContext And Sends null Value Instead Of HttpContext. This Overload Method Does Not Support Query String
        /// </summary>
        public string RunFullPath(string path)
        {
            return RunByPath(path, "SetPageLoadByFullPath");
        }

        public async Task<string> RunAsync(string path)
        {
            return await RunByPathAsync(path, "SetPageLoadByPath");
        }

        public async Task<string> RunFullPathAsync(string path)
        {
            return await RunByPathAsync(path, "SetPageLoadByFullPath");
        }

        public string RunErrorPage(int ErrorValue)
        {
            string path = StaticObject.ErrorPagePathBeforeValue + ErrorValue + StaticObject.ErrorPagePathAfterValue;

            return Run(path);
        }

        public string RunErrorPage(int ErrorValue, HttpContext context)
        {
            string path = StaticObject.ErrorPagePathBeforeValue + ErrorValue + StaticObject.ErrorPagePathAfterValue;

            context.Response.StatusCode = ErrorValue;

            return Run(context, path);
        }

        internal async Task<string> RunControllerValueAsync(HttpContext context, string ViewPath, object CodeBehindModel, NameValueCollection ViewData, string DownloadFilePath, bool? IgnoreLayout, string WebFormsValue, string? WebSocketId, string? SSEId, bool? UseSSE)
        {
            if (string.IsNullOrEmpty(ViewPath) && string.IsNullOrEmpty(DownloadFilePath))
            {
                FoundPage = false;
                return "";
            }

            string path = context.Request.Path.ToString();
            path = System.Net.WebUtility.UrlDecode(path);

            if (StaticObject.PreventAccessDefaultAspx)
                if (path.EndsWith("/Default.aspx") || path.Contains("/Default.aspx/"))
                {
                    FoundPage = false;
                    return "";
                }

            if (context.Request.ContentType == null)
                context.Request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

            Type type = CodeBehindCompiler.CompileAspxAndReturnType();
            object obj = Activator.CreateInstance(type);
            MethodInfo method = CodeBehindCompiler.CompileAspxStaticMethodRunControllerName();
            object[] Arguments = new object[] { context, ViewPath, CodeBehindModel, ViewData, DownloadFilePath, IgnoreLayout, WebFormsValue, WebSocketId, SSEId, UseSSE };
            string ReturnResult = await (Task<string>)method.Invoke(obj, Arguments);

            method = CodeBehindCompiler.CompileAspxStaticMethodPageHasFound();
            FoundPage = (bool)method.Invoke(obj, null);

            method = CodeBehindCompiler.CompileAspxStaticMethodGetWebSocketId();
            WebSocketId = (string)method.Invoke(obj, null);

            method = CodeBehindCompiler.CompileAspxStaticMethodGetSSEId();
            SSEId = (string)method.Invoke(obj, null);

            method = CodeBehindCompiler.CompileAspxStaticMethodGetUseSSE();
            UseSSE = (bool)method.Invoke(obj, null);

            // Set Web-Forms Control
            method = CodeBehindCompiler.CompileAspxStaticMethodGetWebFormsValue();
            string TmpWebFormsValue = (string)method.Invoke(obj, null);

            if (!string.IsNullOrEmpty(TmpWebFormsValue))
            {
                bool HasPostBack = false;

                if (context.Request.Headers.TryGetValue("Post-Back", out var value))
                {
                    if (value == "true")
                    {
                        HasPostBack = true;
                        context.Response.Headers.Add("Content-Type", "text/plain");
                    }
                }
                else if (context.Request.Headers.TryGetValue("Upgrade", out var value2))
                {
                    if (value2 == "websocket")
                        HasPostBack = true;
                }

                if (HasPostBack)
                {
                    ReturnResult = SetWebFormsCombinate(ReturnResult, TmpWebFormsValue);
                    context.Response.Headers.Add("Content-Type", "text/plain");
                }
                else
                    ReturnResult = SetWebFormsCombinateFirstResponse(ReturnResult, TmpWebFormsValue);
            }

            return ReturnResult;
        }

        internal string RunControllerValue(HttpContext context, string ViewPath, object CodeBehindModel, NameValueCollection ViewData, string DownloadFilePath, bool? IgnoreLayout, string WebFormsValue, string? WebSocketId, string? SSEId, bool? UseSSE)
        {
            return RunControllerValueAsync(context, ViewPath, CodeBehindModel, ViewData, DownloadFilePath, IgnoreLayout, WebFormsValue, WebSocketId, SSEId, UseSSE).GetAwaiter().GetResult();
        }

        public string RunController(object ControllerClass, HttpContext context)
        {
            Type type = ControllerClass.GetType();
            MethodInfo method = type.GetMethod("FillSegment");
            method.Invoke(ControllerClass, new object[] { context });
            method = type.GetMethod("PageLoad");
            method.Invoke(ControllerClass, new object[] { context });
            method = type.GetMethod("Run");
            string ReturnResult = (string)method.Invoke(ControllerClass, new object[] { context });

            return ReturnResult;
        }

        // Overload
        public string RunController(object ControllerClass)
        {
            Type type = ControllerClass.GetType();
            MethodInfo method = type.GetMethod("FillSegment");
            method.Invoke(ControllerClass, new object[] { null });
            method = type.GetMethod("PageLoad");
            method.Invoke(ControllerClass, new object[] { null });
            method = type.GetMethod("Run");
            string ReturnResult = (string)method.Invoke(ControllerClass, new object[] { null });

            return ReturnResult;
        }

        public async Task<string> RunControllerAsync(object ControllerClass, HttpContext context)
        {
            Type type = ControllerClass.GetType();
            MethodInfo method = type.GetMethod("FillSegment");
            method.Invoke(ControllerClass, new object[] { context });
            method = type.GetMethod("PageLoad");
            await (Task)method.Invoke(ControllerClass, new object[] { context });
            method = type.GetMethod("RunAsync");
            string ReturnResult = await (Task<string>)method.Invoke(ControllerClass, new object[] { context });

            return ReturnResult;
        }

        // Overload
        public async Task<string> RunControllerAsync(object ControllerClass)
        {
            Type type = ControllerClass.GetType();
            MethodInfo method = type.GetMethod("FillSegment");
            method.Invoke(ControllerClass, new object[] { null });
            method = type.GetMethod("PageLoad");
            await (Task)method.Invoke(ControllerClass, new object[] { null });
            method = type.GetMethod("RunAsync");
            string ReturnResult = await (Task<string>)method.Invoke(ControllerClass, new object[] { null });

            return ReturnResult;
        }

        // Overload
        public async Task<string> RunControllerAsync(string ControllerClass, HttpContext context, bool IsDefaultController = false)
        {
            Type type = CodeBehindCompiler.CompileAspxAndReturnType();
            object obj = Activator.CreateInstance(type);
            MethodInfo method = CodeBehindCompiler.CompileAspxStaticMethodRunControllerName();
            string ReturnResult = await (Task<string>)method.Invoke(obj, new object[] { ControllerClass, context, IsDefaultController, false });

            method = CodeBehindCompiler.CompileAspxStaticMethodControllerHasFound();
            FoundController = (bool)method.Invoke(obj, null);

            method = CodeBehindCompiler.CompileAspxStaticMethodGetWebSocketId();
            WebSocketId = (string)method.Invoke(obj, null);

            method = CodeBehindCompiler.CompileAspxStaticMethodGetSSEId();
            SSEId = (string)method.Invoke(obj, null);

            method = CodeBehindCompiler.CompileAspxStaticMethodGetUseSSE();
            UseSSE = (bool)method.Invoke(obj, null);

            // Set Web-Forms Control
            method = CodeBehindCompiler.CompileAspxStaticMethodGetWebFormsValue();
            string TmpWebFormsValue = (string)method.Invoke(obj, null);

            if (!string.IsNullOrEmpty(TmpWebFormsValue))
            {
                bool HasPostBack = false;

                if (context.Request.Headers.TryGetValue("Post-Back", out var value))
                    if (value == "true")
                    {
                        HasPostBack = true;
                        context.Response.Headers.Add("Content-Type", "text/plain");
                    }
                    else if (context.Request.Headers.TryGetValue("Upgrade", out var value2))
                        HasPostBack = (value2 == "websocket");

                if (HasPostBack)
                {
                    ReturnResult = SetWebFormsCombinate(ReturnResult, TmpWebFormsValue);
                    context.Response.Headers.Add("Content-Type", "text/plain");
                }
                else
                    ReturnResult = SetWebFormsCombinateFirstResponse(ReturnResult, TmpWebFormsValue);
            }

            return ReturnResult;
        }

        public string RunController(string ControllerClass, HttpContext context, bool IsDefaultController = false)
        {
            return RunControllerAsync(ControllerClass, context, IsDefaultController).GetAwaiter().GetResult();
        }

        // Overload
        public string RunController(string ControllerClass)
        {
            return RunController(ControllerClass, null);
        }

        public async Task<string> RunControllerAsync(string ControllerClass)
        {
            return await RunControllerAsync(ControllerClass, null);
        }

        public string RunRoute(HttpContext context, int ControllerSegment)
        {
            if (context == null)
            {
                FoundController = false;
                return "";
            }

            string RequestPath = context.Request.Path;

            if (RequestPath.Length > 0)
                if (RequestPath[0] == '/')
                    RequestPath = RequestPath.Remove(0, 1);

            ValueCollectionLock Segment = new ValueCollectionLock();

            string[] ValueList = RequestPath.Split('/');
            Segment.AddList(ValueList);

            if (Segment.Count() <= ControllerSegment)
            {
                FoundController = false;
                return "";
            }

            string ControllerClass = Segment.GetValue(ControllerSegment);

            if (string.IsNullOrEmpty(ControllerClass))
            {
                if (StaticObject.UseDefaultController)
                    return RunController(StaticObject.DefaultController, context, true);

                FoundController = false;
                return "";
            }

            return RunController(ControllerClass, context);
        }

        public async Task<string> RunRouteAsync(HttpContext context, int ControllerSegment)
        {
            if (context == null)
            {
                FoundController = false;
                return "";
            }

            string RequestPath = context.Request.Path;

            if (RequestPath.Length > 0)
                if (RequestPath[0] == '/')
                    RequestPath = RequestPath.Remove(0, 1);

            ValueCollectionLock Segment = new ValueCollectionLock();

            string[] ValueList = RequestPath.Split('/');
            Segment.AddList(ValueList);

            if (Segment.Count() <= ControllerSegment)
            {
                FoundController = false;
                return "";
            }

            string ControllerClass = Segment.GetValue(ControllerSegment);

            if (string.IsNullOrEmpty(ControllerClass))
            {
                if (StaticObject.UseDefaultController)
                    return await RunControllerAsync(StaticObject.DefaultController, context, true);

                FoundController = false;
                return "";
            }

            return await RunControllerAsync(ControllerClass, context);
        }

        private string SetWebFormsCombinate(string ResponseText, string WebFormsValue)
        {
            if (!string.IsNullOrEmpty(ResponseText))
            {
                if (StaticObject.UseCommentModeForWebFormsCombinate)
                    return ResponseText + WebFormsValue.ExportActionControlsToHtmlComment(true);

                return "[web-forms]" + "\nst" + StaticObject.ViewPlace + "=" + ResponseText.Replace('\n'.ToString(), "$[ln];") + '\n' + WebFormsValue;
            }
            else
                return WebFormsValue.ExportActionControlsToResponse();
        }

        private string SetWebFormsCombinateFirstResponse(string ResponseText, string WebFormsValue)
        {
            if (string.IsNullOrEmpty(ResponseText))
                return "";

            return ResponseText + WebFormsValue.ExportActionControlsToHtmlComment(true);
        }
    }
}
