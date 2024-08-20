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
            }
        }

        private void SetFirstValue()
        {
            List<string> OptionsList = new List<string>();

            OptionsList.Add("[CodeBehind options]; do not change order");
            OptionsList.Add("view_path=wwwroot");
            OptionsList.Add("move_view_from_wwwroot=true");
            OptionsList.Add("rewrite_aspx_file_to_directory=false");
            OptionsList.Add("access_aspx_file_after_rewrite=false");
            OptionsList.Add("ignore_default_after_rewrite=true");
            OptionsList.Add("start_trim_in_aspx_file=true");
            OptionsList.Add("inner_trim_in_aspx_file=true");
            OptionsList.Add("end_trim_in_aspx_file=true");
            OptionsList.Add("set_break_for_layout_page=true");
            OptionsList.Add("convert_cshtml_to_aspx=false");
            OptionsList.Add("show_minor_errors=false");
            OptionsList.Add("error_page_path=/error.aspx/{value}");
            OptionsList.Add("prevent_access_default_aspx=false");
            OptionsList.Add("default_role=guest");
            OptionsList.Add("web_forms_script_path=/script");
            OptionsList.Add("auto_create_web_forms_script=true");
            OptionsList.Add("recreate_web_forms_script_after_recompile=false");
            OptionsList.Add("web_forms_view_place=<body>");
            OptionsList.Add("use_default_controller=true");
            OptionsList.Add("default_controller=DefaultController");
            OptionsList.Add("use_section_in_default_controller=true");
            OptionsList.Add("set_break_for_default_controller=true");

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
