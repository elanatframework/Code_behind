using System.Text.RegularExpressions;
using System.Reflection;

namespace SetCodeBehind
{
    class CodeBehindLibraryCreator
    {
        public string GetCodeBehindViews()
        {
            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            string AllAspxFiles = ""; 

            string FilePath = "code_behind/views_class.cs.tmp";
            if (!File.Exists(FilePath))
            {
                AllAspxFiles = CreateAllAspxFiles();

                string[] lines = AllAspxFiles.Split(System.Environment.NewLine);

                // Create views_class.cs File
                using (StreamWriter writer = File.CreateText(FilePath))
                {
                    foreach (string line in lines)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            else
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        AllAspxFiles += line + System.Environment.NewLine;
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
                        AllAspxFiles += line + System.Environment.NewLine;
                    }
                }
            }

            return AllAspxFiles;
        }

        public string CreateAllAspxFiles()
        {
            string CodeBehindViews = "";
            CodeBehindViews += "using " + Assembly.GetEntryAssembly().GetName().Name + ";" + System.Environment.NewLine;
            CodeBehindViews += "using CodeBehind;" + System.Environment.NewLine;
            CodeBehindViews += "using System;" + System.Environment.NewLine;
            CodeBehindViews += "using System.Runtime;" + System.Environment.NewLine;
            CodeBehindViews += "using Microsoft.AspNetCore.Http;" + System.Environment.NewLine;
            CodeBehindViews += "namespace CodeBehindViews" + System.Environment.NewLine;
            CodeBehindViews += "{" + System.Environment.NewLine;
            CodeBehindViews += "    public class CodeBehindViewsList" + System.Environment.NewLine;
            CodeBehindViews += "    {" + System.Environment.NewLine;

            string CaseCodeTemplateValue = "";
            string MethodCodeTemplateValue = "";


            List<string> ErrorList = new List<string>();

            DirectoryInfo RootDir = new DirectoryInfo("wwwroot");
            int i = 0;
            foreach (FileInfo file in RootDir.GetFiles("*.aspx", SearchOption.AllDirectories))
            {
                i++;
                var Lines = File.OpenText(file.FullName);
                string AspxText = "";
                var TmpLine = "";
                while ((TmpLine = Lines.ReadLine()) != null)
                {
                    AspxText += TmpLine + @"\n";
                }

                AspxText = AspxText.GetTextBeforeLastValue(@"\n");

                Lines.Close();

                string AspxFilePath = file.FullName.GetTextAfterValue(RootDir.FullName);
                string FilePathToMethodName = AspxFilePath.ToMethodNameClean();

                if (!AspxText.Contains("<%@"))
                {
                    ErrorList.Add("Error: Index <%@ not exist in " + AspxFilePath + " file");
                    continue;
                }

                string PageProperties = " " + AspxText.Split(new string[] { "<%@" }, StringSplitOptions.None)[1].Split("%>")[0] + " ";

                if (!PageProperties.Contains(" Page "))
                {
                    ErrorList.Add("Error: Page not exist after index <%@ in " + AspxFilePath + " file");
                    continue;
                }

                if (!PageProperties.Contains(" Controller=\""))
                {
                    ErrorList.Add("Error: Controller not exist or is not well allocated after index <%@ in " + AspxFilePath + " file");
                    continue;
                }

                string Controller = PageProperties.Split(new string[] { "Controller=\"" }, StringSplitOptions.None)[1].Split("\"")[0];

                if (!Controller.ClassPathIsFine())
                {
                    ErrorList.Add("Error: Controller class path is not fine in " + AspxFilePath + " file");
                    continue;
                }

                string Model = (PageProperties.Contains(" Model=\"")) ? PageProperties.Split(new string[] { "Model=\"" }, StringSplitOptions.None)[1].Split("\"")[0] : "";

                if(Model != "")
                if(!Model.ClassPathIsFine())
                {
                    ErrorList.Add("Error: Model class path is not fine in " + AspxFilePath + " file");
                    continue;
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

                // Code Combination
                while (AspxText.Contains("<%"))
                {
                    TextToCodeCombination += GetWriteText(AspxText.GetTextBeforeValue("<%"));
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
                            TextToCodeCombination += GetWriteCode(InnerText.Remove(0, 1));
                        }
                        else
                            TextToCodeCombination += GetAddCode(InnerText);

                    var regex = new Regex(Regex.Escape("<%" + InnerText + "%>"));
                    AspxText = regex.Replace(AspxText, "", 1);
                }


                TextToCodeCombination += GetWriteText(AspxText);


                CaseCodeTemplateValue += "                case \"" + AspxFilePath.Replace("\\", "/") + "\": return " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + i + "(context);" + System.Environment.NewLine;

                MethodCodeTemplateValue += System.Environment.NewLine;
                MethodCodeTemplateValue += "        protected string " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + i + "(HttpContext context)" + System.Environment.NewLine;
                MethodCodeTemplateValue += "        {" + System.Environment.NewLine;
                MethodCodeTemplateValue += "            " + Controller + " CurrentController = new " + Controller + "();" + System.Environment.NewLine;
                MethodCodeTemplateValue += "            CurrentController.PageLoad(context);" + System.Environment.NewLine;

                MethodCodeTemplateValue += "            if (!CurrentController.IgnoreViewAndModel)" + System.Environment.NewLine;
                MethodCodeTemplateValue += "            {" + System.Environment.NewLine;

                if (!string.IsNullOrEmpty(Model))
                {
                    MethodCodeTemplateValue += "                " + Model + " model = (" + Model + ")CurrentController.CodeBehindModel;" + System.Environment.NewLine;
                    MethodCodeTemplateValue += "                CurrentController.ResponseText += model.ResponseText;" + System.Environment.NewLine;
                }

                MethodCodeTemplateValue += TextToCodeCombination + System.Environment.NewLine;
                MethodCodeTemplateValue += "            }" + System.Environment.NewLine;

                MethodCodeTemplateValue += "            return CurrentController.ResponseText;" + System.Environment.NewLine;
                MethodCodeTemplateValue += "        }" + System.Environment.NewLine;
            }

            CodeBehindViews += "        public string SetPageLoadByPath(string path, HttpContext context)" + System.Environment.NewLine;
            CodeBehindViews += "        {" + System.Environment.NewLine;
            CodeBehindViews += "            switch (path)" + System.Environment.NewLine;
            CodeBehindViews += "            {" + System.Environment.NewLine;
            CodeBehindViews += CaseCodeTemplateValue + System.Environment.NewLine;
            CodeBehindViews += "            }" + System.Environment.NewLine;
            CodeBehindViews += "            return \"\";" + System.Environment.NewLine;
            CodeBehindViews += "        }" + System.Environment.NewLine;
            CodeBehindViews += MethodCodeTemplateValue + System.Environment.NewLine;

            CodeBehindViews += "    }" + System.Environment.NewLine;
            CodeBehindViews += "}" + System.Environment.NewLine;


            SaveError(ErrorList);
            return CodeBehindViews;
        }

        public static string GetWriteText(string Text)
        {
            if (Text.Length > 0)
                return "                CurrentController.ResponseText += \"" + Text.Replace("\"", @"\" + "\"") + "\";" + System.Environment.NewLine;
            else
                return "";
        }

        public static string GetWriteCode(string Code)
        {
            return "                CurrentController.ResponseText += " + Code + ";" + System.Environment.NewLine;
        }

        public static string GetAddCode(string Code)
        {
            return "                " + Code.Replace(@"\n", System.Environment.NewLine + "            ") + System.Environment.NewLine;
        }

        private void SaveError(List<string> ErrorList)
        {
            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            // Create views_error.log File
            if (ErrorList.Count > 0)
            {
                const string FilePath = "code_behind/views_class_aggregation_error.log";

                using (StreamWriter writer = File.CreateText(FilePath))
                {
                    writer.WriteLine("date_and_time:" + DateTime.Now.ToString());

                    foreach (string line in ErrorList)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }
    }
}
