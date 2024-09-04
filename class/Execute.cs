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

        private string RunByContext(HttpContext context, string MethodName, string QueryString = "")
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

            bool HasSection = path.Contains(".aspx/");

            if (string.IsNullOrEmpty(extension) && !HasSection)
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

            if (extension == ".aspx" || HasSection)
            {
                // Add QueryString Value
                if (!string.IsNullOrEmpty(QueryString))
                {
                    NameCollection QueryValues = new NameCollection();
                    QueryString TmpQueryString = new QueryString();
                    string[] QueryElements = QueryString.Split('&');
                    foreach (string element in QueryElements)
                    {
                        string[] NameValue = element.Split('=');

                        if (NameValue.Length > 1)
                            TmpQueryString = TmpQueryString.Add(NameValue[0], NameValue[1]);
                        else
                            TmpQueryString = TmpQueryString.Add(NameValue[0], "");

                        QueryValues.Add(NameValue[0]);
                    }

                    string RequestQueryString = context.Request.QueryString.Value;

                    if (!string.IsNullOrEmpty(RequestQueryString))
                    {
                        RequestQueryString = RequestQueryString.GetTextAfterValue("?");
                        string[] TmpQueryElements = RequestQueryString.Split('&');
                        foreach (string element in TmpQueryElements)
                        {
                            string[] NameValue = element.Split('=');

                            if (!QueryValues.Exist(NameValue[0]))
                                if (NameValue.Length > 1)
                                    TmpQueryString = TmpQueryString.Add(NameValue[0], NameValue[1]);
                                else
                                    TmpQueryString = TmpQueryString.Add(NameValue[0], "");
                        }
                    }

                    context.Request.QueryString = TmpQueryString;
                }

                if (context.Request.ContentType == null)
                    context.Request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

                Type type = CodeBehindCompiler.CompileAspxAndReturnType();
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(MethodName);
                object[] Arguments = (MethodName == "SetPageLoadByFullPath") ? new object[] { path, context, "" } : new object[] { path, context };
                string ReturnResult = (string)method.Invoke(obj, Arguments);

                method = type.GetMethod("PageHasFound");
                FoundPage = (bool)method.Invoke(obj, null);

                // Set Web-Forms Control
                method = type.GetMethod("GetWebFormsValue");
                string WebFormsValue = (string)method.Invoke(obj, null);

                if (!string.IsNullOrEmpty(WebFormsValue))
                {
                    bool HasPostBack = false;

                    if (context.Request.Headers.TryGetValue("Post-Back", out var value))
                        if (value == "true")
                            HasPostBack = true;

                    if (HasPostBack)
                    {
                        ReturnResult = SetWebFormsCombinate(ReturnResult, WebFormsValue);
                        context.Response.Headers.Add("Content-Type", "text/plain");
                    }
                    else
                        ReturnResult = SetWebFormsCombinateFirstResponse(ReturnResult, WebFormsValue);
                }

                return ReturnResult;
            }

            FoundPage = false;

            return "";
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

        // Overload
        private string PrivateRun(HttpContext context, string Path, string MethodName)
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

            string ReturnValue = RunByContext(context, MethodName, QueryString);

            context.Request.Path = SavedPath;

            return ReturnValue;
        }

        // Overload
        /// <summary>
        /// It Works Based On Rewriting The Option File
        /// </summary>
        public string Run(HttpContext context, string Path)
        {
            return PrivateRun(context, Path, "SetPageLoadByPath");
        }

        // Overload
        /// <summary>
        /// Load All Page By Full Path, This Method Load Break Page And Does Not Apply Rewrite
        /// </summary>
        public string RunFullPath(HttpContext context, string Path)
        {
            return PrivateRun(context, Path, "SetPageLoadByFullPath");
        }

        // Overload
        /// <summary>
        /// This Overload Method Does Not Support HttpContext And Sends null Value Instead Of HttpContext.
        /// This Overload Method Does Not Support Query String
        /// This Overload Method Does Not Support Web-Forms Control
        /// </summary>
        private string RunByPath(string path, string MethodName)
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

            bool HasSection = path.Contains(".aspx/");

            if (string.IsNullOrEmpty(extension) && !HasSection)
            {
                bool AddSlash = true;

                if (path.Length > 0)
                    AddSlash = (path[path.Length - 1] != '/');

                path = path + (AddSlash ? "/" : "") + "Default.aspx";

                extension = ".aspx";
            }

            if (extension == ".aspx" || HasSection)
            {
                Type type = CodeBehindCompiler.CompileAspxAndReturnType();
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(MethodName);
                object[] Arguments = (MethodName == "SetPageLoadByFullPath") ? new object[] { path, null, "" } : new object[] { path, null };
                string ReturnResult = (string)method.Invoke(obj, Arguments);

                method = type.GetMethod("PageHasFound");
                FoundPage = (bool)method.Invoke(obj, null);

                return ReturnResult;
            }

            FoundPage = false;

            return "";
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

        public string RunErrorPage(int ErrorValue)
        {
            CodeBehindOptions option = new CodeBehindOptions();
            string path = option.ErrorPagePath.Replace("{value}", ErrorValue.ToString());

            return Run(path);
        }

        public string RunErrorPage(int ErrorValue, HttpContext context)
        {
            CodeBehindOptions option = new CodeBehindOptions();
            string path = option.ErrorPagePath.Replace("{value}", ErrorValue.ToString());

            context.Response.StatusCode = ErrorValue;

            return Run(context, path);
        }

        internal string RunControllerValue(HttpContext context, string ViewPath, object CodeBehindModel, NameValueCollection ViewData, string DownloadFilePath, bool IgnoreLayout, string WebFormsValue)
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
            MethodInfo method = type.GetMethod("RunController");
            object[] Arguments = new object[] { context, ViewPath, CodeBehindModel, ViewData, DownloadFilePath, IgnoreLayout, WebFormsValue };
            string ReturnResult = (string)method.Invoke(obj, Arguments);

            method = type.GetMethod("PageHasFound");
            FoundPage = (bool)method.Invoke(obj, null);

            // Set Web-Forms Control
            method = type.GetMethod("GetWebFormsValue");
            string TmpWebFormsValue = (string)method.Invoke(obj, null);

            if (!string.IsNullOrEmpty(TmpWebFormsValue))
            {
                bool HasPostBack = false;

                if (context.Request.Headers.TryGetValue("Post-Back", out var value))
                    if (value == "true")
                        HasPostBack = true;

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

        public string RunController(object ControllerClass, HttpContext context)
        {
            Type type = ControllerClass.GetType();
            MethodInfo method = type.GetMethod("FillSection");
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
            MethodInfo method = type.GetMethod("FillSection");
            method.Invoke(ControllerClass, new object[] { null });
            method = type.GetMethod("PageLoad");
            method.Invoke(ControllerClass, new object[] { null });
            method = type.GetMethod("Run");
            string ReturnResult = (string)method.Invoke(ControllerClass, new object[] { null });

            return ReturnResult;
        }

        // Overload
        public string RunController(string ControllerClass, HttpContext context, bool IsDefaultController = false)
        {
            Type type = CodeBehindCompiler.CompileAspxAndReturnType();
            object obj = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("RunControllerName");
            string ReturnResult = (string)method.Invoke(obj, new object[] { ControllerClass, context, IsDefaultController, false });

            method = type.GetMethod("ControllerHasFound");
            FoundController = (bool)method.Invoke(obj, null);

            // Set Web-Forms Control
            method = type.GetMethod("GetWebFormsValue");
            string TmpWebFormsValue = (string)method.Invoke(obj, null);

            if (!string.IsNullOrEmpty(TmpWebFormsValue))
            {
                bool HasPostBack = false;

                if (context.Request.Headers.TryGetValue("Post-Back", out var value))
                    if (value == "true")
                        HasPostBack = true;

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

        // Overload
        public string RunController(string ControllerClass)
        {
            return RunController(ControllerClass, null);
        }

        public string RunRoute(HttpContext context, int ControllerSection)
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

            ValueCollectionLock Section = new ValueCollectionLock();

            string[] ValueList = RequestPath.Split('/');
            Section.AddList(ValueList);

            if (Section.Count() <= ControllerSection)
            {
                FoundController = false;
                return "";
            }

            if (string.IsNullOrEmpty(Section.GetValue(ControllerSection)))
            {
                if (StaticObject.UseDefaultController)
                    return RunController(StaticObject.DefaultController, context, true);
                else
                {
                    FoundController = false;
                    return "";
                }
            }

            string ControllerClass = Section.GetValue(ControllerSection);

            return RunController(ControllerClass, context);
        }

        private string SetWebFormsCombinate(string ResponseText, string WebFormsValue) => "[web-forms]" + (!string.IsNullOrEmpty(ResponseText) ? Environment.NewLine + "st" + StaticObject.ViewPlace + "=" + ResponseText.Replace('\n'.ToString(), "$[ln];") : "") + WebFormsValue;

        private string SetWebFormsCombinateFirstResponse(string ResponseText, string WebFormsValue)
        {
            if (string.IsNullOrEmpty(ResponseText))
                return "";

            if (!string.IsNullOrEmpty(WebFormsValue))
                if (WebFormsValue.StartsWith(Environment.NewLine))
                    WebFormsValue = WebFormsValue.Remove(0, Environment.NewLine.Length);

            return ResponseText + WebFormsValue.Replace(Environment.NewLine, "$[sln];").ExportActionControlsToWebFormsTag();
        }
    }
}
