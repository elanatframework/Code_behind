using System.Text.RegularExpressions;
using System.Reflection;
using CodeBehind;
using CodeBehind.HtmlData;

namespace SetCodeBehind
{
    class CodeBehindLibraryCreator
    {
        private List<string> ErrorList = new List<string>();
        private bool RewriteAspxFileToDirectory;
        private bool AccessAspxFileAfterRewrite;
        private bool IgnoreDefaultAfterRewrite;
        private bool StartTrimInAspxFile;
        private bool EndTrimInAspxFile;
        private bool SetBreakForLayoutPage;
        private bool InnerTrimInAspxFile;
        private string CaseCodeTemplateValue = "";
        private string CaseCodeTemplateValueForFullPath = "";
        private string MethodCodeTemplateValue = "";
        private string GlobalTemplate = "";

        public string GetCodeBehindViews()
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

        public string GetLastSuccessCompiledViewClass()
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

        public string CreateAllAspxFiles()
        {
            string CodeBehindViews = "";
            CodeBehindViews += "using " + Assembly.GetEntryAssembly().GetName().Name + ";" + Environment.NewLine;
            CodeBehindViews += "using CodeBehind;" + Environment.NewLine;
            CodeBehindViews += "using System;" + Environment.NewLine;
            CodeBehindViews += "using System.Runtime;" + Environment.NewLine;
            CodeBehindViews += "using Microsoft.AspNetCore.Http;" + Environment.NewLine;
            CodeBehindViews += ImportNamespaceList();
            CodeBehindViews += Environment.NewLine;
            CodeBehindViews += "namespace CodeBehindViews" + Environment.NewLine;
            CodeBehindViews += "{" + Environment.NewLine;
            CodeBehindViews += "    public class CodeBehindViewsList" + Environment.NewLine;
            CodeBehindViews += "    {" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "        private CodeBehind.HtmlData.NameValueCollection ViewData = new CodeBehind.HtmlData.NameValueCollection();" + Environment.NewLine;

            CodeBehindOptions options = new CodeBehindOptions();
            RewriteAspxFileToDirectory = options.RewriteAspxFileToDirectory;
            AccessAspxFileAfterRewrite = options.AccessAspxFileAfterRewrite;
            IgnoreDefaultAfterRewrite = options.IgnoreDefaultAfterRewrite;
            StartTrimInAspxFile = options.StartTrimInAspxFile;
            EndTrimInAspxFile = options.EndTrimInAspxFile;
            SetBreakForLayoutPage = options.SetBreakForLayoutPage;
            InnerTrimInAspxFile = options.InnerTrimInAspxFile;


            // Create wwwroot Directory And Set Default.aspx File
            if (options.ViewPath == "wwwroot")
                if (!Directory.Exists("wwwroot"))
                {
                    Directory.CreateDirectory("wwwroot");

                    string FilePath = "wwwroot/layout.aspx";
                    var file1 = File.CreateText(FilePath);

                    file1.WriteLine("@page");
                    file1.WriteLine("@islayout");
                    file1.WriteLine("@{");
                    file1.WriteLine("  string WelcomeText = \"Welcome to the CodeBehind Framework!\";");
                    file1.WriteLine("}");
                    file1.WriteLine("<!DOCTYPE html>");
                    file1.WriteLine("<html>");
                    file1.WriteLine("<head>");
                    file1.WriteLine("  <title>CodeBehind Framework - @ViewData.GetValue(\"title\")</title>");
                    file1.WriteLine("  <style>");
                    file1.WriteLine("  body");
                    file1.WriteLine("  {");
                    file1.WriteLine("      font-family: Arial, sans-serif;");
                    file1.WriteLine("      margin: 0;");
                    file1.WriteLine("      padding: 0;");
                    file1.WriteLine("      line-height: 32px;");
                    file1.WriteLine("  }");
                    file1.WriteLine();
                    file1.WriteLine("  header");
                    file1.WriteLine("  {");
                    file1.WriteLine("      background-color: #f2f2f2;");
                    file1.WriteLine("      text-align: center;");
                    file1.WriteLine("      padding: 20px 0;");
                    file1.WriteLine("  }");
                    file1.WriteLine();
                    file1.WriteLine("  nav");
                    file1.WriteLine("  {");
                    file1.WriteLine("      background-color: #90dbff;");
                    file1.WriteLine("      color: #fff;");
                    file1.WriteLine("      text-align: center;");
                    file1.WriteLine("      padding: 10px 0;");
                    file1.WriteLine("  }");
                    file1.WriteLine();
                    file1.WriteLine("  nav ul");
                    file1.WriteLine("  {");
                    file1.WriteLine("      list-style-type: none;");
                    file1.WriteLine("      padding: 0;");
                    file1.WriteLine("  }");
                    file1.WriteLine();
                    file1.WriteLine("  nav ul li");
                    file1.WriteLine("  {");
                    file1.WriteLine("      display: inline;");
                    file1.WriteLine("      margin: 0 10px;");
                    file1.WriteLine("  }");
                    file1.WriteLine();
                    file1.WriteLine("  footer");
                    file1.WriteLine("  {");
                    file1.WriteLine("      background-color: #333;");
                    file1.WriteLine("      color: #fff;");
                    file1.WriteLine("      text-align: center;");
                    file1.WriteLine("      padding: 10px 0;");
                    file1.WriteLine("  }");
                    file1.WriteLine("  </style>");
                    file1.WriteLine("</head>");
                    file1.WriteLine("<body>");
                    file1.WriteLine();
                    file1.WriteLine("  @LoadPage(\"/header.aspx\")");
                    file1.WriteLine();
                    file1.WriteLine("  <nav>");
                    file1.WriteLine("      <ul>");
                    file1.WriteLine("          <li><a href=\"#\">Home</a></li>");
                    file1.WriteLine("          <li><a href=\"#\">About</a></li>");
                    file1.WriteLine("          <li><a href=\"#\">Contact</a></li>");
                    file1.WriteLine("      </ul>");
                    file1.WriteLine("  </nav>");
                    file1.WriteLine();
                    file1.WriteLine("  <h2>CodeBehind Framework - @ViewData.GetValue(\"title\")</h2>");
                    file1.WriteLine("  <p>Text value is: @WelcomeText</p>");
                    file1.WriteLine();
                    file1.WriteLine("  @PageReturnValue");
                    file1.WriteLine();
                    file1.WriteLine("  @LoadPage(\"/footer.aspx\")");
                    file1.WriteLine();
                    file1.WriteLine("</body>");
                    file1.WriteLine("</html>");

                    file1.Dispose();
                    file1.Close();


                    FilePath = "wwwroot/Default.aspx";
                    var file2 = File.CreateText(FilePath);

                    file2.WriteLine("@page");
                    file2.WriteLine("@layout \"/layout.aspx\"");
                    file2.WriteLine("@{");
                    file2.WriteLine("  ViewData.Add(\"title\",\"Main page\");");
                    file2.WriteLine("}");
                    file2.WriteLine("  <main>");
                    file2.WriteLine("      <p>CodeBehind library is a modern back-end framework and is an alternative to ASP.NET Core. This library is a programming model based on the MVC structure, which provides the possibility of creating dynamic aspx files in .NET Core and has high serverside independence. CodeBehind framework supports standard syntax and Razor syntax. This framework guarantees the separation of server-side codes from the design part (html) and there is no need to write server-side codes in the view.</p>");
                    file2.WriteLine("      <p>Code Behind framework inherits every advantage of ASP.NET Core and gives it more simplicity, power and flexibility.</p>");
                    file2.WriteLine("      <p><b>CodeBehind framework is an alternative to ASP.NET Core.</b></p>");
                    file2.WriteLine("      <h3>Why use CodeBehind?</h3>");
                    file2.WriteLine("      <ul>");
                    file2.WriteLine("          <li><b>Fast:</b> The CodeBehind framework is faster than the default structure of cshtml pages in ASP.NET Core.</li>");
                    file2.WriteLine("          <li><b>Simple:</b> Developing with CodeBehind is very simple. You can use mvc pattern or model-view or controller-view or only view.</li>");
                    file2.WriteLine("          <li><b>Modular:</b> It is modular. Just copy the new project files, including dll and aspx, into the current active project.</li>");
                    file2.WriteLine("          <li><b>Get output:</b> You can call the output of the aspx page in another aspx page and modify its output.</li>");
                    file2.WriteLine("          <li><b>Under .NET Core:</b> Your project will still be under ASP.NET Core and you will benefit from all the benefits of .NET Core.</li>");
                    file2.WriteLine("          <li><b>Code-Behind:</b> Code-Behind pattern will be fully respected.</li>");
                    file2.WriteLine("          <li><b>Modern:</b> CodeBehind is a modern framework with revolutionary ideas.</li>");
                    file2.WriteLine("          <li><b>Understandable:</b> View is preferable to controller and there is no need to set controllers in route.</li>");
                    file2.WriteLine("      </ul>");
                    file2.WriteLine("      <p><b>CodeBehind is .NET Diamond!</b></p>");
                    file2.WriteLine("      <p>In every scenario, CodeBehind performs better than the default structure in ASP.NET Core.</p>");
                    file2.WriteLine("  </main>");

                    file2.Dispose();
                    file2.Close();


                    FilePath = "wwwroot/header.aspx";
                    var file3 = File.CreateText(FilePath);

                    file3.WriteLine("@page");
                    file3.WriteLine("@break");
                    file3.WriteLine("  <header>");
                    file3.WriteLine("      <h1>Company name</h1>");
                    file3.WriteLine("  </header>");

                    file3.Dispose();
                    file3.Close();


                    FilePath = "wwwroot/footer.aspx";
                    var file4 = File.CreateText(FilePath);

                    file4.WriteLine("@page");
                    file4.WriteLine("@break");
                    file4.WriteLine("  <footer>");
                    file4.WriteLine("      <p>&copy; @DateTime.Now.ToString(\"yyyy\") Company name - Built with <a href=\"https://elanat.net/page_content/code_behind\" title=\"CodeBehind Framework\">CodeBehind Framework</a></p>");
                    file4.WriteLine("  </footer>");

                    file4.Dispose();
                    file4.Close();
                }

            // Fill Global Template
            FillGlobalTemplate();


            // Move View From Wwwroot
            if ((options.ViewPath != "wwwroot") && options.MoveViewFromWwwroot)
            {
                MoveViewFromWwwroot(options.ViewPath, "aspx");
                MoveViewFromWwwroot(options.ViewPath, "astx");

                if (options.ConvertCsHtmlToAspx)
                    MoveViewFromWwwroot(options.ViewPath, "cshtml");
            }


            DirectoryInfo RootDir = new DirectoryInfo(options.ViewPath);
            string RootDirectoryPath = RootDir.FullName;
            int i = 1;
            foreach (FileInfo file in RootDir.GetFiles("*.aspx", SearchOption.AllDirectories))
            {
                AspxTextAndCodeCombination(file.FullName, RootDirectoryPath, i);
                i++;
            }

            if (options.ConvertCsHtmlToAspx)
            foreach (FileInfo file in RootDir.GetFiles("*.cshtml", SearchOption.AllDirectories))
                {
                    AspxTextAndCodeCombination(file.FullName, RootDirectoryPath, i);
                    i++;
                }

            CodeBehindViews += "        // It Works Based On Rewriting The Option File" + Environment.NewLine;
            CodeBehindViews += "        public string SetPageLoadByPath(string path, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            switch (path)" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += CaseCodeTemplateValue + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine;
            CodeBehindViews += "            return \"\";" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Load All Page By Full Path, This Method Load Break Page And Does Not Apply Rewrite" + Environment.NewLine;
            CodeBehindViews += "        public string SetPageLoadByFullPath(string path, HttpContext context, string PageReturnValue = \"\")" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            switch (path)" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += CaseCodeTemplateValueForFullPath + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine;
            CodeBehindViews += "            return \"\";" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            // Add Load Page Method
            CodeBehindViews += "        private string LoadPage(string path, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return SetPageLoadByFullPath(path, context);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Overload" + Environment.NewLine;
            CodeBehindViews += "        private string LoadPage(string path)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return SetPageLoadByFullPath(path, null);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        private void Download(HttpContext context, string FilePath)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            long FileSize = new FileInfo(FilePath).Length;" + Environment.NewLine;
            CodeBehindViews += "            var response = context.Response;" + Environment.NewLine;
            CodeBehindViews += "            response.Headers.Add(\"Content-Length\", FileSize.ToString());" + Environment.NewLine;
            CodeBehindViews += "            response.ContentType = \"application/octet-stream\";" + Environment.NewLine;
            CodeBehindViews += "            response.Headers.Add(\"Content-Disposition\", $\"attachment; filename=\\\"{System.IO.Path.GetFileName(FilePath)}\\\"\");" + Environment.NewLine;
            CodeBehindViews += "            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += "                var bufferSize = 64 * 1024; // 64KB" + Environment.NewLine;
            CodeBehindViews += "                var buffer = new byte[bufferSize];" + Environment.NewLine;
            CodeBehindViews += "                int bytesRead;" + Environment.NewLine;
            CodeBehindViews += "                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)" + Environment.NewLine;
            CodeBehindViews += "                {" + Environment.NewLine;
            CodeBehindViews += "                    try" + Environment.NewLine;
            CodeBehindViews += "                    {" + Environment.NewLine;
            CodeBehindViews += "                        response.Body.WriteAsync(buffer, 0, bytesRead);" + Environment.NewLine;
            CodeBehindViews += "                        response.Body.FlushAsync();" + Environment.NewLine;
            CodeBehindViews += "                    }" + Environment.NewLine;
            CodeBehindViews += "                    catch" + Environment.NewLine;
            CodeBehindViews += "                    {" + Environment.NewLine;
            CodeBehindViews += "                        break;" + Environment.NewLine;
            CodeBehindViews += "                    }" + Environment.NewLine;
            CodeBehindViews += "                }" + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += MethodCodeTemplateValue + Environment.NewLine;

            CodeBehindViews += "    }" + Environment.NewLine;
            CodeBehindViews += "}" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "namespace " + Assembly.GetEntryAssembly().GetName().Name + Environment.NewLine;
            CodeBehindViews += "{" + Environment.NewLine;
            CodeBehindViews += "    public partial class CodeBehindEmptyClass" + Environment.NewLine;
            CodeBehindViews += "    {" + Environment.NewLine;
            CodeBehindViews += "    }" + Environment.NewLine;
            CodeBehindViews += "}";


            SaveError(ErrorList);
            return CodeBehindViews;
        }

