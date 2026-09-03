using Microsoft.AspNetCore.Http;

namespace CodeBehind
{
    public abstract class CodeBehindModel
    {
        public string ResponseText = "";
        public string WebFormsValue = "";
        public bool IgnoreView = false;
        public bool? IgnoreLayout = null;
        public string? WebSocketId = null;
        public string? SSEId = null;
        public bool? UseSSE = null;
        public HtmlData.NameValueCollection ViewData = new HtmlData.NameValueCollection();
        public ValueCollectionLock Segment = new ValueCollectionLock();
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

        public void Control(WebForms Forms)
        {
            WebFormsValue = Forms.GetFormsActionData();
        }

        public void IgnoreAll()
        {
            IgnoreView = true;
            IgnoreLayout = true;
        }

        public void IgnoreLayoutForPostBack(IHeaderDictionary Headers)
        {
            if (Headers.TryGetValue("Post-Back", out var value))
                if (value == "true")
                    IgnoreLayout = true;
        }

        public void Download(string FilePath)
        {
            DownloadFilePath = FilePath;
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
