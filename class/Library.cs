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
            string Sections = RequestPath;

            Sections = Sections.GetTextBeforeValue("?");

            if (StaticObject.PreventAccessDefaultAspx && Sections.EndsWith("/Default.aspx"))
                Sections = Sections.GetTextBeforeLastValue("/Default.aspx");

            if (string.IsNullOrEmpty(Sections))
                return;

            if (Sections.StartsWith(AspxPagePath))
                Sections = Sections.Remove(0, AspxPagePath.Length);
            else if (Sections.StartsWith(AspxPagePath.GetTextBeforeValue(".aspx") + "/") && RewriteAspxFileToDirectory && !IgnoreDefaultAfterRewrite)
                Sections = Sections.Remove(0, AspxPagePath.GetTextBeforeValue(".aspx").Length);
            else if (Sections.StartsWith(AspxPagePath.GetTextBeforeValue("/Default.aspx")))
                Sections = Sections.Remove(0, AspxPagePath.GetTextBeforeValue("/Default.aspx").Length);
            else if (Sections.StartsWith(AspxPagePath.GetTextBeforeValue(".aspx")))
            {
                if (RewriteAspxFileToDirectory)
                    if (!(IgnoreDefaultAfterRewrite && AspxPagePath.EndsWith("/Default.aspx")))
                        Sections = Sections.Remove(0, AspxPagePath.GetTextBeforeValue(".aspx").Length);
            }
            else
                return;


            if (Sections.Length == 0)
                return;

            if (Sections[0] != '/')
                return;

            if (Sections == "/Default" && RewriteAspxFileToDirectory && !IgnoreDefaultAfterRewrite)
                return;

            Sections = Sections.Remove(0, 1);

            ValueList = Sections.Split("/");

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

    public class WebSocketsBroadcas
    {
        public string Message { get; set; }
        public string RoleName { get; set; }
        public string WebSocketId { get; set; }
        public string ClientId { get; set; }

        public WebSocketsBroadcas()
        {

        }

        public WebSocketsBroadcas(string Message, string RoleName, string WebSocketId, string ClientId)
        {
            this.Message = Message;
            this.RoleName = RoleName;
            this.WebSocketId = WebSocketId;
            this.ClientId = ClientId;
        }
    }

    public class WebSocketsBroadcastCollection
    {
        private List<WebSocketsBroadcas> WebSocketsBroadcasList = new List<WebSocketsBroadcas>();

        public void Add(string Message, string RoleName, string WebSocketId, string ClientId)
        {
            WebSocketsBroadcasList.Add(new WebSocketsBroadcas(Message, RoleName, WebSocketId, ClientId));
        }

        public void DeleteByIndex(int Index)
        {
            int TmpIndex = (Index >= 0) ? Index : WebSocketsBroadcasList.Count + Index;
            WebSocketsBroadcasList.RemoveAt(TmpIndex);
        }

        public void Empty()
        {
            WebSocketsBroadcasList = new List<WebSocketsBroadcas>();
        }

        public void AddList(List<WebSocketsBroadcas> WebSocketsBroadcasList)
        {
            foreach (WebSocketsBroadcas wsb in WebSocketsBroadcasList)
                this.WebSocketsBroadcasList.Add(wsb);
        }

        public string GetMessageByIndex(int Index)
        {
            int TmpIndex = (Index >= 0) ? Index : WebSocketsBroadcasList.Count + Index;
            return WebSocketsBroadcasList[TmpIndex].Message;
        }

        public string GetRoleNameByIndex(int Index)
        {
            int TmpIndex = (Index >= 0) ? Index : WebSocketsBroadcasList.Count + Index;
            return WebSocketsBroadcasList[TmpIndex].RoleName;
        }

        public string GetWebSocketIdByIndex(int Index)
        {
            int TmpIndex = (Index >= 0) ? Index : WebSocketsBroadcasList.Count + Index;
            return WebSocketsBroadcasList[TmpIndex].WebSocketId;
        }

        public string GetClientIdByIndex(int Index)
        {
            int TmpIndex = (Index >= 0) ? Index : WebSocketsBroadcasList.Count + Index;
            return WebSocketsBroadcasList[TmpIndex].ClientId;
        }

        public int Count()
        {
            return WebSocketsBroadcasList.Count;
        }

        public List<WebSocketsBroadcas> GetList()
        {
            return WebSocketsBroadcasList;
        }
    }
}
