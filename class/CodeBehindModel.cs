namespace CodeBehind
{
    public abstract class CodeBehindModel
    {
        public string ResponseText = "";
        public HtmlData.NameValueCollection ViewData = new HtmlData.NameValueCollection();
        public string DownloadFilePath { get; protected set; } = "";

        public void Write(string Text)
        {
            ResponseText += Text;
        }

        public void Download(string FilePath)
        {
            DownloadFilePath = FilePath;
        }
    }
}
