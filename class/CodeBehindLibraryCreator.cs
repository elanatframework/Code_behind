using System;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.RegularExpressions;
using CodeBehind;
using Microsoft.CodeAnalysis;
using static System.Net.Mime.MediaTypeNames;

namespace SetCodeBehind
{
    class CodeBehindLibraryCreator
    {
        private List<string> ErrorList = new List<string>();
        private string CaseCodeTemplateValue = "";
        private string SectionTemplateValue = "";
        private string CaseCodeTemplateValueForFullPath = "";
        private string CaseCodeTemplateValueForFullPathWithModel = "";
        private string MethodCodeTemplateValue = "";

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
            CodeBehindViews += "        private string CallerViewPath { get; set; } = \"\";" + Environment.NewLine;
            CodeBehindViews += "        private string CallerViewDirectoryPath { get; set; } = \"\";" + Environment.NewLine;
            CodeBehindViews += "        private bool FoundPage { get; set; } = true;" + Environment.NewLine;
            CodeBehindViews += "        private bool FoundController { get; set; } = true;" + Environment.NewLine + Environment.NewLine;

            CodeBehindOptions options = new CodeBehindOptions();

            // Create wwwroot Directory And Set Default Pages
            if (options.ViewPath == "wwwroot" && !Directory.Exists("wwwroot"))
                new DefaultPages().Set();


            // Move View From Wwwroot
            if ((options.ViewPath != "wwwroot") && options.MoveViewFromWwwroot)
            {
                MoveViewFromWwwroot(options.ViewPath, "aspx");
                MoveViewFromWwwroot(options.ViewPath, "astx");

                if (options.ConvertCsHtmlToAspx)
                    MoveViewFromWwwroot(options.ViewPath, "cshtml");
            }

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

                combination.Set(file.FullName, RootDirectoryPath, i);

                lock (EmptyObjectForLock)
                {
                    CaseCodeTemplateValue += combination.CaseCodeTemplateValue;
                    SectionTemplateValue += combination.SectionTemplateValue;
                    CaseCodeTemplateValueForFullPath += combination.CaseCodeTemplateValueForFullPath;
                    CaseCodeTemplateValueForFullPathWithModel += combination.CaseCodeTemplateValueForFullPathWithModel;
                    MethodCodeTemplateValue += combination.MethodCodeTemplateValue;

                    i++;
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

                    combination.Set(file.FullName, RootDirectoryPath, i);

                    lock (EmptyObjectForLock)
                    {
                        CaseCodeTemplateValue += combination.CaseCodeTemplateValue;
                        SectionTemplateValue += combination.SectionTemplateValue;
                        CaseCodeTemplateValueForFullPath += combination.CaseCodeTemplateValueForFullPath;
                        CaseCodeTemplateValueForFullPathWithModel += combination.CaseCodeTemplateValueForFullPathWithModel;
                        MethodCodeTemplateValue += combination.MethodCodeTemplateValue;

                        i++;
                    }
                });

