using Microsoft.AspNetCore.Http;

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

        public void View(object ModelClass)
        {
            CodeBehindModel = ModelClass;
        }

        // Overload
        public void View(string ViewPath)
        {
            this.ViewPath = ViewPath;
        }

        // Overload
        public void View(string ViewPath, object ModelClass)
        {
            this.ViewPath = ViewPath;
            CodeBehindModel = ModelClass;
        }

        public void Download(string FilePath)
        {
            DownloadFilePath = FilePath;
        }

        /// <summary>
        /// Never Call This Method In Controller
        /// </summary>
        public string Run(HttpContext context)
        {
            if (!string.IsNullOrEmpty(CallerViewPath))
                return "";

            if (IgnoreViewAndModel)
                return ResponseText;

            CodeBehindExecute execute = new CodeBehindExecute();
            return ResponseText + execute.RunControllerValue(context, ViewPath, CodeBehindModel, ViewData, DownloadFilePath);
        }

        public void FillSection(HttpContext context)
        {
            if (!string.IsNullOrEmpty(CallerViewPath))
                return;

            if (context == null)
                return;

            string RequestPath = context.Request.Path;

            if (RequestPath.Length > 0)
                if (RequestPath[0] == '/')
                    RequestPath = RequestPath.Remove(0, 1);

            string[] ValueList = RequestPath.GetTextBeforeValue("?").Split('/');
            Section.AddList(ValueList);
        }
    }
}
