using CodeBehind;
using CodeBehind.HtmlData;
using System.Text.RegularExpressions;

namespace SetCodeBehind
{
    public class ViewCodeCombination
    {
        public List<string> ErrorList = new List<string>();
        public bool RewriteAspxFileToDirectory;
        public bool AccessAspxFileAfterRewrite;
        public bool IgnoreDefaultAfterRewrite;
        public bool StartTrimInAspxFile;
        public bool EndTrimInAspxFile;
        public bool SetBreakForLayoutPage;
        public bool InnerTrimInAspxFile;
        public string CaseCodeTemplateValue = "";
        public string SectionTemplateValue = "";
        public string CaseCodeTemplateValueForFullPath = "";
        public string CaseCodeTemplateValueForFullPathWithModel = "";
        public string MethodCodeTemplateValue = "";
        public string GlobalTemplate = "";

        public void Set(string FilePath, string RootDirectoryPath, int MethodIndexer)
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
            FullTrim ft = new FullTrim();
            if (ft.FullTrimInStart(AspxText).StartsWith("<%@"))
                AspxTextAndCodeCombinationStandard(AspxText, FilePath, RootDirectoryPath, MethodIndexer);
            else
                AspxTextAndCodeCombinationRazor(AspxText, FilePath, RootDirectoryPath, MethodIndexer);
        }

