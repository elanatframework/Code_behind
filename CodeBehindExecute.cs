using Microsoft.AspNetCore.Http;
using SetCodeBehind;
using System.IO;
using System.Reflection;

namespace CodeBehind
{
    public class CodeBehindExecute
    {
        public string Run(HttpContext context)
        {
            string path = context.Request.Path.ToString();
            string extension = Path.GetExtension(path);
            path = System.Net.WebUtility.UrlDecode(path);

            if (string.IsNullOrEmpty(extension))
            {
                if (path.Contains('?'))
                    path = path.GetTextBeforeLastValue("/") + "/Default.aspx?" + path.GetTextAfterValue("?");
                else
                    path = path.GetTextBeforeLastValue("/") + "/Default.aspx";

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


                Assembly assembly = CodeBehindCompiler.CompileAspx();
                Type type = assembly.GetType("CodeBehindViews.CodeBehindViewsList");
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod("SetPageLoadByPath");
                string ReturnResult = (string)method.Invoke(obj, new object[] { path, context });

                return ReturnResult;
            }

            return "";
        }

        // Overload
        public string Run(HttpContext context, string Path)
        {
            string SavedPath = context.Request.Path;

            context.Request.Path = Path;

            string ReturnValue = Run(context);

            context.Request.Path = SavedPath;

            return ReturnValue;
        }

        // Overload
        /// <summary>
        /// This Overload Method Does Not Support HttpContext And Sends null Value Instead Of HttpContext. This Overload Method Does Not Support Query String
        /// </summary>
        public string Run(string path)
        {
            string extension = Path.GetExtension(path);
            path = System.Net.WebUtility.UrlDecode(path);

            if (string.IsNullOrEmpty(extension))
            {
                path = path.GetTextBeforeLastValue("/") + "/Default.aspx";

                extension = ".aspx";
            }

            if (extension == ".aspx")
            {
                Assembly assembly = CodeBehindCompiler.CompileAspx();
                Type type = assembly.GetType("CodeBehindViews.CodeBehindViewsList");
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod("SetPageLoadByPath");
                string ReturnResult = (string)method.Invoke(obj, new object[] { path, null });

                return ReturnResult;
            }

            return "";
        }
    }
}
