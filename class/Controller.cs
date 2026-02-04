using Microsoft.AspNetCore.Http;

namespace CodeBehind
{
    public abstract class CodeBehindController
    {
        public object CodeBehindModel { get; private set; }
        public string ResponseText = "";
        public string WebFormsValue = "";
        public bool IgnoreViewAndModel = false;
        public bool? IgnoreLayout = null;
        public string? WebSocketId = null;
        public string? SSEId = null;
        public bool? UseSSE = null;
        public HtmlData.NameValueCollection ViewData = new HtmlData.NameValueCollection();
        public ValueCollectionLock Segment = new ValueCollectionLock();
        public string ViewPath { get; private set; } = "";
        public string DownloadFilePath { get; private set; } = "";

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

        public void Control(WebForms Forms)
        {
            WebFormsValue = Forms.GetFormsActionData();
        }

        public void Control(WebForms Forms, bool IgnoreAll)
        {
            Control(Forms);

            if (IgnoreAll)
               this.IgnoreAll();
        }

        public void IgnoreAll()
        {
            IgnoreViewAndModel = true;
            IgnoreLayout = true;
        }

        public void IgnoreLayoutForPostBack(IHeaderDictionary Headers)
        {
            if (Headers.TryGetValue("Post-Back", out var value))
                if (value == "true")
                    IgnoreLayout = true;
        }

        /// <summary>
        /// This Method Supports Query String
        /// </summary>
        public void SetViewPath(HttpContext context, string Path, bool UpdateRequestPath = false)
        {
            if (Path.Contains("?"))
            {
                ViewPath = ">" + Path.GetTextBeforeValue("?");

                string QueryString = Path.GetTextAfterValue("?");
                new RequestQuery().AddQueryString(context, QueryString);
            }
            else
                ViewPath = ">" + Path;

            if (UpdateRequestPath)
                context.Request.Path = Path;
        }

        /// <summary>
        /// This Method Not Supports Query String
        /// </summary>
        public void SetViewPath(string Path)
        {
            ViewPath = ">" + Path;
        }

        public void SetErrorPage(HttpContext context, int ErrorValue)
        {
            SetViewPath(context, StaticObject.ErrorPagePathBeforeValue + ErrorValue + StaticObject.ErrorPagePathAfterValue);

            context.Response.StatusCode = ErrorValue;
        }

        public void Download(string FilePath)
        {
            DownloadFilePath = FilePath;
        }

        /// <summary>
        /// Never Call This Method In Controller. Before Call This Method You Should Call PageLoad Method
        /// </summary>
        public string Run(HttpContext context)
        {
            if (!string.IsNullOrEmpty(CallerViewPath))
                return "";

            if (IgnoreViewAndModel)
                return ResponseText;

            CodeBehindExecute execute = new CodeBehindExecute();
            return ResponseText + execute.RunControllerValue(context, ViewPath, CodeBehindModel, ViewData, DownloadFilePath, IgnoreLayout, WebFormsValue, WebSocketId, SSEId, UseSSE);
        }

        public async Task<string> RunAsync(HttpContext context)
        {
            if (!string.IsNullOrEmpty(CallerViewPath))
                return "";

            if (IgnoreViewAndModel)
                return ResponseText;

            CodeBehindExecute execute = new CodeBehindExecute();
            return ResponseText + await execute.RunControllerValueAsync(context, ViewPath, CodeBehindModel, ViewData, DownloadFilePath, IgnoreLayout, WebFormsValue, WebSocketId, SSEId, UseSSE);
        }

        public void FillSegment(HttpContext context, string FillAfter = "")
        {
            if (!string.IsNullOrEmpty(CallerViewPath))
                return;

            if (context == null)
                return;

            string RequestPath = context.Request.Path;

            if (!string.IsNullOrEmpty(FillAfter))
                if (RequestPath.StartsWith(FillAfter))
                    RequestPath = RequestPath.Remove(0, FillAfter.Length);

            if (string.IsNullOrEmpty(RequestPath))
                return;

            if (RequestPath.Length > 0)
                if (RequestPath[0] == '/')
                    RequestPath = RequestPath.Remove(0, 1);
               
            string[] ValueList = RequestPath.Split('/');
            Segment.AddList(ValueList);
        }

