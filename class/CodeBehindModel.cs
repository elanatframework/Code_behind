namespace CodeBehind
{
    public abstract class CodeBehindModel
    {
        public string ResponseText = "";
        public string WebFormsValue = "";
        public bool IgnoreView = false;
        public bool IgnoreLayout = false;
        public HtmlData.NameValueCollection ViewData = new HtmlData.NameValueCollection();
        public ValueCollectionLock Section = new ValueCollectionLock();
        public string DownloadFilePath { get; protected set; } = "";
        /// <summary>
        /// This Attribute Does Not Have A Value In The Constructor Method Of The Class, And Is Set Only After An Instance Of The Class Is Created.
        /// </summary>
        public string CallerViewPath { get; set; } = "";
        /// <summary>
        /// This Attribute Does Not Have A Value In The Constructor Method Of The Class, And Is Set Only After An Instance Of The Class Is Created.
        /// </summary>
        public string CallerViewDirectoryPath { get; set; } = "";

        public void Write(string Text)
        {
            ResponseText += Text;
        }

        // Overload
        public void Write(int Number)
        {
            ResponseText += Number;
        }

        // Overload
        public void Write(long Number)
        {
            ResponseText += Number;
        }

        public void WriteLine(string Text)
        {
            Write(Text + Environment.NewLine);
        }

        // Overload
        public void WriteLine(int Number)
        {
            Write(Number + Environment.NewLine);
        }

        // Overload
        public void WriteLine(long Number)
        {
            Write(Number + Environment.NewLine);
        }

        public void Control(WebForms Forms)
        {
            WebFormsValue = Forms.GetFormsActionData();
        }

        public void IgnoreAll()
        {
            IgnoreView = true;
            IgnoreLayout = true;
        }

        public void Download(string FilePath)
        {
            DownloadFilePath = FilePath;
        }
    }
}
