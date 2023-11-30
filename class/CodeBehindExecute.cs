using Microsoft.AspNetCore.Http;
using SetCodeBehind;
using System.Reflection;

namespace CodeBehind
{
    public class CodeBehindExecute
    {
        private string PrivateRun(HttpContext context, string MethodName)
        {
            string path = context.Request.Path.ToString();
            string extension = Path.GetExtension(path);
            path = System.Net.WebUtility.UrlDecode(path);

            if (string.IsNullOrEmpty(extension))
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

            if (extension == ".aspx")
            {
                // Add QueryString Value
                if (path.Contains('?'))
                {
                    string QueryString = path.GetTextAfterValue("?");

                    string[] queryElements = QueryString.Split('&');
                    foreach (string element in queryElements)
                    {
                        string[] NameValue = element.Split('=');

                        if (NameValue.Length > 1)
                            context.Request.QueryString.Add(NameValue[0], NameValue[1]);
                        else
                            context.Request.QueryString.Add(NameValue[0], "");
                    }
                }

                if (context.Request.ContentType == null)
                    context.Request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

                Assembly assembly = CodeBehindCompiler.CompileAspx();
                Type type = assembly.GetType("CodeBehindViews.CodeBehindViewsList");
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(MethodName);
                string ReturnResult = (string)method.Invoke(obj, new object[] { path, context });

                return ReturnResult;
            }

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

            if (string.IsNullOrEmpty(extension))
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

            if (extension == ".aspx")
            {
                Assembly assembly = CodeBehindCompiler.CompileAspx();
                Type type = assembly.GetType("CodeBehindViews.CodeBehindViewsList");
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(MethodName);
                string ReturnResult = (string)method.Invoke(obj, new object[] { path, null });

                return ReturnResult;
            }

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
    }
}