        public void AspxTextAndCodeCombination(string FilePath, string RootDirectoryPath, int MethodIndexer)
        {
            var Lines = File.OpenText(FilePath);
            string AspxText = "";
            var TmpLine = "";
            while ((TmpLine = Lines.ReadLine()) != null)
            {
                AspxText += TmpLine + '\n';
            }

            AspxText = AspxText.GetTextBeforeLastValue('\n'.ToString());

            Lines.Close();

            // Fetch Page
            if (AspxText.Contains("<%@"))
                AspxTextAndCodeCombinationStandard(AspxText, FilePath, RootDirectoryPath, MethodIndexer);
            else
                AspxTextAndCodeCombinationRazor(AspxText, FilePath, RootDirectoryPath, MethodIndexer);
        }

        public void SetMethod(string AspxFilePath, string Controller, string ControllerConstructor, string Model, string ModelConstructor, bool ControllerIsSet, string Layout, bool IsLayout, bool IsBreak, int MethodIndexer, string TextToCodeCombination)
        {
            if (AspxFilePath.EndsWith(".cshtml"))
                AspxFilePath = AspxFilePath.GetTextBeforeLastValue(".cshtml") + ".aspx";


            string FilePathToMethodName = AspxFilePath.ToMethodNameClean();
            bool PageIsOnlyView = !ControllerIsSet;

            if (!IsBreak)
                if (IsLayout && SetBreakForLayoutPage)
                    IsBreak = true;

            if (!IsBreak)
            {
                if (RewriteAspxFileToDirectory)
                    if (IgnoreDefaultAfterRewrite && (AspxFilePath.GetTextAfterLastValue('\\'.ToString()) == "Default.aspx"))
                        CaseCodeTemplateValue += "                case \"" + AspxFilePath.Replace("\\", "/") + "\": return " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(context" + (IsLayout ? ", \"\"" : "") + ");" + Environment.NewLine;
                    else
                        CaseCodeTemplateValue += "                case \"" + AspxFilePath.Replace("\\", "/").GetTextBeforeLastValue(".aspx") + "/Default.aspx" + "\": return " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(context" + (IsLayout? ", \"\"" : "") + ");" + Environment.NewLine;

                if (!RewriteAspxFileToDirectory || (RewriteAspxFileToDirectory && AccessAspxFileAfterRewrite && !(IgnoreDefaultAfterRewrite && (AspxFilePath.GetTextAfterLastValue('\\'.ToString()) == "Default.aspx"))))
                    CaseCodeTemplateValue += "                case \"" + AspxFilePath.Replace("\\", "/") + "\": return " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(context" + (IsLayout? ", \"\"" : "") + ");" + Environment.NewLine;
            }

            CaseCodeTemplateValueForFullPath += "                case \"" + AspxFilePath.Replace("\\", "/") + "\": return " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(context" + (IsLayout? ", PageReturnValue" : "") + ");" + Environment.NewLine;

            string TmpMethodCodeTemplateValue = Environment.NewLine;
            TmpMethodCodeTemplateValue += "        protected string " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(HttpContext context" + (IsLayout? ", string PageReturnValue" : "") + ")" + Environment.NewLine;
            TmpMethodCodeTemplateValue += "        {" + Environment.NewLine;

            if (!PageIsOnlyView)
            {
                TmpMethodCodeTemplateValue += "            " + Controller + " controller = new " + Controller + "();" + Environment.NewLine;

                if (!string.IsNullOrEmpty(ControllerConstructor))
                    TmpMethodCodeTemplateValue += "            controller.CodeBehindConstructor(" + ControllerConstructor + ");" + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            controller.PageLoad(context);" + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            ViewData.AddList(controller.ViewData.GetList());" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            if (!string.IsNullOrEmpty(controller.ViewPath))" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                return LoadPage(controller.ViewPath, context);" + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            if (!string.IsNullOrEmpty(controller.DownloadFilePath))" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            {" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                Download(context, controller.DownloadFilePath);" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                return \"\";" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            }" + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            if (!controller.IgnoreViewAndModel)" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            {" + Environment.NewLine;

                if (!string.IsNullOrEmpty(Model))
                {
                    TmpMethodCodeTemplateValue += "                " + Model + " model = (" + Model + ")controller.CodeBehindModel;" + Environment.NewLine;

                    if (!string.IsNullOrEmpty(ModelConstructor))
                        TmpMethodCodeTemplateValue += "                model.CodeBehindConstructor(" + ModelConstructor + ");" + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "                ViewData.AddList(model.ViewData.GetList());" + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "                if (!string.IsNullOrEmpty(model.DownloadFilePath))" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                {" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                    Download(context, model.DownloadFilePath);" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                    return \"\";" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                }" + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "                controller.ResponseText += model.ResponseText;" + Environment.NewLine;
                }

                TmpMethodCodeTemplateValue += TextToCodeCombination;
                TmpMethodCodeTemplateValue += "            }" + Environment.NewLine;
                
                TmpMethodCodeTemplateValue += "            return " + (!string.IsNullOrEmpty(Layout) ? "SetPageLoadByFullPath(\"" + Layout + "\", context, controller.ResponseText)" : "controller.ResponseText") + ";" + Environment.NewLine;
            }
            else
            {
                TmpMethodCodeTemplateValue += "            string ReturnValue = \"\";" + Environment.NewLine;

                if (!string.IsNullOrEmpty(Model))
                {
                    TmpMethodCodeTemplateValue += "            " + Model + " model = new " + Model + "();" + Environment.NewLine;

                    if (!string.IsNullOrEmpty(ModelConstructor))
                        TmpMethodCodeTemplateValue += "            model.CodeBehindConstructor(" + ModelConstructor + ");" + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "            ViewData.AddList(model.ViewData.GetList());" + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "            ReturnValue += model.ResponseText;" + Environment.NewLine;
                }

                TmpMethodCodeTemplateValue += TextToCodeCombination;

                TmpMethodCodeTemplateValue += "            return " + (!string.IsNullOrEmpty(Layout) ? "SetPageLoadByFullPath(\"" + Layout + "\", context, ReturnValue)" : "ReturnValue") + ";" + Environment.NewLine;
            }

            TmpMethodCodeTemplateValue += "        }" + Environment.NewLine;

            MethodCodeTemplateValue += TmpMethodCodeTemplateValue;
        }

