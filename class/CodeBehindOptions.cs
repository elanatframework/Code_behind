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
                MoveViewFromWwwroot = (reader.ReadLine().GetTextAfterValue("=") == "true");
                RewriteAspxFileToDirectory = (reader.ReadLine().GetTextAfterValue("=") == "true");
                AccessAspxFileAfterRewrite = (reader.ReadLine().GetTextAfterValue("=") == "true");
                IgnoreDefaultAfterRewrite = (reader.ReadLine().GetTextAfterValue("=") == "true");
                StartTrimInAspxFile = (reader.ReadLine().GetTextAfterValue("=") == "true");
                InnerTrimInAspxFile = (reader.ReadLine().GetTextAfterValue("=") == "true");
                EndTrimInAspxFile = (reader.ReadLine().GetTextAfterValue("=") == "true");
                SetBreakForLayoutPage = (reader.ReadLine().GetTextAfterValue("=") == "true");
                ConvertCsHtmlToAspx = (reader.ReadLine().GetTextAfterValue("=") == "true");
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
                using (StreamWriter writer = File.CreateText(OptionsFilePath))
                {
                    foreach (string line in OptionsList)
                        writer.WriteLine(line);
                }
            }
        }
    }
}