        public void AspxTextAndCodeCombinationStandard(string AspxText, string FilePath, string RootDirectoryPath, int MethodIndexer)
        {
            string AspxFilePath = FilePath.GetTextAfterValue(RootDirectoryPath);

            if (!AspxText.Contains("<%@"))
            {
                ErrorList.Add("Error: Index <%@ not exist in " + AspxFilePath + " file");
                return;
            }

            string PageProperties = AspxText.Split(new string[] { "<%@" }, StringSplitOptions.None)[1].Split("%>")[0] + " ";

            FullTrim ft = new FullTrim();
            PageProperties = " " + ft.FullTrimInStart(PageProperties);

            if (PageProperties.Length < 5)
            {
                ErrorList.Add("Error: Page not exist after index <%@ in " + AspxFilePath + " file");
                return;
            }

            if (PageProperties.Substring(0, 5) != " Page" && PageProperties.Substring(0, 5) != " page")
            {
                ErrorList.Add("Error: Page not exist after index <%@ in " + AspxFilePath + " file");
                return;
            }

            // Support Lowercase
            PageProperties = " Page" + PageProperties.Remove(0, 5);
            PageProperties = PageProperties.Replace(" controller=\"", " Controller=\"");
            PageProperties = PageProperties.Replace(" model=\"", " Model=\"");
            PageProperties = PageProperties.Replace(" layout=\"", " Layout=\"");
            PageProperties = PageProperties.Replace(" islayout=\"", " IsLayout=\"");
            PageProperties = PageProperties.Replace(" break=\"", " Break=\"");
            PageProperties = PageProperties.Replace(" section=\"", " Section=\"");
            PageProperties = PageProperties.Replace(" template=\"", " Template=\"");


            bool PageIsOnlyView = (PageProperties.Trim() == "Page");

            if (!PageIsOnlyView)
                if (!PageProperties.Contains(" Controller=\""))
                    PageIsOnlyView = true;


            // Set Controller
            string Controller = "";
            string ControllerConstructor = "";

            if (!PageIsOnlyView)
            {
                Controller = PageProperties.Split(new string[] { "Controller=\"" }, StringSplitOptions.None)[1].Split("\"")[0];

                // Get Controller Constructor Method
                if (Controller.Contains("("))
                {
                    ControllerConstructor = "(" + Controller.GetTextAfterValue("(").GetTextBeforeLastValue(")").Replace("&quot;", "\"") + ")";
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
            string ModelConstructor = "";
            bool ModelUseAbstract = true;

            if (Model != "")
            {
                if (Model.Length > 1)
                    if ((Model[0] == '{') && (Model[Model.Length - 1] == '}'))
                    {
                        Model = Model.Remove(0, 1);
                        Model = Model.Remove(Model.Length - 1, 1);
                        ModelUseAbstract = false;
                    }

                // Get Model Constructor Method
                if (Model.Contains("("))
                {
                    ModelConstructor = "(" + Model.GetTextAfterValue("(").GetTextBeforeLastValue(")").Replace("&quot;", "\"") + ")";
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


            // Set Section
            string Section = (PageProperties.Contains(" Section=\"")) ? PageProperties.Split(new string[] { "Section=\"" }, StringSplitOptions.None)[1].Split("\"")[0] : "";
            bool UseSection = (Section == "true");


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

            SetMethod(AspxFilePath, Controller, ControllerConstructor, Model, ModelConstructor, ModelUseAbstract, !PageIsOnlyView, Layout, IsLayout, IsBreak, UseSection, MethodIndexer, TextToCodeCombination);
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


            // Fetch Page Attribute
            string Controller = "";
            string Model = "";
            string Layout = "";
            bool IsLayout = false;
            bool IsBreak = false;
            bool UseSection = false;
            string Template = "";

            string[] AspxLine = AspxText.Split('\n');
            foreach (string line in AspxLine)
            {
                string TmpLine = ft.FullTrimAll(line);

                if (TmpLine.Length == 0)
                    continue;

                if (TmpLine[0] != '@')
                    break;

                // Fetch Controller, Model, Layout, IsLayout, IsBreak, UseSection, Template
                if (TmpLine.StartsWith("@controller"))
                {
                    Controller = ft.FullTrimAll(TmpLine.GetTextAfterValue("@controller"));
                    AspxText = AspxText.GetTextAfterValue(line);
                    continue;
                }
                else if (TmpLine.StartsWith("@model"))
                {
                    Model = ft.FullTrimAll(TmpLine.GetTextAfterValue("@model"));
                    AspxText = AspxText.GetTextAfterValue(line);
                    continue;
                }
                else if (TmpLine.StartsWith("@layout"))
                {
                    Layout = ft.FullTrimAll(TmpLine.GetTextAfterValue("@layout"));
                    AspxText = AspxText.GetTextAfterValue(line);
                    continue;
                }
                else if (TmpLine == "@islayout")
                {
                    IsLayout = true;
                    AspxText = AspxText.GetTextAfterValue(line);
                    continue;
                }
                else if (TmpLine == "@break")
                {
                    IsBreak = true;
                    AspxText = AspxText.GetTextAfterValue(line);
                    continue;
                }
                else if (TmpLine == "@section")
                {
                    UseSection = true;
                    AspxText = AspxText.GetTextAfterValue(line);
                    continue;
                }
                else if (TmpLine.StartsWith("@template"))
                {
                    Template = ft.FullTrimAll(TmpLine.GetTextAfterValue("@template"));
                    AspxText = AspxText.GetTextAfterValue(line);
                    continue;
                }
                else
                    break;
            }


            // Set Controller
            string ControllerConstructor = "";
            bool ControllerIsSet = false;
            if (Controller != "")
            {
                // Get Controller Constructor Method
                if (Controller.Contains("("))
                {
                    ControllerConstructor = "(" + Controller.GetTextAfterValue("(").GetTextBeforeLastValue(")") + ")";
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
            bool ModelUseAbstract = true;

            if (Model != "")
            {
                if (Model.Length > 1)
                    if ((Model[0] == '{') && (Model[Model.Length - 1] == '}'))
                    {
                        Model = Model.Remove(0, 1);
                        Model = Model.Remove(Model.Length - 1, 1);
                        ModelUseAbstract = false;
                    }

                // Get Model Constructor Method
                if (Model.Contains("("))
                {
                    ModelConstructor = "(" + Model.GetTextAfterValue("(").GetTextBeforeLastValue(")") + ")";
                    Model = Model.GetTextBeforeValue("(");
                }

                if (!Model.ClassPathIsFine())
                {
                    ErrorList.Add("Error: Model class path is not fine in " + AspxFilePath + " file");
                    return;
                }
            }


            // Set Layout
            if (Layout != "")
            {
                if (Layout.Length < 2)
                {
                    ErrorList.Add("Error: The layout content is not specified correctly in " + AspxFilePath + " path");
                    return;
                }

                if (!Layout.StartsWith('"'))
                {
                    ErrorList.Add("Error: The layout content value does not start with double quotes in " + AspxFilePath + " file");
                    return;
                }

                if (!Layout.EndsWith('"'))
                {
                    ErrorList.Add("Error: The layout content value does not end with double quotes in " + AspxFilePath + " file");
                    return;
                }

                Layout = Layout.Remove(0, 1);
                Layout = Layout.Remove(Layout.Length - 1, 1);

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

            // Set Global Template
            AspxText = GlobalTemplate + AspxText;

            // Set Template
            if (Template != "")
            {
                if (Template.Length < 2)
                {
                    ErrorList.Add("Error: The template content is not specified correctly in " + AspxFilePath + " path");
                    return;
                }

                if (!Template.StartsWith('"'))
                {
                    ErrorList.Add("Error: The template content value does not start with double quotes in " + AspxFilePath + " file");
                    return;
                }

                if (!Template.EndsWith('"'))
                {
                    ErrorList.Add("Error: The template content value does not end with double quotes in " + AspxFilePath + " file");
                    return;
                }

                Template = Template.Remove(0, 1);
                Template = Template.Remove(Template.Length - 1, 1);

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
            string TmpAspxText = AspxText;
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

                        // If Detection
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

                                        TextToCodeCombination += GetAddCode((HasElseIf ? "else " : "") + "if (" + syntex.FetchSyntexWithEndedCharacter("@" + AspxText.Substring(i)) + ")", !ControllerIsSet);
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

                                    if ((i + 1) < AspxText.Length)
                                    {
                                        string TmpAspxTextForFindElse = ft.FullTrimInStart(AspxText.Substring(i + 1));

                                        if (TmpAspxTextForFindElse.Length < 4)
                                            continue;

                                        if (TmpAspxTextForFindElse.Substring(0, 4) != "else")
                                            continue;
                                    }


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
                                if (AspxText.Substring(i + 1, 2) == "do" && (AspxText[i + 3] == ' ' || AspxText[i + 3] == '\n' || AspxText[i + 3] == '\t' || AspxText[i + 3] == '\r'))
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

            SetMethod(AspxFilePath, Controller, ControllerConstructor, Model, ModelConstructor, ModelUseAbstract, ControllerIsSet, Layout, IsLayout, IsBreak, UseSection, MethodIndexer, TextToCodeCombination);
        }

        public void SetMethod(string AspxFilePath, string Controller, string ControllerConstructor, string Model, string ModelConstructor, bool ModelUseAbstract, bool ControllerIsSet, string Layout, bool IsLayout, bool IsBreak, bool UseSection, int MethodIndexer, string TextToCodeCombination)
        {
            if (AspxFilePath.EndsWith(".cshtml"))
                AspxFilePath = AspxFilePath.GetTextBeforeLastValue(".cshtml") + ".aspx";


            string FilePathToMethodName = AspxFilePath.ToMethodNameClean();
            bool PageIsOnlyView = !ControllerIsSet;

            if (!IsBreak)
                if (IsLayout && SetBreakForLayoutPage)
                    IsBreak = true;

            string AspxFilePathUrl = AspxFilePath.Replace("\\", "/");

            if (!IsBreak)
            {
                string ReturnMethodValue = FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(context" + (IsLayout ? ", \"\"" : "") + ")";


                if (!RewriteAspxFileToDirectory || (RewriteAspxFileToDirectory && AccessAspxFileAfterRewrite && !(IgnoreDefaultAfterRewrite && (AspxFilePath.GetTextAfterLastValue('\\'.ToString()) == "Default.aspx"))))
                {
                    CaseCodeTemplateValue += "                case \"" + AspxFilePathUrl + "\": return " + ReturnMethodValue + ";" + Environment.NewLine;

                    if (UseSection)
                    {
                        SectionTemplateValue += "            if (path.StartsWith(\"" + AspxFilePathUrl + "/\")" + ((AspxFilePath.GetTextAfterLastValue('\\'.ToString()) == "Default.aspx") ? " || path.StartsWith(\"" + AspxFilePathUrl.GetTextBeforeLastValue("Default.aspx") + "\")" : "") + ")" + Environment.NewLine;
                        SectionTemplateValue += "                return " + ReturnMethodValue + ";" + Environment.NewLine;
                    }
                }

                if (RewriteAspxFileToDirectory)
                    if (IgnoreDefaultAfterRewrite && (AspxFilePath.GetTextAfterLastValue('\\'.ToString()) == "Default.aspx"))
                    {
                        CaseCodeTemplateValue += "                case \"" + AspxFilePathUrl + "\": return " + ReturnMethodValue + ";" + Environment.NewLine;

                        if (UseSection)
                        {
                            SectionTemplateValue += "            if (path.StartsWith(\"" + AspxFilePathUrl.GetTextBeforeLastValue("Default.aspx") + "\") || path.StartsWith(\"" + AspxFilePathUrl + "/\"))" + Environment.NewLine;
                            SectionTemplateValue += "                return " + ReturnMethodValue + ";" + Environment.NewLine;
                        }
                    }
                    else
                    {
                        CaseCodeTemplateValue += "                case \"" + AspxFilePathUrl.GetTextBeforeLastValue(".aspx") + "/Default.aspx" + "\": return " + ReturnMethodValue + ";" + Environment.NewLine;

                        if (UseSection)
                        {
                            SectionTemplateValue += "            if (path.StartsWith(\"" + AspxFilePathUrl.GetTextBeforeLastValue(".aspx") + "/\"))" + Environment.NewLine;
                            SectionTemplateValue += "                return " + ReturnMethodValue + ";" + Environment.NewLine;
                        }
                    }
            }

            CaseCodeTemplateValueForFullPath += "                case \"" + AspxFilePathUrl + "\": return " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(context" + (IsLayout ? ", PageReturnValue" : "") + ");" + Environment.NewLine;

            string CallerViewDirectoryPath = AspxFilePathUrl.GetTextBeforeLastValue("/");
            if (string.IsNullOrEmpty(CallerViewDirectoryPath))
                CallerViewDirectoryPath = "/";

            string TmpMethodCodeTemplateValue = Environment.NewLine;
            TmpMethodCodeTemplateValue += "        // View Path: " + AspxFilePathUrl + Environment.NewLine;
            TmpMethodCodeTemplateValue += "        protected string " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(HttpContext context" + (IsLayout ? ", string PageReturnValue" : "") + ")" + Environment.NewLine;
            TmpMethodCodeTemplateValue += "        {" + Environment.NewLine;
            TmpMethodCodeTemplateValue += "            string PreviousRequestPath = RequestPath;" + Environment.NewLine;
            TmpMethodCodeTemplateValue += "            string PreviousCallerViewPath = CallerViewPath;" + Environment.NewLine;
            TmpMethodCodeTemplateValue += "            string PreviousCallerViewDirectoryPath = CallerViewDirectoryPath;" + Environment.NewLine;
            TmpMethodCodeTemplateValue += "            CallerViewPath = \"" + AspxFilePathUrl + "\";" + Environment.NewLine;
            TmpMethodCodeTemplateValue += "            CallerViewDirectoryPath = \"" + CallerViewDirectoryPath + "\";" + Environment.NewLine + Environment.NewLine;

            if (UseSection)
                TmpMethodCodeTemplateValue += "            ValueCollectionLock Section = new ValueCollectionLock(\"" + AspxFilePathUrl + "\", RequestPath, " + (RewriteAspxFileToDirectory ? "true" : "false") + ", " + (IgnoreDefaultAfterRewrite ? "true" : "false") + ");" + Environment.NewLine + Environment.NewLine;

            if (!PageIsOnlyView)
            {
                TmpMethodCodeTemplateValue += "            " + Controller + " controller = new " + Controller + "();" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            controller.CallerViewPath = CallerViewPath;" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            controller.CallerViewDirectoryPath = CallerViewDirectoryPath;" + Environment.NewLine;

                if (UseSection)
                    TmpMethodCodeTemplateValue += "            controller.Section.AddList(Section.GetList());" + Environment.NewLine;

                if (!string.IsNullOrEmpty(ControllerConstructor))
                    TmpMethodCodeTemplateValue += "            controller.CodeBehindConstructor" + ControllerConstructor + ";" + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            controller.PageLoad(context);" + Environment.NewLine + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            ViewData.AddList(controller.ViewData.GetList());" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            if (!string.IsNullOrEmpty(controller.ViewPath))" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            {" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                if (controller.ViewPath != CallerViewPath)" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                {" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                    if (controller.CodeBehindModel == null)" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                        return controller.ResponseText + LoadPage(controller.ViewPath, context);" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                    else" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                        return controller.ResponseText + LoadPage(controller.ViewPath, controller.CodeBehindModel, context);" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                }" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            }" + Environment.NewLine + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            if (!string.IsNullOrEmpty(controller.DownloadFilePath))" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            {" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                Download(context, controller.DownloadFilePath);" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "                return \"\";" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            }" + Environment.NewLine + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            if (!controller.IgnoreViewAndModel)" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            {" + Environment.NewLine;

                if (!string.IsNullOrEmpty(Model))
                {
                    TmpMethodCodeTemplateValue += "                " + Model + " model = (" + Model + ")controller.CodeBehindModel;" + Environment.NewLine;

                    if (ModelUseAbstract)
                    {
                        TmpMethodCodeTemplateValue += "                model.CallerViewPath = CallerViewPath;" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                model.CallerViewDirectoryPath = CallerViewDirectoryPath;" + Environment.NewLine;

                        if (UseSection)
                            TmpMethodCodeTemplateValue += "                model.Section.AddList(Section.GetList());" + Environment.NewLine;
                    }

                    if (!string.IsNullOrEmpty(ModelConstructor))
                        TmpMethodCodeTemplateValue += "                model.CodeBehindConstructor" + ModelConstructor + ";" + Environment.NewLine;

                    if (ModelUseAbstract)
                    {
                        TmpMethodCodeTemplateValue += "                ViewData.AddList(model.ViewData.GetList());" + Environment.NewLine;

                        TmpMethodCodeTemplateValue += "                if (!string.IsNullOrEmpty(model.DownloadFilePath))" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                {" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                    Download(context, model.DownloadFilePath);" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                    return \"\";" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                }" + Environment.NewLine + Environment.NewLine;

                        TmpMethodCodeTemplateValue += "                controller.ResponseText += model.ResponseText;" + Environment.NewLine;
                    }
                }

                TmpMethodCodeTemplateValue += TextToCodeCombination;
                TmpMethodCodeTemplateValue += "            }" + Environment.NewLine + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            RequestPath = PreviousRequestPath;" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            CallerViewPath = PreviousCallerViewPath;" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            CallerViewDirectoryPath = PreviousCallerViewDirectoryPath;" + Environment.NewLine + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            return " + (!string.IsNullOrEmpty(Layout) ? "SetPageLoadByFullPath(\"" + Layout + "\", context, controller.ResponseText)" : "controller.ResponseText") + ";" + Environment.NewLine;
            }
            else
            {
                TmpMethodCodeTemplateValue += "            string ReturnValue = \"\";" + Environment.NewLine;

                if (!string.IsNullOrEmpty(Model))
                {
                    TmpMethodCodeTemplateValue += "            " + Model + " model = new " + Model + "();" + Environment.NewLine;

                    if (ModelUseAbstract)
                    {
                        TmpMethodCodeTemplateValue += "            model.CallerViewPath = CallerViewPath;" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "            model.CallerViewDirectoryPath = CallerViewDirectoryPath;" + Environment.NewLine;

                        if (UseSection)
                            TmpMethodCodeTemplateValue += "            model.Section.AddList(Section.GetList());" + Environment.NewLine;
                    }

                    if (!string.IsNullOrEmpty(ModelConstructor))
                        TmpMethodCodeTemplateValue += "            model.CodeBehindConstructor" + ModelConstructor + ";" + Environment.NewLine;

                    if (ModelUseAbstract)
                    {

                        TmpMethodCodeTemplateValue += "            ViewData.AddList(model.ViewData.GetList());" + Environment.NewLine;

                        TmpMethodCodeTemplateValue += "            if (!string.IsNullOrEmpty(model.DownloadFilePath))" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "            {" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                Download(context, model.DownloadFilePath);" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                return \"\";" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "            }" + Environment.NewLine + Environment.NewLine;

                        TmpMethodCodeTemplateValue += "            ReturnValue += model.ResponseText;" + Environment.NewLine + Environment.NewLine;
                    }
                }

                TmpMethodCodeTemplateValue += TextToCodeCombination + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            RequestPath = PreviousRequestPath;" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            CallerViewPath = PreviousCallerViewPath;" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            CallerViewDirectoryPath = PreviousCallerViewDirectoryPath;" + Environment.NewLine + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            return " + (!string.IsNullOrEmpty(Layout) ? "SetPageLoadByFullPath(\"" + Layout + "\", context, ReturnValue)" : "ReturnValue") + ";" + Environment.NewLine;
            }

            TmpMethodCodeTemplateValue += "        }" + Environment.NewLine;

            if (!string.IsNullOrEmpty(Model))
            {
                CaseCodeTemplateValueForFullPathWithModel += "                case \"" + AspxFilePathUrl + "\": return " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(context" + (IsLayout ? ", PageReturnValue" : "") + ", model);" + Environment.NewLine;

                TmpMethodCodeTemplateValue += Environment.NewLine;

                // Overload With Model Parameter
                TmpMethodCodeTemplateValue += "        // View Path: " + AspxFilePathUrl + Environment.NewLine;
                TmpMethodCodeTemplateValue += "        protected string " + FilePathToMethodName + "_" + Controller.Replace('.', '_') + "_PageLoad" + MethodIndexer + "(HttpContext context" + (IsLayout ? ", string PageReturnValue" : "") + ", object ModelClass)" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "        {" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            string PreviousRequestPath = RequestPath;" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            string PreviousCallerViewPath = CallerViewPath;" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            string PreviousCallerViewDirectoryPath = CallerViewDirectoryPath;" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            CallerViewPath = \"" + AspxFilePathUrl + "\";" + Environment.NewLine;
                TmpMethodCodeTemplateValue += "            CallerViewDirectoryPath = \"" + CallerViewDirectoryPath + "\";" + Environment.NewLine + Environment.NewLine;

                TmpMethodCodeTemplateValue += "            " + Model + " model = (" + Model + ")ModelClass;" + Environment.NewLine + Environment.NewLine;

                if (UseSection)
                    TmpMethodCodeTemplateValue += "            ValueCollectionLock Section = new ValueCollectionLock(\"" + AspxFilePathUrl + "\", RequestPath, " + (RewriteAspxFileToDirectory ? "true" : "false") + ", " + (IgnoreDefaultAfterRewrite ? "true" : "false") + ");" + Environment.NewLine + Environment.NewLine;

                if (!PageIsOnlyView)
                {
                    TmpMethodCodeTemplateValue += "            " + Controller + " controller = new " + Controller + "();" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            controller.CallerViewPath = CallerViewPath;" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            controller.CallerViewDirectoryPath = CallerViewDirectoryPath;" + Environment.NewLine;

                    if (UseSection)
                        TmpMethodCodeTemplateValue += "            controller.Section.AddList(Section.GetList());" + Environment.NewLine;

                    if (!string.IsNullOrEmpty(ControllerConstructor))
                        TmpMethodCodeTemplateValue += "            controller.CodeBehindConstructor" + ControllerConstructor + ";" + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "            controller.PageLoad(context);" + Environment.NewLine + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "            ViewData.AddList(controller.ViewData.GetList());" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            if (!string.IsNullOrEmpty(controller.ViewPath))" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            {" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                if (controller.ViewPath != CallerViewPath)" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                {" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                    if (controller.CodeBehindModel == null)" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                        return controller.ResponseText + LoadPage(controller.ViewPath, context);" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                    else" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                        return controller.ResponseText + LoadPage(controller.ViewPath, controller.CodeBehindModel, context);" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                }" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            }" + Environment.NewLine + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "            if (!string.IsNullOrEmpty(controller.DownloadFilePath))" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            {" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                Download(context, controller.DownloadFilePath);" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "                return \"\";" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            }" + Environment.NewLine + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "            if (!controller.IgnoreViewAndModel)" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            {" + Environment.NewLine;

                    if (ModelUseAbstract)
                    {
                        TmpMethodCodeTemplateValue += "                model.CallerViewPath = CallerViewPath;" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                model.CallerViewDirectoryPath = CallerViewDirectoryPath;" + Environment.NewLine;

                        if (UseSection)
                            TmpMethodCodeTemplateValue += "                model.Section.AddList(Section.GetList());" + Environment.NewLine;
                    }

                    if (!string.IsNullOrEmpty(ModelConstructor))
                        TmpMethodCodeTemplateValue += "                model.CodeBehindConstructor" + ModelConstructor + ";" + Environment.NewLine;

                    if (ModelUseAbstract)
                    {
                        TmpMethodCodeTemplateValue += "                ViewData.AddList(model.ViewData.GetList());" + Environment.NewLine;

                        TmpMethodCodeTemplateValue += "                if (!string.IsNullOrEmpty(model.DownloadFilePath))" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                {" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                    Download(context, model.DownloadFilePath);" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                    return \"\";" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                }" + Environment.NewLine + Environment.NewLine;

                        TmpMethodCodeTemplateValue += "                controller.ResponseText += model.ResponseText;" + Environment.NewLine;
                    }

                    TmpMethodCodeTemplateValue += TextToCodeCombination;
                    TmpMethodCodeTemplateValue += "            }" + Environment.NewLine + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "            RequestPath = PreviousRequestPath;" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            CallerViewPath = PreviousCallerViewPath;" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            CallerViewDirectoryPath = PreviousCallerViewDirectoryPath;" + Environment.NewLine + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            return " + (!string.IsNullOrEmpty(Layout) ? "SetPageLoadByFullPath(\"" + Layout + "\", context, controller.ResponseText)" : "controller.ResponseText") + ";" + Environment.NewLine;
                }
                else
                {
                    TmpMethodCodeTemplateValue += "            string ReturnValue = \"\";" + Environment.NewLine;

                    if (ModelUseAbstract)
                    {
                        TmpMethodCodeTemplateValue += "            model.CallerViewPath = CallerViewPath;" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "            model.CallerViewDirectoryPath = CallerViewDirectoryPath;" + Environment.NewLine;

                        if (UseSection)
                            TmpMethodCodeTemplateValue += "            model.Section.AddList(Section.GetList());" + Environment.NewLine;
                    }

                    if (!string.IsNullOrEmpty(ModelConstructor))
                        TmpMethodCodeTemplateValue += "            model.CodeBehindConstructor" + ModelConstructor + ";" + Environment.NewLine;

                    if (ModelUseAbstract)
                    {

                        TmpMethodCodeTemplateValue += "            ViewData.AddList(model.ViewData.GetList());" + Environment.NewLine;

                        TmpMethodCodeTemplateValue += "            if (!string.IsNullOrEmpty(model.DownloadFilePath))" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "            {" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                Download(context, model.DownloadFilePath);" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "                return \"\";" + Environment.NewLine;
                        TmpMethodCodeTemplateValue += "            }" + Environment.NewLine + Environment.NewLine;

                        TmpMethodCodeTemplateValue += "            ReturnValue += model.ResponseText;" + Environment.NewLine + Environment.NewLine;
                    }

                    TmpMethodCodeTemplateValue += TextToCodeCombination + Environment.NewLine;

                    TmpMethodCodeTemplateValue += "            RequestPath = PreviousRequestPath;" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            CallerViewPath = PreviousCallerViewPath;" + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            CallerViewDirectoryPath = PreviousCallerViewDirectoryPath;" + Environment.NewLine + Environment.NewLine;
                    TmpMethodCodeTemplateValue += "            return " + (!string.IsNullOrEmpty(Layout) ? "SetPageLoadByFullPath(\"" + Layout + "\", context, ReturnValue)" : "ReturnValue") + ";" + Environment.NewLine;
                }

                TmpMethodCodeTemplateValue += "        }" + Environment.NewLine;
            }

            MethodCodeTemplateValue += TmpMethodCodeTemplateValue;
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
