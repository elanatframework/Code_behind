namespace CodeBehind
{
    internal class CodeBehindOptions
    {
        private string OptionsFilePath = "code_behind/options.ini";
        internal string ViewPath { private set; get; }
        internal bool MoveViewFromWwwroot { private set; get; }
        internal bool RewriteAspxFileToDirectory { private set; get; }
        internal bool AccessAspxFileAfterRewrite { private set; get; }
        internal bool IgnoreDefaultAfterRewrite { private set; get; }
        internal bool StartTrimInAspxFile { private set; get; }
        internal bool InnerTrimInAspxFile { private set; get; }
        internal bool EndTrimInAspxFile { private set; get; }
        internal bool SetBreakForLayoutPage { private set; get; }
        internal bool ConvertCsHtmlToAspx { private set; get; }
        internal bool ShowMinorErrors { private set; get; }
        internal string ErrorPagePath { private set; get; }
        internal bool PreventAccessDefaultAspx { private set; get; }
        internal string DefaultRole { private set; get; }
        internal string WebFormsScriptPath { private set; get; }
        internal bool AutoCreateWebFormsScript { private set; get; }
        internal bool RecreateWebFormsScriptAfterRecompile { private set; get; }
        internal string WebFormsViewPlace { private set; get; }
        internal bool UseDefaultController { private set; get; }
        internal string DefaultController { private set; get; }
        internal bool UseSectionInDefaultController { private set; get; }
        internal bool SetBreakForDefaultController { private set; get; }
        internal bool AccessControllerByLowerCase { private set; get; }
        internal bool JustAccessControllerByLowerCase { private set; get; }
        internal bool SetDefaultPages { private set; get; }

        internal CodeBehindOptions()
        {
            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            SetValue();
        }

        private void SetValue()
        {
            SetFirstValue();


            using (StreamReader reader = new StreamReader(OptionsFilePath))
            {
                reader.ReadLine();

                ViewPath = reader.ReadLine().GetTextAfterValue("=");
                MoveViewFromWwwroot = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                RewriteAspxFileToDirectory = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                AccessAspxFileAfterRewrite = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                IgnoreDefaultAfterRewrite = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                StartTrimInAspxFile = (reader.ReadLine().GetTextAfterValue("=").Trim().Trim() == "true");
                InnerTrimInAspxFile = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                EndTrimInAspxFile = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                SetBreakForLayoutPage = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                ConvertCsHtmlToAspx = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                ShowMinorErrors = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                ErrorPagePath = (reader.ReadLine().GetTextAfterValue("="));
                PreventAccessDefaultAspx = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                DefaultRole = (reader.ReadLine().GetTextAfterValue("=").Trim());
                WebFormsScriptPath = (reader.ReadLine().GetTextAfterValue("="));
                AutoCreateWebFormsScript = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                RecreateWebFormsScriptAfterRecompile = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                WebFormsViewPlace = (reader.ReadLine().GetTextAfterValue("=").Trim());
                UseDefaultController = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                DefaultController = (reader.ReadLine().GetTextAfterValue("=").Trim());
                UseSectionInDefaultController = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                SetBreakForDefaultController = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                AccessControllerByLowerCase = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                JustAccessControllerByLowerCase = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
                SetDefaultPages = (reader.ReadLine().GetTextAfterValue("=").Trim() == "true");
            }
        }

        private void SetFirstValue()
        {
            List<string> OptionsList = new List<string>
            {
                "[CodeBehind options]; do not change order",
                "view_path=wwwroot",
                "move_view_from_wwwroot=true",
                "rewrite_aspx_file_to_directory=false",
                "access_aspx_file_after_rewrite=false",
                "ignore_default_after_rewrite=true",
                "start_trim_in_aspx_file=true",
                "inner_trim_in_aspx_file=true",
                "end_trim_in_aspx_file=true",
                "set_break_for_layout_page=true",
                "convert_cshtml_to_aspx=false",
                "show_minor_errors=false",
                "error_page_path=/error.aspx/{value}",
                "prevent_access_default_aspx=false",
                "default_role=guest",
                "web_forms_script_path=/script",
                "auto_create_web_forms_script=true",
                "recreate_web_forms_script_after_recompile=false",
                "web_forms_view_place=<body>",
                "use_default_controller=true",
                "default_controller=DefaultController",
                "use_section_in_default_controller=true",
                "set_break_for_default_controller=true",
                "access_controller_by_lower_case=true",
                "just_access_controller_by_lower_case=true",
                "set_default_pages=true"
            };

            bool HasMoreOption = false;

            if (File.Exists(OptionsFilePath))
            {
                using (StreamReader reader = new StreamReader(OptionsFilePath))
                {
                    reader.ReadLine();

                    int LineCount = 1;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        LineCount++;
                        int i = 0;
                        foreach (string option in OptionsList)
                        {
                            if (option.GetTextBeforeValue("=") == line.GetTextBeforeValue("="))
                            {
                                OptionsList[i] = OptionsList[i].GetTextBeforeValue("=") + "=" + line.GetTextAfterValue("=");

                                break;
                            }
                            i++;
                        }
                    }

                    if (LineCount < OptionsList.Count)
                        HasMoreOption = true;
                }
            }

            if (!File.Exists(OptionsFilePath) || HasMoreOption)
            {
                var file = File.CreateText(OptionsFilePath);

                foreach (string line in OptionsList)
                    file.WriteLine(line);

                file.Dispose();
                file.Close();
            }
        }
    }
}