        public void SetWebSocketId(string Id)
        {
            WebSocketId = Id;
        }

        // WebSockets Broadcast
        public void Broadcast(HttpContext context, string Message, bool IgnoreThis = false)
        {
            CodeBehindMiddlewareExtensions.WebSocketsBroadcast(context, Message, "", "", "", IgnoreThis);
        }

        public async void BroadcastAsync(HttpContext context, string Message, bool IgnoreThis = false)
        {
            await CodeBehindMiddlewareExtensions.WebSocketsBroadcastAsync(context, Message, "", "", "", IgnoreThis);
        }

        public void Broadcast(HttpContext context, string Message, string RoleName, string Id, string ClientId, bool IgnoreThis = false)
        {
            CodeBehindMiddlewareExtensions.WebSocketsBroadcast(context, Message, RoleName, Id, ClientId, IgnoreThis);
        }

        public async void BroadcastAsync(HttpContext context, string Message, string RoleName, string Id, string ClientId, bool IgnoreThis = false)
        {
            await CodeBehindMiddlewareExtensions.WebSocketsBroadcastAsync(context, Message, RoleName, Id, ClientId, IgnoreThis);
        }

        public void BroadcastForRole(HttpContext context, string Message, string RoleName, bool IgnoreThis = false)
        {
            CodeBehindMiddlewareExtensions.WebSocketsBroadcast(context, Message, RoleName, "", "", IgnoreThis);
        }

        public async void BroadcastForRoleAsync(HttpContext context, string Message, string RoleName, bool IgnoreThis = false)
        {
            await CodeBehindMiddlewareExtensions.WebSocketsBroadcastAsync(context, Message, RoleName, "", "", IgnoreThis);
        }

        public void BroadcastForWebSocketId(HttpContext context, string Message, string Id, bool IgnoreThis = false)
        {
            CodeBehindMiddlewareExtensions.WebSocketsBroadcast(context, Message, "", Id, "", IgnoreThis);
        }

        public async void BroadcastForWebSocketIdAsync(HttpContext context, string Message, string Id, bool IgnoreThis = false)
        {
            await CodeBehindMiddlewareExtensions.WebSocketsBroadcastAsync(context, Message, "", Id, "", IgnoreThis);
        }

        public void BroadcastForClientId(HttpContext context, string Message, string ClientId, bool IgnoreThis = false)
        {
            CodeBehindMiddlewareExtensions.WebSocketsBroadcast(context, Message, "", "", ClientId, IgnoreThis);
        }

        public async void BroadcastForClientIdAsync(HttpContext context, string Message, string ClientId, bool IgnoreThis = false)
        {
            await CodeBehindMiddlewareExtensions.WebSocketsBroadcastAsync(context, Message, "", "", ClientId, IgnoreThis);
        }

        public void SetSSEId(string Id)
        {
            SSEId = Id;
        }

        public void EnableSSE()
        {
            UseSSE = true;
        }

        // SSE Broadcast
        public void BroadcastSSE(HttpContext context, string Message, bool IgnoreThis = false)
        {
            CodeBehindMiddlewareExtensions.SSEsBroadcast(context, Message, "", "", "", IgnoreThis);
        }

        public void BroadcastSSE(HttpContext context, string Message, string RoleName, string Id, string ClientId, bool IgnoreThis = false)
        {
            CodeBehindMiddlewareExtensions.SSEsBroadcast(context, Message, RoleName, Id, ClientId, IgnoreThis);
        }

        public void BroadcastSSEForRole(HttpContext context, string Message, string RoleName, bool IgnoreThis = false)
        {
            CodeBehindMiddlewareExtensions.SSEsBroadcast(context, Message, RoleName, "", "", IgnoreThis);
        }

        public void BroadcastSSEForSSEId(HttpContext context, string Message, string Id, bool IgnoreThis = false)
        {
            CodeBehindMiddlewareExtensions.SSEsBroadcast(context, Message, "", Id, "", IgnoreThis);
        }

        public void BroadcastSSEForClientId(HttpContext context, string Message, string ClientId, bool IgnoreThis = false)
        {
            CodeBehindMiddlewareExtensions.SSEsBroadcast(context, Message, "", "", ClientId, IgnoreThis);
        }
    }
}
