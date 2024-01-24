namespace CodeBehind
{
    public class CodeBehindOptions
    {
        private string OptionsFilePath = "code_behind/options.ini";
        public string ViewPath { private set; get; }
        public bool MoveViewFromWwwroot { private set; get; }
        public bool RewriteAspxFileToDirectory { private set; get; }
        public bool AccessAspxFileAfterRewrite { private set; get; }
        public bool IgnoreDefaultAfterRewrite { private set; get; }
        public bool StartTrimInAspxFile { private set; get; }
        public bool InnerTrimInAspxFile { private set; get; }
        public bool EndTrimInAspxFile { private set; get; }
        public bool SetBreakForLayoutPage { private set; get; }
        public bool ConvertCsHtmlToAspx { private set; get; }
        public bool ShowMinorErrors { private set; get; }
        public string ErrorPagePath { private set; get; }
        public CodeBehindOptions()
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
