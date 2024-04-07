using CodeBehind.HtmlData;
using Microsoft.AspNetCore.Http;
using SetCodeBehind;
using System.Reflection;

namespace CodeBehind
{
    public class CodeBehindExecute
    {
        public bool FoundPage { get; set; } = true;
        public bool FoundController { get; set; } = true;

        private string PrivateRun(HttpContext context, string MethodName)
        {
            string path = context.Request.Path.ToString();
            path = System.Net.WebUtility.UrlDecode(path);
            string extension = Path.GetExtension(path.GetTextBeforeValue("?"));

            if (StaticObject.PreventAccessDefaultAaspx && MethodName == "SetPageLoadByPath")
                if (path.GetTextBeforeValue("?").EndsWith("/Default.aspx") || path.GetTextBeforeValue("?").Contains("/Default.aspx/"))
                {
                    FoundPage = false;
                    return "";
                }

            bool HasSection = path.GetTextBeforeValue("?").Contains(".aspx/");

            if (string.IsNullOrEmpty(extension) && !HasSection)
            {
                bool AddSlash = true;

                if (path.Length > 0)
                    AddSlash = (path[path.Length - 1] != '/');

                if (path.Contains('?'))
                    path = path.GetTextBeforeValue("?") + (AddSlash ? "/" : "") + "Default.aspx?" + path.GetTextAfterValue("?");
                else
                    path = path + (AddSlash ? "/" : "") + "Default.aspx";

                extension = ".aspx";
            }

            if (extension == ".aspx" || HasSection)
            {
                // Add QueryString Value
                if (path.Contains('?'))
                {
                    string QueryString = path.GetTextAfterValue("?");

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
                        path = path.GetTextBeforeValue("?");
                    }
                }

                if (context.Request.ContentType == null)
                    context.Request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

                Assembly assembly = CodeBehindCompiler.CompileAspx();
                Type type = assembly.GetType("CodeBehindViews.CodeBehindViewsList");
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(MethodName);
                object[] Arguments = (MethodName == "SetPageLoadByFullPath") ? new object[] { path, context, "" } : new object[] { path, context };
                string ReturnResult = (string)method.Invoke(obj, Arguments);

                method = type.GetMethod("PageHasFound");
                FoundPage = (bool)method.Invoke(obj, null);

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
            return PrivateRun(context, "SetPageLoadByPath");
        }

        /// <summary>
        /// Load All Page By Full Path, This Method Load Break Page And Does Not Apply Rewrite
        /// </summary>
        public string RunFullPath(HttpContext context)
        {
            return PrivateRun(context, "SetPageLoadByFullPath");
        }

        // Overload
        private string PrivateRun(HttpContext context, string Path, string MethodName)
        {
            string SavedPath = context.Request.Path;

            context.Request.Path = Path;

            string ReturnValue = PrivateRun(context, MethodName);

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
        /// This Overload Method Does Not Support HttpContext And Sends null Value Instead Of HttpContext. This Overload Method Does Not Support Query String
        /// </summary>
        private string PrivateRun(string path, string MethodName)
        {
            string extension = Path.GetExtension(path);
            path = System.Net.WebUtility.UrlDecode(path);

            if (StaticObject.PreventAccessDefaultAaspx && MethodName == "SetPageLoadByPath")
                if (path.GetTextBeforeValue("?").EndsWith("/Default.aspx") || path.GetTextBeforeValue("?").Contains("/Default.aspx/"))
                {
                    FoundPage = false;
                    return "";
                }

            bool HasSection = path.GetTextBeforeValue("?").Contains(".aspx/");

            if (string.IsNullOrEmpty(extension) && !HasSection)
            {
                bool AddSlash = true;

                if (path.Length > 0)
                    AddSlash = (path[path.Length - 1] != '/');

                if (path.Contains('?'))
                    path = path.GetTextBeforeValue("?") + (AddSlash ? "/" : "") + "Default.aspx?" + path.GetTextAfterValue("?");
                else
                    path = path + (AddSlash ? "/" : "") + "Default.aspx";

                extension = ".aspx";
            }

            if (extension == ".aspx" || HasSection)
            {
                Assembly assembly = CodeBehindCompiler.CompileAspx();
                Type type = assembly.GetType("CodeBehindViews.CodeBehindViewsList");
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
            return PrivateRun(path, "SetPageLoadByPath");
        }

        // Overload
        /// <summary>
        /// Load All Page By Full Path, This Method Load Break Page And Does Not Apply Rewrite
        /// This Overload Method Does Not Support HttpContext And Sends null Value Instead Of HttpContext. This Overload Method Does Not Support Query String
        /// </summary>
        public string RunFullPath(string path)
        {
            return PrivateRun(path, "SetPageLoadByFullPath");
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

            return Run(context, path);
        }

        internal string RunControllerValue(HttpContext context, string ViewPath, object CodeBehindModel, NameValueCollection ViewData, string DownloadFilePath)
        {
            if (string.IsNullOrEmpty(ViewPath) && string.IsNullOrEmpty(DownloadFilePath))
            {
                FoundPage = false;
                return "";
            }

            string path = context.Request.Path.ToString();
            path = System.Net.WebUtility.UrlDecode(path);

            if (StaticObject.PreventAccessDefaultAaspx)
                if (path.GetTextBeforeValue("?").EndsWith("/Default.aspx") || path.GetTextBeforeValue("?").Contains("/Default.aspx/"))
                {
                    FoundPage = false;
                    return "";
                }

            if (context.Request.ContentType == null)
                context.Request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

            Assembly assembly = CodeBehindCompiler.CompileAspx();
            Type type = assembly.GetType("CodeBehindViews.CodeBehindViewsList");
            object obj = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("RunController");
            object[] Arguments = new object[] { context, ViewPath, CodeBehindModel, ViewData, DownloadFilePath };
            string ReturnResult = (string)method.Invoke(obj, Arguments);

            method = type.GetMethod("PageHasFound");
            FoundPage = (bool)method.Invoke(obj, null);

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
        public string RunController(string ControllerClass, HttpContext context)
        {
            Assembly assembly = CodeBehindCompiler.CompileAspx();
            Type type = assembly.GetType("CodeBehindViews.CodeBehindViewsList");
            object obj = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("RunControllerName");
            string ReturnResult = (string)method.Invoke(obj, new object[] { ControllerClass, context });

            method = type.GetMethod("ControllerHasFound");
            FoundController = (bool)method.Invoke(obj, null);

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

            string[] ValueList = RequestPath.GetTextBeforeValue("?").Split('/');
            Section.AddList(ValueList);

            if (Section.Count() <= ControllerSection)
            {
                FoundController = false;
                return "";
            }

            if (string.IsNullOrEmpty(Section.GetValue(ControllerSection)))
            {
                FoundController = false;
                return "";
            }

            string ControllerClass = Section.GetValue(ControllerSection);

            if (!ControllerClass.ClassPathIsFine())
            {
                FoundController = false;
                return "";
            }

            return RunController(ControllerClass, context);
        }
    }
}