        public void AspxTextAndCodeCombinationStandard(string AspxText, string FilePath, string RootDirectoryPath, int MethodIndexer)
        {
            string AspxFilePath = FilePath.GetTextAfterValue(RootDirectoryPath);

            if (!AspxText.Contains("<%@"))
            {
                ErrorList.Add("Error: Index <%@ not exist in " + AspxFilePath + " file");
                return;
            }

            string PageProperties = " " + AspxText.Split(new string[] { "<%@" }, StringSplitOptions.None)[1].Split("%>")[0] + " ";

            if (!PageProperties.Contains(" Page "))
            {
                ErrorList.Add("Error: Page not exist after index <%@ in " + AspxFilePath + " file");
                return;
            }


            bool PageIsOnlyView = (PageProperties.Trim() == "Page");

            if (!PageIsOnlyView)
                if (!PageProperties.Contains(" Controller=\""))
                    PageIsOnlyView = true;


            // Set Controller
            string Controller = "";
            string ControllerConstructor = "";
            string ModelConstructor = "";

            if (!PageIsOnlyView)
            {
                Controller = PageProperties.Split(new string[] { "Controller=\"" }, StringSplitOptions.None)[1].Split("\"")[0];

                // Get Controller Constructor Method
                if (Controller.Contains("("))
                {
                    ControllerConstructor = Controller.GetTextAfterValue("(").GetTextBeforeLastValue(")").Replace("&quot;", "\"");
                    Controller = Controller.GetTextBeforeValue("(");
                }


                if (!Controller.ClassPathIsFine())
                {
                    ErrorList.Add("Error: Controller class path is not fine in " + AspxFilePath + " file");
                    return;
                }
            }


            // Set Model
            string Model = (PageProperties.Contains(" Model=\"")) ? PageProperties.Split(new string[] { "Model=\"" }, StringSplitOptions.None)[1].Split("\"")[0] : "";

            if (Model != "")
            {
                // Get Model Constructor Method
                if (Model.Contains("("))
                {
                    ModelConstructor = Model.GetTextAfterValue("(").GetTextBeforeLastValue(")").Replace("&quot;", "\"");
                    Model = Model.GetTextBeforeValue("(");
                }


                if (!Model.ClassPathIsFine())
                {
                    ErrorList.Add("Error: Model class path is not fine in " + AspxFilePath + " file");
                    return;
                }
            }


            // Set Layout
            string Layout = (PageProperties.Contains(" Layout=\"")) ? PageProperties.Split(new string[] { "Layout=\"" }, StringSplitOptions.None)[1].Split("\"")[0] : "";

            if (Layout != "")
            {
                string LayoutPath = "";

                if (Layout[0] == '/' || Layout[0].ToString() == @"\")
                    LayoutPath = RootDirectoryPath + @"\" + Layout;
                else
                    LayoutPath = FilePath.GetTextBeforeLastValue(@"\") + @"\" + Layout;

                if (!Path.HasExtension(LayoutPath))
                {
                    Layout += ".aspx";
                    LayoutPath += ".aspx";
                }

                if (Path.GetExtension(LayoutPath) != ".aspx")
                {
                    ErrorList.Add("Error: Layout extension is not valid in " + AspxFilePath + " file");
                    return;
                }

                if (!File.Exists(LayoutPath))
                {
                    ErrorList.Add("Error: Layout file is not exist in " + LayoutPath + " path");
                    return;
                }
            }


            // Set If Is Layout
            string TmpIsLayout = (PageProperties.Contains(" IsLayout=\"")) ? PageProperties.Split(new string[] { "IsLayout=\"" }, StringSplitOptions.None)[1].Split("\"")[0] : "";
            bool IsLayout = (TmpIsLayout == "true");


            // Set Break
            string Break = (PageProperties.Contains(" Break=\"")) ? PageProperties.Split(new string[] { "Break=\"" }, StringSplitOptions.None)[1].Split("\"")[0] : "";
            bool IsBreak = (Break == "true");

            // Set Global Template
            AspxText = GlobalTemplate + AspxText;

            // Set Template
            string Template = (PageProperties.Contains(" Template=\"")) ? PageProperties.Split(new string[] { "Template=\"" }, StringSplitOptions.None)[1].Split("\"")[0] : "";

            if (Template != "")
            {
                string[] Templates = Template.Split(';');

                foreach (string TmpTemplate in Templates)
                {
                    Template = TmpTemplate;

                    string TemplatePath = "";

                    if (Template[0] == '/' || Template[0].ToString() == @"\")
                        TemplatePath = RootDirectoryPath + @"\" + Template;
                    else
                        TemplatePath = FilePath.GetTextBeforeLastValue(@"\") + @"\" + Template;

                    if (!Path.HasExtension(TemplatePath))
                        TemplatePath += ".astx";

                    if (Path.GetExtension(TemplatePath) != ".astx")
                    {
                        ErrorList.Add("Error: Template extension is not valid in " + AspxFilePath + " file");
                        return;
                    }

                    if (!File.Exists(TemplatePath))
                    {
                        ErrorList.Add("Error: Template file is not exist in " + TemplatePath + " path");
                        return;
                    }

                    string AstxText = "";
                    var Lines = File.OpenText(TemplatePath);
                    var TmpLine = "";
                    while ((TmpLine = Lines.ReadLine()) != null)
                    {
                        AstxText += TmpLine + '\n';
                    }

                    AspxText = AstxText + AspxText;
                }
            }

            string TextToCodeCombination = "";

            // Clear Page Properties
            while (AspxText.Contains("<%@"))
            {
                if (!AspxText.Contains("%>"))
                {
                    ErrorList.Add("Error: Index <%@ not closed %> for clear in " + AspxFilePath + " file");
                    break;
                }

                string InnerText = AspxText.Split(new string[] { "<%@" }, StringSplitOptions.None)[1].Split("%>")[0];

                AspxText = AspxText.Replace("<%@" + InnerText + "%>", "");
            }


            // Fetch Template
            string TmpAspxText = AspxText;
            while (TmpAspxText.Contains("<#"))
            {
                TmpAspxText = "<#" + TmpAspxText.GetTextAfterValue("<#");

                if (!TmpAspxText.Contains("#>"))
                {
                    ErrorList.Add("Error: Index <# not closed #> for code combination in " + AspxFilePath + " file");
                    break;
                }

                if (TmpAspxText.Length < 1)
                    break;

                CodeBehindFetchStandardSyntex syntex1 = new CodeBehindFetchStandardSyntex();
                string InnerText = syntex1.FetchInnerText(TmpAspxText, "<#", "#>");

                string PartName = GetTemplatePartName(InnerText);
                char CharacterAfterTemplatePartName = GetCharacterAfterTemplatePartName(InnerText);
                bool PartHasTemplate = PartHasTemplateValue(InnerText);

                if (PartHasTemplate)
                {
                    string TmpInnerTextForReplace = InnerText;

                    if (AspxText.Contains("<#" + PartName + "="))
                    {
                        CodeBehindFetchStandardSyntex syntex2 = new CodeBehindFetchStandardSyntex();
                        string ReturnedInnerText = syntex2.FetchInnerText("<#" + PartName + "=" + AspxText.GetTextAfterValue("<#" + PartName + "="), "<#", "#>");

                        if (InnerText.Contains("<#" + PartName + "#>"))
                        {
                            AspxText = AspxText.Replace("<#" + InnerText + "#>", "");

                            string TmpInnerTextForCodeBlock = InnerText;
                            while (TmpInnerTextForCodeBlock.Contains("<%"))
                            {
                                if (!TmpInnerTextForCodeBlock.Contains("%>"))
                                {
                                    ErrorList.Add("Error: Index <% not closed %> in template part " + PartName + " for code combination in " + AspxFilePath + " file");
                                    break;
                                }

                                string TmpNewInnerText = TmpInnerTextForCodeBlock.Split(new string[] { "<%" }, StringSplitOptions.None)[1].Split("%>")[0];

                                if (TmpNewInnerText.Contains("<#" + PartName + "#>"))
                                {
                                    if (TmpNewInnerText[0] == '=')
                                        TmpInnerTextForReplace = TmpInnerTextForReplace.Replace("<#" + PartName + "#>", "%>" + "<#" + PartName + "#>" + "<%=");
                                    else
                                        TmpInnerTextForReplace = TmpInnerTextForReplace.Replace("<#" + PartName + "#>", "%>" + "<#" + PartName + "#>" + "<%");
                                }

                                var regex2 = new Regex(Regex.Escape("<%" + TmpNewInnerText + "%>"));
                                TmpInnerTextForCodeBlock = regex2.Replace(TmpInnerTextForCodeBlock, "", 1);
                            }
                        }

                        if (!ReturnedInnerText.Contains("<#" + PartName + "#>"))
                            TmpInnerTextForReplace = TmpInnerTextForReplace.Replace("<#" + PartName + "#>", ReturnedInnerText.GetTextAfterValue("="));

                        string TmpInnerTextValue;
                        if (CharacterAfterTemplatePartName == '<')
                            TmpInnerTextValue = TmpInnerTextForReplace.GetTextAfterValue(PartName);
                        else
                            TmpInnerTextValue = TmpInnerTextForReplace.GetTextAfterValue(CharacterAfterTemplatePartName.ToString());

                        AspxText = AspxText.Replace("<#" + ReturnedInnerText + "#>", TmpInnerTextValue);
                    }

                    string InnerTextValue;
                    if (CharacterAfterTemplatePartName == '<')
                        InnerTextValue = TmpInnerTextForReplace.GetTextAfterValue(PartName);
                    else
                        InnerTextValue = TmpInnerTextForReplace.GetTextAfterValue(CharacterAfterTemplatePartName.ToString());

                    if (!InnerTextValue.Contains("<#" + PartName + "#>"))
                    {
                        AspxText = AspxText.Replace('{' + "<#" + PartName + "#>" + '}', InnerTextValue.Replace("\"", @"\" + "\"").Replace('\n'.ToString(), @"\" + "n"));
                        AspxText = AspxText.Replace("<#" + PartName + "#>", InnerTextValue);
                    }

                    var regex = new Regex(Regex.Escape("<#" + InnerText + "#>"));
                    AspxText = regex.Replace(AspxText, "", 1);
                }

                if (syntex1.FindSyntex)
                    TmpAspxText = TmpAspxText.Remove(syntex1.StartSyntex, syntex1.EndSyntex - syntex1.StartSyntex);
                else
                    TmpAspxText = TmpAspxText.Replace("<#" + InnerText + "#>", "");
            }


            // Set Trim Option
            FullTrim ft = new FullTrim();
            if (StartTrimInAspxFile)
                AspxText = ft.FullTrimInStart(AspxText);
            if (EndTrimInAspxFile)
                AspxText = ft.FullTrimInEnd(AspxText);
            if (InnerTrimInAspxFile)
                AspxText = AspxText.Replace('\n' + "<%", "<%");

            bool RemoveFirstEmptyLine = false;
            if (AspxText.Length > 4 && StartTrimInAspxFile)
                if (AspxText.Substring(0, 2) == "<%")
                    RemoveFirstEmptyLine = true;

            // Code Combination
            while (AspxText.Contains("<%"))
            {
                // Set Remove First Empty Line
                if (RemoveFirstEmptyLine)
                {
                    if (AspxText.Length > 1)
                        if (AspxText[0] == ('\n'))
                            AspxText = AspxText.Remove(0, 1);

                    RemoveFirstEmptyLine = false;
                }

                TextToCodeCombination += GetWriteText(AspxText.GetTextBeforeValue("<%"), PageIsOnlyView);

                AspxText = "<%" + AspxText.GetTextAfterValue("<%");

                if (!AspxText.Contains("%>"))
                {
                    ErrorList.Add("Error: Index <% not closed %> for code combination in " + AspxFilePath + " file");
                    break;
                }

                string InnerText = AspxText.Split(new string[] { "<%" }, StringSplitOptions.None)[1].Split("%>")[0];

                if (InnerText.Length > 0)
                    if (InnerText[0] == '=')
                    {
                        TextToCodeCombination += GetWriteCode(InnerText.Remove(0, 1), PageIsOnlyView);
                    }
                    else
                        TextToCodeCombination += GetAddCode(InnerText, PageIsOnlyView);

                var regex = new Regex(Regex.Escape("<%" + InnerText + "%>"));
                AspxText = regex.Replace(AspxText, "", 1);
            }

            TextToCodeCombination += GetWriteText(AspxText, PageIsOnlyView);

            SetMethod(AspxFilePath, Controller, ControllerConstructor, Model, ModelConstructor, !PageIsOnlyView, Layout, IsLayout, IsBreak, MethodIndexer, TextToCodeCombination);
        }

        public void AspxTextAndCodeCombinationRazor(string AspxText, string FilePath, string RootDirectoryPath, int MethodIndexer)
        {
            string AspxFilePath = FilePath.GetTextAfterValue(RootDirectoryPath);

            CodeBehindFetchRazorSyntex syntex = new CodeBehindFetchRazorSyntex();

            FullTrim ft = new FullTrim();
            AspxText = ft.FullTrimInStart(AspxText);

            // Fetch Page
            if (AspxText.Length < 5)
            {
                ErrorList.Add("Error: Index @page not exist for code combination in " + AspxFilePath + " file");
                return;
            }

            if (AspxText.Substring(0, 5) != "@page")
            {
                ErrorList.Add("Error: Index @page not exist for code combination in " + AspxFilePath + " file");
                return;
            }

            AspxText = AspxText.Remove(0, 5);


            // Set Controller
            string ControllerConstructor = "";
            bool ControllerIsSet = false;
            string Controller = "";

            string TmpAspxText = AspxText;

            while (TmpAspxText.Contains("@controller"))
            {
                TmpAspxText = TmpAspxText.GetTextAfterValue("@controller");

                if (TmpAspxText.Length > 1)
                {
                    char CharacterAfterController = TmpAspxText[0];

                    if (CharacterAfterController == ' ' || CharacterAfterController == '\n' || CharacterAfterController == '\t' || CharacterAfterController == '\r')
                    {
                        TmpAspxText = "@" + ft.FullTrimInStart(TmpAspxText);

                        Controller = syntex.FetchExpressions(TmpAspxText);

                        string BetweenText = AspxText.Split(new string[] { "@controller" + CharacterAfterController }, StringSplitOptions.None)[1].Split(Controller)[0];

                        AspxText = AspxText.Replace("@controller" + CharacterAfterController + BetweenText + Controller, "");

                        break;
                    }
                }
            }

            if (Controller != "")
            {
                // Get Controller Constructor Method
                if (Controller.Contains("("))
                {
                    ControllerConstructor = Controller.GetTextAfterValue("(").GetTextBeforeLastValue(")");
                    Controller = Controller.GetTextBeforeValue("(");
                }

                if (!Controller.ClassPathIsFine())
                {
                    ErrorList.Add("Error: Controller class path is not fine in " + AspxFilePath + " file");
                    return;
                }

                ControllerIsSet = true;
            }


            // Set Model
            string ModelConstructor = "";
            string Model = "";
            
            TmpAspxText = AspxText;

            while (TmpAspxText.Contains("@model"))
            {
                TmpAspxText = TmpAspxText.GetTextAfterValue("@model");

                if (TmpAspxText.Length > 1)
                {
                    char CharacterAfterModel = TmpAspxText[0];

                    if (CharacterAfterModel == ' ' || CharacterAfterModel == '\n' || CharacterAfterModel == '\t' || CharacterAfterModel == '\r')
                    {
                        TmpAspxText = "@" + ft.FullTrimInStart(TmpAspxText);

                        Model = syntex.FetchExpressions(TmpAspxText);

                        string BetweenText = AspxText.Split(new string[] { "@model" + CharacterAfterModel }, StringSplitOptions.None)[1].Split(Model)[0];

                        AspxText = AspxText.Replace("@model" + CharacterAfterModel + BetweenText + Model, "");

                        break;
                    }
                }
            }

            if (Model != "")
            {
                // Get Model Constructor Method
                if (Model.Contains("("))
                {
                    ModelConstructor = Model.GetTextAfterValue("(").GetTextBeforeLastValue(")");
                    Model = Model.GetTextBeforeValue("(");
                }

                if (!Model.ClassPathIsFine())
                {
                    ErrorList.Add("Error: Model class path is not fine in " + AspxFilePath + " file");
                    return;
                }
            }


            // Set Layout
            string Layout = "";

            TmpAspxText = AspxText.GetTextBeforeValue("<");

            while (TmpAspxText.Contains("@layout"))
            {
                TmpAspxText = TmpAspxText.GetTextAfterValue("@layout");

                if (TmpAspxText.Length > 1)
                {
                    char CharacterAfterLayout = TmpAspxText[0];

                    if (CharacterAfterLayout == ' ' || CharacterAfterLayout == '\n' || CharacterAfterLayout == '\t' || CharacterAfterLayout == '\r')
                    {
                        TmpAspxText = ft.FullTrimInStart(TmpAspxText) + "\\";

                        if (TmpAspxText[0] != '\"')
                            break;

                        for (int i = 1; i < TmpAspxText.Length; i++)
                        {
                            if (TmpAspxText[i] == '\"')
                                break;

                            Layout += TmpAspxText[i];
                        }

                        string BetweenText = AspxText.Split(new string[] { "@layout" + CharacterAfterLayout }, StringSplitOptions.None)[1].Split(Layout)[0];

                        AspxText = AspxText.Replace("@layout" + CharacterAfterLayout + BetweenText + Layout + '\"', "");

                        break;
                    }
                }
            }

            if (Layout != "")
            {
                string LayoutPath = "";

                if (Layout[0] == '/' || Layout[0].ToString() == @"\")
                    LayoutPath = RootDirectoryPath + @"\" + Layout;
                else
                    LayoutPath = FilePath.GetTextBeforeLastValue(@"\") + @"\" + Layout;

                if (!Path.HasExtension(LayoutPath))
                {
                    Layout += ".aspx";
                    LayoutPath += ".aspx";
                }

                if (Path.GetExtension(LayoutPath) != ".aspx")
                {
                    ErrorList.Add("Error: Layout extension is not valid in " + AspxFilePath + " file");
                    return;
                }

                if (!File.Exists(LayoutPath))
                {
                    ErrorList.Add("Error: Layout file is not exist in " + LayoutPath + " path");
                    return;
                }
            }


            // Set If Is Layout
            bool IsLayout = false;

            TmpAspxText = AspxText.GetTextBeforeValue("<");

            while (TmpAspxText.Contains("@islayout"))
            {
                TmpAspxText = TmpAspxText.GetTextAfterValue("@islayout");

                if (TmpAspxText.Length > 1)
                {
                    char CharacterAfterIsLayout = TmpAspxText[0];

                    if (CharacterAfterIsLayout == ' ' || CharacterAfterIsLayout == '\n' || CharacterAfterIsLayout == '\t' || CharacterAfterIsLayout == '\r')
                    {
                        AspxText = AspxText.Replace("@islayout" + CharacterAfterIsLayout, CharacterAfterIsLayout.ToString());
                        IsLayout = true;

                        break;
                    }
                }
            }
            
            
            // Set Break
            bool IsBreak = false;

            TmpAspxText = AspxText.GetTextBeforeValue("<");

            while (TmpAspxText.Contains("@break"))
            {
                TmpAspxText = TmpAspxText.GetTextAfterValue("@break");

                if (TmpAspxText.Length > 1)
                {
                    char CharacterAfterBreak = TmpAspxText[0];

                    if (CharacterAfterBreak == ' ' || CharacterAfterBreak == '\n' || CharacterAfterBreak == '\t' || CharacterAfterBreak == '\r')
                    {
                        AspxText = AspxText.Replace("@break" + CharacterAfterBreak, CharacterAfterBreak.ToString());
                        IsBreak = true;

                        break;
                    }
                }
            }

            // Set Global Template
            AspxText = GlobalTemplate + AspxText;

            // Set Template
            string Template = "";

            TmpAspxText = AspxText.GetTextBeforeValue("<");

            while (TmpAspxText.Contains("@template"))
            {
                TmpAspxText = TmpAspxText.GetTextAfterValue("@template");

                if (TmpAspxText.Length > 1)
                {
                    char CharacterAfterTemplate = TmpAspxText[0];

                    if (CharacterAfterTemplate == ' ' || CharacterAfterTemplate == '\n' || CharacterAfterTemplate == '\t' || CharacterAfterTemplate == '\r')
                    {
                        TmpAspxText = ft.FullTrimInStart(TmpAspxText) + "\\";

                        if (TmpAspxText[0] != '\"')
                            break;

                        for (int i = 1; i < TmpAspxText.Length; i++)
                        {
                            if (TmpAspxText[i] == '\"')
                                break;

                            Template += TmpAspxText[i];
                        }

                        string BetweenText = AspxText.Split(new string[] { "@template" + CharacterAfterTemplate }, StringSplitOptions.None)[1].Split(Template)[0];

                        AspxText = AspxText.Replace("@template" + CharacterAfterTemplate + BetweenText + Template + '\"', "");

                        break;
                    }
                }
            }

            if (Template != "")
            {
                string[] Templates = Template.Split(';');

                foreach (string TmpTemplate in Templates)
                {
                    Template = TmpTemplate;

                    string TemplatePath = "";

                    if (Template[0] == '/' || Template[0].ToString() == @"\")
                        TemplatePath = RootDirectoryPath + @"\" + Template;
                    else
                        TemplatePath = FilePath.GetTextBeforeLastValue(@"\") + @"\" + Template;

                    if (!Path.HasExtension(TemplatePath))
                        TemplatePath += ".astx";

                    if (Path.GetExtension(TemplatePath) != ".astx")
                    {
                        ErrorList.Add("Error: Template extension is not valid in " + AspxFilePath + " file");
                        return;
                    }

                    if (!File.Exists(TemplatePath))
                    {
                        ErrorList.Add("Error: Template file is not exist in " + TemplatePath + " path");
                        return;
                    }

                    string AstxText = "";
                    var Lines = File.OpenText(TemplatePath);
                    var TmpLine = "";
                    while ((TmpLine = Lines.ReadLine()) != null)
                    {
                        AstxText += TmpLine + '\n';
                    }

                    AspxText = AstxText + AspxText;
                }
            }

            // Fetch Template
            TmpAspxText = AspxText;
            while (TmpAspxText.Contains("@#"))
            {
                TmpAspxText = "@#" + TmpAspxText.GetTextAfterValue("@#");

                if (TmpAspxText.Length < 1)
                    break;

                string PartName = "";
                char CharacterAfterTemplatePartName = '\0';

                for (int i = 2; i < TmpAspxText.Length; i++)
                {
                    if (char.IsLetter(TmpAspxText[i]) || char.IsNumber(TmpAspxText[i]))
                        PartName += TmpAspxText[i];
                    else
                    {
                        CharacterAfterTemplatePartName = TmpAspxText[i];
                        break;
                    }
                }

                if (string.IsNullOrEmpty(PartName))
                {
                    ErrorList.Add("Error: Name not set after Index @# for code combination in " + AspxFilePath + " file");
                    break;
                }

                bool PartHasTemplate = (CharacterAfterTemplatePartName == '{');

                CodeBehindFetchRazorSyntex syntex1 = new CodeBehindFetchRazorSyntex();
                string InnerText = "";
                    
                if (PartHasTemplate)
                    InnerText = syntex1.FetchSyntexWithEndedCharacterText("@" + TmpAspxText.Remove(0, PartName.Length + 2));
                else
                    InnerText = syntex1.FetchExpressions(TmpAspxText.Remove(1, 1)); // Remove Sharp (#)

                if (PartHasTemplate)
                {
                    string TmpInnerTextForReplace = InnerText;
                    bool HasPartName = false;

                    if (AspxText.Contains("@#" + PartName + "="))
                    {
                        CodeBehindFetchRazorSyntex syntex2 = new CodeBehindFetchRazorSyntex();
                        string ReturnedInnerText = syntex2.FetchSyntexWithEndedCharacterText("@" + AspxText.GetTextAfterValue("@#" + PartName + "="));

                        // Contains Part Name
                        HasPartName = false;
                        string TmpInnerText = InnerText;
                        while (TmpInnerText.Contains("@#" + PartName))
                        {
                            TmpInnerText = TmpInnerText.GetTextAfterValue("@#" + PartName) + '\\';

                            char TmpCharacter = TmpInnerText[0];
                            if (!char.IsLetter(TmpCharacter) && !char.IsNumber(TmpCharacter))
                            {
                                HasPartName = true;
                                break;
                            }
                        }

                        if (HasPartName)
                            AspxText = AspxText.Replace("@#" + PartName + "{" + InnerText + "}", "");


                        // Check Exist @#Template In @#Template={*}
                        HasPartName = false;
                        string TmpReturnedInnerText = ReturnedInnerText;
                        while (TmpReturnedInnerText.Contains("@#" + PartName))
                        {
                            TmpReturnedInnerText = TmpReturnedInnerText.GetTextAfterValue("@#" + PartName) + '\\';

                            char TmpCharacter = TmpReturnedInnerText[0];
                            if (!char.IsLetter(TmpCharacter) && !char.IsNumber(TmpCharacter))
                            {
                                HasPartName = true;
                                break;
                            }
                        }

                        if (!HasPartName)
                        {
                            string TmpReturnedInnerText2 = TmpInnerTextForReplace;
                            while (TmpReturnedInnerText2.Contains("@#" + PartName))
                            {
                                TmpReturnedInnerText2 = TmpReturnedInnerText2.GetTextAfterValue("@#" + PartName);

                                if (TmpReturnedInnerText2.Length > 0)
                                {
                                    char TmpCharacter = TmpReturnedInnerText2[0];
                                    if (!char.IsLetter(TmpCharacter) && !char.IsNumber(TmpCharacter))
                                        TmpInnerTextForReplace = TmpInnerTextForReplace.Replace("@#" + PartName + TmpCharacter, ReturnedInnerText + TmpCharacter);
                                }
                                else
                                    TmpInnerTextForReplace = TmpInnerTextForReplace.Replace("@#" + PartName, ReturnedInnerText);
                            }
                        }

                        AspxText = AspxText.Replace("@#" + PartName + "={" + ReturnedInnerText + "}", TmpInnerTextForReplace);
                    }

                    string InnerTextValue = TmpInnerTextForReplace;

                    HasPartName = false;
                    string TmpInnerTextValue1 = InnerTextValue;
                    while (TmpInnerTextValue1.Contains("@#" + PartName))
                    {
                        TmpInnerTextValue1 = TmpInnerTextValue1.GetTextAfterValue("@#" + PartName) + '\\';

                        char TmpCharacter = TmpInnerTextValue1[0];
                        if (!char.IsLetter(TmpCharacter) && !char.IsNumber(TmpCharacter))
                        {
                            HasPartName = true;
                            break;
                        }
                    }

                    if (!HasPartName)
                    {
                        string TmpAspxText2 = AspxText;
                        while (TmpAspxText2.Contains("@#" + PartName))
                        {
                            TmpAspxText2 = TmpAspxText2.GetTextAfterValue("@#" + PartName);

                            if (TmpAspxText2.Length > 0)
                            {
                                char TmpCharacter = TmpAspxText2[0];
                                if (!char.IsLetter(TmpCharacter) && !char.IsNumber(TmpCharacter) && TmpCharacter != '{' && TmpCharacter != '=')
                                {
                                    AspxText = AspxText.Replace('{' + "@#" + PartName + '}', InnerTextValue.Replace("\"", @"\" + "\"").Replace('\n'.ToString(), @"\" + "n"));
                                    AspxText = AspxText.Replace("@#" + PartName + TmpCharacter, InnerTextValue + TmpCharacter);
                                }
                            }
                            else
                            {
                                AspxText = AspxText.Replace('{' + "@#" + PartName + '}', InnerTextValue.Replace("\"", @"\" + "\"").Replace('\n'.ToString(), @"\" + "n"));
                                AspxText = AspxText.Replace("@#" + PartName, InnerTextValue);
                            }
                        }
                    }

                    if (PartHasTemplate)
                    {
                        var regex = new Regex(Regex.Escape("@#" + PartName + ((CharacterAfterTemplatePartName == '=') ? "=" : "") + "{" + InnerText + "}"));
                        AspxText = regex.Replace(AspxText, "", 1);
                    }
                    else
                    {
                        var regex = new Regex(Regex.Escape("@#" + PartName + CharacterAfterTemplatePartName));
                        AspxText = regex.Replace(AspxText, CharacterAfterTemplatePartName.ToString(), 1);
                    }
                }

                if (InnerText.Length > 0)
                {
                    int TemplateLength = 0;

                    if (PartHasTemplate)
                    {
                        if (CharacterAfterTemplatePartName == '=')
                            TemplateLength = InnerText.Length + PartName.Length + 5;

                        if (CharacterAfterTemplatePartName == '{')
                            TemplateLength = InnerText.Length + PartName.Length + 4;
                    }
                    else
                        TemplateLength = PartName.Length + 2;

                    TmpAspxText = TmpAspxText.Remove(0, TemplateLength);
                }
                else
                    if (PartHasTemplate)
                        TmpAspxText = TmpAspxText.Replace("@#" + PartName + ((CharacterAfterTemplatePartName == '=') ? "=" : "") + "{" + InnerText + "}", "");
                    else
                        TmpAspxText = TmpAspxText.Replace("@#" + PartName + CharacterAfterTemplatePartName, CharacterAfterTemplatePartName.ToString());
            }


            // Set Trim Option
            if (StartTrimInAspxFile)
                AspxText = ft.FullTrimInStart(AspxText);
            if (EndTrimInAspxFile)
                AspxText = ft.FullTrimInEnd(AspxText);
            if (InnerTrimInAspxFile)
                AspxText = AspxText.Replace('\n' + "@{", "@{");


            // Clean Razor Comments
            TmpAspxText = AspxText;
            while (TmpAspxText.Contains("@*"))
            {
                TmpAspxText = TmpAspxText.GetTextAfterValue("@*");

                if (!TmpAspxText.Contains("*@"))
                {
                    // ErrorList.Add("Error: Index @* not closed @* for code combination in " + AspxFilePath + " file");
                    break;
                }

                string Comment = AspxText.Split(new string[] { "@*" }, StringSplitOptions.None)[1].Split("*@")[0];

                AspxText = AspxText.Replace(Comment, "");
            }


            // Remove First Empty Line
            bool RemoveFirstEmptyLine = false;
            if (AspxText.Length > 1 && StartTrimInAspxFile)
                if (AspxText[0] == '@')
                    RemoveFirstEmptyLine = true;


            string TextToCodeCombination = "";
            bool HasElseIf = false;

            string TextForWrite = "";
            for (int i = 0; i < AspxText.Length; i++)
            {
                if (AspxText[i] == '@')
                {
                    // Set Remove First Empty Line
                    if (RemoveFirstEmptyLine && (i > 0))
                    {
                        if (TextForWrite.Length > 1)
                            if (TextForWrite[0] == '\n')
                                TextForWrite = TextForWrite.Remove(0, 1);

                        RemoveFirstEmptyLine = false;
                    }

                    TextToCodeCombination += GetWriteText(TextForWrite, !ControllerIsSet);
                    TextForWrite = "";

                    if ((i + 1) < AspxText.Length)
                    {
                        // Escape Email Address
                        if ((i > 0) && (AspxText[i + 1] != '('))
                        {
                            if ((char.IsLetter(AspxText[i - 1]) || char.IsNumber(AspxText[i - 1])) && (char.IsLetter(AspxText[i + 1]) || char.IsNumber(AspxText[i + 1])))
                            {
                                TextForWrite += "@";
                                continue;
                            }
                        }

                        // Escape Symbol For Double AtSign (@@)
                        if (AspxText[i + 1] == '@')
                        {
                            TextForWrite += "@";
                            i++;
                            continue;
                        }

                        if (AspxText[i + 1] == '(')
                        {
                            TextToCodeCombination += GetWriteCode(syntex.FetchSyntexWithEndedCharacter(AspxText.Substring(i)), !ControllerIsSet);

                            i += syntex.RazorIndexLength - 1;
                            continue;
                        }

                        if (char.IsLetter(AspxText[i + 1]))
                        {
                            if (i + 5 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 2) == "if" && (AspxText[i + 3] == ' ' || AspxText[i + 3] == '(' || AspxText[i + 3] == '\n' || AspxText[i + 3] == '\t' || AspxText[i + 3] == '\r'))
                                {
                                    i += 1;

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '(')
                                            continue;

                                        TextToCodeCombination += GetAddCode((HasElseIf? "else " : "") + "if (" + syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i)) + ")", !ControllerIsSet);
                                        HasElseIf = false;

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '{')
                                            continue;

                                        TextToCodeCombination += GetAddCode("{", !ControllerIsSet);

                                        syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i));

                                        foreach (NameValue nv in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (nv.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(nv.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(nv.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(nv.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 3;

                                        break;
                                    }

                                    string TmpAspxTextForFindElse = ft.FullTrimInStart(AspxText.Substring(i + 1));

                                    if (TmpAspxTextForFindElse.Length < 4)
                                        continue;

                                    if (TmpAspxTextForFindElse.Substring(0, 4) != "else")
                                        continue;


                                    int ElseIndex = i - 1;
                                    while (ElseIndex + 1 < AspxText.Length)
                                    {
                                        if (AspxText[++ElseIndex] != 'e')
                                            continue;

                                        if (ElseIndex + 7 < AspxText.Length)
                                            if (AspxText.Substring(ElseIndex, 4) == "else" && (AspxText[ElseIndex + 4] == ' ' || AspxText[ElseIndex + 4] == '\n' || AspxText[ElseIndex + 4] == '\t' || AspxText[ElseIndex + 4] == '\r'))
                                            {
                                                i = ElseIndex;

                                                int ElseIfIndex = i + 4;
                                                for (; (ElseIfIndex + 2) < AspxText.Length; ElseIfIndex++)
                                                {
                                                    if (AspxText[ElseIfIndex] == 'i' && AspxText[ElseIfIndex + 1] == 'f' && (AspxText[ElseIfIndex + 2] == ' ' || AspxText[ElseIfIndex + 2] == '(' || AspxText[ElseIfIndex + 2] == '\n' || AspxText[ElseIfIndex + 2] == '\t' || AspxText[ElseIfIndex + 2] == '\r'))
                                                    {
                                                        HasElseIf = true;
                                                        break;
                                                    }
                                                }

                                                if (HasElseIf)
                                                {
                                                    AspxText = AspxText.Insert(ElseIfIndex, "@");
                                                    i += 4;
                                                    break;
                                                }


                                                i += 3;

                                                TextToCodeCombination += GetAddCode("else", !ControllerIsSet);

                                                while (i < AspxText.Length)
                                                {
                                                    if (AspxText[++i] != '{')
                                                        continue;

                                                    TextToCodeCombination += GetAddCode("{", !ControllerIsSet);

                                                    syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i));

                                                    foreach (NameValue nv in syntex.AddedTextForEndedCharacter.GetList())
                                                    {
                                                        switch (nv.Name)
                                                        {
                                                            case "add_code": TextToCodeCombination += GetAddCode(nv.Value, !ControllerIsSet); break;
                                                            case "write_code": TextToCodeCombination += GetWriteCode(nv.Value, !ControllerIsSet, true); break;
                                                            case "write_text": TextToCodeCombination += GetWriteText(nv.Value, !ControllerIsSet, true); break;
                                                        }
                                                    }

                                                    TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                                    i += syntex.RazorIndexLength - 3;

                                                    break;
                                                }
                                            }
                                        break;
                                    }

                                    continue;
                                }

                            // Do While Detection
                            if (i + 5 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 2) == "do" && (AspxText[i + 3] == ' ' || AspxText[i + 3] == '\n'|| AspxText[i + 3] == '\t'||AspxText[i + 3] == '\r'))
                                {
                                    i += 1;

                                    TextToCodeCombination += GetAddCode("do", !ControllerIsSet);

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '{')
                                            continue;

                                        TextToCodeCombination += GetAddCode("{", !ControllerIsSet);

                                        syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i));

                                        foreach (NameValue nv in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (nv.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(nv.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(nv.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(nv.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 3;

                                        break;
                                    }

                                    while (i + 1 < AspxText.Length)
                                    {
                                        bool BreakWhile = false;

                                        if (AspxText[++i] != 'w')
                                            continue;

                                        if (i + 8 < AspxText.Length)
                                            if (AspxText.Substring(i, 5) == "while" && (AspxText[i + 5] == ' ' || AspxText[i + 5] == '(' || AspxText[i + 5] == '\n' || AspxText[i + 5] == '\t' || AspxText[i + 5] == '\r'))
                                            {
                                                i += 5;

                                                while (i < AspxText.Length)
                                                {
                                                    if (AspxText[++i] != '(')
                                                        continue;

                                                    TextToCodeCombination += GetAddCode("while (" + syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i)) + ");", !ControllerIsSet);

                                                    i += syntex.RazorIndexLength - 1;

                                                    BreakWhile = true;
                                                    break;
                                                }
                                            }

                                        if (BreakWhile)
                                            break;
                                    }

                                    continue;
                                }

                            // For Detection
                            if (i + 6 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 3) == "for" && (AspxText[i + 4] == ' ' || AspxText[i + 4] == '(' || AspxText[i + 4] == '\n' || AspxText[i + 4] == '\t' || AspxText[i + 4] == '\r'))
                                {
                                    i += 2;

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '(')
                                            continue;

                                        TextToCodeCombination += GetAddCode("for (" + syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i)) + ")", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '{')
                                            continue;

                                        TextToCodeCombination += GetAddCode("{", !ControllerIsSet);

                                        syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i));

                                        foreach (NameValue nv in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (nv.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(nv.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(nv.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(nv.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 3;

                                        break;
                                    }

                                    continue;
                                }

                            // Lock Detection
                            if (i + 7 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 4) == "lock" && (AspxText[i + 5] == ' ' || AspxText[i + 5] == '(' || AspxText[i + 5] == '\n' || AspxText[i + 5] == '\t' || AspxText[i + 5] == '\r'))
                                {
                                    i += 3;

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '(')
                                            continue;

                                        TextToCodeCombination += GetAddCode("lock (" + syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i)) + ")", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '{')
                                            continue;

                                        TextToCodeCombination += GetAddCode("{", !ControllerIsSet);

                                        syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i));

                                        foreach (NameValue nv in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (nv.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(nv.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(nv.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(nv.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 3;

                                        break;
                                    }

                                    continue;
                                }

                            // While Detection
                            if (i + 8 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 5) == "while" && (AspxText[i + 6] == ' ' || AspxText[i + 6] == '(' || AspxText[i + 6] == '\n' || AspxText[i + 6] == '\t' || AspxText[i + 6] == '\r'))
                                {
                                    i += 4;

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '(')
                                            continue;

                                        TextToCodeCombination += GetAddCode("while (" + syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i)) + ")", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '{')
                                            continue;

                                        TextToCodeCombination += GetAddCode("{", !ControllerIsSet);

                                        syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i));

                                        foreach (NameValue nv in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (nv.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(nv.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(nv.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(nv.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 3;

                                        break;
                                    }

                                    continue;
                                }

                            // Using Detection
                            if (i + 8 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 5) == "using" && (AspxText[i + 6] == ' ' || AspxText[i + 6] == '(' || AspxText[i + 6] == '\n' || AspxText[i + 6] == '\t' || AspxText[i + 6] == '\r'))
                                {
                                    i += 4;

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '(')
                                            continue;

                                        TextToCodeCombination += GetAddCode("using (" + syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i)) + ")", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '{')
                                            continue;

                                        TextToCodeCombination += GetAddCode("{", !ControllerIsSet);

                                        syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i));

                                        foreach (NameValue nv in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (nv.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(nv.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(nv.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(nv.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 3;

                                        break;
                                    }

                                    continue;
                                }

                            // Switch Detection
                            if (i + 9 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 6) == "switch" && (AspxText[i + 7] == ' ' || AspxText[i + 7] == '(' || AspxText[i + 7] == '\n' || AspxText[i + 7] == '\t' || AspxText[i + 7] == '\r'))
                                {
                                    i += 5;

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '(')
                                            continue;

                                        TextToCodeCombination += GetAddCode("switch (" + syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i)) + ")", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '{')
                                            continue;

                                        TextToCodeCombination += GetAddCode("{", !ControllerIsSet);

                                        syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i));

