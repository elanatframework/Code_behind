using CodeBehind;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace SetCodeBehind
{
    internal class CodeBehindLibraryCreator
    {
        private List<string> ErrorList = new List<string>();
        private string CaseCodeTemplateValue = "";
        private string SegmentTemplateValue = "";
        private string CaseCodeTemplateValueForFullPath = "";
        private string CaseCodeTemplateValueForFullPathWithModel = "";
        private string MethodCodeTemplateValue = "";

        internal string GetCodeBehindViews()
        {
            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            string AllAspxFiles = "";

            string FilePath = "code_behind/views_class.cs.tmp";
            if (!File.Exists(FilePath))
            {
                AllAspxFiles = CreateAllAspxFiles();

                string[] lines = AllAspxFiles.Split(Environment.NewLine);

                // Create views_class.cs File
                var file = File.CreateText(FilePath);

                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }

                file.Dispose();
                file.Close();
            }
            else
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        AllAspxFiles += line + Environment.NewLine;
                    }
                }
            }

            return AllAspxFiles;
        }

        internal string GetLastSuccessCompiledViewClass()
        {
            string AllAspxFiles = "";

            if (!Directory.Exists("code_behind"))
            {
                Directory.CreateDirectory("code_behind");
                return AllAspxFiles;
            }

            const string FilePath = "code_behind/views_class_last_success_compiled.cs.tmp";
            if (File.Exists(FilePath))
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        AllAspxFiles += line + Environment.NewLine;
                    }
                }
            }

            return AllAspxFiles;
        }

        private string CreateAllAspxFiles()
        {
            string AssemblyCleanName = SetCleanNameForClass(Assembly.GetEntryAssembly().GetName().Name);

            string CodeBehindViews = "";
            CodeBehindViews += "using " + AssemblyCleanName + ";" + Environment.NewLine;
            CodeBehindViews += "using CodeBehind;" + Environment.NewLine;
            CodeBehindViews += "using System;" + Environment.NewLine;
            CodeBehindViews += "using System.Runtime;" + Environment.NewLine;
            CodeBehindViews += "using System.Reflection;" + Environment.NewLine;
            CodeBehindViews += "using Microsoft.AspNetCore.Http;" + Environment.NewLine;
            CodeBehindViews += ImportNamespaceList();
            CodeBehindViews += Environment.NewLine;
            CodeBehindViews += "namespace CodeBehindViews" + Environment.NewLine;
            CodeBehindViews += "{" + Environment.NewLine;
            CodeBehindViews += "    public class CodeBehindViewsList" + Environment.NewLine;
            CodeBehindViews += "    {" + Environment.NewLine;
            CodeBehindViews += "        private CodeBehind.HtmlData.NameValueCollection ViewData = new CodeBehind.HtmlData.NameValueCollection();" + Environment.NewLine;
            CodeBehindViews += "        private string RequestPath { get; set; } = \"\";" + Environment.NewLine;
            CodeBehindViews += "        private string WebFormsValue { get; set; } = \"\";" + Environment.NewLine;
            CodeBehindViews += "        private string CallerViewPath { get; set; } = \"\";" + Environment.NewLine;
            CodeBehindViews += "        private string CallerViewDirectoryPath { get; set; } = \"\";" + Environment.NewLine;
            CodeBehindViews += "        private bool FoundPage { get; set; } = true;" + Environment.NewLine;
            CodeBehindViews += "        private bool FoundController { get; set; } = true;" + Environment.NewLine;
            CodeBehindViews += "        private bool? IgnoreLayout { get; set; } = false;" + Environment.NewLine;
            CodeBehindViews += "        private string WebSocketId { get; set; } = \"\";" + Environment.NewLine;
            CodeBehindViews += "        private string SSEId { get; set; } = \"\";" + Environment.NewLine;
            CodeBehindViews += "        private bool? UseSSE { get; set; } = false;" + Environment.NewLine;
            CodeBehindViews += "        private string ResponseText { get; set; } = \"\";" + Environment.NewLine + Environment.NewLine;

            CodeBehindOptions options = new CodeBehindOptions();

            // Create wwwroot Directory And Set Default Pages
            if (options.SetDefaultPages && options.ViewPath == "wwwroot" && !Directory.Exists("wwwroot"))
                new DefaultPages().Set();

            // Create Web-Forms Script
            if (!Directory.Exists("wwwroot"))
                Directory.CreateDirectory("wwwroot");

            if (options.AutoCreateWebFormsScript)
                new DefaultPages().SetWebFormsScript(options.WebFormsScriptPath, options.RecreateWebFormsScriptAfterRecompile);

            // Move View From wwwroot
            if ((options.ViewPath != "wwwroot") && options.MoveViewFromWwwroot)
            {
                MoveViewFromWwwroot(options.ViewPath, "aspx");
                MoveViewFromWwwroot(options.ViewPath, "astx");

                if (options.ConvertCsHtmlToAspx)
                    MoveViewFromWwwroot(options.ViewPath, "cshtml");
            }

            // Move Dll From wwwroot/bin
            if ((options.DllPath != "wwwroot/bin") && options.MoveDllFromWwwrootBin)
                MoveDllFromWwwrootBin(options.DllPath);

            string GlobalTemplate = GetGlobalTemplate();

            DirectoryInfo RootDir = new DirectoryInfo(options.ViewPath);
            string RootDirectoryPath = RootDir.FullName;
            object EmptyObjectForLock = new object();
            int i = 1;
            Parallel.ForEach(RootDir.GetFiles("*.aspx", SearchOption.AllDirectories), (file) =>
            {
                ViewCodeCombination combination = new ViewCodeCombination();
                combination.RewriteAspxFileToDirectory = options.RewriteAspxFileToDirectory;
                combination.AccessAspxFileAfterRewrite = options.AccessAspxFileAfterRewrite;
                combination.IgnoreDefaultAfterRewrite = options.IgnoreDefaultAfterRewrite;
                combination.StartTrimInAspxFile = options.StartTrimInAspxFile;
                combination.EndTrimInAspxFile = options.EndTrimInAspxFile;
                combination.SetBreakForLayoutPage = options.SetBreakForLayoutPage;
                combination.InnerTrimInAspxFile = options.InnerTrimInAspxFile;
                combination.GlobalTemplate = GlobalTemplate;

                combination.Set(file.FullName, RootDirectoryPath, i++);

                lock (EmptyObjectForLock)
                {
                    CaseCodeTemplateValue += combination.CaseCodeTemplateValue;
                    SegmentTemplateValue += combination.SegmentTemplateValue;
                    CaseCodeTemplateValueForFullPath += combination.CaseCodeTemplateValueForFullPath;
                    CaseCodeTemplateValueForFullPathWithModel += combination.CaseCodeTemplateValueForFullPathWithModel;
                    MethodCodeTemplateValue += combination.MethodCodeTemplateValue;
                    ErrorList = ErrorList.AddList(combination.ErrorList);
                }
            });

            if (options.ConvertCsHtmlToAspx)
                Parallel.ForEach(RootDir.GetFiles("*.cshtml", SearchOption.AllDirectories), (file) =>
                {
                    ViewCodeCombination combination = new ViewCodeCombination();
                    combination.RewriteAspxFileToDirectory = options.RewriteAspxFileToDirectory;
                    combination.AccessAspxFileAfterRewrite = options.AccessAspxFileAfterRewrite;
                    combination.IgnoreDefaultAfterRewrite = options.IgnoreDefaultAfterRewrite;
                    combination.StartTrimInAspxFile = options.StartTrimInAspxFile;
                    combination.EndTrimInAspxFile = options.EndTrimInAspxFile;
                    combination.SetBreakForLayoutPage = options.SetBreakForLayoutPage;
                    combination.InnerTrimInAspxFile = options.InnerTrimInAspxFile;
                    combination.GlobalTemplate = GlobalTemplate;

                    combination.Set(file.FullName, RootDirectoryPath, i++);

                    lock (EmptyObjectForLock)
                    {
                        CaseCodeTemplateValue += combination.CaseCodeTemplateValue;
                        SegmentTemplateValue += combination.SegmentTemplateValue;
                        CaseCodeTemplateValueForFullPath += combination.CaseCodeTemplateValueForFullPath;
                        CaseCodeTemplateValueForFullPathWithModel += combination.CaseCodeTemplateValueForFullPathWithModel;
                        MethodCodeTemplateValue += combination.MethodCodeTemplateValue;
                        ErrorList = ErrorList.AddList(combination.ErrorList);
                    }
                });

            CodeBehindViews += "        // It Works Based On Rewriting The Option File" + Environment.NewLine;
            CodeBehindViews += "        public async Task<string> SetPageLoadByPath(string path, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            if (options.IgnoreLayoutForPostBack)
                CodeBehindViews += "            try{IgnoreLayoutForPostBack(context.Request.Headers);} catch(NullReferenceException){}" + Environment.NewLine + Environment.NewLine;
            if (options.SetTextHtmlContentTypeForPostBack)
                CodeBehindViews += "            try{SetTextHtmlContentTypeForPostBack(context);} catch(NullReferenceException){}" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            RequestPath = path;" + Environment.NewLine;
            CodeBehindViews += "            FoundPage = true;" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += SegmentTemplateValue + "/*{SegmentTemplateValue}*/" + Environment.NewLine;
            CodeBehindViews += "            switch (path)" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += CaseCodeTemplateValue + Environment.NewLine + "/*{CaseCodeTemplateValue}*/" + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            FoundPage = false;" + Environment.NewLine;
            CodeBehindViews += "            return \"\";" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Load All Page By Full Path, This Method Load Break Page And Does Not Apply Rewrite" + Environment.NewLine;
            CodeBehindViews += "        public async Task<string> SetPageLoadByFullPath(string path, HttpContext context, string PageReturnValue = \"\")" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            if (options.IgnoreLayoutForPostBack)
                CodeBehindViews += "            try{IgnoreLayoutForPostBack(context.Request.Headers);} catch(NullReferenceException){}" + Environment.NewLine + Environment.NewLine;
            if (options.SetTextHtmlContentTypeForPostBack)
                CodeBehindViews += "            try{SetTextHtmlContentTypeForPostBack(context);} catch(NullReferenceException){}" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            RequestPath = path;" + Environment.NewLine;
            CodeBehindViews += "            FoundPage = true;" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            switch (path)" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += CaseCodeTemplateValueForFullPath + Environment.NewLine + "/*{CaseCodeTemplateValueForFullPath}*/" + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            FoundPage = false;" + Environment.NewLine;
            CodeBehindViews += "            return \"\";" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Load All Page By Full Path With Model, This Method Load Break Page And Does Not Apply Rewrite" + Environment.NewLine;
            CodeBehindViews += "        public async Task<string> SetPageLoadByFullPathWithModel(string path, HttpContext context, string PageReturnValue = \"\", object model = null)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            if (options.IgnoreLayoutForPostBack)
                CodeBehindViews += "            try{IgnoreLayoutForPostBack(context.Request.Headers);} catch(NullReferenceException){}" + Environment.NewLine + Environment.NewLine;
            if (options.SetTextHtmlContentTypeForPostBack)
                CodeBehindViews += "            try{SetTextHtmlContentTypeForPostBack(context);} catch(NullReferenceException){}" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            RequestPath = path;" + Environment.NewLine;
            CodeBehindViews += "            FoundPage = true;" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            switch (path)" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += CaseCodeTemplateValueForFullPathWithModel + Environment.NewLine + "/*{CaseCodeTemplateValueForFullPathWithModel}*/" + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            FoundPage = false;" + Environment.NewLine;
            CodeBehindViews += "            return \"\";" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            // Add Load Page Method
            CodeBehindViews += "        private async Task<string> LoadPageAsync(string path, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return await SetPageLoadByFullPath(path, context, \"\");" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private string LoadPage(string path, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return SetPageLoadByFullPath(path, context, \"\").GetAwaiter().GetResult();" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Overload" + Environment.NewLine;
            CodeBehindViews += "        private async Task<string> LoadPageAsync(string path, object ModelClass, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return await SetPageLoadByFullPathWithModel(path, context, \"\", ModelClass);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Overload" + Environment.NewLine;
            CodeBehindViews += "        private string LoadPage(string path, object ModelClass, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return SetPageLoadByFullPathWithModel(path, context, \"\", ModelClass).GetAwaiter().GetResult();" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Overload" + Environment.NewLine;
            CodeBehindViews += "        private async Task<string> LoadPageAsync(string path, object ModelClass)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return await SetPageLoadByFullPathWithModel(path, null, \"\", ModelClass);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Overload" + Environment.NewLine;
            CodeBehindViews += "        private string LoadPage(string path, object ModelClass)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return SetPageLoadByFullPathWithModel(path, null, \"\", ModelClass).GetAwaiter().GetResult();" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Overload" + Environment.NewLine;
            CodeBehindViews += "        private async Task<string> LoadPageAsync(string path)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return await SetPageLoadByFullPath(path, null, \"\");" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Overload" + Environment.NewLine;
            CodeBehindViews += "        private string LoadPage(string path)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return SetPageLoadByFullPath(path, null, \"\").GetAwaiter().GetResult();" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public async Task<string> RunController(HttpContext context, string ViewPath, object ModelClass, CodeBehind.HtmlData.NameValueCollection ViewData, string DownloadFilePath, bool? IgnoreLayout, string WebFormsValue, string? WebSocketId, string? SSEId, bool? UseSSE)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            if (!string.IsNullOrEmpty(DownloadFilePath))" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += "                await DownloadAsync(context, DownloadFilePath);" + Environment.NewLine;
            CodeBehindViews += "                return \"\";" + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            ViewData.AddList(ViewData.GetList());" + Environment.NewLine;
            CodeBehindViews += "            if (IgnoreLayout != null)" + Environment.NewLine;
            CodeBehindViews += "                this.IgnoreLayout = IgnoreLayout;" + Environment.NewLine;
            CodeBehindViews += "            this.WebFormsValue += WebFormsValue;" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            if (string.IsNullOrEmpty(ViewPath))" + Environment.NewLine;
            CodeBehindViews += "                return \"\";" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "            if (WebSocketId != null)" + Environment.NewLine;
            CodeBehindViews += "                this.WebSocketId = WebSocketId;" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "            if (SSEId != null)" + Environment.NewLine;
            CodeBehindViews += "                this.SSEId = SSEId;" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "            if (UseSSE != null)" + Environment.NewLine;
            CodeBehindViews += "                this.UseSSE = UseSSE;" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "            if (ViewPath[0] == '>')" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += "                string TmpViewPath = ViewPath;" + Environment.NewLine;
            CodeBehindViews += "                return await SetPageLoadByPath(TmpViewPath.Remove(0, 1), context);" + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "            if (ModelClass != null)" + Environment.NewLine;
            CodeBehindViews += "                return await LoadPageAsync(ViewPath, ModelClass, context);" + Environment.NewLine;
            CodeBehindViews += "            else" + Environment.NewLine;
            CodeBehindViews += "                return await LoadPageAsync(ViewPath, context);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public async Task<string> RunControllerName(string ControllerClass, HttpContext context, bool IsDefaultController, bool BreakDefaultInSwitch)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            if (options.IgnoreLayoutForPostBack)
                CodeBehindViews += "            try{IgnoreLayoutForPostBack(context.Request.Headers);} catch(NullReferenceException){}" + Environment.NewLine + Environment.NewLine;
            if (options.SetTextHtmlContentTypeForPostBack)
                CodeBehindViews += "            try{SetTextHtmlContentTypeForPostBack(context);} catch(NullReferenceException){}" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            string TmpViewPath = \"\";" + Environment.NewLine;
            CodeBehindViews += "            switch (ControllerClass)" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += FillControllerNameCase();
            CodeBehindViews += "            }" + Environment.NewLine;
            CodeBehindViews += Environment.NewLine;
            CodeBehindViews += "            FoundController = false;" + Environment.NewLine;
            CodeBehindViews += "            return \"\";" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public bool PageHasFound()" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return FoundPage;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void IgnoreLayoutForPostBack(IHeaderDictionary Headers)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            if (Headers.TryGetValue(\"Post-Back\", out var value))" + Environment.NewLine;
            CodeBehindViews += "                if (value == \"true\")" + Environment.NewLine;
            CodeBehindViews += "                    IgnoreLayout = true;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void SetTextHtmlContentTypeForPostBack(HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            if (context.Request.Headers.TryGetValue(\"Post-Back\", out var value))" + Environment.NewLine;
            CodeBehindViews += "                if (value == \"true\")" + Environment.NewLine;
            CodeBehindViews += "                    context.Response.ContentType = \"text/html; charset=utf-8\";" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void SetWebSocketId(string Id)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            WebSocketId = Id;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public string GetWebSocketId()" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return WebSocketId;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void SetSSEId(string Id)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            SSEId = Id;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public string GetSSEId()" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return SSEId;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void EnableSSE()" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            UseSSE = true;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public bool? GetUseSSE()" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return UseSSE;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public bool ControllerHasFound()" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return FoundController;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public string GetWebFormsValue()" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return WebFormsValue;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void Control(WebForms Forms)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            WebFormsValue += Forms.GetFormsActionData();" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void Write(string Text)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            ResponseText += Text;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void Write(int Number)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            ResponseText += Number;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void Write(long Number)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            ResponseText += Number;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void WriteLine(string Text)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            Write(Text + Environment.NewLine);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void WriteLine(int Number)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            Write(Number + Environment.NewLine);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void WriteLine(long Number)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            Write(Number + Environment.NewLine);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // WebSockets Broadcast" + Environment.NewLine;
            CodeBehindViews += "        private void Broadcast(HttpContext context, string Message, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            CodeBehindMiddlewareExtensions.WebSocketsBroadcast(context, Message, \"\", \"\", \"\", IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private async void BroadcastAsync(HttpContext context, string Message, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            await CodeBehindMiddlewareExtensions.WebSocketsBroadcastAsync(context, Message, \"\", \"\", \"\", IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void Broadcast(HttpContext context, string Message, string RoleName, string Id, string ClientId, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            CodeBehindMiddlewareExtensions.WebSocketsBroadcast(context, Message, RoleName, Id, ClientId, IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private async void BroadcastAsync(HttpContext context, string Message, string RoleName, string Id, string ClientId, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            await CodeBehindMiddlewareExtensions.WebSocketsBroadcastAsync(context, Message, RoleName, Id, ClientId, IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void BroadcastForRole(HttpContext context, string Message, string RoleName, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            CodeBehindMiddlewareExtensions.WebSocketsBroadcast(context, Message, RoleName, \"\", \"\", IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private async void BroadcastForRoleAsync(HttpContext context, string Message, string RoleName, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            await CodeBehindMiddlewareExtensions.WebSocketsBroadcastAsync(context, Message, RoleName, \"\", \"\", IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void BroadcastForWebSocketId(HttpContext context, string Message, string Id, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            CodeBehindMiddlewareExtensions.WebSocketsBroadcast(context, Message, \"\", Id, \"\", IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private async void BroadcastForWebSocketIdAsync(HttpContext context, string Message, string Id, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            await CodeBehindMiddlewareExtensions.WebSocketsBroadcastAsync(context, Message, \"\", Id, \"\", IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void BroadcastForClientId(HttpContext context, string Message, string ClientId, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            CodeBehindMiddlewareExtensions.WebSocketsBroadcast(context, Message, \"\", \"\", ClientId, IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private async void BroadcastForClientIdAsync(HttpContext context, string Message, string ClientId, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            await CodeBehindMiddlewareExtensions.WebSocketsBroadcastAsync(context, Message, \"\", \"\", ClientId, IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // SSE Broadcast" + Environment.NewLine;
            CodeBehindViews += "        private void BroadcastSSE(HttpContext context, string Message, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            CodeBehindMiddlewareExtensions.SSEsBroadcast(context, Message, \"\", \"\", \"\", IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void BroadcastSSE(HttpContext context, string Message, string RoleName, string Id, string ClientId, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            CodeBehindMiddlewareExtensions.SSEsBroadcast(context, Message, RoleName, Id, ClientId, IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void BroadcastSSEForRole(HttpContext context, string Message, string RoleName, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            CodeBehindMiddlewareExtensions.SSEsBroadcast(context, Message, RoleName, \"\", \"\", IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void BroadcastSSEForSSEId(HttpContext context, string Message, string Id, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            CodeBehindMiddlewareExtensions.SSEsBroadcast(context, Message, \"\", Id, \"\", IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void BroadcastSSEForClientId(HttpContext context, string Message, string ClientId, bool IgnoreThis = false)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            CodeBehindMiddlewareExtensions.SSEsBroadcast(context, Message, \"\", \"\", ClientId, IgnoreThis);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private async Task DownloadAsync(HttpContext context, string FilePath)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            long FileSize = new FileInfo(FilePath).Length;" + Environment.NewLine;
            CodeBehindViews += "            var response = context.Response;" + Environment.NewLine;
            CodeBehindViews += "            response.Headers.Add(\"Content-Length\", FileSize.ToString());" + Environment.NewLine;
            CodeBehindViews += "            response.ContentType = \"application/octet-stream\";" + Environment.NewLine;
            CodeBehindViews += "            response.Headers.Add(\"Content-Disposition\", $\"attachment; filename=\\\"{System.IO.Path.GetFileName(FilePath)}\\\"\");" + Environment.NewLine;
            CodeBehindViews += "            await using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, bufferSize: 64 * 1024, useAsync: true))" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += "                var buffer = new byte[64 * 1024]; // 64KB" + Environment.NewLine;
            CodeBehindViews += "                int bytesRead;" + Environment.NewLine;
            CodeBehindViews += "                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)" + Environment.NewLine;
            CodeBehindViews += "                {" + Environment.NewLine;
            CodeBehindViews += "                    try" + Environment.NewLine;
            CodeBehindViews += "                    {" + Environment.NewLine;
            CodeBehindViews += "                        await response.Body.WriteAsync(buffer, 0, bytesRead);" + Environment.NewLine;
            CodeBehindViews += "                        await response.Body.FlushAsync();" + Environment.NewLine;
            CodeBehindViews += "                    }" + Environment.NewLine;
            CodeBehindViews += "                    catch" + Environment.NewLine;
            CodeBehindViews += "                    {" + Environment.NewLine;
            CodeBehindViews += "                        break;" + Environment.NewLine;
            CodeBehindViews += "                    }" + Environment.NewLine;
            CodeBehindViews += "                }" + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void Download(HttpContext context, string FilePath)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            DownloadAsync(context, FilePath).GetAwaiter().GetResult();" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine;

            CodeBehindViews += MethodCodeTemplateValue + "/*{MethodCodeTemplateValue}*/" + Environment.NewLine;

            CodeBehindViews += "    }" + Environment.NewLine;
            CodeBehindViews += "}" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "namespace " + AssemblyCleanName + Environment.NewLine;
            CodeBehindViews += "{" + Environment.NewLine;
            CodeBehindViews += "    public partial class CodeBehindEmptyClass" + Environment.NewLine;
            CodeBehindViews += "    {" + Environment.NewLine;
            CodeBehindViews += "    }" + Environment.NewLine;
            CodeBehindViews += "}";


            SaveError(ErrorList);
            return CodeBehindViews;
        }

        private void SaveError(List<string> ErrorList)
        {
            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            // Create views_error.log File
            if (ErrorList.Count > 0)
            {
                const string FilePath = "code_behind/views_class_aggregation_error.log";

                var file = File.CreateText(FilePath);

                file.WriteLine("date_and_time:" + DateTime.Now.ToString());

                foreach (string line in ErrorList)
                {
                    file.WriteLine(line);
                }

                file.Dispose();
                file.Close();
            }
        }

        private string GetGlobalTemplate()
        {
            string GlobalTemplate = "";

            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            const string FilePath = "code_behind/global_template.astx";

            if (!File.Exists(FilePath))
            {
                var file = File.CreateText(FilePath);

                file.Dispose();
                file.Close();

                return "";
            }

            var Lines = File.OpenText(FilePath);
            var TmpLine = "";
            while ((TmpLine = Lines.ReadLine()) != null)
            {
                GlobalTemplate += TmpLine + '\n';
            }

            return GlobalTemplate;
        }

        private void MoveViewFromWwwroot(string ViewPath, string Extension)
        {
            if (!Directory.Exists("wwwroot"))
                return;

            if (!Directory.Exists(Path.GetFullPath(ViewPath)))
                Directory.CreateDirectory(Path.GetFullPath(ViewPath));

            DirectoryInfo WwwrootDir = new DirectoryInfo("wwwroot");

            foreach (FileInfo file in WwwrootDir.GetFiles("*." + Extension, SearchOption.AllDirectories))
            {
                string ParrentDirectories = file.FullName.GetTextAfterValue(Path.GetFullPath("wwwroot")).GetTextBeforeLastValue(StaticObject.OsDirectorySplitter + file.Name);

                if (!Directory.Exists(Path.GetFullPath(ViewPath) + ParrentDirectories))
                    Directory.CreateDirectory(Path.GetFullPath(ViewPath) + ParrentDirectories);

                File.Move(file.FullName, Path.GetFullPath(ViewPath) + ParrentDirectories + StaticObject.OsDirectorySplitter + file.Name, true);
            }
        }

        private void MoveDllFromWwwrootBin(string DllPath)
        {
            var dir = new DirectoryInfo("wwwroot/bin");

            if (!dir.Exists)
                return;

            DirectoryCopy(Path.GetFullPath("wwwroot/bin"), Path.GetFullPath(DllPath), true);

            if (!dir.EnumerateFileSystemInfos().Any())
                dir.Delete();
        }

        private string ImportNamespaceList()
        {
            const string NamespaceImportListPath = "code_behind/namespace_import_list.ini";
            string ReturnValue = "";

            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            if (!File.Exists(NamespaceImportListPath))
            {
                var file = File.CreateText(NamespaceImportListPath);

                file.Write("[CodeBehind-namespace-import-list]" + Environment.NewLine);
                file.Write("namespace=System.IO" + Environment.NewLine);
                file.Write("namespace=System.Collections" + Environment.NewLine);
                file.Write("namespace=System.Collections.Generic" + Environment.NewLine);
                file.Write("namespace=System.Linq" + Environment.NewLine);
                file.Write("namespace=System.Threading" + Environment.NewLine);
                file.Write("namespace=System.Threading.Tasks");

                file.Dispose();
                file.Close();
            }

            using (StreamReader reader = new StreamReader(NamespaceImportListPath))
            {
                reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                    ReturnValue += "using " + line.GetTextAfterValue("=") + ";" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(ReturnValue))
            {
                ReturnValue = "// Start Import Namespace List" + Environment.NewLine + ReturnValue + "// End Import Namespace List" + Environment.NewLine;
            }

            return ReturnValue;
        }

        private string FillAssemblyControllerCase(Assembly assembly, string EntryAssemblyName)
        {
            Type ControllerType = typeof(CodeBehindController);
            var AssemblyClasses = assembly.GetTypes().Where(type => ControllerType.IsAssignableFrom(type) && !type.IsAbstract);

            CodeBehindOptions options = new CodeBehindOptions();

            string ReturnValue = "";

            foreach (var TmpClass in AssemblyClasses)
            {
                string NameSpace = (string.IsNullOrEmpty(TmpClass.Namespace) || (TmpClass.Namespace == EntryAssemblyName)) ? "" : TmpClass.Namespace + ".";
                string ClassName = "Tmp" + TmpClass.Namespace + "_" + TmpClass.Name;

                string ClassNameForCall = TmpClass.Name;
                if (options.PutTwoUnderlinesEqualToDashForController)
                    ClassNameForCall = ClassNameForCall.Replace("__", '-'.ToString());
                if (ClassNameForCall.StartsWith(options.IgnorePrefixController))
                    ClassNameForCall = ClassNameForCall.Remove(0, options.IgnorePrefixController.Length);
                if (ClassNameForCall.EndsWith(options.IgnoreSuffixController))
                    ClassNameForCall = ClassNameForCall.GetTextBeforeLastValue(options.IgnoreSuffixController);


                if (ClassNameForCall.ToLower() != ClassNameForCall)
                {
                    if (!options.JustAccessControllerByLowerCase)
                        ReturnValue += "                case \"" + ClassNameForCall + "\":" + Environment.NewLine;

                    if (options.AccessControllerByLowerCase || options.JustAccessControllerByLowerCase)
                        ReturnValue += "                case \"" + ClassNameForCall.ToLower() + "\":" + Environment.NewLine;
                }
                else
                    ReturnValue += "                case \"" + ClassNameForCall + "\":" + Environment.NewLine;

                if (StaticObject.SetBreakForDefaultController && (TmpClass.Name == options.DefaultController))
                {
                    ReturnValue += "                if (!IsDefaultController)" + Environment.NewLine;
                    ReturnValue += "                {" + Environment.NewLine;
                    ReturnValue += "                    FoundController = false;" + Environment.NewLine;
                    ReturnValue += "                    return \"\";" + Environment.NewLine;
                    ReturnValue += "                }" + Environment.NewLine;
                }

                // Get Cache
                CodeBehindControllerCache ControllerCache = new CodeBehindControllerCache();
                bool ControllerHasCache = ControllerCache.ControllerHasCache(TmpClass.Name);
                if (ControllerHasCache)
                {
                    ReturnValue += "                // Get Cache" + Environment.NewLine;
                    ReturnValue += "                CodeBehindControllerCache cbcc = new CodeBehindControllerCache();" + Environment.NewLine;
                    ReturnValue += "                bool HasMatchingController = cbcc.HasMatchingController(context.Request, \"" + TmpClass.Name + "\");" + Environment.NewLine;
                    ReturnValue += "                if (HasMatchingController)" + Environment.NewLine;
                    ReturnValue += "                {" + Environment.NewLine;
                    ReturnValue += "                    ControllerCache cache = new ControllerCache(context);" + Environment.NewLine;
                    ReturnValue += "                    string CacheResult = cache.GetControllerCache(\"" + TmpClass.Name + "\" + cbcc.CacheFilter);" + Environment.NewLine;
                    ReturnValue += "                    if (cache.ControllerHasCache)" + Environment.NewLine;
                    ReturnValue += "                        return CacheResult;" + Environment.NewLine;
                    ReturnValue += "                }" + Environment.NewLine + Environment.NewLine;
                }

                ReturnValue += "                " + NameSpace + TmpClass.Name + " " + ClassName + " = new " + NameSpace + TmpClass.Name + "();" + Environment.NewLine;
                ReturnValue += "                " + ClassName + ".FillSegment(context, \"/\" + ControllerClass);" + Environment.NewLine;

                // Check async in PageLoad
                MethodInfo? PageLoadMethod = TmpClass.GetMethod("PageLoad", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                bool IsAsyncPageLoad = PageLoadMethod != null && typeof(Task).IsAssignableFrom(PageLoadMethod.ReturnType);


                ReturnValue += "                " + (IsAsyncPageLoad? "await " : " ") + ClassName + ".PageLoad(context);" + Environment.NewLine;
                ReturnValue += "                this.WebFormsValue += " + ClassName + ".WebFormsValue;" + Environment.NewLine + Environment.NewLine;

                ReturnValue += "                if (" + ClassName + ".WebSocketId != null)" + Environment.NewLine;
                ReturnValue += "                    this.WebSocketId = " + ClassName + ".WebSocketId;" + Environment.NewLine + Environment.NewLine;

                ReturnValue += "                if (" + ClassName + ".SSEId != null)" + Environment.NewLine;
                ReturnValue += "                    this.SSEId = " + ClassName + ".SSEId;" + Environment.NewLine + Environment.NewLine;

                ReturnValue += "                if (" + ClassName + ".UseSSE != null)" + Environment.NewLine;
                ReturnValue += "                    this.UseSSE = " + ClassName + ".UseSSE;" + Environment.NewLine;

                ReturnValue += Environment.NewLine;

                // Set Cache
                if (ControllerHasCache)
                {
                    ReturnValue += "                if (HasMatchingController)" + Environment.NewLine;
                    ReturnValue += "                {" + Environment.NewLine;
                    ReturnValue += "                    ControllerCache cache = new ControllerCache(context);" + Environment.NewLine;
                    ReturnValue += "                    if (" + ClassName + ".IgnoreViewAndModel)" + Environment.NewLine;
                    ReturnValue += "                    {" + Environment.NewLine;
                    ReturnValue += "                        cache.SetControllerCache(\"" + TmpClass.Name + "\" + cbcc.CacheFilter, " + ClassName + ".ResponseText, " + ControllerCache.Duration + ");" + Environment.NewLine;
                    ReturnValue += "                        return " + ClassName + ".ResponseText;" + Environment.NewLine;
                    ReturnValue += "                    }" + Environment.NewLine;
                    ReturnValue += "                    else" + Environment.NewLine;
                    ReturnValue += "                    {" + Environment.NewLine;
                    ReturnValue += "                        string ControllerReturnValue = " +  "await RunController(context, " + ClassName + ".ViewPath, " + ClassName + ".CodeBehindModel, " + ClassName + ".ViewData, " + ClassName + ".DownloadFilePath, " + ClassName + ".IgnoreLayout, " + ClassName + ".WebFormsValue, " + ClassName + ".WebSocketId, " + ClassName + ".SSEId, " + ClassName + ".UseSSE) + " + ClassName + ".ResponseText;" + Environment.NewLine;
                    ReturnValue += "                        cache.SetControllerCache(\"" + TmpClass.Name + "\" + cbcc.CacheFilter, ControllerReturnValue, " + ControllerCache.Duration + ");" + Environment.NewLine;
                    ReturnValue += "                        return ControllerReturnValue;" + Environment.NewLine;
                    ReturnValue += "                    }" + Environment.NewLine;
                    ReturnValue += "                }" + Environment.NewLine + Environment.NewLine;
                }

                ReturnValue += "                TmpViewPath = " + ClassName + ".ViewPath;" + Environment.NewLine;
                ReturnValue += "                if (" + ClassName + ".IgnoreViewAndModel)" + Environment.NewLine;
                ReturnValue += "                    TmpViewPath = \"\";" + Environment.NewLine + Environment.NewLine;

                ReturnValue += "                return " + "await RunController(context, TmpViewPath, " + ClassName + ".CodeBehindModel, " + ClassName + ".ViewData, " + ClassName + ".DownloadFilePath, " + ClassName + ".IgnoreLayout, " + ClassName + ".WebFormsValue, " + ClassName + ".WebSocketId, " + ClassName + ".SSEId, " + ClassName + ".UseSSE) + " + ClassName + ".ResponseText;" + Environment.NewLine + Environment.NewLine;
            }

            ReturnValue += "/*{CaseCodeTemplateValueForControllerName}*/" + Environment.NewLine;

            return ReturnValue;
        }

        private string FillDefaultAssemblyControllerCase()
        {
            string ReturnValue = "";

            if (StaticObject.UseDefaultController && StaticObject.UseSegmentInDefaultController)
            {
                ReturnValue += "                default:" + Environment.NewLine;
                ReturnValue += "                    if (!BreakDefaultInSwitch)" + Environment.NewLine;
                ReturnValue += "                        return await RunControllerName(\"" + StaticObject.DefaultController + "\", context, true, true);" + Environment.NewLine;
                ReturnValue += "                break;" + Environment.NewLine;
            }

            return ReturnValue;
        }

        private string FillDllBinAssemblyControllerCase(string EntryAssemblyName)
        {
            if (!Directory.Exists(StaticObject.DllPath))
                return "";

            string ReturnValue = "";

            DirectoryInfo BinDir = new DirectoryInfo(StaticObject.DllPath);
            foreach (FileInfo file in BinDir.GetFiles("*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom(file.FullName);
                ReturnValue += FillAssemblyControllerCase(assembly, EntryAssemblyName);
            }

            return ReturnValue;
        }

        private string FillControllerNameCase()
        {
            string AssemblyCleanName = SetCleanNameForClass(Assembly.GetEntryAssembly().GetName().Name);

            Assembly assembly = Assembly.GetEntryAssembly();

            return FillAssemblyControllerCase(assembly, AssemblyCleanName) + FillDllBinAssemblyControllerCase(AssemblyCleanName) + FillDefaultAssemblyControllerCase();
        }

        private string SetCleanNameForClass(string CleanName)
        {
            Regex regex = new Regex("[^a-zA-Z0-9_]");
            CleanName = regex.Replace(CleanName, "_");

            if (char.IsNumber(CleanName[0]))
                CleanName = '_' + CleanName;

            return CleanName;
        }

        private void DirectoryCopy(string SourceDirName, string DestDirName, bool CopySubDirs, bool OwerWrite = false)
        {
            DirectoryInfo dir = new DirectoryInfo(SourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(DestDirName))
            {
                Directory.CreateDirectory(DestDirName);
            }

            // Create All Directories, Include Empty Directories
            foreach (DirectoryInfo subdir in dirs)
            {
                if (!Directory.Exists(DestDirName + "/" + subdir.Name))
                    Directory.CreateDirectory(DestDirName + "/" + subdir.Name);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string TmpPath = Path.Combine(DestDirName, file.Name);
                file.CopyTo(TmpPath, OwerWrite);
            }

            if (CopySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string TmpPath = Path.Combine(DestDirName, subdir.Name);

                    // Set Recursive
                    DirectoryCopy(subdir.FullName, TmpPath, CopySubDirs, OwerWrite);
                }
            }
        }
    }
}