            CodeBehindViews += "        // It Works Based On Rewriting The Option File" + Environment.NewLine;
            CodeBehindViews += "        public string SetPageLoadByPath(string path, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            RequestPath = path;" + Environment.NewLine;
            CodeBehindViews += "            FoundPage = true;" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += SectionTemplateValue + "/*{SectionTemplateValue}*/" + Environment.NewLine;
            CodeBehindViews += "            switch (path)" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += CaseCodeTemplateValue + Environment.NewLine + "/*{CaseCodeTemplateValue}*/" + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            FoundPage = false;" + Environment.NewLine;
            CodeBehindViews += "            return \"\";" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Load All Page By Full Path, This Method Load Break Page And Does Not Apply Rewrite" + Environment.NewLine;
            CodeBehindViews += "        public string SetPageLoadByFullPath(string path, HttpContext context, string PageReturnValue = \"\")" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
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
            CodeBehindViews += "        public string SetPageLoadByFullPathWithModel(string path, HttpContext context, string PageReturnValue = \"\", object model = null)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
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
            CodeBehindViews += "        private string LoadPage(string path, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return SetPageLoadByFullPath(path, context);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Overload" + Environment.NewLine;
            CodeBehindViews += "        private string LoadPage(string path, object ModelClass, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return SetPageLoadByFullPathWithModel(path, context, \"\", ModelClass);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Overload" + Environment.NewLine;
            CodeBehindViews += "        private string LoadPage(string path, object ModelClass)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return SetPageLoadByFullPathWithModel(path, null, \"\", ModelClass);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        // Overload" + Environment.NewLine;
            CodeBehindViews += "        private string LoadPage(string path)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return SetPageLoadByFullPath(path, null);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public string RunController(HttpContext context, string ViewPath, object ModelClass, CodeBehind.HtmlData.NameValueCollection ViewData, string DownloadFilePath)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            if (!string.IsNullOrEmpty(DownloadFilePath))" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += "                Download(context, DownloadFilePath);" + Environment.NewLine;
            CodeBehindViews += "                return \"\";" + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            ViewData.AddList(ViewData.GetList());" + Environment.NewLine + Environment.NewLine;
            CodeBehindViews += "            if (ModelClass != null)" + Environment.NewLine;
            CodeBehindViews += "                return LoadPage(ViewPath, ModelClass, context);" + Environment.NewLine;
            CodeBehindViews += "            else" + Environment.NewLine;
            CodeBehindViews += "                return LoadPage(ViewPath, context);" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public string RunControllerName(string ControllerClass, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            switch (ControllerClass)" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += FillControllerNameCase();
            CodeBehindViews += "/*{CaseCodeTemplateValueForControllerName}*/" + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine;
            CodeBehindViews += Environment.NewLine;
            CodeBehindViews += "            FoundController = false;" + Environment.NewLine;
            CodeBehindViews += "            return \"\";" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public bool PageHasFound()" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return FoundPage;" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "        public bool ControllerHasFound()" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            return FoundController;" + Environment.NewLine;
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

        private string FillAssemblyControllerCase(Assembly assembly, string EntryAssemblyName)
        {
            Type ControllerType = typeof(CodeBehindController);
            var AssemblyClasses = assembly.GetTypes().Where(type => ControllerType.IsAssignableFrom(type) && !type.IsAbstract);

            string ReturnValue = "";

            foreach (var TmpClass in AssemblyClasses)
            {
                string NameSpace = (string.IsNullOrEmpty(TmpClass.Namespace) || (TmpClass.Namespace == EntryAssemblyName)) ? "" : TmpClass.Namespace + ".";
                string ClassName = "Tmp" + TmpClass.Namespace + "_" + TmpClass.Name;

                ReturnValue += "                case \"" + TmpClass.Name + "\":" + Environment.NewLine;
                ReturnValue += "                " + NameSpace + TmpClass.Name + " " + ClassName + " = new " + NameSpace + TmpClass.Name + "();" + Environment.NewLine;
                ReturnValue += "                " + ClassName + ".FillSection(context);" + Environment.NewLine;
                ReturnValue += "                " + ClassName + ".PageLoad(context);" + Environment.NewLine;

                ReturnValue += Environment.NewLine;
                ReturnValue += "                if (" + ClassName + ".IgnoreViewAndModel)" + Environment.NewLine;
                ReturnValue += "                    return " + ClassName + ".ResponseText;" + Environment.NewLine + Environment.NewLine;
                ReturnValue += "                return " + ClassName + ".ResponseText + RunController(context, " + ClassName + ".ViewPath, " + ClassName + ".CodeBehindModel, " + ClassName + ".ViewData, " + ClassName + ".DownloadFilePath); " + Environment.NewLine + Environment.NewLine;
            }

            return ReturnValue;
        }

        private string FillDllBinAssemblyControllerCase(string EntryAssemblyName)
        {
            if (!Directory.Exists("wwwroot/bin"))
                return "";

            string ReturnValue = "";

            DirectoryInfo BinDir = new DirectoryInfo("wwwroot/bin");
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

            return FillAssemblyControllerCase(assembly, AssemblyCleanName) + FillDllBinAssemblyControllerCase(AssemblyCleanName);
        }

        private string SetCleanNameForClass(string CleanName)
        {
            Regex regex = new Regex("[^a-zA-Z0-9_]");
            CleanName = regex.Replace(CleanName, "_");

            if (char.IsNumber(CleanName[0]))
                CleanName = '_' + CleanName;

            return CleanName;
        }
    }
}