                                        foreach (NameValue nv in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (nv.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(nv.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(nv.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(nv.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 3;

                                        break;
                                    }

                                    continue;
                                }

                            // Foreach Detection
                            if (i + 10 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 7) == "foreach" && (AspxText[i + 8] == ' ' || AspxText[i + 8] == '(' || AspxText[i + 8] == '\n' || AspxText[i + 8] == '\t' || AspxText[i + 8] == '\r'))
                                {
                                    i += 6;

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '(')
                                            continue;

                                        TextToCodeCombination += GetAddCode("foreach (" + syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i)) + ")", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '{')
                                            continue;

                                        TextToCodeCombination += GetAddCode("{", !ControllerIsSet);

                                        syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i));

                                        foreach (NameValue nv in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (nv.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(nv.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(nv.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(nv.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 3;

                                        break;
                                    }

                                    continue;
                                }

                            // Fetch Expressions
                            TextToCodeCombination += GetWriteCode(syntex.FetchExpressions(AspxText.Substring(i)), !ControllerIsSet);

                            i += syntex.ExpressionsIndexLength - 1;
                            continue;
                        }

                        if (AspxText[i + 1] == '{')
                        {
                            syntex.FetchSyntexWithEndedCharacter(AspxText.Substring(i));

                            foreach (NameValue nv in syntex.AddedTextForEndedCharacter.GetList())
                            {
                                switch (nv.Name)
                                {
                                    case "add_code": TextToCodeCombination += GetAddCode(nv.Value, !ControllerIsSet); break;
                                    case "write_code": TextToCodeCombination += GetWriteCode(nv.Value, !ControllerIsSet); break;
                                    case "write_text": TextToCodeCombination += GetWriteText(nv.Value, !ControllerIsSet); break;
                                }
                            }

                            i += syntex.RazorIndexLength - 2;
                            continue;
                        }
                    }
                }
                else
                    TextForWrite += AspxText[i];
            }

            // Set Remove First Empty Line
            if (RemoveFirstEmptyLine)
            {
                if (TextForWrite.Length > 0)
                    if (TextForWrite[0] == ('\n'))
                        TextForWrite = TextForWrite.Remove(0, 1);

                RemoveFirstEmptyLine = false;
            }

            TextToCodeCombination += GetWriteText(TextForWrite, !ControllerIsSet);

            SetMethod(AspxFilePath, Controller, ControllerConstructor, Model, ModelConstructor, ControllerIsSet, Layout, IsLayout, IsBreak, MethodIndexer, TextToCodeCombination);
        }

        public string GetWriteText(string Text, bool PageIsOnlyView, bool IsInsideControl = false)
        {
            if (Text.Length > 0)
            {
                Text = Text.Replace("\\", "\\\\");

                Text = Text.Replace("\"", @"\" + "\"");

                Text = Text.Replace('\n'.ToString(), "\\" + "n");

                string TmpTab = (IsInsideControl) ? "    " : "";

                if (!PageIsOnlyView)
                    return TmpTab + "                controller.ResponseText += \"" + Text + "\";" + Environment.NewLine;
                else
                    return TmpTab + "            ReturnValue += \"" + Text + "\";" + Environment.NewLine;
            }
            else
                return "";
        }

        public string GetWriteCode(string Code, bool PageIsOnlyView, bool IsInsideControl = false)
        {
            string TmpTab = (IsInsideControl) ? "    " : "";

            if (!PageIsOnlyView)
                return TmpTab + "                controller.ResponseText += " + Code + ";" + Environment.NewLine;
            else
                return TmpTab + "            ReturnValue += " + Code + ";" + Environment.NewLine;
        }

        public string GetAddCode(string Code, bool PageIsOnlyView)
        {
            if (!PageIsOnlyView)
                return "                " + Code.Replace('\n'.ToString(), Environment.NewLine + "            ") + Environment.NewLine;
            else
                return "            " + Code.Replace('\n'.ToString(), Environment.NewLine + "            ") + Environment.NewLine;
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

        private void FillGlobalTemplate()
        {
            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            const string FilePath = "code_behind/global_template.astx";

            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
                return;
            }

            var Lines = File.OpenText(FilePath);
            var TmpLine = "";
            while ((TmpLine = Lines.ReadLine()) != null)
            {
                GlobalTemplate += TmpLine + '\n';
            }
        }

        private void MoveViewFromWwwroot(string ViewPath, string Extension)
        {
            if (!Directory.Exists("wwwroot"))
                return;

            DirectoryInfo WwwrootDir = new DirectoryInfo("wwwroot");

            if (!Directory.Exists(Path.GetFullPath(ViewPath)))
                Directory.CreateDirectory(Path.GetFullPath(ViewPath));

            foreach (FileInfo file in WwwrootDir.GetFiles("*." + Extension, SearchOption.AllDirectories))
            {
                string ParrentDirectories = file.FullName.GetTextAfterValue(Path.GetFullPath("wwwroot")).GetTextBeforeLastValue(@"\" + file.Name);

                if (!Directory.Exists(Path.GetFullPath(ViewPath) + ParrentDirectories))
                    Directory.CreateDirectory(Path.GetFullPath(ViewPath) + ParrentDirectories);

                File.Move(file.FullName, Path.GetFullPath(ViewPath) + ParrentDirectories + @"\" + file.Name, true);
            }
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

                file.Write("[CodeBehind namespace import list]" + Environment.NewLine);
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

        public string GetTemplatePartName(string Text)
        {
            string ReturnValue = "";

            if (Text.Length < 1)
                return ReturnValue;


            for (int i = 0; i < Text.Length; i++)
                if (char.IsLetter(Text[i]) || char.IsNumber(Text[i]))
                    ReturnValue += Text[i];
                else
                    break;

            return ReturnValue;
        }

        public char GetCharacterAfterTemplatePartName(string Text)
        {
            if (Text.Length < 1)
                return '!';

            int i = 0;
            for (; i < Text.Length; i++)
                if (!char.IsLetter(Text[i]) && !char.IsNumber(Text[i]))
                    return Text[i];

            return Text[i - 1];
        }

        public bool PartHasTemplateValue(string Text)
        {
            if (Text.Length < 1)
                return false;

            for (int i = 0; i < Text.Length; i++)
                if (!char.IsLetter(Text[i]) && !char.IsNumber(Text[i]))
                {
                    if (Text[i] == ' ' || Text[i] == '<' || Text[i] == '\n' || Text[i] == '\t' || Text[i] == '\r')
                        return true;

                    break;
                }

            return false;
        }

        public bool HasOpenedSyntax(string InnerText, string OpenSyntaxValue, string CloseSyntaxValue)
        {
            if ((InnerText.Length < OpenSyntaxValue.Length) || (InnerText.Length < CloseSyntaxValue.Length))
                return false;


            int OpenSyntaxCount = InnerText.Split(new string[] { OpenSyntaxValue }, StringSplitOptions.None).Length - 1;
            int CloseSyntaxCount = InnerText.Split(new string[] { CloseSyntaxValue }, StringSplitOptions.None).Length - 1;

            return OpenSyntaxCount > CloseSyntaxCount;
        }
    }
}
