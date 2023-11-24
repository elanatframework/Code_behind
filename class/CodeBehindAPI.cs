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
            string SharedPath = RunTimePath.GetTextBeforeLastValue("/").GetTextBeforeLastValue("/");
            string Version = RunTimePath.GetTextAfterLastValue("/");
            AspRunTimePath = SharedPath + "/Microsoft.AspNetCore.App/" + Version; 
        }
    }
}
