namespace CodeBehind
{
    public abstract class CodeBehindModel
    {
        public string ResponseText = "";
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

        public void Download(string FilePath)
        {
            DownloadFilePath = FilePath;
        }
    }
}
