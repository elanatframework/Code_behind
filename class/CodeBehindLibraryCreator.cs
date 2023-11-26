using System.Text.RegularExpressions;
using System.Reflection;
using CodeBehind;
using CodeBehind.HtmlData;

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

                string[] lines = AllAspxFiles.Split(Environment.NewLine);

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
            CodeBehindViews += "    {" + Environment.NewLine;

            CodeBehindOptions options = new CodeBehindOptions();
            RewriteAspxFileToDirectory = options.RewriteAspxFileToDirectory;
            AccessAspxFileAfterRewrite = options.AccessAspxFileAfterRewrite;
            IgnoreDefaultAfterRewrite = options.IgnoreDefaultAfterRewrite;
            StartTrimInAspxFile = options.StartTrimInAspxFile;
            EndTrimInAspxFile = options.EndTrimInAspxFile;
            InnerTrimInAspxFile = options.InnerTrimInAspxFile;


            // Move View From Wwwroot
            if ((options.ViewPath != "wwwroot") && options.MoveViewFromWwwroot)
                MoveViewFromWwwroot(options.ViewPath);


            DirectoryInfo RootDir = new DirectoryInfo(options.ViewPath);
            string RootDirectoryPath = RootDir.FullName;
            int i = 1;
            foreach (FileInfo file in RootDir.GetFiles("*.aspx", SearchOption.AllDirectories))
            {
                AspxTextAndCodeCombination(file.FullName, RootDirectoryPath, i);
                i++;
            }

            CodeBehindViews += "        public string SetPageLoadByPath(string path, HttpContext context)" + Environment.NewLine;
            CodeBehindViews += "        {" + Environment.NewLine;
            CodeBehindViews += "            switch (path)" + Environment.NewLine;
            CodeBehindViews += "            {" + Environment.NewLine;
            CodeBehindViews += CaseCodeTemplateValue + Environment.NewLine;
            CodeBehindViews += "            }" + Environment.NewLine;
            CodeBehindViews += "            return \"\";" + Environment.NewLine;
            CodeBehindViews += "        }" + Environment.NewLine;
            CodeBehindViews += MethodCodeTemplateValue + Environment.NewLine;

            CodeBehindViews += "    }" + Environment.NewLine;
            CodeBehindViews += "}" + Environment.NewLine + Environment.NewLine;

            CodeBehindViews += "namespace " + Assembly.GetEntryAssembly().GetName().Name + "{public partial class CodeBehindEmptyClass{}}";


            SaveError(ErrorList);
            return CodeBehindViews;
        }

        private List<string> ErrorList = new List<string>();
        private bool RewriteAspxFileToDirectory;
        private bool AccessAspxFileAfterRewrite;
        private bool IgnoreDefaultAfterRewrite;
        private bool StartTrimInAspxFile;
        private bool EndTrimInAspxFile;
        private bool InnerTrimInAspxFile;
        private string CaseCodeTemplateValue = "";
        private string MethodCodeTemplateValue = "";
        public void AspxTextAndCodeCombination(string FilePath, string RootDirectoryPath, int MethodIndexer)
        {
            var Lines = File.OpenText(FilePath);
            string AspxText = "";
            var TmpLine = "";
            while ((TmpLine = Lines.ReadLine()) != null)
                AspxText += TmpLine + @"\n";

            AspxText = AspxText.GetTextBeforeLastValue(@"\n");

            Lines.Close();

            // Fetch Page
            if (AspxText.Contains("<%@"))
                AspxTextAndCodeCombinationStandard(AspxText, FilePath, RootDirectoryPath, MethodIndexer);
            else
                AspxTextAndCodeCombinationRazor(AspxText, FilePath, RootDirectoryPath, MethodIndexer);
        }

        public void SetMethod(string AspxFilePath, string Controller, string ControllerConstructor, string Model, string ModelConstructor, bool ControllerIsSet, int MethodIndexer, string TextToCodeCombination)
        {
            string FilePathToMethodName = AspxFilePath.ToMethodNameClean();
            bool PageIsOnlyView = !ControllerIsSet;

            if (RewriteAspxFileToDirectory)
                if (!IgnoreDefaultAfterRewrite || (AspxFilePath.GetTextAfterLastValue("/") != "Default.aspx"))
                    CaseCodeTemplateValue += "                case \"" + AspxFilePath.Replace("\\", "/").GetTextBeforeLastValue(".aspx") + "/Default.aspx" + "\": return " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(context);" + Environment.NewLine;
                else
                    CaseCodeTemplateValue += "                case \"" + AspxFilePath.Replace("\\", "/") + "\": return " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(context);" + Environment.NewLine;

            if (!RewriteAspxFileToDirectory || (RewriteAspxFileToDirectory && AccessAspxFileAfterRewrite))
                CaseCodeTemplateValue += "                case \"" + AspxFilePath.Replace("\\", "/") + "\": return " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(context);" + Environment.NewLine;

            string TmpMethodCodeTemplateValue = Environment.NewLine;
            TmpMethodCodeTemplateValue += "        protected string " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(HttpContext context)" + Environment.NewLine;
            TmpMethodCodeTemplateValue += "        {" + Environment.NewLine;

            if (!PageIsOnlyView)
            {
                TmpMethodCodeTemplateValue += "            " + Controller + " CurrentController = new " + Controller + "();" + Environment.NewLine;

                if (!string.IsNullOrEmpty(ControllerConstructor))
                    TmpMethodCodeTemplateValue += "            CurrentController.CodeBehindConstructor(" + ModelConstructor + ");" + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            CurrentController.PageLoad(context);" + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            if (!CurrentController.IgnoreViewAndModel)" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            {" + Environment.NewLine;

                if (!string.IsNullOrEmpty(Model))
                {
                    TmpMethodCodeTemplateValue += "                " + Model + " model = (" + Model + ")CurrentController.CodeBehindModel;" + Environment.NewLine;

                    if (!string.IsNullOrEmpty(ModelConstructor))
                        TmpMethodCodeTemplateValue += "                model.CodeBehindConstructor(" + ModelConstructor + ");" + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "                CurrentController.ResponseText += model.ResponseText;" + Environment.NewLine;
                }

                TmpMethodCodeTemplateValue += TextToCodeCombination;
                TmpMethodCodeTemplateValue += "            }" + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            return CurrentController.ResponseText;" + Environment.NewLine;
            }
            else
            {
                TmpMethodCodeTemplateValue += "            string ReturnValue = \"\";" + Environment.NewLine;

                if (!string.IsNullOrEmpty(Model))
                {
                    TmpMethodCodeTemplateValue += "            " + Model + " model = new " + Model + "();" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            ReturnValue += model.ResponseText;" + Environment.NewLine;
                }

                TmpMethodCodeTemplateValue += TextToCodeCombination;

                TmpMethodCodeTemplateValue += "            return ReturnValue;" + Environment.NewLine;
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
                if ((PageProperties.GetNumberOfCharacter('=') == 1) && (PageProperties.Contains(" Model=\"") || PageProperties.Contains(" Template=\"")))
                    PageIsOnlyView = true;

            if (!PageIsOnlyView)
                if ((PageProperties.GetNumberOfCharacter('=') == 2) && (PageProperties.Contains(" Model=\"") && PageProperties.Contains(" Template=\"")))
                    PageIsOnlyView = true;


            // Set Controller
            string Controller = "";
            string ControllerConstructor = "";
            string ModelConstructor = "";

            if (!PageIsOnlyView)
            {
                if (!PageProperties.Contains(" Controller=\""))
                {
                    ErrorList.Add("Error: Controller not exist or is not well allocated after index <%@ in " + AspxFilePath + " file");
                    return;
                }

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


            // Set Template
            string Template = (PageProperties.Contains(" Template=\"")) ? PageProperties.Split(new string[] { "Template=\"" }, StringSplitOptions.None)[1].Split("\"")[0] : "";

            if (Template != "")
            {
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
                var Lines2 = File.OpenText(TemplatePath);
                var TmpLine2 = "";
                while ((TmpLine2 = Lines2.ReadLine()) != null)
                {
                    AstxText += TmpLine2 + @"\n";
                }

                AspxText += AstxText;
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
                        {
                            char SecondCharacterAfterTemplatePartName = TmpInnerTextForReplace.GetTextAfterValue(CharacterAfterTemplatePartName.ToString())[0];

                            if ((CharacterAfterTemplatePartName == '\\') && ((SecondCharacterAfterTemplatePartName == 'n') || (SecondCharacterAfterTemplatePartName == 't') || (SecondCharacterAfterTemplatePartName == 'r')))
                            {

                                TmpInnerTextValue = TmpInnerTextForReplace.GetTextAfterValue(CharacterAfterTemplatePartName.ToString() + SecondCharacterAfterTemplatePartName.ToString());
                            }
                            else
                                TmpInnerTextValue = TmpInnerTextForReplace.GetTextAfterValue(CharacterAfterTemplatePartName.ToString());
                        }

                        AspxText = AspxText.Replace("<#" + ReturnedInnerText + "#>", TmpInnerTextValue);
                    }

                    string InnerTextValue;
                    if (CharacterAfterTemplatePartName == '<')
                        InnerTextValue = TmpInnerTextForReplace.GetTextAfterValue(PartName);
                    else
                    {
                        char SecondCharacterAfterTemplatePartName = TmpInnerTextForReplace.GetTextAfterValue(CharacterAfterTemplatePartName.ToString())[0];

                        if ((CharacterAfterTemplatePartName == '\\') && ((SecondCharacterAfterTemplatePartName == 'n') || (SecondCharacterAfterTemplatePartName == 't') || (SecondCharacterAfterTemplatePartName == 'r')))
                        {
                            InnerTextValue = TmpInnerTextForReplace.GetTextAfterValue(CharacterAfterTemplatePartName.ToString() + SecondCharacterAfterTemplatePartName.ToString());
                        }
                        else
                            InnerTextValue = TmpInnerTextForReplace.GetTextAfterValue(CharacterAfterTemplatePartName.ToString());
                    }

                    if (!InnerTextValue.Contains("<#" + PartName + "#>"))
                        AspxText = AspxText.Replace("<#" + PartName + "#>", InnerTextValue);

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
                AspxText = ft.FullTrimInStartOverBackslash(AspxText);
            if (EndTrimInAspxFile)
                AspxText = ft.FullTrimInEndOverBackslash(AspxText);
            if (InnerTrimInAspxFile)
                AspxText = AspxText.Replace('\\' + "n<%", "<%");


            // Code Combination
            while (AspxText.Contains("<%"))
            {
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

            SetMethod(AspxFilePath, Controller, ControllerConstructor, Model, ModelConstructor, !PageIsOnlyView, MethodIndexer, TextToCodeCombination);
        }

        public void AspxTextAndCodeCombinationRazor(string AspxText, string FilePath, string RootDirectoryPath, int MethodIndexer)
        {
            string AspxFilePath = FilePath.GetTextAfterValue(RootDirectoryPath);

            CodeBehindFetchRazorSyntex syntex = new CodeBehindFetchRazorSyntex();

            FullTrim ft = new FullTrim();
            AspxText = ft.FullTrimInStartOverBackslash(AspxText);

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

                    if (CharacterAfterController == ' ' || CharacterAfterController == '\\')
                    {
                        TmpAspxText = "@" + ft.FullTrimInStartOverBackslash(TmpAspxText);

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

                    if (CharacterAfterModel == ' ' || CharacterAfterModel == '\\')
                    {
                        TmpAspxText = "@" + ft.FullTrimInStartOverBackslash(TmpAspxText);

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


            // Set Template
            string Template = "";

            TmpAspxText = AspxText;

            while (TmpAspxText.Contains("@template"))
            {
                TmpAspxText = TmpAspxText.GetTextAfterValue("@template");

                if (TmpAspxText.Length > 1)
                {
                    char CharacterAfterTemplate = TmpAspxText[0];

                    if (CharacterAfterTemplate == ' ' || CharacterAfterTemplate == '\\')
                    {
                        TmpAspxText = ft.FullTrimInStartOverBackslash(TmpAspxText) + "\\";

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
                var Lines2 = File.OpenText(TemplatePath);
                var TmpLine2 = "";
                while ((TmpLine2 = Lines2.ReadLine()) != null)
                {
                    AstxText += TmpLine2 + @"\n";
                }

                AspxText += AstxText;
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

                // bool PartHasTemplate = (CharacterAfterTemplatePartName == '=' || CharacterAfterTemplatePartName == '{');
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
                                    AspxText = AspxText.Replace("@#" + PartName + TmpCharacter, InnerTextValue + TmpCharacter);
                            }
                            else
                                AspxText = AspxText.Replace("@#" + PartName, InnerTextValue);
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
                AspxText = ft.FullTrimInStartOverBackslash(AspxText);
            if (EndTrimInAspxFile)
                AspxText = ft.FullTrimInEndOverBackslash(AspxText);
            if (InnerTrimInAspxFile)
                AspxText = AspxText.Replace('\\' + "n@{", "@{");


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


            string TextToCodeCombination = "";
            bool HasElseIf = false;

            string TextForWrite = "";
            for (int i = 0; i < AspxText.Length; i++)
            {
                if (AspxText[i] == '@')
                {
                    TextToCodeCombination += GetWriteText(TextForWrite, !ControllerIsSet);
                    TextForWrite = "";

                    if ((i + 1) < AspxText.Length)
                    {
                        // Escape Email Address
                        if ((i > 0) && (AspxText[i + 1] != '('))
                        {
                            if ((char.IsLetter(AspxText[i - 1]) || char.IsNumber(AspxText[i - 1])) && (char.IsLetter(AspxText[i + 1]) || char.IsNumber(AspxText[i + 1])))
                            {
                                if (i < 2)
                                {
                                    TextForWrite += "@";
                                    continue;
                                }

                                if (AspxText[i - 1] != 'n' && AspxText[i - 1] != 'r' && AspxText[i - 1] != 't')
                                {
                                    TextForWrite += "@";
                                    continue;
                                }
                                else if (AspxText[i - 2] != '\\')
                                {
                                    TextForWrite += "@";
                                    continue;
                                }
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

                            i += syntex.RazorIndexLength;
                            continue;
                        }

                        if (char.IsLetter(AspxText[i + 1]))
                        {
                            if (i + 5 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 2) == "if" && (AspxText[i + 3] == ' ' ||AspxText[i + 3] == '(' ||AspxText[i + 3] == '\\'))
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

                                        foreach (CodeBehind.HtmlData.Attribute atr in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (atr.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(atr.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(atr.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(atr.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    string TmpAspxTextForFindElse = ft.FullTrimInStartOverBackslash(AspxText.Substring(i - 1));

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
                                            if (AspxText.Substring(ElseIndex, 4) == "else" && (AspxText[ElseIndex + 4] == ' ' || AspxText[ElseIndex + 4] == '\\'))
                                            {
                                                i = ElseIndex;

                                                int ElseIfIndex = i + 4;
                                                for (; (ElseIfIndex + 2) < AspxText.Length; ElseIfIndex++)
                                                {
                                                    if (AspxText[ElseIfIndex] == 'i' && AspxText[ElseIfIndex + 1] == 'f' && (AspxText[ElseIfIndex + 2] == ' ' || AspxText[ElseIfIndex + 2] == '(' || AspxText[ElseIfIndex + 2] == '\\'))
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

                                                    foreach (CodeBehind.HtmlData.Attribute atr in syntex.AddedTextForEndedCharacter.GetList())
                                                    {
                                                        switch (atr.Name)
                                                        {
                                                            case "add_code": TextToCodeCombination += GetAddCode(atr.Value, !ControllerIsSet); break;
                                                            case "write_code": TextToCodeCombination += GetWriteCode(atr.Value, !ControllerIsSet, true); break;
                                                            case "write_text": TextToCodeCombination += GetWriteText(atr.Value, !ControllerIsSet, true); break;
                                                        }
                                                    }

                                                    TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                                    i += syntex.RazorIndexLength - 1;

                                                    break;
                                                }
                                            }
                                        break;
                                    }

                                    continue;
                                }

                            // Do While Detection
                            if (i + 5 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 2) == "do" && (AspxText[i + 3] == ' ' ||AspxText[i + 3] == '\\'))
                                {
                                    i += 1;

                                    TextToCodeCombination += GetAddCode("do", !ControllerIsSet);

                                    while (i < AspxText.Length)
                                    {
                                        if (AspxText[++i] != '{')
                                            continue;

                                        TextToCodeCombination += GetAddCode("{", !ControllerIsSet);

                                        syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i));

                                        foreach (CodeBehind.HtmlData.Attribute atr in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (atr.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(atr.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(atr.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(atr.Value, !ControllerIsSet, true); break;
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
                                            if (AspxText.Substring(i, 5) == "while" && (AspxText[i + 5] == ' ' || AspxText[i + 5] == '(' || AspxText[i + 5] == '\\'))
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
                                if (AspxText.Substring(i + 1, 3) == "for" && (AspxText[i + 4] == ' ' || AspxText[i + 4] == '(' || AspxText[i + 4] == '\\'))
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

                                        foreach (CodeBehind.HtmlData.Attribute atr in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (atr.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(atr.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(atr.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(atr.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    continue;
                                }

                            // Lock Detection
                            if (i + 7 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 4) == "lock" && (AspxText[i + 5] == ' ' || AspxText[i + 5] == '(' || AspxText[i + 5] == '\\'))
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

                                        foreach (CodeBehind.HtmlData.Attribute atr in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (atr.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(atr.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(atr.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(atr.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    continue;
                                }

                            // While Detection
                            if (i + 8 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 5) == "while" && (AspxText[i + 6] == ' ' || AspxText[i + 6] == '(' || AspxText[i + 6] == '\\'))
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

                                        foreach (CodeBehind.HtmlData.Attribute atr in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (atr.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(atr.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(atr.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(atr.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    continue;
                                }

                            // Using Detection
                            if (i + 8 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 5) == "using" && (AspxText[i + 6] == ' ' || AspxText[i + 6] == '(' || AspxText[i + 6] == '\\'))
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

                                        foreach (CodeBehind.HtmlData.Attribute atr in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (atr.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(atr.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(atr.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(atr.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    continue;
                                }

                            // Switch Detection
                            if (i + 9 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 6) == "switch" && (AspxText[i + 7] == ' ' || AspxText[i + 7] == '(' || AspxText[i + 7] == '\\'))
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

                                        foreach (CodeBehind.HtmlData.Attribute atr in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (atr.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(atr.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(atr.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(atr.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

                                        break;
                                    }

                                    continue;
                                }

                            // Foreach Detection
                            if (i + 10 < AspxText.Length)
                                if (AspxText.Substring(i + 1, 7) == "foreach" && (AspxText[i + 8] == ' ' || AspxText[i + 8] == '(' || AspxText[i + 8] == '\\'))
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

                                        foreach (CodeBehind.HtmlData.Attribute atr in syntex.AddedTextForEndedCharacter.GetList())
                                        {
                                            switch (atr.Name)
                                            {
                                                case "add_code": TextToCodeCombination += GetAddCode(atr.Value, !ControllerIsSet); break;
                                                case "write_code": TextToCodeCombination += GetWriteCode(atr.Value, !ControllerIsSet, true); break;
                                                case "write_text": TextToCodeCombination += GetWriteText(atr.Value, !ControllerIsSet, true); break;
                                            }
                                        }

                                        TextToCodeCombination += GetAddCode("}", !ControllerIsSet);

                                        i += syntex.RazorIndexLength - 1;

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

                            foreach (CodeBehind.HtmlData.Attribute atr in syntex.AddedTextForEndedCharacter.GetList())
                            {
                                switch (atr.Name)
                                {
                                    case "add_code": TextToCodeCombination += GetAddCode(atr.Value, !ControllerIsSet); break;
                                    case "write_code": TextToCodeCombination += GetWriteCode(atr.Value, !ControllerIsSet); break;
                                    case "write_text": TextToCodeCombination += GetWriteText(atr.Value, !ControllerIsSet); break;
                                }
                            }

                            i += syntex.RazorIndexLength;
                            continue;
                        }
                    }
                }
                else
                    TextForWrite += AspxText[i];
            }

            TextToCodeCombination += GetWriteText(TextForWrite, !ControllerIsSet);

            SetMethod(AspxFilePath, Controller, ControllerConstructor, Model, ModelConstructor, ControllerIsSet, MethodIndexer, TextToCodeCombination);
        }

        public string GetWriteText(string Text, bool PageIsOnlyView, bool IsInsideControl = false)
        {
            if (Text.Length > 0)
            {
                string TmpTab = (IsInsideControl) ? "    " : "";

                if (!PageIsOnlyView)
                    return TmpTab + "                CurrentController.ResponseText += \"" + Text.Replace("\"", @"\" + "\"") + "\";" + Environment.NewLine;
                else
                    return TmpTab + "            ReturnValue += \"" + Text.Replace("\"", @"\" + "\"") + "\";" + Environment.NewLine;
            }
            else
                return "";
        }

        public string GetWriteCode(string Code, bool PageIsOnlyView, bool IsInsideControl = false)
        {
            string TmpTab = (IsInsideControl) ? "    " : "";

            if (!PageIsOnlyView)
                return TmpTab + "                CurrentController.ResponseText += " + Code + ";" + Environment.NewLine;
            else
                return TmpTab + "            ReturnValue += " + Code + ";" + Environment.NewLine;
        }

        public string GetAddCode(string Code, bool PageIsOnlyView)
        {
            if (!PageIsOnlyView)
                return "                " + Code.Replace(@"\n", Environment.NewLine + "            ") + Environment.NewLine;
            else
                return "            " + Code.Replace(@"\n", Environment.NewLine + "            ") + Environment.NewLine;
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

        private void MoveViewFromWwwroot(string ViewPath)
        {
            DirectoryInfo WwwrootDir = new DirectoryInfo("wwwroot");

            if (!Directory.Exists(Path.GetFullPath(ViewPath)))
                Directory.CreateDirectory(Path.GetFullPath(ViewPath));

            foreach (FileInfo file in WwwrootDir.GetFiles("*.aspx|*.astx", SearchOption.AllDirectories))
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
                using (StreamWriter writer = File.CreateText(NamespaceImportListPath))
                {
                    writer.Write("[CodeBehind namespace import list]" + Environment.NewLine);
                    writer.Write("namespace=System.IO" + Environment.NewLine);
                    writer.Write("namespace=System.Collections" + Environment.NewLine);
                    writer.Write("namespace=System.Collections.Generic" + Environment.NewLine);
                    writer.Write("namespace=System.Linq" + Environment.NewLine);
                    writer.Write("namespace=System.Threading" + Environment.NewLine);
                    writer.Write("namespace=System.Threading.Tasks");
                }
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
                if (char.IsLetter(Text[i]))
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
                    if (Text[i] == ' ' || Text[i] == '<')
                        return true;

                    if (Text[i] == '/' && (i + 1 < Text.Length))
                        if (Text[i + 1] == 'n' || Text[i + 1] == 't' || Text[i + 1] == 'r')
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
