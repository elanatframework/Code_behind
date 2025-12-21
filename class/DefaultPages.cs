namespace SetCodeBehind
{
    internal class DefaultPages
    {
        internal void Set()
        {
            Directory.CreateDirectory("wwwroot");

            string FilePath = "wwwroot/layout.aspx";
            var file1 = File.CreateText(FilePath);

            file1.Write(@"@page
@islayout
@{
    string WelcomeText = ""Welcome to the CodeBehind Framework!"";
}
<!DOCTYPE html>
<html>
<head>
    <title>CodeBehind Framework - @ViewData.GetValue(""title"")</title>
    <script type=""module"" src=""/script/web-forms.js""></script>
    <meta charset=""utf-8"" />
    <style>
    body
    {
        display: grid;
        grid-template-areas:
        'header header header'
        'left main main'
        'footer footer footer';
        grid-template-columns: 1fr 3fr 1fr;
        grid-template-rows: auto 1fr auto;
        height: 100vh;
        margin: 0;
        font-family: Arial, sans-serif;
        line-height: 26px;
    }

    header
    {
        grid-area: header;
        background: #90dbff;
        padding: 1rem;
    }

    footer
    {
        grid-area: footer;
        background: #464646;
        padding: 1rem;
        color: white;
        text-align: center;
    }

    .left-menu
    {
        grid-area: left;
        background: #f2f2f2;
        padding: 1rem;
    }

    main
    {
        grid-area: main;
        background: #fff;
        padding: 1rem;
    }

    a:visited
    {
        color: lightblue;
    }
    </style>
</head>
<body>

    @LoadPage(""/header.aspx"")

    @LoadPage(""/left_menu.aspx"")

    <main>
        <h2>CodeBehind Framework - @ViewData.GetValue(""title"")</h2>
        <p>Text value is: @WelcomeText</p>
        @PageReturnValue
    </main>

    @LoadPage(""/footer.aspx"")

</body>
</html>");

            file1.Dispose();
            file1.Close();


            FilePath = "wwwroot/Default.aspx";
            var file2 = File.CreateText(FilePath);

            file2.Write(@"@page
@layout ""/layout.aspx""
@{
  ViewData.Add(""title"",""Main page"");
}
        <p>CodeBehind library is a modern back-end framework and is an alternative to ASP.NET Core. This library is a programming model based on the MVC structure, which provides the possibility of creating dynamic aspx files in .NET Core and has high serverside independence. CodeBehind framework supports standard syntax and Razor syntax. This framework guarantees the separation of server-side codes from the design part (html) and there is no need to write server-side codes in the view.</p>
        <p>Code Behind framework inherits every advantage of ASP.NET Core and gives it more simplicity, power and flexibility.</p>
        <p><b>CodeBehind framework is an alternative to ASP.NET Core.</b></p>
        <h3>Why use CodeBehind?</h3>
        <ul>
            <li><b>Fast:</b> The CodeBehind framework is faster than the default structure of cshtml pages in ASP.NET Core.</li>
            <li><b>Simple:</b> Developing with CodeBehind is very simple. You can use mvc pattern or model-view or controller-view or only view.</li>
            <li><b>Modular:</b> It is modular. Just copy the new project files, including DLL and aspx, into the current published project (plug and play).</li>
            <li><b>Get output:</b> You can call the output of the aspx page in another aspx page and modify its output.</li>
            <li><b>Under .NET Core:</b> Your project will still be under ASP.NET Core and you will benefit from all the benefits of .NET Core.</li>
            <li><b>Code-Behind:</b> Code-Behind pattern will be fully respected.</li>
            <li><b>Modern:</b> CodeBehind is a modern framework with revolutionary ideas.</li>
            <li><b>Understandable:</b> View is preferable to controller and there is no need to set controllers in route.</li>
            <li><b>Adaptable:</b> The CodeBehind framework can even be used with Razor Pages and ASP.NET Core MVC.</li>
            <li><b>Loose coupling:</b> The different components of CodeBehind work independently of each other.</li>
            <li><b>RAD:</b> Everything is automated in CodeBehind framework, just focus on development.</li>
            <li><b>WebForms Core technology:</b> Supports a new and unique approach modeled after Microsoft's former WebForms.</li>
            <li><b>Full Stack:</b> Manage both back-end and front-end together; you can manage HTML tags from the server-side.</li>
        </ul>
        <p><b>CodeBehind is .NET Diamond!</b></p>
        <p>In every scenario, CodeBehind performs better than the default structure in ASP.NET Core.</p>");

            file2.Dispose();
            file2.Close();


            FilePath = "wwwroot/header.aspx";
            var file3 = File.CreateText(FilePath);

            file3.Write(@"@page
@break
    <header>
        <h1>Company name</h1>
    </header>");

            file3.Dispose();
            file3.Close();


            FilePath = "wwwroot/footer.aspx";
            var file4 = File.CreateText(FilePath);

            file4.Write(@"@page
@break
    <footer>
        <p>&copy; @DateTime.Now.ToString(""yyyy"") Company name - Built with <a href=""https://elanat.net/page_content/code_behind"" title=""CodeBehind Framework"">CodeBehind Framework</a></p>
    </footer>");

            file4.Dispose();
            file4.Close();


            FilePath = "wwwroot/left_menu.aspx";
            var file5 = File.CreateText(FilePath);

            file5.Write(@"@page
@break
    <div class=""left-menu"">
        <ul>
            <li><a href=""/"">Home</a></li>
            <li><a href=""#"">About</a></li>
            <li><a href=""#"">Contact</a></li>
        </ul>
    </div>");

            file5.Dispose();
            file5.Close();


            FilePath = "wwwroot/error.aspx";
            var file6 = File.CreateText(FilePath);

            file6.Write(@"@page
@layout ""/layout.aspx""
@segment
@{
    ViewData.Add(""title"",""Error page"");

    int ErrorValue = 0;
    if (Segment.GetValue(0).IsNumber())
        ErrorValue = Segment.GetValue(0).ToNumber();
}
    <div>
    @if (ErrorValue == 400)
    {
    <h1>Error 400 Bad request</h1>
    <h3>The path you requested is incorrect or the server cannot respond to this request.</h3>
    }
    else if (ErrorValue == 401)
    {
    <h1>Error 401 Authorization required</h1>
    <h3>The path you requested requires validation. Either you don't have access to the path or you need to log in.</h3>
    }
    else if (ErrorValue == 403)
    {
    <h1>Error 403 Forbidden</h1>
    <h3>The path you requested cannot be accessed.</h3>
    }
    else if (ErrorValue == 404)
    {
    <h1>Error 404 Page not found</h1>
    <h3>No page was found in the path you requested.</h3>
    }
    else if (ErrorValue == 500)
    {
    <h1>Error 500 Internal server error</h1>
    <h3>The server encountered an unexpected problem, so the problem prevented us from responding to your request.</h3>
    }
    else
    {
    <h1>Error! Status Code: @ErrorValue</h1>
    <h3>A problem has occurred.</h3>
    }
    </div>");

            file6.Dispose();
            file6.Close();
        }

        internal void SetWebFormsScript(string path, bool ReplaceIfExist = false)
        {
            string FilePath = "wwwroot" + path + "/web-forms.js";

            if (!Directory.Exists("wwwroot"))
                Directory.CreateDirectory("wwwroot");
            else
                if (File.Exists(FilePath) && !ReplaceIfExist)
                return;

            if (!Directory.Exists("wwwroot" + path))
                Directory.CreateDirectory("wwwroot" + path);

            var file = File.CreateText(FilePath);

            file.Write(@"/* WebFormsJS 2.0 - The Front-End Part of WebForms Core Technology, Owned by Elanat (https://elanat.net) */

/* Start Options */

var WebFormsOptions = new Object();

// Initialization
WebFormsOptions.AutoSetSubmitOnClick = true;

// Service Worker
WebFormsOptions.RegisterServiceWorker = false;
WebFormsOptions.RegisterServicePath = ""/service-worker.js"";
WebFormsOptions.RegisterServiceScopePath = '/';
WebFormsOptions.ReloadServiceWorkerIfNeed = true;
WebFormsOptions.UseServiceWorkerPush = false;
WebFormsOptions.UseServiceWorkerPushSubscribe = ""/subscribe"";
WebFormsOptions.ServiceWorkerPushVapidPublicKey = ""BOr9UhjogkDpIVlYweq0mSx0Gcnt8Y6XmvfPWeryfdaWebFormsCorekf1q1qgW93z7pX_AbeD23CE3vZhAkZTY"";
WebFormsOptions.ServiceWorkerWaitForControl = 100;

// Send
WebFormsOptions.SendDataOnlyByPostMethod = false;

// Response
WebFormsOptions.SetResponseInsideDivTag = true;

// State
WebFormsOptions.UseSPALink = true;
WebFormsOptions.SPASaveStateDelay = 500;
WebFormsOptions.SetTitleBySPALink = true;
WebFormsOptions.SPAGlobalTitle = ""My WebSite - "";

// Queue
WebFormsOptions.UseQueue = true;
WebFormsOptions.UseQueueForWebFormsValue = true;
WebFormsOptions.UseDebounceDelay = true;
WebFormsOptions.QueueDebounceDelay = 200;

// Message
WebFormsOptions.MessageDuration = 3000;
WebFormsOptions.ConnectionErrorMessage = ""Connection Error"";
WebFormsOptions.UseConnectionErrorMessage = true;
WebFormsOptions.AddMessageForProblemInDeterminingElement = true;
WebFormsOptions.AddMessageForWebSocketInitializing = true;
WebFormsOptions.AddMessageForWebSocketOpen = true;
WebFormsOptions.AddMessageForWebSocketClose = true;
WebFormsOptions.AddMessageForWebSocketError = true;
WebFormsOptions.AddMessageForSSEInitializing = true;
WebFormsOptions.AddMessageForSSEConnect = true;
WebFormsOptions.AddMessageForSSEDisconnected = true;
WebFormsOptions.AddMessageForSSEReconnecting = true;
WebFormsOptions.AddMessageForSSEClose = true;
WebFormsOptions.AddMessageForSSECloseAll = true;
WebFormsOptions.AddMessageForIncomprehensibleSetWebFormsValue = false;
WebFormsOptions.AddMessageForIncomprehensibleSetValueToInput = false;
WebFormsOptions.AddMessageForIncomprehensibleFetchValue = false;
WebFormsOptions.AddMessageForIncomprehensibleSaveValue = false;
WebFormsOptions.AddMessageForIncomprehensibleCheckCondition = false;
WebFormsOptions.AddMessageForProblemInSetWebFormsValue = false;
WebFormsOptions.AddMessageForProblemInSetValueToInput = false;
WebFormsOptions.AddMessageForProblemInFetchValue = false;
WebFormsOptions.AddMessageForProblemInSaveValue = false;
WebFormsOptions.AddMessageForProblemInCheckCondition = false;

// Console Message
WebFormsOptions.AddLog = true;
WebFormsOptions.AddLogForWebSockets = true;
WebFormsOptions.AddLogForSSE = true;
WebFormsOptions.AddLogForModule = true;

// Non-Response Management
WebFormsOptions.IgnoreEmptyResult = false;
WebFormsOptions.UseRetryRequest = false;
WebFormsOptions.MaxRetryCount = 3;
WebFormsOptions.RetryRequestInterval = 3000;

// Animation
WebFormsOptions.UseProgressBar = true;
WebFormsOptions.UseLoader = true;
WebFormsOptions.HideLoaderTimeout = 5000;
WebFormsOptions.HideLoaderWhenUpload = true;
WebFormsOptions.HideLoaderAfterUploaded = 1024 * 1024;

// Compress
WebFormsOptions.UseGzipFileSend = false;
WebFormsOptions.UseGzipFileSendIgnoreList = [""zip"", ""gzip"", ""rar""];
WebFormsOptions.UseGzipDataSend = false;
WebFormsOptions.UseGzipDataSendLargerThan = 5 * 1024;

// Async/Await
WebFormsOptions.AwaitConditionInterval = 100;

// Security
WebFormsOptions.DisableEval = false;
WebFormsOptions.DisableAppendJavaScriptTag = false;
WebFormsOptions.DisableLoadModule = false;
WebFormsOptions.UseLoadModulePathOnlyInAcceptedList = false;
WebFormsOptions.LoadModulePathOnlyInAcceptedList = [""math""];
WebFormsOptions.DisableCallMethod = false;
WebFormsOptions.UseCallMethodOnlyInAcceptedList = false;
WebFormsOptions.CallMethodOnlyInAcceptedList = [""alert""];
WebFormsOptions.DisableCallModuleMethod = false;
WebFormsOptions.UseCallModuleMethodOnlyInAcceptedList = false;
WebFormsOptions.CallModuleMethodOnlyInAcceptedList = [""confirm""];
WebFormsOptions.SendChecksum = false;
WebFormsOptions.ChecksumName = ""checksum"";

// Language
WebFormsOptions.CheckConditionIsIncomprehensibleLang = ""Check condition is incomprehensible"";
WebFormsOptions.SaveValueIsIncomprehensibleLang = ""Save value is incomprehensible"";
WebFormsOptions.FetchValueIsIncomprehensibleLang = ""Fetch value is incomprehensible"";
WebFormsOptions.SetValueToInputIsIncomprehensibleLang = ""Set value to input is incomprehensible"";
WebFormsOptions.SetWebFormsValueIsIncomprehensibleLang = ""Set webforms value is incomprehensible"";
WebFormsOptions.ConnectionErrorLang = ""Connection error"";
WebFormsOptions.ProblemInCheckConditionLang = ""Problem in check condition"";
WebFormsOptions.ProblemInSaveValueLang = ""Problem in save value"";
WebFormsOptions.ProblemInFetchValueLang = ""Problem in fetch value"";
WebFormsOptions.ProblemInDeterminingElementLang = ""Problem in determining element"";
WebFormsOptions.ProblemInSetValueToInputLang = ""Problem in set value to input"";
WebFormsOptions.ProblemInSetWebFormsValueLang = ""Problem in set webforms value"";
WebFormsOptions.SSEClosingAllLang = ""SSE Closing all"";
WebFormsOptions.SSEManuallyCloseLang = ""SSE Manually close"";
WebFormsOptions.SSEReconnectingLang = ""SSE Reconnecting"";
WebFormsOptions.SSEDisconnectedLang = ""SSE Disconnected"";
WebFormsOptions.SSEConnectedLang = ""SSE Connected"";
WebFormsOptions.SSETryingToConnectLang = ""SSE Trying to connect"";
WebFormsOptions.InitializingNewWebSocketLang = ""Initializing new WebSocket"";
WebFormsOptions.WebSocketErrorLang = ""WebSocket error"";
WebFormsOptions.WebSocketDisconnectedLang = ""WebSocket disconnected"";
WebFormsOptions.WebSocketConnectedLang = ""WebSocket connected"";

// Style
WebFormsOptions.WebFormsTagsBackgroundColor = ""#eee"";
WebFormsOptions.ProgressBarStyle = ""width:100%;min-width:300px;max-width:600px;background-color:#eee;margin:2px 0px"";
WebFormsOptions.ProgressBarPercentLoadedStyle = ""position:absolute;padding:0px 4px;line-height:22px"";
WebFormsOptions.ProgressBarValueStyle = ""height:20px;background-color:#4D93DD;width:0%"";
WebFormsOptions.MessageNoneStyle = ""background-color: #AEAEAE"";
WebFormsOptions.MessageWarningStyle = ""background-color: #AF4C4C"";
WebFormsOptions.MessageProblemStyle = ""background-color: #AFA04C"";
WebFormsOptions.MessageHelpStyle = ""background-color: #4C81AF"";
WebFormsOptions.MessageSuccessStyle = ""background-color: #4CAF8F"";

function cb_GetResponseLocation()
{
    return document.body;
}

/* End Options */

/* Start Dynamic Options */

function cb_EnableQueue(enable)
{
    WebFormsOptions.UseQueue = cb_IsTrue(enable);
}
window.cb_EnableQueue = cb_EnableQueue;

function cb_EnableDebounceDelay(enable)
{
    WebFormsOptions.UseDebounceDelay = cb_IsTrue(enable);
}
window.cb_EnableDebounceDelay = cb_EnableDebounceDelay;

function cb_EnableUseLoader(enable)
{
    WebFormsOptions.UseLoader = cb_IsTrue(enable);
}
window.cb_EnableUseLoader = cb_EnableUseLoader;

function cb_EnableGzipFileSend(enable)
{
    WebFormsOptions.UseGzipFileSend = cb_IsTrue(enable);
}
window.cb_EnableGzipFileSend = cb_EnableGzipFileSend;

function cb_EnableGzipDataSend(enable)
{
    WebFormsOptions.UseGzipDataSend = cb_IsTrue(enable);
}
window.cb_EnableGzipDataSend = cb_EnableGzipDataSend;

/* End Dynamic Options */

/* Start Check Browser Support */

// Check If WebFormsJS Is Not Load Module Mode
if (document.currentScript)
    console.error(""The WebFormsJS library must be loaded with <script type=\""module\"">."");

var cb_UnsupportedFeatures = [];

// Feature List
var cb_BrowseFeatures =
[
    // DOM / Form
    { name: ""FormData"", check: () => typeof FormData !== ""undefined"" },
    { name: ""replaceChildren"", check: () => ""replaceChildren"" in document.createElement(""div"") },

    // ES6 / JavaScript
    { name: ""let/const (ES6 support)"", check: () => { try { eval(""let a=1; const b=2;""); return true; } catch { return false; } } },
    { name: ""Promise"", check: () => typeof Promise !== ""undefined"" },
    { name: ""Array.prototype.forEach"", check: () => typeof Array.prototype.forEach === ""function"" },
    { name: ""Array.prototype.indexOf"", check: () => typeof Array.prototype.indexOf === ""function"" },
    { name: ""Object.keys"", check: () => typeof Object.keys === ""function"" },
    { name: ""JSON.parse/stringify"", check: () => typeof JSON !== ""undefined"" && typeof JSON.parse === ""function"" },
    { name: ""Map"", check: () => typeof Map !== ""undefined"" },
    { name: ""Set"", check: () => typeof Set !== ""undefined"" },
    { name: ""Symbol"", check: () => typeof Symbol !== ""undefined"" },
    { name: ""BigInt"", check: () => typeof BigInt !== ""undefined"" },

    // Web APIs
    { name: ""fetch"", check: () => typeof fetch !== ""undefined"" },
    { name: ""WebSocket"", check: () => typeof WebSocket !== ""undefined"" },
    { name: ""ServiceWorker"", check: () => ""serviceWorker"" in navigator },
    { name: ""WebRTC (RTCPeerConnection)"", check: () => typeof RTCPeerConnection !== ""undefined"" },
    { name: ""Web Animations API"", check: () => ""animate"" in document.createElement(""div"") },

    // Observers / UI
    { name: ""IntersectionObserver"", check: () => ""IntersectionObserver"" in window },
    { name: ""ResizeObserver"", check: () => ""ResizeObserver"" in window },
    { name: ""MutationObserver"", check: () => ""MutationObserver"" in window },

    // Media / Clipboard
    { name: ""Clipboard API"", check: () => ""clipboard"" in navigator },
    { name: ""MediaDevices API"", check: () => ""mediaDevices"" in navigator },

    // Storage
    { name: ""localStorage"", check: () => { try { return ""localStorage"" in window && window.localStorage !== null; } catch { return false; } } },

    // Intl / Localization
    { name: ""Intl API"", check: () => typeof Intl !== ""undefined"" }
];

// Check Each Feature
cb_BrowseFeatures.forEach(f =>
{
    if (!f.check())
        cb_UnsupportedFeatures.push(f.name);
});

// Report
var cb_WebFormsCoreUsingMessage = ""You are using a web application built with WebForms Core technology."";

if (cb_UnsupportedFeatures.length > 0)
    console.warn(cb_WebFormsCoreUsingMessage + ""\nYour browser is outdated and may experience performance issues because it does not support the following features:"", cb_UnsupportedFeatures.join("", ""));
else
    if (WebFormsOptions.AddLog)
        console.log(cb_WebFormsCoreUsingMessage + ""\nCongratulations! All core browser features are supported."");

/* End Check Browser Support */

/* Start WebSocket */

var cb_UseWebSocketPath = [];
var cb_UseWebSocket = false;
var cb_WebSockets = {};

function cb_AddWebSocketPath(path)
{
    if (!path)
        path = window.location.pathname;

    if (cb_UseWebSocketPath.indexOf(path) === -1)
        cb_UseWebSocketPath.push(path);
}

function cb_WebSocketInitialization(Url, formAction)
{
    var ws = new WebSocket(Url);

    ws.onclose = function (evt) { cb_WebSocketOnClose(evt, formAction); };
    ws.onerror = function (evt) { cb_WebSocketOnError(evt, formAction); };

    cb_WebSockets[formAction] = ws;
}

function cb_WebSocketOnOpen(evt, formAction)
{
    if (WebFormsOptions.AddLogForWebSockets)
        console.log(""WebSocket connected, path: "" + formAction);

    if (WebFormsOptions.AddMessageForWebSocketOpen)
        cb_ShowMessage(WebFormsOptions.WebSocketConnectedLang, ""success"", WebFormsOptions.MessageDuration);
}

function cb_WebSocketOnClose(evt, formAction)
{
    if (WebFormsOptions.AddLogForWebSockets)
        console.log(""WebSocket disconnected, path: "" + formAction);

    if (WebFormsOptions.AddMessageForWebSocketClose)
        cb_ShowMessage(WebFormsOptions.WebSocketDisconnectedLang, ""none"", WebFormsOptions.MessageDuration);

    delete cb_WebSockets[formAction];
}

function cb_WebSocketOnError(evt, formAction)
{
    if (WebFormsOptions.AddLogForWebSockets)
        console.log(""WebSocket error, path: "" + formAction + ""\n"" + evt.data);

    if (WebFormsOptions.AddMessageForWebSocketError)
        cb_ShowMessage(WebFormsOptions.WebSocketErrorLang, ""problem"", WebFormsOptions.MessageDuration);
}

function cb_WebSocketDoSend(Message)
{
    if (WebFormsOptions.AddLogForWebSockets)
        console.log(""WebSocket sent:\n"" + Message);

    for (var formAction in cb_WebSockets)
        if (cb_WebSockets[formAction].readyState === WebSocket.OPEN)
            cb_WebSockets[formAction].send(Message);
}

function cb_WebSocketSet(formAction)
{
    if (!formAction)
        formAction = window.location.pathname;

    var Url = cb_ConvertToWebSocketUrl(formAction)

    if (WebFormsOptions.AddLogForWebSockets)
        console.log(""WebSocket request path: "" + formAction);

    var active = false;
    if (cb_WebSockets[formAction] && (cb_WebSockets[formAction].readyState === WebSocket.OPEN || cb_WebSockets[formAction].readyState === WebSocket.CONNECTING))
        active = true;

    if (!active)
    {
        if (WebFormsOptions.AddLogForWebSockets)
            console.log(""No active WebSocket for this path, initializing new one..."");

        if (WebFormsOptions.AddMessageForWebSocketInitializing)
            cb_ShowMessage(WebFormsOptions.InitializingNewWebSocketLang, ""help"", WebFormsOptions.MessageDuration);

        cb_WebSocketInitialization(Url, formAction);
    }
    else
    {
        if (WebFormsOptions.AddLogForWebSockets)
            console.log(""WebSocket already connected or connecting for this path"");
    }
}

/* End WebSocket */

/* Start SSE */

const cb_SSEConnections = {};

function cb_ConnectToSSE(evt, path, shouldReconnect = true, reconnectTryTimeout = 3000, viewState)
{
    if (!path)
        path = window.location.pathname;

    // Close Old Connection On The Same Path (If Exist)
    if (cb_SSEConnections[path])
    {
        cb_SSEConnections[path].close();
        delete cb_SSEConnections[path];
    }

    if (WebFormsOptions.AddLogForSSE)
        console.log(`SSE Trying to connect: ${path}`);

    if (WebFormsOptions.AddMessageForSSEInitializing)
        cb_ShowMessage(WebFormsOptions.SSETryingToConnectLang, ""help"", WebFormsOptions.MessageDuration);

    const source = new EventSource(path);

    source.onopen = () =>
    {
        if (WebFormsOptions.AddLogForSSE)
            console.log(`SSE Connected: ${path}`);

        if (WebFormsOptions.AddMessageForSSEConnect)
            cb_ShowMessage(WebFormsOptions.SSEConnectedLang, ""success"", WebFormsOptions.MessageDuration);
    };

    source.onmessage = (event) =>
    {
        if (WebFormsOptions.AddLogForSSE)
            console.log(`SSE Message from ${path}:`, event.data);

        var response = event.data.Replace(""$[sln];"" , '\n');

        cb_SetResponse(evt, response, viewState, """");
    };

    source.onerror = () =>
    {
        if (WebFormsOptions.AddLogForSSE)
            console.warn(`SSE Disconnected: ${path}`);

        if (WebFormsOptions.AddMessageForSSEDisconnected)
            cb_ShowMessage(WebFormsOptions.SSEDisconnectedLang, ""problem"", WebFormsOptions.MessageDuration);

        source.close();
        delete cb_SSEConnections[path];

        if (shouldReconnect)
        {
            console.log(`SSE Reconnecting to ${path} in ${reconnectTryTimeout}ms...`);

            if (WebFormsOptions.AddMessageForSSEReconnecting)
                cb_ShowMessage(WebFormsOptions.SSEReconnectingLang + "" ..."", ""none"", WebFormsOptions.MessageDuration);

            setTimeout(() => cb_ConnectToSSE(evt, path, shouldReconnect, reconnectTryTimeout, viewState), reconnectTryTimeout);
        }
    };

    // Add Current Event Source
    cb_SSEConnections[path] = source;
}

function cb_DisconnectSSE(path)
{
    const source = cb_SSEConnections[path];

    if (source)
    {
        source.close();
        delete cb_SSEConnections[path];
        if (WebFormsOptions.AddLogForSSE)
            console.log(`SSE Manually closed connection: ${path}`);

        if (WebFormsOptions.AddMessageForSSEClose)
            cb_ShowMessage(WebFormsOptions.SSEManuallyCloseLang, ""none"", WebFormsOptions.MessageDuration);
    }
}

function cb_DisconnectAllSSE()
{
    for (const path in cb_SSEConnections)
    {
        cb_SSEConnections[path].close();
        if (WebFormsOptions.AddLogForSSE)
            console.log(`SSE Closed: ${path}`);
    }

    Object.keys(cb_SSEConnections).forEach(k => delete cb_SSEConnections[k]);

    if (WebFormsOptions.AddMessageForSSECloseAll)
        cb_ShowMessage(WebFormsOptions.SSEClosingAllLang, ""none"", WebFormsOptions.MessageDuration);
}

/* End SSE */

/* Start Event */

function cb_FakeEvent()
{
    return new Event(""load"", { bubbles: false, cancelable: false });
}
function cb_SetPostBackFunctionToSubmit(obj)
{
    if (!WebFormsOptions.AutoSetSubmitOnClick)
        return;

    const SubmitInputs = (obj) ? obj.querySelectorAll('input[type=""submit""], button[type=""submit""]') : document.querySelectorAll('input[type=""submit""], button[type=""submit""]');

    SubmitInputs.forEach(function (InputElement)
    {
        if (InputElement.hasAttribute(""onclick""))
        {
            var OnClickAttr = InputElement.getAttribute(""onclick"");

            if (!OnClickAttr)
            {
                InputElement.setAttribute(""onclick"", ""PostBack(event)"");
                return;
            }

            if (!OnClickAttr.ContainsNameWithSpliter(""PostBack"", ';', '('))
                if (OnClickAttr.charAt(OnClickAttr.length - 1) == ';')
                    InputElement.setAttribute(""onclick"", OnClickAttr + ""PostBack(event)"");
                else
                    InputElement.setAttribute(""onclick"", OnClickAttr + "";PostBack(event)"");
        }
        else
            InputElement.setAttribute(""onclick"", ""PostBack(event)"");
    });
}

function cb_RemovePostBackFunctionInSubmit(obj, evt)
{
    if (obj.tagName)
        if (obj.tagName.toLowerCase() == ""input"" || obj.tagName.toLowerCase() == ""button"")
            if (obj.hasAttribute(""type""))
                if (obj.getAttribute(""type"").toLowerCase() == ""submit"")
                    if (evt.toLowerCase() == ""onclick"" || evt.toLowerCase() == ""click"")
                        if (obj.hasAttribute(""onclick""))
                            if (obj.getAttribute(""onclick"") == ""PostBack(event)"")
                                obj.removeAttribute(""onclick"");
}

window.onload = function ()
{
    cb_Initialization();
};

function cb_Initialization(obj)
{
    cb_SetWebFormsTagsValue(obj);
    cb_SetWebFormsCommentsValue(obj);
    cb_SetPostBackFunctionToSubmit(obj);
    cb_SetSPALink(obj);

    cb_CleanExpiredCache();
}

function cb_AddEvent(obj, event, functionWithArgs)
{
    // Remove Auto PostBack
    cb_RemovePostBackFunctionInSubmit(obj, event);

    if (obj.hasAttribute(event))
        if (obj.getAttribute(event))
        {
            var currentAttribute = obj.getAttribute(event);

            if (event == ""onload"")
            {
                var tmpObjOnload = obj.onload;
                obj.onload = new Function(functionWithArgs);
                obj.onload();
                obj.onload = tmpObjOnload;
                obj.setAttribute(event, functionWithArgs);

                if (!obj)
                    return;

                if (obj.getAttribute(event).length > functionWithArgs.length)
                    currentAttribute += ';' + obj.getAttribute(event).Replace(functionWithArgs, """");
            }

            obj.setAttribute(event, currentAttribute + ';' + functionWithArgs);
            return;
        }

    if (event == ""onload"")
    {
        var tmpObjOnload = obj.onload;
        obj.onload = new Function(functionWithArgs);
        obj.onload();
        obj.onload = tmpObjOnload;
    }

    obj.setAttribute(event, functionWithArgs);
}

function cb_RemoveEvent(obj, event, functionName)
{
    var currentEvent = obj.getAttribute(event);

    if (!currentEvent)
        return;

    var escaped = functionName.replace(/[.*+?^${}()|[\]\\]/g, ""\\$&"");

    var regex = new RegExp(escaped + ""\\([^)]*\\)(?:\\([^)]*\\))?;?"", 'g');

    var updatedEvent = currentEvent.replace(regex, """").trim();

    obj.setAttribute(event, updatedEvent);
}

var cb_EventRegistry = {};

async function cb_AddEventListener(obj, event, currentFunction, args = [], functionType = ""event"")
{
    // Remove Auto PostBack
    cb_RemovePostBackFunctionInSubmit(obj, event);

    var callback = async function (evt)
    {
        args = await cb_SetDynamicValueForArgs(evt, args);

        switch (functionType)
        {
            case ""event"": currentFunction.apply(this, [evt, ...args]); break;
            case ""method"": currentFunction.apply(window, [...args]); break;
        }
    };

    if (obj && event == ""load"")
    {
        var fakeEvent = cb_FakeEvent();
        args = await cb_SetDynamicValueForArgs(fakeEvent, args);

        switch (functionType)
        {
            case ""event"":
                currentFunction.apply(obj, [fakeEvent, ...args]);
                break;
            case ""method"": currentFunction.apply(window, [...args]); break;
        }
    }

    obj.addEventListener(event, callback);

    // Generate A Unique ID If The Element Doesn't Have
    var objId;
    if (obj instanceof Element)
    {
        objId = obj.id;
        if (!objId)
        {
            objId = ""cb_"" + Math.random().toString(36).substring(2, 9);
            obj.id = objId;
            // Store As Data Attribute For Easier Lookup During DOM Replacement
            obj.setAttribute(""data-cb-id"", objId);
        }
    }
    else
        objId = ""_cb_global_"" + event;

    if (!cb_EventRegistry[objId])
        cb_EventRegistry[objId] = {};

    if (!cb_EventRegistry[objId][event])
        cb_EventRegistry[objId][event] = [];

    // Check If This Exact Listener Already Exists
    const existingListener = cb_EventRegistry[objId][event].find(
        entry => entry.currentFunction === currentFunction && JSON.stringify(entry.args) === JSON.stringify(args)
    );

    if (!existingListener)
        cb_EventRegistry[objId][event].push({ callback, currentFunction, args });
}

function cb_RemoveEventListener(obj, event, currentFunction)
{
    var objId = obj instanceof Element ? (obj.id || obj) : ""_cb_global_"" + event;
    var listeners = cb_EventRegistry[objId]?.[event];

    if (listeners)
    {
        const listenerIndex = listeners.findIndex((entry) => entry.currentFunction === currentFunction);

        if (listenerIndex !== -1)
        {
            const listener = listeners[listenerIndex];
            obj.removeEventListener(event, listener.callback);

            // Remove From Registry
            cb_EventRegistry[objId][event].splice(listenerIndex, 1);

            if (cb_EventRegistry[objId][event].length === 0)
                delete cb_EventRegistry[objId][event];

            if (Object.keys(cb_EventRegistry[objId]).length === 0)
                delete cb_EventRegistry[objId];
        }
    }
}

function cb_CleanupEventRegistry()
{
    Object.keys(cb_EventRegistry).forEach(objId =>
    {
        const element = document.getElementById(objId) || document.querySelector(`[data-cb-id=""${objId}""]`);
        if (!element)
            delete cb_EventRegistry[objId];
    });
}

function cb_PreServedEvent(evt)
{
    var captured = {};

    if (evt)
    {
        captured.currentTarget = evt.currentTarget;
        captured.target = evt.target;
        captured.type = evt.type;
        captured.bubbles = evt.bubbles;
        captured.cancelable = evt.cancelable;
        captured.clientX = evt.clientX;
        captured.clientY = evt.clientY;
        captured.pageX = evt.pageX;
        captured.pageY = evt.pageY;
        captured.offsetX = evt.offsetX;
        captured.offsetY = evt.offsetY;
        captured.keyCode = evt.keyCode;
        captured.key = evt.key;
        captured.button = evt.button;
        captured.which = evt.which;
        captured.altKey = evt.altKey;
        captured.ctrlKey = evt.ctrlKey;
        captured.metaKey = evt.metaKey;
        captured.shiftKey = evt.shiftKey;
        captured.deltaY = evt.deltaY;
        // ... Has More Event Properties

        if (typeof evt.preventDefault === ""function"")
            captured.preventDefault = function (){evt.preventDefault();};

        if (typeof evt.stopPropagation === ""function"")
            captured.stopPropagation = function () {evt.stopPropagation();};

        if (typeof evt.getModifierState === ""function"")
            captured.getModifierState = function (keyArg) { return evt.getModifierState(keyArg); };


        // ... Has More Event Methods
    }

    return captured;
}

function PreventDefault(evt)
{
    evt.preventDefault();
}
window.PreventDefault = PreventDefault;

function StopPropagation(evt)
{
    evt.stopPropagation();
}
window.StopPropagation = StopPropagation;

function cb_SetSPALink(obj)
{
    if (!WebFormsOptions.UseSPALink)
        return;

    const links = (obj) ? obj.querySelectorAll('a') : document.body.querySelectorAll('a');

    links.forEach(link =>
    {
        var targetAttr = link.getAttribute(""target"");
        var hrefAttr = link.getAttribute(""href"");

        if (hrefAttr && !hrefAttr.includes(""://"") && !hrefAttr.startsWith(""mailto:"") && !hrefAttr.startsWith(""tel:"") && (!targetAttr || targetAttr === ""_self""))
            link.setAttribute(""onclick"", `PreventDefault(event);GetBack(event, '${hrefAttr}');`);
    });
}

function cb_TriggerEvent(element, constructorNameOrEvent, eventNameOrOptions, maybeOptions = {})
{
    let event, constructorName, eventName, options;

    if (typeof eventNameOrOptions === ""string"")
    {
        constructorName = constructorNameOrEvent?.toLowerCase?.();
        eventName = eventNameOrOptions;
        options = maybeOptions;
    }
    else
    {
        constructorName = null;
        eventName = constructorNameOrEvent;
        options = eventNameOrOptions || {};
    }

    // Default Options
    const defaultOptions = { bubbles: true, cancelable: true, ...options };

    // Automatic Event Type Detection
    if (!constructorName)
    {
        if (/key/i.test(eventName))
            constructorName = ""keyboardevent"";
        else if
            (/click|mouse/i.test(eventName)) constructorName = ""mouseevent"";
        else if
            (/input/i.test(eventName)) constructorName = ""inputevent"";
        else if
            (/focus|blur/i.test(eventName)) constructorName = ""focusevent"";
        else if
            (/scroll|resize/i.test(eventName)) constructorName = ""uievent"";
        else constructorName = ""event"";
    }

    // Create Event Based In Constructor
    switch (constructorName)
    {
        case ""mouseevent"": event = new MouseEvent(eventName, defaultOptions); break;
        case ""keyboardevent"":
            event = new KeyboardEvent(eventName,
            {
                key: options.key || 'a',
                code: options.code || 'KeyA',
                ...defaultOptions
            });
            break;
        case ""inputevent"": event = new InputEvent(eventName, defaultOptions); break;
        case ""focusevent"": event = new FocusEvent(eventName, defaultOptions); break;
        case ""uievent"": event = new UIEvent(eventName, defaultOptions); break;
        default: event = new Event(eventName, defaultOptions); break;
    }

    element.dispatchEvent(event);
}

function cb_EventSerialize(e)
{
    const obj = {};

    for (let key in e)
    {
        try
        {
            const v = e[key];
            if (typeof v !== ""object"" && typeof v !== ""function"")
                obj[key] = v;
        }
        catch
        {
            /* empty */
        }
    }

    return JSON.stringify(obj, null, 2);
}

/* End Event */

/* Start Custom Event */

// Scroll To Button
function cb_WindowBottomReached()
{
    const bottomReached = window.innerHeight + window.scrollY >= document.body.scrollHeight;

    if (bottomReached)
    {
        const event = new CustomEvent(""scrollbottom"");
        window.dispatchEvent(event);
    }
}

function cb_EnableScrollBottomEvent(enable = true)
{
    if (enable)
        window.addEventListener(""scroll"", cb_WindowBottomReached);
    else
        window.removeEventListener(""scroll"", cb_WindowBottomReached);
}

// After Element Reached
function cb_ElementReachedHandler(currentElement, once)
{
    return function handler()
    {
        if (cb_ElementReachedCheck(currentElement) && once)
            window.removeEventListener(""scroll"", handler);
    };
}

function cb_ElementReachedCheck(currentElement)
{
    const rect = currentElement.getBoundingClientRect();
    const inView = rect.top < window.innerHeight && rect.bottom > 0;

    if (inView)
    {
        const event = new CustomEvent(""elementreached"");
        currentElement.dispatchEvent(event);
        return true;
    }
    return false;
}

function cb_EnableReachedElementEvent(currentElement, once = false, enable = true)
{
    if (!currentElement._cbHandler)
        currentElement._cbHandler = cb_ElementReachedHandler(currentElement, once);

    const handler = currentElement._cbHandler;

    if (enable)
    {
        window.addEventListener(""scroll"", handler);
        handler(); // First Check
    }
    else
        window.removeEventListener(""scroll"", handler);
}

function cb_CreateCustomDOMEvent(element, eventName, watch = ""attribute"", key = """", compare = ""equal"", value, range = [0, 0], immediate = false, delay = 0)
{
    let lastValue = null;
    let paused = false;
    let timeout = null;
    let lastExecution = 0;

    const dispatch = () =>
    {
        const e = new Event(eventName);
        element.dispatchEvent(e);

        const attrName = 'on' + eventName;
        const handlerName = element.getAttribute(attrName);
        if (handlerName && typeof window[handlerName] === ""function"")
            window[handlerName]();
    };

    const safeParse = (v) =>
    {
        const n = parseFloat(v);
        return Number.isFinite(n) ? n : NaN;
    };

    const compareValues = (current) =>
    {
        if (watch === ""children"")
            current = (current?.length) || 0;

        if (compare === ""changed"")
        {
            const changed = current !== lastValue;
            lastValue = current;
            return changed;
        }
        if (compare === ""greater"")
        {
            const num = safeParse(current);
            if (isNaN(num))
                return false;
            return num > value;
        }
        if (compare === ""less"")
        {
            const num = safeParse(current);
            if (isNaN(num))
                return false;
            return num < value;
        }
        if (compare === ""equal"")
            return current === value;
        if (compare === ""notequal"")
            return current !== value;
        if (compare === ""includes"")
            return ("""" + current).includes(value);
        if (compare === ""startswith"")
            return ("""" + current).startsWith(value);
        if (compare === ""endswith"")
            return ("""" + current).endsWith(value);
        if (compare === ""matches"")
        {
            try
            {
                return new RegExp(value).test(current);
            }
            catch
            {
                return false;
            }
        }
        if (compare === ""inrange"")
        {
            const num = safeParse(current);
            if (isNaN(num))
                return false;
            return num >= range[0] && num <= range[1];
        }
        if (compare === ""lengthgreater"")
            return (current?.length) > value;
        if (compare === ""lengthless"")
            return (current?.length) < value;
        if (compare === ""lengthequal"")
            return (current?.length) === value;

        return false;
    };

    const getCurrent = () =>
    {
        if (watch === ""value"" && (element instanceof HTMLInputElement || element instanceof HTMLTextAreaElement || element instanceof HTMLSelectElement))
            return element.value;

        switch (watch)
        {
            case ""attribute"": return element.getAttribute(key);
            case ""style"": return getComputedStyle(element)[key];
            case ""text"": return element.textContent?.trim() || """";
            case ""children"": return element.children;
            default: return null;
        }
    };

    const checkAndDispatch = () =>
    {
        if (paused)
            return;

        const now = Date.now();
        const current = getCurrent();

        if (!compareValues(current))
            return;

        const execute = () =>
        {
            lastExecution = Date.now();
            dispatch();
        };

        if (delay > 0)
        {
            if (now - lastExecution >= delay)
                execute();
            else
            {
                if (timeout)
                    clearTimeout(timeout);
                timeout = setTimeout(execute, delay);
            }
        }
        else
            execute();
    };

    let observer = null;

    if (watch === ""value"" && (element instanceof HTMLInputElement || element instanceof HTMLTextAreaElement || element instanceof HTMLSelectElement))
    {
        element.addEventListener(""input"", checkAndDispatch);
        element.addEventListener(""change"", checkAndDispatch);
    }
    else
    {
        observer = new MutationObserver(checkAndDispatch);
        observer.observe(element,
        {
            attributes: watch === ""attribute"" || watch === ""style"",
            attributeFilter: watch === ""attribute"" ? [key] : undefined,
            childList: watch === ""children"" || watch === ""text"",
            subtree: watch === ""children"" || watch === ""text"",
            characterData: watch === ""text"",
        });
    }

    if (immediate)
        checkAndDispatch();

    return {
        disconnect: () =>
        {
            if (observer)
                observer.disconnect();
            if (watch === ""value"")
            {
                element.removeEventListener(""input"", checkAndDispatch);
                element.removeEventListener(""change"", checkAndDispatch);
            }
        },
        pause: () => { paused = true; },
        resume: () => { paused = false; }
    };
}

/* End Custom Event */

/* Start Post-Back */

function cb_PostRequestAndResponse(evt, ViewState, formElement, retryCount = 0, resolveCallback)
{
    cb_ShowLoader();

    evt = evt || cb_FakeEvent();
    evt = cb_PreServedEvent(evt);

    var obj = evt.currentTarget || null;

    // Set Form Value
    var Form = obj;

    if (!formElement)
    {
        do
        {
            if (!Form.parentNode)
            {
                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();

                return;
            }

            Form = Form.parentNode;
        }
        while (Form.nodeName.toLowerCase() != ""form"");
    }
    else
    {
        Form = cb_GetElementByElementPlace(formElement);
        if (!obj)
            obj = Form;
    }

    if (Form.nodeName.toLowerCase() != ""form"")
    {
        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    var FormMethod = (WebFormsOptions.SendDataOnlyByPostMethod) ? ""POST"" : (Form.hasAttribute(""method"") ? Form.getAttribute(""method"") : ""GET"");
    var FormAction = Form.hasAttribute(""action"")? Form.getAttribute(""action"") : """";

    // Chek Form Multi Part
    var FormIsMultiPart = false;
    if (Form.hasAttribute(""enctype"") && (FormMethod.toLowerCase() == ""post"" || FormMethod.toLowerCase() == ""put""))
        if (Form.getAttribute(""enctype"") == ""multipart/form-data"")
            FormIsMultiPart = true;


    // Set Progress Tag
    if (WebFormsOptions.UseProgressBar)
        cb_SetProgressTag(obj, Form);


    // Set Input Value
    var TagSubmitValue = null;
    switch (obj.nodeName.toLowerCase())
    {
        case ""input"":
        case ""button"":
            TagSubmitValue = (obj.getAttribute(""value"")) ? obj.getAttribute(""value"") : """";
            break;
        case ""select"": TagSubmitValue =  (obj.options[obj.selectedIndex].value) ? obj.options[obj.selectedIndex].value : """";
    }

    var OldObjectType;
    if (obj.getAttribute(""type""))
        if (obj.getAttribute(""type"").toLowerCase() == ""submit"")
        {
            OldObjectType = obj.type.toLowerCase();
            obj.setAttribute(""type"", ""button"");
            obj.setAttribute(""main-type"", ""submit"");
        }

    // Create Request Name
    var RequestNameForCache = '<';
    var RequestName = (FormAction == """") ? window.location.pathname : FormAction;
    if (FormAction.length > 0)
    {
        if (FormAction.substring(0, 1) == '#')
            RequestName = window.location.pathname + FormAction;

        if (FormAction.Contains('#'))
            RequestNameForCache = '#' + FormAction.GetTextAfter('#');
    }
    if (obj.getAttribute(""name""))
        RequestName = obj.getAttribute(""name"") + '|' + TagSubmitValue + '|' + RequestName;

    // Check Cache
    if (cb_UsedCache(evt, RequestName, RequestNameForCache))
    {
        // Reset Input Type
        setTimeout(function () { if (OldObjectType === ""submit"") obj.type = ""submit""; }, 1);

        if (obj.hasAttribute(""main-type""))
            obj.removeAttribute(""main-type"");

        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    // Using WebSocket Protocol
    var tmpFormAction = (FormAction == """") ? window.location.pathname : FormAction;
    if (window.WebSocket && (cb_UseWebSocket || Form.hasAttribute(""usewebsocket"") || (cb_UseWebSocketPath.indexOf(tmpFormAction) >= 0)))
    {
        if (cb_UseWebSocket == '$')
            cb_UseWebSocket = false;

        cb_WebSocketSet(tmpFormAction);

        if (cb_WebSockets[tmpFormAction])
        {
            var formDataSerialize = ""form=true&"" + cb_FormDataSerialize(Form, obj.getAttribute(""name""), TagSubmitValue, OldObjectType, false);

            if (cb_WebSockets[tmpFormAction].readyState === WebSocket.OPEN)
                cb_WebSocketDoSend(formDataSerialize);
            else
            {
                cb_WebSockets[tmpFormAction].onopen = function ()
                {
                    cb_WebSocketDoSend(formDataSerialize);
                };
            }

            cb_WebSockets[tmpFormAction].onmessage = function (event)
            {
                var WebSocketResult = event.data;
                cb_SetResponse(evt, WebSocketResult, ViewState, RequestName);

                Form.focus();

                // Reset Input Type
                setTimeout(function () { if (OldObjectType === ""submit"") obj.type = ""submit""; }, 1);

                if (obj.hasAttribute(""main-type""))
                    obj.removeAttribute(""main-type"");

                if (WebFormsOptions.AddLogForWebSockets)
                    console.log(""WebSocket server response:\n"" + event.data);
            };
        }

        // Reset Input Type
        setTimeout(function () { if (OldObjectType === ""submit"") obj.type = ""submit""; }, 1);

        if (obj.hasAttribute(""main-type""))
            obj.removeAttribute(""main-type"");

        cb_HideLoader();


        if (resolveCallback)
            resolveCallback();

        return;
    }

    // Using Http Protocol
    var XMLHttp = new XMLHttpRequest();
    XMLHttp.onreadystatechange = function ()
    {
        if (XMLHttp.readyState == 4)
        {
            if (XMLHttp.status >= 200 && XMLHttp.status < 300)
            {
                if (XMLHttp.status != 202 && XMLHttp.status != 204)
                {
                    var HttpResult = XMLHttp.responseText;
                    cb_SetResponse(evt, HttpResult, ViewState, RequestName);
                }

                // Reset Input Type
                setTimeout(function () { if (OldObjectType === ""submit"") obj.type = ""submit""; }, 1);

                if (obj.hasAttribute(""main-type""))
                    obj.removeAttribute(""main-type"");

                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();
            }
            else if (XMLHttp.status >= 400 && XMLHttp.status < 500)
            {
                if (WebFormsOptions.UseConnectionErrorMessage)
                    cb_ShowConnectionError(XMLHttp.status);

                // Reset Input Type
                setTimeout(function () { if (OldObjectType === ""submit"") obj.type = ""submit""; }, 1);

                if (obj.hasAttribute(""main-type""))
                    obj.removeAttribute(""main-type"");

                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();
            }
            else if (XMLHttp.status >= 500 && XMLHttp.status < 600)
            {
                if (WebFormsOptions.UseRetryRequest && retryCount < WebFormsOptions.MaxRetryCount)
                {
                    setTimeout(() => { cb_PostRequestAndResponse(evt, ViewState, formElement, retryCount + 1); }, WebFormsOptions.RetryRequestInterval);
                }
                else
                {
                    if (WebFormsOptions.UseConnectionErrorMessage)
                        cb_ShowConnectionError(XMLHttp.status);

                    // Reset Input Type
                    setTimeout(function () { if (OldObjectType === ""submit"") obj.type = ""submit""; }, 1);

                    if (obj.hasAttribute(""main-type""))
                        obj.removeAttribute(""main-type"");

                    cb_HideLoader();

                    if (resolveCallback)
                        resolveCallback();
                }
            }

            Form.focus();

            // Reset Input Type
            setTimeout(function () { if (OldObjectType === ""submit"") obj.type = ""submit""; }, 1);

            if (obj.hasAttribute(""main-type""))
                obj.removeAttribute(""main-type"");
        }
    }

    XMLHttp.onerror = function ()
    {
        if (WebFormsOptions.UseRetryRequest && retryCount < WebFormsOptions.MaxRetryCount)
        {
            setTimeout(() => { cb_PostRequestAndResponse(evt, ViewState, formElement, retryCount + 1); }, WebFormsOptions.RetryRequestInterval);
        }
        else
        {
            if (WebFormsOptions.UseConnectionErrorMessage)
                cb_ShowConnectionError();

            // Reset Input Type
            setTimeout(function () { if (OldObjectType === ""submit"") obj.type = ""submit""; }, 1);

            if (obj.hasAttribute(""main-type""))
                obj.removeAttribute(""main-type"");

            cb_HideLoader();

            if (resolveCallback)
                    resolveCallback();
        }

        // Clean Progress Value
        if (WebFormsOptions.UseProgressBar)
            cb_CleanProgressValue();

        // Reset Input Type
        setTimeout(function () { if (OldObjectType === ""submit"") obj.type = ""submit""; }, 1);

        if (obj.hasAttribute(""main-type""))
            obj.removeAttribute(""main-type"");
    }

    XMLHttp.upload.onprogress = function (event)
    {
        if (WebFormsOptions.HideLoaderWhenUpload && event.lengthComputable && event.loaded >= WebFormsOptions.HideLoaderAfterUploaded)
            cb_HideLoader();
    };

    var formDataSerialize = cb_FormDataSerialize(Form, obj.getAttribute(""name""), TagSubmitValue, OldObjectType, FormIsMultiPart);
    if ((FormMethod.toLowerCase() != ""post"") && (FormMethod.toLowerCase() != ""put""))
    {
        FormAction = cb_AddQueryToUrl(FormAction, formDataSerialize);
        formDataSerialize = """";
    }
        
    XMLHttp.open(FormMethod, FormAction, true);

    if (WebFormsOptions.UseProgressBar && cb_HasFileInput(Form))
        XMLHttp.upload.addEventListener(""progress"", cb_ProgressHandler, false);

    if (!FormIsMultiPart)
        XMLHttp.setRequestHeader(""Content-Type"", ""application/x-www-form-urlencoded"");

    XMLHttp.setRequestHeader(""Post-Back"", ""true"");

    if (FormIsMultiPart && ""CompressionStream"" in window && cb_HasFileInput(Form) && WebFormsOptions.UseGzipFileSend)
    {
        // Gzip Files
        (async () =>
        {
            const newFormData = new FormData();
            for (const [key, value] of formDataSerialize.entries())
            {
                if (value instanceof File)
                {
                    const ext = value.name.split('.').pop().toLowerCase();

                    // Check Ignore List
                    if (WebFormsOptions.UseGzipFileSendIgnoreList.includes(ext))
                    {
                        // The File Is Added Unchanged.
                        newFormData.append(key, value);
                        continue;
                    }

                    const arrayBuffer = await value.arrayBuffer();
                    const cs = new CompressionStream(""gzip"");
                    const compressedStream = new Blob([arrayBuffer]).stream().pipeThrough(cs);
                    const compressedBlob = await new Response(compressedStream).blob();
                    const gzippedFile = new File([compressedBlob], value.name + "".gz"", { type: ""application/gzip"" });
                    newFormData.append(key, gzippedFile, gzippedFile.name);
                }
                else
                    newFormData.append(key, value);
            }
            XMLHttp.setRequestHeader(""X-Files-Gzip"", ""true"");
            XMLHttp.send(newFormData);
        })();
    }
    else if (!FormIsMultiPart && ""CompressionStream"" in window && formDataSerialize && WebFormsOptions.UseGzipDataSend && WebFormsOptions.UseGzipDataSendLargerThan <= (new TextEncoder().encode(formDataSerialize).length))
    {
        // Gzip All Data (Except for multipart)
        (async () =>
        {
            const dataArray = new TextEncoder().encode(formDataSerialize);
            const cs = new CompressionStream(""gzip"");
            const compressedStream = new Blob([dataArray]).stream().pipeThrough(cs);
            const compressedBlob = await new Response(compressedStream).blob();
            XMLHttp.setRequestHeader(""Content-Encoding"", ""gzip"");
            XMLHttp.send(compressedBlob);
        })();
    }
    else
        formDataSerialize ? XMLHttp.send(formDataSerialize) : XMLHttp.send();
}

function PostBack(evt, ViewState)
{
    evt = cb_PreServedEvent(evt);

    if (evt.target)
        if (evt.target.type)
            if (evt.target.type.toLowerCase() == ""submit"")
                evt.preventDefault();

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_PostRequestAndResponse(evt, ViewState, null, 0, resolve);
    }));
}
window.PostBack = PostBack;

/* End Post-Back */

/* Start Request And Response */

function cb_RequestAndResponse(evt, FormAction, ViewState, Method, retryCount = 0, resolveCallback)
{
    cb_ShowLoader();

    evt = evt || cb_FakeEvent();
    evt = cb_PreServedEvent(evt);

    // Create Request Name
    var RequestNameForCache = '<';
    if (!FormAction)
        FormAction = """";
    var RequestName = (FormAction == """") ? window.location.pathname : FormAction;
    if (FormAction.length > 0)
    {
        if (FormAction.substring(0, 1) == '#')
            RequestName = window.location.pathname + FormAction;

        if (FormAction.Contains('#'))
            RequestNameForCache = '#' + FormAction.GetTextAfter('#');
    }

    // Check Cache
    if (cb_UsedCache(evt, RequestName, RequestNameForCache))
    {
        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    // Using WebSocket Protocol
    var tmpFormAction = (FormAction == """") ? window.location.pathname : FormAction;
    if (window.WebSocket && (cb_UseWebSocket || (cb_UseWebSocketPath.indexOf(tmpFormAction) >= 0)))
    {
        if (cb_UseWebSocket == '@')
            cb_UseWebSocket = false;

        cb_WebSocketSet(tmpFormAction);

        if (cb_WebSockets[tmpFormAction])
        {
            cb_WebSockets[tmpFormAction].onmessage = function (event)
            {
                var WebSocketResult = event.data;
                cb_SetResponse(evt, WebSocketResult, ViewState, RequestName);

                if (WebFormsOptions.AddLogForWebSockets)
                    console.log(""WebSocket server response:\n"" + event.data);
            };
        }

        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    // Using Http Protocol
    var XMLHttp = new XMLHttpRequest();
    XMLHttp.onreadystatechange = function ()
    {
        if (XMLHttp.readyState == 4)
        {
            if (XMLHttp.status >= 200 && XMLHttp.status < 300)
            {
                if (XMLHttp.status != 202 && XMLHttp.status != 204)
                {
                    var HttpResult = XMLHttp.responseText;
                    if (Method != ""HEAD"")
                        cb_SetResponse(evt, HttpResult, ViewState, RequestName);

                    if (evt.currentTarget && evt.currentTarget.tagName)
                    {
                        var IsSPALink = evt.currentTarget.tagName.toLowerCase() == 'a';
                        if (IsSPALink)
                        {
                            const linkPath = evt.currentTarget.getAttribute(""href"");
                            const linkTitle = (WebFormsOptions.SetTitleBySPALink && (evt.currentTarget.hasAttribute(""title"")) ? WebFormsOptions.SPAGlobalTitle + evt.currentTarget.getAttribute(""title"") : null);

                            if (linkTitle)
                                document.title = linkTitle;

                            setTimeout(() => { cb_SPA.saveState(linkPath, linkTitle) }, WebFormsOptions.SPASaveStateDelay);
                        }
                    }                   
                }

                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();
            }
            else if (XMLHttp.status >= 400 && XMLHttp.status < 500)
            {
                if (WebFormsOptions.UseConnectionErrorMessage)
                    cb_ShowConnectionError(XMLHttp.status);

                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();
            }
            else if (XMLHttp.status >= 500 && XMLHttp.status < 600)
            {
                if (WebFormsOptions.UseRetryRequest && retryCount < WebFormsOptions.MaxRetryCount)
                {
                    setTimeout(() => { cb_RequestAndResponse(evt, FormAction, ViewState, Method, retryCount + 1); }, WebFormsOptions.RetryRequestInterval);
                }
                else
                {
                    if (WebFormsOptions.UseConnectionErrorMessage)
                        cb_ShowConnectionError(XMLHttp.status);

                    cb_HideLoader();

                    if (resolveCallback)
                        resolveCallback();
                }
            }
        }
    }

    XMLHttp.onerror = function ()
    {
        if (WebFormsOptions.UseRetryRequest && retryCount < WebFormsOptions.MaxRetryCount)
        {
            setTimeout(() => { cb_RequestAndResponse(evt, FormAction, ViewState, Method, retryCount + 1); }, WebFormsOptions.RetryRequestInterval);
        }
        else
        {
            if (WebFormsOptions.UseConnectionErrorMessage)
                cb_ShowConnectionError();

            cb_HideLoader();

            if (resolveCallback)
                resolveCallback();
        }
    }

    XMLHttp.open(Method, FormAction, true);

    XMLHttp.setRequestHeader(""Post-Back"", ""true"");

    XMLHttp.send();
}

function GetBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, ""GET"", 0, resolve);
    }));
}
window.GetBack = GetBack;

function PatchBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, ""PATCH"", 0, resolve);
    }));
}
window.PatchBack = PatchBack;

function DeleteBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, ""DELETE"", 0, resolve);
    }));
}
window.DeleteBack = DeleteBack;

function HeadBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, ""HEAD"", 0, resolve, 0, resolve);
    }));
}
window.HeadBack = HeadBack;

function OptionsBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, ""OPTIONS"", 0, resolve, 0, resolve);
    }));
}
window.OptionsBack = OptionsBack;

function TraceBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, ""TRACE"", 0, resolve);
    }));
}
window.TraceBack = TraceBack;

function ConnectBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, ""CONNECT"", 0, resolve);
    }));
}
window.ConnectBack = ConnectBack;

/* End Request And Response */

/* Start Set Response Value */

function cb_SetResponse(evt, ResponseResult, ViewState, RequestName)
{
	var IsWebForms = false;

	// Check Exist WebForms Values
    if (ResponseResult.TrimStart().length >= 11)
        if (ResponseResult.TrimStart().substring(0, 11) == ""[web-forms]"")
		{
            ResponseResult = ResponseResult.TrimStart();
			IsWebForms = true;
		}

	if (IsWebForms)
        cb_SetWebFormsValues(evt, RequestName, ResponseResult, true);
	else
    {
        var TmpDiv = document.createElement(""div"");
        TmpDiv.innerHTML = cb_RemoveScripts(ResponseResult).toDOM();

		if (ViewState)
		{
			if (typeof ViewState === ""string"")
			{
				var ViewStateObject = cb_GetElementByElementPlace(ViewState);
                ViewStateObject.replaceChildren(TmpDiv);
                cb_AppendJavaScriptTag(ResponseResult);
				cb_Initialization(ViewStateObject.getElementsByTagName(""div"")[0]);
                if (!WebFormsOptions.SetResponseInsideDivTag)
                {
                    var divElement = ViewStateObject.getElementsByTagName(""div"")[0];
                    divElement.replaceChildren(...divElement.childNodes);
                }
			}
			else if (typeof ViewState === ""object"")
			{
                ViewState.replaceChildren(TmpDiv);
                cb_AppendJavaScriptTag(ResponseResult);
                cb_Initialization(ViewState.getElementsByTagName(""div"")[0]);
                if (!WebFormsOptions.SetResponseInsideDivTag)
                {
                    var divElement = ViewState.getElementsByTagName(""div"")[0];
                    divElement.replaceChildren(...divElement.childNodes);
                }
			}
			else
			{
                cb_GetResponseLocation().prepend(TmpDiv);
                cb_AppendJavaScriptTag(ResponseResult);
				cb_Initialization(cb_GetResponseLocation().getElementsByTagName(""div"")[0]);
                if (!WebFormsOptions.SetResponseInsideDivTag)
                {
                    var divElement = cb_GetResponseLocation().getElementsByTagName(""div"")[0];
                    divElement.replaceChildren(...divElement.childNodes);
                }
			}
		}
        else if (ResponseResult || !WebFormsOptions.IgnoreEmptyResult)
        {
            cb_GetResponseLocation().replaceChildren(...(WebFormsOptions.SetResponseInsideDivTag ? [TmpDiv] : TmpDiv.childNodes));
            cb_AppendJavaScriptTag(ResponseResult);
			cb_Initialization(cb_GetResponseLocation());
        }
	}
}

/* End Set Response Value */

/* Start Tag-Back */

function TagBack(evt, OutputPlace)
{
    cb_ShowLoader();
    evt = evt || cb_FakeEvent();

    var elementPlace = cb_GetElementByElementPlace(OutputPlace);
    var ActionControls = elementPlace.getAttribute(""ac"");
    cb_SetWebFormsValues(evt, """", ActionControls, false, true);
    cb_HideLoader();
}
window.TagBack = TagBack;

/* End Tag-Back */

/* Start Comment-Back */

function CommentBack(evt, index, OutputPlace)
{
    cb_ShowLoader();
    evt = evt || cb_FakeEvent();

    var elementPlace = OutputPlace ? cb_GetElementByElementPlace(OutputPlace) : null;

    if (index)
        index = '#' + index;

    cb_SetWebFormsCommentsValue(elementPlace, index, true);

    cb_HideLoader();
}
window.CommentBack = CommentBack;

/* End Comment-Back */

/* Start Wasm-Back */

async function WasmBack(evt, wasmLanguage, wasmUrl, funcName, args, OutputPlace)
{
    cb_ShowLoader();
    evt = evt || cb_FakeEvent();

    for (let i = 0; i < args.length; i++)
        if (args[i] == ""string"")
            args[i] = args[i].Replace(""$[co];"", ',');

    args = await cb_SetDynamicValueForArgs(evt, args);

    var result = await cb_RunWasmMethodResult(wasmLanguage, wasmUrl, funcName, args);

    cb_SetResponse(evt, String(result), OutputPlace, """");

    cb_HideLoader();
}
window.WasmBack = WasmBack;

/* End Wasm-Back */

/* Start Front-Back */

async function FrontBack(evt, modulePath, OutputPlace, ...args)
{
    cb_ShowLoader();

    if (WebFormsOptions.DisableLoadModule)
    {
        if (WebFormsOptions.AddLogForModule)
            console.warn(""Access for load the module is disabled but is being attempted.\nModule path: "" + modulePath);
        return null;
    }

    if (WebFormsOptions.UseLoadModulePathOnlyInAcceptedList)
        if (!WebFormsOptions.LoadModulePathOnlyInAcceptedList.some(p => cb_MatchesPattern(p, modulePath)))
        {
            if (WebFormsOptions.AddLogForModule)
                console.warn(""Access to load the module is only possible in the list, but is being attempted.\nModule path: "" + modulePath);
            return null;
        }

    evt = evt || cb_FakeEvent();

    for (let i = 0; i < args.length; i++)
        if (args[i] == ""string"")
            args[i] = args[i].Replace(""$[co];"", ',');

    args = await cb_SetDynamicValueForArgs(evt, args);

    try
    {
        const mod = await import(modulePath);
        var result = await mod[""PageLoad""](evt, ...args);

        cb_SetResponse(evt, String(result), OutputPlace, """");
    }
    catch (er)
    {
        if (WebFormsOptions.AddLog)
            console.error(""Error loading module:"", er);
    }

    cb_HideLoader();
}
window.FrontBack = FrontBack;

/* End Front-Back */

/* Start WebSocket-Back */

function WebSocketBack(evt, Path)
{
    cb_AddWebSocketPath(Path);
    GetBack(evt, Path);
}
window.WebSocketBack = WebSocketBack;

function cb_WebSocketBackWithoutQueue(evt, Path)
{
    cb_AddWebSocketPath(Path);
    cb_RequestAndResponse(evt, Path, undefined, ""GET"");
}

/* End WebSocket-Back */

/* Start Send-Back */

async function cb_SendRequestAndResponse(evt, ViewState, path, method = ""POST"", isMultiPart, contentType = ""text/plain"", data, retryCount = 0, resolveCallback)
{
    cb_ShowLoader();

    if (data)
        data = data.Replace(""$[ln];"", '\n').Replace(""$[dq];"", ""\"""").Replace(""$[sq];"", ""'"");

    if (data.startsWith(""@""))
        data = await cb_FetchValue(evt, data);

    evt = evt || cb_FakeEvent();
    evt = cb_PreServedEvent(evt);

    var obj = evt.currentTarget || null;

    if (!path)
        path = """";

    // Create Request Name
    var RequestNameForCache = '<';
    var RequestName = (path == """") ? window.location.pathname : path;
    if (path.length > 0)
    {
        if (path.substring(0, 1) == '#')
            RequestName = window.location.pathname + path;

        if (path.Contains('#'))
            RequestNameForCache = '#' + path.GetTextAfter('#');
    }
    if (obj && obj.getAttribute(""name""))
        RequestName = obj.getAttribute(""name"") + '|' + RequestName;

    // Check Cache
    if (cb_UsedCache(evt, RequestName, RequestNameForCache))
    {
        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    // Using WebSocket Protocol
    var tmpPath = (path == """") ? window.location.pathname : path;
    if (window.WebSocket && (cb_UseWebSocket || (cb_UseWebSocketPath.indexOf(tmpPath) >= 0)))
    {
        if (cb_UseWebSocket == '$')
            cb_UseWebSocket = false;

        cb_WebSocketSet(tmpPath);

        if (cb_WebSockets[tmpPath])
        {
            if (cb_WebSockets[tmpPath].readyState === WebSocket.OPEN)
                cb_WebSocketDoSend(data);
            else
            {
                cb_WebSockets[tmpPath].onopen = function ()
                {
                    cb_WebSocketDoSend(data);
                };
            }

            cb_WebSockets[tmpPath].onmessage = function (event)
            {
                var WebSocketResult = event.data;
                cb_SetResponse(evt, WebSocketResult, ViewState, RequestName);

                if (WebFormsOptions.AddLogForWebSockets)
                    console.log(""WebSocket server response:\n"" + event.data);
            };
        }

        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    // Using Http Protocol
    var XMLHttp = new XMLHttpRequest();
    XMLHttp.onreadystatechange = function ()
    {
        if (XMLHttp.readyState == 4)
        {
            if (XMLHttp.status >= 200 && XMLHttp.status < 300)
            {
                if (XMLHttp.status != 202 && XMLHttp.status != 204)
                {
                    var HttpResult = XMLHttp.responseText;
                    cb_SetResponse(evt, HttpResult, ViewState, RequestName);
                }

                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();
            }
        }
        else if (XMLHttp.status >= 400 && XMLHttp.status < 500)
        {
            if (WebFormsOptions.UseConnectionErrorMessage)
                cb_ShowConnectionError(XMLHttp.status);

            cb_HideLoader();

            if (resolveCallback)
                resolveCallback();
        }
        else if (XMLHttp.status >= 500 && XMLHttp.status < 600)
        {
            if (WebFormsOptions.UseRetryRequest && retryCount < WebFormsOptions.MaxRetryCount)
            {
                setTimeout(() => { SendBack(evt, ViewState, path, method, isMultiPart, contentType, data, retryCount + 1) }, WebFormsOptions.RetryRequestInterval);
            }
            else
            {
                if (WebFormsOptions.UseConnectionErrorMessage)
                    cb_ShowConnectionError(XMLHttp.status);

                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();
            }
        }
    }

    XMLHttp.onerror = function ()
    {
        if (WebFormsOptions.UseRetryRequest && retryCount < WebFormsOptions.MaxRetryCount)
        {
            setTimeout(() => { SendBack(evt, ViewState, path, method, isMultiPart, contentType, data, retryCount + 1) }, WebFormsOptions.RetryRequestInterval);
        }
        else
        {
            if (WebFormsOptions.UseConnectionErrorMessage)
                cb_ShowConnectionError();

            cb_HideLoader();

            if (resolveCallback)
                resolveCallback();
        }
    }

    if ((method.toLowerCase() != ""post"") && (method.toLowerCase() != ""put""))
    {
        path = cb_AddQueryToUrl(path, data);
        data = """";
    }
        
    XMLHttp.open(method, path, true);

    if (!isMultiPart)
        XMLHttp.setRequestHeader(""Content-Type"", contentType);

    XMLHttp.setRequestHeader(""Post-Back"", ""true"");

    if (!isMultiPart && ""CompressionStream"" in window && data && WebFormsOptions.UseGzipDataSend && WebFormsOptions.UseGzipDataSendLargerThan <= (new TextEncoder().encode(data).length))
    {
        // Gzip All Data (Except for multipart)
        (async () =>
        {
            const dataArray = new TextEncoder().encode(data);
            const cs = new CompressionStream(""gzip"");
            const compressedStream = new Blob([dataArray]).stream().pipeThrough(cs);
            const compressedBlob = await new Response(compressedStream).blob();
            XMLHttp.setRequestHeader(""Content-Encoding"", ""gzip"");
            XMLHttp.send(compressedBlob);
        })();
    }
    else if (data)
    {
        if (isMultiPart)
        {
            const formData = new FormData();
            formData.append(""content"", data);
            formData.append(""type"", contentType);
            XMLHttp.send(formData);
        }
        else
            XMLHttp.send(data);
    }
    else
        XMLHttp.send();
}

function SendBack(evt, ViewState, path, method, isMultiPart, contentType, data)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_SendRequestAndResponse(evt, ViewState, path, method, isMultiPart, contentType, data, 0, resolve);
    }));
}
window.SendBack = SendBack;

/* End Send-Back */

/* Start SSE-Back */

function SSEBack(evt, path, shouldReconnect = true, reconnectTryTimeout = 3000, viewState)
{
    cb_ShowLoader();
    cb_ConnectToSSE(evt, path, shouldReconnect, reconnectTryTimeout, viewState);
    cb_HideLoader();
}
window.SSEBack = SSEBack;

/* End SSE-Back */

/* Start Form Data */

function cb_FormDataSerialize(form, TagSubmitName, TagSubmitValue, TagSubmitType, FormIsMultiPart)
{   
    var FormString = """";
    var TmpFormData = new FormData();

    if (!form || form.nodeName.toLowerCase() != ""form"")
        return;

    const useOnlyChanged = form.hasAttribute(""use-only-change-update"");

    for (var i = form.elements.length - 1; i >= 0; i = i - 1)
    {
        let el = form.elements[i];
        if (el.name === """" || el.disabled)
            continue;

        var parent = el.parentElement;
        var skip = false;
        while (parent)
        {
            if (parent.tagName.toLowerCase() === ""fieldset"" && parent.disabled)
            {
                skip = true;
                break;
            }
            parent = parent.parentElement;
        }
        if (skip)
            continue;

        let firstValue = useOnlyChanged ? el.getAttribute(""cb-first-value"") : undefined;

        switch (el.nodeName.toLowerCase())
        {
            case ""input"":
                switch (el.type.toLowerCase())
                {
                    case ""text"":
                    case ""number"":
                    case ""hidden"":
                    case ""password"":
                    case ""reset"":
                    case ""color"":
                    case ""date"":
                    case ""range"":
                    case ""search"":
                    case ""time"":
                    case ""datetime-local"":
                    case ""email"":
                    case ""month"":
                    case ""tel"":
                    case ""url"":
                    case ""week"":
                        {
                            if (firstValue === el.value)
                                continue;

                            if (FormIsMultiPart)
                                TmpFormData.append(el.name, el.value);
                            else
                                FormString += el.name + '=' + encodeURIComponent(el.value) + '&';
                        }
                        break;
                    case ""checkbox"":
                    case ""radio"":
                        if (el.checked)
                        {
                            if (FormIsMultiPart)
                                TmpFormData.append(el.name, el.value);
                            else
                                FormString += el.name + '=' + el.value + '&';
                        }
                        break;
                    case ""file"":
                        {
                            var files = el.files;

                            if (files.length == 0)
                                break;

                            for (var k = 0; k < files.length; k++)
                            {
                                var file = files[k];
                                if (FormIsMultiPart)
                                    TmpFormData.append(el.name, file, file.name);
                                else
                                    FormString += el.name + '=' + encodeURIComponent(file.name) + '&';
                            }
                        }
                        break;
                }
                break;
            case ""textarea"":
                {
                    if (firstValue === el.value)
                        continue;

                    if (FormIsMultiPart)
                        TmpFormData.append(el.name, el.value);
                    else
                        FormString += el.name + '=' + encodeURIComponent(el.value) + '&';
                }
                break;
            case ""output"":
                {
                    if (firstValue === el.textContent)
                        continue;

                    if (FormIsMultiPart)
                        TmpFormData.append(el.name, el.textContent);
                    else
                        FormString += el.name + '=' + encodeURIComponent(el.textContent) + '&';
                }
                break;
            case ""select"":
                switch (el.type.toLowerCase())
                {
                    case ""select-one"":
                        {
                            if (firstValue === el.value)
                                continue;

                            if (FormIsMultiPart)
                                TmpFormData.append(el.name, el.value);
                            else
                                FormString += el.name + '=' + encodeURIComponent(el.value) + '&';
                        }
                        break;
                    case ""select-multiple"":
                        let selectedValues = [];
                        for (let option of el.options)
                            if (option.selected)
                                selectedValues.push(option.value);

                        if (firstValue && firstValue.split(',').sort().join(',') === selectedValues.sort().join(','))
                            continue;

                        for (var j = el.options.length - 1; j >= 0; j = j - 1)
                        {
                            if (el.options[j].selected)
                            {
                                if (FormIsMultiPart)
                                    TmpFormData.append(el.name, el.options[j].value);
                                else
                                    FormString += el.name + '=' + encodeURIComponent(el.options[j].value) + '&';
                            }
                        }
                        break;
                }
                break;
        }
    }

    // Add Button Submit
    if (TagSubmitType === ""submit"")
    {
        if (FormIsMultiPart)
            TmpFormData.append(TagSubmitName, TagSubmitValue);
        else
            FormString += TagSubmitName + '=' + encodeURIComponent(TagSubmitValue);
    }
    else if (!FormIsMultiPart && FormString.length > 0)
        FormString = FormString.substring(0, FormString.length - 1);

    // Add Checksum Value
    if (WebFormsOptions.SendChecksum)
        if (FormIsMultiPart)
        {
            let checksumSource = """";
            for (let [key, value] of TmpFormData.entries())
                checksumSource += key + '=' + value + '&';

            if (checksumSource.endsWith('&'))
                checksumSource = checksumSource.slice(0, -1);

            TmpFormData.append(WebFormsOptions.ChecksumName, cb_Checksum(checksumSource, true));
        }
        else
            FormString += '&' + WebFormsOptions.ChecksumName + '=' + cb_Checksum(FormString, true);

    return FormIsMultiPart ? TmpFormData : FormString;
}

function cb_UseOnlyChangeUpdate(currentElement)
{
    if (!currentElement)
        return;

    currentElement.setAttribute(""use-only-change-update"", ""true"");

    const fields = currentElement.querySelectorAll(""input, textarea, select"");

    for (let el of fields)
    {
        if (!el.name || el.disabled)
            continue;

        let value = """";

        switch (el.nodeName.toLowerCase())
        {
            case ""input"":
                switch (el.type.toLowerCase())
                {
                    case ""checkbox"":
                    case ""radio"":
                    case ""file"":
                        continue;
                    default:
                        value = el.value;
                        break;
                }
                break;

            case ""textarea"":
                value = el.value;
                break;

            case ""output"":
                value = el.textContent;
                break;

            case ""select"":
                if (el.type === ""select-one"")
                    value = el.value;
                else if (el.type === ""select-multiple"")
                {
                    let selectedValues = [];
                    for (let option of el.options)
                        if (option.selected)
                            selectedValues.push(option.value);
                    value = selectedValues.join(',');
                }
                break;

            default:
                continue;
        }

        el.setAttribute(""cb-first-value"", value);
    }
}

/* End Form Data */

/* Start Append Java Script */

function cb_ExtractScriptTags(Html)
{
    var ScriptList = new Array();
    const regex = /<script[^>]*>([\s\S]*?)<\/script>/g;
    let match;

    while ((match = regex.exec(Html)) !== null)
    {
        const ScriptTag = document.createElement(""script"");
        const ScriptContent = match[1];

        // Extract Attributes
        const AttrRegex = /([a-zA-Z0-9_]+)=""([^""]*)""/g;
        let AttrMatch;

        while ((AttrMatch = AttrRegex.exec(match[0])) !== null)
        {
            const Name = AttrMatch[1];
            const Value = AttrMatch[2];
            ScriptTag.setAttribute(Name, Value);
        }

        const TextNode = document.createTextNode(ScriptContent);

        ScriptTag.appendChild(TextNode);
        ScriptList.push(ScriptTag);
    }

    return ScriptList;
}

function cb_IsScriptAlreadyInDOM(script)
{
    const allScripts = document.querySelectorAll(""script"");

    for (let i = 0; i < allScripts.length; i++)
    {
        const existing = allScripts[i];

        if (script.src)
        {
            if (existing.src === script.src)
                return true;
        }
        else
        {
            if (existing.textContent.trim() === script.textContent.trim())
                return true;
        }
    }

    return false;
}

function cb_AppendJavaScriptTag(HtmlSource)
{
    if (WebFormsOptions.DisableAppendJavaScriptTag)
    {
        if (WebFormsOptions.AddLog)
            console.warn(""Access to the JavaScript is disabled but is being attempted."");
        return;
    }

    var ScriptList = cb_ExtractScriptTags(HtmlSource);

    for (var i = 0; i < ScriptList.length; i++)
    {
        const script = ScriptList[i];

        if (cb_IsScriptAlreadyInDOM(script))
        {
            if (WebFormsOptions.AddLog)
                console.error(""Warning: Exist duplicate script!\nThis issue occurs when the WebForms Core technology is incorrectly configured."");
            continue;
        }

        document.body.appendChild(script);
    }
}

function cb_RemoveScripts(html)
{
    const div = document.createElement(""div"");
    div.innerHTML = html;

    div.querySelectorAll(""script"").forEach(s => s.remove());

    return div.innerHTML;
}

/* End Append Java Script */

/* Start Progress Bar */
function cb_ProgressHandler(event)
{
    var Percent = (event.loaded / event.total) * 100;

    if (event.total >= 1048576)
        document.getElementById(""div_ProgressPercentLoaded"").textContent = (event.loaded / 1048576).toFixed(1) + '(' + Math.round(Percent) + ""%)"" + "" / "" + (event.total / 1048576).toFixed(1) + "" MB"";
    else
        document.getElementById(""div_ProgressPercentLoaded"").textContent = (event.loaded / 1024).toFixed(1) + '(' + Math.round(Percent) + ""%)"" + "" / "" + (event.total / 1024).toFixed(1) + "" KB"";

    document.getElementById(""div_ProgressUploadValue"").style.width = Math.round(Percent) + '%';
}

function cb_SetProgressTag(obj, form)
{
    if (!cb_HasFileInput(form))
        return;

    if (!document.getElementById(""div_ProgressUpload""))
    {
        var DivProgressUpload = document.createElement(""div"");
        DivProgressUpload.id = ""div_ProgressUpload"";
        DivProgressUpload.setAttribute(""style"", WebFormsOptions.ProgressBarStyle);

        var DivProgressPercentLoaded = document.createElement(""div"");
        DivProgressPercentLoaded.id = ""div_ProgressPercentLoaded"";
        DivProgressPercentLoaded.setAttribute(""style"", WebFormsOptions.ProgressBarPercentLoadedStyle);

        var DivProgressUploadValue = document.createElement(""div"");
        DivProgressUploadValue.id = ""div_ProgressUploadValue"";
        DivProgressUploadValue.setAttribute(""style"", WebFormsOptions.ProgressBarValueStyle);

        DivProgressUpload.appendChild(DivProgressPercentLoaded);
        DivProgressUpload.appendChild(DivProgressUploadValue);

        if (obj.parentElement)
            obj.parentElement.appendChild(DivProgressUpload);
        else
            document.body.appendChild(DivProgressUpload);
    }
}

function cb_CleanProgressValue()
{
    if (document.getElementById(""div_ProgressUploadValue""))
        document.getElementById(""div_ProgressUpload"").remove();
}

function cb_HasFileInput(Form)
{
    if (Form.getElementsByTagName(""file"").length > 0)
        return true;

    var InputCount = Form.getElementsByTagName(""input"").length;

    for (var i = 0; i < InputCount; i++)
        if (Form.getElementsByTagName(""input"").item(i).hasAttribute(""type""))
            if (Form.getElementsByTagName(""input"").item(i).getAttribute(""type"").toLowerCase() == ""file"")
                return true;

    return false;
}

/* End Progress Bar */

/* Start Web-Forms Tags */

function cb_SetWebFormsTagsValue(obj)
{
    const WebFormsTags = (obj) ? obj.querySelectorAll(""web-forms"") : document.querySelectorAll(""web-forms"");

    WebFormsTags.forEach(function (WebForms)
    {
        if (WebForms.hasAttribute(""done""))
            return;

        WebForms.setAttribute(""done"", ""true"");

        if (WebForms.hasAttribute(""src""))
        {
            WebForms.style.backgroundColor = WebFormsOptions.WebFormsTagsBackgroundColor;
            if (WebForms.hasAttribute(""width""))
                WebForms.style.width = WebForms.getAttribute(""width"");
            if (WebForms.hasAttribute(""height""))
                WebForms.style.height = WebForms.getAttribute(""height"");

            var Src = WebForms.getAttribute(""src"");
            if (Src)
                cb_RequestAndResponse(document, Src, WebForms, ""GET"");

            WebForms.style.backgroundColor = ""unset"";
        }

        if (WebForms.hasAttribute(""ac""))
        {
            var ActionControl = WebForms.getAttribute(""ac"");
            if (ActionControl)
                cb_SetWebFormsValues(document, """", ActionControl.Replace(""$[dq];"", ""\""""), false, true);
        }
    });
}

/* End Web-Forms Tags */

/* Start Web-Forms Comment */

function cb_SetWebFormsCommentsValue(obj, requestName = """", breakDone = false)
{
    const root = obj || document;

    const walker = document.createTreeWalker(root, NodeFilter.SHOW_COMMENT);
    let node;

    while ((node = walker.nextNode()))
    {
        if (!node.nodeValue.trim().startsWith(""[web-forms]""))
            continue;

        if (node._done && !breakDone)
            continue;
        node._done = true;

        const rawData = node.nodeValue.trim();

        if (!rawData)
            continue;

        cb_SetWebFormsValues(document, requestName, rawData.Replace(""$[dq];"", ""\""""), true, false);
    }
}

/* End Web-Forms Comment */

/* Start Execute Web-Forms */

async function cb_RunWebFormsValues(evt, RequestName, WebFormsValues, UsePostBack, WithoutWebFormsSection, loopIndex = 0)
{
    // Initialization to Index
    var StartIndex = RequestName.Contains('#') ? RequestName.GetTextAfter('#') : """";
    var IndexHasStarted = ((StartIndex == """") || (StartIndex == '0'));
    var StartIndexIsNumber = StartIndex.IsNumber();
    var StartIndexIndex = StartIndexIsNumber ? parseInt(StartIndex) : 0;
    var IndexForStartIndex = 1;

    // Condition
    var ConditionHasStart = false;
    var ConditionIsTrue = false;
    var ConditionIsAsync = false;
    var ConditionIsAwait= false;
    var ConditionBracketHasStart = false;
    var ConditionPeriodMiliSecond = -1;
    var ConditionAsyncList = new Array();

    // Remove Request Name For Cache
    if (RequestName.length > 1)
        if (RequestName.substring(0, 1) == '<')
            RequestName = """";

    if (!WithoutWebFormsSection)
        WebFormsValues = WebFormsValues.substring(11);

    var WebFormsList = (UsePostBack) ? WebFormsValues.split('\n') : WebFormsValues.split(""$[sln];"");

    var TransientDOM = null;
    var TransientDOMPlace = null;
    var LastElementPlaceList = null;

    for (var i = loopIndex; i < WebFormsList.length; i++)
    {
        try
        {
            var ActionControl = WebFormsList[i].FullTrim();

            if (!ActionControl)
                continue;

            if (ActionControl.length > 1)
            {
                if (ActionControl == ""SH"")
                {
                    var hash = await cb_GetHashSHA256(WebFormsValues);
                    cb_ActionControlHashList.push(String(hash));
                    continue;
                }
                if (ActionControl == ""CS"")
                {
                    var checksum = cb_Checksum(WebFormsValues);
                    cb_ActionControlHashList.push(String(checksum));
                    continue;
                }
            }

            // Checking Await
            if (ConditionIsAwait)
            {
                cb_WaitForCondition(WebFormsOptions.AwaitConditionInterval, cb_CheckCondition, evt, WebFormsList[i - 1].substring(4)).then(async () =>
                {
                    await cb_RunWebFormsValues(evt, RequestName, WebFormsList.join('\n'), true, true, i);
                }).catch(() => { });

                return;
            }

            if (ConditionPeriodMiliSecond > 0)
            {
                // Add Condition
                if (ConditionAsyncList.length == 0)
                    ConditionAsyncList.push(WebFormsList[i - 1].GetTextAfter(')'));

                if (ActionControl == '{')
                {
                    ConditionAsyncList.push(ActionControl);
                    ConditionBracketHasStart = true;
                    continue;
                }

                if (ConditionBracketHasStart)
                {
                    if (ActionControl == '}')
                    {
                        ConditionAsyncList.push(ActionControl);

                        const TmpConditionAsyncList = ConditionAsyncList;

                        if (ConditionIsAsync)
                        {
                            // Is Async
                            TmpConditionAsyncList.shift();
                            cb_RunAsync(() => { cb_RunWebFormsValues(evt, """", TmpConditionAsyncList.join('\n'), true, true); });
                        }
                        else
                        {
                            // Is Async Interval
                            cb_WaitForCondition(ConditionPeriodMiliSecond, cb_CheckCondition, evt, ConditionAsyncList[0]).then(async () =>
                            {
                                TmpConditionAsyncList.shift();
                                await cb_RunWebFormsValues(evt, """", TmpConditionAsyncList.join('\n'), true, true);
                            }).catch(() => { });
                        }

                        ConditionBracketHasStart = false;
                        ConditionPeriodMiliSecond = -1;
                        ConditionAsyncList = new Array();
                    }
                    else
                        ConditionAsyncList.push(ActionControl);
                }
                else
                {
                    ConditionAsyncList.push(ActionControl);

                    const TmpConditionAsyncList = ConditionAsyncList;

                    if (ConditionIsAsync)
                    {
                        // Is Async
                        TmpConditionAsyncList.shift();
                        cb_RunAsync(() => { cb_RunWebFormsValues(evt, """", TmpConditionAsyncList.join('\n'), true, true); });
                    }
                    else
                    {
                        // Is Async Interval
                        cb_WaitForCondition(ConditionPeriodMiliSecond, cb_CheckCondition, evt, ConditionAsyncList[0]).then(async () =>
                        {
                            TmpConditionAsyncList.shift();
                            await cb_RunWebFormsValues(evt, """", TmpConditionAsyncList.join('\n'), true, true);
                        }).catch(() => { });
                    }

                    ConditionPeriodMiliSecond = -1;
                    ConditionAsyncList = new Array();
                }
                continue;
            }

            var PreRunner = new Array();
            var FirstChar = WebFormsList[i].substring(0, 1);
            var PreRunnerIndexer = 0;
            while ((FirstChar == ':') || (FirstChar == '(') || (FirstChar == ','))
            {
                PreRunner[PreRunnerIndexer] = WebFormsList[i].GetTextBefore(')');
                WebFormsList[i] = WebFormsList[i].GetTextAfter(')');
                FirstChar = WebFormsList[i].substring(0, 1);
                PreRunnerIndexer++;
            }
            if (PreRunner.length > 0)
            {
                let tmpActionControl = WebFormsList[i];
                cb_SetPreRunnerQueue(PreRunner, async () => await cb_RunWebFormsValues(evt, """", tmpActionControl, true, true));
                continue;
            }

            // Set Dynamic Value
            if (ActionControl.Contains('='))
                ActionControl = ActionControl.GetTextBefore('=') + '=' + (await cb_SetDynamicValue(evt, ActionControl.GetTextAfter('='), '|')).Replace('\n', ""$[ln];"");
            var Value = ActionControl.GetTextAfter('=').Replace(""$[ln];"", '\n');

            if (ConditionHasStart)
            {
                if (ActionControl == '{')
                {
                    ConditionBracketHasStart = true;
                    continue;
                }

                if (ConditionBracketHasStart)
                {
                    if (ActionControl == '}')
                    {
                        ConditionBracketHasStart = false;
                        ConditionHasStart = false;
                        ConditionIsTrue = false;
                        ConditionIsAsync = false;
                        continue;
                    }
                }

                if (!ConditionIsTrue)
                {
                    if (!ConditionBracketHasStart)
                    {
                        ConditionHasStart = false;
                        ConditionIsAsync = false;
                        ConditionBracketHasStart = false;
                    }
                    continue;
                }           
            }

            // Checking Index Process
            if (IndexHasStarted)
            {
                if (ActionControl.substring(0, 1) == '#')
                    break;
            }
            else
            {
                if (StartIndexIsNumber)
                {
                    if (ActionControl.substring(0, 1) == '#')
                        if (StartIndexIndex == IndexForStartIndex)
                            IndexHasStarted = true;
                        else
                            IndexForStartIndex++;
                }   
                else
                    if (ActionControl == (""#="" + StartIndex))
                        IndexHasStarted = true;

                    continue;
            }

            if (FirstChar == ';')
                break;

            if (ActionControl == '{')
                continue;

            var SecondChar = ActionControl.substring(1, 2);
            switch (FirstChar)
            {
                case '{':
                    if (SecondChar == '(')                   
                        ConditionPeriodMiliSecond = ActionControl.GetTextAfter('(').GetTextBefore(')');
                    else
                        ConditionHasStart = true;

                    if (ConditionPeriodMiliSecond == '0')
                        ConditionIsAwait= true;
                    else if (ConditionPeriodMiliSecond == ""-1"")
                        ConditionIsTrue = await cb_CheckCondition(evt, ActionControl.substring(1));
                    else if (ConditionPeriodMiliSecond == 'a')
                    {
                        ConditionIsTrue = true;
                        ConditionIsAsync = true;
                        ConditionPeriodMiliSecond = 3600000;
                    }
                    continue;

                case '_':
                    var ScriptValue = Value.Replace(""$[ln];"", ""\n"").FullTrim();
                    if (WebFormsOptions.DisableEval)
                    {
                        if (WebFormsOptions.AddLog)
                            console.warn(""Access to the eval method is disabled but is being attempted.\nScript value: "" + ScriptValue);
                        continue;
                    }
                    eval(ScriptValue);
                    continue;

                case 'l':
                    switch (SecondChar)
                    {
                        case 'm':
                        case 'M':
                        if (Value.Contains('|'))
                        {
                            var funcName = Value.GetTextBefore('|');
                            const args = Value.GetTextAfter('|').split('|');

                            if (SecondChar == 'm')
                                await cb_RunMethod(evt, funcName, args);
                            else
                                await cb_RunModuleMethod(evt, funcName, args);
                            continue;
                        }
                        
                        if (SecondChar == 'm')
                            await cb_RunMethod(evt, Value);
                        else
                            await cb_RunModuleMethod(evt, Value);
                        continue;

                        case 'r': location.reload(); continue;
                        case 'h': location.href = Value; continue;

                        case 'A':
                            var currentEvent = (Value.GetTextBefore('|') == '1') ? evt : cb_FakeEvent();
                            var Value = Value.GetTextAfter('|');
                            var withoutWebFormsSection = (Value.GetTextBefore('|') == '1');
                            Value = Value.GetTextAfter('|');
                            var index = Value.GetTextBefore('|') ? '#' + index : """";
                            var actionControls = Value.GetTextAfter('|');
                            
                            cb_SetWebFormsValues(currentEvent, index, actionControls, true, withoutWebFormsSection);
                            continue;
                    }
                    break;

                case 'D':
                    switch (SecondChar)
                    {
                        case 'e': await new Promise(resolve => setTimeout(resolve, Value)); continue;
                        case 'i': cb_DeletePreRunnerInterval(Value); continue;
                        case 's':
                            if (Value)
                                cb_DisconnectSSE(Value);
                            else
                                cb_DisconnectAllSSE();
                            continue;
                        case 'S':
                            if (Value == '*')
                                cb_SPA.clearAllStates();
                            else if (Value)
                                cb_SPA.deleteState(Value);
                            else
                                cb_SPA.deleteState(window.location.pathname);
                            continue;
                    }
                    break;

                case '.':
                    switch (SecondChar)
                    {
                        case 'C': cb_StorageSet(Value.GetTextBefore('|'), Value.GetTextAfter('|')); continue;
                        case 'D': cb_StorageDelete(Value); continue;
                        case 'a':
                            var key = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var formatChar = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');

                            switch (formatChar)
                            {
                                case 'j':
                                    var value = Value.GetTextBefore('|');
                                    var path = Value.GetTextAfter('|');
                                    cb_StorageSet(key, cb_AddJSON(cb_StorageGet(key), path, value));
                                    continue;
                                case 'x':
                                    var name = Value.GetTextBefore('|').Replace(""$[at];"", '@');

                                    if (name.length > 2)
                                        if (name.substring(0, 2) == ""@@"")
                                        {
                                            name = name.substring(1);
                                            name = '@' + (await cb_SetDynamicForValue(evt, name));
                                        }

                                    Value = Value.GetTextAfter('|');
                                    var value = Value.GetTextBefore('|');
                                    var path = Value.GetTextAfter('|');
                                    cb_StorageSet(key, cb_AddXML(cb_StorageGet(key), path, name, value));
                                    continue;
                                case 'i':
                                    var isINILike = Value.GetTextBefore('|') == '1';
                                    Value = Value.GetTextAfter('|');
                                    var value = Value.GetTextBefore('|');
                                    var path = Value.GetTextAfter('|');
                                    cb_StorageSet(key, cb_AddINI(cb_StorageGet(key), path, value, isINILike));
                                    continue;
                                case 't':
                                    var text = Value.GetTextBefore('|');
                                    var line = Value.GetTextAfterLast('|');
                                    cb_StorageSet(key, cb_AppendTextLine(cb_StorageGet(key), line, text));
                                    continue;
                                case 'v': cb_StorageSet(key, Value); continue;
                            }
                            break;
                        case 'u':
                            var key = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var formatChar = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');

                            switch (formatChar)
                            {
                                case 'j':
                                    var value = Value.GetTextBefore('|');
                                    var path = Value.GetTextAfter('|');
                                    cb_StorageSet(key, cb_SetJSON(cb_StorageGet(key), path, value));
                                    continue;
                                case 'x':
                                    var value = Value.GetTextBefore('|');
                                    var path = Value.GetTextAfter('|');
                                    cb_StorageSet(key, cb_SetXML(cb_StorageGet(key), path, value));
                                    continue;
                                case 'i':
                                    var isINILike = Value.GetTextBefore('|') == '1';
                                    Value = Value.GetTextAfter('|');
                                    var value = Value.GetTextBefore('|');
                                    var path = Value.GetTextAfter('|');
                                    cb_StorageSet(key, cb_UpdateINI(cb_StorageGet(key), path, value, isINILike));
                                    continue;
                                case 't':
                                    var text = Value.GetTextBefore('|');
                                    var line = Value.GetTextAfterLast('|');
                                    cb_StorageSet(key, cb_SetTextLine(cb_StorageGet(key), line, text));
                                    continue;
                                case 'v': cb_StorageSet(key, Value); continue;
                            }
                            break;
                        case 'i':
                            var key = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var formatChar = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');

                            switch (formatChar)
                            {
                                case 'v':
                                    var text = Value.GetTextBefore('|');
                                    var line = Value.GetTextAfterLast('|');
                                    cb_StorageSet(key, Number(cb_StorageGet(key)) + Number(Value));
                                    continue;
                            }
                            break;
                        case 'd':
                            var key = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var formatChar = (Value.Contains('|') ? Value.GetTextBefore('|') : Value);
                            Value = Value.GetTextAfter('|');

                            switch (formatChar)
                            {
                                case 'j': cb_StorageSet(key, cb_DeleteJSON(cb_StorageGet(key), Value)); continue;
                                case 'x': cb_StorageSet(key, cb_DeleteXML(cb_StorageGet(key), Value)); continue;
                                case 'i':
                                    var isINILike = Value.GetTextBefore('|') == '1';
                                    var path = Value.GetTextAfter('|');
                                    cb_StorageSet(key, cb_DeleteINI(cb_StorageGet(key), path, isINILike));
                                    continue;
                                case 't': cb_StorageSet(key, cb_DeleteTextLine(cb_StorageGet(key), Value)); continue;
                                case 'v': cb_StorageDelete(key); continue;
                            }
                    }
                    break;

                case 'w':
                    switch (SecondChar)
                    {
                        case 'g': history.go(Value); continue;
                        case 's': window.scrollTo(Value.GetTextBefore('|'), Value.GetTextAfter('|')); continue;
                        case 'R':
                            var [path, scobePath] = Value.split('|');
                            await cb_ServiceWorker.register(path, scobePath);
                            await navigator.serviceWorker.ready;
                            continue;
                        case 'p': await cb_ServiceWorker.preCacheStatic(Value.split('|')); continue;
                        case 'c':
                            var [path, seconds] = Value.split('|');
                            await cb_ServiceWorker.cache.add(path, seconds);
                            continue;
                        case 'd':
                            if (Value)
                                await cb_ServiceWorker.cache.remove(Value);
                            else
                                await cb_ServiceWorker.cache.clear();
                            continue;
                        case 't':
                            var [path, seconds] = Value.split('|');
                            await cb_ServiceWorker.cache.setTTL(path, seconds);
                            continue;
                        case 'r':
                            var [path, type, cacheDynamic] = Value.split('|');
                            cacheDynamic = (cacheDynamic == '1');
                            await cb_ServiceWorker.routeSet(path, type, cacheDynamic);
                            continue;
                        case 'a':
                            var [path, to] = Value.split('|');
                            await cb_ServiceWorker.routeAlias(path, to);
                            continue;
                        case 'C':
                            await cb_ServiceWorker.routeRemoveAlias(Value);
                            continue;
                        case 'D':
                            if (Value)
                                await cb_ServiceWorker.routeRemove(Value);
                            else
                                await cb_ServiceWorker.routeClear();
                            continue;
                    }
                    break;

                case 'L':
                    var control = Value;
                    var currentEvent;
                    var callMethodSecondArg;
                    var callMethodThirdArg;

                    if (control.Contains('|'))
                    {
                        currentEvent = (control.GetTextBefore('|') == '1') ? evt : cb_FakeEvent();
                        control = control.GetTextAfter('|');

                        if (control.Contains('|'))
                        {
                            callMethodSecondArg = control.GetTextBefore('|');
                            callMethodThirdArg = control.GetTextAfter('|');
                        }
                        else
                            callMethodSecondArg = control;
                    }
                    else
                        currentEvent = (control == '1') ? evt : cb_FakeEvent();

                    switch (SecondChar)
                    {
                        case 'p': cb_PostRequestAndResponse(currentEvent, callMethodThirdArg, callMethodSecondArg); continue;
                        case 't': TagBack(currentEvent, callMethodSecondArg); continue;
                        case 'C': CommentBack(currentEvent, callMethodSecondArg, callMethodThirdArg); continue;
                        case 'y':
                            var wasmUrl = callMethodThirdArg.GetTextBefore('|');
                            callMethodThirdArg = callMethodThirdArg.GetTextAfter('|');
                            var funcName = callMethodThirdArg.GetTextBefore('|');
                            callMethodThirdArg = callMethodThirdArg.GetTextAfter('|')
                            var args = callMethodThirdArg.GetTextBefore('|');
                            outputPlace = callMethodThirdArg.GetTextAfter('|');

                            await WasmBack(currentEvent, callMethodSecondArg, wasmUrl, funcName, args.split(','), outputPlace);
                            continue;
                        case 'w': cb_WebSocketBackWithoutQueue(currentEvent, callMethodSecondArg); continue;
                        case 'g': cb_RequestAndResponse(currentEvent, callMethodSecondArg, callMethodThirdArg, ""GET""); continue;
                        case 'P': cb_RequestAndResponse(currentEvent, callMethodSecondArg, callMethodThirdArg, ""PATCH""); continue;
                        case 'd': cb_RequestAndResponse(currentEvent, callMethodSecondArg, callMethodThirdArg, ""DELETE""); continue;
                        case 'h': cb_RequestAndResponse(currentEvent, callMethodSecondArg, callMethodThirdArg, ""HEAD""); continue;
                        case 'o': cb_RequestAndResponse(currentEvent, callMethodSecondArg, callMethodThirdArg, ""OPTIONS""); continue;
                        case 'T': cb_RequestAndResponse(currentEvent, callMethodSecondArg, callMethodThirdArg, ""TRACE""); continue;
                        case 'c': cb_RequestAndResponse(currentEvent, callMethodSecondArg, callMethodThirdArg, ""CONNECT""); continue;
                        case 's':
                            var shouldReconnect = callMethodThirdArg.GetTextBefore('|');
                            callMethodThirdArg = callMethodThirdArg.GetTextAfter('|');
                            var ReconnectTryTimeout = callMethodThirdArg;
                            var outputPlace = null;
                            if (ReconnectTryTimeout.Contains('|'))
                            {
                                outputPlace = ReconnectTryTimeout.GetTextAfter('|');
                                ReconnectTryTimeout = ReconnectTryTimeout.GetTextBefore('|');
                            }
                            shouldReconnect = shouldReconnect == '1';
                            SSEBack(currentEvent, callMethodSecondArg, shouldReconnect, ReconnectTryTimeout, outputPlace);
                            continue;
                        case 'S':
                            var method = callMethodThirdArg.GetTextBefore('|');
                            callMethodThirdArg = callMethodThirdArg.GetTextAfter('|');
                            var isMultiPart = callMethodThirdArg.GetTextBefore('|') == '1';
                            callMethodThirdArg = callMethodThirdArg.GetTextAfter('|');
                            var contentType = callMethodThirdArg.GetTextBefore('|');
                            callMethodThirdArg = callMethodThirdArg.GetTextAfter('|');
                            var data = callMethodThirdArg;
                            var outputPlace = null;
                            if (data.Contains('|'))
                            {
                                outputPlace = data.GetTextAfter('|');
                                data = data.GetTextBefore('|');
                            }
                            data = data.Replace(""$[vb];"", '|');
                            cb_SendRequestAndResponse(currentEvent, outputPlace, callMethodSecondArg, method, isMultiPart, contentType, data);
                            continue;
                        case 'j':
                            var args;
                            if (callMethodThirdArg && callMethodThirdArg.Contains('|'))
                            {
                                var [...args] = callMethodThirdArg.GetTextAfter('|').split('|');
                                callMethodThirdArg = callMethodThirdArg.GetTextBefore('|');
                            }

                            await FrontBack(currentEvent, callMethodSecondArg, callMethodThirdArg, ...args);
                            continue;
                    }
                    break;
           
                case `@`:
                    await cb_SaveValue(evt, ActionControl.substring(1, 2), ActionControl.substring(2, 3), ActionControl.substring(3), LastElementPlaceList, TransientDOM);
                    continue;

                case '&':
                    {
                        var GoToValue = Value;
                        var LineIndex = GoToValue.GetTextBefore('|');
                        var Repeat = GoToValue.GetTextAfter('|');
                        var InitialRepeat = Repeat;

                        if (Repeat.Contains('|'))
                        {
                            InitialRepeat = Repeat.GetTextAfter('|');
                            Repeat = GoToValue.GetTextBefore('|');
                        }

                        if (parseInt(Repeat, 10) == 0)
                        {
                            WebFormsList[i] = ""&="" + LineIndex + '|' + InitialRepeat;
                            continue;
                        }
                        Repeat = parseInt(Repeat, 10) - 1;

                        WebFormsList[i] = ""&="" + LineIndex + '|' + Repeat + '|' + InitialRepeat;

                        if (LineIndex.substring(0,1) == '#')
                        {
                            i = 0;
                            IndexHasStarted = false;
                            StartIndex = LineIndex.GetTextAfter('#');
                            StartIndexIsNumber = StartIndex.IsNumber();
                            StartIndexIndex = StartIndexIsNumber ? parseInt(StartIndex) : 0;
                            IndexForStartIndex = 1;
                        }
                        else
                        {
                            WebFormsList[i] = ""&="" + LineIndex + '|' + Repeat;
                            var LineIndexInt = parseInt(LineIndex, 10);
                            if (LineIndexInt >= 0)
                                i = LineIndexInt;
                            else
                                i = i + LineIndexInt;
                        }
                        continue;
                    }

                case 'r':
                    var CacheKeyValue = Value;
                    switch (SecondChar)
                    {
                        case 's':
                            if (CacheKeyValue == '*')
                                sessionStorage.clear();
                            else
                                sessionStorage.removeItem(CacheKeyValue);
                            continue;
                        case 'd':
                            if (CacheKeyValue == '*')
                                localStorage.clear();
                            else
                                localStorage.removeItem(CacheKeyValue);
                            continue;
                    }
                    break;

                case 's':
                    switch (SecondChar)
                    {
                        case 'C':
                            var [key, value, seconds, path] = Value.split('|');
                            cb_SetCookie(key, value, seconds, path);
                            continue;
                    }
                    break;

                case 'c':
                    switch (SecondChar)
                    {
                        case 's':
                            if (!RequestName)
                                continue;
                            sessionStorage.setItem(RequestName, WebFormsValues);
                            continue;
                        case 'd':
                            if (!RequestName)
                                continue;
                            localStorage.setItem(RequestName, WebFormsValues);
                            var DurationValue = Value;

                            if (DurationValue != '*')
                            {
                                var UntilDate = new Date();
                                UntilDate.setSeconds(UntilDate.getSeconds() + parseInt(DurationValue));

                                localStorage.setItem(RequestName + ""-date"", UntilDate);
                            }
                            continue;
                        case 'u':
                            window.history.replaceState({ url: Value }, null, Value);
                            continue;
                    }
                    break;

                case 'C':
                case 'S':
                    var [cacheName, cacheValue] = Value.split('|');
                    var isCache = (FirstChar == 'C');
                    cacheValue = cacheValue.Replace(""$[ln];"", '\n');
                    switch (SecondChar)
                    {
                        case 'A':
                            cb_SetStorage(isCache, cacheName, cacheValue);
                            continue;
                        case 'I':
                            const exists = isCache ? cb_LocalCacheExists(cacheName) : cb_SessionCacheExists(cacheName);

                            if (!exists)
                                cb_SetStorage(isCache, cacheName, cacheValue);

                            continue;
                    }
                    break;

                case 'e':
                    switch (SecondChar)
                    {
                        case 'w':
                            if (Value == '$')
                                cb_UseWebSocket = '$';
                            else
                                cb_UseWebSocket = (Value == '1');
                            continue;

                        case 'b':
                            cb_EnableScrollBottomEvent(Value == '1');
                            continue;
                    }
                    break;

                case 'a':
                    switch (SecondChar)
                    {
                        case 'w':
                            cb_AddWebSocketPath(Value);
                            continue;
                    }
                    break;

                case 'h':
                    switch (SecondChar)
                    {
                        case 't':
                            document.title = Value;
                            continue;
                    }
                    break;

                case 'A':
                    switch (SecondChar)
                    {
                        case 'l':
                            var [text, type, title, okText] = Value.split('|');

                            if (!type)
                                type = ""none"";
                            if (!title)
                                title = ""Alert"";
                            if (!okText)
                                okText = ""OK"";

                            cb_ShowAlert(text, type, title, okText);
                            continue;
                        case 'S':
                            var [linkPath, linkTitle] = Value.split(""|"");
                            if (!linkPath)
                                linkPath = window.location.pathname + window.location.search + window.location.hash;

                            setTimeout(() => { cb_SPA.saveState(linkPath, linkTitle) }, WebFormsOptions.SPASaveStateDelay);
                            continue;
                    }
                    break;

                case 'M':
                    switch (SecondChar)
                    {
                        case 'l':
                            var modulePath = Value;
                            var moduleMethods;

                            if (modulePath.Contains('|'))
                            {
                                moduleMethods = modulePath.GetTextAfter('|').split('|');
                                modulePath = modulePath.GetTextBefore('|');
                            }

                            await cb_LoadModule(modulePath, moduleMethods);
                            continue;
                        case 'u': cb_UnloadModule(Value); continue;
                        case 'd': cb_DeleteModuleMethod(Value); continue;
                    }
                    break;

                case 'm':
                    switch (SecondChar)
                    {
                        case 'e':
                            var [text, type, duration] = Value.split('|');

                            if (!type)
                                type = ""none"";
                            if (!duration)
                                duration = 0;

                            cb_ShowMessage(text, type, duration);
                            continue;
                        case 'c':
                            Value = Value.Replace(""$[ln];"", '\n');
                            var type = ""log"";
                            var text = Value;

                            if (text.Contains('|'))
                            {
                                type = text.GetTextAfterLast('|');
                                text = text.GetTextBeforeLast('|');
                            }
                            switch (type)
                            {
                                case ""log"": console.log(text); break;
                                case ""info"": console.info(text); break;
                                case ""warn"": console.warn(text); break;
                                case ""error"": console.error(text); break;
                                case ""debug"": console.debug(text); break;
                                case ""trace"": console.trace(text); break;
                                case ""group"": console.group(text); break;
                                case ""groupend"": console.groupEnd(text); break;
                                case ""table"": console.table(text); break;
                                default: console.log(Value);
                            }
                            continue;
                        case 'a':
                            var text = Value.Replace(""$[ln];"", '\n');
                            var condition = text.GetTextAfter('|');
                            text = text.GetTextBefore('|');
                            console.assert(condition, text);
                            continue;
                    }
                    break;

                case 't':
                    switch (SecondChar)
                    {
                        case 'd':
                            if (Value == ';')
                            {
                                // Reset Input Type
                                cb_SetMainSubmitTypeToButtons(TransientDOM);

                                var HtmlDOM = cb_GetElementByElementPlace(TransientDOMPlace);                                  
                                HtmlDOM.replaceWith(TransientDOM);

                                TransientDOM = null;
                                TransientDOMPlace = null;
                            }
                            else
                            {
                                TransientDOMPlace = Value;
                                var HtmlDOM = cb_GetElementByElementPlace(TransientDOMPlace)

                                TransientDOM = HtmlDOM.cloneNode(true);

                                // State Preservation
                                TransientDOM = cb_SetStatePreservation(HtmlDOM, TransientDOM);
                            }
                    }
                    break;

                case 'n':
                    switch (SecondChar)
                    {
                        case 'w': await navigator.clipboard.writeText(Value);
                    }
            }

            // Extension
            if (await cb_SetWebFormsValuesExtension(evt, FirstChar, SecondChar, Value, LastElementPlaceList, TransientDOM))
                continue;

            var ActionName = ActionControl.substring(0, 2);
            var ActionValue = ActionControl.substring(2);

            var ActionOperation = ActionName.substring(0, 1);
            var ActionFeature = ActionName.substring(1, 2);

            LastElementPlaceList = await cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue, LastElementPlaceList, TransientDOM);
        }
        catch (er)
        {
            if (WebFormsOptions.AddLog)
                console.warn(""There was a problem in webforms value whene executing the command: "" + er + ""\nError in command: "" + WebFormsList[i]);

            if (WebFormsOptions.AddMessageForProblemInSetWebFormsValue)
                cb_ShowMessage(WebFormsOptions.ProblemInSetWebFormsValueLang, ""problem"", WebFormsOptions.MessageDuration);
        }
    }
}

function cb_SetWebFormsValues(evt, RequestName, WebFormsValues, UsePostBack, WithoutWebFormsSection)
{
    evt = cb_PreServedEvent(evt);

    if (WebFormsOptions.UseQueueForWebFormsValue)
    {
        cb_AddToQueue(async () =>
        {
            await cb_RunWebFormsValues(evt, RequestName, WebFormsValues, UsePostBack, WithoutWebFormsSection);
        });
    }
    else
        cb_RunWebFormsValues(evt, RequestName, WebFormsValues, UsePostBack, WithoutWebFormsSection);
}

async function cb_SetValueToInput(evt, ActionOperation, ActionFeature, ActionValue, LastElementPlaceList, TransientDOM)
{
    var ElementPlace = ActionValue.Contains('=') ? ActionValue.GetTextBefore('=') : ActionValue;
    var Value = ActionValue.GetTextAfter('=').FullTrim().Replace(""$[ln];"", '\n');

    var LabelForIndexer = 0;
    var ElementPlaceList;

    const CurrentDocument = TransientDOM ?? document;

    if (ElementPlace == '-')
    {
        ElementPlaceList = LastElementPlaceList;
    }
    else
    {
        var HasRequester = false;
        var Requester;
        if (ElementPlace.substring(0, 1) == '$')
        {
            HasRequester = true;
            ElementPlace = ElementPlace.substring(1);
            Requester = evt.currentTarget;
        }
        if (ElementPlace.substring(0, 1) == '!')
        {
            HasRequester = true;
            ElementPlace = ElementPlace.substring(1);
            Requester = evt.target;
        }

        if (ElementPlace.length > 0)
        {
            ElementPlaceList = new Array();

            if (HasRequester)
            {
                let tmpElementRequester = cb_GetElementByElementPlace(ElementPlace, Requester, TransientDOM);

                if (cb_CountElements(tmpElementRequester) > 1)
                    ElementPlaceList = tmpElementRequester;
                else
                    ElementPlaceList[0] = tmpElementRequester;
            }
            else
            {
                let tmpElement = cb_GetElementByElementPlace(ElementPlace, null, TransientDOM);

                if (cb_CountElements(tmpElement) > 1)
                    ElementPlaceList = tmpElement;
                else
                    ElementPlaceList[0] = tmpElement;
            }
        }
        else
        {
            ElementPlaceList = new Array();
            ElementPlaceList[0] = Requester;
        }
    }

    for (var i = 0; i < ElementPlaceList.length; i++)
    {
        try
        {
            var CurrentElement = ElementPlaceList[i];

            if (!CurrentElement)
                continue;

            // Without Server Attribute
            switch (ActionOperation)
            {
                case 'a':
                    switch (ActionFeature)
                    {
                        case 'i': CurrentElement.id = (CurrentElement.id) ? CurrentElement.id + Value : Value; break;
                        case 'n':
                            if (CurrentElement.tagName.IsInput())
                                CurrentElement.name = (CurrentElement.name) ? CurrentElement.name + Value : Value;
                            else
                                if (CurrentElement.hasAttribute(""name""))
                                {
                                    var NameAttr = CurrentElement.getAttribute(""name"");
                                    CurrentElement.setAttribute(""name"", NameAttr + Value);
                                }
                                else
                                    CurrentElement.setAttribute(""name"", Value);
                            break;
                        case 'v':
                            if (CurrentElement.tagName.IsInput())
                                CurrentElement.value = (CurrentElement.value) ? CurrentElement.value + Value : Value;
                            else
                                if (CurrentElement.hasAttribute(""value""))
                                {
                                    var ValueAttr = CurrentElement.getAttribute(""value"");
                                    CurrentElement.setAttribute(""value"", ValueAttr + Value);
                                }
                                else
                                    CurrentElement.setAttribute(""value"", Value);
                            break;
                        case 'c':
                            if (CurrentElement.hasAttribute(""class""))
                            {
                                var ClassAttr = CurrentElement.getAttribute(""class"");
                                CurrentElement.setAttribute(""class"", ClassAttr + ' ' + Value);
                            }
                            else
                                CurrentElement.setAttribute(""class"", Value);
                            break;
                        case 's':
                            if (CurrentElement.hasAttribute(""style""))
                            {
                                var StyleAttr = CurrentElement.getAttribute(""style"");
                                if (StyleAttr.charAt(StyleAttr.length - 1) == ';')
                                    CurrentElement.setAttribute(""style"", StyleAttr + Value);
                                else
                                    CurrentElement.setAttribute(""style"", StyleAttr + ';' + Value);
                            }
                            else
                                CurrentElement.setAttribute(""style"", Value);
                            break;
                        case 'o':
                            var OptionTag = document.createElement(""option"");
                            var OptionValue = Value.GetTextBefore('|');
                            var OptionText = Value.GetTextAfter('|');
                            if (OptionText.Contains('|'))
                            {
                                OptionTag.selected = (OptionText.GetTextAfter('|') == '1');
                                OptionText = OptionText.GetTextBefore('|');
                            }

                            OptionTag.value = OptionValue;
                            OptionTag.text = OptionText;

                            CurrentElement.appendChild(OptionTag);
                            break;
                        case 'k':
                            var CheckBoxTag = document.createElement(""input"");
                            CheckBoxTag.setAttribute(""type"", ""checkbox"");

                            var CheckBoxValue = Value.GetTextBefore('|');
                            var CheckBoxText = Value.GetTextAfter('|');
                            if (CheckBoxText.Contains('|'))
                            {
                                CheckBoxTag.checked = (CheckBoxText.GetTextAfter('|') == '1');
                                CheckBoxText = CheckBoxText.GetTextBefore('|');
                            }

                            CheckBoxTag.setAttribute(""value"", CheckBoxValue);
                            var CeckBoxIndex = CurrentElement.querySelectorAll('input[type=""checkbox""]').length;

                            var CheckBoxNameAndText = ""cblst_NoneSet"";
                            if (CurrentElement.id)
                                CheckBoxNameAndText = CurrentElement.id;
                            else
                                if (CeckBoxIndex > 0)
                                    CheckBoxNameAndText = CurrentElement.querySelectorAll('input[type=""checkbox""]')[0].name.GetTextBefore('$');

                            CheckBoxTag.id = CheckBoxNameAndText + '_' + CeckBoxIndex;
                            CheckBoxTag.name = CheckBoxNameAndText + '$' + CeckBoxIndex;

                            CurrentElement.appendChild(document.createElement(""br""));

                            CurrentElement.appendChild(CheckBoxTag);

                            var LabelTag = document.createElement(""label"");
                            LabelTag.setAttribute(""for"", CheckBoxTag.id);
                            LabelTag.innerText = CheckBoxText;
                            CurrentElement.appendChild(LabelTag);

                            break;
                        case 'l':
                            if (CurrentElement.hasAttribute(""title""))
                            {
                                var TitleAttr = CurrentElement.getAttribute(""title"");
                                CurrentElement.setAttribute(""title"", TitleAttr + Value);
                            }
                            else
                                CurrentElement.setAttribute(""title"", Value);
                            break;

                        case 'A':
                            if (!CurrentElement.id)
                                CurrentElement.id = ""tmp_Element"" + LabelForIndexer++;

                            var LabelTag = CurrentDocument.querySelector('label[for=""' + CurrentElement.id + '""]');

                            if (LabelTag)
                                LabelTag.innerText = LabelTag.innerText + Value;
                            else
                            {
                                LabelTag = document.createElement(""label"");
                                LabelTag.setAttribute(""for"", CurrentElement.id);
                                LabelTag.innerText = Value;
                                CurrentElement.insertAdjacentElement(""beforebegin"", LabelTag);
                            }
                            break;
                        case 't':
                            Value = Value.Replace(""$[ln];"", ""\n"");
                            if (Value.HasTag())
                            {
                                CurrentElement.insertAdjacentHTML(""beforeend"", cb_RemoveScripts(Value).toDOM());
                                cb_AppendJavaScriptTag(Value);
                                cb_Initialization(CurrentElement);
                            }
                            else
                                CurrentElement.insertAdjacentHTML(""beforeend"", Value);
                            break;
                        case 'a':
                            var AttrName = Value.GetTextBefore('|');
                            var Splitter = Value.GetTextAfter('|');
                            var AttrValue = """";
                            if (Splitter.Contains('|'))
                            {
                                AttrValue = Splitter.GetTextAfter('|');
                                Splitter = Splitter.GetTextBefore('|');
                            }
                            if (CurrentElement.hasAttribute(AttrName))
                            {
                                var CurrentAttr = CurrentElement.getAttribute(AttrName);
                                if (CurrentAttr.charAt(CurrentAttr.length - 1) == Splitter)
                                    CurrentElement.setAttribute(AttrName, CurrentAttr + AttrValue);
                                else
                                    CurrentElement.setAttribute(AttrName, CurrentAttr + Splitter + AttrValue);
                            }
                            else
                                CurrentElement.setAttribute(AttrName, AttrValue);
                            break;
                        case 'h':
                            var TmpTag = document.createElement(""input"");
                            TmpTag.setAttribute(""type"", ""hidden"");
                            TmpTag.value = Value;
                            if (Value.Contains('|'))
                            {
                                var TagValue = Value.GetTextBefore('|');
                                var TagId = Value.GetTextAfter('|');
                                TmpTag.value = TagValue;
                                TmpTag.setAttribute(""id"", TagId);
                            }
                            CurrentElement.append(TmpTag);
                    }
                    break;

                case 's':
                case 'i':
                    switch (ActionFeature)
                    {
                        case 'i':
                            if ((ActionOperation == 'i') && (CurrentElement.id))
                                break;

                            CurrentElement.id = Value;
                            break;
                        case 'n':
                            if (CurrentElement.tagName.IsInput())
                            {
                                if ((ActionOperation == 'i') && CurrentElement.name)
                                    break;

                                CurrentElement.name = Value;
                            }
                            else
                            {
                                if (ActionOperation == 'i' && CurrentElement.hasAttribute(""name""))
                                    break;

                                CurrentElement.setAttribute(""name"", Value);
                            }
                            break;
                        case 'v':
                            if (CurrentElement.tagName.IsInput())
                            {
                                if ((ActionOperation == 'i') && CurrentElement.value)
                                    break;

                                CurrentElement.value = Value;
                            }
                            else
                            {
                                if (ActionOperation == 'i' && CurrentElement.hasAttribute(""value""))
                                    break;

                                CurrentElement.setAttribute(""value"", Value);
                            }
                            break;
                        case 'c':
                            if (CurrentElement.hasAttribute(""class""))
                            {
                                var ClassAttr = CurrentElement.getAttribute(""class"");

                                if ((ActionOperation == 'i') && (ClassAttr.ContainsWithSpliter(Value, ' ')))
                                    break;

                                CurrentElement.setAttribute(""class"", ClassAttr + ' ' + Value);
                            }
                            else
                                CurrentElement.setAttribute(""class"", Value);
                            break;
                        case 's':
                            if (CurrentElement.hasAttribute(""style""))
                                cb_AddInlineStyle(CurrentElement, Value, ActionOperation != 'i');
                            else
                                CurrentElement.setAttribute(""style"", Value);
                            break;
                        case 'o':
                            if ((ActionOperation == 'i') && (CurrentElement.querySelectorAll('option[value=""' + Value.GetTextBefore('|') + ' ""]').length > 0))
                                break;

                            var tmpOptionTag = CurrentElement.querySelector('option[value=""' + Value.GetTextBefore('|') + '""]');

                            var OptionTag = tmpOptionTag ?? document.createElement(""option"");
                            var OptionValue = Value.GetTextBefore('|');
                            var OptionText = Value.GetTextAfter('|');
                            if (OptionText.Contains('|'))
                            {
                                OptionTag.selected = (OptionText.GetTextAfter('|') == '1');
                                OptionText = OptionText.GetTextBefore('|');
                            }

                            OptionTag.value = OptionValue;
                            OptionTag.text = OptionText;

                            if (!tmpOptionTag)
                                CurrentElement.appendChild(OptionTag);
                            break;
                        case 'k':
                            if ((CurrentElement.tagName.toLowerCase() == ""input"") && ((CurrentElement.type.toLowerCase() == ""checkbox"") || (CurrentElement.type.toLowerCase() == ""radio"")))
                            {
                                CurrentElement.checked = (Value == '1');
                                break;
                            }

                            if ((ActionOperation == 'i') && (CurrentElement.querySelectorAll('input[type=""checkbox""][value=""' + Value.GetTextBefore('|') + '""]').length > 0))
                                break;

                            var CheckBoxTag = document.createElement(""input"");
                            CheckBoxTag.setAttribute(""type"", ""checkbox"");

                            var CheckBoxValue = Value.GetTextBefore('|');
                            var CheckBoxText = Value.GetTextAfter('|');
                            if (CheckBoxText.Contains('|'))
                            {
                                CheckBoxTag.checked = (CheckBoxText.GetTextAfter('|') == '1');
                                CheckBoxText = CheckBoxText.GetTextBefore('|');
                            }

                            CheckBoxTag.setAttribute(""value"", CheckBoxValue);
                            var CeckBoxIndex = CurrentElement.querySelectorAll('input[type=""checkbox""]').length;

                            var CheckBoxNameAndText = ""cblst_NoneSet"";
                            if (CurrentElement.id)
                                CheckBoxNameAndText = CurrentElement.id;
                            else
                                if (CeckBoxIndex > 0)
                                    CheckBoxNameAndText = CurrentElement.querySelectorAll('input[type=""checkbox""]')[0].name.GetTextBefore('$');

                            CheckBoxTag.id = CheckBoxNameAndText + '_' + CeckBoxIndex;
                            CheckBoxTag.name = CheckBoxNameAndText + '$' + CeckBoxIndex;

                            CurrentElement.appendChild(document.createElement(""br""));

                            CurrentElement.appendChild(CheckBoxTag);

                            var LabelTag = document.createElement(""label"");
                            LabelTag.setAttribute(""for"", CheckBoxTag.id);
                            LabelTag.innerText = CheckBoxText;
                            CurrentElement.appendChild(LabelTag);

                            break;
                        case 'l':
                            if (CurrentElement.hasAttribute(""title""))
                                if ((ActionOperation == 'i') && CurrentElement.hasAttribute(""title""))
                                    break;

                            CurrentElement.setAttribute(""title"", Value);
                            break;
                        case 'A':
                            if (!CurrentElement.id)
                                CurrentElement.id = ""tmp_Element"" + LabelForIndexer++;

                            var LabelTag = CurrentDocument.querySelector('label[for=""' + CurrentElement.id + '""]');

                            if (LabelTag)
                            {
                                if ((ActionOperation == 'i') && LabelTag.innerText)
                                    break;

                                LabelTag.innerText = Value;
                            }
                            else
                            {
                                LabelTag = document.createElement(""label"");
                                LabelTag.setAttribute(""for"", CurrentElement.id);
                                LabelTag.innerText = Value;
                                CurrentElement.insertAdjacentElement(""beforebegin"", LabelTag);
                            }
                            break;
                        case 't':
                            if ((ActionOperation == 'i') && (CurrentElement.innerHTML || CurrentElement.innerText))
                                break;

                            Value = Value.Replace(""$[ln];"", ""\n"");
                            if (Value.HasTag())
                            {

                                CurrentElement.replaceChildren();
                                CurrentElement.insertAdjacentHTML(""beforeend"", cb_RemoveScripts(Value).toDOM());
                                cb_AppendJavaScriptTag(Value);
                                cb_Initialization(CurrentElement);
                            }
                            else
                                CurrentElement.textContent = Value;
                            break;
                        case 'a':
                            var AttrName = Value.GetTextBefore('|');
                            var Splitter = Value.GetTextAfter('|');
                            var AttrValue = """";
                            if (Splitter.Contains('|'))
                            {
                                AttrValue = Splitter.GetTextAfter('|');
                                Splitter = Splitter.GetTextBefore('|');
                            }
                            if (CurrentElement.hasAttribute(AttrName))
                            {
                                var CurrentAttr = CurrentElement.getAttribute(AttrName);

                                if ((ActionOperation == 'i') && (CurrentAttr.ContainsWithSpliter(AttrValue, Splitter)))
                                    break;
                            }
                            CurrentElement.setAttribute(AttrName, AttrValue);
                    }
                    break;

                case 'd':
                    switch (ActionFeature)
                    {
                        case 'i':
                            if (CurrentElement.id)
                                CurrentElement.removeAttribute(""id"");
                            break;
                        case 'n':
                            if (CurrentElement.name)
                                CurrentElement.removeAttribute(""name"");
                            break;
                        case 'v':
                            if (CurrentElement.value)
                                CurrentElement.value = """";
                            break;
                        case 'c':
                            if (CurrentElement.className)
                                CurrentElement.className = CurrentElement.className.DeleteHtmlClass(Value);
                            break;
                        case 's':
                            if (CurrentElement.hasAttribute(""style""))
                                CurrentElement.style.removeProperty(Value);
                            break;
                        case 'o':
                            if (Value == '*')
                            {
                                var OptionList = CurrentElement.querySelectorAll(""option"");
                                for (var OptionIndex = 0; OptionIndex < OptionList.length; OptionIndex++)
                                    OptionList[OptionIndex].remove();
                                break;
                            }
                            if (CurrentElement.querySelectorAll('option[value=""' + Value + '""]').length > 0)
                                CurrentElement.querySelectorAll('option[value=""' + Value + '""]')[0].remove();
                            break;
                        case 'k':
                            if (Value == '*')
                            {
                                var CheckBoxList = CurrentElement.querySelectorAll('input[type=""checkbox""]');
                                for (var CheckBoxTagIndex = 0; CheckBoxTagIndex < CheckBoxList.length; CheckBoxTagIndex++)
                                {
                                    var LabelTag = CurrentDocument.querySelector('label[for=""' + CheckBoxList[CheckBoxTagIndex].id + '""]');
                                    if (LabelTag)
                                        LabelTag.remove();

                                    CheckBoxList[CheckBoxTagIndex].remove();
                                }
                                break;
                            }
                            var CheckBoxTagLength = CurrentElement.querySelectorAll('input[type=""checkbox""][value=""' + Value + '""]').length;
                            if (CheckBoxTagLength > 0)
                            {
                                var CheckBoxTag = CurrentElement.querySelectorAll('input[type=""checkbox""][value=""' + Value + '""]')[0];
                                if (CheckBoxTag.id)
                                    if (CurrentElement.querySelectorAll('label[for=""' + CheckBoxTag.id + '""]').length > 0)
                                        CurrentElement.querySelectorAll('label[for=""' + CheckBoxTag.id + '""]')[0].remove();

                                CheckBoxTag.remove();
                            }
                            break;
                        case 'l':
                            if (CurrentElement.hasAttribute(""title""))
                                CurrentElement.removeAttribute(""title"");
                            break;
                        case 'A':
                            if (CurrentElement.id)
                            {
                                var LabelTag = CurrentDocument.querySelector('label[for=""' + CurrentElement.id + '""]');
                                if (LabelTag)
                                    LabelTag.remove();
                            }
                            break;
                        case 't':
                            CurrentElement.replaceChildren();
                            break;
                        case 'a':
                            if (CurrentElement.hasAttribute(Value))
                                CurrentElement.removeAttribute(Value);
                            break;
                        case 'e':
                            var LabelTag = CurrentDocument.querySelector('label[for=""' + CurrentElement.id + '""]');
                            if (LabelTag)
                                LabelTag.remove();
                            CurrentElement.remove();
                            break;
                        case 'p':
                            CurrentElement.parentElement.remove();
                    }
                    break;

                case '+':
                case '-':
                    switch (ActionFeature)
                    {
                        case 'n':
                            if (CurrentElement.hasAttribute(""minlength""))
                            {
                                var ElementMinLength = (ActionOperation == '+') ? parseInt(CurrentElement.getAttribute(""minlength"")) + parseInt(Value) : parseInt(CurrentElement.getAttribute(""minlength"")) - parseInt(Value);
                                CurrentElement.setAttribute(""minlength"", ElementMinLength);
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.setAttribute(""minlength"", Value);
                            break;
                        case 'x':
                            if (CurrentElement.hasAttribute(""maxlength""))
                            {
                                var ElementMaxLength = (ActionOperation == '+') ? parseInt(CurrentElement.getAttribute(""maxlength"")) + parseInt(Value) : parseInt(CurrentElement.getAttribute(""maxlength"")) - parseInt(Value);
                                CurrentElement.setAttribute(""maxlength"", ElementMaxLength);
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.setAttribute(""maxlength"", Value);
                            break;
                        case 'f':
                            if (CurrentElement.style.fontSize)
                            {
                                var Unit = CurrentElement.style.fontSize.GetUnit();
                                var ElementFontSize = (ActionOperation == '+') ? parseInt(CurrentElement.style.fontSize) + parseInt(Value) : parseInt(CurrentElement.style.fontSize) - parseInt(Value);
                                CurrentElement.style.fontSize = ElementFontSize.toString() + Unit;
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.style.fontSize = Value + ""px"";
                            break;
                        case 'w':
                            if (CurrentElement.style.width)
                            {
                                var Unit = CurrentElement.style.width.GetUnit();
                                var ElementWidth = (ActionOperation == '+') ? parseInt(CurrentElement.style.width) + parseInt(Value) : parseInt(CurrentElement.style.width) - parseInt(Value);
                                CurrentElement.style.width = ElementWidth.toString() + Unit;
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.style.width = Value + ""px"";
                            break;
                        case 'h':
                            if (CurrentElement.style.height)
                            {
                                var Unit = CurrentElement.style.height.GetUnit();
                                var ElementHeight = (ActionOperation == '+') ? parseInt(CurrentElement.style.height) + parseInt(Value) : parseInt(CurrentElement.style.height) - parseInt(Value);
                                CurrentElement.style.height = ElementHeight.toString() + Unit;
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.style.height = Value + ""px"";
                            break;
                        case 'v':
                            if (CurrentElement.value)
                            {
                                var ElementValue = (ActionOperation == '+') ? parseInt(CurrentElement.value) + parseInt(Value) : parseInt(CurrentElement.value) - parseInt(Value);
                                CurrentElement.value = ElementValue.toString();
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.value = Value;
                    }
                    break;

                case 'g':
                    var action = Value.GetTextBefore('|');
                    Value = Value.GetTextAfter('|');
                    switch (ActionFeature)
                    {
                        case 't':
                            switch (action)
                            {
                                case 'i': CurrentElement.textContent = parseFloat(CurrentElement.textContent) + parseFloat(Value); break;
                                case 'r':
                                    var [value, newValue, alsoStartTag, deep] = Value.split('|');
                                    deep = (deep == '1');
                                    alsoStartTag = (alsoStartTag == '1');
                                    value = value.Replace(""$[at];"", '@');
                                    newValue = newValue.Replace(""$[at];"", '@');
                                    newValue = await cb_SetDynamicForValue(evt, newValue);

                                    if (deep)
                                        cb_ReplaceDeep(CurrentElement, value, newValue, alsoStartTag);
                                    else
                                    {
                                        CurrentElement.textContent = CurrentElement.textContent.Replace(value, newValue);

                                        if (alsoStartTag)
                                            cb_ReplaceStartTag(CurrentElement, value, newValue);
                                    }
                                    break;
                                case 's':
                                    var [value, newValue] = Value.split('|');
                                    value = value.Replace(""$[at];"", '@');
                                    newValue = newValue.Replace(""$[at];"", '@');
                                    newValue = await cb_SetDynamicForValue(evt, newValue);

                                    cb_ReplaceStartTag(CurrentElement, value, newValue);
                            }
                    }
                    break;

                case 'E':
                    switch (ActionFeature)
                    {
                        case 'p':
                            if (Value.Contains('|'))
                            {
                                var HtmlEvent = Value.GetTextBefore('|');
                            
                                if (Value.GetTextAfter('|') == '+')
                                    cb_AddEvent(CurrentElement, HtmlEvent, ""PostBack(event, true)"");
                                else
                                    cb_AddEvent(CurrentElement, HtmlEvent, ""PostBack(event, '"" + Value.GetTextAfter('|') + ""')"");
                            }
                            else
                                cb_AddEvent(CurrentElement, Value, ""PostBack(event)"");
                            break;
                        case 'P':
                            if (Value.Contains('|'))
                            {
                                var HtmlEvent = Value.GetTextBefore('|');
                            
                                if (Value.GetTextAfter('|') == '+')
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, PostBack, [true]);
                                else
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, PostBack, [Value.GetTextAfter('|')]);
                                break;
                            }
                            else
                                await cb_AddEventListener(CurrentElement, Value, PostBack, []);
                            break;
                        case 'g':
                        case 'a':
                        case 'l':
                        case 'h':
                        case 'o':
                        case 'r':
                        case 'c':
                            var FunctionName = ""GetBack"";
                            switch (ActionFeature)
                            {
                                case 'a': FunctionName = ""PatchBack""; break;
                                case 'l': FunctionName = ""DeleteBack""; break;
                                case 'h': FunctionName = ""HeadBack""; break;
                                case 'o': FunctionName = ""OptionsBack""; break;
                                case 'r': FunctionName = ""TraceBack""; break;
                                case 'c': FunctionName = ""ConnectBack""; break;
                            }
                            var HtmlEvent = Value.GetTextBefore('|');
                            var Path = Value.GetTextAfter('|');

                            if (Path.Contains('|'))
                            {
                                if (Path.GetTextBefore('|') == '#')
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + ""(event, '', '"" + Path.GetTextAfter('|') + ""')"");
                                else
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + ""(event, '"" + Path.GetTextBefore('|') + ""', '"" + Path.GetTextAfter('|') + ""')"");
                            }
                            else
                            {
                                if (Path == '#')
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + ""(event)"");
                                else
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + ""(event, '"" + Path + ""')"");
                            }
                            break;
                        case 'G':
                        case 'A':
                        case 'L':
                        case 'H':
                        case 'O':
                        case 'R':
                        case 'C':
                            var FunctionValue = GetBack;
                            switch (ActionFeature)
                            {
                                case 'A': FunctionValue = PatchBack; break;
                                case 'L': FunctionValue = DeleteBack; break;
                                case 'H': FunctionValue = HeadBack; break;
                                case 'O': FunctionValue = OptionsBack; break;
                                case 'R': FunctionValue = TraceBack; break;
                                case 'C': FunctionValue = ConnectBack; break;
                            }
                            var HtmlEvent = Value.GetTextBefore('|');
                            var Path = Value.GetTextAfter('|');

                            if (Path.Contains('|'))
                            {
                                if (Path.GetTextBefore('|') == '#')
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, ["""", Path.GetTextAfter('|')]);
                                else
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, [Path.GetTextBefore('|'), Path.GetTextAfter('|')]);
                            }
                            else
                            {
                                if (Path == '#')
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, []);
                                else
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, [Path]);
                            }
                            break;
                        case 't': cb_AddEvent(CurrentElement, Value.GetTextBefore('|'), ""TagBack(event, '"" + Value.GetTextAfter('|') + ""')""); break;
                        case 'T': await cb_AddEventListener(CurrentElement, Value.GetTextBefore('|'), TagBack, [Value.GetTextAfter('|')]); break;
                        case 'b':
                        case 'B':
                            var event = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var index = Value.GetTextBefore('|');
                            var outputPlace = Value.GetTextAfter('|');
                            if (ActionFeature == 'b')
                                cb_AddEvent(CurrentElement, event, `CommentBack(event, '${index}', '${outputPlace}')`);
                            else
                                await cb_AddEventListener(CurrentElement, event, CommentBack, [outputPlace, index]);
                            break;
                        case 'y':
                        case 'Y':
                            var event = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var wasmLanguage = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var wasmUrl = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var funcName = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var args = Value.GetTextBefore('|');
                            outputPlace = Value.GetTextAfter('|');

                            if (ActionFeature == 'y')
                                cb_AddEvent(CurrentElement, event, `WasmBack(event, '${wasmLanguage}', '${wasmUrl}', '${funcName}', [${args}], '${outputPlace}')`);
                            else
                                await cb_AddEventListener(CurrentElement, event, WasmBack, [wasmLanguage, wasmUrl, funcName, args.split(','), outputPlace]);
                            break;
                        case 'w': cb_AddEvent(CurrentElement, Value.GetTextBefore('|'), ""WebSocketBack(event, '"" + Value.GetTextAfter('|') + ""')""); break;
                        case 'W': await cb_AddEventListener(CurrentElement, Value.GetTextBefore('|'), WebSocketBack, [Value.GetTextAfter('|')]); break;
                        case 'e':
                        case 'E':
                            var htmlEvent = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var path = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var shouldReconnect = Value.GetTextBefore('|') == '1';
                            Value = Value.GetTextAfter('|');
                            var reconnectTryTimeout = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var outputPlace = Value;

                            if (ActionFeature == 'e')
                                cb_AddEvent(CurrentElement, htmlEvent, `SSEBack(event, '${path}', ${shouldReconnect}, ${reconnectTryTimeout}` + (outputPlace ? "", '"" + outputPlace + ""')"" : ')'));
                            else
                                await cb_AddEventListener(CurrentElement, htmlEvent, SSEBack, [path, shouldReconnect, reconnectTryTimeout, outputPlace]);
                            break;
                        case 'j':
                        case 'J':
                            var htmlEvent = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var modulePath = Value.GetTextBefore('|');
                            var outputPlace = Value.GetTextAfter('|');

                            var args;
                            var argsString;
                            if (outputPlace.Contains('|'))
                            {
                                argsString = outputPlace.GetTextAfter('|').split('|');

                                var args = argsString.map(x =>
                                {
                                    if ((x.startsWith(""'"") && x.endsWith(""'"")) || (x.startsWith('""') && x.endsWith('""')))
                                        return `'${x.slice(1, -1)}'`;

                                    if (!isNaN(x))
                                        return Number(x);

                                    if (!/^[a-zA-Z_$][0-9a-zA-Z_$\.]*$/.test(x))
                                        return `'${x}'`;

                                    return x;
                                });

                                outputPlace = outputPlace.GetTextBefore('|');
                            }

                            if (argsString && argsString.length > 0)
                                var argsString = args.join("","");

                            if (ActionFeature == 'j')
                                cb_AddEvent(CurrentElement, htmlEvent, `FrontBack(event, '${modulePath}', '${outputPlace}'` + (argsString ? ', ' + argsString : """") + ')');
                            else
                            {
                                if (!outputPlace)
                                    outputPlace = """";

                                await cb_AddEventListener(CurrentElement, htmlEvent, FrontBack, [modulePath, outputPlace, ...args]);
                            }
                            break;
                        case 'u':
                        case 'U':
                            var [event, outputPlace] = Value.split('|');
                            if (ActionFeature == 'u')
                                cb_AddEvent(CurrentElement, event, ""cb_MasterPages(event"" + (outputPlace ? "", '"" + outputPlace + ""'"" : """") + "")"");
                            else
                                await cb_AddEventListener(CurrentElement, event, cb_MasterPages, [outputPlace]);
                            break;
                        case 'n':
                        case 'N':
                            var htmlEvent = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var data = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var path = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var method = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var isMultiPart = (Value.GetTextBefore('|') == '1') ? ""true"" : ""false"";
                            Value = Value.GetTextAfter('|');
                            var contentType = Value.GetTextBefore('|');
                            Value = Value.GetTextAfter('|');
                            var outputPlace = Value;

                            if (ActionFeature == 'n')
                                cb_AddEvent(CurrentElement, htmlEvent, `SendBack(event, '${outputPlace}', '${path}', '${method}', ${isMultiPart}, '${contentType}', '${data}')`);
                            else
                                await cb_AddEventListener(CurrentElement, htmlEvent, SendBack, [outputPlace, path, method, isMultiPart == ""true"", contentType, data]);
                            break;
                        case 'd': cb_AddEvent(CurrentElement, Value, ""PreventDefault(event)""); break;
                        case 'D': await cb_AddEventListener(CurrentElement, Value, PreventDefault); break;
                        case 's': cb_AddEvent(CurrentElement, Value, ""StopPropagation(event)""); break;
                        case 'S': await cb_AddEventListener(CurrentElement, Value, StopPropagation); break;
                        case 'm':
                        case 'M':
                        case 'x':
                        case 'X':
                            var eventName = Value.GetTextBefore('|');
                            var funcName = Value.GetTextAfter('|')
                            var args = """";
                            var argsForListener = """";
                            if (funcName.Contains('|'))
                            {
                                var args = funcName.GetTextAfter('|').split('|').map(x =>
                                {
                                    if ((x.startsWith(""'"") && x.endsWith(""'"")) || (x.startsWith('""') && x.endsWith('""')))
                                        return `'${x.slice(1, -1)}'`;

                                    if (!isNaN(x))
                                        return Number(x);

                                    if (!/^[a-zA-Z_$][0-9a-zA-Z_$\.]*$/.test(x))
                                        return `'${x}'`;

                                    return x;
                                });

                                var argsString = args.join(',');

                                argsForListener = funcName.GetTextAfter('|').split('|');
                                funcName = funcName.GetTextBefore('|');
                            }
                            if (ActionFeature == 'm' || ActionFeature == 'M')
                            {
                                if (WebFormsOptions.DisableCallMethod)
                                {
                                    if (WebFormsOptions.AddLog)
                                        console.warn(""Access to the call method is disabled but is being attempted.\nMethod: "" + funcName);
                                    break;
                                }

                                if (WebFormsOptions.UseCallMethodOnlyInAcceptedList)
                                    if (!WebFormsOptions.CallMethodOnlyInAcceptedList.some(p => cb_MatchesPattern(p, funcName)))
                                    {
                                        if (WebFormsOptions.AddLog)
                                            console.warn(""Access to call method is only possible in the list, but is being attempted.\nMethod: "" + funcName);
                                        break;
                                    }
                            }
                            if (ActionFeature == 'm')
                                cb_AddEvent(CurrentElement, eventName, `cb_GetMethod('${funcName}')(${argsString})`);
                            else if (ActionFeature == 'M')
                                await cb_AddEventListener(CurrentElement, eventName, cb_GetMethod(funcName), argsForListener, ""method"");
                            else if (ActionFeature == 'x')
                                cb_AddEvent(CurrentElement, eventName, `cb_GetModuleMethod('${funcName}')(${argsString})`);
                            else
                                await cb_AddEventListener(CurrentElement, eventName, cb_GetModuleMethod(funcName), argsForListener, ""method"");
                            break;
                        case 'f':
                            var [text, type, title, okText, cancelText] = Value.GetTextAfter('|').split('|');

                            if (!text)
                                text = ""Are you sure you want to proceed?"";
                            if (!type)
                                type = ""none"";
                            if (!title)
                                title = ""Confirm"";
                            if (!okText)
                                okText = ""OK"";
                            if (!cancelText)
                                cancelText = ""Cancel"";
                        
                            var CurrentEvent = Value.GetTextBefore('|');

                            if (!CurrentElement.hasAttribute(CurrentEvent))
                                break;

                            var CurrentAttributeValue = CurrentElement.getAttribute(CurrentEvent);

                            CurrentAttributeValue = ""cb_ShowConfirm('"" + text + ""', '"" + type + ""', '"" + title + ""', '"" + okText + ""', '"" + cancelText + ""').then(() => {cb_ConfirmIsAccept = undefined;"" + CurrentAttributeValue + ""}).catch(() => { });"";

                            CurrentElement.setAttribute(CurrentEvent, CurrentAttributeValue);

                            break;
                    }
                    break;

                case 'R':
                    switch (ActionFeature)
                    {
                        case 'p': cb_RemoveEvent(CurrentElement, Value, ""PostBack""); break;
                        case 'g': cb_RemoveEvent(CurrentElement, Value, ""GetBack""); break;
                        case 'a': cb_RemoveEvent(CurrentElement, Value, ""PatchBack""); break;
                        case 'l': cb_RemoveEvent(CurrentElement, Value, ""DeleteBack""); break;
                        case 'h': cb_RemoveEvent(CurrentElement, Value, ""HeadBack""); break;
                        case 'o': cb_RemoveEvent(CurrentElement, Value, ""OptionsBack""); break;
                        case 'r': cb_RemoveEvent(CurrentElement, Value, ""TraceBack""); break;
                        case 'c': cb_RemoveEvent(CurrentElement, Value, ""ConnectBack""); break;
                        case 't': cb_RemoveEvent(CurrentElement, Value, ""TagBack""); break;
                        case 'b': cb_RemoveEvent(CurrentElement, Value, ""CommentBack""); break;
                        case 'y': cb_RemoveEvent(CurrentElement, Value, ""WasmBack""); break;
                        case 'w': cb_RemoveEvent(CurrentElement, Value, ""WebSocketBack""); break;
                        case 'e': cb_RemoveEvent(CurrentElement, Value, ""SSEBack""); break;
                        case 'j': cb_RemoveEvent(CurrentElement, Value, ""FrontBack""); break;
                        case 'n': cb_RemoveEvent(CurrentElement, Value, ""SendBack""); break;
                        case 'u': cb_RemoveEvent(CurrentElement, Value, ""cb_MasterPages""); break;
                        case 'd': cb_RemoveEvent(CurrentElement, Value, ""PreventDefault""); break;
                        case 's': cb_RemoveEvent(CurrentElement, Value, ""StopPropagation""); break;
                        case 'm': cb_RemoveEvent(CurrentElement, Value.GetTextBefore('|'), `cb_GetMethod('${Value.GetTextAfter('|')}')`); break;
                        case 'x': cb_RemoveEvent(CurrentElement, Value.GetTextBefore('|'), `cb_GetModuleMethod('${Value.GetTextAfter('|')}')`); break;
                        case 'f':
                            var CurrentAttributeValue = CurrentElement.getAttribute(Value);

                            if (CurrentAttributeValue)
                            {
                                CurrentAttributeValue = CurrentAttributeValue.replace(/cb_ShowConfirm\(.*?\)\.then\(\s*?\(\)\s*?=>\s*?{/, """");
                                CurrentAttributeValue = CurrentAttributeValue.replace(/}\)\.catch\(\(\)\s*?=>\s*?{ }\);/, """");

                                CurrentElement.setAttribute(Value, CurrentAttributeValue.trim());
                            }
                            break;
                        case 'P': cb_RemoveEventListener(CurrentElement, Value, PostBack); break;
                        case 'G': cb_RemoveEventListener(CurrentElement, Value, GetBack); break;
                        case 'A': cb_RemoveEventListener(CurrentElement, Value, PatchBack); break;
                        case 'L': cb_RemoveEventListener(CurrentElement, Value, DeleteBack); break;
                        case 'H': cb_RemoveEventListener(CurrentElement, Value, HeadBack); break;
                        case 'O': cb_RemoveEventListener(CurrentElement, Value, OptionsBack); break;
                        case 'R': cb_RemoveEventListener(CurrentElement, Value, TraceBack); break;
                        case 'C': cb_RemoveEventListener(CurrentElement, Value, ConnectBack); break;
                        case 'T': cb_RemoveEventListener(CurrentElement, Value, TagBack); break;
                        case 'B': cb_RemoveEventListener(CurrentElement, Value, CommentBack); break;
                        case 'Y': cb_RemoveEventListener(CurrentElement, Value, WasmBack); break;
                        case 'W': cb_RemoveEventListener(CurrentElement, Value, WebSocketBack); break;
                        case 'E': cb_RemoveEventListener(CurrentElement, Value, SSEBack); break;
                        case 'J': cb_RemoveEventListener(CurrentElement, Value, FrontBack); break;
                        case 'N': cb_RemoveEventListener(CurrentElement, Value, SendBack); break;
                        case 'U': cb_RemoveEventListener(CurrentElement, Value, cb_MasterPages); break;
                        case 'D': cb_RemoveEventListener(CurrentElement, Value, PreventDefault); break;
                        case 'S': cb_RemoveEventListener(CurrentElement, Value, StopPropagation); break;
                        case 'M': cb_RemoveEventListener(CurrentElement, Value.GetTextBefore('|'), window[Value.GetTextAfter('|')]); break;
                        case 'X': cb_RemoveEventListener(CurrentElement, Value.GetTextBefore('|'), cb_GetModuleMethod(Value.GetTextAfter('|'))); break;
                    }
                    break;

                case 'T':
                    switch (ActionFeature)
                    {
                        case 'E':
                            let constructorName;
                            if (Value.Contains('|'))
                            {
                                constructorName = Value.GetTextAfter('|');
                                Value = Value.GetTextBefore('|');
                            }
                            cb_TriggerEvent(CurrentElement, constructorName, Value);
                    }
                    break;

                case 'u':
                    switch (ActionFeature)
                    {
                        case 'o': cb_UseOnlyChangeUpdate(CurrentElement); break;
                        case 'w': CurrentElement.setAttribute(""usewebsocket"", ""true"");
                    }
                    break;

                case 'e':
                    switch (ActionFeature)
                    {
                        case 'C':
                            var [eventName, watch, key, compare, tmpValue, range, immediate, delay] = Value.split('|');
                            var rangeFrom = """";
                            var rangeTo = """";
                            immediate = immediate == '1';
                            if (range)
                            {
                                rangeFrom = range.GetTextBefore(',');
                                rangeTo = range.GetTextAfter(',');
                            }

                            cb_CreateCustomDOMEvent(CurrentElement, eventName, watch, key, compare, tmpValue, [rangeFrom, rangeTo], immediate, delay);
                            break;
                        case 'r': cb_EnableReachedElementEvent(CurrentElement, Value.GetTextBefore('|') == '1', Value.GetTextAfter('|') == '1'); 
                    }
            }

            switch (ActionOperation + ActionFeature)
            {
                case ""sw"": CurrentElement.style.width = Value; break;
                case ""sh"": CurrentElement.style.height = Value; break;
                case ""bc"": CurrentElement.style.backgroundColor = Value; break;
                case ""tc"": CurrentElement.style.color = Value; break;
                case ""fn"": CurrentElement.style.fontFamily = Value; break;
                case ""fs"": CurrentElement.style.fontSize = Value; break;
                case ""fb"": CurrentElement.style.fontWeight = (Value == '1') ? ""bold"" : ""unset""; break;
                case ""vi"": CurrentElement.style.visibility = (Value == '1') ? ""visible"" : ""hidden""; break;
                case ""ta"": CurrentElement.style.textAlign = Value; break;
                case ""sr"": (Value == '1') ? CurrentElement.setAttribute(""readonly"", """") : CurrentElement.removeAttribute(""readonly""); break;
                case ""sd"": (Value == '1') ? CurrentElement.setAttribute(""disabled"", """") : CurrentElement.removeAttribute(""disabled""); break;
                case ""sf"": (Value == '1') ? CurrentElement.focus() : CurrentElement.blur(); break;
                case ""mn"": CurrentElement.setAttribute(""minlength"", Value); break;
                case ""mx"": CurrentElement.setAttribute(""maxlength"", Value); break;
                case ""ts"": CurrentElement.value = Value; break;
                case ""ti"":
                    var SelectedIndex = parseInt(Value);
                    if (SelectedIndex >= 0)
                        CurrentElement.selectedIndex = SelectedIndex;
                    else
                        CurrentElement.selectedIndex = (CurrentElement.getElementsByTagName(""option"").length + SelectedIndex);
                    break;
                case ""ks"":
                    var CheckBoxValue = Value.GetTextBefore('|');
                    var CheckBoxChecked = Value.GetTextAfter('|');
                    var CheckBoxTagLength = CurrentElement.querySelectorAll('input[type=""checkbox""][value=""' + CheckBoxValue + '""]').length;
                    if (CheckBoxTagLength > 0)
                        CurrentElement.querySelectorAll('input[type=""checkbox""][value=""' + CheckBoxValue + '""]')[0].checked = (CheckBoxChecked == '1');
                    break;
                case ""ki"":
                    var CheckBoxIndex = parseInt(Value.GetTextBefore('|'));
                    var CheckBoxChecked = Value.GetTextAfter('|');
                    var CheckBoxTags = CurrentElement.querySelectorAll('input[type=""checkbox""]');
                    var CheckBoxTag = (CheckBoxIndex >= 0) ? CheckBoxTags[CheckBoxIndex] : CheckBoxTags[CheckBoxTags.length + CheckBoxIndex];
                    if (CheckBoxTag)
                        CheckBoxTag.checked = (CheckBoxChecked == '1');
                    break;
                case ""nt"":
                    if (Value.Contains('|'))
                    {
                        var TagName = Value.GetTextBefore('|');
                        var TagId = Value.GetTextAfter('|');
                        var TmpTag = document.createElement(TagName);
                        TmpTag.id = TagId;
                        CurrentElement.appendChild(TmpTag);
                    }
                    else
                        CurrentElement.appendChild(document.createElement(Value));
                    break;
                case ""ut"":
                    if (Value.Contains('|'))
                    {
                        var TagName = Value.GetTextBefore('|');
                        var TagId = Value.GetTextAfter('|');
                        var TmpTag = document.createElement(TagName);
                        TmpTag.id = TagId;
                        CurrentElement.prepend(TmpTag);
                    }
                    else
                        CurrentElement.prepend(document.createElement(Value));
                    break;
                case ""bt"":
                    if (Value.Contains('|'))
                    {
                        var TagName = Value.GetTextBefore('|');
                        var TagId = Value.GetTextAfter('|');
                        var TmpTag = document.createElement(TagName);
                        TmpTag.id = TagId;
                        CurrentElement.insertAdjacentElement(""beforebegin"", TmpTag);
                    }
                    else
                        CurrentElement.insertAdjacentElement(""beforebegin"", document.createElement(Value));
                    break;
                case ""ft"":
                    if (Value.Contains('|'))
                    {
                        var TagName = Value.GetTextBefore('|');
                        var TagId = Value.GetTextAfter('|');
                        var TmpTag = document.createElement(TagName);
                        TmpTag.id = TagId;
                        CurrentElement.insertAdjacentElement(""afterend"", TmpTag);
                    }
                    else
                        CurrentElement.insertAdjacentElement(""afterend"", document.createElement(Value));
                    break;
                case ""pt"":
                    Value = Value.Replace(""$[ln];"", ""\n"");
                    if (Value.HasTag())
                    {
                        CurrentElement.insertAdjacentHTML(""afterbegin"", Value.toDOM());
                        cb_AppendJavaScriptTag(cb_RemoveScripts(Value));
                        cb_Initialization(CurrentElement);
                    }
                    else
                        CurrentElement.insertAdjacentHTML(""afterbegin"", Value);
                    break;
                case ""lu"": cb_RequestAndResponse(evt, Value, ElementPlace, ""GET""); break;
                case ""sp"":
                    var OutputPlace = cb_GetElementByElementPlace(Value);
                    const placeHolder = document.createElement(""div"");
                    CurrentElement.parentNode.insertBefore(placeHolder, CurrentElement);
                    OutputPlace.replaceWith(CurrentElement);
                    placeHolder.replaceWith(OutputPlace);
                    break;
                case ""sR"": await cb_SetReflection(CurrentElement, Value); break;
                case ""iR"": await cb_SetReflection(CurrentElement, cb_GetElementByElementPlace(Value)); break
                case ""At"": cb_AssertEqual(CurrentElement, Value.Replace(""$[ln];"", ""\n"")); break;
                case ""Ao"": cb_AssertEqual(CurrentElement, cb_GetElementByElementPlace(Value)); 
            }

            // Extension
            await cb_SetValueToInputExtension(evt, ActionOperation, ActionFeature, CurrentElement, Value);
        }
        catch (er)
        {
            if (WebFormsOptions.AddLog)
                console.warn(""There was a problem in set value to input whene executing the command: "" + er + ""\nError in command: "" + CurrentElement);

            if (WebFormsOptions.AddMessageForProblemInSetValueToInput)
                cb_ShowMessage(WebFormsOptions.ProblemInSetValueToInputLang, ""problem"", WebFormsOptions.MessageDuration);
        }
    }

    return ElementPlaceList;
}

function cb_CountElements(result)
{
    if (!result)
        return 0;

    if (Array.isArray(result) || result instanceof NodeList || result instanceof HTMLCollection)
        return result.length;

    return 1;
}

function cb_GetElement(evt, elementPlace, lastElementPlaceList, transientDOM)
{
    let currentElement;
    if (elementPlace.substring(0, 1) == '$')
        currentElement = (elementPlace.length > 1) ? cb_GetElementByElementPlace(elementPlace.substring(1), evt.currentTarget, transientDOM) : evt.currentTarget;
    else if (elementPlace.substring(0, 1) == '!')
        currentElement = (elementPlace.length > 1) ? cb_GetElementByElementPlace(elementPlace.substring(1), evt.target, transientDOM) : evt.target;
    else
    {
        if (elementPlace == '-')
            currentElement = lastElementPlaceList;
        else
            currentElement = cb_GetElementByElementPlace(elementPlace, null, transientDOM);
    }

    return currentElement;
}

function cb_GetElementByElementPlace(ElementPlace, obj, TransientDOM)
{
    var element = cb_FetchElementByElementPlace(ElementPlace, obj, TransientDOM);

    if (element.tagName)
        if (element.tagName.toLowerCase() == ""template"")
        {
            const htmlString = element.innerHTML;

            const tmp = document.createElement(""div"");
            tmp.innerHTML = htmlString;

            const tags = tmp.children;
            return tags[0];
        }

    return element;
}

function cb_FetchElementByElementPlace(ElementPlace, obj, TransientDOM)
{
    try
    {
        if (ElementPlace.substring(0, 1) == '^')
            if (ElementPlace.length == 1)
            {
                return document.head;
            }
            else
            {
                return cb_GetElementByElementPlace(ElementPlace.substring(1), document.head);
            }

        if (ElementPlace.substring(0, 1) != '>')
            if (ElementPlace.Contains('|'))
                ElementPlace = '>' + ElementPlace

        var ElementPlaceFirstChar = ElementPlace.substring(0, 1);

        const CurrentDocument = TransientDOM ?? document;

        const FromPlace = (obj) ? obj : CurrentDocument;

        switch (ElementPlaceFirstChar)
        {
            case '<':
                var TagName = ElementPlace.substring(1).GetTextBefore('>');
                var TagIndex = 0;
                if (ElementPlace.length > (TagName.length + 2))
                {
                    TagIndex = ElementPlace.substring(TagName.length + 2);

                    if (TagIndex != '*')
                        TagIndex = parseInt(TagIndex);
                }
                if (TagIndex == '*')
                    return FromPlace.getElementsByTagName(TagName);
                else if (TagIndex >= 0)
                    return FromPlace.getElementsByTagName(TagName)[TagIndex];
                else
                    return FromPlace.getElementsByTagName(TagName)[FromPlace.getElementsByTagName(TagName).length + TagIndex];

            case '(':
                var TagNameAttr = ElementPlace.substring(1).GetTextBefore(')');
                var TagNameIndex = 0;
                if (ElementPlace.length > (TagNameAttr.length + 2))
                {
                    TagNameIndex = ElementPlace.substring(TagNameAttr.length + 2);

                    if (TagNameIndex != '*')
                        TagNameIndex = parseInt(TagNameIndex);
                }
                if (TagNameIndex == '*')
                    return FromPlace.getElementsByName(TagNameAttr);
                else if (TagNameIndex >= 0)
                    return FromPlace.getElementsByName(TagNameAttr)[TagNameIndex];
                else
                    return FromPlace.getElementsByName(TagNameAttr)[FromPlace.getElementsByName(TagNameAttr).length + TagNameIndex];

            case '{':
                var ClassName = ElementPlace.substring(1).GetTextBefore('}');
                var ClassIndex = 0;
                if (ElementPlace.length > (ClassName.length + 2))
                {
                    ClassIndex = ElementPlace.substring(ClassName.length + 2);

                    if (ClassIndex != '*')
                        ClassIndex = parseInt(ClassIndex);
                }
                if (ClassIndex == '*')
                    return FromPlace.getElementsByClassName(ClassName);
                else if (ClassIndex >= 0)
                    return FromPlace.getElementsByClassName(ClassName)[ClassIndex];
                else
                    return FromPlace.getElementsByClassName(ClassName)[FromPlace.getElementsByClassName(ClassName).length + ClassIndex];

            case '*':
                var Query = ElementPlace.substring(1);
                return FromPlace.querySelector(Query.Replace(""$[eq];"", '='));

            case '[':
                var Query = ElementPlace.substring(1);
                return FromPlace.querySelectorAll(Query.Replace(""$[eq];"", '='));

            case '~': return FromPlace;
            case '`': return window;
            case '%': return screen.orientation;

            case '>':
                var PlaceList = ElementPlace.substring(1).split('|');
                var TmpPlace;

                for (var i = 0; i < PlaceList.length; i++)
                {
                    var TmpElementPlace = PlaceList[i];
                    TmpPlace = (i == 0) ? cb_GetElementByElementPlace(TmpElementPlace, null, TransientDOM) : cb_GetElementByElementPlace(TmpElementPlace, TmpPlace);
                }

                return TmpPlace;

            case '/':
                var i = 0;
                while (ElementPlace.length > 0)
                {
                    if (ElementPlace.substring(0, 1) == '/')
                        i++;
                    else
                        break;

                    ElementPlace = ElementPlace.substring(1);
                }

                var TmpElementPlace = (obj) ? obj : cb_GetElementByElementPlace(ElementPlace, null, TransientDOM);

                while (i > 0)
                {
                    TmpElementPlace = TmpElementPlace.parentElement;
                    i--;
                }

                if ((ElementPlace.length > 0) && obj)
                    return cb_GetElementByElementPlace(ElementPlace, TmpElementPlace, TransientDOM);

                return TmpElementPlace;

            default: return FromPlace.getElementById(ElementPlace);
        }
    }
    catch (er)
    {
        if (WebFormsOptions.AddLog)
            console.warn(""Problem in determining element: "" + er + ""\nError in input place: "" + ElementPlace);

        if (WebFormsOptions.AddMessageForProblemInDeterminingElement)
            cb_ShowMessage(WebFormsOptions.ProblemInDeterminingElementLang, ""problem"", WebFormsOptions.MessageDuration);
    }
}

async function cb_FetchValue(evt, Value)
{
    try
    {
        Value = Value.substring(1);
      
        if (!Value)
            return Value;

        var ActionOperation = Value.substring(0, 1);

        if (ActionOperation == '@')
            return Value;

        if (ActionOperation == ':')
        {
            Value = Value.substring(1);
            return await cb_ReplaceInjectValue(evt, Value);
        }

        if (ActionOperation == '_')
        {
            var ScriptValue = Value.substring(1).Replace(""$[ln];"", ""\n"").FullTrim();

            if (WebFormsOptions.DisableEval)
            {
                if (WebFormsOptions.AddLog)
                    console.warn(""Access to the eval method is disabled but is being attempted.\nScript value:"" + ScriptValue);
                return """";
            }

            return eval(ScriptValue);
        }

        var ActionFeature = Value.substring(1, 2);
        Value = Value.substring(2);

        switch (ActionOperation)
        {
            case 'm':
                switch (ActionFeature)
                {
                    case 'r':
                        var MinValue = 0;
                        if (Value.Contains(','))
                        {
                            MinValue = Number(Value.GetTextAfter(','));
                            Value = Value.GetTextBefore(',');
                        }
                        var MaxValue = Number(Value);
                        return Math.floor(Math.random() * (MaxValue - MinValue)) + MinValue;

                    case 's': return evt.getModifierState(Value);
                }
                break;

            case 'd':
                var CurrentDate = new Date();
                switch (ActionFeature)
                {
                    case 'y': return CurrentDate.getFullYear();
                    case 'm': return CurrentDate.getMonth() + 1;
                    case 'd': return CurrentDate.getDay();
                    case 'h': return CurrentDate.getHours();
                    case 'i': return CurrentDate.getMinutes();
                    case 's': return CurrentDate.getSeconds();
                    case 'l': return CurrentDate.getMilliseconds();
                    case 'L':
                        if (Value.Contains('['))
                        {
                            var lines = localStorage.getItem(Value.GetTextBefore('[')).split(""\n"");
                            return lines[Value.GetTextAfter('[')];
                        }
                        else
                        {
                            var lines = localStorage.getItem(Value).split(""\n"");
                            var FirtsLine = lines[0];
                            lines.shift();
                            localStorage.setItem(Value, lines.join('\n'));

                            return FirtsLine;
                        }
                    case 'I':
                        var lines = localStorage.getItem(Value.GetTextBefore('[')).split(""\n"");

                        for (var i = 0; i < lines.length; i++)
                            if (lines[i].GetTextBefore('=') == Value.GetTextAfter('['))
                                return lines[i].GetTextAfter('=');
                        break;
                    case 'a': return document.visibilityState == ""visible"";
                }
                break;

            case 'c':
                switch (ActionFeature)
                {
                    case 'o': return cb_GetCookie(Value);
                    case 's':
                        if (Value.Contains(','))
                        {
                            var TmpValue = sessionStorage.getItem(Value.GetTextBefore(','));
                            sessionStorage.setItem(Value.GetTextBefore(','), Value.GetTextAfter(','));
                            return TmpValue;
                        }
                        else
                            return sessionStorage.getItem(Value);
                    case 'l':
                        var TmpValue = sessionStorage.getItem(Value);
                        sessionStorage.removeItem(Value);
                        return TmpValue;
                    case 'd':
                        if (Value.Contains(','))
                        {
                            var TmpValue = localStorage.getItem(Value.GetTextBefore(','));
                            localStorage.setItem(Value.GetTextBefore(','), Value.GetTextAfter(','));
                            return TmpValue;
                        }
                        else
                            return localStorage.getItem(Value);
                    case 't':
                        var TmpValue = localStorage.getItem(Value);
                        localStorage.removeItem(Value);
                        return TmpValue;
                    case 'm':
                    case 'M':
                        if (Value.Contains(','))
                        {
                            var funcName = Value.GetTextBefore(',');
                            var args = Value.GetTextAfter(',').split(',');

                            for (let i = 0; i < args.length; i++)
                                args[i] = args[i].Replace(""$[co];"", ',');

                            args = await cb_SetDynamicValueForArgs(evt, args);

                            if (ActionFeature == 'm')
                                return await cb_RunMethod(evt, funcName, args);
                            else
                                return await cb_RunModuleMethod(evt, funcName, args);
                        }

                        if (ActionFeature == 'm')
                            return await cb_RunMethod(evt, Value);
                        else
                            return await cb_RunModuleMethod(evt, Value);
                }
                break;

            case 'l':
                switch (ActionFeature)
                {
                    case 'u':
                        var url = Value;
                        var fetchScript = false;
                        if (url.Contains(','))
                        {
                            fetchScript = url.GetTextAfter(',') == '1';
                            url = url.GetTextBefore(',');
                        }
                        return await cb_GetUrl(url, fetchScript);
                    case 'h':
                        var url = Value.GetTextBefore(',');
                        var fetchScript = Value.GetTextAfter(',');
                        Value = Value.GetTextAfter(',');

                        if (Value.Contains(','))
                        {
                            fetchScript = Value.GetTextBefore(',') == '1';
                            Value = Value.GetTextAfter(',')
                        }
                        var urlData = await cb_GetUrl(url, fetchScript);
                        return cb_FetchInputPlace(urlData, Value);
                    case 'l':
                        var [url, line] = Value.split(',');
                        var urlData = await cb_GetUrl(url);
                        return cb_GetTextLine(urlData, line);
                    case 'i':
                        var [url, name, isINILike] = Value.split(',');
                        isINILike = (isINILike == '1');
                        var urlData = await cb_GetUrl(url);
                        return cb_GetINI(urlData, name, isINILike);
                    case 'j':
                        var url = Value.GetTextBefore(',');
                        var name = Value.GetTextAfter(',');
                        var urlData = await cb_GetUrl(url);
                        return cb_GetJSON(urlData, name);
                    case 'x':
                        var url = Value.GetTextBefore(',');
                        var name = Value.GetTextAfter(',');
                        var urlData = await cb_GetUrl(url, false, true);
                        return cb_GetXML((new XMLSerializer().serializeToString(urlData)), name);
                    case 'L':
                        if (Value.Contains('['))
                        {
                            var lines = sessionStorage.getItem(Value.GetTextBefore('[')).split(""\n"");
                            return lines[Value.GetTextAfter('[')];
                        }
                        else
                        {
                            var lines = sessionStorage.getItem(Value).split(""\n"");
                            var FirtsLine = lines[0];
                            lines.shift();
                            sessionStorage.setItem(Value, lines.join('\n'));

                            return FirtsLine;
                        }
                    case 'I':
                        var lines = sessionStorage.getItem(Value.GetTextBefore('[')).split(""\n"");

                        for (var i = 0; i < lines.length; i++)
                            if (lines[i].GetTextBefore('=') == Value.GetTextAfter('['))
                                return lines[i].GetTextAfter('=');
                }
                break;

            case 'M':
                switch (ActionFeature)
                {
                    case '#':
                        if (Value.Contains(','))
                        {
                            var funcName = Value.GetTextBefore(',');
                            var args = Value.GetTextAfter(',').split(',');

                            for (let i = 0; i < args.length; i++)
                                args[i] = args[i].Replace(""$[co];"", ',');

                            args = await cb_SetDynamicValueForArgs(evt, args);

                            return await cb_RunMathMethod(evt, funcName, args);
                        }
                        return await cb_RunMathMethod(evt, Value);
                }
                break;

            case 's':
                switch (ActionFeature)
                {
                    case 'c': return Value.GetTextAfter(',').Replace(' ', Value.GetTextBefore(','));
                    case 'p': return ' ';
                    case 'a': return '@';
                    case 'w': return screen.width;
                    case 'h': return screen.height;
                    case 'o': return screen.orientation.type;
                    case 'r': return screen.orientation.angle;
                }
                break;

            case 'p':
                switch (ActionFeature)
                {
                    case 't': return performance.timeOrigin;
                    case 'n': return performance.now();
                }
                break;

            case 'e':
                switch (ActionFeature)
                {
                    case 'k': return evt.key;
                    case 'w': return evt.which;
                    case 'x': return evt.clientX;
                    case 'y': return evt.clientY;
                    case 'X': return evt.pageX;
                    case 'Y': return evt.pageY;
                    case 'd': return evt.deltaY;
                }
                break;

            case 'w':
                switch (ActionFeature)
                {
                    case 'f': return window.location.href;
                    case 'P': return window.location.pathname;
                    case 'q': return window.location.search;
                    case 'h': return window.location.hash;
                    case 'H': return window.location.host;
                    case 'n': return window.location.hostname;
                    case 'T': return window.location.port;
                    case 'o': return window.location.origin;
                    case 's': return window.getSelection().toString();
                    case 'x': return window.scrollX;
                    case 'y': return window.scrollY;
                    case 'A':
                        var [wasmLanguage, wasmUrl, funcName, ...args] = Value.split(',');

                        for (let i = 0; i < args.length; i++)
                            args[i] = args[i].Replace(""$[co];"", ',');

                        args = await cb_SetDynamicValueForArgs(evt, args);

                        return await cb_RunWasmMethodResult(wasmLanguage, wasmUrl, funcName, args);
                }
                break;

            case 'n':
                switch (ActionFeature)
                {
                    case 'L': return navigator.language;
                    case 'o': return navigator.onLine;
                    case 'a': return navigator.userAgent;
                    case 'W':
                    case 'O':
                        var coords = await cb_GetGeoPosition();
                        if (ActionFeature == 'W')
                            return coords.latitude;
                        return coords.longitude;

                    case 'C': return await navigator.clipboard.readText();
                }
                break;

            case 'E':
                switch (ActionFeature)
                {
                    case 'V': return evt;
                    case 's': return cb_EventSerialize(evt);
                    case 'x': return evt.offsetX;
                    case 'y': return evt.offsetY;
                }
                break;

            case 'H':
                switch (ActionFeature)
                {
                    case 'H': return cb_ActionControlHashList.includes(String(Value));
                }
                break;

            case 'h':
                switch (ActionFeature)
                {
                    case 'm': return Value in window;
                    case 'M': return Value in cb_ModuleMethodMap;
                }
                break;

            case 'u':
                switch (ActionFeature)
                {
                    case 'd': return decodeURI(Value);
                    case 'e': return encodeURI(Value);
                }
                break;

            case 'f':
                switch (ActionFeature)
                {
                    case 'r':
                    case 'v':
                        return cb_StorageGet(Value);
                    case 'x': return cb_GetXML(cb_StorageGet(Value.GetTextBeforeLast(',')), Value.GetTextAfterLast(','));
                    case 'j': return cb_GetJSON(cb_StorageGet(Value.GetTextBeforeLast(',')), Value.GetTextAfterLast(','));
                    case 'i': return cb_GetINI(cb_StorageGet(Value.GetTextBeforeLast(',')), Value.GetTextAfterLast(','));
                    case 't': return cb_GetTextLine(cb_StorageGet(Value.GetTextBeforeLast(',')), Value.GetTextAfterLast(','));
                }
                break;

            case '$':
                var elementPlace = Value;

                if (ActionFeature == 'a')
                {
                    elementPlace = Value.GetTextBeforeLast(',');
                    Value = Value.GetTextAfterLast(',');
                }

                if (!elementPlace)
                    elementPlace = ""<body>"";

                var currentElement = cb_GetElement(evt, elementPlace);

                return cb_GetValue(evt, ActionFeature, Value, currentElement);
        }

        // Extension
        return await cb_FetchValueExtension(evt, ActionOperation, ActionFeature, Value);
    }
    catch (er)
    {
        if (WebFormsOptions.AddLog)
            console.warn(""There was a problem in fetch value whene executing the command: "" + er + ""\nError in value: "" + Value);

        if (WebFormsOptions.AddMessageForProblemInFetchValue)
            cb_ShowMessage(WebFormsOptions.ProblemInFetchValueLang, ""problem"", WebFormsOptions.MessageDuration);
    }
}

async function cb_SaveValue(evt, ActionOperation, ActionFeature, ActionValue, LastElementPlaceList, TransientDOM)
{
    try
    {
        var Name = ActionValue.GetTextAfter('=');
        var ElementPlace = ActionValue.GetTextBefore('=');

        if (!ElementPlace)
            ElementPlace = ""<body>"";

        var currentElement = cb_GetElement(evt, ElementPlace, LastElementPlaceList, TransientDOM);

        var isCache = (ActionOperation == 'c');

        // Fill Value For Sync Action
        var value;
        var tmpName = Name;
        if (tmpName.Contains('|'))
        {
            value = tmpName.GetTextAfter('|');
            tmpName = tmpName.GetTextBefore('|');
        }

        switch (ActionOperation)
        {
            case 'g':
            case 'c':
                // Sync Action
                var returnValue = cb_GetValue(evt, ActionFeature, value, currentElement);
                if (returnValue)
                {
                    cb_SetStorage(isCache, tmpName, returnValue);
                    return;
                }

                // Async Action
                switch (ActionFeature)
                {
                    case 'u':
                    var url = value;
                    var fetchScript = false;
                    if (url.Contains('|'))
                    {
                        fetchScript = url.GetTextAfter('|') == '1';
                        url = url.GetTextBefore('|');
                    }
                    var urlData = await cb_GetUrl(url, fetchScript);
                    cb_SetStorage(isCache, Name.GetTextBefore('|'), urlData);
                    return;
                }
        }

        // Extension
        await cb_SaveValueExtension(evt, ActionOperation, ActionFeature, Name, currentElement);
    }
    catch (er)
    {
        if (WebFormsOptions.AddLog)
            console.warn(""There was a problem in save value whene executing the command: "" + er + ""\nError in command: "" + ActionOperation + ActionFeature + ""\nError in value: "" + ActionValue);

        if (WebFormsOptions.AddMessageForProblemInSaveValue)
            cb_ShowMessage(WebFormsOptions.ProblemInSaveValueLang, ""problem"", WebFormsOptions.MessageDuration);
    }
}

function cb_GetValue(evt, action, value, currentElement)
{
    switch (action)
    {
        case 'i': return currentElement.id;
        case 'n': return currentElement.name;
        case 'v': return currentElement.value;
        case 'e': return currentElement.value.length.toString();
        case 'c': return currentElement.className;
        case 's': return currentElement.style.cssText;
        case 'l':
            if (currentElement.hasAttribute(""title""))
                return currentElement.getAttribute(""title"");
            return """";
        case 'A':
            if (currentElement.id)
            {
                var labelTag = document.querySelector('label[for=""' + currentElement.id + '""]');
                if (labelTag)
                    return labelTag.textContent;
            }
            return """";
        case 't': return currentElement.innerHTML;
        case 'o': return currentElement.outerHTML;
        case 'g': return currentElement.innerHTML.length;
        case 'a': return currentElement.getAttribute(value);
        case 'w': return getComputedStyle(currentElement).width;
        case 'h': return getComputedStyle(currentElement).height;
        case 'r': return (currentElement.hasAttribute(""readonly"") ? ""true"" : ""false"");
        case 'x': return currentElement.selectedIndex.toString();
        case 'I': return Array.from(currentElement.parentElement.children).indexOf(currentElement);
        case 'T': return currentElement.style.textAlign || ""left"";
        case 'L': return currentElement.childNodes.length;
        case 'V': return ((currentElement.style.visibility == ""hidden"") ? ""false"" : ""true"");
    }
}

function cb_SetStorage(IsCache, Name, Value)
{
    if (IsCache)
        localStorage.setItem(Name, Value);
    else
        sessionStorage.setItem(Name, Value);
}

function cb_GetStorage(IsCache, Name)
{
    if (IsCache)
        localStorage.getItem(Name);
    else
        sessionStorage.getItem(Name);
}

async function cb_SetDynamicValue(evt, Value, Spliter)
{
    var ValueArray = Value.split(Spliter);
    for (var index = 0; index < ValueArray.length; index++)
        if (ValueArray[index].length > 0)
        {
            if (ValueArray[index].substring(0, 1) == '@')
                ValueArray[index] = await cb_FetchValue(evt, ValueArray[index]);
        }

    return ValueArray.join(Spliter);
}

async function cb_ReplaceInjectValue(evt, text)
{
    const regex = /\$\[@([^\]]+)\];/g;
    const matches = [];

    let match;
    while ((match = regex.exec(text)) !== null)
        matches.push(match);

    if (matches.length === 0)
        return text;

    const replacements = await Promise.all(matches.map(async (m) =>
        {
            const key = m[1];
            var fetchValue = await cb_FetchValue(evt, '@' + key);
            if (fetchValue)
                fetchValue = fetchValue.toString();

            return fetchValue ?? """";
        })
    );

    let result = text;
    matches.forEach((m, i) =>
    {
        result = result.replace(m[0], replacements[i]);
    });

    return result;
}

async function cb_SetDynamicForValue(evt, Value)
{
    if (typeof Value === ""string"")
        if (Value.substring(0, 1) == '@')
            return await cb_FetchValue(evt, Value);

    return Value;
}

async function cb_SetDynamicValueForArgs(evt, args)
{
    if (args)
        for (let i = 0; i < args.length; i++)
        {
            let tmpValue = await cb_SetDynamicForValue(evt, args[i]);

            if (tmpValue)
                args[i] = cb_ConvertDynamicValue(tmpValue);
            else
                args[i] = cb_ConvertDynamicValue(args[i]);
        }

    return args;
}

/* End Execute Web-Forms */

/* Start Cache */

function cb_UsedCache(evt, RequestName, RequestNameForCache)
{
    var SessionCacheValue = sessionStorage.getItem(RequestName);
    if (SessionCacheValue)
    {
        cb_SetWebFormsValues(evt, RequestNameForCache, SessionCacheValue, true, true);
        return true;
    }

    var LocalCacheValue = localStorage.getItem(RequestName);
    if (LocalCacheValue)
    {
        var LocalCacheDateValue = localStorage.getItem(RequestName + ""-date"");
        if (LocalCacheDateValue)
        {
            var CacheDate = new Date(LocalCacheDateValue);
            var CurrentDate = new Date();

            if (CacheDate.getTime() > CurrentDate.getTime())
            {
                cb_SetWebFormsValues(evt, RequestNameForCache, LocalCacheValue, true, true);
                return true;
            }
            else
            {
                localStorage.removeItem(RequestName);
                localStorage.removeItem(RequestName + ""-date"");
            }
        }
        else
        {
            cb_SetWebFormsValues(evt, RequestNameForCache, LocalCacheValue, true, true);
            return true;
        }
    }

    return false;
}

function cb_CleanExpiredCache()
{
    const now = new Date().getTime();

    for (let i = 0; i < localStorage.length; i++)
    {
        const key = localStorage.key(i);

        if (key.endsWith(""-date""))
        {
            const expirationDate = new Date(localStorage.getItem(key)).getTime();

            if (now >= expirationDate)
            {
                const originalKey = key.replace(""-date"", """");
                localStorage.removeItem(originalKey);
                localStorage.removeItem(key);
            }
        }
    }
}

function cb_LocalCacheExists(key)
{
    return localStorage.getItem(key) !== null;
}

function cb_SessionCacheExists(key)
{
    return sessionStorage.getItem(key) !== null;
}

/* End Cache */

/* Start URL / Path */

function cb_GetUrl(url, fetchScript = false, isXML = false)
{
    return new Promise(function (resolve, reject)
    {
        var xhr = new XMLHttpRequest();
        xhr.open(""GET"", url, true);

        xhr.onload = function ()
        {
            if (xhr.status === 200)
            {
                var responseText = xhr.responseText;

                if (fetchScript)
                    setTimeout(() => cb_AppendJavaScriptTag(responseText), 500);

                if (isXML)
                {
                    try
                    {
                        var parser = new DOMParser();
                        var xmlDoc = parser.parseFromString(responseText, ""application/xml"");
                        resolve(xmlDoc);
                    }
                    catch (er)
                    {
                        reject(""Failed to parse XML: "" + er.message);
                    }
                }
                else
                    resolve(cb_RemoveScripts(responseText));
            }
            else
                reject(""HTTP Error: "" + xhr.status);
        };

        xhr.onerror = () => reject(""Network Error"");
        xhr.send();
    });
}

function cb_ConvertToWebSocketUrl(url)
{
    const currentUrl = window.location.href;
    const protocol = window.location.protocol === ""https:"" ? ""wss:"" : ""ws:"";
    const host = window.location.host;

    if (url.startsWith('?'))
        return `${protocol}//${host}${currentUrl.split(host)[1]}${url}`;

    if (url.startsWith(""http://"") || url.startsWith(""https://""))
        return url.replace(/^http/, ""ws"");

    if (url.startsWith(""ws://"") || url.startsWith(""wss://""))
        return url;

    if (!url.includes(""://""))
    {
        const currentPath = currentUrl.split(host)[1];
        const basePath = currentPath.endsWith('/') ? currentPath : currentPath.substring(0, currentPath.lastIndexOf('/') + 1);

        const sanitizedUrl = url.startsWith('/') ? url.substring(1) : url;
        return `${protocol}//${host}${basePath}${sanitizedUrl}`;
    }

    return url;
}

function cb_AddQueryToUrl(formAction, formDataSerialize)
{
    var url = formAction;
    var separator = url.includes('?') ? '&' : '?';
    if (formDataSerialize)
        url += separator + formDataSerialize;

    return url;
}

function cb_MasterPages(evt, viewState)
{
    GetBack(evt, location.pathname + location.search + location.hash, viewState);
}
window.cb_MasterPages = cb_MasterPages;

/* End URL / Path */

/* Start Cookie */

function cb_GetCookie(key)
{
    const Cookies = document.cookie.split(';');
    for (let cookie of Cookies)
    {
        cookie = cookie.trim();
        if (cookie.startsWith(key + '='))
            return cookie.substring(key.length + 1);
    }

    return """";
}

function cb_SetCookie(key, value, seconds, path = ""/"")
{
    let expires = """";
    if (seconds)
    {
        const date = new Date();
        date.setTime(date.getTime() + (seconds * 1000));
        expires = ""; expires="" + date.toUTCString();
    }
    document.cookie = key + ""="" + value + expires + ""; path="" + path;
}

/* End Cookie */

/* Start Condition */

async function cb_WaitForCondition(interval, checkFunc, ...args)
{
    return new Promise((resolve, reject) =>
    {
        const check = async () =>
        {
            try
            {
                const Result = await checkFunc(...args);
                if (Result)
                    resolve();
                else if (Result === null)
                    reject();
                else
                    setTimeout(check, interval);
            }
            catch (er)
            {
                reject(er);
            }
        };
        check();
    });
}

// A Value Of True Satisfies The Time Condition, And A Value Of Null Escapes The Time Condition
async function cb_CheckCondition(evt, ActionControl)
{
    try
    {
        var Action = ActionControl.GetTextBefore('=');
        var Control = ActionControl.GetTextAfter('=');

        // Set Dynamic Value
        Control = await cb_SetDynamicValue(evt, Control, '|');

        switch (Action)
        {
            case ""gt"": return (Control.GetTextBefore('|') > Control.GetTextAfter('|'));
            case ""lt"": return (Control.GetTextBefore('|') < Control.GetTextAfter('|'));
            case ""et"": return (Control.GetTextBefore('|') == Control.GetTextAfter('|'));
            case ""Nt"": return (Control.GetTextBefore('|') != Control.GetTextAfter('|'));
            case ""ex"": return (Control ? true : false);
            case ""nx"": return (Control ? false : true);
            case ""tr"": return (cb_IsTrue(Control));
            case ""fa"": return (!cb_IsTrue(Control));
            case ""mm"": return (matchMedia(Control).matches);
            case ""nm"": return !(matchMedia(Control).matches);
            case ""In"": return Control.GetTextAfter('|').includes(Control.GetTextBefore('|'));
            case ""Nn"": return !(Control.GetTextAfter('|').includes(Control.GetTextBefore('|')));
            case ""eE"": return cb_GetElementByElementPlace(Control);
            case ""nE"": return (cb_GetElementByElementPlace(Control) ? false : true);
            case ""re"":
            case ""rn"":
                {
                    var value = Control.GetTextBefore('|');
                    var pattern = Control.GetTextAfter('|');
                    try
                    {
                        var regex = new RegExp(pattern);
                        var result = regex.test(value);

                        if (Action == ""re"")
                            return result;
                        else
                            return !result;
                    }
                    catch
                    {
                        if (WebFormsOptions.AddLog)
                            console.error(""Invalid regex pattern:"", pattern);
                        return null;
                    }
                }
            case ""ct"":
            case ""cf"":
                {
                    if (cb_ConfirmIsAccept === undefined) 
                    {
                        var [text, type, title, okText, cancelText] = Control.split('|');

                        if (!text)
                            text = ""Are you sure you want to proceed?"";
                        if (!type)
                            type = ""none"";
                        if (!title)
                            title = ""Confirm"";
                        if (!okText)
                            okText = ""OK"";
                        if (!cancelText)
                            cancelText = ""Cancel"";

                        cb_ShowConfirm(text, type, title, okText, cancelText).catch(() => { });
                    }
                    else if (cb_ConfirmIsAccept === true)
                    {
                        cb_ConfirmIsAccept = undefined;

                        if (Action == ""ct"")
                            return true;
                        else
                            return null;
                    }
                    else if (cb_ConfirmIsAccept === false)
                    {
                        cb_ConfirmIsAccept = undefined;

                        if (Action == ""ct"")
                            return null;
                        else
                            return true;
                    }
                    return;
                }
        }

        // Extension
        return await cb_CheckConditionExtension(evt, Action, Control);
    }
    catch (er)
    {
        if (WebFormsOptions.AddLog)
            console.warn(""There was a problem in check condition whene executing the command: "" + er + ""\nError in action control: "" + ActionControl);

        if (WebFormsOptions.AddMessageForProblemInCheckCondition)
            cb_ShowMessage(WebFormsOptions.ProblemInCheckConditionLang, ""problem"", WebFormsOptions.MessageDuration);
    }
}

/* End Condition */

/* Start Unit Testing */

function cb_AssertEqual(element, tag, recursiveDepth = 0)
{
    if (recursiveDepth > 0)
        console.log(cb_Indent(recursiveDepth) + ""Start inner testing depth: "" + recursiveDepth);
    else
        console.log(""Start unit testing assert equal"");

    // Normalize Both Inputs Into DOM Elements
    const temp = document.createElement(""div"");
    let newElement = null;

    if (tag instanceof Node)
        newElement = tag.cloneNode(true);
    else if (typeof tag === ""string"")
    {
        temp.innerHTML = tag.trim();
        newElement = temp.firstElementChild;
    }
    else
    {
        console.info(cb_Indent(recursiveDepth) + ""[ASSERT FAIL] Invalid 'tag' type — must be a DOM Node or HTML string."");

        if (recursiveDepth > 0)
            console.log(cb_Indent(recursiveDepth) + ""End inner testing depth: "" + recursiveDepth);
        else
            console.log(""End unit testing assert equal"");
        return false;
    }

    if (!newElement)
    {
        console.info(cb_Indent(recursiveDepth) + ""[ASSERT FAIL] Failed to create comparable element from input."");

        if (recursiveDepth > 0)
            console.log(cb_Indent(recursiveDepth) + ""End inner testing depth: "" + recursiveDepth);
        else
            console.log(""End unit testing assert equal"");
        return false;
    }

    let isEqual = true;

    // Compare Tag Names
    if (element.tagName !== newElement.tagName)
    {
        console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Tag mismatch: expected <${newElement.tagName}> but got <${element.tagName}>`);
        isEqual = false;
    }

    // Compare Attributes
    const elAttrs = [...element.attributes].map(a => [a.name, a.value]);
    const newAttrs = [...newElement.attributes].map(a => [a.name, a.value]);

    const elAttrMap = Object.fromEntries(elAttrs);
    const newAttrMap = Object.fromEntries(newAttrs);

    for (let [name, value] of Object.entries(newAttrMap))
        if (elAttrMap[name] !== value)
        {
            console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Attribute mismatch on ""${name}"": expected ""${value}"" but got ""${elAttrMap[name] ?? 'undefined'}""`);
            isEqual = false;
        }

    for (let [name] of Object.entries(elAttrMap))
        if (!(name in newAttrMap))
        {
            console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Unexpected attribute ""${name}"" found on element`);
            isEqual = false;
        }

    // Compare Classes
    const elClasses = [...element.classList];
    const newClasses = [...newElement.classList];

    if (elClasses.sort().join("" "") !== newClasses.sort().join("" ""))
    {
        console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Class list mismatch: expected [${newClasses}] but got [${elClasses}]`);
        isEqual = false;
    }

    // Compare Styles
    const elStyle = element.getAttribute(""style"") || """";
    const newStyle = newElement.getAttribute(""style"") || """";
    if (elStyle.trim() !== newStyle.trim())
    {
        console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Style mismatch: expected ""${newStyle}"" but got ""${elStyle}""`);
        isEqual = false;
    }

    // Compare Form Values
    const tagName = element.tagName.toLowerCase();
    if ([""input"", ""textarea"", ""select""].includes(tagName))
    {
        let val1 = element.value?.trim?.() ?? """";
        let val2 = newElement.value?.trim?.() ?? """";

        // Normalize checkbox/radio values
        if (element.type === ""checkbox"" || element.type === ""radio"")
        {
            val1 = element.checked;
            val2 = newElement.checked;
        }

        if (val1 !== val2)
        {
            console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Value mismatch in <${tagName}>: expected ""${val2}"" but got ""${val1}""`);
            isEqual = false;
        }
    }

    // Compare Child Nodes (Deep Comparison)
    const elChildren = [...element.childNodes].filter(n => n.nodeType !== Node.COMMENT_NODE);
    const newChildren = [...newElement.childNodes].filter(n => n.nodeType !== Node.COMMENT_NODE);

    if (elChildren.length !== newChildren.length)
    {
        console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Different number of child nodes: expected ${newChildren.length}, got ${elChildren.length}`);
        isEqual = false;
    }

    const len = Math.min(elChildren.length, newChildren.length);
    for (let i = 0; i < len; i++)
    {
        const c1 = elChildren[i];
        const c2 = newChildren[i];

        if (c1.nodeType !== c2.nodeType)
        {
            console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Node type mismatch at child index ${i}`);
            isEqual = false;
            continue;
        }

        if (c1.nodeType === Node.TEXT_NODE)
        {
            const t1 = c1.textContent.trim();
            const t2 = c2.textContent.trim();
            if (t1 !== t2)
            {
                console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Text mismatch at index ${i}: expected ""${t2}"" but got ""${t1}""`);
                isEqual = false;
            }
        }
        else if (c1.nodeType === Node.ELEMENT_NODE)
        {
            const result = cb_AssertEqual(c1, c2, recursiveDepth + 1);
            if (!result)
                isEqual = false;
        }
    }

    if (isEqual)
        console.info(cb_Indent(recursiveDepth) + ""[ASSERT PASS] Elements are deeply equal"");
    else
        console.warn(cb_Indent(recursiveDepth) + ""[ASSERT FAIL] Differences found"");


    if (recursiveDepth > 0)
        console.log(cb_Indent(recursiveDepth) + ""End inner testing depth: "" + recursiveDepth);
    else
        console.log(""End unit testing assert equal"");

    return isEqual;
}

/* End Unit Testing */

/* Start Style */

function cb_AddInlineStyle(el, styleString, overwrite = true)
{
    const currentStyle = el.getAttribute(""style"") || """";

    const styleObj = {};
    currentStyle.split("";"").forEach(pair =>
    {
        if (!pair.trim())
            return;
        const [prop, val] = pair.split("":"");
        if (prop && val)
            styleObj[prop.trim()] = val.trim();
    });


    styleString.split("";"").forEach(pair =>
    {
        if (!pair.trim())
            return;
        const [prop, val] = pair.split("":"");
        if (!prop || !val)
            return;

        const p = prop.trim();
        const v = val.trim();

        if (overwrite)
            styleObj[p] = v;
        else
            if (!(p in styleObj))
                styleObj[p] = v;
    });

    const finalStyle = Object.entries(styleObj).map(([prop, val]) => `${prop}: ${val}`).join(""; "");

    el.setAttribute(""style"", finalStyle);
}

/* End Style */

/* Start String */

function cb_MatchesPattern(pattern, input)
{
    if (pattern.startsWith(""re:""))
    {
        const regexBody = pattern.slice(3);
        const regex = new RegExp(regexBody);
        return regex.test(input);
    }

    if (pattern.includes('*') || pattern.includes('?'))
    {
        let regexPattern = pattern.replace(/[.+^${}()|[\]\\]/g, ""\\$&"");
        regexPattern = regexPattern.replace(/\*/g, "".*"").replace(/\?/g, '.');
        const regex = new RegExp(`^${regexPattern}$`);
        return regex.test(input);
    }

    return pattern === input;
}

function cb_IsLiteralString(v)
{
    return (
        typeof v === ""string"" &&
        (
            (v.startsWith('""') && v.endsWith('""')) || (v.startsWith(""'"") && v.endsWith(""'""))
        )
    );
}

function cb_ConvertDynamicValue(v)
{
    if (typeof v !== ""string"")
        return v;

    if (/^[+-]?\d+(\.\d+)?$/.test(v.trim()))
        return Number(v);

    if (cb_IsLiteralString(v))
        return v.slice(1, -1);

    try
    {
        return JSON.parse(v);
    }
    catch
    {
        /* empty */
    }

    try
    {
        return Function('""use strict""; return (' + v + ')')();
    }
    catch
    {
        /* empty */
    }

    return v;
}

function cb_Indent(depth)
{
    return ""-"".repeat(depth) + "" "";
}

function cb_JSONParsePath(path)
{
    return path.match(/[^.[\]]+/g) || [];
}

function cb_IsTrue(value)
{
    if (value === true || value === 1)
        return true;

    if (typeof value === ""string"")
    {
        const v = value.trim().toLowerCase();
        return [""true"", ""1"", ""yes"", ""y"", ""on"", ""t"", ""enable"", ""active""].includes(v);
    }

    return false;
}

/* End String */

/* Start Extension Methods */

String.prototype.toDOM = function ()
{
    var DivTag = document.createElement(""div"");
    DivTag.innerHTML = this;

    return DivTag.innerHTML;
};

String.prototype.HasTag = function ()
{
    const tempElement = document.createElement(""div"");
    tempElement.innerHTML = this;
    return tempElement.childNodes.length > 0;
}

String.prototype.FullTrim = function ()
{
    return this.trim().replace(/^\s\n+|\s\n+$/g, """");
};

String.prototype.TrimStart = function ()
{
    return this.replace(/^[\s\n]+/, """");
};

String.prototype.IsInput = function ()
{
    var TagName = this.toLowerCase();

    switch (TagName)
    {
        case ""input"":
        case ""textarea"":
        case ""select"":
        case ""file"":
        case ""button"":
            return true;
    }
    return false;
};

String.prototype.GetTextBefore = function (Text)
{
    if (!Text)
        return this;

    var index = this.indexOf(Text);
    if (index === -1)
        return """";

    return this.substring(0, index);
};

String.prototype.GetTextBeforeLast = function (Text)
{
    if (!Text)
        return this;

    var index = this.lastIndexOf(Text);
    if (index === -1)
        return """";

    return this.substring(0, index);
};

String.prototype.GetTextAfter = function (Text)
{
    if (!Text)
        return this;

    var index = this.indexOf(Text);
    if (index === -1)
        return """";

    return this.substring(index + Text.length);
};

String.prototype.GetTextAfterLast = function (Text)
{
    if (!Text)
        return this;

    var index = this.lastIndexOf(Text);
    if (index === -1)
        return """";

    return this.substring(index + Text.length);
};

String.prototype.DeleteHtmlClass = function(ClassName)
{
    var ClassText = this;

    if (!ClassText)
        return """";

    var ClassNameIndex = ClassText.indexOf(ClassName);

    var Space = (ClassNameIndex == 0) ? """" : ' ';
        
    ClassText = ClassText.replace(Space + ClassName, """");

    if (ClassText)
        if (ClassText[0] == ' ')
            ClassText = ClassText.slice(1);

    return ClassText;
};

String.prototype.DeleteHtmlStyle = function (StyleName)
{
    var StyleText = this;
    if (!StyleText) return """";

    var StartIndex = StyleText.indexOf(StyleName);
    if (StartIndex == -1)
        return StyleText;

    var EndIndex = StartIndex + StyleName.length;
    if (StyleText[EndIndex] == ';')
        EndIndex++;

    return StyleText.substring(0, StartIndex) + StyleText.substring(EndIndex);
};

String.prototype.Contains = function (Text)
{
    if (!this)
        return false;

    return this.indexOf(Text) !== -1;
};

String.prototype.ContainsWithSpliter = function (Text, Spliter)
{
    return (Spliter + this + Spliter).indexOf(Spliter + Text + Spliter) !== -1;
};

String.prototype.ContainsNameWithSpliter = function (Text, Spliter, SpliterNameValue)
{
    return (Spliter + this).indexOf(Spliter + Text + SpliterNameValue) !== -1;
};

String.prototype.Replace = function (SearchValue, ReplaceValue)
{
    var MainText = this;
    
    if (!MainText)
        return MainText;

    if (!SearchValue)
        return MainText;

    while (MainText.indexOf(SearchValue) > -1)
        MainText = MainText.replace(SearchValue, ReplaceValue);

    return MainText;
};

String.prototype.EndsWith = function (Suffix)
{
    return this.indexOf(Suffix, this.length - Suffix.length) !== -1;
};

String.prototype.GetUnit = function ()
{
    var Value = this.toLowerCase();

    if (Value.EndsWith('%'))
        return '%';
    if (Value.EndsWith(""vmax""))
        return ""vmax"";
    if (Value.EndsWith(""vmin""))
        return ""vmin"";
    if (Value.EndsWith(""rem""))
        return ""rem"";
    if (Value.EndsWith(""pt""))
        return ""pt"";
    if (Value.EndsWith(""px""))
        return ""px"";
    if (Value.EndsWith(""em""))
        return ""em"";
    if (Value.EndsWith(""vw""))
        return ""vw"";
    if (Value.EndsWith(""vh""))
        return ""vh"";
    if (Value.EndsWith(""ch""))
        return ""ch"";
    if (Value.EndsWith(""ex""))
        return ""ex"";
    if (Value.EndsWith(""cm""))
        return ""cm"";
    if (Value.EndsWith(""mm""))
        return ""mm"";
    if (Value.EndsWith(""in""))
        return ""in"";
    if (Value.EndsWith(""pc""))
        return ""pc"";

    return """";
};

String.prototype.IsNumber = function ()
{
    var num = parseFloat(this);
    return !isNaN(num) && isFinite(num);
};

/* End Extension Methods */

/* Start Queue Management */

let cb_QueueList = [];
let cb_QueueIsPending = false;
let cb_QueueDebounceTimer = null;

async function cb_ProcessQueue()
{
    if (cb_QueueIsPending || cb_QueueList.length === 0)
        return;

    cb_QueueIsPending = true;
    const { action, resolve, reject } = cb_QueueList.shift();

    try
    {
        const result = await action();
        resolve(result);
    }
    catch (er)
    {
        reject(er);
    }
    finally
    {
        cb_QueueIsPending = false;

        // Next Execution
        cb_ProcessQueue();
    }
}

function cb_AddToQueue(action)
{
    if (!WebFormsOptions.UseQueue)
    {
        return new Promise((resolve, reject) =>
        {
            try
            {
                const result = action();

                if (result instanceof Promise)
                    result.then(resolve).catch(reject);
                else
                    resolve(result);
            }
            catch (er)
            {
                reject(er);
            }
        });
    }

    return new Promise((resolve, reject) =>
    {
        cb_QueueList.push({ action, resolve, reject });
        cb_ProcessQueue();
    });
}

function cb_RunInQueue(action)
{
    if (WebFormsOptions.UseDebounceDelay)
    {
        clearTimeout(cb_QueueDebounceTimer);
        cb_QueueDebounceTimer = setTimeout(() => cb_AddToQueue(action), WebFormsOptions.QueueDebounceDelay);
    }
    else
        cb_AddToQueue(action);
}

const cb_PreRunnerIntervals = {};

function cb_DeletePreRunnerInterval(id)
{
    if (cb_PreRunnerIntervals[id])
    {
        clearInterval(cb_PreRunnerIntervals[id]);
        delete cb_PreRunnerIntervals[id];
    }
}

function cb_SetPreRunnerQueue(PreRunner, CodeExecutor)
{
    if (PreRunner.length < 1)
    {
        CodeExecutor();
        return;
    }

    var FirstChar = PreRunner[0].substring(0, 1);

    switch (FirstChar)
    {
        case '(':
            var periodMiliSecond = PreRunner[0].GetTextAfter('(');
            var id;
            if (periodMiliSecond.Contains('|'))
            {
                id = periodMiliSecond.GetTextAfter('|');
                periodMiliSecond = periodMiliSecond.GetTextBefore('|');
            }
            if (!id)
                for (var i = 0; i < 1000000; i++)
                    if (!cb_PreRunnerIntervals[i])
                        id = i;
                    
            PreRunner.shift();
            cb_PreRunnerIntervals[id] = setInterval(() => cb_SetPreRunnerQueue(PreRunner, CodeExecutor), periodMiliSecond);
            break;
        case ':':
            var delayMiliSecond = PreRunner[0].GetTextAfter(':');
            PreRunner.shift();
            setTimeout(() => cb_SetPreRunnerQueue(PreRunner, CodeExecutor), delayMiliSecond);
            break;
        case ',':
            var numberOfRepetitions = parseInt(PreRunner[0].GetTextAfter(','));
            PreRunner.shift();
            for (let i = 0; i < numberOfRepetitions; i++)
                cb_SetPreRunnerQueue(PreRunner.slice(), CodeExecutor);
            break;
    }
}

async function cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue, LastElementPlaceList, TransientDOM)
{
    if (PreRunner.length < 1)
    {
        // Return Element Place. Is Array Object List For QueryAll, And Array Object List With One Item For Other
        return await cb_SetValueToInput(evt, ActionOperation, ActionFeature, ActionValue, LastElementPlaceList, TransientDOM);
    }

    var FirstChar = PreRunner[0].substring(0, 1);

    switch (FirstChar)
    {
        case '(':
            var periodMiliSecond = PreRunner[0].GetTextAfter('(');
            var id;
            if (periodMiliSecond.Contains('|'))
            {
                id = periodMiliSecond.GetTextAfter('|');
                periodMiliSecond = periodMiliSecond.GetTextBefore('|');
            }
            if (!id)
                for (var i = 0; i < 1000000; i++)
                    if (!cb_PreRunnerIntervals[i])
                        id = i;

            PreRunner.shift();
            cb_PreRunnerIntervals[id] = setInterval(async function () { await cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue); }, periodMiliSecond);
            break;
        case ':':
            var delayMiliSecond = PreRunner[0].GetTextAfter(':');
            PreRunner.shift();
            setTimeout(async function () { await cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue); }, delayMiliSecond);
            break;
        case ',':
            var numberOfRepetitions = PreRunner[0].GetTextAfter(',');
            PreRunner.shift();
            for (var i = 0; i < numberOfRepetitions; i++)
                await cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue);
    }
}

/* End Queue Management */

/* Start Async Await */

function cb_RunAsync(fn)
{
    return new Promise(resolve =>
    {
        setTimeout(() =>
        {
            try
            {
                const result = fn();
                resolve(result);
            }
            catch (er)
            {
                if (WebFormsOptions.AddLog)
                    console.error(""Async error:"", er);
                resolve();
            }
        }, 0);
    });
}

/* End Async Await */

/* Start State Management */

class cb_SPA
{
    static contentCache = {};
    static titleCache = {};
    static scrollX = {};
    static scrollY = {};

    static init()
    {
        const html = cb_SPA.getCurrentContent();

        cb_SPA.contentCache[window.location.pathname] = html;
        cb_SPA.titleCache[window.location.pathname] = document.title;
        cb_SPA.scrollX[window.location.pathname] = window.scrollX;
        cb_SPA.scrollY[window.location.pathname] = window.scrollY;

        history.replaceState({
            html,
            title: document.title,
            scrollX: window.scrollX,
            scrollY: window.scrollY
        }, """", window.location.pathname + window.location.search + window.location.hash);

        window.addEventListener(""popstate"", (event) =>
        {
            const pathname = window.location.pathname;
            if (event.state && event.state.html)
            {
                cb_GetResponseLocation().innerHTML = event.state.html;
                document.title = event.state.title || """";
                window.scrollTo(event.state.scrollX || 0, event.state.scrollY || 0);

                cb_SPA.contentCache[pathname] = event.state.html;
                cb_SPA.titleCache[pathname] = event.state.title;
                cb_SPA.scrollX[pathname] = event.state.scrollX;
                cb_SPA.scrollY[pathname] = event.state.scrollY;
            }
            else
                cb_SPA.render(pathname);
        });
    }

    static getCurrentContent()
    {
        const el = cb_GetResponseLocation();
        return el ? el.innerHTML : """";
    }

    static render(pathname)
    {
        const html = cb_SPA.contentCache[pathname];
        if (html)
        {
            cb_GetResponseLocation().innerHTML = html;
            document.title = cb_SPA.titleCache[pathname] || """";
            window.scrollTo(cb_SPA.scrollX[pathname] || 0, cb_SPA.scrollY[pathname] || 0);
        }
        else
            window.location.href = pathname; // Fallback
    }

    static deleteState(pathname)
    {
        delete cb_SPA.contentCache[pathname];
        delete cb_SPA.titleCache[pathname];
        delete cb_SPA.scrollX[pathname];
        delete cb_SPA.scrollY[pathname];

        history.replaceState(null, """", pathname);
    }

    static clearAllStates()
    {
        cb_SPA.contentCache = {};
        cb_SPA.titleCache = {};
        cb_SPA.scrollX = {};
        cb_SPA.scrollY = {};

        history.replaceState(null, """", window.location.pathname + window.location.search + window.location.hash);
    }

    static saveState(pathname, linkTitle)
    {
        const html = cb_SPA.getCurrentContent();
        cb_SPA.contentCache[pathname] = html;
        cb_SPA.titleCache[pathname] = linkTitle || document.title;
        cb_SPA.scrollX[pathname] = window.scrollX;
        cb_SPA.scrollY[pathname] = window.scrollY;

        history.pushState({
            html,
            title: linkTitle || document.title,
            scrollX: window.scrollX,
            scrollY: window.scrollY
        }, """", pathname);
    }
}

cb_SPA.init();

function cb_SetMainSubmitTypeToButtons(obj)
{
    const buttons = obj.querySelectorAll('input[type=""button""], button[type=""button""]');

    buttons.forEach(button =>
    {
        if (button.getAttribute(""main-type"") === ""submit"")
        {
            button.setAttribute(""type"", ""submit"");
            button.removeAttribute(""main-type"");
        }
    });
}

function cb_SetStatePreservation(HtmlDOM, TransientDOM)
{
    // Save Current DOM state Including Select Values
    const selectValues = {};
    HtmlDOM.querySelectorAll(""select"").forEach((select, index) =>
    {
        selectValues[`select-${index}`] = select.value;
    });

    // Save And Transfer Event Listeners
    const elementsWithEvents = Object.keys(cb_EventRegistry);

    // Restore Select Values To TransientDOM
    TransientDOM.querySelectorAll(""select"").forEach((select, index) =>
    {
        if (selectValues[`select-${index}`])
            select.value = selectValues[`select-${index}`];
    });

    // Transfer Event Listeners From Old Elements To New Elements
    elementsWithEvents.forEach(objId =>
    {
        const events = cb_EventRegistry[objId];

        let originalElement = null;
        if (objId.startsWith(""cb_""))
            originalElement = document.querySelector(`[data-cb-id=""${objId}""]`);
        else
            originalElement = document.getElementById(objId);

        if (originalElement && HtmlDOM.contains(originalElement))
        {
            let newElement = null;
            if (objId.startsWith(""cb_""))
                newElement = TransientDOM.querySelector(`[data-cb-id=""${objId}""]`);
            else
                newElement = TransientDOM.getElementById(objId);

            if (newElement)
            {
                Object.keys(events).forEach(eventType =>
                {
                    events[eventType].forEach(listener =>
                    {
                        newElement.addEventListener(eventType, listener.callback);

                        if (!cb_EventRegistry[objId])
                            cb_EventRegistry[objId] = {};
                        if (!cb_EventRegistry[objId][eventType])
                            cb_EventRegistry[objId][eventType] = [];

                        // Ensure We Don't Duplicate Listeners
                        const exists = cb_EventRegistry[objId][eventType].some(l => l.functionName === listener.functionName);

                        if (!exists)
                            cb_EventRegistry[objId][eventType].push(listener);
                    });
                });
            }
        }
    });

    return TransientDOM;
}

var cb_ActionControlHashList = [];

function cb_Checksum(text, useDecodeURI = false)
{
    if (useDecodeURI)
        text = decodeURIComponent(text);

    let sum = 0;
    const mod = 65536;
    const shift = 5;

    for (let i = 0; i < text.length; i++)
    {
        const c = text.charCodeAt(i);
        sum = (((sum << shift) | (sum >>> (16 - shift))) ^ c) % mod;
    }

    return sum;
}

async function cb_GetHashSHA256(text)
{
    const encoder = new TextEncoder();
    const data = encoder.encode(text);
    const hashBuffer = await crypto.subtle.digest(""SHA-256"", data);
    const hashArray = Array.from(new Uint8Array(hashBuffer));
    const hashHex = hashArray.map(b => b.toString(16).padStart(2, '0')).join("""");
    return hashHex;
}

/* End State Management */

/* Start Loader */

let cb_LoaderTimeout = null;
let cb_LoaderStartTime = null;

function cb_ShowLoader()
{
    if (!WebFormsOptions.UseLoader)
        return;

    cb_CreateLoader();
    const loader = document.getElementById(""cb_Loader"");
    if (!loader)
        return

    loader.style.display = ""flex"";
    cb_LoaderStartTime = Date.now();

    if (cb_LoaderTimeout)
    {
        clearTimeout(cb_LoaderTimeout);
        cb_LoaderTimeout = null;
    }

    cb_LoaderTimeout = setTimeout(() => { cb_HideLoader(); }, WebFormsOptions.HideLoaderTimeout);
}

function cb_HideLoader()
{
    const loader = document.getElementById(""cb_Loader"");
    if (!loader)
        return;

    const elapsed = Date.now() - (cb_LoaderStartTime || 0);
    const remaining = 300 - elapsed;

    const hide = () =>
    {
        loader.style.display = ""none"";
        if (cb_LoaderTimeout)
        {
            clearTimeout(cb_LoaderTimeout);
            cb_LoaderTimeout = null;
        }
        cb_LoaderStartTime = null;
    };

    if (remaining > 0)
        setTimeout(hide, remaining);
    else
        hide();
}

function cb_CreateLoader()
{
    if (document.getElementById(""cb_Loader""))
        return;

    // Making Outer Element
    const loader = document.createElement(""div"");
    loader.id = ""cb_Loader"";
    Object.assign(loader.style,
    {
        display: ""none"",
        position: ""fixed"",
        top: '0',
        left: '0',
        width: ""100%"",
        height: ""100%"",
        background: ""rgba(0,0,0,0.4)"",
        zIndex: ""9999"",
        justifyContent: ""center"",
        alignItems: ""center""
    });

    // Making Spinner
    const spinner = document.createElement(""div"");
    Object.assign(spinner.style,
    {
        width: ""50px"",
        height: ""50px"",
        border: ""6px solid #ccc"",
        borderTopColor: ""#3498db"",
        borderRadius: ""50%"",
        animation: ""spin 1s linear infinite""
    });

    // Adding Spinner To The Loader
    loader.appendChild(spinner);
    document.body.appendChild(loader);

    // Adding keyframes To Style
    const style = document.createElement(""style"");
    style.textContent = `
    @keyframes spin
    {
      to { transform: rotate(360deg); }
    }
  `;
    document.head.appendChild(style);
}

/* End Loader */

/* Start Service Worker */

const cb_ServiceWorker = (function ()
{
    async function register(path, scopePath)
    {
        if (!path)
            path = WebFormsOptions.RegisterServicePath;

        if (!scopePath)
            scopePath = WebFormsOptions.RegisterServiceScopePath;

        if (!(""serviceWorker"" in navigator))
        {
            console.log(""[Service Worker] no Service Worker support"");
            return;
        }

        const reg = await navigator.serviceWorker.register(path, { scope: scopePath });


        if (reg.waiting)
            try { await rpcSend({ action: ""skip-waiting"" }); } catch { /* empty */ }

        await navigator.serviceWorker.ready;

        await new Promise(r => setTimeout(r, WebFormsOptions.ServiceWorkerWaitForControl));

        if (!navigator.serviceWorker.controller)
        {
            if (WebFormsOptions.ReloadServiceWorkerIfNeed)
                location.reload();

            if (WebFormsOptions.AddLog)
                console.warn(""[Service Worker] Service Worker installed but not controlling page yet — a reload may be required in this browser."");
        }

        return reg;
    }

    // RPC Helper
    function rpcSend(message)
    {
        return new Promise((resolve, reject) =>
        {
            const waitForController = async () =>
            {
                await cb_ServiceWorker.register();
                await navigator.serviceWorker.ready;

                if (!navigator.serviceWorker.controller)
                {
                    console.warn(""[Service Worker] Service Worker installed but not controlling page yet."");
                    return;
                }

                const channel = new MessageChannel();
                const id = Math.random().toString(36).slice(2);
                channel.port1.onmessage = ev =>
                {
                    const msg = ev.data || {};
                    if (msg && msg.id === id)
                    {
                        if (msg.result && msg.result.ok)
                            resolve(msg.result.data);
                        else
                            reject(new Error((msg.result && msg.result.error) || ""unknown""));
                    }
                };
                setTimeout(() => reject(new Error(""[Service Worker] Service Worker rpc timeout"")), 10000);
                navigator.serviceWorker.controller.postMessage({ id, ...message }, [channel.port2]);
            };

            waitForController();
        });
    }

    // Public API (Concise)
    const API = {
        register,

        // Cache
        cache: {
            add: (url, ttl) => rpcSend({ action: ""cache-add"", payload: { url, ttl } }),
            remove: url => rpcSend({ action: ""cache-remove"", payload: { url } }),
            has: url => rpcSend({ action: ""cache-has"", payload: { url } }).then(r => r.has),
            list: () => rpcSend({ action: ""cache-list""}).then(r => r.urls),
            clear: () => rpcSend({ action: ""cache-clear"" }),
            setTTL: (url, seconds) => rpcSend({ action: ""set-ttl"", payload: { url, ttl: seconds } })
        },
        preCacheStatic: assets => rpcSend({ action: ""static-precache"", payload: { assets } }),
        listStatic: () => rpcSend({ action: ""static-list"" }).then(r => r.urls),

        // Routing
        routeSet: (pattern, type = ""networkonly"", cacheDynamic = false) => rpcSend({ action: ""route-set"", payload: { pattern, type, cacheDynamic } }),
        routeClear: () => rpcSend({ action: ""route-clear"" }),
        routeAlias: (from, to) => rpcSend({ action: ""route-alias"", payload: { from, to } }),
        routeRemoveAlias: (from) => rpcSend({ action: ""route-remove-alias"", payload: { from } }),
        routeRemove: (pattern) => rpcSend({ action: ""route-remove"", payload: { pattern } }),

        // Helper
        isRegistered: async () => !!(await navigator.serviceWorker.getRegistration())
    };

    return API;
})();

window.cb_ServiceWorker = cb_ServiceWorker;

if (WebFormsOptions.RegisterServiceWorker)
{
    (async () => {
        await cb_ServiceWorker.register();
        await navigator.serviceWorker.ready;
        console.log(""[Service Worker] Service Worker is ready and controlling this page."");
    })();
}

async function cb_ServiceWorkerPush()
{
    const reg = await navigator.serviceWorker.ready;
    const permission = await Notification.requestPermission();

    if (permission !== ""granted"")
        return;

    const sub = await reg.pushManager.subscribe({
        userVisibleOnly: true,
        applicationServerKey: cb_UrlBase64ToUint8Array(WebFormsOptions.ServiceWorkerPushVapidPublicKey)
    });

    await fetch(WebFormsOptions.UseServiceWorkerPushSubscribe,
    {
        method: ""POST"",
        headers: { ""Content-Type"": ""application/json"" },
        body: JSON.stringify(sub)
    });
}

function cb_UrlBase64ToUint8Array(base64String)
{
    const padding = '='.repeat((4 - base64String.length % 4) % 4);
    const base64 = (base64String + padding)
        .replace(/-/g, '+')
        .replace(/_/g, '/');
    const rawData = atob(base64);
    return Uint8Array.from([...rawData].map(char => char.charCodeAt(0)));
}

async function cb_InitServiceWorker()
{
    if (WebFormsOptions.UseServiceWorkerPush)
    {
        const permission = await Notification.requestPermission();
        if (permission === ""granted"")
            await cb_ServiceWorkerPush();
    }
}

cb_InitServiceWorker();

/* End Service Worker */

/* Start Message */

// Common styles
const cb_OverlayStyle = `
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.6);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 1000;
        `;

const cb_AlertBoxStyle = `
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
            width: 320px;
            max-width: 90%;
            overflow: hidden;
            animation: popIn 0.3s ease-out;
            text-align: center;
        `;

const cb_HeaderStyle = `
            color: white;
            padding: 20px;
            margin: 0;
            font-weight: 600;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        `;

const cb_TextStyle = `
            padding: 25px 20px;
            color: #333;
            line-height: 1.5;
            margin: 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        `;

const cb_ButtonStyle = `
            background-color: #AEAEAE;
            color: white;
            border: none;
            padding: 12px 30px;
            border-radius: 50px;
            cursor: pointer;
            font-size: 16px;
            font-weight: 600;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            margin: 0 10px 20px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        `;

const cb_CancelButtonStyle = `
            background: #f0f0f0;
            color: #333;
            border: none;
            padding: 12px 30px;
            border-radius: 50px;
            cursor: pointer;
            font-size: 16px;
            font-weight: 600;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            margin: 0 10px 20px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        `;

const cb_MessageStyle = `
            color: white;
            padding: 16px 24px;
            border-radius: 8px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-weight: 500;
            animation: messageSlideIn 0.3s ease-out;
            display: flex;
            align-items: center;
            justify-content: space-between;
            min-width: 300px;
            max-width: 90%;
            background-color: #AEAEAE;
        `;

const cb_MessageContainerStyle = `
            position: fixed;
            top: 20px;
            left: 50%;
            transform: translateX(-50%);
            z-index: 1001;
            display: flex;
            flex-direction: column;
            gap: 12px; /* spacing between messages */
            align-items: center;
        `;

const cb_MessageButtonStyle = `
            background: transparent;
            color: white;
            border: none;
            margin-left: 15px;
            cursor: pointer;
            font-size: 18px;
            font-weight: bold;
            padding: 0;
            width: 24px;
            height: 24px;
            display: flex;
            align-items: center;
            justify-content: center;
            border-radius: 50%;
        `;

function cb_AddAnimationStyles()
{
    if (!document.getElementById(""alertAnimations""))
    {
        const style = document.createElement(""style"");
        style.id = ""alertAnimations"";
        style.textContent = `
                    @keyframes popIn {
                        0% {
                            opacity: 0;
                            transform: scale(0.8) translateY(-20px);
                        }
                        100% {
                            opacity: 1;
                            transform: scale(1) translateY(0);
                        }
                    }
                    @keyframes messageSlideIn {
                        0% {
                            opacity: 0;
                        }
                        100% {
                            opacity: 1;
                        }
                    }
                    @keyframes messageFadeOut {
                        0% {
                            opacity: 1;
                        }
                        100% {
                            opacity: 0;
                        }
                    }
                `;
        document.head.appendChild(style);
    }
}

function cb_MessageTypeStyle(type)
{
    switch (type)
    {
        case ""warning"": return WebFormsOptions.MessageWarningStyle;
        case ""problem"": return WebFormsOptions.MessageProblemStyle;
        case ""help"": return WebFormsOptions.MessageHelpStyle;
        case ""success"": return WebFormsOptions.MessageSuccessStyle;
        case ""none"": return WebFormsOptions.MessageNoneStyle;
    }
}

function cb_ShowAlert(text, type = ""none"", title = ""Alert"", okText = ""OK"")
{
    const overlay = document.createElement(""div"");
    overlay.setAttribute(""style"", cb_OverlayStyle);

    const alertBox = document.createElement(""div"");
    alertBox.setAttribute(""style"", cb_AlertBoxStyle);

    const alertHeader = document.createElement(""h2"");
    alertHeader.textContent = title;
    alertHeader.setAttribute(""style"", cb_HeaderStyle + cb_MessageTypeStyle(type));

    const alertText = document.createElement('p');
    alertText.textContent = text;
    alertText.setAttribute(""style"", cb_TextStyle);

    const okButton = document.createElement(""button"");
    okButton.textContent = okText;
    okButton.setAttribute(""style"", cb_ButtonStyle);

    alertBox.appendChild(alertHeader);
    alertBox.appendChild(alertText);
    alertBox.appendChild(okButton);

    overlay.appendChild(alertBox);

    document.body.appendChild(overlay);

    cb_AddAnimationStyles();

    okButton.addEventListener(""click"", function ()
    {
        document.body.removeChild(overlay);
    });

    // Close With Escape Key
    document.addEventListener(""keydown"", function closeOnEscape(e)
    {
        if (e.key === ""Escape"" && document.body.contains(overlay))
        {
            document.body.removeChild(overlay);
            document.removeEventListener(""keydown"", closeOnEscape);
        }
    });
}

window.cb_ConfirmIsAccept = undefined;

function cb_ShowConfirm(text = ""Are you sure you want to proceed?"", type = ""none"", title = ""Confirm"", okText = ""OK"", cancelText = ""Cancel"")
{
    cb_ConfirmIsAccept = null;

    return new Promise((resolve, reject) =>
    {

        const overlay = document.createElement(""div"");
        overlay.setAttribute(""style"", cb_OverlayStyle);

        const confirmBox = document.createElement(""div"");
        confirmBox.setAttribute(""style"", cb_AlertBoxStyle);

        const confirmHeader = document.createElement(""h2"");
        confirmHeader.textContent = title;
        confirmHeader.setAttribute(""style"", cb_HeaderStyle + cb_MessageTypeStyle(type));

        const confirmText = document.createElement('p');
        confirmText.textContent = text;
        confirmText.setAttribute(""style"", cb_TextStyle);

        const buttonContainer = document.createElement(""div"");

        const cancelButton = document.createElement(""button"");
        cancelButton.textContent = cancelText;
        cancelButton.setAttribute(""style"", cb_CancelButtonStyle);

        const okButton = document.createElement(""button"");
        okButton.textContent = okText;
        okButton.setAttribute(""style"", cb_ButtonStyle);

        buttonContainer.appendChild(cancelButton);
        buttonContainer.appendChild(okButton);

        confirmBox.appendChild(confirmHeader);
        confirmBox.appendChild(confirmText);
        confirmBox.appendChild(buttonContainer);

        overlay.appendChild(confirmBox);
        document.body.appendChild(overlay);

        cb_AddAnimationStyles();

        // OK
        okButton.addEventListener(""click"", function handleOK()
        {
            document.body.removeChild(overlay);
            cb_ConfirmIsAccept = true;
            resolve();
        });

        // Cancel
        cancelButton.addEventListener(""click"", function handleCancel()
        {
            document.body.removeChild(overlay);
            cb_ConfirmIsAccept = false;
            reject();
        });

        // ESC
        const escListener = (e) =>
        {
            if (e.key === ""Escape"" && document.body.contains(overlay))
            {
                document.body.removeChild(overlay);
                document.removeEventListener(""keydown"", escListener);
                cb_ConfirmIsAccept = false;
                reject();
            }
        };
        document.addEventListener(""keydown"", escListener);
    });
}
window.cb_ShowConfirm = cb_ShowConfirm;
window.cb_ConfirmIsAccept = cb_ConfirmIsAccept;

function cb_ShowMessage(text, type, duration = 0)
{
    const message = document.createElement(""div"");
    message.setAttribute(""style"", cb_MessageStyle + cb_MessageTypeStyle(type));

    const messageText = document.createElement(""span"");
    messageText.textContent = text;

    const closeButton = document.createElement(""button"");
    closeButton.textContent = '×';
    closeButton.setAttribute(""style"", cb_MessageButtonStyle);
    closeButton.setAttribute(""title"", ""Close"");

    message.appendChild(messageText);
    message.appendChild(closeButton);

    var messageContainer;
    if (document.getElementById(""cb_MessageContainer""))
        messageContainer = document.getElementById(""cb_MessageContainer"")
    else
    {
        messageContainer = document.createElement(""div"");
        messageContainer.id = ""cb_MessageContainer"";
        messageContainer.setAttribute(""style"", cb_MessageContainerStyle);
    }

    messageContainer.appendChild(message);

    document.body.appendChild(messageContainer);

    cb_AddAnimationStyles();

    // Add Event Listener To Close Button
    closeButton.addEventListener(""click"", function ()
    {
        message.style.animation = ""messageFadeOut 0.3s ease-out forwards"";
        setTimeout(() =>
        {
            if (messageContainer)
                if (messageContainer.contains(message))
                {
                    messageContainer.removeChild(message);

                    if (messageContainer.childNodes.length == 0)
                        document.body.removeChild(messageContainer);
                }
        }, 300);
    });

    // Auto-Remove After Duration If Specified
    if (duration > 0)
    {
        setTimeout(() =>
        {
            if (messageContainer)
                if (messageContainer.contains(message))
                {
                    message.style.animation = ""messageFadeOut 0.3s ease-out forwards"";
                    setTimeout(() =>
                    {
                        if (messageContainer)
                            if (messageContainer.contains(message))
                            {
                                messageContainer.removeChild(message);

                                if (messageContainer.childNodes.length == 0)
                                    document.body.removeChild(messageContainer);
                            }
                    }, 300);
                }
        }, duration);
    }
}

function cb_ShowConnectionError(errorCode)
{
    var message = WebFormsOptions.ConnectionErrorLang;

    if (errorCode)
        message += "": "" + errorCode;

    cb_ShowMessage(message, ""problem"", WebFormsOptions.MessageDuration);
}

/* End Message */

/* Start Tag Change */

function cb_ReplaceDeep(element, value, newValue, replaceFirstAttribute)
{
    for (let node of element.childNodes)
        if (node.nodeType === Node.TEXT_NODE && node.nodeValue.includes(value))
            node.nodeValue = node.nodeValue.Replace(value, newValue);

    if (replaceFirstAttribute)
        cb_ReplaceStartTag(element, value, newValue);

    for (let child of element.children)
        cb_ReplaceDeep(child, value, newValue, true);
}

function cb_ReplaceStartTag(element, value, newValue)
{
    const attrs = Array.from(element.attributes || []);

    attrs.forEach(attr =>
    {
        let { name: attrName, value: attrValue } = attr;

        let newAttrValue = attrValue.includes(value) ? attrValue.split(value).join(newValue) : attrValue;

        if (attrName.includes(value))
        {
            let newAttrName = attrName.split(value).join(newValue);
            element.setAttribute(newAttrName, newAttrValue);
            element.removeAttribute(attrName);
        }
        else if (newAttrValue !== attrValue)
            element.setAttribute(attrName, newAttrValue);
    });
}

async function cb_SetReflection(element, tag)
{
    const temp = document.createElement(""div"");
    let newElement = null;

    if (tag instanceof Node)
    {
        newElement = tag.cloneNode(true);

        // Tranfer EventListeners from cb_EventRegistry
        const objId = tag.id || tag.getAttribute(""data-cb-id"");
        if (objId && cb_EventRegistry[objId])
        {
            for (const event of Object.keys(cb_EventRegistry[objId]))
                for (const entry of cb_EventRegistry[objId][event])
                    await cb_AddEventListener(newElement, event, entry.functionName, entry.args);
        }
    }
    else if(typeof tag === ""string"")
    {
        temp.innerHTML = tag.trim();
        newElement = temp.firstElementChild;
    }
    else
        return;

    if (!newElement)
        return;

    // Merge Classes
    newElement.classList.forEach(cls => element.classList.add(cls));

    // Merge Styles
    const existingStyle = element.style;
    const incomingStyle = newElement.style;
    for (let i = 0; i < incomingStyle.length; i++)
    {
        const prop = incomingStyle[i];
        existingStyle.setProperty(prop, incomingStyle.getPropertyValue(prop));
    }

    // Merge Other Attributes (skip class/style/events)
    for (let attr of newElement.attributes)
    {
        if (/^(class|style|on)/i.test(attr.name))
            continue;
        element.setAttribute(attr.name, attr.value);
    }

    // Append Children Only If They Don't Already Exist
    newElement.childNodes.forEach(node =>
    {
        if (!element.contains(node))
            element.appendChild(node.cloneNode(true));
    });
}

/* End Tag Change */

/* Start Call Method */

async function cb_RunMethod(evt, funcName, args)
{
    // Set Dynamic Value For Arguments
    args = await cb_SetDynamicValueForArgs(evt, args);

    if (args)
        return cb_GetMethod(funcName)(...args);
    else
        return cb_GetMethod(funcName)();
}

function cb_GetMethod(funcName)
{
    const noop = () => { }; // Empty Fallback To Avoid Runtime Errors

    if (WebFormsOptions.DisableCallMethod)
    {
        if (WebFormsOptions.AddLog)
            console.warn(""Access to the call method is disabled but is being attempted.\nMethod: "" + funcName);
        return """";
    }

    if (WebFormsOptions.UseCallMethodOnlyInAcceptedList)
        if (!WebFormsOptions.CallMethodOnlyInAcceptedList.some(p => cb_MatchesPattern(p, funcName)))
        {
            if (WebFormsOptions.AddLog)
                console.warn(""Access to call method is only possible in the list, but is being attempted.\nMethod: "" + funcName);
            return """";
        }

    const fn = window[funcName];
    if (typeof fn === ""function"")
        return fn;
    else
    {
        if (WebFormsOptions.AddLog)
            console.warn(`Method ""${funcName}"" not found or not loaded yet.`);
        return noop;
    }
}
window.cb_GetMethod = cb_GetMethod;

async function cb_RunModuleMethod(evt, funcName, args)
{
    // Set Dynamic Value For Arguments
    args = await cb_SetDynamicValueForArgs(evt, args);

    if (args)
        return cb_GetModuleMethod(funcName)(...args);
    else
        return cb_GetModuleMethod(funcName)();
}

async function cb_RunMathMethod(evt, funcName, args)
{
    // Set Dynamic Value For Arguments
    args = await cb_SetDynamicValueForArgs(evt, args);

    if (args)
        return window[""Math""][funcName](...args);
    else
        return window[""Math""][funcName]();
}

/* End Call Method */

/* Start Wasm */

async function cb_RunWasmMethodResult(wasmLanguage, wasmUrl, funcName, args = [])
{
    switch (wasmLanguage)
    {
        case 'c': return (await cb_RunWasmMethod_C(wasmUrl, funcName, args)).result;
        case ""rust"": return (await cb_RunWasmMethod_Rust(wasmUrl, funcName, args)).result;
        case ""csharp"": return (await cb_RunWasmMethod_CSharp(wasmUrl, funcName, args)).result;
        case ""go"": return (await cb_RunWasmMethod_Go(wasmUrl, funcName, args)).result;
        case ""java"": return (await cb_RunWasmMethod_Java(wasmUrl, funcName, args)).result;
        case ""as"": return (await cb_RunWasmMethod_AS(wasmUrl, funcName, args)).result;
    }

    return null;
}

// RUST
async function cb_RunWasmMethod_Rust(wasmUrl, funcName, args = [])
{
    let instance;
    let memory;

    const imports = {
        env: {
            memory: new WebAssembly.Memory({ initial: 256 }),
            table: new WebAssembly.Table({ initial: 0, element: ""anyfunc"" }),
            __wbindgen_throw: (ptr, len) =>
            {
                const memView = new Uint8Array(memory.buffer);
                const msg = new TextDecoder(""utf-8"").decode(memView.subarray(ptr, ptr + len));
                throw new Error(msg);
            }
        }
    };

    try
    {
        const response = await fetch(wasmUrl);
        const bytes = await response.arrayBuffer();
        const { instance: inst } = await WebAssembly.instantiate(bytes, imports);
        instance = inst;
        memory = instance.exports.memory || imports.env.memory;
    }
    catch (er)
    {
        throw new Error(`Failed to instantiate WASM module: ${er.message}`);
    }

    const method = instance.exports[funcName];
    if (typeof method !== ""function"")
        throw new Error(`Function ""${funcName}"" not found. Available: ${Object.keys(instance.exports).join("", "")}`);

    // Inputs
    const processedArgs = [];
    for (const arg of args)
    {
        if (typeof arg === ""string"")
        {
            if (!instance.exports.alloc)
            {
                if (WebFormsOptions.AddLog)
                    console.warn(""alloc not exported: cannot pass strings to WASM directly"");

                processedArgs.push(0);
            }
            else
            {
                const encoder = new TextEncoder();
                const encoded = encoder.encode(arg + ""\0"");
                const ptr = instance.exports.alloc(encoded.length);
                new Uint8Array(memory.buffer).set(encoded, ptr);
                processedArgs.push(ptr);
            }
        }
        else
        {
            processedArgs.push(arg);
        }
    }

    let result = method(...processedArgs);

    // Output Detection
    if (typeof result === ""number"" && result > 0 && memory)
    {
        try
        {
            const memView = new Uint8Array(memory.buffer);
            let end = result;

            while (end < memView.length && memView[end] !== 0)
                end++;

            const text = new TextDecoder(""utf-8"").decode(memView.subarray(result, end));
            if (text.trim().length > 0)
                result = text;
        }
        catch
        {
            /* empty */
        }
    }

    return { result, memory };
}

// C/C++
async function cb_RunWasmMethod_C(wasmUrl, funcName, args = [])
{
    let instance;
    let memory;

    const imports = {
        env: {
            memory: new WebAssembly.Memory({ initial: 256 }),
            table: new WebAssembly.Table({ initial: 0, element: ""anyfunc"" }),
            abort: () => { throw new Error(""WASM aborted""); }
        }
    };

    try
    {
        const response = await fetch(wasmUrl);
        const bytes = await response.arrayBuffer();
        const { instance: inst } = await WebAssembly.instantiate(bytes, imports);
        instance = inst;
        memory = instance.exports.memory || imports.env.memory;
    }
    catch (er)
    {
        throw new Error(`Failed to instantiate WASM module: ${er.message}`);
    }

    const method = instance.exports[funcName];
    if (typeof method !== ""function"")
        throw new Error(`Function ""${funcName}"" not found. Available: ${Object.keys(instance.exports).join("", "")}`);

    // Inputs
    const processedArgs = [];
    for (const arg of args)
    {
        if (typeof arg === ""string"")
        {
            if (WebFormsOptions.AddLog)
                console.warn(""Passing strings requires custom alloc in C/C++ wasm"");

            processedArgs.push(0);
        }
        else
            processedArgs.push(arg);
    }

    let result = method(...processedArgs);

    // Output Detection
    if (typeof result === ""number"" && result > 0 && memory)
    {
        try
        {
            const memView = new Uint8Array(memory.buffer);
            let end = result;

            while (end < memView.length && memView[end] !== 0)
                end++;

            const text = new TextDecoder(""utf-8"").decode(memView.subarray(result, end));
            if (text.trim().length > 0)
                result = text;
        }
        catch
        {
            /* empty */
        }
    }

    return { result, memory };
}

// C# (.NET)
async function cb_RunWasmMethod_CSharp(wasmUrl, funcName, args = [])
{
    let instance;
    let memory;

    const imports = { env: {} };

    try
    {
        const response = await fetch(wasmUrl);
        const bytes = await response.arrayBuffer();
        const { instance: inst } = await WebAssembly.instantiate(bytes, imports);
        instance = inst;
        memory = instance.exports.memory;
    }
    catch (er)
    {
        throw new Error(`C# WASM init failed: ${er.message}`);
    }

    const method = instance.exports[funcName];
    if (typeof method !== ""function"")
        throw new Error(`Function ${funcName} not found in C# WASM exports`);

    // Inputs
    const processedArgs = [];
    for (const arg of args)
    {
        if (typeof arg === ""string"")
        {
            const encoder = new TextEncoder();
            const encoded = encoder.encode(arg);
            const ptr = instance.exports.malloc(encoded.length);
            new Uint8Array(memory.buffer).set(encoded, ptr);
            processedArgs.push(ptr, encoded.length);
        }
        else
            processedArgs.push(arg);
    }

    let result = method(...processedArgs);

    // Output Detection
    if (typeof result === ""number"" && result > 0)
    {
        const memView = new Uint8Array(memory.buffer);
        let end = result;

        while (end < memView.length && memView[end] !== 0)
            end++;

        const text = new TextDecoder(""utf-8"").decode(memView.subarray(result, end));
        if (text.trim().length > 0)
            result = text;
    }

    return { result, memory };
}

// GO
async function cb_RunWasmMethod_Go(wasmUrl, funcName, args = [])
{
    let instance;
    let memory;

    const imports = { env: {} };

    try
    {
        const response = await fetch(wasmUrl);
        const bytes = await response.arrayBuffer();
        const { instance: inst } = await WebAssembly.instantiate(bytes, imports);
        instance = inst;
        memory = instance.exports.memory;
    }
    catch (er)
    {
        throw new Error(`Go WASM init failed: ${er.message}`);
    }

    const method = instance.exports[funcName];
    if (typeof method !== ""function"")
        throw new Error(`Function ${funcName} not found in Go WASM exports`);

    // Inputs
    const processedArgs = [];
    for (const arg of args)
    {
        if (typeof arg === ""string"")
        {
            const encoder = new TextEncoder();
            const encoded = encoder.encode(arg);
            const ptr = instance.exports.malloc(encoded.length);
            new Uint8Array(memory.buffer).set(encoded, ptr);
            processedArgs.push(ptr, encoded.length);
        }
        else
            processedArgs.push(arg);
    }

    let result = method(...processedArgs);

    // Output Detection
    if (typeof result === ""number"" && result > 0)
    {
        const memView = new Uint8Array(memory.buffer);
        let end = result;

        while (end < memView.length && memView[end] !== 0)
            end++;

        const text = new TextDecoder(""utf-8"").decode(memView.subarray(result, end));
        if (text.trim().length > 0)
            result = text;
    }

    return { result, memory };
}

// JAVA
async function cb_RunWasmMethod_Java(wasmUrl, funcName, args = [])
{
    let instance;
    let memory;

    const imports = { env: {} };

    try
    {
        const response = await fetch(wasmUrl);
        const bytes = await response.arrayBuffer();
        const { instance: inst } = await WebAssembly.instantiate(bytes, imports);
        instance = inst;
        memory = instance.exports.memory;
    }
    catch (er)
    {
        throw new Error(`Java WASM init failed: ${er.message}`);
    }

    const method = instance.exports[funcName];
    if (typeof method !== ""function"")
        throw new Error(`Function ${funcName} not found in Java WASM exports`);

    // Inputs
    const processedArgs = [];
    for (const arg of args)
    {
        if (typeof arg === ""string"")
        {
            const encoder = new TextEncoder();
            const encoded = encoder.encode(arg);
            const ptr = instance.exports.malloc(encoded.length);
            new Uint8Array(memory.buffer).set(encoded, ptr);
            processedArgs.push(ptr, encoded.length);
        }
        else
            processedArgs.push(arg);
    }

    let result = method(...processedArgs);

    // Output Detection
    if (typeof result === ""number"" && result > 0)
    {
        const memView = new Uint8Array(memory.buffer);
        let end = result;

        while (end < memView.length && memView[end] !== 0)
            end++;

        const text = new TextDecoder(""utf-8"").decode(memView.subarray(result, end));
        if (text.trim().length > 0)
            result = text;
    }

    return { result, memory };
}

// AssemblyScript
async function cb_RunWasmMethod_AS(wasmUrl, funcName, args = [])
{
    const response = await fetch(wasmUrl);
    const bytes = await response.arrayBuffer();

    const memory = new WebAssembly.Memory({ initial: 256 });
    const imports = {
        env: {
            memory,
            table: new WebAssembly.Table({ initial: 0, element: ""anyfunc"" }),
        }
    };

    const { instance } = await WebAssembly.instantiate(bytes, imports);

    const method = instance.exports[funcName];
    if (typeof method !== ""function"")
        throw new Error(`Function ""${funcName}"" not found. Available: ${Object.keys(instance.exports).join("", "")}`);

    const processedArgs = [];
    const stringPointers = [];

    // Inputs
    for (const arg of args)
    {
        if (typeof arg === ""string"")
        {
            if (!instance.exports.__new) throw new Error(""__new not exported for string allocation"");
            const encoder = new TextEncoder();
            const encoded = encoder.encode(arg);
            const ptr = instance.exports.__new(encoded.length, 0); // 0: String Type In AssemblyScript Runtime
            new Uint8Array(memory.buffer).set(encoded, ptr);
            processedArgs.push(ptr);
            processedArgs.push(encoded.length);
            stringPointers.push(ptr);
        }
        else
            processedArgs.push(arg);
    }

    let result = method(...processedArgs);

    // Output Detection
    if (typeof result === ""number"" && result > 0)
    {
        try
        {
            const memView = new Uint8Array(memory.buffer);
            let end = result;

            while (end < memView.length && memView[end] !== 0)
                end++;

            result = new TextDecoder(""utf-8"").decode(memView.subarray(result, end));
        }
        catch
        {
            /* empty */
        }
    }

    return { result, memory };
}

/* End Wasm */

/* Start Module */

const cb_ModuleMethodMap = {};
const cb_LoadedModules = {};

async function cb_LoadModule(modulePath, methods = [])
{
    if (WebFormsOptions.DisableLoadModule)
    {
        if (WebFormsOptions.AddLogForModule)
            console.warn(""Access for load the module is disabled but is being attempted.\nModule path: "" + modulePath);
        return null;
    }

    if (WebFormsOptions.UseLoadModulePathOnlyInAcceptedList)
        if (!WebFormsOptions.LoadModulePathOnlyInAcceptedList.some(p => cb_MatchesPattern(p, modulePath)))
        {
            if (WebFormsOptions.AddLogForModule)
                console.warn(""Access to load the module is only possible in the list, but is being attempted.\nModule path: "" + modulePath);
            return null;
        }

    if (cb_LoadedModules[modulePath])
    {
        if (WebFormsOptions.AddLogForModule)
            console.warn(`Module ""${modulePath}"" is already loaded.`);
        return cb_LoadedModules[modulePath];
    }

    try
    {
        const mod = await import(modulePath);
        cb_LoadedModules[modulePath] = mod;

        const methodsToLoad = methods.length > 0 ? methods : Object.keys(mod).filter(k => typeof mod[k] === ""function"");

        methodsToLoad.forEach(method => { cb_ModuleMethodMap[method] = mod[method]; });

        if (WebFormsOptions.AddLogForModule)
            console.log(`Module ""${modulePath}"" loaded (${methodsToLoad.length} methods).`);

        return mod;
    }
    catch (er)
    {
        console.error(`Error loading module ${modulePath}:`, er);
        throw er;
    }
}

// Unload An Entire Module And Remove Its Methods
function cb_UnloadModule(modulePath)
{
    const mod = cb_LoadedModules[modulePath];
    if (mod)
    {
        Object.keys(mod).forEach(method =>
        {
            if (cb_ModuleMethodMap[method] && cb_ModuleMethodMap[method] === mod[method])
                delete cb_ModuleMethodMap[method];
        });

        delete cb_LoadedModules[modulePath];
        if (WebFormsOptions.AddLogForModule)
            console.log(`Module ""${modulePath}"" and its methods were unloaded.`);
    }
    else
        if (WebFormsOptions.AddLogForModule)
            console.warn(`Module ""${modulePath}"" not found or not loaded.`);
}

function cb_GetModuleMethod(method)
{
    const noop = () => { }; // Empty Fallback To Avoid Runtime Errors

    if (WebFormsOptions.DisableCallModuleMethod)
    {
        if (WebFormsOptions.AddLogForModule)
            console.warn(""Access to the call module method is disabled but is being attempted.\nMethod: "" + method);
        return noop;
    }

    if (WebFormsOptions.UseCallModuleMethodOnlyInAcceptedList)
        if (!WebFormsOptions.CallModuleMethodOnlyInAcceptedList.some(p => cb_MatchesPattern(p, method)))
        {
            if (WebFormsOptions.AddLogForModule)
                console.warn(""Access to call module method is only possible in the list, but is being attempted.\nMethod: "" + method);
            return noop;
        }

    const fn = cb_ModuleMethodMap[method];
    if (typeof fn === ""function"")
        return fn;
    else
    {
        if (WebFormsOptions.AddLogForModule)
            console.warn(`Method ""${method}"" not found or not loaded yet.`);
        return noop;
    }
}
window.cb_GetModuleMethod = cb_GetModuleMethod;

// Remove A Specific Method
function cb_DeleteModuleMethod(method)
{
    if (cb_ModuleMethodMap[method])
    {
        delete cb_ModuleMethodMap[method];

        if (WebFormsOptions.AddLogForModule)
            console.log(`Method ""${method}"" removed.`);
    }
    else
        if (WebFormsOptions.AddLogForModule)
            console.warn(`Method ""${method}"" not found for removal.`);
}

/* End Module */

/* Start Format */

// HTML
function cb_FetchInputPlace(html, inputPlace)
{
    if (!inputPlace)
        return html;

    let htmlDataFragment = document.createRange().createContextualFragment(html);

    let result = cb_GetElementByElementPlace(inputPlace, htmlDataFragment);
    htmlDataFragment = null;
    return result.outerHTML;
}

// Text
function cb_GetTextLine(text, line)
{
    const lines = text.split(""\n"");
    line = parseInt(line, 10);

    if (line < 0)
    {
        line = lines.length + line;
        if (line < 0)
            return null;
    }

    return lines[line] !== undefined ? lines[line] : null;
}

function cb_SetTextLine(text, line, newValue)
{
    const lines = text.split(""\n"");
    line = parseInt(line, 10);

    if (line < 0)
        line = lines.length + line;

    while (line >= lines.length)
        lines.push("""");

    lines[line] = newValue;
    return lines.join(""\n"");
}

function cb_AppendTextLine(text, line, add)
{
    const lines = text.split(""\n"");
    line = parseInt(line, 10);

    if (line < 0)
    {
        line = lines.length + line;
        if (line < 0 || line >= lines.length)
            line = lines.length - 1;
    }

    while (line >= lines.length)
        lines.push("""");

    lines[line] += add;
    return lines.join(""\n"");
}

function cb_DeleteTextLine(text, line, remove)
{
    const lines = text.split(""\n"");
    line = parseInt(line, 10);

    if (line < 0)
        line = lines.length + line;

    if (line >= 0 && line < lines.length)
    {
        if (remove === undefined || remove === null)
            lines.splice(line, 1);
        else
            lines[line] = lines[line].replace(remove, """");
    }

    return lines.join(""\n"");
}

// INI
function cb_GetINI(text, path, isINILike = false)
{
    if (isINILike || !path.includes('.'))
    {
        const name = path.trim();
        const lines = text.split(""\n"");

        for (let raw of lines)
        {
            let line = raw.trim();
            if (!line)
                continue;

            const idx = line.indexOf('=');
            if (idx === -1)
                continue;

            const k = line.substring(0, idx).trim();
            const v = line.substring(idx + 1).trim();

            if (k === name)
                return v;
        }
        return null;
    }

    const obj = cb_ParseINI(text);
    const parts = path.split('.');
    let value = obj;

    for (let part of parts)
    {
        if (!value)
            return null;
        value = value[part];
    }

    return value ?? null;
}

function cb_ParseINI(text)
{
    const result = {};
    let currentSection = null;

    const lines = text.split(/\r?\n/);

    for (let raw of lines)
    {
        let line = raw.trim();
        if (!line || line.startsWith(';') || line.startsWith('#'))
            continue;

        const sectionMatch = line.match(/^\[(.+?)\]$/);
        if (sectionMatch)
        {
            currentSection = sectionMatch[1].trim();
            if (!result[currentSection])
                result[currentSection] = {};
            continue;
        }

        const idx = line.indexOf('=');
        if (idx !== -1)
        {
            const key = line.substring(0, idx).trim();
            const value = line.substring(idx + 1).trim();
            if (currentSection)
                result[currentSection][key] = value;
            else
                result[key] = value;
        }
    }

    return result;
}

// Add If Not Exist And Update If Exist. addOnly: Add Even Exist Key. updateOnly: Only Update If Exist
function cb_SetINI(text, path, value, isINILike = false, addOnly = false, updateOnly = false)
{
    const lines = text.split('\n');

    if (isINILike || !path.includes('.'))
    {
        const name = path.trim();
        let found = false;

        for (let i = 0; i < lines.length; i++)
        {
            const raw = lines[i].trim();
            const idx = raw.indexOf('=');
            if (idx === -1)
                continue;

            const k = raw.substring(0, idx).trim();
            if (k === name)
            {
                found = true;
                if (!addOnly)
                    lines[i] = `${name}=${value}`;
                break;
            }
        }

        if (!found && !updateOnly)
            lines.push(`${name}=${value}`);
        return lines.join('\n');
    }

    const [sec, key] = path.split('.');
    let foundSection = false;
    let keyExists = false;

    for (let i = 0; i < lines.length; i++)
    {
        const line = lines[i].trim();

        if (line === `[${sec}]`)
        {
            foundSection = true;

            for (let j = i + 1; j < lines.length; j++)
            {
                const ln = lines[j].trim();
                if (ln.startsWith('[') && ln.endsWith(']'))
                    break;

                const idx = ln.indexOf('=');
                if (idx === -1)
                    continue;

                const k = ln.substring(0, idx).trim();
                if (k === key)
                {
                    keyExists = true;
                    if (!addOnly)
                        lines[j] = `${key}=${value}`;
                    break;
                }
            }

            if (!keyExists && !updateOnly)
                lines.splice(i + 1, 0, `${key}=${value}`);
            return lines.join('\n');
        }
    }

    if (!foundSection && !updateOnly)
    {
        lines.push("""");
        lines.push(`[${sec}]`);
        lines.push(`${key}=${value}`);
    }

    return lines.join('\n');
}

function cb_AddINI(text, path, value, isINILike = false)
{
    return cb_SetINI(text, path, value, isINILike, true);
}

function cb_UpdateINI(text, path, value, isINILike = false)
{
    return cb_SetINI(text, path, value, isINILike, false, true);
}

function cb_DeleteINI(text, path, isINILike = false)
{
    const lines = text.split('\n');

    if (isINILike || !path.includes('.'))
    {
        const name = path.trim();

        const filtered = lines.filter(raw =>
        {
            const idx = raw.indexOf('=');
            if (idx === -1)
                return true;

            const k = raw.substring(0, idx).trim();
            return k !== name;
        });

        return filtered.join('\n');
    }

    const [sec, key] = path.split('.');
    let inSection = false;

    const filtered = lines.filter(raw =>
    {
        const line = raw.trim();

        if (line === `[${sec}]`)
        {
            inSection = true;
            return true;
        }

        if (inSection)
        {
            if (line.startsWith('[') && line.endsWith(']'))
            {
                inSection = false;
                return true;
            }

            const idx = line.indexOf('=');
            if (idx !== -1)
            {
                const k = line.substring(0, idx).trim();
                if (k === key)
                    return false;
            }
        }

        return true;
    });

    return filtered.join('\n');
}

// XML
function cb_GetXML(text, path)
{
    try
    {
        const parser = new DOMParser();
        const xml = parser.parseFromString(text, ""text/xml"");

        const parserError = xml.getElementsByTagName(""parsererror"");
        if (parserError.length > 0)
            return null;

        // Name
        if (!path.includes('/') && !path.includes('[') && !path.includes('@'))
        {
            const el = xml.getElementsByTagName(path)[0];
            return el ? el.textContent.trim() : null;
        }

        // XPath
        const evaluator = new XPathEvaluator();
        const result = evaluator.evaluate(path, xml, null, XPathResult.ANY_TYPE, null);

        switch (result.resultType)
        {
            case XPathResult.STRING_TYPE:
                return result.stringValue.trim();

            case XPathResult.NUMBER_TYPE:
                return result.numberValue;

            case XPathResult.BOOLEAN_TYPE:
                return result.booleanValue;

            default:
                const node = result.iterateNext();
                if (!node)
                    return null;

                if (node.nodeType === Node.ATTRIBUTE_NODE)
                    return node.value;

                return node.textContent.trim();
        }
    }
    catch (er)
    {
        if (WebFormsOptions.AddLog)
            console.error(""XML Get error: "", er);
        return null;
    }
}

function cb_SetXML(text, path, value)
{
    try
    {
        const xml = new DOMParser().parseFromString(text, ""text/xml"");

        const parserError = xml.getElementsByTagName(""parsererror"");
        if (parserError.length > 0)
            return text;

        // Name
        if (!path.includes('/') && !path.includes('[') && !path.includes('@'))
        {
            const el = xml.getElementsByTagName(path)[0];
            if (el)
                el.textContent = value;
            return new XMLSerializer().serializeToString(xml);
        }

        // XPath
        const evaluator = new XPathEvaluator();
        const nodes = evaluator.evaluate(
            path,
            xml,
            null,
            XPathResult.ORDERED_NODE_SNAPSHOT_TYPE,
            null
        );

        for (let i = 0; i < nodes.snapshotLength; i++)
        {
            const node = nodes.snapshotItem(i);

            if (node.nodeType === Node.ATTRIBUTE_NODE)
                node.value = value;
            else
                node.textContent = value;
        }

        return new XMLSerializer().serializeToString(xml);
    }
    catch (er)
    {
        if (WebFormsOptions.AddLog)
            console.error(""XML Set error: "", er);
        return text;
    }
}

function cb_DeleteXML(text, path)
{
    try
    {
        const xml = new DOMParser().parseFromString(text, ""text/xml"");

        const parserError = xml.getElementsByTagName(""parsererror"");
        if (parserError.length > 0)
            return text;

        const evaluator = new XPathEvaluator();
        const nodes = evaluator.evaluate(path, xml, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);

        for (let i = nodes.snapshotLength - 1; i >= 0; i--)
        {
            const node = nodes.snapshotItem(i);

            if (!node)
                continue;

            if (node.nodeType === Node.ATTRIBUTE_NODE)
                node.ownerElement.removeAttributeNode(node);
            else if (node.parentNode)
                node.parentNode.removeChild(node);
        }

        return new XMLSerializer().serializeToString(xml);
    }
    catch (er)
    {
        if (WebFormsOptions.AddLog)
            console.error(""XML Delete error: "", er);
        return text;
    }
}

function cb_AddXML(text, path, name, value = """")
{
    try
    {
        // Parse XML
        const xml = new DOMParser().parseFromString(text, ""text/xml"");

        // Check Parse Errors
        const parserError = xml.getElementsByTagName(""parsererror"");
        if (parserError.length > 0)
            return text;

        // Evaluate XPath
        const evaluator = new XPathEvaluator();
        const nodes = evaluator.evaluate(
            path,
            xml,
            null,
            XPathResult.ORDERED_NODE_SNAPSHOT_TYPE,
            null
        );

        // Determine If Adding Attribute
        const isAttribute = typeof name === ""string"" && name.startsWith('@');
        const attrName = isAttribute ? name.substring(1) : name;

        // Iterate Over Matched Nodes
        for (let i = 0; i < nodes.snapshotLength; i++)
        {
            const parent = nodes.snapshotItem(i);
            if (!parent)
                continue;

            if (isAttribute)
            {
                // Add Attribute
                parent.setAttribute(attrName, value);
            }
            else if (typeof attrName === ""string"" && attrName.length > 0)
            {
                // Add Element
                const newNode = xml.createElement(attrName);
                if (value !== null && value !== undefined)
                    newNode.textContent = value;
                parent.appendChild(newNode);
            }
        }

        return new XMLSerializer().serializeToString(xml);
    }
    catch (er)
    {
        if (WebFormsOptions.AddLog)
            console.error(""XML Add error: "", er);
        return text;
    }
}

// JSON
function cb_GetJSON(text, path)
{
    try
    {
        const obj = JSON.parse(text);
        if (!path)
            return obj;

        const keys = cb_JSONParsePath(path);
        let val = obj;

        for (let k of keys)
        {
            if (Array.isArray(val) && !isNaN(k))
                val = val[parseInt(k)];
            else if (val && typeof val === ""object"" && k in val)
                val = val[k];
            else
                return null;
        }

        return (val !== null && val !== undefined) ? val.toString() : null;
    }
    catch
    {
        return null;
    }
}

function cb_SetJSON(text, path, value)
{
    let obj = JSON.parse(text);
    const keys = cb_JSONParsePath(path);
    let target = obj;

    for (let i = 0; i < keys.length - 1; i++)
    {
        let k = keys[i];
        let nextK = keys[i + 1];

        if (Array.isArray(target) && !isNaN(k))
        {
            k = parseInt(k);
            if (target[k] === undefined)
                target[k] = isNaN(nextK) ? {} : [];
        }
        else if (!(k in target))
            target[k] = isNaN(nextK) ? {} : [];

        target = target[k];
    }

    let finalKey = keys[keys.length - 1];
    if (Array.isArray(target) && !isNaN(finalKey))
        finalKey = parseInt(finalKey);

    target[finalKey] = value;

    return JSON.stringify(obj);
}

function cb_DeleteJSON(text, path)
{
    let obj = JSON.parse(text);
    const keys = cb_JSONParsePath(path);
    let target = obj;

    for (let i = 0; i < keys.length - 1; i++)
    {
        let k = keys[i];

        if (Array.isArray(target) && !isNaN(k))
            k = parseInt(k);

        if (!(k in target))
            return text;

        target = target[k];
    }

    let finalKey = keys[keys.length - 1];

    if (Array.isArray(target) && !isNaN(finalKey))
    {
        finalKey = parseInt(finalKey);
        if (finalKey >= 0 && finalKey < target.length)
            target.splice(finalKey, 1);
    }
    else
        delete target[finalKey];

    return JSON.stringify(obj);
}

function cb_AddJSON(text, path, value)
{
    let obj = JSON.parse(text);
    if (!path)
        throw ""Path cannot be empty"";

    const keys = cb_JSONParsePath(path);
    let target = obj;

    for (let i = 0; i < keys.length - 1; i++)
    {
        let k = keys[i];
        let nextK = keys[i + 1];

        if (Array.isArray(target) && !isNaN(k))
        {
            k = parseInt(k);
            if (!target[k] || typeof target[k] !== ""object"")
                target[k] = isNaN(nextK) ? {} : [];
        }
        else if (!(k in target) || typeof target[k] !== ""object"")
            target[k] = isNaN(nextK) ? {} : [];

        target = target[k];
    }

    let finalKey = keys[keys.length - 1];
    if (Array.isArray(target) && !isNaN(finalKey))
        finalKey = parseInt(finalKey);

    if (!(finalKey in target))
        target[finalKey] = value;
    else
    {
        const cur = target[finalKey];

        if (Array.isArray(cur))
            cur.push(value);
        else if (cur !== null && typeof cur === ""object"")
        {
            const newKey = ""item"" + (Object.keys(cur).length + 1);
            cur[newKey] = value;
        }
        else
            target[finalKey] = [cur, value];
    }

    return JSON.stringify(obj);
}

/* End Format */

/* Start Format Storage */

const cb_StorageMemory = {};
let cb_StorageDB = null;

function cb_StorageInitDB()
{
    return new Promise((resolve, reject) =>
    {
        const request = indexedDB.open(""CB_DB"", 1);

        request.onupgradeneeded = () =>
        {
            const db = request.result;
            if (!db.objectStoreNames.contains(""cbStorage""))
                db.createObjectStore(""cbStorage"");
        };

        request.onsuccess = () =>
        {
            cb_StorageDB = request.result;
            resolve(cb_StorageDB);
        };

        request.onerror = () => reject(request.error);
    });
}

// Load IndexedDB To Memory (Run Once)
async function cb_StorageLoadToMemory()
{
    const db = await cb_StorageInitDB();

    return new Promise((resolve, reject) =>
    {
        const tx = db.transaction(""cbStorage"", ""readonly"");
        const store = tx.objectStore(""cbStorage"");
        const req = store.getAllKeys();

        req.onsuccess = () =>
        {
            const keys = req.result;

            if (keys.length === 0)
                return resolve();

            const tx2 = db.transaction(""cbStorage"", ""readonly"");
            const store2 = tx2.objectStore(""cbStorage"");
            let pending = keys.length;

            keys.forEach(key =>
            {
                const r = store2.get(key);
                r.onsuccess = () =>
                {
                    cb_StorageMemory[key] = r.result;
                    if (--pending === 0)
                        resolve();
                };
                r.onerror = () => reject(r.error);
            });
        };

        req.onerror = () => reject(req.error);
    });
}

function cb_StorageGet(key)
{
    return cb_StorageMemory[key] ?? null;
}

function cb_StorageSet(key, value)
{
    cb_StorageMemory[key] = value;

    if (cb_StorageDB)
    {
        const tx = cb_StorageDB.transaction(""cbStorage"", ""readwrite"");
        tx.objectStore(""cbStorage"").put(value, key);
    }

    return true;
}

function cb_StorageDelete(key)
{
    delete cb_StorageMemory[key];

    if (cb_StorageDB)
    {
        const tx = cb_StorageDB.transaction(""cbStorage"", ""readwrite"");
        tx.objectStore(""cbStorage"").delete(key);
    }
    
    return true;
}

function cb_StorageHas(key)
{
    return key in cb_StorageMemory;
}

function cb_StorageKeys()
{
    return Object.keys(cb_StorageMemory);
}

cb_StorageLoadToMemory();

/* End Format Storage */

/* Start Hardware */

// Loacation
function cb_GetGeoPosition()
{
    return new Promise((resolve, reject) =>
    {
        navigator.geolocation.getCurrentPosition(
            (pos) => resolve(pos.coords),
            (err) => reject(err)
        );
    });
}

/* End Hardware */

/* Start Extension */

// In this Section You Can Extend the WebForms Core Technology and Modify the Following Examples. Please Note that Only Use Numbers for Actions, Because Using String Abbreviations for Actions is a Risk due to Possible Conflicts.

async function cb_SetWebFormsValuesExtension(evt, ActionOperation, ActionFeature, Value, LastElementPlaceList, TransientDOM)
{
    switch (ActionOperation)
    {
        case '0':
            switch (ActionFeature)
            {
                case '0': alert(""Hello "" + Value); return true;

                default:
                    if (WebFormsOptions.AddLog)
                        console.warn(""This action in webforms value is incomprehensible: "" + ActionOperation + ActionFeature + ""\nError in value: "" + Value);

                    if (WebFormsOptions.AddMessageForIncomprehensibleSetWebFormsValue)
                        cb_ShowMessage(WebFormsOptions.SetWebFormsValueIsIncomprehensibleLang, ""problem"", WebFormsOptions.MessageDuration);
            }
    }
}

async function cb_SetValueToInputExtension(evt, ActionOperation, ActionFeature, CurrentElement, Value)
{
    switch (ActionOperation)
    {
        case '1':
            switch (ActionFeature)
            {
                case '0': console.log(CurrentElement.outerHTML + '|' + Value); break;

                default:
                    if (WebFormsOptions.AddLog)
                        console.warn(""This action in set value to input is incomprehensible: "" + ActionOperation + ActionFeature + ""\nError in value: "" + Value);

                    if (WebFormsOptions.AddMessageForIncomprehensibleSetValueToInput)
                        cb_ShowMessage(WebFormsOptions.SetValueToInputIsIncomprehensibleLang, ""problem"", WebFormsOptions.MessageDuration);
            }
    }
}

async function cb_FetchValueExtension(evt, ActionOperation, ActionFeature, Value)
{
    switch (ActionOperation)
    {
        case '2':
            switch (ActionFeature)
            {
                case '0': return ""Hello "" + Value;

                default:
                    if (WebFormsOptions.AddLog)
                        console.warn(""This action in fetch value is incomprehensible: "" + ActionOperation + ActionFeature + ""\nError in value: "" + Value);

                    if (WebFormsOptions.AddMessageForIncomprehensibleFetchValue)
                        cb_ShowMessage(WebFormsOptions.FetchValueIsIncomprehensibleLang, ""problem"", WebFormsOptions.MessageDuration);
            }
    }
}

async function cb_SaveValueExtension(evt, ActionOperation, ActionFeature, Name, CurrentElement)
{
    switch (ActionOperation)
    {
        case '3':
            switch (ActionFeature)
            {
                case '0': cb_SetStorage(true, Name, ""Hello saved in local storage""); break;
                case '1': cb_SetStorage(false, Name, ""Hello saved in session storage""); break;

                default:
                    if (WebFormsOptions.AddLog)
                        console.warn(""This action in save value is incomprehensible: "" + ActionOperation + ActionFeature + ""\nError in name: "" + Name);

                    if (WebFormsOptions.AddMessageForIncomprehensibleSaveValue)
                        cb_ShowMessage(WebFormsOptions.SaveValueIsIncomprehensibleLang, ""problem"", WebFormsOptions.MessageDuration);
            }
    }
}

async function cb_CheckConditionExtension(evt, Action, Control)
{
    switch (Action)
    {
        case ""40"": return (Control == ""Hello"");

        default:
            if (WebFormsOptions.AddLog)
                console.warn(""This action in check condition is incomprehensible: "" + Action + ""\nError in control: "" + Control);

            if (WebFormsOptions.AddMessageForIncomprehensibleCheckCondition)
                cb_ShowMessage(WebFormsOptions.CheckConditionIsIncomprehensibleLang, ""problem"", WebFormsOptions.MessageDuration);
    }
}

/* End Extension */");

            file.Dispose();
            file.Close();
        }
    }
}
