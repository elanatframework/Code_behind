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

            file.Write(
"""
/* WebFormsJS 2.1 - The Front-End Part of WebForms Core Technology, Owned by Elanat (https://elanat.net) */

/* Start Options */

const WebFormsOptions = new Object();

// Initialization
WebFormsOptions.AutoSetSubmitOnClick = true;
WebFormsOptions.AutoSetFormCustomValidity = true;

// Service Worker
WebFormsOptions.RegisterServiceWorker = false;
WebFormsOptions.RegisterServicePath = "/service-worker.js";
WebFormsOptions.RegisterServiceScopePath = '/';
WebFormsOptions.ReloadServiceWorkerIfNeed = true;
WebFormsOptions.UseServiceWorkerPush = false;
WebFormsOptions.UseServiceWorkerPushSubscribe = "/subscribe";
WebFormsOptions.ServiceWorkerPushVapidPublicKey = "BOr9UhjogkDpIVlYweq0mSx0Gcnt8Y6XmvfPWeryfdaWebFormsCorekf1q1qgW93z7pX_AbeD23CE3vZhAkZTY";
WebFormsOptions.ServiceWorkerWaitForControl = 100;

// Send
WebFormsOptions.SendDataOnlyByPostMethod = false;
WebFormsOptions.CheckValidityForFormSubmit = true;

// Response
WebFormsOptions.SetResponseInsideDivTag = true;
WebFormsOptions.ResponseLocation = "<body>";
WebFormsOptions.CreateCommentForWebFormsResponse = false;

// Non-Response Management
WebFormsOptions.IgnoreEmptyResult = false;
WebFormsOptions.UseRetryRequest = false;
WebFormsOptions.MaxRetryCount = 3;
WebFormsOptions.RetryRequestInterval = 3000;

// State
WebFormsOptions.StateBodyLocation = "<body>";
WebFormsOptions.UseSPALink = true;
WebFormsOptions.SPASaveStateDelay = 500;
WebFormsOptions.SetTitleBySPALink = true;
WebFormsOptions.IgnoreQueryAndHashInSPALink = false;
WebFormsOptions.ReloadOnMissingHistory = true;
WebFormsOptions.CloseAllFixedFeaturesAfterHistoryReview = true;
WebFormsOptions.RestoreListenersAfterHistoryReview = true;

// Queue
WebFormsOptions.UseQueue = true;
WebFormsOptions.UseQueueForWebFormsValue = true;
WebFormsOptions.UseDebounceDelay = true;
WebFormsOptions.QueueDebounceDelay = 200;

// Message
WebFormsOptions.MessageDuration = 3000;
WebFormsOptions.UseConnectionErrorMessage = true;
WebFormsOptions.AddMessageForProblemInDeterminingElement = false;
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
WebFormsOptions.AddConsoleMessage = true;
WebFormsOptions.UseConsoleStackTrace = false;
WebFormsOptions.AddConsoleMessageForHTTP = false;
WebFormsOptions.AddConsoleMessageForURL = true;
WebFormsOptions.AddConsoleMessageForWebSockets = true;
WebFormsOptions.AddConsoleMessageForSSE = true;
WebFormsOptions.AddConsoleMessageForModule = true;

// Animation
WebFormsOptions.UseProgressBar = true;
WebFormsOptions.UseLoader = true;
WebFormsOptions.UseLoaderForFirstPageLoad = true;
WebFormsOptions.LoaderMinimumDuration = 500;
WebFormsOptions.HideLoaderTimeout = 5000;
WebFormsOptions.HideLoaderWhenUpload = true;
WebFormsOptions.HideLoaderAfterUploaded = 1024 * 1024;

// Compress
WebFormsOptions.UseGzipFileSend = false;
WebFormsOptions.UseGzipFileSendIgnoreList = ["zip", "gzip", "rar"];
WebFormsOptions.UseGzipDataSend = false;
WebFormsOptions.UseGzipDataSendLargerThan = 5 * 1024;

// Async/Await
WebFormsOptions.AwaitConditionInterval = 100;

// Connection
WebFormsOptions.WebSocketReconnectMaxRetries = 5;
WebFormsOptions.SSEReconnectMaxRetries = 5;

// Security
WebFormsOptions.DisableEval = false;
WebFormsOptions.DisablePassObject = false;
WebFormsOptions.DisableAppendJavaScriptTag = false;
WebFormsOptions.DisableLoadModule = false;
WebFormsOptions.UseLoadModulePathOnlyInAcceptedList = false;
WebFormsOptions.LoadModulePathOnlyInAcceptedList = ["math"];
WebFormsOptions.DisableCallMethod = false;
WebFormsOptions.UseCallMethodOnlyInAcceptedList = false;
WebFormsOptions.CallMethodOnlyInAcceptedList = ["alert"];
WebFormsOptions.DisableCallModuleMethod = false;
WebFormsOptions.UseCallModuleMethodOnlyInAcceptedList = false;
WebFormsOptions.CallModuleMethodOnlyInAcceptedList = ["confirm"];
WebFormsOptions.DisableLoadExternalHost = false;
WebFormsOptions.UseLoadExternalHostOnlyInAcceptedList = false;
WebFormsOptions.LoadExternalHostOnlyInAcceptedList = ["example.com"];
WebFormsOptions.SendChecksum = false;
WebFormsOptions.ChecksumName = "checksum";

// Language
WebFormsOptions.SPAGlobalTitle = "My WebSite - {{title}}";
WebFormsOptions.ConnectionErrorMessage = "Connection Error";
WebFormsOptions.CheckConditionIsIncomprehensibleLang = "Check condition is incomprehensible";
WebFormsOptions.SaveValueIsIncomprehensibleLang = "Save value is incomprehensible";
WebFormsOptions.FetchValueIsIncomprehensibleLang = "Fetch value is incomprehensible";
WebFormsOptions.SetValueToInputIsIncomprehensibleLang = "Set value to input is incomprehensible";
WebFormsOptions.SetWebFormsValueIsIncomprehensibleLang = "Set webforms value is incomprehensible";
WebFormsOptions.ConnectionErrorLang = "Connection error";
WebFormsOptions.ProblemInCheckConditionLang = "Problem in check condition";
WebFormsOptions.ProblemInSaveValueLang = "Problem in save value";
WebFormsOptions.ProblemInFetchValueLang = "Problem in fetch value";
WebFormsOptions.ProblemInDeterminingElementLang = "Problem in determining element";
WebFormsOptions.ProblemInSetValueToInputLang = "Problem in set value to input";
WebFormsOptions.ProblemInSetWebFormsValueLang = "Problem in set webforms value";
WebFormsOptions.SSEClosingAllLang = "SSE Closing all";
WebFormsOptions.SSEManuallyCloseLang = "SSE Manually close";
WebFormsOptions.SSEReconnectingLang = "SSE Reconnecting";
WebFormsOptions.SSEDisconnectedLang = "SSE Disconnected";
WebFormsOptions.SSEConnectedLang = "SSE Connected";
WebFormsOptions.SSETryingToConnectLang = "SSE Trying to connect";
WebFormsOptions.InitializingNewWebSocketLang = "Initializing new WebSocket";
WebFormsOptions.WebSocketErrorLang = "WebSocket error";
WebFormsOptions.WebSocketDisconnectedLang = "WebSocket disconnected";
WebFormsOptions.WebSocketConnectedLang = "WebSocket connected";
WebFormsOptions.ValidityValueMissing = "Please fill out this field";
WebFormsOptions.ValidityTypeMismatch = "Please enter a valid value";
WebFormsOptions.ValidityPatternMismatch = "Please enter a value in the correct format";
WebFormsOptions.ValidityTooLong = "Please use no more than {{maxLength}} characters (you are currently using {{valueLength}} characters).";
WebFormsOptions.ValidityTooShort = "Please use at least {{minLength}} characters (you are currently using {{valueLength}} characters).";
WebFormsOptions.ValidityRangeUnderflow = "Value must be greater than or equal to {{min}}.";
WebFormsOptions.ValidityRangeOverflow = "Value must be less than or equal to {{max}}.";
WebFormsOptions.ValidityStepMismatch = "The value entered is not a valid step";
WebFormsOptions.ValidityBadInput = "Please enter a valid value";
WebFormsOptions.ValidityCustomError = "The value entered is invalid";

// Style
WebFormsOptions.WebFormsTagsBackgroundColor = "#eee";
WebFormsOptions.ProgressBarStyle = "width:100%;min-width:300px;max-width:600px;background-color:#eee;margin:2px 0px";
WebFormsOptions.ProgressBarPercentLoadedStyle = "position:absolute;padding:0px 4px;line-height:22px";
WebFormsOptions.ProgressBarValueStyle = "height:20px;background-color:#4D93DD;width:0%";
WebFormsOptions.MessageNoneStyle = "background-color: #464646";
WebFormsOptions.MessageWarningStyle = "background-color: #AF4C4C";
WebFormsOptions.MessageProblemStyle = "background-color: #AFA04C";
WebFormsOptions.MessageHelpStyle = "background-color: #4C81AF";
WebFormsOptions.MessageSuccessStyle = "background-color: #4CAF8F";

const WebFormsDefaultOptions = Object.assign({}, WebFormsOptions);

/* End Options */

/* Start Global Constant */

const FS = String.fromCharCode(28); // File Separator
const GS = String.fromCharCode(29); // Group Separator
const RS = String.fromCharCode(30); // Record Separator
const US = String.fromCharCode(31); // Unit Separator

/* End Global Constant */

/* Start Check Browser Support */

// Check If WebFormsJS Is Not Load Module Mode
if (document.currentScript)
    console.error("The WebFormsJS library must be loaded with <script type=\"module\">.");

const cb_UnsupportedFeatures = [];

// Feature List
const cb_BrowseFeatures =
[
    // DOM / Form
    { name: "FormData", check: () => typeof FormData !== "undefined" },
    { name: "replaceChildren", check: () => "replaceChildren" in document.createElement("div") },

    // ES2020 / JavaScript
    { name: "ES2020 (BigInt / optional chaining / nullish coalescing)", check: () =>
    {
        try
        {
            eval("123n; null?.x; 1 ?? 2;");
            return true;
        }
        catch
        {
            return false;
        }
    } },

    // Web APIs
    { name: "fetch", check: () => typeof fetch !== "undefined" },
    { name: "WebSocket", check: () => typeof WebSocket !== "undefined" },
    { name: "ServiceWorker", check: () => "serviceWorker" in navigator },
    { name: "WebRTC (RTCPeerConnection)", check: () => typeof RTCPeerConnection !== "undefined" },
    { name: "Web Animations API", check: () => "animate" in document.createElement("div") },
    { name: "CompressionStream", check: () => "CompressionStream" in window },
    { name: "Blob.prototype.stream", check: () => "stream" in Blob.prototype },
    { name: "Server-Sent Events (EventSource)", check: () => typeof EventSource !== "undefined" },

    // Observers / UI
    { name: "IntersectionObserver", check: () => "IntersectionObserver" in window },
    { name: "ResizeObserver", check: () => "ResizeObserver" in window },
    { name: "MutationObserver", check: () => "MutationObserver" in window },

    // Media / Clipboard
    { name: "Clipboard API", check: () => "clipboard" in navigator },
    { name: "MediaDevices API", check: () => "mediaDevices" in navigator },

    // Storage
    { name: "localStorage", check: () => { try { return "localStorage" in window && window.localStorage !== null; } catch { return false; } } },
    { name: "IndexedDB", check: () => "indexedDB" in window },

    // Intl / Localization
    { name: "Intl API", check: () => typeof Intl !== "undefined" }
];

// Check Each Feature
cb_BrowseFeatures.forEach(f =>
{
    if (!f.check())
        cb_UnsupportedFeatures.push(f.name);
});

// Report
const cb_WebFormsCoreUsingMessage = "You are using a web application built with WebForms Core technology.";

if (cb_UnsupportedFeatures.length > 0)
    console.warn(cb_WebFormsCoreUsingMessage + "\nYour browser is outdated or unsuitable and may experience performance issues because it does not support the following features:", cb_UnsupportedFeatures.join(", "));
else
    if (WebFormsOptions.AddConsoleMessage)
        console.log(cb_WebFormsCoreUsingMessage + "\nCongratulations! All core browser features are supported.");

/* End Check Browser Support */

/* Start WebSocket */

const cb_UseWebSocketPath = [];
let cb_UseWebSocket = false;
const cb_WebSockets = {};
const cb_WebSocketRetryCount = {};

function cb_AddWebSocketPath(path)
{
    if (!path)
        path = window.location.pathname;

    if (cb_UseWebSocketPath.indexOf(path) === -1)
        cb_UseWebSocketPath.push(path);
}

function cb_WebSocketInitialization(Url, formAction)
{
    const ws = new WebSocket(Url);

    ws.onclose = function (evt)
    {
        cb_WebSocketOnClose(evt, formAction);

        cb_WebSocketReconnect(Url, formAction);
    };
    ws.onerror = function (evt)
    {
        cb_WebSocketOnError(evt, formAction);
    };

    cb_WebSockets[formAction] = ws;
}

function cb_WebSocketReconnect(Url, formAction)
{

    if (!cb_WebSocketRetryCount[formAction])
        cb_WebSocketRetryCount[formAction] = WebFormsOptions.WebSocketReconnectMaxRetries;

    const attempt = cb_WebSocketRetryCount[formAction];

    if (attempt >= WebFormsOptions.WebSocketReconnectMaxRetries)
    {
        if (WebFormsOptions.AddConsoleMessageForWebSockets)
            console.warn("WebSocket max retry reached. Stop reconnecting.");
        return;
    }

    const delay = Math.min(1000 * Math.pow(2, attempt), 30000);

    cb_WebSocketRetryCount[formAction]++;
    
    setTimeout(() =>
    {
        cb_WebSocketInitialization(Url, formAction);
    }, delay);
}

function cb_WebSocketOnClose(evt, formAction)
{
    if (WebFormsOptions.AddConsoleMessageForWebSockets)
        console.log("WebSocket disconnected, path: " + formAction);

    if (WebFormsOptions.AddMessageForWebSocketClose)
        cb_ShowMessage(WebFormsOptions.WebSocketDisconnectedLang, "none", WebFormsOptions.MessageDuration);

    delete cb_WebSockets[formAction];
}

function cb_WebSocketDelete(formAction)
{
    if (!formAction)
        formAction = window.location.pathname;

    const ws = cb_WebSockets[formAction];

    if (ws)
    {
        if (ws.readyState === WebSocket.OPEN || ws.readyState === WebSocket.CONNECTING)
            ws.close(1000, "Client closed connection");

        delete cb_WebSockets[formAction];

        if (WebFormsOptions.AddConsoleMessageForWebSockets)
            console.log("WebSocket manually closed and removed: " + formAction);
    }
}

function cb_WebSocketIsConnected(path)
{
    if (!path)
        path = window.location.pathname;

    const ws = cb_WebSockets[path];

    if (ws)
        return (ws.readyState === WebSocket.OPEN || ws.readyState === WebSocket.CONNECTING);

    return false;
}

function cb_WebSocketOnError(evt, formAction)
{
    if (WebFormsOptions.AddConsoleMessageForWebSockets)
        console.log("WebSocket error, path: " + formAction);

    if (WebFormsOptions.AddMessageForWebSocketError)
        cb_ShowMessage(WebFormsOptions.WebSocketErrorLang, "problem", WebFormsOptions.MessageDuration);
}

function cb_WebSocketDoSend(Message)
{
    if (WebFormsOptions.AddConsoleMessageForWebSockets)
        console.log("WebSocket sent:\n" + Message);

    for (let formAction in cb_WebSockets)
        if (cb_WebSockets[formAction].readyState === WebSocket.OPEN)
            cb_WebSockets[formAction].send(Message);
}

function cb_WebSocketSet(formAction)
{
    if (!formAction)
        formAction = window.location.pathname;

    const Url = cb_ConvertToWebSocketUrl(formAction)

    if (WebFormsOptions.AddConsoleMessageForWebSockets)
        console.log("WebSocket request path: " + formAction);

    let active = false;
    if (cb_WebSockets[formAction] && (cb_WebSockets[formAction].readyState === WebSocket.OPEN || cb_WebSockets[formAction].readyState === WebSocket.CONNECTING))
        active = true;

    if (!active)
    {
        if (WebFormsOptions.AddConsoleMessageForWebSockets)
            console.log("No active WebSocket for this path, initializing new one...");

        if (WebFormsOptions.AddMessageForWebSocketInitializing)
            cb_ShowMessage(WebFormsOptions.InitializingNewWebSocketLang, "help", WebFormsOptions.MessageDuration);

        cb_WebSocketInitialization(Url, formAction);
    }
    else
    {
        if (WebFormsOptions.AddConsoleMessageForWebSockets)
            console.log("WebSocket already connected or connecting for this path");
    }
}

/* End WebSocket */

/* Start SSE */

const cb_SSEConnections = {};
const cb_SSERetryCount = {};

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

    if (WebFormsOptions.AddConsoleMessageForSSE)
        console.log(`SSE Trying to connect: ${path}`);

    if (WebFormsOptions.AddMessageForSSEInitializing)
        cb_ShowMessage(WebFormsOptions.SSETryingToConnectLang, "help", WebFormsOptions.MessageDuration);

    const source = new EventSource(path);

    source.onopen = () =>
    {
        if (WebFormsOptions.AddConsoleMessageForSSE)
            console.log(`SSE Connected: ${path}`);

        if (WebFormsOptions.AddMessageForSSEConnect)
            cb_ShowMessage(WebFormsOptions.SSEConnectedLang, "success", WebFormsOptions.MessageDuration);
    };

    source.onmessage = (event) =>
    {
        if (WebFormsOptions.AddConsoleMessageForSSE)
            console.log(`SSE Message from ${path}:`, event.data);

        const response = event.data.Replace("$[sln];" , '\n');

        cb_SetResponse(evt, response, viewState, "");
    };

    source.onerror = () =>
    {
        if (WebFormsOptions.AddConsoleMessageForSSE)
            console.warn(`SSE Disconnected: ${path}`);

        if (WebFormsOptions.AddMessageForSSEDisconnected)
            cb_ShowMessage(WebFormsOptions.SSEDisconnectedLang, "problem", WebFormsOptions.MessageDuration);

        source.close();
        delete cb_SSEConnections[path];

        if (!shouldReconnect)
            return;

        if (!cb_SSERetryCount[path])
            cb_SSERetryCount[path] = 0;

        if (cb_SSERetryCount[path] >= WebFormsOptions.SSEReconnectMaxRetries)
        {
            if (WebFormsOptions.AddConsoleMessageForSSE)
                console.log("SSE max retry reached");
            return;
        }

        cb_SSERetryCount[path]++;

        if (WebFormsOptions.AddConsoleMessageForSSE)
            console.log(`SSE Reconnecting to ${path} in ${reconnectTryTimeout}ms attempt ${cb_SSERetryCount[path]}...`);

        if (WebFormsOptions.AddMessageForSSEReconnecting)
            cb_ShowMessage(WebFormsOptions.SSEReconnectingLang + " ...", "none", WebFormsOptions.MessageDuration);

        setTimeout(() => cb_ConnectToSSE(evt, path, shouldReconnect, reconnectTryTimeout, viewState), reconnectTryTimeout);
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
        if (WebFormsOptions.AddConsoleMessageForSSE)
            console.log(`SSE Manually closed connection: ${path}`);

        if (WebFormsOptions.AddMessageForSSEClose)
            cb_ShowMessage(WebFormsOptions.SSEManuallyCloseLang, "none", WebFormsOptions.MessageDuration);
    }
}

function cb_SSEIsConnected(path)
{
    const source = cb_SSEConnections[path];

    if (source)
        return (source.readyState === EventSource.CONNECTING || source.readyState === EventSource.OPEN);

    return false;
}

function cb_DisconnectAllSSE()
{
    for (const path in cb_SSEConnections)
    {
        cb_SSEConnections[path].close();
        if (WebFormsOptions.AddConsoleMessageForSSE)
            console.log(`SSE Closed: ${path}`);
    }

    Object.keys(cb_SSEConnections).forEach(k => delete cb_SSEConnections[k]);

    if (WebFormsOptions.AddMessageForSSECloseAll)
        cb_ShowMessage(WebFormsOptions.SSEClosingAllLang, "none", WebFormsOptions.MessageDuration);
}

/* End SSE */

/* Start Event */

function cb_FakeEvent()
{
    return new Event("load", { bubbles: false, cancelable: false });
}

function cb_SetPostBackFunctionToSubmit(obj)
{
    if (!WebFormsOptions.AutoSetSubmitOnClick)
        return;

    const SubmitInputs = (obj) ? obj.querySelectorAll('input[type="submit"], button[type="submit"]') : document.querySelectorAll('input[type="submit"], button[type="submit"]');

    SubmitInputs.forEach(function (InputElement)
    {
        if (InputElement.hasAttribute("onclick"))
        {
            const OnClickAttr = InputElement.getAttribute("onclick");

            if (!OnClickAttr)
            {
                InputElement.setAttribute("onclick", "PostBack(event)");
                return;
            }

            if (!OnClickAttr.ContainsNameWithSplitter("PostBack", ';', '('))
                if (OnClickAttr.charAt(OnClickAttr.length - 1) == ';')
                    InputElement.setAttribute("onclick", OnClickAttr + "PostBack(event)");
                else
                    InputElement.setAttribute("onclick", OnClickAttr + ";PostBack(event)");
        }
        else
            InputElement.setAttribute("onclick", "PostBack(event)");
    });
}

function cb_RemovePostBackFunctionInSubmit(obj, evt)
{
    if (obj.tagName)
        if (obj.tagName.toLowerCase() == "input" || obj.tagName.toLowerCase() == "button")
            if (obj.hasAttribute("type"))
                if (obj.getAttribute("type").toLowerCase() == "submit")
                    if (evt.toLowerCase() == "onclick" || evt.toLowerCase() == "click")
                        if (obj.hasAttribute("onclick"))
                            if (obj.getAttribute("onclick") == "PostBack(event)")
                                obj.removeAttribute("onclick");
}

window.onload = function ()
{
    if (WebFormsOptions.UseLoaderForFirstPageLoad)
        cb_ShowLoader();

    cb_Initialization();

    if (WebFormsOptions.UseLoaderForFirstPageLoad)
        cb_HideLoader();
};

function cb_Initialization(obj)
{
    cb_SetWebFormsCommentsValue(obj);
    cb_SetPostBackFunctionToSubmit(obj);
    cb_SetFormCustomValidity(obj);
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
            let currentAttribute = obj.getAttribute(event);

            if (event == "onload")
            {
                var tmpObjOnload = obj.onload;
                obj.onload = new Function(functionWithArgs);
                obj.onload();
                obj.onload = tmpObjOnload;
                obj.setAttribute(event, functionWithArgs);

                if (!obj)
                    return;

                if (obj.getAttribute(event).length > functionWithArgs.length)
                    currentAttribute += ';' + obj.getAttribute(event).Replace(functionWithArgs, "");
            }

            obj.setAttribute(event, currentAttribute + ';' + functionWithArgs);
            return;
        }

    if (event == "onload")
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
    const currentEvent = obj.getAttribute(event);

    if (!currentEvent)
        return;

    const escaped = functionName.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");

    const regex = new RegExp(escaped + "\\([^)]*\\)(?:\\([^)]*\\))?;?", 'g');

    const updatedEvent = currentEvent.replace(regex, "").trim();

    obj.setAttribute(event, updatedEvent);
}

const cb_EventRegistry = {};

async function cb_AddEventListener(obj, event, currentFunction, args, functionType = "event")
{
    args = args ?? [];

    // Remove Auto PostBack
    cb_RemovePostBackFunctionInSubmit(obj, event);

    const callback = async function (evt)
    {
        args = await cb_SetDynamicValueForArgs(evt, args);
        switch (functionType)
        {
            case "event": currentFunction.apply(this, [evt, ...args]); break;
            case "method": currentFunction.apply(window, [...args]); break;
        }
    };

    if (obj && event == "load")
    {
        const fakeEvent = cb_FakeEvent();
        args = await cb_SetDynamicValueForArgs(fakeEvent, args);

        switch (functionType)
        {
            case "event":
                currentFunction.apply(obj, [fakeEvent, ...args]);
                break;
            case "method": currentFunction.apply(window, [...args]); break;
        }
    }

    obj.addEventListener(event, callback);

    // Generate A Unique ID If The Element Doesn't Have
    let objId;
    if (obj instanceof Element)
    {
        objId = obj.id;
        if (!objId)
        {
            objId = "cb_" + Math.random().toString(36).substring(2, 9);
            obj.id = objId;
            // Store As Data Attribute For Easier Lookup During DOM Replacement
            obj.setAttribute("cb-data-id", objId);
        }
    }
    else
        objId = "_cb_global_" + event;

    if (!cb_EventRegistry[objId])
        cb_EventRegistry[objId] = {};

    if (!cb_EventRegistry[objId][event])
        cb_EventRegistry[objId][event] = [];

    // Check If This Exact Listener Already Exists
    const existingListener = cb_EventRegistry[objId][event].find(
        entry => entry.currentFunction === currentFunction && JSON.stringify(entry.args) === JSON.stringify(args)
    );

    if (!existingListener)
        cb_EventRegistry[objId][event].push({ callback, currentFunction, args, functionType });
}

function cb_RemoveEventListener(obj, event, currentFunction)
{
    const objId = obj instanceof Element ? (obj.id || obj) : "_cb_global_" + event;
    const listeners = cb_EventRegistry[objId]?.[event];

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

function cb_RestoreListenersAfterDOMReplace(containerElement = document)
{
    Object.keys(cb_EventRegistry).forEach(objId =>
    {
        let newElement = null;
        
        if (objId.startsWith("cb_"))
            newElement = containerElement.querySelector(`[cb-data-id="${objId}"]`);
        else if (objId.startsWith("_cb_global_"))
            return;
        else
            newElement = containerElement.getElementById(objId);
        
        if (newElement)
        {
            const events = cb_EventRegistry[objId];
            
            Object.keys(events).forEach(eventType =>
            {
                events[eventType].forEach(listener =>
                {
                    const restoredCallback = async function(evt)
                    {
                        let args = listener.args || [];
                        
                        if (typeof cb_SetDynamicValueForArgs === "function")
                            args = await cb_SetDynamicValueForArgs(evt, args);
                        
                        if (listener.currentFunction)
                        {
                            switch (listener.functionType || "event")
                            {
                                case "event":
                                    listener.currentFunction.apply(this, [evt, ...args]);
                                    break;
                                case "method":
                                    listener.currentFunction.apply(window, [...args]);
                                    break;
                            }
                        }
                    };
                    
                    newElement.addEventListener(eventType, restoredCallback);
                    
                    listener.callback = restoredCallback;
                });
            });
        }
    });
}

function cb_PreServedEvent(evt)
{
    const captured = {};

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

        if (typeof evt.preventDefault === "function")
            captured.preventDefault = function (){evt.preventDefault();};

        if (typeof evt.stopPropagation === "function")
            captured.stopPropagation = function () {evt.stopPropagation();};

        if (typeof evt.getModifierState === "function")
            captured.getModifierState = function (keyArg) { return evt.getModifierState(keyArg); };


        // ... Has More Event Methods
    }

    return captured;
}

function PreventDefault(evt)
{
    evt.preventDefault();
}

function StopPropagation(evt)
{
    evt.stopPropagation();
}

function cb_SetSPALink(obj)
{
    if (!WebFormsOptions.UseSPALink)
        return;

    const links = (obj) ? obj.querySelectorAll('a') : document.body.querySelectorAll('a');

    links.forEach(link =>
    {
        const targetAttr = link.getAttribute("target");
        let hrefAttr = link.getAttribute("href");

        if (hrefAttr.length > 1)
            if (hrefAttr.substring(0, 2) == "#~")
                hrefAttr = hrefAttr.substring(2);

        if (hrefAttr && !hrefAttr.includes("://") && !hrefAttr.startsWith("mailto:") && !hrefAttr.startsWith("tel:") && (!targetAttr || targetAttr === "_self"))
            link.setAttribute("onclick", `PreventDefault(event);GetBack(event, '${hrefAttr}');`);
    });
}

function cb_TriggerEvent(element, constructorNameOrEvent, eventNameOrOptions, maybeOptions = {})
{
    let event, constructorName, eventName, options;

    if (typeof eventNameOrOptions === "string")
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
            constructorName = "keyboardevent";
        else if
            (/click|mouse/i.test(eventName)) constructorName = "mouseevent";
        else if
            (/input/i.test(eventName)) constructorName = "inputevent";
        else if
            (/focus|blur/i.test(eventName)) constructorName = "focusevent";
        else if
            (/scroll|resize/i.test(eventName)) constructorName = "uievent";
        else constructorName = "event";
    }

    // Create Event Based In Constructor
    switch (constructorName)
    {
        case "mouseevent": event = new MouseEvent(eventName, defaultOptions); break;
        case "keyboardevent":
            event = new KeyboardEvent(eventName,
            {
                key: options.key || 'a',
                code: options.code || "KeyA",
                ...defaultOptions
            });
            break;
        case "inputevent": event = new InputEvent(eventName, defaultOptions); break;
        case "focusevent": event = new FocusEvent(eventName, defaultOptions); break;
        case "uievent": event = new UIEvent(eventName, defaultOptions); break;
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
            if (typeof v !== "object" && typeof v !== "function")
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
        const event = new CustomEvent("scrollbottom");
        window.dispatchEvent(event);
    }
}

function cb_EnableScrollBottomEvent(enable = true)
{
    if (enable)
        window.addEventListener("scroll", cb_WindowBottomReached);
    else
        window.removeEventListener("scroll", cb_WindowBottomReached);
}

// After Element Reached
function cb_ElementReachedHandler(currentElement, once)
{
    return function handler()
    {
        if (cb_ElementReachedCheck(currentElement) && once)
            window.removeEventListener("scroll", handler);
    };
}

function cb_ElementReachedCheck(currentElement)
{
    const rect = currentElement.getBoundingClientRect();
    const inView = rect.top < window.innerHeight && rect.bottom > 0;

    if (inView)
    {
        const event = new CustomEvent("elementreached");
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
        window.addEventListener("scroll", handler);
        handler(); // First Check
    }
    else
        window.removeEventListener("scroll", handler);
}

function cb_CreateCustomDOMEvent(element, eventName, watch = "attribute", key = "", compare = "equal", value, range = [0, 0], immediate = false, delay = 0)
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
        if (handlerName && typeof window[handlerName] === "function")
            window[handlerName]();
    };

    const safeParse = (v) =>
    {
        const n = parseFloat(v);
        return Number.isFinite(n) ? n : NaN;
    };

    const compareValues = (current) =>
    {
        if (watch === "children")
            current = (current?.length) || 0;

        if (compare === "changed")
        {
            const changed = current !== lastValue;
            lastValue = current;
            return changed;
        }
        if (compare === "greater")
        {
            const num = safeParse(current);
            if (isNaN(num))
                return false;
            return num > value;
        }
        if (compare === "less")
        {
            const num = safeParse(current);
            if (isNaN(num))
                return false;
            return num < value;
        }
        if (compare === "equal")
            return current === value;
        if (compare === "notequal")
            return current !== value;
        if (compare === "includes")
            return ("" + current).includes(value);
        if (compare === "startswith")
            return ("" + current).startsWith(value);
        if (compare === "endswith")
            return ("" + current).endsWith(value);
        if (compare === "matches")
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
        if (compare === "inrange")
        {
            const num = safeParse(current);
            if (isNaN(num))
                return false;
            return num >= range[0] && num <= range[1];
        }
        if (compare === "lengthgreater")
            return (current?.length) > value;
        if (compare === "lengthless")
            return (current?.length) < value;
        if (compare === "lengthequal")
            return (current?.length) === value;

        return false;
    };

    const getCurrent = () =>
    {
        if (watch === "value" && (element instanceof HTMLInputElement || element instanceof HTMLTextAreaElement || element instanceof HTMLSelectElement))
            return element.value;

        switch (watch)
        {
            case "attribute": return element.getAttribute(key);
            case "style": return getComputedStyle(element)[key];
            case "text": return element.textContent?.trim() || "";
            case "children": return element.children;
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

    if (watch === "value" && (element instanceof HTMLInputElement || element instanceof HTMLTextAreaElement || element instanceof HTMLSelectElement))
    {
        element.addEventListener("input", checkAndDispatch);
        element.addEventListener("change", checkAndDispatch);
    }
    else
    {
        observer = new MutationObserver(checkAndDispatch);
        observer.observe(element,
        {
            attributes: watch === "attribute" || watch === "style",
            attributeFilter: watch === "attribute" ? [key] : undefined,
            childList: watch === "children" || watch === "text",
            subtree: watch === "children" || watch === "text",
            characterData: watch === "text",
        });
    }

    if (immediate)
        checkAndDispatch();

    return {
        disconnect: () =>
        {
            if (observer)
                observer.disconnect();
            if (watch === "value")
            {
                element.removeEventListener("input", checkAndDispatch);
                element.removeEventListener("change", checkAndDispatch);
            }
        },
        pause: () => { paused = true; },
        resume: () => { paused = false; }
    };
}

/* End Custom Event */

/* Start Debugger */

let cb_DebugActive = false;
let cb_DebugPaused = false;
let cb_DebugStepPending = false;
let cb_DebugCurrentStep = 0;
let cb_DebugPanelCreated = false;
let cb_DebuggerListenerAdded = false;

function cb_DebugStart()
{
    cb_DebugActive = true;
    cb_DebugPaused = false;
    cb_DebugCurrentStep = 0;
    cb_UpdateDebugStatus();
}

function cb_DebugGo()
{
    if (!cb_DebugActive)
        cb_DebugStart();

    cb_DebugPaused = false;
    cb_UpdateDebugStatus();cb_CreateDebugger
}

function cb_DebugPause()
{
    if (!cb_DebugActive)
        return;

    cb_DebugPaused = true;
    cb_UpdateDebugStatus();
}

function cb_DebugStop()
{
    cb_DebugActive = false;
    cb_DebugPaused = false;
    cb_DebugCurrentStep = 0;
    cb_UpdateDebugStatus();
}

function cb_DebugStepNext()
{
    if (!cb_DebugActive || !cb_DebugPaused)
        return;

    if (!cb_DebugStepPending)
            return;

    cb_DebugCurrentStep++;
    cb_UpdateDebugStep();

    cb_DebugPaused = false;
}

function cb_DebugClose()
{
    cb_DebugActive = false;
    cb_DebugPaused = false;
    cb_DebugCurrentStep = 0;

    cb_RemoveDebugger();
}

function cb_UpdateDebugStatus()
{
    const statusSpan = document.getElementById("cb-debug-status");

    if (statusSpan)
    {
        let status = "Inactive";
        if (cb_DebugActive && !cb_DebugPaused)
            status = "Running";
        else if (cb_DebugActive && cb_DebugPaused)
            status = "Paused";
        statusSpan.textContent = status;
    }
}

function cb_UpdateDebugStep()
{
    const stepSpan = document.getElementById("cb-debug-step");

    if (stepSpan)
        stepSpan.textContent = cb_DebugCurrentStep;
}

// Create Debugger Panel
function cb_CreateDebugger()
{
    if (cb_DebugPanelCreated)
        return;
    
    const panel = document.createElement("div");
    panel.id = "cb-debugger-panel";
    panel.className = "cb-debugger-panel";
    panel.innerHTML = `
        <div class="cb-debugger-header">
            WebForms Core Debugger
        </div>
        <div class="cb-debugger-content">
            <div class="cb-debugger-buttons">
                <button id="cb-btn-debug-go" class="cb-btn cb-btn-go">&gt; Go</button>
                <button id="cb-btn-debug-pause" class="cb-btn cb-btn-pause">|| Pause</button>
                <button id="cb-btn-debug-stop" class="cb-btn cb-btn-stop"># Stop</button>
                <button id="cb-btn-debug-step" class="cb-btn cb-btn-step">! Step</button>
                <button id="cb-btn-debug-close" class="cb-btn cb-btn-close">X Close</button>
            </div>
            <div class="cb-debugger-info">
                Status: <span id="cb-debug-status">Inactive</span> | Step: <span id="cb-debug-step">0</span>
            </div>
        </div>
    `;
    
    document.body.appendChild(panel);
    cb_DebugPanelCreated = true;
    
    // Inject CSS
    cb_InjectDebuggerStyles();

    // Event Listeners
    if (!cb_DebuggerListenerAdded)
    {
        document.addEventListener("click", cb_DebugClickHandler);
        cb_DebuggerListenerAdded = true;
    }
}

function cb_DebugClickHandler(e)
{
    if (e.target.matches("#cb-btn-debug-go"))
        cb_DebugGo();
    else if (e.target.matches("#cb-btn-debug-pause"))
        cb_DebugPause();
    else if (e.target.matches("#cb-btn-debug-stop"))
        cb_DebugStop();
    else if (e.target.matches("#cb-btn-debug-step"))
        cb_DebugStepNext();
    else if (e.target.matches("#cb-btn-debug-close"))
        cb_DebugClose();
}

// Remove Debugger Panel
function cb_RemoveDebugger()
{
    const panel = document.getElementById("cb-debugger-panel");
    if (panel)
        panel.remove();

    if (cb_DebuggerListenerAdded)
    {
        document.removeEventListener("click", cb_DebugClickHandler);
        cb_DebuggerListenerAdded = false;
    }
    
    cb_DebugPanelCreated = false;

    cb_DebugPanelCreated = false;
}

// Inject CSS Styles
function cb_InjectDebuggerStyles()
{
    const style = document.createElement("style");
    style.textContent = `
.cb-debugger-panel {
    position: fixed;
    bottom: 20px;
    right: 20px;
    background: #f5f5f5;
    border: 1px solid #ddd;
    border-radius: 8px;
    font-family: 'Segoe UI', monospace;
    font-size: 12px;
    z-index: 10000;
    box-shadow: 0 2px 10px rgba(0,0,0,0.1);
    min-width: 280px;
}

.cb-debugger-header {
    background: #e0e0e0;
    padding: 8px 12px;
    border-radius: 8px 8px 0 0;
    font-weight: bold;
    font-size: 12px;
    color: #333;
    border-bottom: 1px solid #ccc;
}

.cb-debugger-content {
    padding: 12px;
}

.cb-debugger-buttons {
    display: flex;
    gap: 6px;
    flex-wrap: wrap;
    margin-bottom: 10px;
}

.cb-btn {
    padding: 5px 10px;
    cursor: pointer;
    border-radius: 4px;
    font-size: 11px;
    font-weight: normal;
    border: 1px solid #ccc;
    transition: all 0.2s;
    background: #fff;
}

.cb-btn-go {
    color: #2e7d32;
    border-color: #c8e6c9;
}
.cb-btn-go:hover {
    background: #e8f5e9;
}
        
.cb-btn-pause {
    color: #df9d00;
    border-color: #ffefb2;
}
.cb-btn-pause:hover {
    background: #fff9db;
}

.cb-btn-stop {
    color: #c62828;
    border-color: #ffcdd2;
}
.cb-btn-stop:hover {
    background: #ffebee;
}

.cb-btn-step {
    color: #1565c0;
    border-color: #bbdefb;
}
.cb-btn-step:hover {
    background: #e3f2fd;
}

.cb-btn-close {
    color: #333;
    border-color: #bbb;
}
.cb-btn-close:hover {
    background: #f2f2f2;
}

.cb-debugger-info {
    font-size: 11px;
    color: #555;
    background: #fafafa;
    padding: 6px;
    border-radius: 4px;
    border: 1px solid #eee;
}

#cb-debug-status {
    font-weight: bold;
    color: #2e7d32;
}
    `;
    document.head.appendChild(style);
}

/* End Debugger */

/* Start Post-Back */

function cb_PostRequestAndResponse(evt, ViewState, formElement, retryCount = 0, resolveCallback)
{
    cb_ShowLoader();

    evt = evt || cb_FakeEvent();
    evt = cb_PreServedEvent(evt);

    let obj = evt.currentTarget || null;

    // Set Form Value
    let Form = obj;

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
        while (Form.nodeName.toLowerCase() != "form");
    }
    else
    {
        Form = cb_GetElementByElementPlace(formElement);
        if (!obj)
            obj = Form;
    }

    if (Form.nodeName.toLowerCase() != "form")
    {
        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    if (WebFormsOptions.CheckValidityForFormSubmit && !Form.checkValidity())
    {
        cb_RefreshFormCustomValidity();

        const elements = Form.elements;

        for (const element of elements)
            cb_SetValidityMessage(element);

        Form.reportValidity();

        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    const FormMethod = (WebFormsOptions.SendDataOnlyByPostMethod) ? "POST" : (Form.hasAttribute("method") ? Form.getAttribute("method") : "GET");
    var FormAction = Form.hasAttribute("action")? Form.getAttribute("action") : "";

    // Chek Form Multi Part
    let FormIsMultiPart = false;
    if (Form.hasAttribute("enctype") && (FormMethod.toLowerCase() == "post" || FormMethod.toLowerCase() == "put" || FormMethod.toLowerCase() == "patch" || FormMethod.toLowerCase() == "delete"))
        if (Form.getAttribute("enctype") == "multipart/form-data")
            FormIsMultiPart = true;


    // Set Progress Tag
    if (WebFormsOptions.UseProgressBar)
        cb_SetProgressTag(obj, Form);


    // Set Input Value
    let TagSubmitValue = null;
    switch (obj.nodeName.toLowerCase())
    {
        case "input":
        case "button":
            TagSubmitValue = (obj.getAttribute("value")) ? obj.getAttribute("value") : "";
            break;
        case "select": TagSubmitValue = (obj.options[obj.selectedIndex].value) ? obj.options[obj.selectedIndex].value : "";
    }

    let OldObjectType;
    if (obj.getAttribute("type"))
        if (obj.getAttribute("type").toLowerCase() == "submit")
        {
            OldObjectType = obj.type.toLowerCase();
            obj.setAttribute("type", "button");
            obj.setAttribute("main-type", "submit");
        }

    // Create Request Name
    let RequestNameForCache = '<';
    let RequestName = (FormAction == "") ? window.location.pathname : FormAction;
    if (FormAction.length > 0)
    {
        if (FormAction.substring(0, 1) == '#')
            RequestName = window.location.pathname + FormAction;

        if (FormAction.Contains('#'))
            RequestNameForCache = '#' + FormAction.GetTextAfter('#');
    }
    if (obj.getAttribute("name"))
        RequestName = obj.getAttribute("name") + '|' + TagSubmitValue + '|' + RequestName;

    // Check Cache
    if (cb_UsedCache(evt, RequestName, RequestNameForCache))
    {
        // Reset Input Type
        setTimeout(function () { if (OldObjectType === "submit") obj.type = "submit"; }, 1);

        if (obj.hasAttribute("main-type"))
            obj.removeAttribute("main-type");

        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    // Check Accepted URL
    if (!cb_IsInternalUrl(FormAction))
    {
        const tmpURL = new URL(FormAction, window.location.origin);

        if (WebFormsOptions.DisableLoadExternalHost)
        {
            if (WebFormsOptions.AddConsoleMessageForURL)
                console.warn("Access for load the external host is disabled but is being attempted.\npath: " + FormAction);

            cb_HideLoader();

            if (resolveCallback)
                resolveCallback();

            return;
        }

        if (WebFormsOptions.UseLoadExternalHostOnlyInAcceptedList)
            if (!WebFormsOptions.LoadExternalHostOnlyInAcceptedList.some(p => cb_MatchesPattern(p, tmpURL.hostname)))
            {
                if (WebFormsOptions.AddConsoleMessageForURL)
                    console.warn("Access to load the external host is only possible in the list, but is being attempted.\npath: " + FormAction);
                
                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();
                
                return;
            }
    }

    // Using WebSocket Protocol
    const tmpFormAction = (FormAction == "") ? window.location.pathname : FormAction;
    if (window.WebSocket && (cb_UseWebSocket || Form.hasAttribute("usewebsocket") || (cb_UseWebSocketPath.indexOf(tmpFormAction) >= 0)))
    {
        if (cb_UseWebSocket == '$')
            cb_UseWebSocket = false;

        cb_WebSocketSet(tmpFormAction);

        if (cb_WebSockets[tmpFormAction])
        {
            var formDataSerialize = "form=true&" + cb_FormDataSerialize(Form, obj.getAttribute("name"), TagSubmitValue, OldObjectType, false);

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
                const WebSocketResult = event.data;
                cb_SetResponse(evt, WebSocketResult, ViewState, RequestName);

                Form.focus();

                // Reset Input Type
                setTimeout(function () { if (OldObjectType === "submit") obj.type = "submit"; }, 1);

                if (obj.hasAttribute("main-type"))
                    obj.removeAttribute("main-type");

                if (WebFormsOptions.AddConsoleMessageForWebSockets)
                    console.log("WebSocket server response:\n" + event.data);
            };
        }

        // Reset Input Type
        setTimeout(function () { if (OldObjectType === "submit") obj.type = "submit"; }, 1);

        if (obj.hasAttribute("main-type"))
            obj.removeAttribute("main-type");

        cb_HideLoader();


        if (resolveCallback)
            resolveCallback();

        return;
    }

    // Using Http Protocol
    const XMLHttp = new XMLHttpRequest();
    XMLHttp.onreadystatechange = function ()
    {
        if (WebFormsOptions.AddConsoleMessageForHTTP)
            console.log("HTTP request with method " + FormMethod.toUpperCase() + ", path: " + FormAction);

        if (XMLHttp.readyState == 4)
        {
            if (XMLHttp.status >= 200 && XMLHttp.status < 300)
            {
                if (XMLHttp.status != 202 && XMLHttp.status != 204)
                {
                    const HttpResult = XMLHttp.responseText;
                    cb_SetResponse(evt, HttpResult, ViewState, RequestName);
                }

                // Reset Input Type
                setTimeout(function () { if (OldObjectType === "submit") obj.type = "submit"; }, 1);

                if (obj.hasAttribute("main-type"))
                    obj.removeAttribute("main-type");

                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();

                Form.focus();
            }
            else if (XMLHttp.status >= 400 && XMLHttp.status < 500)
            {
                if (WebFormsOptions.UseConnectionErrorMessage)
                    cb_ShowConnectionError(XMLHttp.status);

                // Reset Input Type
                setTimeout(function () { if (OldObjectType === "submit") obj.type = "submit"; }, 1);

                if (obj.hasAttribute("main-type"))
                    obj.removeAttribute("main-type");

                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();

                Form.focus();
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
                    setTimeout(function () { if (OldObjectType === "submit") obj.type = "submit"; }, 1);

                    if (obj.hasAttribute("main-type"))
                        obj.removeAttribute("main-type");

                    cb_HideLoader();

                    if (resolveCallback)
                        resolveCallback();

                    Form.focus();
                }
            }
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
            setTimeout(function () { if (OldObjectType === "submit") obj.type = "submit"; }, 1);

            if (obj.hasAttribute("main-type"))
                obj.removeAttribute("main-type");

            cb_HideLoader();

            if (resolveCallback)
                resolveCallback();

            Form.focus();
        }

        // Clean Progress Value
        if (WebFormsOptions.UseProgressBar)
            cb_CleanProgressValue();
    }

    XMLHttp.upload.onprogress = function (event)
    {
        if (WebFormsOptions.HideLoaderWhenUpload && event.lengthComputable && event.loaded >= WebFormsOptions.HideLoaderAfterUploaded)
            cb_HideLoader();
    };

    var formDataSerialize = cb_FormDataSerialize(Form, obj.getAttribute("name"), TagSubmitValue, OldObjectType, FormIsMultiPart);
    if ((FormMethod.toLowerCase() != "post") && (FormMethod.toLowerCase() != "put") && (FormMethod.toLowerCase() != "patch") && (FormMethod.toLowerCase() != "delete"))
    {
        FormAction = cb_AddQueryToUrl(FormAction, formDataSerialize);
        formDataSerialize = "";
    }
        
    XMLHttp.open(FormMethod, FormAction, true);

    if (WebFormsOptions.UseProgressBar && cb_HasFileInput(Form))
        XMLHttp.upload.addEventListener("progress", cb_ProgressHandler, false);

    if (!FormIsMultiPart)
        XMLHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

    XMLHttp.setRequestHeader("Post-Back", "true");

    if (FormIsMultiPart && "CompressionStream" in window && Blob.prototype.stream && cb_HasFileInput(Form) && WebFormsOptions.UseGzipFileSend)
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

                    const cs = new CompressionStream("gzip");
                    const compressedStream = value.stream().pipeThrough(cs);
                    const compressedBlob = await new Response(compressedStream).blob();
                    const gzippedFile = new File([compressedBlob], value.name + ".gz", { type: "application/gzip" });
                    newFormData.append(key, gzippedFile, gzippedFile.name);
                }
                else
                    newFormData.append(key, value);
            }
            XMLHttp.setRequestHeader("X-Files-Gzip", "true");
            XMLHttp.send(newFormData);
        })();
    }
    else if (!FormIsMultiPart && "CompressionStream" in window && Blob.prototype.stream && formDataSerialize && WebFormsOptions.UseGzipDataSend && WebFormsOptions.UseGzipDataSendLargerThan <= (new TextEncoder().encode(formDataSerialize).length))
    {
        // Gzip All Data (Except for multipart)
        (async () =>
        {
            const dataArray = new TextEncoder().encode(formDataSerialize);
            const cs = new CompressionStream("gzip");
            const compressedStream = new Blob([dataArray]).stream().pipeThrough(cs);
            const compressedBlob = await new Response(compressedStream).blob();
            XMLHttp.setRequestHeader("Content-Encoding", "gzip");
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
            if (evt.target.type.toLowerCase() == "submit")
                evt.preventDefault();

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_PostRequestAndResponse(evt, ViewState, null, 0, resolve);
    }));
}

/* End Post-Back */

/* Start Request And Response */

function cb_RequestAndResponse(evt, FormAction, ViewState, Method, retryCount = 0, resolveCallback)
{
    cb_ShowLoader();

    evt = evt || cb_FakeEvent();
    evt = cb_PreServedEvent(evt);

    // Create Request Name
    let RequestNameForCache = '<';
    let useHashPath = false;
    if (!FormAction)
        FormAction = "";
    let RequestName = (FormAction == "") ? window.location.pathname : FormAction;
    if (FormAction.length > 0)
    {
        if (FormAction.length > 1)
        {
            if (FormAction.substring(0, 2) == "#~")
            {
                FormAction = FormAction.substring(2);
                useHashPath = true;
            }
        }

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

    // Check Accepted URL
    if (!cb_IsInternalUrl(FormAction))
    {
        const tmpURL = new URL(FormAction, window.location.origin);

        if (WebFormsOptions.DisableLoadExternalHost)
        {
            if (WebFormsOptions.AddConsoleMessageForURL)
                console.warn("Access for load the external host is disabled but is being attempted.\npath: " + FormAction);

            cb_HideLoader();

            if (resolveCallback)
                resolveCallback();

            return;
        }

        if (WebFormsOptions.UseLoadExternalHostOnlyInAcceptedList)
            if (!WebFormsOptions.LoadExternalHostOnlyInAcceptedList.some(p => cb_MatchesPattern(p, tmpURL.hostname)))
            {
                if (WebFormsOptions.AddConsoleMessageForURL)
                    console.warn("Access to load the external host is only possible in the list, but is being attempted.\npath: " + FormAction);
                
                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();
                
                return;
            }
    }

    // Using WebSocket Protocol
    const tmpFormAction = (FormAction == "") ? window.location.pathname : FormAction;
    if (window.WebSocket && (cb_UseWebSocket || (cb_UseWebSocketPath.indexOf(tmpFormAction) >= 0)))
    {
        if (cb_UseWebSocket == '@')
            cb_UseWebSocket = false;

        cb_WebSocketSet(tmpFormAction);

        if (cb_WebSockets[tmpFormAction])
        {
            cb_WebSockets[tmpFormAction].onmessage = function (event)
            {
                const WebSocketResult = event.data;
                cb_SetResponse(evt, WebSocketResult, ViewState, RequestName);

                if (WebFormsOptions.AddConsoleMessageForWebSockets)
                    console.log("WebSocket server response:\n" + event.data);
            };
        }

        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    // Using Http Protocol
    const XMLHttp = new XMLHttpRequest();
    XMLHttp.onreadystatechange = function ()
    {
        if (WebFormsOptions.AddConsoleMessageForHTTP)
            console.log("HTTP request with method " + Method.toUpperCase() + ", path: " + FormAction);

        if (XMLHttp.readyState == 4)
        {
            if (XMLHttp.status >= 200 && XMLHttp.status < 300)
            {
                if (XMLHttp.status != 202 && XMLHttp.status != 204)
                {
                    let IsSPALink;
                    let linkPath;
                    let linkTitle;
                    let IsIgnoreQueryAndHash = false;

                    if (evt.currentTarget && evt.currentTarget.tagName)
                    {
                        IsSPALink = evt.currentTarget.tagName.toLowerCase() == 'a';
                        if (IsSPALink)
                        {
                            linkPath = evt.currentTarget.getAttribute("href");
                            linkTitle = (WebFormsOptions.SetTitleBySPALink && (evt.currentTarget.hasAttribute("title")) ? WebFormsOptions.SPAGlobalTitle.Replace("{{title}}", evt.currentTarget.getAttribute("title")) : null);

                            if (linkTitle)
                                document.title = linkTitle;

                            if (linkPath)
                            {
                                const currentUrl = new URL(window.location.href);
                                const targetUrl = new URL(linkPath, window.location.href);
                                
                                IsIgnoreQueryAndHash = (WebFormsOptions.IgnoreQueryAndHashInSPALink && targetUrl.pathname == currentUrl.pathname);

                                if (!IsIgnoreQueryAndHash)
                                    history.pushState(window.history.state, "", linkPath);
                                window.scrollTo(0, 0);
                            }
                        }
                    }

                    const HttpResult = XMLHttp.responseText;
                    if (Method.toUpperCase() != "HEAD")
                        cb_SetResponse(evt, HttpResult, ViewState, RequestName);

                    if (IsSPALink)
                    {
                        cb_PopstateIsPending = true;
                        setTimeout(() =>
                        {
                            try
                            {
                                if (!IsIgnoreQueryAndHash)
                                    cb_SPA.setState(false, linkPath, linkTitle);
                            }
                            catch
                            {
                                cb_PopstateIsPending = false;
                            }
                            cb_PopstateIsPending = false;
                        }, WebFormsOptions.SPASaveStateDelay);
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
                    setTimeout(() => { cb_RequestAndResponse(evt, (useHashPath? "#~" : "") + FormAction, ViewState, Method, retryCount + 1); }, WebFormsOptions.RetryRequestInterval);
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
            setTimeout(() => { cb_RequestAndResponse(evt, (useHashPath? "#~" : "") + FormAction, ViewState, Method, retryCount + 1); }, WebFormsOptions.RetryRequestInterval);
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

    XMLHttp.setRequestHeader("Post-Back", "true");

    XMLHttp.send();
}

function GetBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, "GET", 0, resolve);
    }));
}

function PutBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, "PUT", 0, resolve);
    }));
}

function PatchBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, "PATCH", 0, resolve);
    }));
}

function DeleteBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, "DELETE", 0, resolve);
    }));
}

function HeadBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, "HEAD", 0, resolve);
    }));
}

function OptionsBack(evt, FormAction, ViewState)
{
    evt = cb_PreServedEvent(evt);

    cb_RunInQueue(() => new Promise((resolve) =>
    {
        cb_RequestAndResponse(evt, FormAction, ViewState, "OPTIONS", 0, resolve);
    }));
}

/* End Request And Response */

/* Start Set Response Value */

function cb_SetResponse(evt, ResponseResult, ViewState, RequestName)
{
	let IsWebForms = false;

	// Check Exist WebForms Values
    if (ResponseResult.TrimStart().length >= 11)
        if (ResponseResult.TrimStart().substring(0, 11) == "[web-forms]")
		{
            ResponseResult = ResponseResult.TrimStart();
			IsWebForms = true;
		}

	if (IsWebForms)
    {
        cb_SetWebFormsValues(evt, RequestName, ResponseResult, true);

        if (WebFormsOptions.CreateCommentForWebFormsResponse)
        {
            const comment = document.createComment(ResponseResult);
            cb_GetResponseLocation().append(comment);
        }

        return;
    }

    const tmpDiv = document.createElement("div");
    tmpDiv.innerHTML = cb_RemoveScripts(ResponseResult).toDOM();

    if (ViewState)
    {
        if (typeof ViewState === "string")
        {
            const ViewStateObject = cb_GetElementByElementPlace(ViewState);
            ViewStateObject.replaceChildren(tmpDiv);
            cb_AppendJavaScriptTag(ResponseResult);
            cb_Initialization(ViewStateObject.getElementsByTagName("div")[0]);
            if (!WebFormsOptions.SetResponseInsideDivTag)
            {
                var divElement = ViewStateObject.getElementsByTagName("div")[0];
                divElement.replaceChildren(...divElement.childNodes);
            }
        }
        else if (typeof ViewState === "object")
        {
            ViewState.replaceChildren(tmpDiv);
            cb_AppendJavaScriptTag(ResponseResult);
            cb_Initialization(ViewState.getElementsByTagName("div")[0]);
            if (!WebFormsOptions.SetResponseInsideDivTag)
            {
                var divElement = ViewState.getElementsByTagName("div")[0];
                divElement.replaceChildren(...divElement.childNodes);
            }
        }
        else
        {
            cb_GetResponseLocation().prepend(tmpDiv);
            cb_AppendJavaScriptTag(ResponseResult);
            cb_Initialization(cb_GetResponseLocation().getElementsByTagName("div")[0]);
            if (!WebFormsOptions.SetResponseInsideDivTag)
            {
                var divElement = cb_GetResponseLocation().getElementsByTagName("div")[0];
                divElement.replaceChildren(...divElement.childNodes);
            }
        }
    }
    else if (ResponseResult || !WebFormsOptions.IgnoreEmptyResult)
    {
        cb_GetResponseLocation().replaceChildren(...(WebFormsOptions.SetResponseInsideDivTag ? [tmpDiv] : tmpDiv.childNodes));
        cb_AppendJavaScriptTag(ResponseResult);
        cb_Initialization(cb_GetResponseLocation());
    }
}

/* End Set Response Value */

/* Start Comment-Back */

function CommentBack(evt, index, InputPlace)
{
    cb_ShowLoader();
    evt = evt || cb_FakeEvent();

    const elementPlace = InputPlace ? cb_GetElementByElementPlace(InputPlace) : null;
 
    if (index)
        index = '#' + index;

    cb_SetWebFormsCommentsValue(elementPlace, evt, index, true);

    cb_HideLoader();
}

/* End Comment-Back */

/* Start Wasm-Back */

async function WasmBack(evt, wasmLanguage, wasmUrl, funcName, args, OutputPlace)
{
    cb_ShowLoader();
    evt = evt || cb_FakeEvent();

    const result = await cb_RunWasmMethodResult(wasmLanguage, wasmUrl, funcName, args);

    cb_SetResponse(evt, String(result), OutputPlace, "");

    cb_HideLoader();
}

/* End Wasm-Back */

/* Start Front-Back */

async function FrontBack(evt, modulePath, OutputPlace, ...args)
{
    cb_ShowLoader();

    if (WebFormsOptions.DisableLoadModule)
    {
        if (WebFormsOptions.AddConsoleMessageForModule)
            console.warn("Access for load the module is disabled but is being attempted.\nModule path: " + modulePath);
        return null;
    }

    if (WebFormsOptions.UseLoadModulePathOnlyInAcceptedList)
        if (!WebFormsOptions.LoadModulePathOnlyInAcceptedList.some(p => cb_MatchesPattern(p, modulePath)))
        {
            if (WebFormsOptions.AddConsoleMessageForModule)
                console.warn("Access to load the module is only possible in the list, but is being attempted.\nModule path: " + modulePath);
            return null;
        }

    evt = evt || cb_FakeEvent();

    try
    {
        const mod = await import(modulePath);
        const result = await mod["PageLoad"](evt, ...args);

        cb_SetResponse(evt, String(result), OutputPlace, "");
    }
    catch (er)
    {
        if (WebFormsOptions.AddConsoleMessage)
            console.error("Error loading module:", er);
    }

    cb_HideLoader();
}

/* End Front-Back */

/* Start WebSocket-Back */

function WebSocketBack(evt, Path)
{
    cb_AddWebSocketPath(Path);
    GetBack(evt, Path);
}

function cb_WebSocketBackWithoutQueue(evt, Path)
{
    cb_AddWebSocketPath(Path);
    cb_RequestAndResponse(evt, Path, undefined, "GET");
}

/* End WebSocket-Back */

/* Start Send-Back */

async function cb_SendRequestAndResponse(evt, ViewState, path, method = "POST", isMultiPart, contentType = "text/plain", data, retryCount = 0, resolveCallback)
{
    cb_ShowLoader();

    if (data)
        data = data.Replace("$[ln];", '\n').Replace("$[dq];", "\"").Replace("$[sq];", "'");

    if (data.startsWith('@') || data.startsWith('$'))
        data = await cb_FetchValue(evt, data);

    evt = evt || cb_FakeEvent();
    evt = cb_PreServedEvent(evt);

    const obj = evt.currentTarget || null;

    if (!path)
        path = "";

    // Create Request Name
    let RequestNameForCache = '<';
    let RequestName = (path == "") ? window.location.pathname : path;
    if (path.length > 0)
    {
        if (path.substring(0, 1) == '#')
            RequestName = window.location.pathname + path;

        if (path.Contains('#'))
            RequestNameForCache = '#' + path.GetTextAfter('#');
    }
    if (obj && obj.getAttribute("name"))
        RequestName = obj.getAttribute("name") + '|' + RequestName;

    // Check Cache
    if (cb_UsedCache(evt, RequestName, RequestNameForCache))
    {
        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    // Check Accepted URL
    if (!cb_IsInternalUrl(path))
    {
        const tmpURL = new URL(path, window.location.origin);

        if (WebFormsOptions.DisableLoadExternalHost)
        {
            if (WebFormsOptions.AddConsoleMessageForURL)
                console.warn("Access for load the external host is disabled but is being attempted.\npath: " + path);

            cb_HideLoader();

            if (resolveCallback)
                resolveCallback();

            return;
        }

        if (WebFormsOptions.UseLoadExternalHostOnlyInAcceptedList)
            if (!WebFormsOptions.LoadExternalHostOnlyInAcceptedList.some(p => cb_MatchesPattern(p, tmpURL.hostname)))
            {
                if (WebFormsOptions.AddConsoleMessageForURL)
                    console.warn("Access to load the external host is only possible in the list, but is being attempted.\npath: " + path);
                
                cb_HideLoader();

                if (resolveCallback)
                    resolveCallback();
                
                return;
            }
    }

    // Using WebSocket Protocol
    const tmpPath = (path == "") ? window.location.pathname : path;
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
                const WebSocketResult = event.data;
                cb_SetResponse(evt, WebSocketResult, ViewState, RequestName);

                if (WebFormsOptions.AddConsoleMessageForWebSockets)
                    console.log("WebSocket server response:\n" + event.data);
            };
        }

        cb_HideLoader();

        if (resolveCallback)
            resolveCallback();

        return;
    }

    // Using Http Protocol
    const XMLHttp = new XMLHttpRequest();
    XMLHttp.onreadystatechange = function ()
    {
        if (WebFormsOptions.AddConsoleMessageForHTTP)
            console.log("HTTP request with method " + method.toUpperCase() + ", path: " + path);

        if (XMLHttp.readyState == 4)
        {
            if (XMLHttp.status >= 200 && XMLHttp.status < 300)
            {
                if (XMLHttp.status != 202 && XMLHttp.status != 204)
                {
                    const HttpResult = XMLHttp.responseText;
                    cb_SetResponse(evt, HttpResult, ViewState, RequestName);
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
                    setTimeout(() => { cb_SendRequestAndResponse(evt, ViewState, path, method, isMultiPart, contentType, data, retryCount + 1) }, WebFormsOptions.RetryRequestInterval);
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
            setTimeout(() => { cb_SendRequestAndResponse(evt, ViewState, path, method, isMultiPart, contentType, data, retryCount + 1) }, WebFormsOptions.RetryRequestInterval);
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

    if ((method.toLowerCase() != "post") && (method.toLowerCase() != "put") && (method.toLowerCase() != "patch") && (method.toLowerCase() != "delete"))
    {
        path = cb_AddQueryToUrl(path, data);
        data = "";
    }
        
    XMLHttp.open(method, path, true);

    if (!isMultiPart)
        XMLHttp.setRequestHeader("Content-Type", contentType);

    XMLHttp.setRequestHeader("Post-Back", "true");

    if (!isMultiPart && "CompressionStream" in window && Blob.prototype.stream && data && WebFormsOptions.UseGzipDataSend && WebFormsOptions.UseGzipDataSendLargerThan <= (new TextEncoder().encode(data).length))
    {
        // Gzip All Data (Except for multipart)
        (async () =>
        {
            const dataArray = new TextEncoder().encode(data);
            const cs = new CompressionStream("gzip");
            const compressedStream = new Blob([dataArray]).stream().pipeThrough(cs);
            const compressedBlob = await new Response(compressedStream).blob();
            XMLHttp.setRequestHeader("Content-Encoding", "gzip");
            XMLHttp.send(compressedBlob);
        })();
    }
    else if (data)
    {
        if (isMultiPart)
        {
            const formData = new FormData();
            formData.append("content", data);
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

/* End Send-Back */

/* Start SSE-Back */

function SSEBack(evt, path, shouldReconnect = true, reconnectTryTimeout = 3000, viewState)
{
    cb_ShowLoader();
    cb_ConnectToSSE(evt, path, shouldReconnect, reconnectTryTimeout, viewState);
    cb_HideLoader();
}

/* End SSE-Back */

/* Start Form Data */

function cb_FormDataSerialize(form, TagSubmitName, TagSubmitValue, TagSubmitType, FormIsMultiPart)
{   
    let FormString = "";
    const TmpFormData = new FormData();

    if (!form || form.nodeName.toLowerCase() != "form")
        return;

    const useOnlyChanged = form.hasAttribute("use-only-change-update");

    for (let i = form.elements.length - 1; i >= 0; i = i - 1)
    {
        let el = form.elements[i];
        if (el.name === "" || el.disabled)
            continue;

        let parent = el.parentElement;
        let skip = false;
        while (parent)
        {
            if (parent.tagName.toLowerCase() === "fieldset" && parent.disabled)
            {
                skip = true;
                break;
            }
            parent = parent.parentElement;
        }
        if (skip)
            continue;

        let firstValue = useOnlyChanged ? el.getAttribute("cb-first-value") : undefined;

        switch (el.nodeName.toLowerCase())
        {
            case "input":
                switch (el.type.toLowerCase())
                {
                    case "text":
                    case "number":
                    case "hidden":
                    case "password":
                    case "reset":
                    case "color":
                    case "date":
                    case "range":
                    case "search":
                    case "time":
                    case "datetime-local":
                    case "email":
                    case "month":
                    case "tel":
                    case "url":
                    case "week":
                    {
                        if (firstValue === el.value)
                            continue;

                        if (FormIsMultiPart)
                            TmpFormData.append(el.name, el.value);
                        else
                            FormString += el.name + '=' + encodeURIComponent(el.value) + '&';
                    }
                        break;
                    case "checkbox":
                    case "radio":
                        if (el.checked)
                        {
                            if (FormIsMultiPart)
                                TmpFormData.append(el.name, el.value);
                            else
                                FormString += el.name + '=' + el.value + '&';
                        }
                        break;
                    case "file":
                    {
                        const files = el.files;

                        if (files.length == 0)
                            break;

                        for (let k = 0; k < files.length; k++)
                        {
                            const file = files[k];
                            if (FormIsMultiPart)
                                TmpFormData.append(el.name, file, file.name);
                            else
                                FormString += el.name + '=' + encodeURIComponent(file.name) + '&';
                        }
                    }
                        break;
                }
                break;
            case "textarea":
            {
                if (firstValue === el.value)
                    continue;

                if (FormIsMultiPart)
                    TmpFormData.append(el.name, el.value);
                else
                    FormString += el.name + '=' + encodeURIComponent(el.value) + '&';
            }
                break;
            case "output":
            {
                if (firstValue === el.textContent)
                    continue;

                if (FormIsMultiPart)
                    TmpFormData.append(el.name, el.textContent);
                else
                    FormString += el.name + '=' + encodeURIComponent(el.textContent) + '&';
            }
                break;
            case "select":
                switch (el.type.toLowerCase())
                {
                    case "select-one":
                    {
                        if (firstValue === el.value)
                            continue;

                        if (FormIsMultiPart)
                            TmpFormData.append(el.name, el.value);
                        else
                            FormString += el.name + '=' + encodeURIComponent(el.value) + '&';
                    }
                        break;
                    case "select-multiple":
                    {
                        let selectedValues = [];
                        for (let option of el.options)
                            if (option.selected)
                                selectedValues.push(option.value);

                        if (firstValue && firstValue.split(',').sort().join(',') === selectedValues.sort().join(','))
                            continue;

                        for (let j = el.options.length - 1; j >= 0; j = j - 1)
                        {
                            if (el.options[j].selected)
                            {
                                if (FormIsMultiPart)
                                    TmpFormData.append(el.name, el.options[j].value);
                                else
                                    FormString += el.name + '=' + encodeURIComponent(el.options[j].value) + '&';
                            }
                        }
                    }
                        break;
                }
                break;
        }
    }

    // Add Button Submit
    if (TagSubmitType === "submit")
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
            let checksumSource = "";
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

    currentElement.setAttribute("use-only-change-update", "true");

    const fields = currentElement.querySelectorAll("input, textarea, select");

    for (let el of fields)
    {
        if (!el.name || el.disabled)
            continue;

        let value = "";

        switch (el.nodeName.toLowerCase())
        {
            case "input":
                switch (el.type.toLowerCase())
                {
                    case "checkbox":
                    case "radio":
                    case "file":
                        continue;
                    default:
                        value = el.value;
                        break;
                }
                break;

            case "textarea":
                value = el.value;
                break;

            case "output":
                value = el.textContent;
                break;

            case "select":
                if (el.type === "select-one")
                    value = el.value;
                else if (el.type === "select-multiple")
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

        el.setAttribute("cb-first-value", value);
    }
}

function cb_SetValidityMessage(element)
{
    if (!element.validity)
        return;

    const validity = element.validity;

    if (validity.valueMissing)
        element.setCustomValidity(WebFormsOptions.ValidityValueMissing);
    else if (validity.typeMismatch)
        element.setCustomValidity(WebFormsOptions.ValidityTypeMismatch);
    else if (validity.patternMismatch)
        element.setCustomValidity(WebFormsOptions.ValidityPatternMismatch);
    else if (validity.tooLong)
        element.setCustomValidity(WebFormsOptions.ValidityTooLong.replaceAll("{{maxLength}}", element.maxLength).replaceAll("{{valueLength}}", element.value.length));
    else if (validity.tooShort)
        element.setCustomValidity(WebFormsOptions.ValidityTooShort.replaceAll("{{minLength}}", element.minLength).replaceAll("{{valueLength}}", element.value.length));
    else if (validity.rangeUnderflow)
        element.setCustomValidity(WebFormsOptions.ValidityRangeUnderflow.replaceAll("{{min}}", element.min));
    else if (validity.rangeOverflow)
        element.setCustomValidity(WebFormsOptions.ValidityRangeOverflow.replaceAll("{{max}}", element.max));
    else if (validity.stepMismatch)
        element.setCustomValidity(WebFormsOptions.ValidityStepMismatch);
    else if (validity.badInput)
        element.setCustomValidity(WebFormsOptions.ValidityBadInput);
    else
        element.setCustomValidity("");
}

function cb_SetFormCustomValidity(obj)
{
    if (!WebFormsOptions.AutoSetFormCustomValidity)
        return;

    const Forms = obj ? obj.querySelectorAll("form") : document.querySelectorAll("form");

    Forms.forEach(function(form)
    {
        if (form.dataset.customValidityApplied)
            return;

        const elements = form.elements;

        for (const element of elements)
        {
            if (!element.validity)
                continue;

            function setupValidationEvent(eventType)
            {
                element.addEventListener(eventType, function()
                {
                    cb_SetValidityMessage(this);

                    if (!this.validity.valid)
                        this.reportValidity();
                });
            }

            setupValidationEvent("input");
            setupValidationEvent("change");
        }

        form.dataset.customValidityApplied = "true";
    });
}

function cb_RefreshFormCustomValidity()
{
    document.querySelectorAll("form[data-custom-validity-applied]").forEach(function(form)
    {
        delete form.dataset.customValidityApplied;
    });
    
    cb_SetFormCustomValidity();
}

/* End Form Data */

/* Start Append Java Script */

function cb_ExtractScriptTags(Html)
{
    const scriptList = new Array();
    const regex = /<script[^>]*>([\s\S]*?)<\/script>/g;
    let match;

    while ((match = regex.exec(Html)) !== null)
    {
        const ScriptTag = document.createElement("script");
        const ScriptContent = match[1];

        // Extract Attributes
        const AttrRegex = /([a-zA-Z0-9_]+)="([^"]*)"/g;
        let AttrMatch;

        while ((AttrMatch = AttrRegex.exec(match[0])) !== null)
        {
            const Name = AttrMatch[1];
            const Value = AttrMatch[2];
            ScriptTag.setAttribute(Name, Value);
        }

        const TextNode = document.createTextNode(ScriptContent);

        ScriptTag.appendChild(TextNode);
        scriptList.push(ScriptTag);
    }

    return scriptList;
}

function cb_IsScriptAlreadyInDOM(script)
{
    const allScripts = document.querySelectorAll("script");

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
        if (WebFormsOptions.AddConsoleMessage)
            console.warn("Access to the JavaScript is disabled but is being attempted.");
        return;
    }

    const scriptList = cb_ExtractScriptTags(HtmlSource);

    for (let i = 0; i < scriptList.length; i++)
    {
        const script = scriptList[i];

        if (cb_IsScriptAlreadyInDOM(script))
        {
            if (WebFormsOptions.AddConsoleMessage)
                console.error("Warning: Exist duplicate script!\nThis issue occurs when the WebForms Core technology is incorrectly configured.");
            continue;
        }

        document.body.appendChild(script);
    }
}

function cb_RemoveScripts(html)
{
    const div = document.createElement("div");
    div.innerHTML = html;

    div.querySelectorAll("script").forEach(s => s.remove());

    return div.innerHTML;
}

/* End Append Java Script */

/* Start Progress Bar */
function cb_ProgressHandler(event)
{
    const Percent = (event.loaded / event.total) * 100;

    if (event.total >= 1048576)
        document.getElementById("div_ProgressPercentLoaded").textContent = (event.loaded / 1048576).toFixed(1) + '(' + Math.round(Percent) + "%)" + " / " + (event.total / 1048576).toFixed(1) + " MB";
    else
        document.getElementById("div_ProgressPercentLoaded").textContent = (event.loaded / 1024).toFixed(1) + '(' + Math.round(Percent) + "%)" + " / " + (event.total / 1024).toFixed(1) + " KB";

    document.getElementById("div_ProgressUploadValue").style.width = Math.round(Percent) + '%';
}

function cb_SetProgressTag(obj, form)
{
    if (!cb_HasFileInput(form))
        return;

    if (!document.getElementById("div_ProgressUpload"))
    {
        const DivProgressUpload = document.createElement("div");
        DivProgressUpload.id = "div_ProgressUpload";
        DivProgressUpload.setAttribute("style", WebFormsOptions.ProgressBarStyle);

        const DivProgressPercentLoaded = document.createElement("div");
        DivProgressPercentLoaded.id = "div_ProgressPercentLoaded";
        DivProgressPercentLoaded.setAttribute("style", WebFormsOptions.ProgressBarPercentLoadedStyle);

        const DivProgressUploadValue = document.createElement("div");
        DivProgressUploadValue.id = "div_ProgressUploadValue";
        DivProgressUploadValue.setAttribute("style", WebFormsOptions.ProgressBarValueStyle);

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
    if (document.getElementById("div_ProgressUploadValue"))
        document.getElementById("div_ProgressUpload").remove();
}

function cb_HasFileInput(Form)
{
    if (Form.getElementsByTagName("file").length > 0)
        return true;

    const InputCount = Form.getElementsByTagName("input").length;

    for (var i = 0; i < InputCount; i++)
        if (Form.getElementsByTagName("input").item(i).hasAttribute("type"))
            if (Form.getElementsByTagName("input").item(i).getAttribute("type").toLowerCase() == "file")
                return true;

    return false;
}

/* End Progress Bar */

/* Start Web-Forms Comment */

function cb_SetWebFormsCommentsValue(obj, evt, requestName = "", breakDone = false)
{
    evt = evt || cb_FakeEvent();
    const root = obj || document;

    const walker = document.createTreeWalker(root, NodeFilter.SHOW_COMMENT);
    let node;

    while ((node = walker.nextNode()))
    {
        if (!node.nodeValue.startsWith("[web-forms]"))
            continue;

        if (node._done && !breakDone)
            continue;
        node._done = true;

        let rawData = node.nodeValue.trim();

        if (!rawData)
            continue;

        if (rawData.endsWith("$[da];"))
            rawData = rawData.slice(0, -6) + '-';

        cb_SetWebFormsValues(evt, requestName, rawData.Replace("$[dd];", "--"), true, false);
    }
}

/* End Web-Forms Comment */

/* Start Execute Web-Forms */

async function cb_ReplaceDynamicValue(evt, ActionControl)
{
    if ((ActionControl.length > 1) && (ActionControl.substring(0, 1) == ';'))
    {
        ActionControl = ActionControl.substring(1);

        let searchValue = ActionControl.GetTextBefore(GS);
        ActionControl = ActionControl.GetTextAfter(GS);

        let searchTo = ActionControl.GetTextBefore(GS);
        ActionControl = ActionControl.GetTextAfter(GS);

        searchValue = await cb_SetDynamicForValue(evt, searchValue);
        searchTo = await cb_SetDynamicForValue(evt, searchTo);

        return ActionControl.Replace(searchValue, searchTo);
    }

    return ActionControl;
}

async function cb_RunWebFormsValues(evt, RequestName, WebFormsValues, UsePostBack, WithoutWebFormsSection, loopIndex = 0)
{
    // Initialization to Index
    let StartIndex = RequestName.Contains('#') ? RequestName.GetTextAfter('#') : "";
    let IndexHasStarted = ((StartIndex == "") || (StartIndex == '0'));
    let StartIndexIsNumber = StartIndex.IsNumber();
    let StartIndexIndex = StartIndexIsNumber ? parseInt(StartIndex) : 0;
    let IndexForStartIndex = 1;

    // Loop
    let ForEachStartIndexStack = new Array();
    let ForEachEndIndexStack = new Array();

    // Remove Request Name For Cache
    if (RequestName.length > 1)
        if (RequestName.substring(0, 1) == '<')
            RequestName = "";

    if (!WithoutWebFormsSection)
    {
        WebFormsValues = WebFormsValues.substring(11);
        if (WebFormsValues.length > 0)
            if (WebFormsValues.substring(0,1) == '\n')
                WebFormsValues = WebFormsValues.substring(1);
    }

    const WebFormsList = (UsePostBack) ? WebFormsValues.split('\n') : WebFormsValues.split("$[sln];");

    let TransientDOM = null;
    let TransientDOMPlace = null;
    let LastElementPlaceList = null;

    // Using Debug
    if (cb_DebugActive)
        console.log(WebFormsList);

    for (let i = loopIndex; i < WebFormsList.length; i++)
    {
        try
        {
            // Using Debug
            if (cb_DebugActive)
            {
                console.info("Stop in: " + WebFormsList[i]);

                if (cb_DebugPaused)
                {
                    cb_DebugStepPending = true;
                    await cb_WaitForCondition(500, () => !cb_DebugPaused);
                    cb_DebugStepPending = false;

                    cb_DebugPaused = true;
                }

                console.info("Running: " + WebFormsList[i]);
            }

            let ActionControl = WebFormsList[i].FullTrim();

            if (!ActionControl)
                continue;

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
                    if (ActionControl == ("#=" + StartIndex))
                        IndexHasStarted = true;

                    continue;
            }

            // Replace Dynamic Value
            ActionControl = await cb_ReplaceDynamicValue(evt, ActionControl);

            // Set Dynamic Value
            let v1, v2, v3, v4, v5, v6, v7, v8, v9, v10;
            let vArgs = [];
            if (ActionControl.Contains('='))
            {
                // Token Preservation for Run Action Controls
                let comments = [];

                ActionControl = ActionControl.replace(/<!--[\s\S]*?-->/g, function(match)
                {
                    comments.push(match);
                    return "{{COMMENT_" + (comments.length - 1) + "}}";
                });

                vArgs = ActionControl.GetTextAfter('=').split(GS);

                [v1, v2, v3, v4, v5, v6, v7, v8, v9, v10] = await cb_SetDynamicValueForArgs(evt, vArgs);

                for (let iAC = 0; iAC < comments.length; iAC++)
                    for (let jAC = 0; jAC < vArgs.length; jAC++)
                        if (typeof vArgs[jAC] === "string")
                            vArgs[jAC] = vArgs[jAC].replace("{{COMMENT_" + iAC + "}}", comments[iAC].Replace("$[ln];", "\n"));

                [v1, v2, v3, v4, v5, v6, v7, v8, v9, v10] = vArgs;
            }
            
            // Checking Foreach Loop 
            if (ActionControl.length > 1)
                if (ActionControl.substring(0, 3) == "{fe")
                {
                    await cb_StorageIsReady;
                    var [path, In, key] = [v1, v2, v3];

                    // If ForEach Started
                    if (ForEachStartIndexStack.at(-1) == i)
                    {
                        let updateForEachValue = cb_DeleteJSON(cb_StorageGet(key), path);
                        let ForEachHasValue = cb_GetJSON(cb_StorageGet(key)) != updateForEachValue;

                        if (!ForEachHasValue)
                        {
                            i = ForEachEndIndexStack.at(-1);

                            ForEachStartIndexStack.pop();
                            ForEachEndIndexStack.pop();
                    
                            cb_StorageDelete(key);
                            cb_StorageDelete(key + 'i');

                            continue;
                        }

                        cb_StorageSet(key, updateForEachValue);
                        cb_StorageSet(key + 'i', parseInt(cb_StorageGet(key + 'i')) + 1);
                    }
                    else
                    {
                        let ForEachLoopIn = cb_SerialToJSON(In);
                        cb_StorageSet(key, ForEachLoopIn);
                        cb_StorageSet(key + 'i', '0');
                        ForEachStartIndexStack.push(i);

                        // Find ForEach End Index
                        let forEachEndIndex = i;

                        for (var k = forEachEndIndex + 1; k < WebFormsList.length; k++)
                        {
                            if ((WebFormsList[k].substring(0, 1) == '{') && WebFormsList[k].length > 1)
                                forEachEndIndex++;
                            else
                                break;
                        }

                        if (WebFormsList[forEachEndIndex + 1] == '{')
                        {
                            var openBracket = 1;
                            for (var k = forEachEndIndex + 2; k < WebFormsList.length; k++)
                            {
                                if (WebFormsList[k] == '{')
                                    openBracket++;

                                if (WebFormsList[k] == '}')
                                    openBracket--;

                                forEachEndIndex++;

                                if (openBracket == 0)
                                    break;                               
                            }
                        }
                        forEachEndIndex++;

                        // Else Execution
                        if ((WebFormsList[forEachEndIndex + 1] == "}e"))
                        {
                            forEachEndIndex++;
                            
                            for (var k = forEachEndIndex + 1; k < WebFormsList.length; k++)
                            {
                                if ((WebFormsList[k].substring(0, 1) == '{') && WebFormsList[k].length > 1)
                                    forEachEndIndex++;
                                else
                                    break;
                            }

                            if (WebFormsList[forEachEndIndex + 1] == '{')
                            {
                                var openBracket = 1;
                                for (var k = forEachEndIndex + 2; k < WebFormsList.length; k++)
                                {
                                    if (WebFormsList[k] == '{')
                                        openBracket++;

                                    if (WebFormsList[k] == '}')
                                        openBracket--;

                                    forEachEndIndex++;

                                    if (openBracket == 0)
                                        break;                               
                                }
                            }
                            forEachEndIndex++;
                        }

                        ForEachEndIndexStack.push(forEachEndIndex);
                    }
                }

            if (ForEachEndIndexStack.at(-1) == i)
            {
                i = ForEachStartIndexStack.at(-1) - 1;
                continue;
            }

            // Pre Runner
            const PreRunner = new Array();
            let FirstChar = ActionControl.substring(0, 1);
            let PreRunnerIndexer = 0;
            while ((FirstChar == ':') || (FirstChar == '(') || (FirstChar == ','))
            {
                PreRunner[PreRunnerIndexer] = ActionControl.GetTextBefore(')');
                ActionControl = ActionControl.GetTextAfter(')');
                FirstChar = ActionControl.substring(0, 1);
                PreRunnerIndexer++;
            }
            if (PreRunner.length > 0)
            {
                let tmpActionControl = ActionControl;
                cb_SetPreRunnerQueue(PreRunner, async () => await cb_RunWebFormsValues(evt, "", tmpActionControl, true, true));
                continue;
            }

            // Hash
            if (ActionControl.length > 1)
            {
                if (ActionControl == "SH")
                {
                    var hash = await cb_GetHashSHA256(WebFormsValues);
                    cb_ActionControlHashList.push(String(hash));
                    continue;
                }
                if (ActionControl == "CS")
                {
                    var checksum = cb_Checksum(WebFormsValues);
                    cb_ActionControlHashList.push(String(checksum));
                    continue;
                }
            }

            // Set Value
            var Value = ActionControl.GetTextAfter('=').Replace("$[ln];", '\n');

            if (ActionControl == ';')
                break;

            if (ActionControl == '{' || ActionControl == '}')
                continue;

            var SecondChar = ActionControl.substring(1, 2);
            switch (FirstChar)
            {
                // Check Condition
                case '{':
                {
                    let ConditionIsTrue = false;
                    let ConditionIsAsync = false;
                    let ConditionPeriodMiliSecond = -1;

                    if (SecondChar == '(')                   
                        ConditionPeriodMiliSecond = ActionControl.GetTextAfter('(').GetTextBefore(')');

                    // Await all Action Controls Until True Condition then Async Condition
                    if (ConditionPeriodMiliSecond == '0')
                    {
                        // Replace Dynamic Value
                        let tmpActionControl = WebFormsList[i];
                        tmpActionControl = await cb_ReplaceDynamicValue(evt, tmpActionControl);
                        
                        cb_WaitForCondition(WebFormsOptions.AwaitConditionInterval, cb_CheckCondition, evt, tmpActionControl.substring(4)).then(async () =>
                        {
                            cb_RunWebFormsValues(evt, "", WebFormsList.join('\n'), true, true, i + 1);
                        }).catch(() => { });

                        return;
                    }
                    else if (ConditionPeriodMiliSecond == "-1") // Sync Condition (Once)
                        ConditionIsTrue = await cb_CheckCondition(evt, ActionControl.substring(1));
                    else if (ConditionPeriodMiliSecond == 'a') // Async
                    {
                        ConditionIsTrue = true;
                        ConditionIsAsync = true;
                        ConditionPeriodMiliSecond = 3600000;
                    }

                    // Check Period and Async Condition
                    if (ConditionPeriodMiliSecond > 0)
                    {
                        var tmpActionControl = WebFormsList[i];
                        let conditionAsyncList = new Array();

                        for (var k = i + 1; k < WebFormsList.length; k++)
                        {
                            if ((WebFormsList[k].substring(0, 1) == '{') && WebFormsList[k].length > 1)
                            {
                                i++;
                                conditionAsyncList.push(WebFormsList[k]);
                            }
                            else
                                break;
                        }

                        if (WebFormsList[i + 1] == '{')
                        {
                            var openBracket = 1;
                            for (var k = i + 2; k < WebFormsList.length; k++)
                            {
                                if (WebFormsList[k] == '{')
                                    openBracket++;

                                if (WebFormsList[k] == '}')
                                    openBracket--;

                                i++;

                                if (openBracket == 0)
                                    break;
                                else
                                    conditionAsyncList.push(WebFormsList[k]);                         
                            }
                        }
                        else
                            conditionAsyncList.push(WebFormsList[i + 1]);
                        i++;

                        // Is Async
                        if (ConditionIsAsync)
                            cb_RunAsync(() => { cb_RunWebFormsValues(evt, "", conditionAsyncList.join('\n'), true, true); });
                        else // Is Async Interval
                        {
                            // Replace Dynamic Value
                            tmpActionControl = await cb_ReplaceDynamicValue(evt, tmpActionControl);

                            cb_WaitForCondition(ConditionPeriodMiliSecond, cb_CheckCondition, evt, tmpActionControl.GetTextAfter(')')).then(async () =>
                            {
                                await cb_RunWebFormsValues(evt, "", conditionAsyncList.join('\n'), true, true);
                            }).catch(() => { });
                        }
                        continue;
                    }

                    // Check Sync Condition
                    if (!ConditionIsTrue)
                    {
                        var isNested = false;

                        for (var k = i + 1; k < WebFormsList.length; k++)
                        {
                            if ((WebFormsList[k].substring(0, 1) == '{') && WebFormsList[k].length > 1)
                            {
                                i++;
                                isNested = true;
                            }
                            else
                                break;
                        }

                        if (WebFormsList[i + 1] == '{')
                        {
                            var openBracket = 1;
                            for (var k = i + 2; k < WebFormsList.length; k++)
                            {
                                if (WebFormsList[k] == '{')
                                    openBracket++;

                                if (WebFormsList[k] == '}')
                                    openBracket--;

                                i++;

                                if (openBracket == 0)
                                    break;                               
                            }
                        }
                        i++;

                        // Else Execution
                        if ((WebFormsList[i + 1] == "}e") && !isNested)
                            i++;
                    }
                    
                    continue;
                }
                // Check Else Condition. Because we Already Handled the Execution of Else, any Else is Ignored.
                case '}':
                    if (SecondChar == 'e')
                    {
                        for (var k = i + 1; k < WebFormsList.length; k++)
                        {
                            if ((WebFormsList[k].substring(0, 1) == '{') && WebFormsList[k].length > 1)
                                i++;
                            else
                                break;
                        }

                        if (WebFormsList[i + 1] == '{')
                        {
                            var openBracket = 1;
                            for (var k = i + 2; k < WebFormsList.length; k++)
                            {
                                if (WebFormsList[k] == '{')
                                    openBracket++;

                                if (WebFormsList[k] == '}')
                                    openBracket--;

                                i++;

                                if (openBracket == 0)
                                    break;                               
                            }
                        }
                            
                        i++;
                        continue;
                    }
                    break;

                case '_':
                {
                    const scriptValue = String(v1).Replace("$[ln];", "\n").FullTrim();
                    if (WebFormsOptions.DisableEval)
                    {
                        if (WebFormsOptions.AddConsoleMessage)
                            console.warn("Access to the eval method is disabled but is being attempted.\nScript value: " + scriptValue);
                        continue;
                    }
                    eval(scriptValue);
                    continue;
                }
                case 'l':
                    switch (SecondChar)
                    {
                        case 'm':
                        case 'M':
                        if (v2)
                        {
                            var funcName = v1;

                            var args = v2;

                            // Delete Bracket
                            args = args.substring(1);

                            args = await cb_SetDynamicValueForArgs(evt, args.split(US));

                            if (SecondChar == 'm')
                                await cb_RunMethod(evt, funcName, args);
                            else
                                await cb_RunModuleMethod(evt, funcName, args);
                            continue;
                        }
                        
                        if (SecondChar == 'm')
                            await cb_RunMethod(evt, v1);
                        else
                            await cb_RunModuleMethod(evt, v1);
                        continue;

                        case 'r': location.reload(); continue;
                        case 'h': location.href = v1; continue;

                        case 'A':
                        {
                            var currentEvent = v1 == '1' ? evt : cb_FakeEvent();
                            var withoutWebFormsSection = v2 == '1';
                            var index = v3 ? '#' + v3 : "";
                            var actionControls = v4;
                            
                            cb_SetWebFormsValues(currentEvent, index, actionControls, true, withoutWebFormsSection);
                            continue;
                        }
                        case 's':
                        {
                            var path = v1;
                            if (!path)
                                path = window.location.pathname + window.location.search + window.location.hash;

                            cb_SPA.render(evt, path);
                            continue;
                        }
                    }
                    break;

                case 'D':
                    switch (SecondChar)
                    {
                        case 'e': await new Promise(resolve => setTimeout(resolve, v1)); continue;
                        case 'i': cb_DeletePreRunnerInterval(v1); continue;
                        case 's':
                            if (v1)
                                cb_DisconnectSSE(v1);
                            else
                                cb_DisconnectAllSSE();
                            continue;
                        case 'S':
                            if (v1 == '*')
                                cb_SPA.clearAllStates();
                            else if (v1)
                                cb_SPA.deleteState(v1);
                            else
                                cb_SPA.deleteState(window.location.pathname);
                            continue;

                        case 'c':
                            cb_CreateDebugger();
                            cb_DebugStart();
                            if (v1 == '1')
                                cb_DebugPause();
                            continue;
                    }
                    break;

                case '.':
                    await cb_StorageIsReady;
                    switch (SecondChar)
                    {
                        case 'C': cb_StorageSet(v1, v2); continue;
                        case 'D': cb_StorageDelete(v1); continue;
                        case 'a':
                        {
                            var key = v1;
                            var formatChar = v2;

                            switch (formatChar)
                            {
                                case 'j':
                                    var value = v3;
                                    var path = v4;
                                    cb_StorageSet(key, cb_AddJSON(cb_StorageGet(key), path, value));
                                    continue;
                                case 'x':
                                    var name = v3;
                                    var value = v4;
                                    var path = v5;
                                    cb_StorageSet(key, cb_AddXML(cb_StorageGet(key), path, name, value));
                                    continue;
                                case 'i':
                                    var isINILike = v3 == '1';
                                    var value = v4;
                                    var path = v5;
                                    cb_StorageSet(key, cb_AddINI(cb_StorageGet(key), path, value, isINILike));
                                    continue;
                                case 't':
                                    var text = v3;
                                    var line = v4;
                                    cb_StorageSet(key, cb_AppendTextLine(cb_StorageGet(key), line, text));
                                    continue;
                                case 'v': cb_StorageSet(key, v3); continue;
                            }
                            break;
                        }
                        case 'u':
                        {
                            var key = v1;
                            var formatChar = v2;

                            switch (formatChar)
                            {
                                case 'j':
                                {
                                    var value = v3;
                                    var path = v4;
                                    cb_StorageSet(key, cb_SetJSON(cb_StorageGet(key), path, value));
                                    continue;
                                }
                                case 'x':
                                {
                                    var value = v3;
                                    var path = v4;
                                    cb_StorageSet(key, cb_SetXML(cb_StorageGet(key), path, value));
                                    continue;
                                }
                                case 'i':
                                {
                                    var isINILike = v3 == '1';
                                    var value = v4;
                                    var path = v5;
                                    cb_StorageSet(key, cb_UpdateINI(cb_StorageGet(key), path, value, isINILike));
                                    continue;
                                }
                                case 't':
                                {
                                    var text = v3;
                                    var line = v4;
                                    cb_StorageSet(key, cb_SetTextLine(cb_StorageGet(key), line, text));
                                    continue;
                                }
                                case 'v': cb_StorageSet(key, v3); continue;
                            }
                            break;
                        }
                        case 'i':
                        {
                            var key = v1;
                            var formatChar = v2;

                            switch (formatChar)
                            {
                                case 'v':
                                    cb_StorageSet(key, Number(cb_StorageGet(key)) + Number(v3));
                                    continue;
                            }
                            break;
                        }
                        case 'd':
                        {
                            var key = v1;
                            var formatChar = v2;

                            switch (formatChar)
                            {
                                case 'j': cb_StorageSet(key, cb_DeleteJSON(cb_StorageGet(key), v3)); continue;
                                case 'x': cb_StorageSet(key, cb_DeleteXML(cb_StorageGet(key), v3)); continue;
                                case 'i':
                                {
                                    var isINILike = v3 == '1';
                                    var path = v4;
                                    cb_StorageSet(key, cb_DeleteINI(cb_StorageGet(key), path, isINILike));
                                    continue;
                                }
                                case 't': cb_StorageSet(key, cb_DeleteTextLine(cb_StorageGet(key), v3)); continue;
                                case 'v': cb_StorageDelete(key); continue;
                            }
                        }
                    }
                    break;

                case 'w':
                    switch (SecondChar)
                    {
                        case 'g': history.go(v1); continue;
                        case 's': window.scrollTo(v1, v2); continue;
                        case 'R':
                        {
                            var [path, scobePath] = [v1, v2];
                            await cb_ServiceWorker.register(path, scobePath);
                            await navigator.serviceWorker.ready;
                            continue;
                        }
                        case 'p': await cb_ServiceWorker.preCacheStatic(vArgs); continue;
                        case 'c':
                            await cb_ServiceWorker.cache.add(v1, v2);
                            continue;
                        case 'd':
                            if (v1)
                                await cb_ServiceWorker.cache.remove(v1);
                            else
                                await cb_ServiceWorker.cache.clear();
                            continue;
                        case 't':
                            await cb_ServiceWorker.cache.setTTL(v1, v2);
                            continue;
                        case 'r':
                            var cacheDynamic = (v3 == '1');
                            await cb_ServiceWorker.routeSet(v1, v2, cacheDynamic);
                            continue;
                        case 'a':
                            await cb_ServiceWorker.routeAlias(v1, v2);
                            continue;
                        case 'C':
                            await cb_ServiceWorker.routeRemoveAlias(v1);
                            continue;
                        case 'D':
                            if (v1)
                                await cb_ServiceWorker.routeRemove(v1);
                            else
                                await cb_ServiceWorker.routeClear();
                            continue;
                    }
                    break;

                case 'L':
                {
                    var currentEvent;
                    currentEvent = (v1 == '1') ? evt : cb_FakeEvent();

                    switch (SecondChar)
                    {
                        case 'p': cb_PostRequestAndResponse(currentEvent, v3, v2); continue;
                        case 'C': CommentBack(currentEvent, String(v2), v3); continue;
                        case 'y':
                        {
                            var wasmUrl = v3;
                            var funcName = v4;
                            var args = v5;
                            var outputPlace = v6;

                            if (args)
                            {
                                // Delete Bracket
                                args = args.substring(1);

                                args = await cb_SetDynamicValueForArgs(evt, args.split(US));
                            }
                            await WasmBack(currentEvent, v2, wasmUrl, funcName, args, outputPlace);
                            continue;
                        }
                        case 'w': cb_WebSocketBackWithoutQueue(currentEvent, v2); continue;
                        case 'g': cb_RequestAndResponse(currentEvent, v2, v3, "GET"); continue;
                        case 't': cb_RequestAndResponse(currentEvent, v2, v3, "PUT"); continue;
                        case 'P': cb_RequestAndResponse(currentEvent, v2, v3, "PATCH"); continue;
                        case 'd': cb_RequestAndResponse(currentEvent, v2, v3, "DELETE"); continue;
                        case 'h': cb_RequestAndResponse(currentEvent, v2, "", "HEAD"); continue;
                        case 'o': cb_RequestAndResponse(currentEvent, v2, v3, "OPTIONS"); continue;
                        case 's':
                        {
                            var shouldReconnect = v3;
                            var ReconnectTryTimeout = v4;
                            var outputPlace = v5;

                            shouldReconnect = shouldReconnect == '1';
                            SSEBack(currentEvent, v2, shouldReconnect, ReconnectTryTimeout, outputPlace);
                            continue;
                        }
                        case 'S':
                        {
                            var method = v3;
                            var isMultiPart = v4 == '1';
                            var contentType = v5;
                            var data = v6;
                            var outputPlace = v7;

                            cb_SendRequestAndResponse(currentEvent, outputPlace, v2, method, isMultiPart, contentType, data);
                            continue;
                        }
                        case 'j':
                        {
                            var args = [];

                            if (v4)
                            {
                                var args = v4;

                                // Delete Bracket
                                args = args.substring(1);

                                args = await cb_SetDynamicValueForArgs(evt, args.split(US));
                            }

                            await FrontBack(currentEvent, v2, v3, ...args);
                            continue;
                        }
                    }
                    break;
                }
                case '@':
                    await cb_SaveValue(evt, ActionControl.substring(1, 2), ActionControl.substring(2, 3), ActionControl.substring(3), LastElementPlaceList, TransientDOM);
                    continue;

                case '&':
                {
                    var LineIndex = String(v1);
                    var Repeat = v2;
                    var InitialRepeat = Repeat;

                    if (v3)
                        InitialRepeat = v3;

                    Repeat = parseInt(Repeat, 10);

                    if (Repeat == 0)
                    {
                        WebFormsList[i] = "&=" + LineIndex + GS + InitialRepeat;
                        continue;
                    }

                    Repeat--;

                    WebFormsList[i] = "&=" + LineIndex + GS + Repeat + GS + InitialRepeat;

                    if (LineIndex.substring(0, 1) == '#')
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
                        var LineIndexInt = parseInt(LineIndex, 10);

                        if (LineIndexInt >= 0)
                            i = LineIndexInt - 2;
                        else
                            i = i + LineIndexInt - 1;
                    }

                    continue;
                }

                case 'r':
                {
                    var CacheKeyValue = v1;
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
                        case 'E':
                        {
                            var [searchValue, searchTo] = [v1, v2];
                            searchValue = await cb_SetDynamicForValue(evt, searchValue);
                            searchTo = await cb_SetDynamicForValue(evt, searchTo);
                            for (var replaceIndex = 0; replaceIndex < WebFormsList.length; replaceIndex++)
                                WebFormsList[replaceIndex] = WebFormsList[replaceIndex].Replace(searchValue, searchTo);
                            continue;
                        }
                        case 'o':
                            if (!v1)
                            {
                                Object.assign(WebFormsOptions, WebFormsDefaultOptions);
                                continue;
                            }

                            if (Object.hasOwn(WebFormsDefaultOptions, v1))
                                WebFormsOptions[v1] = WebFormsDefaultOptions[v1];
                            else
                                if (WebFormsOptions.AddConsoleMessage)
                                    console.log("This option does not exist: " + name);
                            continue;
                    }
                    break;
                }
                case 's':
                    switch (SecondChar)
                    {
                        case 'C':
                            cb_SetCookie(v1, v2, v3, v4);
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
                        {
                            if (!RequestName)
                                continue;
                            localStorage.setItem(RequestName, WebFormsValues);

                            if (v1 != '*')
                            {
                                var UntilDate = new Date();
                                UntilDate.setSeconds(UntilDate.getSeconds() + parseInt(v1));

                                localStorage.setItem(RequestName + "-date", UntilDate);
                            }
                            continue;
                        }
                        case 'u':
                            window.history.replaceState({ url: v1 }, null, v1);
                            continue;
                        case 'o':
                        {
                            var [name, value] = [v1, v2];
                            
                            if (!Object.hasOwn(WebFormsOptions, name))
                            {
                                if (WebFormsOptions.AddConsoleMessage)
                                    console.log("This option does not exist: " + name);
                                continue;
                            }

                            WebFormsOptions[name] = value;

                            continue;
                        }
                    }
                    break;

                case 'C':
                case 'S':
                {
                    const cacheName = String(v1);
                    let cacheValue = String(v2);
                    var isCache = (FirstChar == 'C');
                    cacheValue = cacheValue.Replace("$[ln];", '\n');
                    switch (SecondChar)
                    {
                        case 'A':
                            cb_SetStorage(isCache, cacheName, cacheValue);
                            continue;
                        case 'I':
                        {
                            const exists = isCache ? cb_LocalCacheExists(cacheName) : cb_SessionCacheExists(cacheName);

                            if (!exists)
                                cb_SetStorage(isCache, cacheName, cacheValue);

                            continue;
                        }
                        case 'P':
                            cb_SetStorage(isCache, cacheName, cb_GetStorage(isCache, cacheName) + cacheValue);
                            continue;
                        case 'R':
                        {
                            const searchValue = cacheValue.GetTextAfter(GS);
                            cacheValue = cacheValue.GetTextBefore(GS);
                            cb_SetStorage(isCache, cacheName, cb_GetStorage(isCache, cacheName).Replace(searchValue, cacheValue));
                            continue;
                        }
                    }
                    break;
                }
                case 'e':
                    switch (SecondChar)
                    {
                        case 'w':
                            if (v1 == '$')
                                cb_UseWebSocket = '$';
                            else
                                cb_UseWebSocket = (v1 == '1');
                            continue;

                        case 'b':
                            cb_EnableScrollBottomEvent(v1 == '1');
                            continue;
                    }
                    break;

                case 'd':
                    switch (SecondChar)
                    {
                        case 'w':
                            cb_WebSocketDelete(v1);
                            continue;
                    }
                    break;

                case 'a':
                    switch (SecondChar)
                    {
                        case 'w':
                            cb_AddWebSocketPath(v1);
                            continue;
                    }
                    break;

                case 'h':
                    switch (SecondChar)
                    {
                        case 't':
                            document.title = v1;
                            continue;
                    }
                    break;

                case 'A':
                    switch (SecondChar)
                    {
                        case 'l':
                        {
                            var [text, type, title, okText] = [v1, v2, v3, v4];

                            if (!type)
                                type = "none";
                            if (!title)
                                title = "Alert";
                            if (!okText)
                                okText = "OK";

                            cb_ShowAlert(text, type, title, okText);
                            continue;
                        }
                        case 'S':
                        case 's':
                        {
                            var [linkPath, linkTitle] = [v1, v2];
                            if (!linkPath)
                                linkPath = window.location.pathname + window.location.search + window.location.hash;

                            cb_SPA.setState(SecondChar == 'S', linkPath, linkTitle);
                            continue;
                        }
                    }
                    break;

                case 'M':
                    switch (SecondChar)
                    {
                        case 'l':
                        {
                            var modulePath = v1;
                            var moduleMethods;

                            if (v2)
                            {
                                // Delete Bracket
                                moduleMethods = v2.substring(1);

                                moduleMethods = await cb_SetDynamicValueForArgs(evt, moduleMethods.split(US));
                            }

                            await cb_LoadModule(modulePath, moduleMethods);
                            continue;
                        }
                        case 'u': cb_UnloadModule(v1); continue;
                        case 'd': cb_DeleteModuleMethod(v1); continue;
                    }
                    break;

                case 'm':
                    switch (SecondChar)
                    {
                        case 'e':
                        {
                            var [text, type, duration] = [v1, v2, v3];

                            if (!type)
                                type = "none";
                            if (!duration)
                                duration = 0;

                            cb_ShowMessage(text, type, duration);
                            continue;
                        }
                        case 'c':
                        {
                            var text = v1;
                            var type = v2 ? v2 : "log";

                            switch (type)
                            {
                                case "log": console.log(text); break;
                                case "info": console.info(text); break;
                                case "warn": console.warn(text); break;
                                case "error": console.error(text); break;
                                case "debug": console.debug(text); break;
                                case "trace": console.trace(text); break;
                                case "group": console.group(text); break;
                                case "groupend": console.groupEnd(text); break;
                                case "table": console.table(text); break;
                                default: console.log(text);
                            }
                            continue;
                        }
                        case 'a':
                        {
                            var text = v1;
                            var condition = v2;
                            console.assert(condition, text);
                            continue;
                        }
                    }
                    break;

                case 't':
                    switch (SecondChar)
                    {
                        case 'd':
                            if (v1 == ';')
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
                                TransientDOMPlace = v1;
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
                        case 'w': await navigator.clipboard.writeText(v1);
                    }
            }

            // Extension
            if (await cb_SetWebFormsValuesExtension(evt, FirstChar, SecondChar, Value, vArgs, LastElementPlaceList, TransientDOM))
                continue;

            const ActionName = ActionControl.substring(0, 2);
            const ActionValue = ActionControl.substring(2);

            const ActionOperation = ActionName.substring(0, 1);
            const ActionFeature = ActionName.substring(1, 2);

            LastElementPlaceList = await cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue, vArgs, LastElementPlaceList, TransientDOM);
        }
        catch (er)
        {
            if (WebFormsOptions.AddConsoleMessage)
                console.warn("There was a problem in webforms value whene executing the command: " + er + "\nError in command: " + WebFormsList[i] + (WebFormsOptions.UseConsoleStackTrace ? "\n" + er.stack : ""));

            if (WebFormsOptions.AddMessageForProblemInSetWebFormsValue)
                cb_ShowMessage(WebFormsOptions.ProblemInSetWebFormsValueLang, "problem", WebFormsOptions.MessageDuration);
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

async function cb_SetValueToInput(evt, ActionOperation, ActionFeature, ActionValue, vArgs, LastElementPlaceList, TransientDOM)
{
    let ElementPlace = ActionValue.Contains('=') ? ActionValue.GetTextBefore('=') : ActionValue;
    let Value = ActionValue.GetTextAfter('=').FullTrim().Replace("$[ln];", '\n');

    // Set Dynamic Value
    let [v1, v2, v3, v4, v5, v6, v7, v8, v9, v10] = vArgs;
    
    let LabelForIndexer = 0;
    let ElementPlaceList;

    const CurrentDocument = TransientDOM ?? document;

    if (ElementPlace == '-')
        ElementPlaceList = LastElementPlaceList;
    else
    {
        let HasRequester = false;
        let Requester;
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
            if (HasRequester)
            {
                let tmpElementRequester = cb_GetElementByElementPlace(ElementPlace, Requester, TransientDOM);

                if (Array.isArray(tmpElementRequester) || tmpElementRequester instanceof NodeList || tmpElementRequester instanceof HTMLCollection)
                    ElementPlaceList = tmpElementRequester;
                else
                    ElementPlaceList = [tmpElementRequester];
            }
            else
            {
                let tmpElement = cb_GetElementByElementPlace(ElementPlace, null, TransientDOM);

                if (Array.isArray(tmpElement) || tmpElement instanceof NodeList || tmpElement instanceof HTMLCollection)
                    ElementPlaceList = tmpElement;
                else
                    ElementPlaceList = [tmpElement];
            }
        }
        else
        {
            ElementPlaceList = new Array();
            ElementPlaceList[0] = Requester;
        }
    }

    for (let i = 0; i < ElementPlaceList.length; i++)
    {
        const CurrentElement = ElementPlaceList[i];
        try
        {
            if (!CurrentElement)
                continue;

            // Without Server Attribute
            switch (ActionOperation)
            {
                case 'a':
                    switch (ActionFeature)
                    {
                        case 'i': CurrentElement.id = (CurrentElement.id) ? CurrentElement.id + v1 : v1; break;
                        case 'n':
                            if (CurrentElement.tagName.IsInput())
                                CurrentElement.name = (CurrentElement.name) ? CurrentElement.name + v1 : v1;
                            else
                                if (CurrentElement.hasAttribute("name"))
                                {
                                    var NameAttr = CurrentElement.getAttribute("name");
                                    CurrentElement.setAttribute("name", NameAttr + v1);
                                }
                                else
                                    CurrentElement.setAttribute("name", v1);
                            break;
                        case 'v':
                            if (CurrentElement.tagName.IsInput())
                                CurrentElement.value = (CurrentElement.value) ? CurrentElement.value + v1 : v1;
                            else
                                if (CurrentElement.hasAttribute("value"))
                                {
                                    var ValueAttr = CurrentElement.getAttribute("value");
                                    CurrentElement.setAttribute("value", ValueAttr + v1);
                                }
                                else
                                    CurrentElement.setAttribute("value", v1);
                            break;
                        case 'c':
                            if (CurrentElement.hasAttribute("class"))
                            {
                                var ClassAttr = CurrentElement.getAttribute("class");
                                CurrentElement.setAttribute("class", ClassAttr + ' ' + v1);
                            }
                            else
                                CurrentElement.setAttribute("class", v1);
                            break;
                        case 's':
                            if (CurrentElement.hasAttribute("style"))
                            {
                                var StyleAttr = CurrentElement.getAttribute("style");
                                if (StyleAttr.charAt(StyleAttr.length - 1) == ';')
                                    CurrentElement.setAttribute("style", StyleAttr + v1);
                                else
                                    CurrentElement.setAttribute("style", StyleAttr + ';' + v1);
                            }
                            else
                                CurrentElement.setAttribute("style", v1);
                            break;
                        case 'o':
                        {
                            var OptionTag = document.createElement("option");
                            var OptionValue = v1;
                            var OptionText = v2;
                            
                            OptionTag.selected = (v3 == '1');

                            OptionTag.value = OptionValue;
                            OptionTag.text = OptionText;

                            CurrentElement.appendChild(OptionTag);
                            break;
                        }
                        case 'k':
                        {
                            var CheckBoxTag = document.createElement("input");
                            CheckBoxTag.setAttribute("type", "checkbox");

                            var CheckBoxValue = v1;
                            var CheckBoxText = v2;
                            CheckBoxTag.checked = (v3 == '1');

                            CheckBoxTag.setAttribute("value", CheckBoxValue);
                            var CeckBoxIndex = CurrentElement.querySelectorAll('input[type="checkbox"]').length;

                            var CheckBoxNameAndText = "cblst_NoneSet";
                            if (CurrentElement.id)
                                CheckBoxNameAndText = CurrentElement.id;
                            else
                                if (CeckBoxIndex > 0)
                                    CheckBoxNameAndText = CurrentElement.querySelectorAll('input[type="checkbox"]')[0].name.GetTextBefore('$');

                            CheckBoxTag.id = CheckBoxNameAndText + '_' + CeckBoxIndex;
                            CheckBoxTag.name = CheckBoxNameAndText + '$' + CeckBoxIndex;

                            CurrentElement.appendChild(document.createElement("br"));

                            CurrentElement.appendChild(CheckBoxTag);

                            var LabelTag = document.createElement("label");
                            LabelTag.setAttribute("for", CheckBoxTag.id);
                            LabelTag.innerText = CheckBoxText;
                            CurrentElement.appendChild(LabelTag);

                            break;
                        }
                        case 'l':
                            if (CurrentElement.hasAttribute("title"))
                            {
                                var TitleAttr = CurrentElement.getAttribute("title");
                                CurrentElement.setAttribute("title", TitleAttr + v1);
                            }
                            else
                                CurrentElement.setAttribute("title", v1);
                            break;

                        case 'A':
                        {
                            if (!CurrentElement.id)
                                CurrentElement.id = "tmp_Element" + LabelForIndexer++;

                            var LabelTag = CurrentDocument.querySelector('label[for="' + CurrentElement.id + '"]');

                            if (LabelTag)
                                LabelTag.innerText = LabelTag.innerText + v1;
                            else
                            {
                                LabelTag = document.createElement("label");
                                LabelTag.setAttribute("for", CurrentElement.id);
                                LabelTag.innerText = v1;
                                CurrentElement.insertAdjacentElement("beforebegin", LabelTag);
                            }
                            break;
                        }
                        case 't':
                        {
                            var tmpValue = String(v1);
                            if (tmpValue.HasTag())
                            {
                                CurrentElement.insertAdjacentHTML("beforeend", cb_RemoveScripts(tmpValue).toDOM());
                                cb_AppendJavaScriptTag(tmpValue);
                                cb_Initialization(CurrentElement);
                            }
                            else
                                CurrentElement.insertAdjacentHTML("beforeend", tmpValue);
                            break;
                        }
                        case 'a':
                        {
                            var AttrName = v1;
                            var Splitter = v2;
                            var AttrValue = v3 ? v3 : "";
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
                        }
                        case 'h':
                        {
                            var TmpTag = document.createElement("input");
                            TmpTag.setAttribute("type", "hidden");
                            var [hiddenName, hiddenValue, hiddenId] = [v1, v2, v3];
                            TmpTag.value = hiddenValue;
                            TmpTag.name = hiddenName;

                            if (hiddenId)
                                TmpTag.setAttribute("id", hiddenId);

                            CurrentElement.append(TmpTag);
                        }
                    }
                    break;

                case 's':
                case 'i':
                    switch (ActionFeature)
                    {
                        case 'i':
                            if ((ActionOperation == 'i') && (CurrentElement.id))
                                break;

                            CurrentElement.id = v1;
                            break;
                        case 'n':
                            if (CurrentElement.tagName.IsInput())
                            {
                                if ((ActionOperation == 'i') && CurrentElement.name)
                                    break;

                                CurrentElement.name = v1;
                            }
                            else
                            {
                                if (ActionOperation == 'i' && CurrentElement.hasAttribute("name"))
                                    break;

                                CurrentElement.setAttribute("name", v1);
                            }
                            break;
                        case 'v':
                            if (CurrentElement.tagName.IsInput())
                            {
                                if ((ActionOperation == 'i') && CurrentElement.value)
                                    break;

                                CurrentElement.value = v1;
                            }
                            else
                            {
                                if (ActionOperation == 'i' && CurrentElement.hasAttribute("value"))
                                    break;

                                CurrentElement.setAttribute("value", v1);
                            }
                            break;
                        case 'c':
                            if (CurrentElement.hasAttribute("class"))
                            {
                                var ClassAttr = CurrentElement.getAttribute("class");

                                if ((ActionOperation == 'i') && (ClassAttr.ContainsWithSplitter(v1, ' ')))
                                    break;

                                CurrentElement.setAttribute("class", ClassAttr + ' ' + v1);
                            }
                            else
                                CurrentElement.setAttribute("class", v1);
                            break;
                        case 's':
                            if (CurrentElement.hasAttribute("style"))
                                cb_AddInlineStyle(CurrentElement, v1, ActionOperation != 'i');
                            else
                                CurrentElement.setAttribute("style", v1);
                            break;
                        case 'o':
                        {
                            if ((ActionOperation == 'i') && (CurrentElement.querySelectorAll('option[value="' + v1 + ' "]').length > 0))
                                break;

                            var tmpOptionTag = CurrentElement.querySelector('option[value="' + v1 + '"]');

                            var OptionTag = tmpOptionTag ?? document.createElement("option");
                            var OptionValue = v1;
                            var OptionText = v2;
                            OptionTag.selected = (v3 == '1');

                            OptionTag.value = OptionValue;
                            OptionTag.text = OptionText;

                            if (!tmpOptionTag)
                                CurrentElement.appendChild(OptionTag);
                            break;
                        }
                        case 'k':
                        {
                            if ((CurrentElement.tagName.toLowerCase() == "input") && ((CurrentElement.type.toLowerCase() == "checkbox") || (CurrentElement.type.toLowerCase() == "radio")))
                            {
                                CurrentElement.checked = (v1 == '1');
                                break;
                            }

                            if ((ActionOperation == 'i') && (CurrentElement.querySelectorAll('input[type="checkbox"][value="' + v1 + '"]').length > 0))
                                break;

                            var CheckBoxTag = document.createElement("input");
                            CheckBoxTag.setAttribute("type", "checkbox");

                            var CheckBoxValue = v1;
                            var CheckBoxText = v2;
                            CheckBoxTag.checked = (v3 == '1');

                            CheckBoxTag.setAttribute("value", CheckBoxValue);
                            var CeckBoxIndex = CurrentElement.querySelectorAll('input[type="checkbox"]').length;

                            var CheckBoxNameAndText = "cblst_NoneSet";
                            if (CurrentElement.id)
                                CheckBoxNameAndText = CurrentElement.id;
                            else
                                if (CeckBoxIndex > 0)
                                    CheckBoxNameAndText = CurrentElement.querySelectorAll('input[type="checkbox"]')[0].name.GetTextBefore('$');

                            CheckBoxTag.id = CheckBoxNameAndText + '_' + CeckBoxIndex;
                            CheckBoxTag.name = CheckBoxNameAndText + '$' + CeckBoxIndex;

                            CurrentElement.appendChild(document.createElement("br"));

                            CurrentElement.appendChild(CheckBoxTag);

                            var LabelTag = document.createElement("label");
                            LabelTag.setAttribute("for", CheckBoxTag.id);
                            LabelTag.innerText = CheckBoxText;
                            CurrentElement.appendChild(LabelTag);

                            break;
                        }
                        case 'l':
                            if (CurrentElement.hasAttribute("title"))
                                if ((ActionOperation == 'i') && CurrentElement.hasAttribute("title"))
                                    break;

                            CurrentElement.setAttribute("title", v1);
                            break;
                        case 'A':
                        {
                            if (!CurrentElement.id)
                                CurrentElement.id = "tmp_Element" + LabelForIndexer++;

                            var LabelTag = CurrentDocument.querySelector('label[for="' + CurrentElement.id + '"]');

                            if (LabelTag)
                            {
                                if ((ActionOperation == 'i') && LabelTag.innerText)
                                    break;

                                LabelTag.innerText = v1;
                            }
                            else
                            {
                                LabelTag = document.createElement("label");
                                LabelTag.setAttribute("for", CurrentElement.id);
                                LabelTag.innerText = v1;
                                CurrentElement.insertAdjacentElement("beforebegin", LabelTag);
                            }
                            break;
                        }
                        case 't':
                        {
                            if ((ActionOperation == 'i') && (CurrentElement.innerHTML || CurrentElement.innerText))
                                break;
                            var tmpValue = String(v1);
                            if (tmpValue.HasTag())
                            {

                                CurrentElement.replaceChildren();
                                CurrentElement.insertAdjacentHTML("beforeend", cb_RemoveScripts(tmpValue).toDOM());
                                cb_AppendJavaScriptTag(tmpValue);
                                cb_Initialization(CurrentElement);
                            }
                            else
                                CurrentElement.textContent = tmpValue;
                            break;
                        }
                        case 'a':
                        {
                            var AttrName = v1;
                            var Splitter = v2;
                            var AttrValue = v3 ? v3 : "";
                            if (CurrentElement.hasAttribute(AttrName))
                            {
                                var CurrentAttr = CurrentElement.getAttribute(AttrName);

                                if ((ActionOperation == 'i') && (CurrentAttr.ContainsWithSplitter(AttrValue, Splitter)))
                                    break;
                            }
                            CurrentElement.setAttribute(AttrName, AttrValue);
                        }
                    }
                    break;

                case 'd':
                    switch (ActionFeature)
                    {
                        case 'i':
                            if (CurrentElement.id)
                                CurrentElement.removeAttribute("id");
                            break;
                        case 'n':
                            if (CurrentElement.name)
                                CurrentElement.removeAttribute("name");
                            break;
                        case 'v':
                            if (CurrentElement.value)
                                CurrentElement.value = "";
                            break;
                        case 'c':
                            if (CurrentElement.className)
                                CurrentElement.className = CurrentElement.className.DeleteHtmlClass(v1);
                            break;
                        case 's':
                            if (CurrentElement.hasAttribute("style"))
                                CurrentElement.style.removeProperty(v1);
                            break;
                        case 'o':
                            if (v1 == '*')
                            {
                                var OptionList = CurrentElement.querySelectorAll("option");
                                for (var OptionIndex = 0; OptionIndex < OptionList.length; OptionIndex++)
                                    OptionList[OptionIndex].remove();
                                break;
                            }
                            if (CurrentElement.querySelectorAll('option[value="' + v1 + '"]').length > 0)
                                CurrentElement.querySelectorAll('option[value="' + v1 + '"]')[0].remove();
                            break;
                        case 'k':
                        {
                            if (v1 == '*')
                            {
                                var CheckBoxList = CurrentElement.querySelectorAll('input[type="checkbox"]');
                                for (var CheckBoxTagIndex = 0; CheckBoxTagIndex < CheckBoxList.length; CheckBoxTagIndex++)
                                {
                                    var LabelTag = CurrentDocument.querySelector('label[for="' + CheckBoxList[CheckBoxTagIndex].id + '"]');
                                    if (LabelTag)
                                        LabelTag.remove();

                                    CheckBoxList[CheckBoxTagIndex].remove();
                                }
                                break;
                            }
                            var CheckBoxTagLength = CurrentElement.querySelectorAll('input[type="checkbox"][value="' + v1 + '"]').length;
                            if (CheckBoxTagLength > 0)
                            {
                                var CheckBoxTag = CurrentElement.querySelectorAll('input[type="checkbox"][value="' + v1 + '"]')[0];
                                if (CheckBoxTag.id)
                                    if (CurrentElement.querySelectorAll('label[for="' + CheckBoxTag.id + '"]').length > 0)
                                        CurrentElement.querySelectorAll('label[for="' + CheckBoxTag.id + '"]')[0].remove();

                                CheckBoxTag.remove();
                            }
                            break;
                        }
                        case 'l':
                            if (CurrentElement.hasAttribute("title"))
                                CurrentElement.removeAttribute("title");
                            break;
                        case 'A':
                            if (CurrentElement.id)
                            {
                                var LabelTag = CurrentDocument.querySelector('label[for="' + CurrentElement.id + '"]');
                                if (LabelTag)
                                    LabelTag.remove();
                            }
                            break;
                        case 't':
                            CurrentElement.replaceChildren();
                            break;
                        case 'a':
                            if (CurrentElement.hasAttribute(v1))
                                CurrentElement.removeAttribute(v1);
                            break;
                        case 'e':
                        {
                            var LabelTag = CurrentDocument.querySelector('label[for="' + CurrentElement.id + '"]');
                            if (LabelTag)
                                LabelTag.remove();
                            CurrentElement.remove();
                            break;
                        }
                        case 'p':
                            CurrentElement.parentElement.remove();
                    }
                    break;

                case '+':
                case '-':
                    switch (ActionFeature)
                    {
                        case 'n':
                            if (CurrentElement.hasAttribute("minlength"))
                            {
                                var ElementMinLength = (ActionOperation == '+') ? parseInt(CurrentElement.getAttribute("minlength")) + parseInt(v1) : parseInt(CurrentElement.getAttribute("minlength")) - parseInt(v1);
                                CurrentElement.setAttribute("minlength", ElementMinLength);
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.setAttribute("minlength", v1);
                            break;
                        case 'x':
                            if (CurrentElement.hasAttribute("maxlength"))
                            {
                                var ElementMaxLength = (ActionOperation == '+') ? parseInt(CurrentElement.getAttribute("maxlength")) + parseInt(v1) : parseInt(CurrentElement.getAttribute("maxlength")) - parseInt(v1);
                                CurrentElement.setAttribute("maxlength", ElementMaxLength);
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.setAttribute("maxlength", v1);
                            break;
                        case 'f':
                            if (CurrentElement.style.fontSize)
                            {
                                var Unit = CurrentElement.style.fontSize.GetUnit();
                                var ElementFontSize = (ActionOperation == '+') ? parseInt(CurrentElement.style.fontSize) + parseInt(v1) : parseInt(CurrentElement.style.fontSize) - parseInt(v1);
                                CurrentElement.style.fontSize = ElementFontSize.toString() + Unit;
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.style.fontSize = v1 + "px";
                            break;
                        case 'w':
                            if (CurrentElement.style.width)
                            {
                                var Unit = CurrentElement.style.width.GetUnit();
                                var ElementWidth = (ActionOperation == '+') ? parseInt(CurrentElement.style.width) + parseInt(v1) : parseInt(CurrentElement.style.width) - parseInt(v1);
                                CurrentElement.style.width = ElementWidth.toString() + Unit;
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.style.width = parseInt(getComputedStyle(CurrentElement).width) + parseInt(v1) + "px";
                            break;
                        case 'h':
                            if (CurrentElement.style.height)
                            {
                                var Unit = CurrentElement.style.height.GetUnit();
                                var ElementHeight = (ActionOperation == '+') ? parseInt(CurrentElement.style.height) + parseInt(v1) : parseInt(CurrentElement.style.height) - parseInt(v1);
                                CurrentElement.style.height = ElementHeight.toString() + Unit;
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.style.height = parseInt(getComputedStyle(CurrentElement).height) + parseInt(v1) + "px";
                            break;
                        case 'v':
                            if (CurrentElement.value)
                            {
                                var Elementv1 = (ActionOperation == '+') ? parseInt(CurrentElement.value) + parseInt(v1) : parseInt(CurrentElement.value) - parseInt(v1);
                                CurrentElement.value = Elementv1.toString();
                            }
                            else
                                if ((ActionOperation == '+'))
                                    CurrentElement.value = v1;
                    }
                    break;

                case 'g':
                {
                    var action = v1;
                    switch (ActionFeature)
                    {
                        case 't':
                            switch (action)
                            {
                                case 'i': CurrentElement.textContent = parseFloat(CurrentElement.textContent) + parseFloat(v2); break;
                                case 'r':
                                {
                                    var value = v2;
                                    var newValue = v3;
                                    var alsoStartTag = v4;
                                    var deep = v5;
                                    deep = (deep == '1');
                                    alsoStartTag = (alsoStartTag == '1');

                                    if (deep)
                                        cb_ReplaceDeep(CurrentElement, value, newValue, alsoStartTag);
                                    else
                                    {
                                        CurrentElement.textContent = CurrentElement.textContent.Replace(value, newValue);

                                        if (alsoStartTag)
                                            cb_ReplaceStartTag(CurrentElement, value, newValue);
                                    }
                                    break;
                                }
                                case 's':
                                {
                                    var value = v2;
                                    var newValue = v3;

                                    cb_ReplaceStartTag(CurrentElement, value, newValue);
                                }
                            }
                    }
                    break;
                }
                case 'E':
                    switch (ActionFeature)
                    {
                        case 'p':
                            if (v2)
                            {
                                var HtmlEvent = v1;
                            
                                if (v2 == '+')
                                    cb_AddEvent(CurrentElement, HtmlEvent, "PostBack(event, true)");
                                else
                                    cb_AddEvent(CurrentElement, HtmlEvent, "PostBack(event, '" + v2 + "')");
                            }
                            else
                                cb_AddEvent(CurrentElement, v1, "PostBack(event)");
                            break;
                        case 'P':
                            if (v2)
                            {
                                var HtmlEvent = v1;
                            
                                if (v2 == '+')
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, PostBack, [true]);
                                else
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, PostBack, [v2]);
                                break;
                            }
                            else
                                await cb_AddEventListener(CurrentElement, v1, PostBack, []);
                            break;
                        case 'g':
                        case 't':
                        case 'a':
                        case 'l':
                        case 'h':
                        case 'o':
                        {
                            var FunctionName = "GetBack";
                            switch (ActionFeature)
                            {
                                case 't': FunctionName = "PutBack"; break;
                                case 'a': FunctionName = "PatchBack"; break;
                                case 'l': FunctionName = "DeleteBack"; break;
                                case 'h': FunctionName = "HeadBack"; break;
                                case 'o': FunctionName = "OptionsBack"; break;
                            }
                            var HtmlEvent = v1;
                            var Path = v2;

                            if (v3)
                            {
                                if (Path == '#')
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + "(event, '', '" + v3 + "')");
                                else
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + "(event, '" + Path + "', '" + v3 + "')");
                            }
                            else
                            {
                                if (Path == '#')
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + "(event)");
                                else
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + "(event, '" + Path + "')");
                            }
                            break;
                        }
                        case 'G':
                        case 'T':
                        case 'A':
                        case 'L':
                        case 'H':
                        case 'O':
                        {
                            var FunctionValue = GetBack;
                            switch (ActionFeature)
                            {
                                case 'T': FunctionValue = PutBack; break;
                                case 'A': FunctionValue = PatchBack; break;
                                case 'L': FunctionValue = DeleteBack; break;
                                case 'H': FunctionValue = HeadBack; break;
                                case 'O': FunctionValue = OptionsBack; break;
                            }
                            var HtmlEvent = v1;
                            var Path = v2;

                            if (v3)
                            {
                                if (Path == '#')
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, ["", v3]);
                                else
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, [Path, v3]);
                            }
                            else
                            {
                                if (Path == '#')
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, []);
                                else
                                    await cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, [Path]);
                            }
                            break;
                        }
                        case 'b':
                        case 'B':
                        {
                            var event = v1;
                            var index = v2;
                            var outputPlace = v3;
                            if (ActionFeature == 'b')
                                cb_AddEvent(CurrentElement, event, `CommentBack(event, '${index}', '${outputPlace}')`);
                            else
                                await cb_AddEventListener(CurrentElement, event, CommentBack, [index, outputPlace]);
                            break;
                        }
                        case 'y':
                        case 'Y':
                        {
                            var event = v1;
                            var wasmLanguage = v2;
                            var wasmUrl = v3;
                            var funcName = v4;
                            var args = v5;
                            outputPlace = v6;
                            var argsString = "";
                            var argsForListener = "";
                            if (args)
                            {
                                // Delete Bracket
                                args = args.substring(1);

                                argsForListener = args.split(US);

                                args = await cb_SetDynamicValueInlineMap(evt ,args, US);
                                
                                argsString = args.Replace(US, ',');
                            }
                            if (ActionFeature == 'y')
                                cb_AddEvent(CurrentElement, event, `WasmBack(event, '${wasmLanguage}', '${wasmUrl}', '${funcName}', [${argsString}], '${outputPlace}')`);
                            else
                                await cb_AddEventListener(CurrentElement, event, WasmBack, [wasmLanguage, wasmUrl, funcName, argsForListener, outputPlace]);
                            break;
                        }
                        case 'w': cb_AddEvent(CurrentElement, v1, "WebSocketBack(event, '" + v2 + "')"); break;
                        case 'W': await cb_AddEventListener(CurrentElement, v1, WebSocketBack, [v2]); break;
                        case 'e':
                        case 'E':
                        {
                            var htmlEvent = v1;
                            var path = v2;
                            var shouldReconnect = v3 == '1';
                            var reconnectTryTimeout = v4;
                            var outputPlace = v5;

                            if (ActionFeature == 'e')
                                cb_AddEvent(CurrentElement, htmlEvent, `SSEBack(event, '${path}', ${shouldReconnect}, ${reconnectTryTimeout}` + (outputPlace ? ", '" + outputPlace + "')" : ')'));
                            else
                                await cb_AddEventListener(CurrentElement, htmlEvent, SSEBack, [path, shouldReconnect, reconnectTryTimeout, outputPlace]);
                            break;
                        }
                        case 'j':
                        case 'J':
                        {
                            var htmlEvent = v1;
                            var modulePath = v2;
                            var outputPlace = v3;
                            var argsString;
                            var argsForListener = "";
                            if (v4)
                            {
                                var args = v4;

                                // Delete Bracket
                                args = args.substring(1);

                                argsForListener = args.split(US);

                                args = await cb_SetDynamicValueInlineMap(evt ,args, US);
                                
                                argsString = args.Replace(US, ',');

                                outputPlace = v3;
                            }

                            if (ActionFeature == 'j')
                                cb_AddEvent(CurrentElement, htmlEvent, `FrontBack(event, '${modulePath}', '${outputPlace}'` + (argsString ? ', ' + argsString : "") + ')');
                            else
                            {
                                if (!outputPlace)
                                    outputPlace = "";

                                await cb_AddEventListener(CurrentElement, htmlEvent, FrontBack, [modulePath, outputPlace, ...argsForListener]);
                            }
                            break;
                        }
                        case 'u':
                        case 'U':
                        {
                            var [event, outputPlace] = [v1, v2];
                            if (ActionFeature == 'u')
                                cb_AddEvent(CurrentElement, event, "cb_MasterPages(event" + (outputPlace ? ", '" + outputPlace + "'" : "") + ")");
                            else
                                await cb_AddEventListener(CurrentElement, event, cb_MasterPages, [outputPlace]);
                            break;
                        }
                        case 'n':
                        case 'N':
                        {
                            var htmlEvent = v1;
                            var data = v2;
                            var path = v3;
                            var method = v4;
                            var isMultiPart = (v5 == '1') ? "true" : "false";
                            var contentType = v6;
                            var outputPlace = v7;

                            if (ActionFeature == 'n')
                                cb_AddEvent(CurrentElement, htmlEvent, `SendBack(event, '${outputPlace}', '${path}', '${method}', ${isMultiPart}, '${contentType}', '${data}')`);
                            else
                                await cb_AddEventListener(CurrentElement, htmlEvent, SendBack, [outputPlace, path, method, isMultiPart == "true", contentType, data]);
                            break;
                        }
                        case 'd': cb_AddEvent(CurrentElement, v1, "PreventDefault(event)"); break;
                        case 'D': await cb_AddEventListener(CurrentElement, v1, PreventDefault); break;
                        case 's': cb_AddEvent(CurrentElement, v1, "StopPropagation(event)"); break;
                        case 'S': await cb_AddEventListener(CurrentElement, v1, StopPropagation); break;
                        case 'm':
                        case 'M':
                        case 'x':
                        case 'X':
                        {
                            var eventName = v1;
                            var funcName = v2;
                            var argsString = "";
                            var argsForListener = "";
                            if (v3)
                            {
                                var args = v3;

                                // Delete Bracket
                                args = args.substring(1);

                                argsForListener = args.split(US);

                                args = await cb_SetDynamicValueInlineMap(evt ,args, US);

                                argsString = args.Replace(US, ',');
                            }
                            if (ActionFeature == 'm' || ActionFeature == 'M')
                            {
                                if (WebFormsOptions.DisableCallMethod)
                                {
                                    if (WebFormsOptions.AddConsoleMessage)
                                        console.warn("Access to the call method is disabled but is being attempted.\nMethod: " + funcName);
                                    break;
                                }

                                if (WebFormsOptions.UseCallMethodOnlyInAcceptedList)
                                    if (!WebFormsOptions.CallMethodOnlyInAcceptedList.some(p => cb_MatchesPattern(p, funcName)))
                                    {
                                        if (WebFormsOptions.AddConsoleMessage)
                                            console.warn("Access to call method is only possible in the list, but is being attempted.\nMethod: " + funcName);
                                        break;
                                    }

                            }
                            if (ActionFeature == 'm')
                                cb_AddEvent(CurrentElement, eventName, `cb_GetMethod('${funcName}')(${argsString})`);
                            else if (ActionFeature == 'M')
                                await cb_AddEventListener(CurrentElement, eventName, cb_GetMethod(funcName), argsForListener, "method");
                            else if (ActionFeature == 'x')
                                cb_AddEvent(CurrentElement, eventName, `cb_GetModuleMethod('${funcName}')(${argsString})`);
                            else
                                await cb_AddEventListener(CurrentElement, eventName, cb_GetModuleMethod(funcName), argsForListener, "method");
                            break;
                        }
                        case 'f':
                        {
                            var [text, type, title, okText, cancelText] = [v2, v3, v4, v5, v6];

                            if (!text)
                                text = "Are you sure you want to proceed?";
                            if (!type)
                                type = "none";
                            if (!title)
                                title = "Confirm";
                            if (!okText)
                                okText = "OK";
                            if (!cancelText)
                                cancelText = "Cancel";
                        
                            var CurrentEvent = v1;

                            if (!CurrentElement.hasAttribute(CurrentEvent))
                                break;

                            var CurrentAttributeValue = CurrentElement.getAttribute(CurrentEvent);

                            CurrentAttributeValue = "cb_ShowConfirm('" + text + "', '" + type + "', '" + title + "', '" + okText + "', '" + cancelText + "').then(() => {cb_ConfirmIsAccept = undefined;" + CurrentAttributeValue + "}).catch(() => { });";

                            CurrentElement.setAttribute(CurrentEvent, CurrentAttributeValue);

                            break;
                        }
                    }
                    break;

                case 'R':
                    switch (ActionFeature)
                    {
                        case 'p': cb_RemoveEvent(CurrentElement, v1, "PostBack"); break;
                        case 'g': cb_RemoveEvent(CurrentElement, v1, "GetBack"); break;
                        case 't': cb_RemoveEvent(CurrentElement, v1, "PutBack"); break;
                        case 'a': cb_RemoveEvent(CurrentElement, v1, "PatchBack"); break;
                        case 'l': cb_RemoveEvent(CurrentElement, v1, "DeleteBack"); break;
                        case 'h': cb_RemoveEvent(CurrentElement, v1, "HeadBack"); break;
                        case 'o': cb_RemoveEvent(CurrentElement, v1, "OptionsBack"); break;
                        case 'b': cb_RemoveEvent(CurrentElement, v1, "CommentBack"); break;
                        case 'y': cb_RemoveEvent(CurrentElement, v1, "WasmBack"); break;
                        case 'w': cb_RemoveEvent(CurrentElement, v1, "WebSocketBack"); break;
                        case 'e': cb_RemoveEvent(CurrentElement, v1, "SSEBack"); break;
                        case 'j': cb_RemoveEvent(CurrentElement, v1, "FrontBack"); break;
                        case 'n': cb_RemoveEvent(CurrentElement, v1, "SendBack"); break;
                        case 'u': cb_RemoveEvent(CurrentElement, v1, "cb_MasterPages"); break;
                        case 'd': cb_RemoveEvent(CurrentElement, v1, "PreventDefault"); break;
                        case 's': cb_RemoveEvent(CurrentElement, v1, "StopPropagation"); break;
                        case 'm': cb_RemoveEvent(CurrentElement, v1, `cb_GetMethod('${v2}')`); break;
                        case 'x': cb_RemoveEvent(CurrentElement, v1, `cb_GetModuleMethod('${v2}')`); break;
                        case 'f':
                        {
                            var CurrentAttributeValue = CurrentElement.getAttribute(v1);

                            if (CurrentAttributeValue)
                            {
                                CurrentAttributeValue = CurrentAttributeValue.replace(/cb_ShowConfirm\(.*?\)\.then\(\s*?\(\)\s*?=>\s*?{/, "");
                                CurrentAttributeValue = CurrentAttributeValue.replace(/}\)\.catch\(\(\)\s*?=>\s*?{ }\);/, "");

                                CurrentElement.setAttribute(v1, CurrentAttributeValue.trim());
                            }
                            break;
                        }
                        case 'P': cb_RemoveEventListener(CurrentElement, v1, PostBack); break;
                        case 'G': cb_RemoveEventListener(CurrentElement, v1, GetBack); break;
                        case 'T': cb_RemoveEventListener(CurrentElement, v1, PutBack); break;
                        case 'A': cb_RemoveEventListener(CurrentElement, v1, PatchBack); break;
                        case 'L': cb_RemoveEventListener(CurrentElement, v1, DeleteBack); break;
                        case 'H': cb_RemoveEventListener(CurrentElement, v1, HeadBack); break;
                        case 'O': cb_RemoveEventListener(CurrentElement, v1, OptionsBack); break;
                        case 'B': cb_RemoveEventListener(CurrentElement, v1, CommentBack); break;
                        case 'Y': cb_RemoveEventListener(CurrentElement, v1, WasmBack); break;
                        case 'W': cb_RemoveEventListener(CurrentElement, v1, WebSocketBack); break;
                        case 'E': cb_RemoveEventListener(CurrentElement, v1, SSEBack); break;
                        case 'J': cb_RemoveEventListener(CurrentElement, v1, FrontBack); break;
                        case 'N': cb_RemoveEventListener(CurrentElement, v1, SendBack); break;
                        case 'U': cb_RemoveEventListener(CurrentElement, v1, cb_MasterPages); break;
                        case 'D': cb_RemoveEventListener(CurrentElement, v1, PreventDefault); break;
                        case 'S': cb_RemoveEventListener(CurrentElement, v1, StopPropagation); break;
                        case 'M': cb_RemoveEventListener(CurrentElement, v1, window[v2]); break;
                        case 'X': cb_RemoveEventListener(CurrentElement, v1, cb_GetModuleMethod(v2)); break;
                    }
                    break;

                case 'T':
                    switch (ActionFeature)
                    {
                        case 'E': cb_TriggerEvent(CurrentElement, v2, v1); break;
                        case 'j': cb_BindToTemplate(v1, "json", v2, CurrentElement, v3, v4); break;
                        case 'x': cb_BindToTemplate(v1, "xml", v2, CurrentElement, v3, v4); break;
                        case 'i': cb_BindToTemplate(v1, "ini", v2, CurrentElement, v3, v4);
                    }
                    break;

                case 'u':
                    switch (ActionFeature)
                    {
                        case 'o': cb_UseOnlyChangeUpdate(CurrentElement); break;
                        case 'w': CurrentElement.setAttribute("usewebsocket", "true");
                    }
                    break;

                case 'e':
                    switch (ActionFeature)
                    {
                        case 'C':
                        {
                            var eventName = v1;
                            var watch = v2;
                            var key = v3;
                            var compare = v4;
                            var tmpValue = v5;
                            var range = String(v6);
                            var immediate = v7;
                            var delay = v8;
                            var rangeFrom = "";
                            var rangeTo = "";
                            immediate = immediate == '1';
                            if (range)
                            {
                                rangeFrom = range.GetTextBefore(',');
                                rangeTo = range.GetTextAfter(',');
                            }

                            cb_CreateCustomDOMEvent(CurrentElement, eventName, watch, key, compare, tmpValue, [rangeFrom, rangeTo], immediate, delay);
                            break;
                        }
                        case 'r': cb_EnableReachedElementEvent(CurrentElement, v1 == '1', v2 == '1'); 
                    }
            }

            switch (ActionOperation + ActionFeature)
            {
                case "sw": CurrentElement.style.width = v1; break;
                case "sh": CurrentElement.style.height = v1; break;
                case "bc": CurrentElement.style.backgroundColor = v1; break;
                case "tc": CurrentElement.style.color = v1; break;
                case "fn": CurrentElement.style.fontFamily = v1; break;
                case "fs": CurrentElement.style.fontSize = v1; break;
                case "fb": CurrentElement.style.fontWeight = (v1 == '1') ? "bold" : "unset"; break;
                case "vi": CurrentElement.style.visibility = (v1 == '1') ? "visible" : "hidden"; break;
                case "ta": CurrentElement.style.textAlign = v1; break;
                case "sr": (v1 == '1') ? CurrentElement.setAttribute("readonly", "") : CurrentElement.removeAttribute("readonly"); break;
                case "sd": (v1 == '1') ? CurrentElement.setAttribute("disabled", "") : CurrentElement.removeAttribute("disabled"); break;
                case "sf": (v1 == '1') ? CurrentElement.focus() : CurrentElement.blur(); break;
                case "mn": CurrentElement.setAttribute("minlength", v1); break;
                case "mx": CurrentElement.setAttribute("maxlength", v1); break;
                case "ts": CurrentElement.value = v1; break;
                case "ti":
                {
                    var SelectedIndex = parseInt(v1);
                    if (SelectedIndex >= 0)
                        CurrentElement.selectedIndex = SelectedIndex;
                    else
                        CurrentElement.selectedIndex = (CurrentElement.getElementsByTagName("option").length + SelectedIndex);
                    break;
                }
                case "ks":
                {
                    var CheckBoxValue = v1;
                    var CheckBoxChecked = v2;
                    var CheckBoxTagLength = CurrentElement.querySelectorAll('input[type="checkbox"][value="' + CheckBoxValue + '"]').length;
                    if (CheckBoxTagLength > 0)
                        CurrentElement.querySelectorAll('input[type="checkbox"][value="' + CheckBoxValue + '"]')[0].checked = (CheckBoxChecked == '1');
                    break;
                }
                case "ki":
                {
                    var CheckBoxIndex = parseInt(v1);
                    var CheckBoxChecked = v2;
                    var CheckBoxTags = CurrentElement.querySelectorAll('input[type="checkbox"]');
                    var CheckBoxTag = (CheckBoxIndex >= 0) ? CheckBoxTags[CheckBoxIndex] : CheckBoxTags[CheckBoxTags.length + CheckBoxIndex];
                    if (CheckBoxTag)
                        CheckBoxTag.checked = (CheckBoxChecked == '1');
                    break;
                }
                case "nt":
                    if (v2)
                    {
                        var TagName = v1;
                        var TagId = v2;
                        var TmpTag = document.createElement(TagName);
                        TmpTag.id = TagId;
                        CurrentElement.appendChild(TmpTag);
                    }
                    else
                        CurrentElement.appendChild(document.createElement(v1));
                    break;
                case "ut":
                    if (v2)
                    {
                        var TagName = v1;
                        var TagId = v2;
                        var TmpTag = document.createElement(TagName);
                        TmpTag.id = TagId;
                        CurrentElement.prepend(TmpTag);
                    }
                    else
                        CurrentElement.prepend(document.createElement(v1));
                    break;
                case "bt":
                    if (v2)
                    {
                        var TagName = v1;
                        var TagId = v2;
                        var TmpTag = document.createElement(TagName);
                        TmpTag.id = TagId;
                        CurrentElement.insertAdjacentElement("beforebegin", TmpTag);
                    }
                    else
                        CurrentElement.insertAdjacentElement("beforebegin", document.createElement(v1));
                    break;
                case "ft":
                    if (v2)
                    {
                        var TagName = v1;
                        var TagId = v2;
                        var TmpTag = document.createElement(TagName);
                        TmpTag.id = TagId;
                        CurrentElement.insertAdjacentElement("afterend", TmpTag);
                    }
                    else
                        CurrentElement.insertAdjacentElement("afterend", document.createElement(v1));
                    break;
                case "pt":
                {
                    var tmpValue = String(v1);
                    if (tmpValue.HasTag())
                    {
                        CurrentElement.insertAdjacentHTML("afterbegin", tmpValue.toDOM());
                        cb_AppendJavaScriptTag(cb_RemoveScripts(tmpValue));
                        cb_Initialization(CurrentElement);
                    }
                    else
                        CurrentElement.insertAdjacentHTML("afterbegin", tmpValue);
                    break;
                }
                case "lu": cb_RequestAndResponse(evt, v1, ElementPlace, "GET"); break;
                case "sp":
                {
                    var OutputPlace = cb_GetElementByElementPlace(v1);
                    const placeHolder = document.createElement("div");
                    CurrentElement.parentNode.insertBefore(placeHolder, CurrentElement);
                    OutputPlace.replaceWith(CurrentElement);
                    placeHolder.replaceWith(OutputPlace);
                    break;
                }
                case "sR": await cb_SetReflection(CurrentElement, v1); break;
                case "iR": await cb_SetReflection(CurrentElement, cb_GetElementByElementPlace(v1)); break
                case "sM": await cb_SetMorph(CurrentElement, v1); break;
                case "iM": await cb_SetMorph(CurrentElement, cb_GetElementByElementPlace(v1)); break;
                case "At": cb_AssertEqual(CurrentElement, v1.Replace("$[ln];", "\n")); break;
                case "Ao": cb_AssertEqual(CurrentElement, cb_GetElementByElementPlace(v1)); 
            }

            // Extension
            await cb_SetValueToInputExtension(evt, ActionOperation, ActionFeature, CurrentElement, Value, vArgs);
        }
        catch (er)
        {
            if (WebFormsOptions.AddConsoleMessage)
                console.warn("There was a problem in set value to input whene executing the command: " + er + "\nError in command: " + CurrentElement + (WebFormsOptions.UseConsoleStackTrace ? "\n" + er.stack : ""));

            if (WebFormsOptions.AddMessageForProblemInSetValueToInput)
                cb_ShowMessage(WebFormsOptions.ProblemInSetValueToInputLang, "problem", WebFormsOptions.MessageDuration);
        }
    }

    return ElementPlaceList;
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
    const element = cb_FetchElementByElementPlace(ElementPlace, obj, TransientDOM);

    if (element.tagName)
        if (element.tagName.toLowerCase() == "template")
        {
            const htmlString = element.innerHTML;

            const tmp = document.createElement("div");
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
        if (ElementPlace.substring(0, 1) != '>')
            if (ElementPlace.Contains('|'))
                ElementPlace = '>' + ElementPlace;

        let DirectChildOnly = false;

        if (ElementPlace.length > 1 && ElementPlace[0] == '.')
        {
            DirectChildOnly = true;
            ElementPlace = ElementPlace.substring(1);
        }

        const ElementPlaceFirstChar = ElementPlace.substring(0, 1);

        const CurrentDocument = TransientDOM ?? document;
        const FromPlace = obj ? obj : CurrentDocument;

        var criteria = "";

        switch (ElementPlaceFirstChar)
        {
            case '<':
            {
                if (FromPlace instanceof NodeList || FromPlace instanceof HTMLCollection || Array.isArray(FromPlace))
                    return cb_MapElementPlace(ElementPlace, FromPlace);

                if (ElementPlace.Contains('?'))
                {
                    criteria = '?' + ElementPlace.GetTextAfter('?');
                    ElementPlace = ElementPlace.GetTextBefore('?');
                }

                const TagName = ElementPlace.substring(1).GetTextBefore('>');
                let TagIndex = 0;

                if (ElementPlace.length > (TagName.length + 2))
                {
                    TagIndex = ElementPlace.substring(TagName.length + 2);

                    if (TagIndex != '*')
                        TagIndex = parseInt(TagIndex);
                }

                let TagList;

                if (DirectChildOnly)
                {
                    TagList = [];

                    for (const child of FromPlace.children)
                        if (TagName.length == 0 || child.tagName.toLowerCase() == TagName.toLowerCase())
                            TagList.push(child);
                }
                else
                {
                    if (TagName.length > 0)
                        TagList = FromPlace.getElementsByTagName(TagName);
                    else
                        TagList = FromPlace.children;
                }

                if (TagIndex == '*')
                    return cb_ElementPlaceCriteria(TagList, criteria);
                else if (TagIndex >= 0)
                    return cb_ElementPlaceCriteria(TagList[TagIndex], criteria);
                else
                    return cb_ElementPlaceCriteria(TagList[TagList.length + TagIndex], criteria);
            }
            case '(':
            {
                if (FromPlace instanceof NodeList || FromPlace instanceof HTMLCollection || Array.isArray(FromPlace))
                    return cb_MapElementPlace(ElementPlace, FromPlace);

                if (ElementPlace.Contains('?'))
                {
                    criteria = '?' + ElementPlace.GetTextAfter('?');
                    ElementPlace = ElementPlace.GetTextBefore('?');
                }

                const TagNameAttr = ElementPlace.substring(1).GetTextBefore(')');
                let TagNameIndex = 0;

                if (ElementPlace.length > (TagNameAttr.length + 2))
                {
                    TagNameIndex = ElementPlace.substring(TagNameAttr.length + 2);

                    if (TagNameIndex != '*')
                        TagNameIndex = parseInt(TagNameIndex);
                }

                let NameList;

                if (DirectChildOnly)
                {
                    NameList = [];

                    for (const child of FromPlace.children)
                        if (child.getAttribute("name") == TagNameAttr)
                            NameList.push(child);
                }
                else
                {
                    NameList = FromPlace.getElementsByName(TagNameAttr);
                }

                if (TagNameIndex == '*')
                    return cb_ElementPlaceCriteria(NameList, criteria);
                else if (TagNameIndex >= 0)
                    return cb_ElementPlaceCriteria(NameList[TagNameIndex], criteria);
                else
                    return cb_ElementPlaceCriteria(NameList[NameList.length + TagNameIndex], criteria);
            }
            case '{':
            {
                if (FromPlace instanceof NodeList || FromPlace instanceof HTMLCollection || Array.isArray(FromPlace))
                    return cb_MapElementPlace(ElementPlace, FromPlace);

                if (ElementPlace.Contains('?'))
                {
                    criteria = '?' + ElementPlace.GetTextAfter('?');
                    ElementPlace = ElementPlace.GetTextBefore('?');
                }

                const ClassName = ElementPlace.substring(1).GetTextBefore('}');
                let ClassIndex = 0;

                if (ElementPlace.length > (ClassName.length + 2))
                {
                    ClassIndex = ElementPlace.substring(ClassName.length + 2);

                    if (ClassIndex != '*')
                        ClassIndex = parseInt(ClassIndex);
                }

                let ClassList;

                if (DirectChildOnly)
                {
                    ClassList = [];

                    for (const child of FromPlace.children)
                        if (child.classList.contains(ClassName))
                            ClassList.push(child);
                }
                else
                {
                    ClassList = FromPlace.getElementsByClassName(ClassName);
                }

                if (ClassIndex == '*')
                    return cb_ElementPlaceCriteria(ClassList, criteria);
                else if (ClassIndex >= 0)
                    return cb_ElementPlaceCriteria(ClassList[ClassIndex], criteria);
                else
                    return cb_ElementPlaceCriteria(ClassList[ClassList.length + ClassIndex], criteria);
            }
            case '"':
            {
                if (FromPlace instanceof NodeList || FromPlace instanceof HTMLCollection || Array.isArray(FromPlace))
                    return cb_MapElementPlace(ElementPlace, FromPlace);

                if (ElementPlace.Contains('?'))
                {
                    criteria = '?' + ElementPlace.GetTextAfter('?');
                    ElementPlace = ElementPlace.GetTextBefore('?');
                }

                const Attribute = ElementPlace.substring(1).GetTextBefore('"');
                let [AttributeName, AttributeValue] = Attribute.split('\'');

                let AttributeOperator = '=';

                const LastChar = AttributeName.substring(AttributeName.length - 1);

                if (['^', '$', '*', '~'].includes(LastChar))
                {
                    AttributeOperator = LastChar;
                    AttributeName = AttributeName.substring(0, AttributeName.length - 1);
                }

                let AttributeIndex = 0;

                if (ElementPlace.length > (Attribute.length + 2))
                {
                    AttributeIndex = ElementPlace.substring(Attribute.length + 2);

                    if (AttributeIndex != '*')
                        AttributeIndex = parseInt(AttributeIndex);
                }

                let AttributeQuery;

                if (AttributeValue === undefined)
                    AttributeQuery = '[' + AttributeName + ']';
                else
                    AttributeQuery = '[' + AttributeName + AttributeOperator + '"' + AttributeValue + '"]';

                let AttributeList;

                if (DirectChildOnly)
                {
                    AttributeList = [];

                    for (const child of FromPlace.children)
                        if (child.matches(AttributeQuery))
                            AttributeList.push(child);
                }
                else
                {
                    AttributeList = FromPlace.querySelectorAll(AttributeQuery);
                }

                if (AttributeIndex == '*')
                    return cb_ElementPlaceCriteria(AttributeList, criteria);
                else if (AttributeIndex >= 0)
                    return cb_ElementPlaceCriteria(AttributeList[AttributeIndex], criteria);
                else
                    return cb_ElementPlaceCriteria(AttributeList[AttributeList.length + AttributeIndex], criteria);
            }
            case '*':
            {
                if (FromPlace instanceof NodeList || FromPlace instanceof HTMLCollection || Array.isArray(FromPlace))
                    return cb_MapElementPlace(ElementPlace, FromPlace);

                if (ElementPlace.Contains('?'))
                {
                    criteria = '?' + ElementPlace.GetTextAfter('?');
                    ElementPlace = ElementPlace.GetTextBefore('?');
                }

                if (ElementPlace == '*')
                {
                    if (DirectChildOnly)
                        return cb_ElementPlaceCriteria(FromPlace.children, criteria);
                    else
                        return cb_ElementPlaceCriteria(FromPlace.querySelectorAll('*'), criteria);
                }

                const Query = ElementPlace.substring(1).Replace("$[eq];", '=').Replace("$[vb];", '|').Replace("$[qu];", '?');

                if (DirectChildOnly)
                {
                    let Result = [];

                    for (const child of FromPlace.children)
                        if (child.matches(Query))
                            Result.push(child);

                    return cb_ElementPlaceCriteria(Result, criteria);
                }

                return cb_ElementPlaceCriteria(FromPlace.querySelector(Query), criteria);
            }
            case '[':
            {
                if (FromPlace instanceof NodeList || FromPlace instanceof HTMLCollection || Array.isArray(FromPlace))
                    return cb_MapElementPlace(ElementPlace, FromPlace);

                if (ElementPlace.Contains('?'))
                {
                    criteria = '?' + ElementPlace.GetTextAfter('?');
                    ElementPlace = ElementPlace.GetTextBefore('?');
                }

                const QueryAll = ElementPlace.substring(1).Replace("$[eq];", '=').Replace("$[vb];", '|').Replace("$[qu];", '?');

                if (DirectChildOnly)
                {
                    let Result = [];

                    for (const child of FromPlace.children)
                        if (child.matches(QueryAll))
                            Result.push(child);

                    return cb_ElementPlaceCriteria(Result, criteria);
                }

                return cb_ElementPlaceCriteria(FromPlace.querySelectorAll(QueryAll), criteria);
            }
            case '~':
                if (ElementPlace.Contains('?'))
                {
                    criteria = '?' + ElementPlace.GetTextAfter('?');
                    ElementPlace = ElementPlace.GetTextBefore('?');
                }  
            
            return cb_ElementPlaceCriteria(FromPlace, criteria);

            case ',': return document;
            case '`': return window;
            case '.': return document.documentElement;
            case '%': return screen.orientation;
            case '^':
                if (ElementPlace.Contains('?'))
                {
                    criteria = '?' + ElementPlace.GetTextAfter('?');
                    ElementPlace = ElementPlace.GetTextBefore('?');
                }     
            return cb_ElementPlaceCriteria(document.head, criteria);

            case '>':
            {
                const PlaceList = ElementPlace.substring(1).split('|');
                let TmpPlace;

                for (var i = 0; i < PlaceList.length; i++)
                {
                    var TmpElementPlace = PlaceList[i];
                    TmpPlace = (i == 0) ? cb_GetElementByElementPlace(TmpElementPlace, null, TransientDOM) : cb_GetElementByElementPlace(TmpElementPlace, TmpPlace);
                }

                return TmpPlace;
            }
            case '/':
            {
                if (FromPlace instanceof NodeList || FromPlace instanceof HTMLCollection || Array.isArray(FromPlace))
                    return cb_MapElementPlace(ElementPlace, FromPlace);

                if (ElementPlace.Contains('?'))
                {
                    criteria = '?' + ElementPlace.GetTextAfter('?');
                    ElementPlace = ElementPlace.GetTextBefore('?');
                }

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

                while (i > 0 && TmpElementPlace)
                {
                    TmpElementPlace = TmpElementPlace.parentElement;
                    i--;
                }

                if ((ElementPlace.length > 0) && obj)
                    return cb_ElementPlaceCriteria(cb_GetElementByElementPlace(ElementPlace, TmpElementPlace, TransientDOM), criteria);

                return cb_ElementPlaceCriteria(TmpElementPlace, criteria);
            }
            default:
                if (ElementPlace.Contains('?'))
                {
                    criteria = '?' + ElementPlace.GetTextAfter('?');
                    ElementPlace = ElementPlace.GetTextBefore('?');
                }
            
            return cb_ElementPlaceCriteria(FromPlace.getElementById(ElementPlace), criteria);
        }
    }
    catch (er)
    {
        if (WebFormsOptions.AddConsoleMessage)
            console.log("Problem in determining element: " + er + "\nError in input place: " + ElementPlace);

        if (WebFormsOptions.AddMessageForProblemInDeterminingElement)
            cb_ShowMessage(WebFormsOptions.ProblemInDeterminingElementLang, "problem", WebFormsOptions.MessageDuration);
    }
}

function cb_MapElementPlace(ElementPlace, FromPlace)
{
    let Result = [];

    for (const Item of FromPlace)
    {
        const Value = cb_GetElementByElementPlace(ElementPlace, Item);

        if (!Value)
            continue;

        if (Value instanceof NodeList || Value instanceof HTMLCollection || Array.isArray(Value))
            Result.push(...Value);
        else
            Result.push(Value);
    }

    return Result;
}

function cb_ElementPlaceCriteria(element, criteria)
{
    if (!criteria)
        return element;

    // Normalize
    if (element instanceof NodeList || element instanceof HTMLCollection)
        element = [...element];
    else if (!Array.isArray(element))
        element = [element];

    criteria = criteria.Replace("$[vb];", '|').Replace("$[qu];", '?');

    const criterias = criteria.substring(1).split('?');

    for (const item of criterias)
    {
        let criteria = item;

        const isPositive = !criteria.startsWith('!');

        if (!isPositive)
            criteria = criteria.substring(1);

        const firstChar = criteria.substring(0, 1);
        let secondChar = criteria.substring(1, 2);

        switch (firstChar)
        {
            case 'T': // All Inner Text
            case 't': // Inner Text
            case 'a': // Attribute Value
            case 'g': // Tag Name
            {
                let value = criteria.substring(2);

                element = element.filter(function (e)
                {
                    let tmpValue = value;
                    let text = "";
                    let result = false;

                    switch (firstChar)
                    {
                        case 'T':
                            text = e.innerText ?? "";
                            break;

                        case 't':
                        {
                            for (const node of e.childNodes)
                            {
                                if (node.nodeType === Node.TEXT_NODE)
                                    text += node.textContent;
                            }
                            text = text.trim();
                            break;
                        }
                        case 'a':
                            text = e.hasAttribute(tmpValue.GetTextBefore('"')) ? e.getAttribute(tmpValue.GetTextBefore('"')) : "";
                            tmpValue = tmpValue.GetTextAfter('"');
                            secondChar = tmpValue.substring(0, 1);
                            tmpValue = tmpValue.substring(1);
                            break;

                        case 'g':
                            text = e.tagName.toLowerCase();
                            tmpValue = tmpValue.toLowerCase();
                            break;
                    }

                    switch (secondChar)
                    {
                        case ':':
                            result = (text == tmpValue);
                            break;

                        case '^':
                            result = text.startsWith(tmpValue);
                            break;

                        case '$':
                            result = text.endsWith(tmpValue);
                            break;

                        case '*':
                            result = text.includes(tmpValue);
                            break;

                        case '~':
                            result = text.split(/\s+/).includes(tmpValue);
                            break;

                        case '>':
                        {
                            const hasEqual = tmpValue.startsWith(':');

                            if (hasEqual)
                                tmpValue = tmpValue.substring(1);

                            const n1 = parseFloat(text);
                            const n2 = parseFloat(tmpValue);

                            if (!isNaN(n1) && !isNaN(n2))
                                result = hasEqual ? n1 >= n2 : n1 > n2;
                            else
                                result = hasEqual ? text >= tmpValue : text > tmpValue;

                            break;
                        }

                        case '<':
                        {
                            const hasEqual = tmpValue.startsWith(':');

                            if (hasEqual)
                                tmpValue = tmpValue.substring(1);

                            const n1 = parseFloat(text);
                            const n2 = parseFloat(tmpValue);

                            if (!isNaN(n1) && !isNaN(n2))
                                result = hasEqual ? n1 <= n2 : n1 < n2;
                            else
                                result = hasEqual ? text <= tmpValue : text < tmpValue;

                            break;
                        }
                    }

                    return isPositive ? result : !result;
                });

                break;
            }

            case 'v': // Visible
            {
                element = element.filter(function (e)
                {
                    const result = e.offsetWidth > 0 || e.offsetHeight > 0 || e.getClientRects().length > 0;

                    return isPositive ? result : !result;
                });

                break;
            }

            case 'e': // Enabled
            {
                element = element.filter(function (e)
                {
                    const result = !e.disabled;

                    return isPositive ? result : !result;
                });

                break;
            }

            case 'c': // Checked
            {
                element = element.filter(function (e)
                {
                    const result = e.checked;

                    return isPositive ? result : !result;
                });

                break;
            }

            case 'H': // Has Descendant
            case 'h': // Has Direct Child
            {
                const elementPlace = criteria.substring(1);

                element = element.filter(function (e)
                {
                    let result = false;

                    if (firstChar == 'h')
                    {
                        // Direct Child
                        if (elementPlace)
                        {
                            for (const child of e.children)
                            {
                                const tmp = cb_FetchElementByElementPlace(elementPlace, child.parentElement);

                                if (!tmp)
                                    continue;

                                if (tmp instanceof NodeList || tmp instanceof HTMLCollection || Array.isArray(tmp))
                                {
                                    if ([...tmp].includes(child))
                                    {
                                        result = true;
                                        break;
                                    }
                                }
                                else if (tmp === child)
                                {
                                    result = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            result = e.children.length > 0;
                        }
                    }
                    else
                    {
                        // All Descendants
                        if (elementPlace)
                            result = cb_FetchElementByElementPlace(elementPlace, e);
                        else
                            result = cb_FetchElementByElementPlace('*', e);

                        result = cb_CountElements(result) > 0;
                    }

                    return isPositive ? result : !result;
                });

                break;
            }

            case '[': // Range
            {
                const range = criteria.GetTextBefore(']');
                const parts = range.substring(1).split(':');

                let start = (parts[0] == "") ? 0 : parseInt(parts[0]);
                let end = (parts[1] == "") ? element.length - 1 : parseInt(parts[1]);

                if (start < 0)
                    start = element.length + start;

                if (end < 0)
                    end = element.length + end;

                element = element.filter(function (_, index)
                {
                    const result = index >= start && index <= end;

                    return isPositive ? result : !result;
                });

                break;
            }

            case '+': // Next Element Sibling
            {
                let result = [];

                if (criteria.substring(1, 2) == '+')
                {
                    for (const e of element)
                    {
                        let tmp = e.nextElementSibling;

                        while (tmp)
                        {
                            result.push(tmp);
                            tmp = tmp.nextElementSibling;
                        }
                    }
                }
                else if (criteria.substring(1).IsNumber())
                {
                    const index = parseInt(criteria.substring(1));

                    for (const e of element)
                    {
                        let tmp = e;

                        for (let i = 0; i < index && tmp; i++)
                            tmp = tmp.nextElementSibling;

                        if (tmp)
                            result.push(tmp);
                    }
                }
                else
                {
                    for (const e of element)
                        if (e.nextElementSibling)
                            result.push(e.nextElementSibling);
                }

                element = result;
                break;
            }

            case '-': // Previous Element Sibling
            {
                let result = [];

                if (criteria.substring(1, 2) == '-')
                {
                    for (const e of element)
                    {
                        let tmp = e.previousElementSibling;

                        while (tmp)
                        {
                            result.push(tmp);
                            tmp = tmp.previousElementSibling;
                        }
                    }
                }
                else if (criteria.substring(1).IsNumber())
                {
                    const index = parseInt(criteria.substring(1));

                    for (const e of element)
                    {
                        let tmp = e;

                        for (let i = 0; i < index && tmp; i++)
                            tmp = tmp.previousElementSibling;

                        if (tmp)
                            result.push(tmp);
                    }
                }
                else
                {
                    for (const e of element)
                        if (e.previousElementSibling)
                            result.push(e.previousElementSibling);
                }

                element = result;
                break;
            }

            case '&': // intersection
            {
                const other = cb_FetchElementByElementPlace(criteria.substring(1));
                const list = cb_CountElements(other) > 1 ? [...other] : [other];

                element = element.filter(e => list.includes(e));
                break;
            }

            case '.': // Union
            {
                const other = cb_FetchElementByElementPlace(criteria.substring(1));
                const list = cb_CountElements(other) > 1 ? [...other] : [other];

                element = [...new Set([...element, ...list])];
                break;
            }

            case '\\': // Difference
            {
                const other = cb_FetchElementByElementPlace(criteria.substring(1));
                const list = cb_CountElements(other) > 1 ? [...other] : [other];

                element = element.filter(e => !list.includes(e));
                break;
            }
        }
    }

    if (element.length == 0)
        return null;

    if (element.length == 1)
        return element[0];

    return element;
}

function cb_CountElements(value)
{
    if (value == null)
        return 0;

    if (Array.isArray(value))
        return value.length;

    if (value instanceof NodeList)
        return value.length;

    if (value instanceof HTMLCollection)
        return value.length;

    return 1;
}

function cb_GetResponseLocation()
{
    return cb_GetElementByElementPlace(WebFormsOptions.ResponseLocation);
}

function cb_GetStateBodyLocation()
{
    return cb_GetElementByElementPlace(WebFormsOptions.StateBodyLocation);
}

async function cb_FetchValue(evt, Value)
{
    try
    {
        if (Value.substring(0, 1) == '$')
        {
            Value = Value.substring(1);

            if (Value.substring(1) == '$')
                return Value;

            if (WebFormsOptions.DisablePassObject)
            {
                if (WebFormsOptions.AddConsoleMessage)
                    console.warn("Access to the pass object is disabled but is being attempted.\nvalue:$" + Value);

                return '$' + Value;
            }

            if (Value.substring(0, 1) == '@')
                return '$' + Value;

            let value = globalThis[Value];

            if (value !== undefined)
            {
                if (typeof value === "function")
                    return value();

                return value;
            }

            const parts = Value.split('.');

            if (parts.length > 1)
            {
                value = globalThis[parts[0]];

                if (value !== undefined)
                {
                    for (let i = 1; i < parts.length; i++)
                    {
                        if (value == null)
                            break;

                        value = value[parts[i]];
                    }

                    if (value !== undefined)
                    {
                        if (typeof value === "function")
                            return value();

                        return value;
                    }
                }
            }

            if (!WebFormsOptions.DisableEval)
                return eval(Value);
            else
            {
                if (WebFormsOptions.AddConsoleMessage)
                    console.warn("Access to the eval method is disabled but is being attempted in Fetch.\nScript value:" + Value);

                return value;
            }
        }

        Value = Value.substring(1);
      
        if (!Value)
            return Value;

        if (Value.substring(0, 1) == ';')
        {
            Value = Value.substring(1);
            let searchValue = Value.GetTextBefore(FS);
            Value = Value.GetTextAfter(FS);
            let searchTo = Value.GetTextBefore(FS);
            Value = Value.GetTextAfter(FS);
            searchValue = await cb_SetDynamicForValue(evt, searchValue);
            searchTo = await cb_SetDynamicForValue(evt, searchTo);

            Value = Value.Replace(searchValue, searchTo);
        }

        const ActionOperation = Value.substring(0, 1);

        if (ActionOperation == '@')
            return Value;

        if (ActionOperation == ':')
        {
            Value = Value.substring(1);
            return await cb_ReplaceInjectValue(evt, Value);
        }

        if (ActionOperation == '_')
        {
            var ScriptValue = Value.substring(1).Replace("$[ln];", "\n").FullTrim();

            if (WebFormsOptions.DisableEval)
            {
                if (WebFormsOptions.AddConsoleMessage)
                    console.warn("Access to the eval method is disabled but is being attempted.\nScript value:" + ScriptValue);
                return "";
            }

            return eval(ScriptValue);
        }

        const ActionFeature = Value.substring(1, 2);
        Value = Value.substring(2);

        switch (ActionOperation)
        {
            case 'm':
                switch (ActionFeature)
                {
                    case 'r':
                    {
                        var tmpValue = Value;
                        var MinValue = 0;
                        if (tmpValue.Contains(RS))
                        {
                            MinValue = Number(tmpValue.GetTextAfter(RS));
                            tmpValue = tmpValue.GetTextBefore(RS);
                        }
                        var MaxValue = Number(tmpValue);
                        return Math.floor(Math.random() * (MaxValue - MinValue)) + MinValue;
                    }
                    case 's': return evt.getModifierState(Value);
                }
                break;

            case 'd':
            {
                var CurrentDate = new Date();
                switch (ActionFeature)
                {
                    case 'y': return CurrentDate.getFullYear();
                    case 'm': return CurrentDate.getMonth() + 1;
                    case 'd': return CurrentDate.getDay() + 1;
                    case 'D': return CurrentDate.getDate();
                    case 'h': return CurrentDate.getHours();
                    case 'i': return CurrentDate.getMinutes();
                    case 's': return CurrentDate.getSeconds();
                    case 'l': return CurrentDate.getMilliseconds();
                    case 'L':
                    if (Value.Contains('['))
                    {
                        var lines = localStorage.getItem(Value.GetTextBefore('[')).split("\n");

                        var index = Number(Value.GetTextAfter('['));

                        if (index < 0)
                            index = lines.length + index;

                        return lines[index];
                    }
                    else
                    {
                        var lines = localStorage.getItem(Value).split("\n");
                        var FirtsLine = lines[0];

                        lines.shift();
                        localStorage.setItem(Value, lines.join('\n'));

                        return FirtsLine;
                    }
                    case 'I':
                    {
                        var lines = localStorage.getItem(Value.GetTextBefore('[')).split("\n");

                        for (var i = 0; i < lines.length; i++)
                            if (lines[i].GetTextBefore('=') == Value.GetTextAfter('['))
                                return lines[i].GetTextAfter('=');
                        break;
                    }
                    case 'a': return document.visibilityState == "visible";
                }
                break;
            }
            case 'c':
                switch (ActionFeature)
                {
                    case 'o': return cb_GetCookie(Value);
                    case 's':
                        if (Value.Contains(RS))
                        {
                            var TmpValue = sessionStorage.getItem(Value.GetTextBefore(RS));
                            sessionStorage.setItem(Value.GetTextBefore(RS), Value.GetTextAfter(RS));
                            return TmpValue;
                        }
                        else
                            return sessionStorage.getItem(Value);
                    case 'l':
                    {
                        var TmpValue = sessionStorage.getItem(Value);
                        sessionStorage.removeItem(Value);
                        return TmpValue;
                    }
                    case 'd':
                        if (Value.Contains(RS))
                        {
                            var TmpValue = localStorage.getItem(Value.GetTextBefore(RS));
                            localStorage.setItem(Value.GetTextBefore(RS), Value.GetTextAfter(RS));
                            return TmpValue;
                        }
                        else
                            return localStorage.getItem(Value);
                    case 't':
                    {
                        var TmpValue = localStorage.getItem(Value);
                        localStorage.removeItem(Value);
                        return TmpValue;
                    }
                    case 'm':
                    case 'M':
                        if (Value.Contains(RS))
                        {
                            var funcName = Value.GetTextBefore(RS);
                            var args = Value.GetTextAfter(RS).split(US);

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
                    case 'g': return sessionStorage.getItem(Value).length;
                    case 'G': return localStorage.getItem(Value).length;
                }
                break;

            case 'l':
                switch (ActionFeature)
                {
                    case 'u':
                    {
                        var url = Value;
                        var fetchScript = false;
                        if (url.Contains(RS))
                        {
                            fetchScript = url.GetTextAfter(RS) == '1';
                            url = url.GetTextBefore(RS);
                        }
                        return await cb_GetUrl(url, fetchScript);
                    }
                    case 'h':
                    {
                        var tmpValue = Value;
                        var url = tmpValue.GetTextBefore(RS);
                        var fetchScript = tmpValue.GetTextAfter(RS);
                        tmpValue = tmpValue.GetTextAfter(RS);
                        var inputPlace;

                        if (tmpValue.Contains(RS))
                        {
                            fetchScript = tmpValue.GetTextBefore(RS);
                            inputPlace = tmpValue.GetTextAfter(RS)
                        }

                        fetchScript = fetchScript == '1';

                        var urlData = await cb_GetUrl(url, fetchScript, false, true);
                        return cb_FetchInputPlace(urlData, inputPlace);
                    }
                    case 'l':
                    {
                        var [url, line] = Value.split(RS);
                        var urlData = await cb_GetUrl(url);
                        return cb_GetTextLine(urlData, line);
                    }
                    case 'i':
                    {
                        var [url, name, isINILike] = Value.split(RS);
                        isINILike = (isINILike == '1');
                        var urlData = await cb_GetUrl(url);
                        return cb_GetINI(urlData, name, isINILike);
                    }
                    case 'j':
                    {
                        var url = Value.GetTextBefore(RS);
                        var name = Value.GetTextAfter(RS);
                        var urlData = await cb_GetUrl(url);
                        return cb_GetJSON(urlData, name);
                    }
                    case 'x':
                    {
                        var url = Value.GetTextBefore(RS);
                        var name = Value.GetTextAfter(RS);
                        var urlData = await cb_GetUrl(url, false, true);
                        return cb_GetXML((new XMLSerializer().serializeToString(urlData)), name);
                    }
                    case 'L':
                    if (Value.Contains('['))
                    {
                        var lines = sessionStorage.getItem(Value.GetTextBefore('[')).split("\n");

                        var index = Number(Value.GetTextAfter('['));

                        if (index < 0)
                            index = lines.length + index;

                        return lines[index];
                    }
                    else
                    {
                        var lines = sessionStorage.getItem(Value).split("\n");
                        var FirtsLine = lines[0];

                        lines.shift();
                        sessionStorage.setItem(Value, lines.join('\n'));

                        return FirtsLine;
                    }
                    case 'I':
                    {
                        var lines = sessionStorage.getItem(Value.GetTextBefore('[')).split("\n");

                        for (var i = 0; i < lines.length; i++)
                            if (lines[i].GetTextBefore('=') == Value.GetTextAfter('['))
                                return lines[i].GetTextAfter('=');
                    }
                }
                break;

            case 'M':
                switch (ActionFeature)
                {
                    case '#':
                        if (Value.Contains(RS))
                        {
                            var funcName = Value.GetTextBefore(RS);
                            var args = Value.GetTextAfter(RS).split(US);

                            args = await cb_SetDynamicValueForArgs(evt, args);

                            return await cb_RunMathMethod(evt, funcName, args);
                        }
                        return await cb_RunMathMethod(evt, Value);
                }
                break;

            case 's':
                switch (ActionFeature)
                {
                    case 'c': return Value.GetTextAfter(RS).Replace(' ', Value.GetTextBefore(RS));
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
                    case 'k': return evt.key ?? "";
                    case 'w': return evt.which ?? "";
                    case 'x': return evt.clientX ?? "";
                    case 'y': return evt.clientY ?? "";
                    case 'X': return evt.pageX ?? "";
                    case 'Y': return evt.pageY ?? "";
                    case 'd': return evt.deltaY ?? "";
                }
                break;

            case 'w':
                switch (ActionFeature)
                {
                    case 'f': return window.location.href;
                    case 'P': return window.location.pathname;
                    case 'q':
                        if (Value == '*')
                            return window.location.search;
                        else
                        {
                            var params = new URLSearchParams(window.location.search);
                            return params.get(Value);
                        }
                    case 'h': return window.location.hash;
                    case 'H': return window.location.host;
                    case 'n': return window.location.hostname;
                    case 'T': return window.location.port;
                    case 'o': return window.location.origin;
                    case 's': return window.getSelection().toString();
                    case 'x': return window.scrollX;
                    case 'y': return window.scrollY;
                    case 'A':
                    {
                        var [wasmLanguage, wasmUrl, funcName, args] = Value.split(RS);
                        
                        if (args)
                            args = args.split(US);

                        args = await cb_SetDynamicValueForArgs(evt, args);

                        return await cb_RunWasmMethodResult(wasmLanguage, wasmUrl, funcName, args);
                    }
                    case 'S':
                    {
                        var segments = window.location.pathname.substring(1).split('/');

                        var index = Value;

                        if (index < 0)
                            index = segments.length + index;

                        if (index < 0 || index >= segments.length)
                            return "";

                        return segments[index];
                    }
                    case 't':
                    {
                        var hash = window.location.hash;
                        hash = hash.substring(1);

                        if (hash.substring(0, 1) == '~')
                            hash = hash.substring(1);
                        else
                            return "";

                        if (hash.substring(0, 1) == '/')
                            hash = hash.substring(1);

                        var segments = hash.split('/');

                        var index = Value;
                        if (index < 0)
                             index =  segments.length + index;

                        return segments[index];
                    }
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
                    try
                    {
                        var coords = await cb_GetGeoPosition();

                        if (ActionFeature == 'W')
                            return coords.latitude ?? "";

                        return coords.longitude ?? "";
                    }
                    catch (er)
                    {
                        if (WebFormsOptions.AddConsoleMessage)
                            console.log("Geolocation error:", er);
                        return "";
                    }

                    case 'C':
                        try
                        {
                            return await navigator.clipboard.readText();
                        }
                        catch (er)
                        {
                            if (WebFormsOptions.AddConsoleMessage)
                                console.log("clipboard error:", er);
                            return "";
                        }
                }
                break;

            case 'E':
                switch (ActionFeature)
                {
                    case 'V': return evt;
                    case 's': return cb_EventSerialize(evt);
                    case 'x': return evt.offsetX ?? "";
                    case 'y': return evt.offsetY ?? "";
                }
                break;

            case 'H':
                switch (ActionFeature)
                {
                    case 'H': return cb_ActionControlHashList.includes(String(Value));
                }
                break;

            case 'S':
                switch (ActionFeature)
                {
                    case 'c': return cb_SSEIsConnected(Value);
                }
                break;

            case 'W':
                switch (ActionFeature)
                {
                    case 'c': return cb_WebSocketIsConnected(Value);
                }
                break;

            case 'h':
                switch (ActionFeature)
                {
                    case 'm': return Value in window;
                    case 'M': return Value in cb_ModuleMethodMap;
                    case 's': return cb_SPA.hasState(Value);
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
                await cb_StorageIsReady;
                switch (ActionFeature)
                {
                    case 'r':
                    case 'v':
                        return cb_StorageGet(Value);
                    case 'x': return cb_GetXML(cb_StorageGet(Value.GetTextBeforeLast(RS)), Value.GetTextAfterLast(RS));
                    case 'j': return cb_GetJSON(cb_StorageGet(Value.GetTextBeforeLast(RS)), Value.GetTextAfterLast(RS));
                    case 'i': return cb_GetINI(cb_StorageGet(Value.GetTextBeforeLast(RS)), Value.GetTextAfterLast(RS));
                    case 't': return cb_GetTextLine(cb_StorageGet(Value.GetTextBeforeLast(RS)), Value.GetTextAfterLast(RS));
                }
                break;

            case '$':
            {
                var tmpValue = Value;
                var elementPlace = tmpValue;

                if (ActionFeature == 'a')
                {
                    elementPlace = tmpValue.GetTextBeforeLast(RS);
                    tmpValue = tmpValue.GetTextAfterLast(RS);
                }

                if (!elementPlace)
                    elementPlace = "<body>";

                var currentElement = cb_GetElement(evt, elementPlace);

                return cb_GetValue(evt, ActionFeature, tmpValue, currentElement);
            }
        }

        // Extension
        return await cb_FetchValueExtension(evt, ActionOperation, ActionFeature, Value);
    }
    catch (er)
    {
        if (WebFormsOptions.AddConsoleMessage)
            console.warn("There was a problem in fetch value whene executing the command: " + er + "\nError in value: " + Value + (WebFormsOptions.UseConsoleStackTrace ? "\n" + er.stack : ""));

        if (WebFormsOptions.AddMessageForProblemInFetchValue)
            cb_ShowMessage(WebFormsOptions.ProblemInFetchValueLang, "problem", WebFormsOptions.MessageDuration);

        return "";
    }
}

async function cb_SaveValue(evt, ActionOperation, ActionFeature, ActionValue, LastElementPlaceList, TransientDOM)
{
    try
    {
        var Name = ActionValue.GetTextAfter('=');
        var ElementPlace = ActionValue.GetTextBefore('=');

        if (!ElementPlace)
            ElementPlace = "<body>";

        var currentElement = cb_GetElement(evt, ElementPlace, LastElementPlaceList, TransientDOM);

        var isCache = (ActionOperation == 'c');

        // Fill Value For Sync Action
        var value;
        var tmpName = Name;
        if (tmpName.Contains(GS))
        {
            value = tmpName.GetTextAfter(GS);
            tmpName = tmpName.GetTextBefore(GS);
        }

        switch (ActionOperation)
        {
            case 'g':
            case 'c':
            {
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
                    if (url.Contains(GS))
                    {
                        fetchScript = url.GetTextAfter(GS) == '1';
                        url = url.GetTextBefore(GS);
                    }
                    var urlData = await cb_GetUrl(url, fetchScript);
                    cb_SetStorage(isCache, Name.GetTextBefore(GS), urlData);
                    return;
                }
            }
        }

        // Extension
        await cb_SaveValueExtension(evt, ActionOperation, ActionFeature, Name, currentElement);
    }
    catch (er)
    {
        if (WebFormsOptions.AddConsoleMessage)
            console.warn("There was a problem in save value whene executing the command: " + er + "\nError in command: " + ActionOperation + ActionFeature + "\nError in value: " + ActionValue + (WebFormsOptions.UseConsoleStackTrace ? "\n" + er.stack : ""));

        if (WebFormsOptions.AddMessageForProblemInSaveValue)
            cb_ShowMessage(WebFormsOptions.ProblemInSaveValueLang, "problem", WebFormsOptions.MessageDuration);
    }
}

function cb_GetValue(evt, action, value, currentElement)
{
    switch (action)
    {
        case 'i': return currentElement.id;
        case 'n': return currentElement.getAttribute("name") ?? "";
        case 'v': return currentElement.value;
        case 'e': return currentElement.value.length.toString();
        case 'c': return currentElement.className;
        case 's': return currentElement.style.cssText;
        case 'l':
            if (currentElement.hasAttribute("title"))
                return currentElement.getAttribute("title");
            return "";
        case 'A':
            if (currentElement.id)
            {
                const labelTag = document.querySelector('label[for="' + currentElement.id + '"]');
                if (labelTag)
                    return labelTag.textContent;
            }
            return "";
        case 't': return currentElement.innerHTML;
        case 'o': return currentElement.outerHTML;
        case 'g': return currentElement.innerHTML.length;
        case 'a': return currentElement.getAttribute(value);
        case 'w': return getComputedStyle(currentElement).width;
        case 'h': return getComputedStyle(currentElement).height;
        case 'r': return (currentElement.hasAttribute("readonly") ? "true" : "false");
        case 'x': return currentElement.selectedIndex.toString();
        case 'I': return Array.from(currentElement.parentElement.children).indexOf(currentElement);
        case 'T': return currentElement.style.textAlign || "left";
        case 'L': return currentElement.childNodes.length;
        case 'V': return ((currentElement.style.visibility == "hidden") ? "false" : "true");
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
        return localStorage.getItem(Name) ?? "";
    else
        return sessionStorage.getItem(Name) ?? "";
}

async function cb_SetDynamicValueInlineMap(evt, Value, Splitter)
{
    let ValueArray = Value.split(Splitter);

    for (let index = 0; index < ValueArray.length; index++)
        if (ValueArray[index].length > 0)
        {
            if (ValueArray[index].startsWith('@') || ValueArray[index].startsWith('$'))
                ValueArray[index] = await cb_FetchValue(evt, ValueArray[index]);
        }

    ValueArray = ValueArray.map(x =>
    {
        // If Value not a string
        if (typeof x !== "string")
            return x;

        if (x.startsWith("$@"))
            return x.substring(2);

        // Already Quoted
        if ((x.startsWith("'") && x.endsWith("'")) || (x.startsWith('"') && x.endsWith('"')) || (x.startsWith('`') && x.endsWith('`')))
            return `'${x.slice(1, -1).Replace("'", "\\'")}'`;

        // Numeric string
        if (/^-?(?:0|[1-9]\d*)(?:\.\d+)?$/.test(x) && x !== "-0")
            return Number(x);

        // String
        return `'${x.Replace("'", "\\'")}'`;
    });

    return ValueArray.join(Splitter);
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
            let fetchValue = await cb_FetchValue(evt, '@' + key);
            if (fetchValue)
                fetchValue = fetchValue.toString();

            return fetchValue ?? "";
        })
    );

    let result = text;
    matches.forEach((m, i) =>
    {
        result = result.replace(m[0], replacements[i]);
    });

    return result;
}

async function cb_SetDynamicValueForArgs(evt, args)
{
    if (args)
        for (let i = 0; i < args.length; i++)
        {
            if (typeof args[i] === "string")
                args[i] = args[i].Replace("$[ln];", '\n');
            
            let tmpValue = await cb_SetDynamicForValue(evt, args[i]);
            args[i] = tmpValue;
        }

    return args;
}

async function cb_SetDynamicForValue(evt, Value)
{
    if (typeof Value === "string")
        if (Value.startsWith('@') || Value.startsWith('$'))
            Value = await cb_FetchValue(evt, Value);

    return cb_ConvertDynamicValue(Value);
}

/* End Execute Web-Forms */

/* Start Cache */

function cb_UsedCache(evt, RequestName, RequestNameForCache)
{
    const SessionCacheValue = sessionStorage.getItem(RequestName);
    if (SessionCacheValue)
    {
        cb_SetWebFormsValues(evt, RequestNameForCache, SessionCacheValue, true, true);
        return true;
    }

    const LocalCacheValue = localStorage.getItem(RequestName);
    if (LocalCacheValue)
    {
        const LocalCacheDateValue = localStorage.getItem(RequestName + "-date");
        if (LocalCacheDateValue)
        {
            const CacheDate = new Date(LocalCacheDateValue);
            const CurrentDate = new Date();

            if (CacheDate.getTime() > CurrentDate.getTime())
            {
                cb_SetWebFormsValues(evt, RequestNameForCache, LocalCacheValue, true, true);
                return true;
            }
            else
            {
                localStorage.removeItem(RequestName);
                localStorage.removeItem(RequestName + "-date");
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

        if (key.endsWith("-date"))
        {
            const expirationDate = new Date(localStorage.getItem(key)).getTime();

            if (now >= expirationDate)
            {
                const originalKey = key.replace("-date", "");
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

function cb_GetUrl(url, fetchScript, isXML, sendPostBackHeader)
{
    return new Promise(function (resolve, reject)
    {
        // Check Accepted URL
        if (!cb_IsInternalUrl(url))
        {
            const tmpURL = new URL(url, window.location.origin);

            if (WebFormsOptions.DisableLoadExternalHost)
            {
                if (WebFormsOptions.AddConsoleMessageForURL)
                    console.warn("Access for load the external host is disabled but is being attempted.\npath: " + url);
                resolve(null);
                return;
            }

            if (WebFormsOptions.UseLoadExternalHostOnlyInAcceptedList)
                if (!WebFormsOptions.LoadExternalHostOnlyInAcceptedList.some(p => cb_MatchesPattern(p, tmpURL.hostname)))
                {
                    if (WebFormsOptions.AddConsoleMessageForURL)
                        console.warn("Access to load the external host is only possible in the list, but is being attempted.\npath: " + url);
                    resolve(null);
                    return;
                }
        }

        const XMLHttp = new XMLHttpRequest();
        XMLHttp.open("GET", url, true);

        XMLHttp.onload = function ()
        {
            if (WebFormsOptions.AddConsoleMessageForHTTP)
                console.log("HTTP request with method GET, path: " + url);

            if (XMLHttp.status === 200)
            {
                const responseText = XMLHttp.responseText;

                if (fetchScript)
                    setTimeout(() => cb_AppendJavaScriptTag(responseText), 500);

                if (isXML)
                {
                    try
                    {
                        const parser = new DOMParser();
                        const xmlDoc = parser.parseFromString(responseText, "application/xml");
                        resolve(xmlDoc);
                    }
                    catch (er)
                    {
                        reject("Failed to parse XML: " + er.message);
                    }
                }
                else
                    resolve(cb_RemoveScripts(responseText));
            }
            else
                reject("HTTP Error: " + XMLHttp.status);
        };

        if (sendPostBackHeader)
            XMLHttp.setRequestHeader("Post-Back", "true");

        XMLHttp.onerror = () => reject("Network Error");
        XMLHttp.send();
    });
}

function cb_ConvertToWebSocketUrl(url)
{
    const currentUrl = window.location.href;
    const protocol = window.location.protocol === "https:" ? "wss:" : "ws:";
    const host = window.location.host;

    if (url.startsWith('?'))
        return `${protocol}//${host}${currentUrl.split(host)[1]}${url}`;

    if (url.startsWith("http://") || url.startsWith("https://"))
        return url.replace(/^http/, "ws");

    if (url.startsWith("ws://") || url.startsWith("wss://"))
        return url;

    if (!url.includes("://"))
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
    let url = formAction;
    const separator = url.includes('?') ? '&' : '?';
    if (formDataSerialize)
        url += separator + formDataSerialize;

    return url;
}

function cb_MasterPages(evt, viewState)
{
    GetBack(evt, location.pathname + location.search + location.hash, viewState);
}

function cb_IsInternalUrl(url)
{
    try
    {
        const absoluteUrl = new URL(url, window.location.origin);
        return absoluteUrl.origin === window.location.origin;
    }
    catch
    {
        return false;
    }
}

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

    return "";
}

function cb_SetCookie(key, value, seconds, path = "/")
{
    let expires = "";
    if (seconds)
    {
        const date = new Date();
        date.setTime(date.getTime() + (seconds * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = key + "=" + value + expires + "; path=" + path;
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
        const Action = ActionControl.GetTextBefore('=');
        const Control = ActionControl.GetTextAfter('=');
        let vArgs = ActionControl.GetTextAfter('=').split(GS);
        const [v1, v2, v3, v4, v5] = await cb_SetDynamicValueForArgs(evt, vArgs);

        switch (Action)
        {
            case "gt":
            case "lt":
            {
                const firstValue = v1;
                const secondValue = v2;
                const isNumber = !isNaN(firstValue) && !isNaN(secondValue);

                if (isNumber)
                    return Action == "gt" ? Number(firstValue) > Number(secondValue) : Number(firstValue) < Number(secondValue);

                return Action == "gt" ? firstValue > secondValue : firstValue < secondValue;
            }
            case "et": return (v1 == v2);
            case "Nt": return (v1 != v2);
            case "ex": return (v1 ? true : false);
            case "nx": return (v1 ? false : true);
            case "tr": return (cb_IsTrue(v1));
            case "fa": return (!cb_IsTrue(v1));
            case "mm": return (matchMedia(v1).matches);
            case "nm": return !(matchMedia(v1).matches);
            case "In": return v2.includes(v1);
            case "Nn": return !(v2.includes(v1));
            case "eE":
                try
                {
                    return cb_GetElementByElementPlace(v1);
                }
                catch
                {
                    return false;
                }
            case "nE":
                try
                {
                    return (cb_GetElementByElementPlace(v1) ? false : true);
                }
                catch
                {
                    return true;
                }
            case "re":
            case "rn":
            {
                var value = v1;
                var pattern = v2;
                try
                {
                    var regex = new RegExp(pattern);
                    var result = regex.test(value);

                    if (Action == "re")
                        return result;
                    else
                        return !result;
                }
                catch
                {
                    if (WebFormsOptions.AddConsoleMessage)
                        console.error("Invalid regex pattern:", pattern);
                    return null;
                }
            }
            case "ct":
            case "cf":
            {
                if (cb_ConfirmIsAccept === undefined) 
                {
                    var [text, type, title, okText, cancelText] = [v1, v2, v3, v4, v5];

                    if (!text)
                        text = "Are you sure you want to proceed?";
                    if (!type)
                        type = "none";
                    if (!title)
                        title = "Confirm";
                    if (!okText)
                        okText = "OK";
                    if (!cancelText)
                        cancelText = "Cancel";

                    cb_ShowConfirm(text, type, title, okText, cancelText).catch(() => { });
                }
                else if (cb_ConfirmIsAccept === true)
                {
                    cb_ConfirmIsAccept = undefined;

                    if (Action == "ct")
                        return true;
                    else
                        return null;
                }
                else if (cb_ConfirmIsAccept === false)
                {
                    cb_ConfirmIsAccept = undefined;

                    if (Action == "ct")
                        return null;
                    else
                        return true;
                }
                return;
            }
            case "fe":
            {
                await cb_StorageIsReady;
                var [path, key] = [v1, v3];
                
                if (cb_GetJSON(cb_StorageGet(key), path))
                    return true;
                else
                    return false;
            }
        }

        // Extension
        return await cb_CheckConditionExtension(evt, Action, Control);
    }
    catch (er)
    {
        if (WebFormsOptions.AddConsoleMessage)
            console.warn("There was a problem in check condition whene executing the command: " + er + "\nError in action control: " + ActionControl + (WebFormsOptions.UseConsoleStackTrace ? "\n" + er.stack : ""));

        if (WebFormsOptions.AddMessageForProblemInCheckCondition)
            cb_ShowMessage(WebFormsOptions.ProblemInCheckConditionLang, "problem", WebFormsOptions.MessageDuration);
    }
}

/* End Condition */

/* Start Unit Testing */

function cb_AssertEqual(element, tag, recursiveDepth = 0)
{
    if (recursiveDepth > 0)
        console.log(cb_Indent(recursiveDepth) + "Start inner testing depth: " + recursiveDepth);
    else
        console.log("Start unit testing assert equal");

    // Normalize Both Inputs Into DOM Elements
    const temp = document.createElement("div");
    let newElement;

    if (tag instanceof Node)
        newElement = tag.cloneNode(true);
    else if (typeof tag === "string")
    {
        temp.innerHTML = tag.trim();
        newElement = temp.firstElementChild;
    }
    else
    {
        console.info(cb_Indent(recursiveDepth) + "[ASSERT FAIL] Invalid 'tag' type — must be a DOM Node or HTML string.");

        if (recursiveDepth > 0)
            console.log(cb_Indent(recursiveDepth) + "End inner testing depth: " + recursiveDepth);
        else
            console.log("End unit testing assert equal");
        return false;
    }

    if (!newElement)
    {
        console.info(cb_Indent(recursiveDepth) + "[ASSERT FAIL] Failed to create comparable element from input.");

        if (recursiveDepth > 0)
            console.log(cb_Indent(recursiveDepth) + "End inner testing depth: " + recursiveDepth);
        else
            console.log("End unit testing assert equal");
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
            console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Attribute mismatch on "${name}": expected "${value}" but got "${elAttrMap[name] ?? 'undefined'}"`);
            isEqual = false;
        }

    for (let [name] of Object.entries(elAttrMap))
        if (!(name in newAttrMap))
        {
            console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Unexpected attribute "${name}" found on element`);
            isEqual = false;
        }

    // Compare Classes
    const elClasses = [...element.classList];
    const newClasses = [...newElement.classList];

    if (elClasses.sort().join(" ") !== newClasses.sort().join(" "))
    {
        console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Class list mismatch: expected [${newClasses}] but got [${elClasses}]`);
        isEqual = false;
    }

    // Compare Styles
    const elStyle = element.getAttribute("style") || "";
    const newStyle = newElement.getAttribute("style") || "";
    if (elStyle.trim() !== newStyle.trim())
    {
        console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Style mismatch: expected "${newStyle}" but got "${elStyle}"`);
        isEqual = false;
    }

    // Compare Form Values
    const tagName = element.tagName.toLowerCase();
    if (["input", "textarea", "select"].includes(tagName))
    {
        let val1 = element.value?.trim?.() ?? "";
        let val2 = newElement.value?.trim?.() ?? "";

        // Normalize checkbox/radio values
        if (element.type === "checkbox" || element.type === "radio")
        {
            val1 = element.checked;
            val2 = newElement.checked;
        }

        if (val1 !== val2)
        {
            console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Value mismatch in <${tagName}>: expected "${val2}" but got "${val1}"`);
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
                console.info(cb_Indent(recursiveDepth) + `[ASSERT FAIL] Text mismatch at index ${i}: expected "${t2}" but got "${t1}"`);
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
        console.info(cb_Indent(recursiveDepth) + "[ASSERT PASS] Elements are deeply equal");
    else
        console.warn(cb_Indent(recursiveDepth) + "[ASSERT FAIL] Differences found");


    if (recursiveDepth > 0)
        console.log(cb_Indent(recursiveDepth) + "End inner testing depth: " + recursiveDepth);
    else
        console.log("End unit testing assert equal");

    return isEqual;
}

/* End Unit Testing */

/* Start Style */

function cb_AddInlineStyle(el, styleString, overwrite = true)
{
    const currentStyle = el.getAttribute("style") || "";

    const styleObj = {};
    currentStyle.split(";").forEach(pair =>
    {
        if (!pair.trim())
            return;
        const [prop, val] = pair.split(":");
        if (prop && val)
            styleObj[prop.trim()] = val.trim();
    });


    styleString.split(";").forEach(pair =>
    {
        if (!pair.trim())
            return;
        const [prop, val] = pair.split(":");
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

    const finalStyle = Object.entries(styleObj).map(([prop, val]) => `${prop}: ${val}`).join("; ");

    el.setAttribute("style", finalStyle);
}

/* End Style */

/* Start String */

function cb_MatchesPattern(pattern, input)
{
    if (pattern.startsWith("re:"))
    {
        const regexBody = pattern.slice(3);
        const regex = new RegExp(regexBody);
        return regex.test(input);
    }

    if (pattern.includes('*') || pattern.includes('?'))
    {
        let regexPattern = pattern.replace(/[.+^${}()|[\]\\]/g, "\\$&");
        regexPattern = regexPattern.replace(/\*/g, ".*").replace(/\?/g, '.');
        const regex = new RegExp(`^${regexPattern}$`);
        return regex.test(input);
    }

    return pattern === input;
}

function cb_ConvertDynamicValue(x)
{
    if (typeof x !== "string")
        return x;

    if (x.startsWith("$@"))
    {
        if (WebFormsOptions.DisablePassObject)
        {
            if (WebFormsOptions.AddConsoleMessage)
                console.warn("Access to the pass object is disabled but is being attempted.\nvalue:" + x);

            return x;
        }

        try
        {
            return JSON.parse(x.substring(2));
        }
        catch
        {
            /* empty */
        }

        try
        {
            return Function(`"use strict"; return (${x.substring(2)})`)();
        }
        catch
        {
            /* empty */
        }
    }

    // Already Quoted
    if ((x.startsWith("'") && x.endsWith("'")) || (x.startsWith('"') && x.endsWith('"')) || (x.startsWith('`') && x.endsWith('`')))
        return `${x.slice(1, -1)}`;

    // Numeric string
    if (/^-?(?:0|[1-9]\d*)(?:\.\d+)?$/.test(x) && x !== "-0")
        return Number(x);

    // String
    return x;
}

function cb_Indent(depth)
{
    return "-".repeat(depth) + " ";
}

function cb_JSONParsePath(path)
{
    if (!path)
        return [];
    
    let tokens = [];
    let current = "";
    let inBracket = false;
    let bracketContent = "";
    
    for (let i = 0; i < path.length; i++)
    {
        const ch = path[i];
        
        if (ch === '[' && !inBracket)
        {
            if (current)
            {
                const parts = current.split('.');
                for (let part of parts)
                    if (part)
                        tokens.push(part);
                current = "";
            }
            inBracket = true;
            bracketContent = "";
        }
        else if (ch === ']' && inBracket)
        {
            inBracket = false;
            tokens.push(`[${bracketContent}]`);
        }
        else if (inBracket)
            bracketContent += ch;
        else
            current += ch;
    }
    
    if (current)
    {
        const parts = current.split('.');
        for (let part of parts)
            if (part)
                tokens.push(part);
    }
    
    return tokens;
}

function cb_IsTrue(value)
{
    if (value === true || value === 1)
        return true;

    if (typeof value === "string")
    {
        const v = value.trim().toLowerCase();
        return ["true", "1", "yes", "y", "on", "t", "enable", "active"].includes(v);
    }

    return false;
}

/* End String */

/* Start Extension Methods */

String.prototype.toDOM = function()
{
    const DivTag = document.createElement("div");
    DivTag.innerHTML = this;

    return DivTag.innerHTML;
};

String.prototype.HasTag = function()
{
    const tempElement = document.createElement("div");
    tempElement.innerHTML = this;
    return tempElement.childNodes.length > 0;
};

String.prototype.FullTrim = function()
{
    return this.trim().replace(/^\s\n+|\s\n+$/g, "");
};

String.prototype.TrimStart = function()
{
    return this.replace(/^[\s\n]+/, "");
};

String.prototype.IsInput = function()
{
    const TagName = this.toLowerCase();

    switch (TagName)
    {
        case "input":
        case "textarea":
        case "select":
        case "file":
        case "button":
            return true;
    }
    return false;
};

String.prototype.GetTextBefore = function(Text)
{
    if (!Text)
        return this;

    const index = this.indexOf(Text);
    if (index === -1)
        return "";

    return this.substring(0, index);
};

String.prototype.GetTextBeforeLast = function(Text)
{
    if (!Text)
        return this;

    const index = this.lastIndexOf(Text);
    if (index === -1)
        return "";

    return this.substring(0, index);
};

String.prototype.GetTextAfter = function(Text)
{
    if (!Text)
        return this;

    const index = this.indexOf(Text);
    if (index === -1)
        return "";

    return this.substring(index + Text.length);
};

String.prototype.GetTextAfterLast = function(Text)
{
    if (!Text)
        return this;

    const index = this.lastIndexOf(Text);
    if (index === -1)
        return "";

    return this.substring(index + Text.length);
};

String.prototype.DeleteHtmlClass = function(ClassName)
{
    let ClassText = this;

    if (!ClassText)
        return "";

    const ClassNameIndex = ClassText.indexOf(ClassName);

    const Space = (ClassNameIndex == 0) ? "" : ' ';
        
    ClassText = ClassText.replace(Space + ClassName, "");

    if (ClassText)
        if (ClassText[0] == ' ')
            ClassText = ClassText.slice(1);

    return ClassText;
};

String.prototype.Contains = function(Text)
{
    if (!this)
        return false;

    return this.indexOf(Text) !== -1;
};

String.prototype.ContainsWithSplitter = function(Text, Splitter)
{
    return (Splitter + this + Splitter).indexOf(Splitter + Text + Splitter) !== -1;
};

String.prototype.ContainsNameWithSplitter = function(Text, Splitter, SplitterNameValue)
{
    return (Splitter + this).indexOf(Splitter + Text + SplitterNameValue) !== -1;
};

String.prototype.Replace = function(searchValue, replaceValue)
{
    if (!this || !searchValue)
        return this;

    return this.split(searchValue).join(replaceValue);
};

Number.prototype.Replace = function(searchValue, replaceValue)
{
    const str = this.toString();
    
    if (!isFinite(this))
        return this;
    
    return str.Replace(searchValue, replaceValue);
};

String.prototype.EndsWith = function(Suffix)
{
    return this.indexOf(Suffix, this.length - Suffix.length) !== -1;
};

String.prototype.GetUnit = function()
{
    const Value = this.toLowerCase();

    if (Value.EndsWith('%'))
        return '%';
    if (Value.EndsWith("vmax"))
        return "vmax";
    if (Value.EndsWith("vmin"))
        return "vmin";
    if (Value.EndsWith("rem"))
        return "rem";
    if (Value.EndsWith("pt"))
        return "pt";
    if (Value.EndsWith("px"))
        return "px";
    if (Value.EndsWith("em"))
        return "em";
    if (Value.EndsWith("vw"))
        return "vw";
    if (Value.EndsWith("vh"))
        return "vh";
    if (Value.EndsWith("ch"))
        return "ch";
    if (Value.EndsWith("ex"))
        return "ex";
    if (Value.EndsWith("cm"))
        return "cm";
    if (Value.EndsWith("mm"))
        return "mm";
    if (Value.EndsWith("in"))
        return "in";
    if (Value.EndsWith("pc"))
        return "pc";

    return "";
};

String.prototype.IsNumber = function()
{
    const num = parseFloat(this);
    return !isNaN(num) && isFinite(num);
};

Number.prototype.IsNumber = function()
{
    return !isNaN(this) && isFinite(this);
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
    while (cb_PreRunnerIntervals[id]?.length)
        clearInterval(cb_PreRunnerIntervals[id].pop());

    delete cb_PreRunnerIntervals[id];
}

function cb_SetPreRunnerQueue(PreRunner, CodeExecutor)
{
    if (PreRunner.length < 1)
    {
        CodeExecutor();
        return;
    }

    const FirstChar = PreRunner[0].substring(0, 1);

    switch (FirstChar)
    {
        case '(':
        {
            let periodMiliSecond = PreRunner[0].GetTextAfter('(');
            let id;
            if (periodMiliSecond.Contains('|'))
            {
                id = periodMiliSecond.GetTextAfter('|');
                periodMiliSecond = periodMiliSecond.GetTextBefore('|');
            }
            if (!id)
                for (var i = 0; i < 1000000; i++)
                    if (!cb_PreRunnerIntervals[i])
                        id = i;

            if (!cb_PreRunnerIntervals[id])
                cb_PreRunnerIntervals[id] = [];
                    
            PreRunner.shift();
            cb_PreRunnerIntervals[id].push(setInterval(() => cb_SetPreRunnerQueue(PreRunner, CodeExecutor), periodMiliSecond));
            break;
        }
        case ':':
        {
            const delayMiliSecond = PreRunner[0].GetTextAfter(':');
            PreRunner.shift();
            setTimeout(() => cb_SetPreRunnerQueue(PreRunner, CodeExecutor), delayMiliSecond);
            break;
        }
        case ',':
        {
            const numberOfRepetitions = parseInt(PreRunner[0].GetTextAfter(','));
            PreRunner.shift();
            for (var i = 0; i < numberOfRepetitions; i++)
                cb_SetPreRunnerQueue(PreRunner.slice(), CodeExecutor);
            break;
        }
    }
}

async function cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue, vArgs, LastElementPlaceList, TransientDOM)
{
    if (PreRunner.length < 1)
    {
        // Return Element Place. Is Array Object List For QueryAll, And Array Object List With One Item For Other
        return await cb_SetValueToInput(evt, ActionOperation, ActionFeature, ActionValue, vArgs, LastElementPlaceList, TransientDOM);
    }

    const FirstChar = PreRunner[0].substring(0, 1);

    switch (FirstChar)
    {
        case '(':
        {
            let periodMiliSecond = PreRunner[0].GetTextAfter('(');
            let id;
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
            cb_PreRunnerIntervals[id] = setInterval(async function () { await cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue, vArgs); }, periodMiliSecond);
            break;
        }
        case ':':
        {
            const delayMiliSecond = PreRunner[0].GetTextAfter(':');
            PreRunner.shift();
            setTimeout(async function () { await cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue, vArgs); }, delayMiliSecond);
            break;
        }
        case ',':
        {
            const numberOfRepetitions = PreRunner[0].GetTextAfter(',');
            PreRunner.shift();
            for (var i = 0; i < numberOfRepetitions; i++)
                await cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue, vArgs);
        }
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
                if (WebFormsOptions.AddConsoleMessage)
                    console.error("Async error:", er);
                resolve();
            }
        }, 0);
    });
}

/* End Async Await */

/* Start State Management */

let cb_PopstateIsPending  = false;

class cb_SPA
{
    static init()
    {
        cb_HideLoader(true);
        
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
        }, "", window.location.pathname + window.location.search + window.location.hash);

        window.addEventListener("popstate", (event) =>
        {
            if (cb_PopstateIsPending)
            {
                event.preventDefault();
                history.forward();
                return;
            }
                
            const pathname = window.location.pathname;
            if (event.state && event.state.html)
            {
                cb_GetStateBodyLocation().outerHTML = event.state.html;
                document.title = event.state.title || "";
                window.scrollTo(event.state.scrollX || 0, event.state.scrollY || 0);

                cb_SPA.contentCache[pathname] = event.state.html;
                cb_SPA.titleCache[pathname] = event.state.title;
                cb_SPA.scrollX[pathname] = event.state.scrollX;
                cb_SPA.scrollY[pathname] = event.state.scrollY;

                cb_SPA.InitAfterRestoreState(event);
            }
            else
                cb_SPA.render(event, pathname);
        });
    }

    static InitAfterRestoreState(evt)
    {
        if (WebFormsOptions.CloseAllFixedFeaturesAfterHistoryReview)
            cb_CloseAllFixedFeatures();

        if (WebFormsOptions.RestoreListenersAfterHistoryReview)
            cb_RestoreListenersAfterDOMReplace();

        cb_SetWebFormsCommentsValue(null, evt, "#$");
    }

    static render(evt, pathname)
    {      
        const html = cb_SPA.contentCache[pathname];
        if (html)
        {
            cb_GetStateBodyLocation().outerHTML = html;
            document.title = cb_SPA.titleCache[pathname] || "";
            window.scrollTo(cb_SPA.scrollX[pathname] || 0, cb_SPA.scrollY[pathname] || 0);
        }
        else
            if (WebFormsOptions.ReloadOnMissingHistory)
                window.location.href = pathname; // Fallback

        cb_SPA.InitAfterRestoreState(evt);

        cb_HideLoader();
    }

    static getCurrentContent()
    {
        const el = cb_GetStateBodyLocation();
        return el ? cb_GetBodyState(el) : "";
    }

    static hasState(pathname)
    {
        return !!cb_SPA.contentCache[pathname];
    }

    static deleteState(pathname)
    {
        delete cb_SPA.contentCache[pathname];
        delete cb_SPA.titleCache[pathname];
        delete cb_SPA.scrollX[pathname];
        delete cb_SPA.scrollY[pathname];

        history.replaceState(null, "", pathname);
    }

    static clearAllStates()
    {
        cb_SPA.contentCache = {};
        cb_SPA.titleCache = {};
        cb_SPA.scrollX = {};
        cb_SPA.scrollY = {};

        history.replaceState(null, "", window.location.pathname + window.location.search + window.location.hash);
    }

    static setState(setNew, pathname, linkTitle)
    {
        cb_HideLoader(true);

        const html = cb_SPA.getCurrentContent();
        cb_SPA.contentCache[pathname] = html;
        cb_SPA.titleCache[pathname] = linkTitle || document.title;
        cb_SPA.scrollX[pathname] = window.scrollX;
        cb_SPA.scrollY[pathname] = window.scrollY;

        if (linkTitle)
            document.title = linkTitle;

        if (setNew)
        {
            history.pushState({
                html,
                title: linkTitle || document.title,
                scrollX: window.scrollX,
                scrollY: window.scrollY
            }, "", pathname);
        }
        else
        {
            history.replaceState({
                html,
                title: linkTitle || document.title,
                scrollX: window.scrollX,
                scrollY: window.scrollY
            }, "", pathname);
        }
    }
}

cb_SPA.contentCache = {};
cb_SPA.titleCache = {};
cb_SPA.scrollX = {};
cb_SPA.scrollY = {};

function cb_GetBodyState(tag)
{
    const clonedBody = tag.cloneNode(true);

    const formElements = clonedBody.querySelectorAll("input, select, textarea, output");

    formElements.forEach(el =>
    {
        if (el.tagName === "TEXTAREA")
        {
            el.setAttribute("value", el.value);
            el.textContent = el.value;
        }
        else if (el.tagName === "OUTPUT")
            el.textContent = el.value;
        else if (el.type === "checkbox" || el.type === "radio")
        {
            if (el.checked)
                el.setAttribute("checked", "");
            else
                el.removeAttribute("checked");
        }
        else if (el.tagName === "SELECT")
        {
            const options = el.querySelectorAll("option");

            options.forEach(opt => opt.removeAttribute("selected"));

            for (let opt of options)
                if (opt.selected)
                    opt.setAttribute("selected", "");
        }
        else if (["file", "button", "submit", "reset", "hidden", "image"].includes(el.type))
            return;
        else
            el.setAttribute("value", el.value);
    });

    return clonedBody.outerHTML;
}

setTimeout(() => { cb_SPA.init(); }, WebFormsOptions.SPASaveStateDelay);

function cb_SetMainSubmitTypeToButtons(obj)
{
    const buttons = obj.querySelectorAll('input[type="button"], button[type="button"]');

    buttons.forEach(button =>
    {
        if (button.getAttribute("main-type") === "submit")
        {
            button.setAttribute("type", "submit");
            button.removeAttribute("main-type");
        }
    });
}

function cb_SetStatePreservation(HtmlDOM, TransientDOM)
{
    // Save Current DOM state Including Select Values
    const selectValues = {};
    HtmlDOM.querySelectorAll("select").forEach((select, index) =>
    {
        selectValues[`select-${index}`] = select.value;
    });

    // Save And Transfer Event Listeners
    const elementsWithEvents = Object.keys(cb_EventRegistry);

    // Restore Select Values To TransientDOM
    TransientDOM.querySelectorAll("select").forEach((select, index) =>
    {
        if (selectValues[`select-${index}`])
            select.value = selectValues[`select-${index}`];
    });

    // Transfer Event Listeners From Old Elements To New Elements
    elementsWithEvents.forEach(objId =>
    {
        const events = cb_EventRegistry[objId];

        let originalElement;
        if (objId.startsWith("cb_"))
            originalElement = document.querySelector(`[cb-data-id="${objId}"]`);
        else
            originalElement = document.getElementById(objId);

        if (originalElement && HtmlDOM.contains(originalElement))
        {
            let newElement = null;
            if (objId.startsWith("cb_"))
                newElement = TransientDOM.querySelector(`[cb-data-id="${objId}"]`);
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

const cb_ActionControlHashList = [];

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
    const hashBuffer = await crypto.subtle.digest("SHA-256", data);
    const hashArray = Array.from(new Uint8Array(hashBuffer));
    const hashHex = hashArray.map(b => b.toString(16).padStart(2, '0')).join("");
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
    const loader = document.getElementById("cb_Loader");
    if (!loader)
        return

    loader.style.display = "flex";
    cb_LoaderStartTime = Date.now();

    if (cb_LoaderTimeout)
    {
        clearTimeout(cb_LoaderTimeout);
        cb_LoaderTimeout = null;
    }

    cb_LoaderTimeout = setTimeout(() => { cb_HideLoader(); }, WebFormsOptions.HideLoaderTimeout);
}

function cb_HideLoader(immediate)
{
    const loader = document.getElementById("cb_Loader");
    if (!loader)
        return;

    const elapsed = Date.now() - (cb_LoaderStartTime || 0);
    const remaining = WebFormsOptions.LoaderMinimumDuration - elapsed;

    const hide = () =>
    {
        loader.style.display = "none";
        if (cb_LoaderTimeout)
        {
            clearTimeout(cb_LoaderTimeout);
            cb_LoaderTimeout = null;
        }
        cb_LoaderStartTime = null;
    };

    if (!immediate && (remaining > 0))
        setTimeout(hide, remaining);
    else
        hide();
}

function cb_CreateLoader()
{
    if (document.getElementById("cb_Loader"))
        return;

    // Making Outer Element
    const loader = document.createElement("div");
    loader.id = "cb_Loader";
    Object.assign(loader.style,
    {
        display: "none",
        position: "fixed",
        top: '0',
        left: '0',
        width: "100%",
        height: "100%",
        background: "rgba(0,0,0,0.4)",
        zIndex: "9999",
        justifyContent: "center",
        alignItems: "center"
    });

    // Making Spinner
    const spinner = document.createElement("div");
    Object.assign(spinner.style,
    {
        width: "50px",
        height: "50px",
        border: "6px solid #ccc",
        borderTopColor: "#3498db",
        borderRadius: "50%",
        animation: "spin 1s linear infinite"
    });

    // Adding Spinner To The Loader
    loader.appendChild(spinner);
    document.body.appendChild(loader);

    // Adding keyframes To Style
    const style = document.createElement("style");
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

        if (!("serviceWorker" in navigator))
        {
            console.log("[Service Worker] no Service Worker support");
            return;
        }

        const reg = await navigator.serviceWorker.register(path, { scope: scopePath });


        if (reg.waiting)
            try { await rpcSend({ action: "skip-waiting" }); } catch { /* empty */ }

        await navigator.serviceWorker.ready;

        await new Promise(r => setTimeout(r, WebFormsOptions.ServiceWorkerWaitForControl));

        if (!navigator.serviceWorker.controller)
        {
            if (WebFormsOptions.ReloadServiceWorkerIfNeed)
                location.reload();

            if (WebFormsOptions.AddConsoleMessage)
                console.warn("[Service Worker] Service Worker installed but not controlling page yet — a reload may be required in this browser.");
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
                    console.warn("[Service Worker] Service Worker installed but not controlling page yet.");
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
                            reject(new Error((msg.result && msg.result.error) || "unknown"));
                    }
                };
                setTimeout(() => reject(new Error("[Service Worker] Service Worker rpc timeout")), 10000);
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
            add: (url, ttl) => rpcSend({ action: "cache-add", payload: { url, ttl } }),
            remove: url => rpcSend({ action: "cache-remove", payload: { url } }),
            has: url => rpcSend({ action: "cache-has", payload: { url } }).then(r => r.has),
            list: () => rpcSend({ action: "cache-list"}).then(r => r.urls),
            clear: () => rpcSend({ action: "cache-clear" }),
            setTTL: (url, seconds) => rpcSend({ action: "set-ttl", payload: { url, ttl: seconds } })
        },
        preCacheStatic: assets => rpcSend({ action: "static-precache", payload: { assets } }),
        listStatic: () => rpcSend({ action: "static-list" }).then(r => r.urls),

        // Routing
        routeSet: (pattern, type = "networkonly", cacheDynamic = false) => rpcSend({ action: "route-set", payload: { pattern, type, cacheDynamic } }),
        routeClear: () => rpcSend({ action: "route-clear" }),
        routeAlias: (from, to) => rpcSend({ action: "route-alias", payload: { from, to } }),
        routeRemoveAlias: (from) => rpcSend({ action: "route-remove-alias", payload: { from } }),
        routeRemove: (pattern) => rpcSend({ action: "route-remove", payload: { pattern } }),

        // Helper
        isRegistered: async () => !!(await navigator.serviceWorker.getRegistration())
    };

    return API;
})();

if (WebFormsOptions.RegisterServiceWorker)
{
    (async () => {
        await cb_ServiceWorker.register();
        await navigator.serviceWorker.ready;
        console.log("[Service Worker] Service Worker is ready and controlling this page.");
    })();
}

async function cb_ServiceWorkerPush()
{
    const reg = await navigator.serviceWorker.ready;
    const permission = await Notification.requestPermission();

    if (permission !== "granted")
        return;

    const sub = await reg.pushManager.subscribe({
        userVisibleOnly: true,
        applicationServerKey: cb_UrlBase64ToUint8Array(WebFormsOptions.ServiceWorkerPushVapidPublicKey)
    });

    await fetch(WebFormsOptions.UseServiceWorkerPushSubscribe,
    {
        method: "POST",
        headers: { "Content-Type": "application/json" },
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
        if (permission === "granted")
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
background-color: #464646;
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
    if (!document.getElementById("alertAnimations"))
    {
        const style = document.createElement("style");
        style.id = "alertAnimations";
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
        case "warning": return WebFormsOptions.MessageWarningStyle;
        case "problem": return WebFormsOptions.MessageProblemStyle;
        case "help": return WebFormsOptions.MessageHelpStyle;
        case "success": return WebFormsOptions.MessageSuccessStyle;
        default: return WebFormsOptions.MessageNoneStyle;
    }
}

let cb_AlertListenerAdded = false;

function cb_ShowAlert(text, type = "none", title = "Alert", okText = "OK")
{
    const overlay = document.createElement("div");
    overlay.setAttribute("style", cb_OverlayStyle);
    overlay.setAttribute("cb-fixed", "true");
    overlay.classList.add("cb-alert-overlay");

    const alertBox = document.createElement("div");
    alertBox.setAttribute("style", cb_AlertBoxStyle);

    const alertHeader = document.createElement("h2");
    alertHeader.textContent = title;
    alertHeader.setAttribute("style", cb_HeaderStyle + cb_MessageTypeStyle(type));

    const alertText = document.createElement('p');
    alertText.textContent = text;
    alertText.setAttribute("style", cb_TextStyle);

    const okButton = document.createElement("button");
    okButton.textContent = okText;
    okButton.setAttribute("style", cb_ButtonStyle);
    okButton.classList.add("cb-alert-ok");

    alertBox.appendChild(alertHeader);
    alertBox.appendChild(alertText);
    alertBox.appendChild(okButton);

    overlay.appendChild(alertBox);

    document.body.appendChild(overlay);

    cb_AddAnimationStyles();

    if (!cb_AlertListenerAdded)
    {
        document.addEventListener("click", (e) =>
        {
            if (e.target.matches(".cb-alert-ok"))
            {
                const overlay = e.target.closest(".cb-alert-overlay");
                if (overlay && document.body.contains(overlay))
                    document.body.removeChild(overlay);
            }
        });

        cb_AlertListenerAdded = true;
    }

    // Close With Escape Key
    document.addEventListener("keydown", (e) =>
    {
        if (e.key === "Escape")
        {
            const overlay = document.querySelector(".cb-alert-overlay:last-child");
            if (overlay && document.body.contains(overlay))
            {
                document.body.removeChild(overlay);
            }
        }
    });
}

window.cb_ConfirmIsAccept = undefined;
let cb_ConfirmListenerAdded = false;

function cb_ShowConfirm(text = "Are you sure you want to proceed?", type = "none", title = "Confirm", okText = "OK", cancelText = "Cancel")
{
    cb_ConfirmIsAccept = null;

    return new Promise((resolve, reject) =>
    {
        const overlay = document.createElement("div");
        overlay.setAttribute("style", cb_OverlayStyle);
        overlay.setAttribute("cb-fixed", "true");
        overlay.classList.add("cb-confirm-overlay");

        const confirmBox = document.createElement("div");
        confirmBox.setAttribute("style", cb_AlertBoxStyle);

        const confirmHeader = document.createElement("h2");
        confirmHeader.textContent = title;
        confirmHeader.setAttribute("style", cb_HeaderStyle + cb_MessageTypeStyle(type));

        const confirmText = document.createElement('p');
        confirmText.textContent = text;
        confirmText.setAttribute("style", cb_TextStyle);

        const buttonContainer = document.createElement("div");

        const cancelButton = document.createElement("button");
        cancelButton.textContent = cancelText;
        cancelButton.setAttribute("style", cb_CancelButtonStyle);
        cancelButton.classList.add("cb-confirm-cancel");

        const okButton = document.createElement("button");
        okButton.textContent = okText;
        okButton.setAttribute("style", cb_ButtonStyle);
        okButton.classList.add("cb-confirm-ok");

        buttonContainer.appendChild(cancelButton);
        buttonContainer.appendChild(okButton);

        confirmBox.appendChild(confirmHeader);
        confirmBox.appendChild(confirmText);
        confirmBox.appendChild(buttonContainer);

        overlay.appendChild(confirmBox);
        document.body.appendChild(overlay);

        cb_AddAnimationStyles();

        if (!cb_ConfirmListenerAdded)
        {
            // OK
            document.addEventListener("click", (e) =>
            {
                if (e.target.matches(".cb-confirm-ok"))
                {
                    const overlayTarget = e.target.closest(".cb-confirm-overlay");
                    if (overlayTarget && document.body.contains(overlayTarget))
                    {
                        document.body.removeChild(overlayTarget);
                        cb_ConfirmIsAccept = true;
                        resolve();
                    }
                }
            });

            // Cancel
            document.addEventListener("click", (e) =>
            {
                if (e.target.matches(".cb-confirm-cancel"))
                {
                    const overlayTarget = e.target.closest(".cb-confirm-overlay");
                    if (overlayTarget && document.body.contains(overlayTarget))
                    {
                        document.body.removeChild(overlayTarget);
                        cb_ConfirmIsAccept = false;
                        reject();
                    }
                }
            });

            // ESC
            document.addEventListener("keydown", (e) =>
            {
                if (e.key === "Escape")
                {
                    const overlayTarget = document.querySelector(".cb-confirm-overlay:last-child");
                    if (overlayTarget && document.body.contains(overlayTarget))
                    {
                        document.body.removeChild(overlayTarget);
                        cb_ConfirmIsAccept = false;
                        reject();
                    }
                }
            });

            cb_ConfirmListenerAdded = true;
        }
    });
}

window.cb_ConfirmIsAccept = cb_ConfirmIsAccept;

let cb_MessageListenerAdded = false;

function cb_ShowMessage(text, type, duration = 0)
{
    const message = document.createElement("div");
    message.setAttribute("style", cb_MessageStyle + cb_MessageTypeStyle(type));

    const messageText = document.createElement("span");
    messageText.textContent = text;

    const closeButton = document.createElement("button");
    closeButton.textContent = '×';
    closeButton.setAttribute("style", cb_MessageButtonStyle);
    closeButton.setAttribute("title", "Close");
    closeButton.classList.add("cb-message-close");

    message.appendChild(messageText);
    message.appendChild(closeButton);

    let messageContainer;
    if (document.getElementById("cb_MessageContainer"))
        messageContainer = document.getElementById("cb_MessageContainer")
    else
    {
        messageContainer = document.createElement("div");
        messageContainer.id = "cb_MessageContainer";
        messageContainer.setAttribute("style", cb_MessageContainerStyle);
        messageContainer.setAttribute("cb-fixed", "true");
    }

    messageContainer.appendChild(message);

    document.body.appendChild(messageContainer);

    cb_AddAnimationStyles();

    // Add Event Listener To Close Button
    if (!cb_MessageListenerAdded)
    {
        document.addEventListener("click", (e) =>
        {
            if (e.target.matches(".cb-message-close"))
            {
                const message = e.target.parentElement;
                const messageContainer = message.parentElement;
                
                message.style.animation = "messageFadeOut 0.3s ease-out forwards";
                setTimeout(() =>
                {
                    if (messageContainer && messageContainer.contains(message))
                    {
                        messageContainer.removeChild(message);
                        
                        if (messageContainer.childNodes.length == 0)
                            document.body.removeChild(messageContainer);
                    }
                }, 300);
            }
        });

        cb_MessageListenerAdded = true;
    }

    // Auto-Remove After Duration If Specified
    if (duration > 0)
    {
        setTimeout(() =>
        {
            if (messageContainer && messageContainer.contains(message))
            {
                message.style.animation = "messageFadeOut 0.3s ease-out forwards";
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
    let message = WebFormsOptions.ConnectionErrorLang;

    if (errorCode)
        message += ": " + errorCode;

    cb_ShowMessage(message, "problem", WebFormsOptions.MessageDuration);
}

function cb_CloseAllFixedFeatures()
{
    const allFixed = document.querySelectorAll('[cb-fixed="true"]');
    allFixed.forEach(fixed =>{
        if (fixed && fixed.parentNode)
            fixed.parentNode.removeChild(fixed);
    });
}

/* End Message */

/* Start Template Engine */

function cb_BindToTemplate(data, type, path, element, templatePattern, replaceStartTagAttribute)
{
    let dict;
    
    switch(type)
    {
        case "json":
            dict = JSON.parse(cb_GetJSONDictionary(data, path));
            break;
        case "xml":
            dict = JSON.parse(cb_GetXMLDictionary(data, path));
            break;
        case "ini":
            dict = JSON.parse(cb_GetINIDictionary(data, path));
            break;
        default:
            return;
    }
    
    Object.entries(dict).forEach(([key, value]) =>
    {
        cb_ReplaceDeep(element, templatePattern.replace("value", key), String(value), replaceStartTagAttribute);
    });
}

/* End Template Engine */

/* Start Tag Change */

function cb_ReplaceDeep(element, value, newValue, replaceStartTagAttribute)
{
    for (let node of element.childNodes)
        if (node.nodeType === Node.TEXT_NODE && node.nodeValue.includes(value))
            node.nodeValue = node.nodeValue.Replace(value, newValue);

    if (replaceStartTagAttribute)
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
    const temp = document.createElement("div");
    let newElement;

    if (tag instanceof Node)
    {
        newElement = tag.cloneNode(true);

        // Tranfer EventListeners from cb_EventRegistry
        const objId = tag.id || tag.getAttribute("cb-data-id");
        if (objId && cb_EventRegistry[objId])
        {
            for (const event of Object.keys(cb_EventRegistry[objId]))
                for (const entry of cb_EventRegistry[objId][event])
                    await cb_AddEventListener(newElement, event, entry.functionName, entry.args);
        }
    }
    else if(typeof tag === "string")
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

async function cb_SetMorph(element, tag)
{
    const temp = document.createElement("div");
    let newElement;

    if (tag instanceof Node)
        newElement = tag;
    else if (typeof tag === "string")
    {
        temp.innerHTML = tag.trim();
        newElement = temp.firstElementChild;
    }
    else
        return;

    if (!newElement)
        return;

    await cb_MorphNode(element, newElement);
}

async function cb_MorphNode(oldNode, newNode)
{
    if (oldNode.nodeType !== newNode.nodeType)
    {
        const replacement = newNode.cloneNode(true);

        await cb_MorphTransferEventListeners(newNode, replacement);

        oldNode.replaceWith(replacement);
        return replacement;
    }

    if (oldNode.nodeType === Node.TEXT_NODE)
    {
        if (oldNode.nodeValue !== newNode.nodeValue)
            oldNode.nodeValue = newNode.nodeValue;

        return oldNode;
    }

    if (oldNode.nodeType === Node.COMMENT_NODE)
    {
        if (oldNode.nodeValue !== newNode.nodeValue)
            oldNode.nodeValue = newNode.nodeValue;

        return oldNode;
    }

    if (oldNode.nodeName !== newNode.nodeName)
    {
        const replacement = newNode.cloneNode(true);

        await cb_MorphTransferEventListeners(newNode, replacement);

        oldNode.replaceWith(replacement);
        return replacement;
    }

    for (const attr of [...oldNode.attributes])
        if (!newNode.hasAttribute(attr.name))
            oldNode.removeAttribute(attr.name);

    for (const attr of [...newNode.attributes])
        if (oldNode.getAttribute(attr.name) !== attr.value)
            oldNode.setAttribute(attr.name, attr.value);

    const newChildren = [...newNode.childNodes];

    for (let i = 0; i < newChildren.length; i++)
    {
        const newChild = newChildren[i];

        if (!oldNode.childNodes[i])
        {
            const newChildClone = newChild.cloneNode(true);

            await cb_MorphTransferEventListeners(newChild, newChildClone);

            oldNode.appendChild(newChildClone);
            continue;
        }

        const oldChild = oldNode.childNodes[i];

        const matchedChild = cb_FindMorphMatch(oldNode, oldChild, newChild);

        if (matchedChild && matchedChild !== oldChild)
        {
            oldNode.insertBefore(matchedChild, oldChild);

            await cb_MorphNode(matchedChild, newChild);
        }
        else
            await cb_MorphNode(oldChild, newChild);
    }

    while (oldNode.childNodes.length > newChildren.length)
        oldNode.lastChild.remove();

    return oldNode;
}


async function cb_MorphTransferEventListeners(source, target)
{
    if (!source || !target || source.nodeType !== Node.ELEMENT_NODE)
        return;

    const objId = source.id || source.getAttribute("cb-data-id");

    if (objId && cb_EventRegistry[objId])
        for (const event of Object.keys(cb_EventRegistry[objId]))
            for (const entry of cb_EventRegistry[objId][event])
                await cb_AddEventListener(target, event, entry.functionName, entry.args);

    const sourceChildren = [...source.children];
    const targetChildren = [...target.children];

    const length = Math.min(sourceChildren.length, targetChildren.length);

    for (let i = 0; i < length; i++)
        await cb_MorphTransferEventListeners(sourceChildren[i], targetChildren[i]);
}


function cb_FindMorphMatch(parent, oldChild, newChild)
{
    if (newChild.nodeType !== Node.ELEMENT_NODE)
        return oldChild;

    if (newChild.id)
        for (const child of parent.children)
        {
            if (child === oldChild)
                continue;

            if (child.id === newChild.id)
                return child;
        }

    const newDataId = newChild.getAttribute("cb-data-id");

    if (newDataId)
        for (const child of parent.children)
        {
            if (child === oldChild)
                continue;

            if (child.getAttribute("cb-data-id") === newDataId)
                return child;
        }

    return oldChild;
}

/* End Tag Change */

/* Start Call Method */

async function cb_RunMethod(evt, funcName, args)
{
    if (args)
        return cb_GetMethod(funcName)(...args);
    else
        return cb_GetMethod(funcName)();
}

function cb_GetMethod(funcName)
{
    const noop = () => { return ""; }; // Empty Fallback To Avoid Runtime Errors

    if (WebFormsOptions.DisableCallMethod)
    {
        if (WebFormsOptions.AddConsoleMessage)
            console.warn("Access to the call method is disabled but is being attempted.\nMethod: " + funcName);
        return "";
    }

    if (WebFormsOptions.UseCallMethodOnlyInAcceptedList)
        if (!WebFormsOptions.CallMethodOnlyInAcceptedList.some(p => cb_MatchesPattern(p, funcName)))
        {
            if (WebFormsOptions.AddConsoleMessage)
                console.warn("Access to call method is only possible in the list, but is being attempted.\nMethod: " + funcName);
            return "";
        }

    const fn = window[funcName];
    if (typeof fn === "function")
        return fn;
    else
    {
        if (WebFormsOptions.AddConsoleMessage)
            console.warn(`Method "${funcName}" not found or not loaded yet.`);
        return noop;
    }
}

async function cb_RunModuleMethod(evt, funcName, args)
{
    if (args)
        return cb_GetModuleMethod(funcName)(...args);
    else
        return cb_GetModuleMethod(funcName)();
}

async function cb_RunMathMethod(evt, funcName, args)
{
    if (args)
        return window["Math"][funcName](...args);
    else
        return window["Math"][funcName]();
}

/* End Call Method */

/* Start Wasm */

async function cb_RunWasmMethodResult(wasmLanguage, wasmUrl, funcName, args = [])
{
    switch (wasmLanguage)
    {
        case 'c': return (await cb_RunWasmMethod_C(wasmUrl, funcName, args)).result;
        case "rust": return (await cb_RunWasmMethod_Rust(wasmUrl, funcName, args)).result;
        case "csharp": return (await cb_RunWasmMethod_CSharp(wasmUrl, funcName, args)).result;
        case "csharp-m": return (await cb_RunWasmMethod_CSharpMediator(wasmUrl, funcName, args));
        case "go": return (await cb_RunWasmMethod_Go(wasmUrl, funcName, args)).result;
        case "java": return (await cb_RunWasmMethod_Java(wasmUrl, funcName, args)).result;
        case "as": return (await cb_RunWasmMethod_AS(wasmUrl, funcName, args)).result;
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
            table: new WebAssembly.Table({ initial: 0, element: "anyfunc" }),
            __wbindgen_throw: (ptr, len) =>
            {
                const memView = new Uint8Array(memory.buffer);
                const msg = new TextDecoder("utf-8").decode(memView.subarray(ptr, ptr + len));
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
        throw new Error(`Failed to instantiate WASM module: ${er.message}`, er);
    }

    const method = instance.exports[funcName];
    if (typeof method !== "function")
        throw new Error(`Function "${funcName}" not found. Available: ${Object.keys(instance.exports).join(", ")}`);

    // Inputs
    const processedArgs = [];
    for (const arg of args)
    {
        if (typeof arg === "string")
        {
            if (!instance.exports.alloc)
            {
                if (WebFormsOptions.AddConsoleMessage)
                    console.warn("alloc not exported: cannot pass strings to WASM directly");

                processedArgs.push(0);
            }
            else
            {
                const encoder = new TextEncoder();
                const encoded = encoder.encode(arg + "\0");
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
    if (typeof result === "number" && result > 0 && memory)
    {
        try
        {
            const memView = new Uint8Array(memory.buffer);
            let end = result;

            while (end < memView.length && memView[end] !== 0)
                end++;

            const text = new TextDecoder("utf-8").decode(memView.subarray(result, end));
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
            table: new WebAssembly.Table({ initial: 0, element: "anyfunc" }),
            abort: () => { throw new Error("WASM aborted"); }
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
        throw new Error(`Failed to instantiate WASM module: ${er.message}`, er);
    }

    const method = instance.exports[funcName];
    if (typeof method !== "function")
        throw new Error(`Function "${funcName}" not found. Available: ${Object.keys(instance.exports).join(", ")}`);

    // Inputs
    const processedArgs = [];
    for (const arg of args)
    {
        if (typeof arg === "string")
        {
            if (WebFormsOptions.AddConsoleMessage)
                console.warn("Passing strings requires custom alloc in C/C++ wasm");

            processedArgs.push(0);
        }
        else
            processedArgs.push(arg);
    }

    let result = method(...processedArgs);

    // Output Detection
    if (typeof result === "number" && result > 0 && memory)
    {
        try
        {
            const memView = new Uint8Array(memory.buffer);
            let end = result;

            while (end < memView.length && memView[end] !== 0)
                end++;

            const text = new TextDecoder("utf-8").decode(memView.subarray(result, end));
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
        throw new Error(`C# WASM init failed: ${er.message}`, er);
    }

    const method = instance.exports[funcName];
    if (typeof method !== "function")
        throw new Error(`Function ${funcName} not found in C# WASM exports`);

    // Inputs
    const processedArgs = [];
    for (const arg of args)
    {
        if (typeof arg === "string")
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
    if (typeof result === "number" && result > 0)
    {
        const memView = new Uint8Array(memory.buffer);
        let end = result;

        while (end < memView.length && memView[end] !== 0)
            end++;

        const text = new TextDecoder("utf-8").decode(memView.subarray(result, end));
        if (text.trim().length > 0)
            result = text;
    }

    return { result, memory };
}

async function cb_RunWasmMethod_CSharpMediator(mediatorUrl, funcName, args = [])
{
    const dotnet = await import(mediatorUrl);

    const runtime = await dotnet.dotnet.withApplicationArgumentsFromQuery().create();

    const config = runtime.getConfig();

    const exports = await runtime.getAssemblyExports(config.mainAssemblyName);

    const lastDot = funcName.lastIndexOf(".");

    if (lastDot <= 0 || lastDot === funcName.length - 1)
        throw new Error(`Invalid C# method name: ${funcName}`);

    const typeName = funcName.substring(0, lastDot);
    const methodName = funcName.substring(lastDot + 1);

    const type = exports[typeName];

    if (!type)
        throw new Error(`Type ${typeName} not found`);

    const method = type[methodName];

    if (typeof method !== "function")
        throw new Error(`Function ${funcName} not found`);

    return method(...args);
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
        throw new Error(`Go WASM init failed: ${er.message}`, er);
    }

    const method = instance.exports[funcName];
    if (typeof method !== "function")
        throw new Error(`Function ${funcName} not found in Go WASM exports`);

    // Inputs
    const processedArgs = [];
    for (const arg of args)
    {
        if (typeof arg === "string")
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
    if (typeof result === "number" && result > 0)
    {
        const memView = new Uint8Array(memory.buffer);
        let end = result;

        while (end < memView.length && memView[end] !== 0)
            end++;

        const text = new TextDecoder("utf-8").decode(memView.subarray(result, end));
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
        throw new Error(`Java WASM init failed: ${er.message}`, er);
    }

    const method = instance.exports[funcName];
    if (typeof method !== "function")
        throw new Error(`Function ${funcName} not found in Java WASM exports`);

    // Inputs
    const processedArgs = [];
    for (const arg of args)
    {
        if (typeof arg === "string")
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
    if (typeof result === "number" && result > 0)
    {
        const memView = new Uint8Array(memory.buffer);
        let end = result;

        while (end < memView.length && memView[end] !== 0)
            end++;

        const text = new TextDecoder("utf-8").decode(memView.subarray(result, end));
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
            table: new WebAssembly.Table({ initial: 0, element: "anyfunc" }),
        }
    };

    const { instance } = await WebAssembly.instantiate(bytes, imports);

    const method = instance.exports[funcName];
    if (typeof method !== "function")
        throw new Error(`Function "${funcName}" not found. Available: ${Object.keys(instance.exports).join(", ")}`);

    const processedArgs = [];
    const stringPointers = [];

    // Inputs
    for (const arg of args)
    {
        if (typeof arg === "string")
        {
            if (!instance.exports.__new) throw new Error("__new not exported for string allocation");
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
    if (typeof result === "number" && result > 0)
    {
        try
        {
            const memView = new Uint8Array(memory.buffer);
            let end = result;

            while (end < memView.length && memView[end] !== 0)
                end++;

            result = new TextDecoder("utf-8").decode(memView.subarray(result, end));
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
        if (WebFormsOptions.AddConsoleMessageForModule)
            console.warn("Access for load the module is disabled but is being attempted.\nModule path: " + modulePath);
        return null;
    }

    if (WebFormsOptions.UseLoadModulePathOnlyInAcceptedList)
        if (!WebFormsOptions.LoadModulePathOnlyInAcceptedList.some(p => cb_MatchesPattern(p, modulePath)))
        {
            if (WebFormsOptions.AddConsoleMessageForModule)
                console.warn("Access to load the module is only possible in the list, but is being attempted.\nModule path: " + modulePath);
            return null;
        }

    if (cb_LoadedModules[modulePath])
    {
        if (WebFormsOptions.AddConsoleMessageForModule)
            console.warn(`Module "${modulePath}" is already loaded.`);
        return cb_LoadedModules[modulePath];
    }

    try
    {
        const mod = await import(modulePath);
        cb_LoadedModules[modulePath] = mod;

        const methodsToLoad = methods.length > 0 ? methods : Object.keys(mod).filter(k => typeof mod[k] === "function");

        methodsToLoad.forEach(method => { cb_ModuleMethodMap[method] = mod[method]; });

        if (WebFormsOptions.AddConsoleMessageForModule)
            console.log(`Module "${modulePath}" loaded (${methodsToLoad.length} methods).`);

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
        if (WebFormsOptions.AddConsoleMessageForModule)
            console.log(`Module "${modulePath}" and its methods were unloaded.`);
    }
    else
        if (WebFormsOptions.AddConsoleMessageForModule)
            console.warn(`Module "${modulePath}" not found or not loaded.`);
}

function cb_GetModuleMethod(method)
{
    const noop = () => { return ""; }; // Empty Fallback To Avoid Runtime Errors

    if (WebFormsOptions.DisableCallModuleMethod)
    {
        if (WebFormsOptions.AddConsoleMessageForModule)
            console.warn("Access to the call module method is disabled but is being attempted.\nMethod: " + method);
        return noop;
    }

    if (WebFormsOptions.UseCallModuleMethodOnlyInAcceptedList)
        if (!WebFormsOptions.CallModuleMethodOnlyInAcceptedList.some(p => cb_MatchesPattern(p, method)))
        {
            if (WebFormsOptions.AddConsoleMessageForModule)
                console.warn("Access to call module method is only possible in the list, but is being attempted.\nMethod: " + method);
            return noop;
        }

    const fn = cb_ModuleMethodMap[method];
    if (typeof fn === "function")
        return fn;
    else
    {
        if (WebFormsOptions.AddConsoleMessageForModule)
            console.warn(`Method "${method}" not found or not loaded yet.`);
        return noop;
    }
}

// Remove A Specific Method
function cb_DeleteModuleMethod(method)
{
    if (cb_ModuleMethodMap[method])
    {
        delete cb_ModuleMethodMap[method];

        if (WebFormsOptions.AddConsoleMessageForModule)
            console.log(`Method "${method}" removed.`);
    }
    else
        if (WebFormsOptions.AddConsoleMessageForModule)
            console.warn(`Method "${method}" not found for removal.`);
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
    return result.outerHTML;
}

// Text
function cb_GetTextLine(text, line)
{
    const lines = text.split("\n");
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
    const lines = text.split("\n");
    line = parseInt(line, 10);

    if (line < 0)
        line = lines.length + line;

    while (line >= lines.length)
        lines.push("");

    lines[line] = newValue;
    return lines.join("\n");
}

function cb_AppendTextLine(text, line, add)
{
    const lines = text.split("\n");
    line = parseInt(line, 10);

    if (line < 0)
    {
        line = lines.length + line;
        if (line < 0 || line >= lines.length)
            line = lines.length - 1;
    }

    while (line >= lines.length)
        lines.push("");

    lines[line] += add;
    return lines.join("\n");
}

function cb_DeleteTextLine(text, line, remove)
{
    const lines = text.split("\n");
    line = parseInt(line, 10);

    if (line < 0)
        line = lines.length + line;

    if (line >= 0 && line < lines.length)
    {
        if (remove === undefined || remove === null)
            lines.splice(line, 1);
        else
            lines[line] = lines[line].replace(remove, "");
    }

    return lines.join("\n");
}

// INI
function cb_GetINI(text, path, isINILike = false)
{
    if (isINILike || !path.includes('.'))
    {
        const name = path.trim();
        const lines = text.split("\n");

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
                else
                    lines.push(`${name}=${value}`);

                if (!addOnly)
                    break;
            }
        }

        if (!found && !updateOnly)
            lines.push(`${name}=${value}`);

        return lines.join('\n');
    }

    const [sec, key] = path.split('.');

    for (let i = 0; i < lines.length; i++)
    {
        const line = lines[i].trim();

        if (line === `[${sec}]`)
        {
            let keyExists = false;

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
                    {
                        lines[j] = `${key}=${value}`;
                        break;
                    }
                }
            }

            if (!keyExists || addOnly)
            {
                if (!updateOnly)
                    lines.splice(i + 1, 0, `${key}=${value}`);
            }

            return lines.join('\n');
        }
    }

    if (!updateOnly)
    {
        lines.push("");
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

function cb_GetINIDictionary(text, path)
{
    try
    {
        let targetObj;
        
        if (path.includes('.'))
        {
            const [sec, key] = path.split('.');
            const obj = cb_ParseINI(text);
            
            if (sec && obj[sec] && obj[sec][key])
            {
                targetObj = {};
                targetObj[key] = obj[sec][key];
            }
            else if (sec && obj[sec])
                targetObj = obj[sec];
            else
                targetObj = obj;
        }
        else if (path && path !== "")
        {
            const obj = cb_ParseINI(text);
            
            if (obj[path] && typeof obj[path] === "object")
                targetObj = obj[path];
            else
            {
                const value = cb_GetINI(text, path);
                if (value !== null)
                {
                    targetObj = {};
                    targetObj[path] = value;
                }
                else
                    targetObj = {};
            }
        }
        else
            targetObj = cb_ParseINI(text);
        
        if (targetObj === null || targetObj === undefined)
            return "{}";
        
        const dictionary = {};
        
        if (Array.isArray(targetObj))
        {
            if (targetObj.length === 0)
                return "{}";
            
            if (typeof targetObj[0] === "object" && targetObj[0] !== null)
            {
                const firstItem = targetObj[0];
                for (let key in firstItem)
                {
                    const value = firstItem[key];
                    if (typeof value === "object" && value !== null)
                        dictionary[key] = JSON.stringify(value);
                    else
                        dictionary[key] = value;
                }
            }
            else
                dictionary["value"] = targetObj[0];
        }
        else if (typeof targetObj === "object" && targetObj !== null)
        {
            for (let key in targetObj)
            {
                const value = targetObj[key];
                if (typeof value === "object" && value !== null)
                    dictionary[key] = JSON.stringify(value);
                else
                    dictionary[key] = value;
            }
        }
        else
            dictionary["value"] = targetObj;
        
        return JSON.stringify(dictionary);
    }
    catch
    {
        return "{}";
    }
}

// XML
function cb_GetXML(text, path)
{
    try
    {
        const parser = new DOMParser();
        const xml = parser.parseFromString(text, "text/xml");

        const parserError = xml.getElementsByTagName("parsererror");
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
            {
                const node = result.iterateNext();
                if (!node)
                    return null;

                if (node.nodeType === Node.ATTRIBUTE_NODE)
                    return node.value;

                return node.textContent.trim();
            }
        }
    }
    catch (er)
    {
        if (WebFormsOptions.AddConsoleMessage)
            console.error("XML Get error: ", er);
        return null;
    }
}

function cb_SetXML(text, path, value)
{
    try
    {
        const xml = new DOMParser().parseFromString(text, "text/xml");

        const parserError = xml.getElementsByTagName("parsererror");
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
        if (WebFormsOptions.AddConsoleMessage)
            console.error("XML Set error: ", er);
        return text;
    }
}

function cb_DeleteXML(text, path)
{
    try
    {
        const xml = new DOMParser().parseFromString(text, "text/xml");

        const parserError = xml.getElementsByTagName("parsererror");
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
        if (WebFormsOptions.AddConsoleMessage)
            console.error("XML Delete error: ", er);
        return text;
    }
}

function cb_AddXML(text, path, name, value = "")
{
    try
    {
        // Parse XML
        const xml = new DOMParser().parseFromString(text, "text/xml");

        // Check Parse Errors
        const parserError = xml.getElementsByTagName("parsererror");
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
        const isAttribute = typeof name === "string" && name.startsWith('@');
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
            else if (typeof attrName === "string" && attrName.length > 0)
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
        if (WebFormsOptions.AddConsoleMessage)
            console.error("XML Add error: ", er);
        return text;
    }
}

function cb_GetXMLDictionary(text, path)
{
    try
    {
        const parser = new DOMParser();
        const xml = parser.parseFromString(text, "text/xml");

        const parserError = xml.getElementsByTagName("parsererror");
        if (parserError.length > 0)
            return "{}";

        let targetNode;
        
        if (!path || path === "")
            targetNode = xml.documentElement;
        else
        {
            const evaluator = new XPathEvaluator();
            const result = evaluator.evaluate(path, xml, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null);
            targetNode = result.singleNodeValue;
        }
        
        if (!targetNode)
            return "{}";
        
        const dictionary = {};
        
        function xmlToDict(node, prefix = "")
        {
            const result = {};
            
            const nodeName = prefix ? `${prefix}.${node.nodeName}` : node.nodeName;
            
            // Attributes
            if (node.attributes && node.attributes.length > 0)
            {
                for (let i = 0; i < node.attributes.length; i++)
                {
                    const attr = node.attributes[i];
                    result[`${nodeName}.@${attr.name}`] = attr.value;
                }
            }
            
            // Children
            let hasElementChildren = false;
            let textValue = "";
            
            for (let i = 0; i < node.childNodes.length; i++)
            {
                const child = node.childNodes[i];
                
                if (child.nodeType === Node.ELEMENT_NODE)
                {
                    hasElementChildren = true;
                    const childDict = xmlToDict(child, nodeName);
                    Object.assign(result, childDict);
                }
                else if (child.nodeType === Node.TEXT_NODE)
                {
                    const text = child.textContent.trim();
                    if (text)
                        textValue += text + ' ';
                }
            }
            
            if (!hasElementChildren && textValue.trim())
                result[nodeName] = textValue.trim();
            else if (!hasElementChildren && !textValue.trim() && prefix)
                result[nodeName] = "";
            
            return result;
        }
        
        const fullDict = xmlToDict(targetNode);
        
        for (let key in fullDict)
        {
            const parts = key.split('.');
            const simpleKey = parts[parts.length - 1];
            
            if (!dictionary[simpleKey])
                dictionary[simpleKey] = fullDict[key];
        }
        
        if (Object.keys(dictionary).length === 1 && dictionary["value"])
            return JSON.stringify({ value: dictionary["value"] });
        
        return JSON.stringify(dictionary);
    }
    catch (er)
    {
        if (WebFormsOptions.AddConsoleMessage)
            console.error("XML Get Dictionary error: ", er);
        return "{}";
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
            const resolvedKey = cb_JSONResolveKey(val, k);
            
            if (resolvedKey === null || resolvedKey === undefined)
                return null;
            
            if (typeof resolvedKey === "number" && Array.isArray(val))
            {
                if (resolvedKey < 0 || resolvedKey >= val.length)
                    return null;
                val = val[resolvedKey];
            }
            else if (val && typeof val === "object" && resolvedKey in val)
                val = val[resolvedKey];
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
    try
    {
        let obj = JSON.parse(text);
        const keys = cb_JSONParsePath(path);
        
        if (keys.length === 0)
        {
            obj = value;
            return JSON.stringify(obj);
        }
        
        let target = obj;
        
        for (let i = 0; i < keys.length - 1; i++)
        {
            let k = keys[i];
            let nextK = keys[i + 1];
            
            const resolvedKey = cb_JSONResolveKey(target, k);
            
            if (Array.isArray(target) && typeof resolvedKey === "number")
            {
                if (resolvedKey < 0)
                    return text;
                if (target[resolvedKey] === undefined)
                {
                    const nextResolved = cb_JSONResolveKey({}, nextK);
                    target[resolvedKey] = typeof nextResolved === "number" ? [] : {};
                }
                target = target[resolvedKey];
            }
            else if (typeof resolvedKey === "string" && resolvedKey in target)
                target = target[resolvedKey];
            else if (!(k in target))
            {
                const nextResolved = cb_JSONResolveKey({}, nextK);
                target[k] = typeof nextResolved === "number" ? [] : {};
                target = target[k];
            }
            else
                target = target[k];
        }
        
        let finalKey = keys[keys.length - 1];
        const resolvedFinalKey = cb_JSONResolveKey(target, finalKey);
        
        if (Array.isArray(target) && typeof resolvedFinalKey === "number")
        {
            if (resolvedFinalKey < 0)
            {
                const index = target.length + resolvedFinalKey;
                if (index >= 0)
                    target[index] = value;
            }
            else
                target[resolvedFinalKey] = value;
        }
        else if (typeof resolvedFinalKey === "string")
            target[resolvedFinalKey] = value;
        else
            target[finalKey] = value;
        
        return JSON.stringify(obj);
    }
    catch
    {
        return text;
    }
}

function cb_DeleteJSON(text, path)
{
    try
    {
        let obj = JSON.parse(text);
        const keys = cb_JSONParsePath(path);
        
        if (keys.length === 0)
            return text;
        
        let target = obj;
        
        for (let i = 0; i < keys.length - 1; i++)
        {
            let k = keys[i];
            const resolvedKey = cb_JSONResolveKey(target, k);
            
            if (Array.isArray(target) && typeof resolvedKey === "number")
            {
                if (resolvedKey < 0 || resolvedKey >= target.length)
                    return text;
                target = target[resolvedKey];
            }
            else if (target && typeof target === "object" && resolvedKey in target)
                target = target[resolvedKey];
            else
                return text;
        }
        
        let finalKey = keys[keys.length - 1];
        const resolvedFinalKey = cb_JSONResolveKey(target, finalKey);
        
        if (Array.isArray(target) && typeof resolvedFinalKey === "number")
        {
            let index = resolvedFinalKey;
            if (index < 0)
                index = target.length + index;
            if (index >= 0 && index < target.length)
                target.splice(index, 1);
        }
        else if (target && typeof target === "object" && resolvedFinalKey in target)
            delete target[resolvedFinalKey];
        
        return JSON.stringify(obj);
    }
    catch
    {
        return text;
    }
}

function cb_AddJSON(text, path, value)
{
    try
    {
        let obj = JSON.parse(text);
        
        let finalValue = value;
        if (typeof value === "string")
        {
            try { finalValue = JSON.parse(value); }
            catch { finalValue = value; }
        }
        
        if (!path || path.trim() === "")
        {
            if (Array.isArray(obj))
                obj.push(finalValue);
            else if (typeof obj === "object" && obj !== null)
            {
                const newKey = "item" + (Object.keys(obj).length + 1);
                obj[newKey] = finalValue;
            }
            return JSON.stringify(obj);
        }
        
        const keys = cb_JSONParsePath(path);
        let target = obj;
        
        for (let i = 0; i < keys.length - 1; i++)
        {
            let k = keys[i];
            let nextK = keys[i + 1];
            
            const resolvedKey = cb_JSONResolveKey(target, k);
            
            if (Array.isArray(target) && typeof resolvedKey === "number")
            {
                let index = resolvedKey;
                if (index < 0)
                    index = target.length + index;
                if (!target[index] || typeof target[index] !== "object")
                {
                    const nextResolved = cb_JSONResolveKey({}, nextK);
                    target[index] = typeof nextResolved === "number" ? [] : {};
                }
                target = target[index];
            }
            else if (target && typeof target === "object" && resolvedKey in target)
                target = target[resolvedKey];
            else if (!(k in target))
            {
                const nextResolved = cb_JSONResolveKey({}, nextK);
                target[k] = typeof nextResolved === "number" ? [] : {};
                target = target[k];
            }
            else
                target = target[k];
        }
        
        let finalKey = keys[keys.length - 1];
        const resolvedFinalKey = cb_JSONResolveKey(target, finalKey);
        
        if (Array.isArray(target) && typeof resolvedFinalKey === "number")
        {
            let index = resolvedFinalKey;
            if (index < 0)
                index = target.length + index;
            
            if (index >= 0 && index < target.length && !(finalKey in target))
                target[index] = finalValue;
            else
                target.push(finalValue);
        }
        else if (typeof resolvedFinalKey === "string" && !(resolvedFinalKey in target))
            target[resolvedFinalKey] = finalValue;
        else if (finalKey in target)
        {
            const cur = target[finalKey];
            if (Array.isArray(cur))
                cur.push(finalValue);
            else if (cur !== null && typeof cur === "object")
            {
                const newKey = "item" + (Object.keys(cur).length + 1);
                cur[newKey] = finalValue;
            }
            else
                target[finalKey] = [cur, finalValue];
        }
        else
            target[finalKey] = finalValue;
        
        return JSON.stringify(obj);
    }
    catch
    {
        return text;
    }
}

function cb_GetJSONDictionary(text, path)
{
    try
    {
        const data = JSON.parse(text);
        
        let targetObj = data;
        
        if (path && path !== "")
        {
            const keys = cb_JSONParsePath(path);
            
            for (let k of keys)
            {
                const resolvedKey = cb_JSONResolveKey(targetObj, k);
                
                if (resolvedKey === null || resolvedKey === undefined)
                    return "{}";
                
                if (typeof resolvedKey === "number" && Array.isArray(targetObj))
                {
                    if (resolvedKey < 0 || resolvedKey >= targetObj.length)
                        return "{}";
                    targetObj = targetObj[resolvedKey];
                }
                else if (targetObj && typeof targetObj === "object" && resolvedKey in targetObj)
                    targetObj = targetObj[resolvedKey];
                else
                    return "{}";
            }
        }
        
        if (targetObj === null || targetObj === undefined)
            return "{}";
        
        const dictionary = {};
        
        if (Array.isArray(targetObj))
        {
            if (targetObj.length === 0)
                return "{}";
            
            if (typeof targetObj[0] === "object" && targetObj[0] !== null)
            {
                const firstItem = targetObj[0];
                for (let key in firstItem)
                {
                    const value = firstItem[key];
                    if (typeof value === "object" && value !== null)
                        dictionary[key] = JSON.stringify(value);
                    else
                        dictionary[key] = value;
                }
            }
            else
                dictionary["value"] = targetObj[0];
        }
        else if (typeof targetObj === "object" && targetObj !== null)
        {
            for (let key in targetObj)
            {
                const value = targetObj[key];
                if (typeof value === "object" && value !== null)
                    dictionary[key] = JSON.stringify(value);
                else
                    dictionary[key] = value;
            }
        }
        else
            dictionary["value"] = targetObj;
        
        return JSON.stringify(dictionary);
    }
    catch
    {
        return "{}";
    }
}

function cb_JSONResolveKey(obj, key)
{
    const negativeMatch = key.match(/^\[-(\d+)\]$/);
    if (negativeMatch && Array.isArray(obj))
    {
        const index = parseInt(negativeMatch[1]);
        return obj.length - index;
    }
    
    const bracketMatch = key.match(/^\[(\d+)\]$/);
    if (bracketMatch && Array.isArray(obj))
        return parseInt(bracketMatch[1]);
    
    if (!isNaN(key) && Array.isArray(obj))
        return parseInt(key);
    
    const conditionMatch = key.match(/^\[(.+)\]$/);
    if (conditionMatch && Array.isArray(obj))
    {
        const condition = conditionMatch[1];
        return cb_JSONFindIndexByCondition(obj, condition);
    }
    
    return key;
}

function cb_JSONFindIndexByCondition(arr, condition)
{
    if (!Array.isArray(arr))
        return -1;
    
    if (condition.includes(" AND "))
    {
        const conditions = condition.split(" AND ");
        return arr.findIndex(item => conditions.every(c => cb_JSONEvaluateSimpleCondition(item, c.trim())));
    }
    if (condition.includes(" OR "))
    {
        const conditions = condition.split(" OR ");
        return arr.findIndex(item => conditions.some(c => cb_JSONEvaluateSimpleCondition(item, c.trim())));
    }
    
    return arr.findIndex(item => cb_JSONEvaluateSimpleCondition(item, condition));
}

function cb_JSONEvaluateSimpleCondition(item, condition)
{
    const patterns = [
        /^([\w.]+)\s*~=\s*(.+)$/,                    // Contains
        /^([\w.]+)\s*(==?|!=|>=?|<=?)\s*(.+)$/       // Comparison
    ];
    
    for (let pattern of patterns)
    {
        const match = condition.match(pattern);
        if (!match)
            continue;
        
        let field = match[1];
        let operator = match[2];
        let value = match[3];
        
        let fieldValue = item;
        const fieldParts = field.split('.');
        for (let part of fieldParts)
        {
            if (fieldValue === null || fieldValue === undefined)
                break;

            if (Array.isArray(fieldValue) && !isNaN(part))
                fieldValue = fieldValue[parseInt(part)];
            else
                fieldValue = fieldValue[part];
        }
        
        if (fieldValue === undefined)
            return false;
        
        if (operator === "~=")
        {
            const searchValue = value.replace(/^['"]|['"]$/g, "");
            return String(fieldValue).includes(searchValue);
        }
        
        let parsedValue = value;
        if (value === "true")
            parsedValue = true;
        else if (value === "false")
            parsedValue = false;
        else if (value === "null")
            parsedValue = null;
        else if (!isNaN(value) && value !== "")
            parsedValue = Number(value);
        else if ((value.startsWith('"') && value.endsWith('"')) || (value.startsWith("'") && value.endsWith("'")))
            parsedValue = value.slice(1, -1);
        
        switch(operator)
        {
            case "=": case "==": return fieldValue == parsedValue;
            case "!=": return fieldValue != parsedValue;
            case ">": return fieldValue > parsedValue;
            case "<": return fieldValue < parsedValue;
            case ">=": return fieldValue >= parsedValue;
            case "<=": return fieldValue <= parsedValue;
            default: return false;
        }
    }
    
    return false;
}

/* End Format */

/* Start JSON Converter */

function cb_SerialToJSON(In)
{
    try
    {
        if (In === undefined || In === null)
            return JSON.stringify(null);

        if (typeof In === "object" && !(In instanceof String) && !(In instanceof Number))
            return JSON.stringify(In, null, 2);

        let text = String(In).trim();
        
        if (text === "")
            return JSON.stringify("");

        if ((text.startsWith('{') && text.endsWith('}')) || (text.startsWith('[') && text.endsWith(']')))
        {
            try
            {
                return JSON.stringify(JSON.parse(text), null, 2);
            }
            catch
            {
                // Continue
            }
        }
        
        if (text.includes('\n') && !text.includes('<') && !text.includes('=') &&  !text.includes(':') && !text.startsWith('[') && !text.startsWith('{'))
        {
            const lines = text.split('\n').map(line => line.trim()).filter(line => line !== "");
            
            const allNumbers = lines.every(line => !isNaN(line) && line !== "");
            
            if (allNumbers)
            {
                const numbers = lines.map(line => Number(line));
                return JSON.stringify(numbers, null, 2);
            }
            else
                return JSON.stringify(lines, null, 2);
        }
        
        if (text.match(/^[^<[{\n\t].*,[^<[{\n\t].*$/) && !text.includes('\n'))
        {
            const parts = text.split(',').map(p => p.trim());
            
            const allNumbers = parts.every(p => !isNaN(p) && p !== "");
            
            if (allNumbers)
            {
                const numbers = parts.map(p => Number(p));
                return JSON.stringify(numbers, null, 2);
            }
            else
                return JSON.stringify(parts, null, 2);
        }
        
        if (text.match(/<table[\s>]/i) && text.match(/<\/table>/i))
            return cb_ConvertHTMLTableToJSON(text);

        if (text.match(/^\s*\[.*\]\s*$/m) && text.match(/^\s*[\w.+-]+\s*=\s*/m))
            return cb_ConvertINIToJSON(text);

        if (text.startsWith('<') && text.includes('>') && text.includes("</"))
            return cb_ConvertXMLToJSON(text);

        if ((text.includes(',') || text.includes('\t')) && text.includes('\n') && text.split('\n').length >= 2)
            return cb_ConvertCSVToJSON(text);

        if (text.match(/^[\w.+-]+\s*[:=]\s*.+$/m))
            return cb_ConvertKeyValueToJSON(text);

        return JSON.stringify(text);

    }
    catch (er)
    {
        return JSON.stringify({
            __error: true,
            message: er.message,
            originalInput: String(In)
        }, null, 2);
    }
}

function cb_ConvertHTMLTableToJSON(html)
{
    const parser = new DOMParser();
    const doc = parser.parseFromString(html, "text/html");
    const tables = doc.getElementsByTagName("table");
    
    if (tables.length === 0)
        return { error: "No table found" };
    
    const result = [];
    
    for (let table of tables)
    {
        const tableData = [];
        const headers = [];
        const rows = table.getElementsByTagName("tr");
        
        const headerCells = rows[0]?.getElementsByTagName("th");
        if (headerCells && headerCells.length > 0)
            for (let th of headerCells)
                headers.push(th.textContent.trim());
        
        for (let i = (headers.length > 0 ? 1 : 0); i < rows.length; i++)
        {
            const row = rows[i];
            const cells = row.getElementsByTagName("td");
            const rowData = {};
            
            if (headers.length > 0)
            {
                for (let j = 0; j < cells.length && j < headers.length; j++)
                    rowData[headers[j]] = cells[j].textContent.trim();
                tableData.push(rowData);
            }
            else
            {
                const rowArray = [];
                for (let cell of cells)
                    rowArray.push(cell.textContent.trim());
                tableData.push(rowArray);
            }
        }
        
        result.push({
            table: tableData,
            rowCount: tableData.length,
            hasHeaders: headers.length > 0,
            headers: headers
        });
    }
    
    return JSON.stringify(result.length === 1 ? result[0] : result, null, 2);
}

function cb_ConvertINIToJSON(ini)
{
    const result = {};
    let currentSection = null;
    
    const lines = ini.split('\n');
    
    for (let line of lines)
    {
        line = line.trim();
        
        if (line === "" || line.startsWith(';') || line.startsWith('#'))
            continue;
        
        if (line.startsWith('[') && line.endsWith(']'))
        {
            currentSection = line.slice(1, -1);
            if (!result[currentSection])
                result[currentSection] = {};
            continue;
        }
        
        const equalIndex = line.indexOf('=');
        if (equalIndex > 0)
        {
            const key = line.substring(0, equalIndex).trim();
            let value = line.substring(equalIndex + 1).trim();
            
            if (value === "true")
                value = true;
            else if (value === "false")
                value = false;
            else if (value === "null")
                value = null;
            else if (!isNaN(value) && value !== "")
                value = Number(value);
            else if ((value.startsWith('"') && value.endsWith('"')) || (value.startsWith("'") && value.endsWith("'")))
                value = value.slice(1, -1);
            
            if (currentSection) 
                result[currentSection][key] = value;
            else
                result[key] = value;
        }
    }
    
    return JSON.stringify(result, null, 2);
}

function cb_ConvertXMLToJSON(xml)
{
    const parser = new DOMParser();
    const xmlDoc = parser.parseFromString(xml, "text/xml");
    
    const parserError = xmlDoc.querySelector("parsererror");
    if (parserError)
        return JSON.stringify({ error: "Invalid XML", message: parserError.textContent }, null, 2);
    
    function parseNode(node)
    {
        const obj = {};
        
        if (node.attributes && node.attributes.length > 0)
        {
            obj["@attributes"] = {};
            for (let i = 0; i < node.attributes.length; i++)
            {
                const attr = node.attributes[i];
                obj["@attributes"][attr.name] = attr.value;
            }
        }
        
        if (node.childNodes.length > 0)
        {
            let hasText = false;
            let textContent = "";
            
            for (let child of node.childNodes)
            {
                if (child.nodeType === Node.TEXT_NODE)
                {
                    const text = child.textContent.trim();
                    if (text)
                    {
                        hasText = true;
                        textContent += text;
                    }
                }
                else if (child.nodeType === Node.ELEMENT_NODE)
                {
                    const childObj = parseNode(child);
                    const tagName = child.nodeName;
                    
                    if (obj[tagName])
                    {
                        if (!Array.isArray(obj[tagName]))
                            obj[tagName] = [obj[tagName]];
                        obj[tagName].push(childObj);
                    }
                    else 
                        obj[tagName] = childObj;
                }
            }
            
            if (hasText && Object.keys(obj).length === 0)
                return textContent;
            else if (hasText)
                obj["#text"] = textContent;
        }
        
        return obj;
    }
    
    const root = xmlDoc.documentElement;
    const result = {
        [root.nodeName]: parseNode(root)
    };
    
    return JSON.stringify(result, null, 2);
}

function cb_ConvertCSVToJSON(csv)
{
    const lines = csv.split('\n').filter(line => line.trim());
    if (lines.length === 0)
        return JSON.stringify([]);
    
    const delimiter = lines[0].includes('\t') ? '\t' : ',';
    const headers = lines[0].split(delimiter).map(h => h.trim().replace(/^["']|["']$/g, ""));
    
    const result = [];
    
    for (let i = 1; i < lines.length; i++)
    {
        const values = [];
        let inQuotes = false;
        let currentValue = "";
        
        for (let char of lines[i])
        {
            if (char === '"' && !inQuotes)
                inQuotes = true;
            else if (char === '"' && inQuotes)
                inQuotes = false;
            else if (char === delimiter && !inQuotes)
            {
                values.push(currentValue.trim().replace(/^["']|["']$/g, ""));
                currentValue = "";
            }
            else
                currentValue += char;
        }
        values.push(currentValue.trim().replace(/^["']|["']$/g, ""));
        
        const row = {};
        for (let j = 0; j < headers.length && j < values.length; j++)
        {
            let value = values[j];
            
            if (value === "true")
                value = true;
            else if (value === "false")
                value = false;
            else if (value === "null")
                value = null;
            else if (!isNaN(value) && value !== "")
                value = Number(value);
            
            row[headers[j]] = value;
        }
        result.push(row);
    }
    
    return JSON.stringify(result, null, 2);
}

function cb_ConvertKeyValueToJSON(text)
{
    const result = {};
    const lines = text.split('\n');
    
    for (let line of lines)
    {
        line = line.trim();
        if (line === "")
            continue;
        
        const separator = line.includes(':') ? ':' : '=';
        const parts = line.split(separator);
        
        if (parts.length >= 2)
        {
            const key = parts[0].trim();
            let value = parts.slice(1).join(separator).trim();
            
            if (value === "true")
                value = true;
            else if (value === "false")
                value = false;
            else if (value === "null")
                value = null;
            else if (!isNaN(value) && value !== "")
                value = Number(value);
            else if ((value.startsWith('"') && value.endsWith('"')) || (value.startsWith("'") && value.endsWith("'")))
                value = value.slice(1, -1);
            
            result[key] = value;
        }
    }
    
    return JSON.stringify(result, null, 2);
}

/* End JSON Converter */

/* Start Format Storage */

const cb_StorageMemory = {};
let cb_StorageDB = null;

function cb_StorageInitDB()
{
    return new Promise((resolve, reject) =>
    {
        const request = indexedDB.open("WebFormsCore_DB", 1);

        request.onupgradeneeded = () =>
        {
            const db = request.result;
            if (!db.objectStoreNames.contains("cbStorage"))
                db.createObjectStore("cbStorage");
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
        const tx = db.transaction("cbStorage", "readonly");
        const store = tx.objectStore("cbStorage");
        const req = store.getAllKeys();

        req.onsuccess = () =>
        {
            const keys = req.result;

            if (keys.length === 0)
                return resolve();

            const tx2 = db.transaction("cbStorage", "readonly");
            const store2 = tx2.objectStore("cbStorage");
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
        const tx = cb_StorageDB.transaction("cbStorage", "readwrite");
        tx.objectStore("cbStorage").put(value, key);
    }

    return true;
}

function cb_StorageDelete(key)
{
    delete cb_StorageMemory[key];

    if (cb_StorageDB)
    {
        const tx = cb_StorageDB.transaction("cbStorage", "readwrite");
        tx.objectStore("cbStorage").delete(key);
    }
    
    return true;
}

const cb_StorageIsReady = cb_StorageLoadToMemory();

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

/* Start Global Method */

window.PreventDefault = PreventDefault;
window.StopPropagation = StopPropagation;
window.PostBack = PostBack;
window.GetBack = GetBack;
window.PutBack = PutBack;
window.PatchBack = PatchBack;
window.DeleteBack = DeleteBack;
window.HeadBack = HeadBack;
window.OptionsBack = OptionsBack;
window.CommentBack = CommentBack;
window.WasmBack = WasmBack;
window.FrontBack = FrontBack;
window.WebSocketBack = WebSocketBack;
window.SendBack = SendBack;
window.SSEBack = SSEBack;
window.cb_GetElementByElementPlace = cb_GetElementByElementPlace;
window.cb_MasterPages = cb_MasterPages;
window.cb_ServiceWorker = cb_ServiceWorker;
window.cb_ShowConfirm = cb_ShowConfirm;
window.cb_GetMethod = cb_GetMethod;
window.cb_GetModuleMethod = cb_GetModuleMethod;

/* End Global Method */

/* Start Extension */

// In this Section You Can Extend the WebForms Core Technology and Modify the Following Examples. Please Note that Only Use Numbers for Actions, Because Using String Abbreviations for Actions is a Risk due to Possible Conflicts.

async function cb_SetWebFormsValuesExtension(evt, ActionOperation, ActionFeature, Value, vArgs, LastElementPlaceList, TransientDOM)
{
    switch (ActionOperation)
    {
        case '0':
            switch (ActionFeature)
            {
                case '0': alert("Hello " + vArgs[0]); return true;
                case '1': if (LastElementPlaceList) alert("Hello " + vArgs[0]); return true;
                case '2': if (TransientDOM) alert("Hello " + vArgs[0]); return true;

                default:
                    if (WebFormsOptions.AddConsoleMessage)
                        console.warn("This action in webforms value is incomprehensible: " + ActionOperation + ActionFeature + "\nError in value: " + Value);

                    if (WebFormsOptions.AddMessageForIncomprehensibleSetWebFormsValue)
                        cb_ShowMessage(WebFormsOptions.SetWebFormsValueIsIncomprehensibleLang, "problem", WebFormsOptions.MessageDuration);
            }
    }
}

async function cb_SetValueToInputExtension(evt, ActionOperation, ActionFeature, CurrentElement, Value, vArgs)
{
    switch (ActionOperation)
    {
        case '1':
            switch (ActionFeature)
            {
                case '0': console.log(CurrentElement.outerHTML + '|' + vArgs[0]); break;

                default:
                    if (WebFormsOptions.AddConsoleMessage)
                        console.warn("This action in set value to input is incomprehensible: " + ActionOperation + ActionFeature + "\nError in value: " + Value);

                    if (WebFormsOptions.AddMessageForIncomprehensibleSetValueToInput)
                        cb_ShowMessage(WebFormsOptions.SetValueToInputIsIncomprehensibleLang, "problem", WebFormsOptions.MessageDuration);
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
                case '0': return "Hello " + Value;

                default:
                    if (WebFormsOptions.AddConsoleMessage)
                        console.warn("This action in fetch value is incomprehensible: " + ActionOperation + ActionFeature + "\nError in value: " + Value);

                    if (WebFormsOptions.AddMessageForIncomprehensibleFetchValue)
                        cb_ShowMessage(WebFormsOptions.FetchValueIsIncomprehensibleLang, "problem", WebFormsOptions.MessageDuration);
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
                case '0': cb_SetStorage(true, Name, "Hello saved in local storage"); break;
                case '1': cb_SetStorage(false, Name, "Hello saved in session storage"); break;
                case '3': cb_SetStorage(true, Name, CurrentElement.outerHTML); break;

                default:
                    if (WebFormsOptions.AddConsoleMessage)
                        console.warn("This action in save value is incomprehensible: " + ActionOperation + ActionFeature + "\nError in name: " + Name);

                    if (WebFormsOptions.AddMessageForIncomprehensibleSaveValue)
                        cb_ShowMessage(WebFormsOptions.SaveValueIsIncomprehensibleLang, "problem", WebFormsOptions.MessageDuration);
            }
    }
}

async function cb_CheckConditionExtension(evt, Action, Control)
{
    switch (Action)
    {
        case "40": return (Control == "Hello");

        default:
            if (WebFormsOptions.AddConsoleMessage)
                console.warn("This action in check condition is incomprehensible: " + Action + "\nError in control: " + Control);

            if (WebFormsOptions.AddMessageForIncomprehensibleCheckCondition)
                cb_ShowMessage(WebFormsOptions.CheckConditionIsIncomprehensibleLang, "problem", WebFormsOptions.MessageDuration);
    }
}

/* End Extension */
""");

            file.Dispose();
            file.Close();
        }
    }
}
