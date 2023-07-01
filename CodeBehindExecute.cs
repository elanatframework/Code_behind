﻿using Microsoft.AspNetCore.Http;
using SetCodeBehind;
using System.Reflection;

namespace CodeBehind
{
    public class CodeBehindExecute
    {
        public string Run(HttpContext context)
        {
            string path = context.Request.Path.ToString();
            string extension = Path.GetExtension(path);

            if (extension == ".aspx")
            {
                path = System.Net.WebUtility.UrlDecode(path);

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


                Assembly assembly = SetCodeBehind.CodeBehindCompiler.CompileAspx();
                Type type = assembly.GetType("CodeBehindViews.CodeBehindViewsList");
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod("SetPageLoadByPath");
                string ReturnResult = (string)method.Invoke(obj, new object[] { path, context });

                return ReturnResult;
            }

            return "";
        }
    }
}