namespace CodeBehind
{
    public class ValueCollectionLock
    {
        private string[] ValueList;
        private bool Lock = false;

        public ValueCollectionLock()
        {

        }

        public ValueCollectionLock(string AspxPagePath, string RequestPath, bool RewriteAspxFileToDirectory, bool IgnoreDefaultAfterRewrite)
        {
            string Segments = RequestPath;

            Segments = Segments.GetTextBeforeValue("?");

            if (StaticObject.PreventAccessDefaultAspx && Segments.EndsWith("/Default.aspx"))
                Segments = Segments.GetTextBeforeLastValue("/Default.aspx");

            if (string.IsNullOrEmpty(Segments))
                return;

            if (Segments.StartsWith(AspxPagePath))
                Segments = Segments.Remove(0, AspxPagePath.Length);
            else if (Segments.StartsWith(AspxPagePath.GetTextBeforeValue(".aspx") + "/") && RewriteAspxFileToDirectory && !IgnoreDefaultAfterRewrite)
                Segments = Segments.Remove(0, AspxPagePath.GetTextBeforeValue(".aspx").Length);
            else if (Segments.StartsWith(AspxPagePath.GetTextBeforeValue("/Default.aspx")))
                Segments = Segments.Remove(0, AspxPagePath.GetTextBeforeValue("/Default.aspx").Length);
            else if (Segments.StartsWith(AspxPagePath.GetTextBeforeValue(".aspx")))
            {
                if (RewriteAspxFileToDirectory)
                    if (!(IgnoreDefaultAfterRewrite && AspxPagePath.EndsWith("/Default.aspx")))
                        Segments = Segments.Remove(0, AspxPagePath.GetTextBeforeValue(".aspx").Length);
            }
            else
                return;


            if (Segments.Length == 0)
                return;

            if (Segments[0] != '/')
                return;

            if (Segments == "/Default" && RewriteAspxFileToDirectory && !IgnoreDefaultAfterRewrite)
                return;

            Segments = Segments.Remove(0, 1);

            ValueList = Segments.Split("/");

            Lock = true;
        }

        public bool Exist(string Name)
        {
            if (!Lock)
                return false;

            if (ValueList == null)
                return false;

            for (int i = 0; i < ValueList.Length; i++)
            {
                if (ValueList[i] == Name)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// This Method Only Accepts Data For One Time And Ignores The Next Times.
        /// </summary>
        public void AddList(string[] ValueList)
        {
            if (Lock)
                return;

            this.ValueList = ValueList;

            Lock = true;
        }

        public string GetValue(int id)
        {
            if (!Lock)
                return "";

            if (ValueList == null)
                return "";

            if (id >= ValueList.Length)
                return "";

            return ValueList[id];
        }

        public string GetDecodeValue(int id)
        {
            return System.Web.HttpUtility.UrlDecode(GetValue(id));
        }

        public int Count()
        {
            if (!Lock)
                return 0;

            if (ValueList == null)
                return 0;

            return ValueList.Length;
        }

        public string[] GetList()
        {
            return ValueList;
        }
    }
}
