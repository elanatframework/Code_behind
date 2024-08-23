using System.Reflection;

namespace CodeBehind.API
{
    public class Path
    {
        public string RunTimePath { get; private set; }
        public string AspRunTimePath { get; private set; }
        public string BaseDirectory { get; private set; }

        public Path()
        {
            BaseDirectory = AppContext.BaseDirectory;
            BaseDirectory = BaseDirectory.Replace("\\", "/");

            RunTimePath = System.IO.Path.GetDirectoryName(typeof(object).GetTypeInfo().Assembly.Location);
            RunTimePath = RunTimePath.Replace("\\", "/");

            var assembly = typeof(Microsoft.AspNetCore.Http.HttpContext).Assembly;
            var codeBase = assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var AspNetCorePath = System.IO.Path.GetDirectoryName(path);

            AspRunTimePath = AspNetCorePath.Replace("\\", "/"); 
        }
    }
}
