using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace CodeBehind
{
    public abstract class CodeBehindController
    {
        public object CodeBehindModel { get; protected set; }
        public string ResponseText = "";
        public bool IgnoreViewAndModel = false;
        public HtmlData.NameValueCollection ViewData = new HtmlData.NameValueCollection();
        public ValueCollectionLock Section = new ValueCollectionLock();
        public string ViewPath { get; protected set; } = "";
        public string DownloadFilePath { get; protected set; } = "";

        /// <summary>
        /// This Attribute Does Not Have A Value In The Constructor Method Of The Class, And Is Set Only After An Instance Of The Class Is Created.
        /// </summary>
        public string CallerViewPath { get; set; } = "";
        /// <summary>
        /// This Attribute Does Not Have A Value In The Constructor Method Of The Class, And Is Set Only After An Instance Of The Class Is Created.
        /// </summary>
        public string CallerViewDirectoryPath { get; set; } = "";

        public void PageLoad(HttpContext context)
        {

        }

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
