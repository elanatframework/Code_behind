using SetCodeBehind;

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
        public bool IgnoreCorruptView { private set; get; }

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
                string line;
                reader.ReadLine();

                ViewPath = reader.ReadLine().GetTextAfterValue("=");
                MoveViewFromWwwroot = (reader.ReadLine().GetTextAfterValue("=") == "true");
                RewriteAspxFileToDirectory = (reader.ReadLine().GetTextAfterValue("=") == "true");
                AccessAspxFileAfterRewrite = (reader.ReadLine().GetTextAfterValue("=") == "true");
                IgnoreDefaultAfterRewrite = (reader.ReadLine().GetTextAfterValue("=") == "true");
                IgnoreCorruptView = (reader.ReadLine().GetTextAfterValue("=") == "true");
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
            OptionsList.Add("ignore_corrupt_view=false");

            bool HasBreak = false;

            if (File.Exists(OptionsFilePath))
            {
                using (StreamReader reader = new StreamReader(OptionsFilePath))
                {
                    reader.ReadLine();

                    for (int i = 1; i < OptionsList.Count; i++)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                            HasBreak = true;
                            break;
                        }

                        if (OptionsList[i].GetTextBeforeValue("=") != line.GetTextBeforeValue("="))
                        {
                            HasBreak = true;
                            break;
                        }
                        else
                            OptionsList[i] = OptionsList[i].GetTextBeforeValue("=") + "=" + line.GetTextAfterValue("=");
                    }
                }
            }

            if (!File.Exists(OptionsFilePath) || HasBreak)
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