namespace CodeBehind
{
    public abstract class CodeBehindController
    {
        public object CodeBehindModel { get; protected set; }
        public string ResponseText = "";
        public bool IgnoreViewAndModel = false;
        public HtmlData.NameValueCollection ViewData = new HtmlData.NameValueCollection();
        public string ViewPath { get; protected set; } = "";
        public string DownloadFilePath { get; protected set; } = "";

        public void Write(string Text)
        {
            ResponseText += Text;
        }

        public void View(object ModelClass)
        {
            CodeBehindModel = ModelClass;
        }

        public void View(string ViewPath)
        {
            this.ViewPath = ViewPath;
        }

        public void Download(string FilePath)
        {
            DownloadFilePath = FilePath;
        }
    }
}
