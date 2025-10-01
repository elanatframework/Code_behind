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
    <script type=""text/javascript"" src=""/script/web-forms.js""></script>
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
@section
@{
    ViewData.Add(""title"",""Error page"");

    int ErrorValue = 0;
    if (Section.GetValue(0).IsNumber())
        ErrorValue = Section.GetValue(0).ToNumber();
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

            file.Write(@"/* WebFormsJS 1.9 - The Front-End Part of WebForms Core Technology, Owned by Elanat (elanat.net) */

/* Start Options */

var PostBackOptions = new Object();
PostBackOptions.UseProgressBar = true;
PostBackOptions.UseConnectionErrorMessage = true;
PostBackOptions.ConnectionErrorMessage = ""Connection Error"";
PostBackOptions.AutoSetSubmitOnClick = true;
PostBackOptions.SendDataOnlyByPostMethod = false;
PostBackOptions.WebFormsTagsBackgroundColor = ""#eee"";
PostBackOptions.SetResponseInsideDivTag = true;
PostBackOptions.ProgressBarStyle = ""width:100%;min-width:300px;max-width:600px;background-color:#eee;margin:2px 0px"";
PostBackOptions.ProgressBarPercentLoadedStyle = ""position:absolute;padding:0px 4px;line-height:22px"";
PostBackOptions.ProgressBarValueStyle = ""height:20px;background-color:#4D93DD;width:0%"";
PostBackOptions.MessageNoneStyle = ""background-color: #AEAEAE"";
PostBackOptions.MessageWarningStyle = ""background-color: #AF4C4C"";
PostBackOptions.MessageProblemStyle = ""background-color: #AFA04C"";
PostBackOptions.MessageHelpStyle = ""background-color: #4C81AF"";
PostBackOptions.MessageSuccessStyle = ""background-color: #4CAF8F"";
PostBackOptions.AddLog = true;
PostBackOptions.AddLogForWebSockets = true;
PostBackOptions.UseSPALink = true;

function cb_GetResponseLocation()
{
    return document.body;
}

/* End Options */

/* Start Check Browser Support */

if (PostBackOptions.AddLog)
    if (!FormData || !('replaceChildren' in document.createElement('div')))
        console.warn(""Your browser is out of date and does not support WebForms Core technology"");

/* End Check Browser Support */

/* Start WebSocket */

var cb_UseWebSocketPath = [];
var cb_UseWebSocket = false;
var cb_WebSockets = {};

function cb_AddWebSocketPath(path)
{
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
    if (PostBackOptions.AddLogForWebSockets)
        console.log(""WebSocket connected, FormAction: "" + formAction);
}

function cb_WebSocketOnClose(evt, formAction)
{
    if (PostBackOptions.AddLogForWebSockets)
        console.log(""WebSocket disconnected, FormAction: "" + formAction);

    delete cb_WebSockets[formAction];
}

function cb_WebSocketOnError(evt, formAction)
{
    if (PostBackOptions.AddLogForWebSockets)
        console.log(""WebSocket error, FormAction: "" + formAction + ""\n"" + evt.data);
}

function cb_WebSocketDoSend(Message)
{
    if (PostBackOptions.AddLogForWebSockets)
        console.log(""WebSocket sent:\n"" + Message);

    for (var formAction in cb_WebSockets)
        if (cb_WebSockets[formAction].readyState === WebSocket.OPEN)
            cb_WebSockets[formAction].send(Message);
}

function cb_WebSocketSet(formAction)
{
    Url = cb_ConvertToWebSocketUrl(formAction)

    if (PostBackOptions.AddLogForWebSockets)
        console.log(""WebSocket request FormAction: "" + formAction);

    var active = false;
    if (cb_WebSockets[formAction] && (cb_WebSockets[formAction].readyState === WebSocket.OPEN || cb_WebSockets[formAction].readyState === WebSocket.CONNECTING))
        active = true;

    if (!active)
    {
        if (PostBackOptions.AddLogForWebSockets)
            console.log(""No active WebSocket for this FormAction, initializing new one..."");

        cb_WebSocketInitialization(Url, formAction);
    }
    else
    {
        if (PostBackOptions.AddLogForWebSockets)
            console.log(""WebSocket already connected or connecting for this FormAction"");
    }
}

/* End WebSocket */

/* Start Event */

function cb_SetPostBackFunctionToSubmit(obj)
{
    if (!PostBackOptions.AutoSetSubmitOnClick)
        return;

    const SubmitInputs = (obj) ? obj.querySelectorAll('input[type=""submit""]') : document.querySelectorAll('input[type=""submit""]');

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

window.onload = function ()
{
    cb_Initialization();
};

function cb_Initialization(obj)
{
    if (obj)
    {
        cb_SetWebFormsTagsValue(obj);
        cb_SetPostBackFunctionToSubmit(obj);
        cb_SetSPALink(obj);
    }
    else
    {
        cb_SetWebFormsTagsValue();
        cb_SetPostBackFunctionToSubmit();
        cb_SetSPALink();
    }

    cb_CleanExpiredCache();
    cb_AddFirstPageSPA();
}

function cb_AddEvent(obj, event, functionWithArgs)
{
    if (obj.hasAttribute(event))
        if (obj.getAttribute(event))
        {
            currentAttribute = obj.getAttribute(event);

            if (event == ""onload"")
            {
                obj.setAttribute(event, functionWithArgs);
                obj.onload();

                if (!obj)
                    return;

                if (obj.getAttribute(event).length > functionWithArgs.length)
                    currentAttribute += "";"" + obj.getAttribute(event).Replace(functionWithArgs, """");
            }

            obj.setAttribute(event, currentAttribute + "";"" + functionWithArgs);
            return;
        }

    obj.setAttribute(event, functionWithArgs);
    if (event == ""onload"")
        obj.onload();
}

function cb_RemoveEvent(obj, event, functionName)
{
    var currentEvent = obj.getAttribute(event);

    if (currentEvent)
    {
        var regex = new RegExp(functionName + '\\(.*?\\);?', 'g');

        var updatedEvent = currentEvent.replace(regex, '');

        obj.setAttribute(event, updatedEvent.trim());
    }
}

var cb_EventRegistry = {};

function cb_AddEventListener(obj, event, functionName, args = [])
{
    var callback = function (evt)
    {
        // The Three Dot Character (...) Is Spread Operator For Expands Args Array
        functionName.apply(this, [evt, ...args]);
    };

    obj.addEventListener(event, callback);

    // Generate A Unique ID If The Element Doesn't Have
    var objId = obj.id;
    if (!objId)
    {
        objId = 'cb_' + Math.random().toString(36).substr(2, 9);
        obj.id = objId;
        // Store As Data Attribute For Easier Lookup During DOM Replacement
        obj.setAttribute('data-cb-id', objId);
    }

    if (!cb_EventRegistry[objId])
        cb_EventRegistry[objId] = {};

    if (!cb_EventRegistry[objId][event])
        cb_EventRegistry[objId][event] = [];

    // Check If This Exact Listener Already Exists
    const existingListener = cb_EventRegistry[objId][event].find(
        entry => entry.functionName === functionName && JSON.stringify(entry.args) === JSON.stringify(args)
    );

    if (!existingListener)
        cb_EventRegistry[objId][event].push({ callback, functionName, args });
}

function cb_RemoveEventListener(obj, event, functionName)
{
    var objId = obj.id || obj;
    var listeners = cb_EventRegistry[objId]?.[event];

    if (listeners)
    {
        const listenerIndex = listeners.findIndex((entry) => entry.functionName === functionName);

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

function cb_PreServedEevent(evt)
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
        // ... Has More Event Properties

        if (typeof evt.preventDefault === 'function')
            captured.preventDefault = function (){evt.preventDefault();};

        if (typeof evt.stopPropagation === 'function')
            captured.stopPropagation = function () {evt.stopPropagation();};

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
    if (!PostBackOptions.UseSPALink)
        return;

    const links = (obj) ? obj.querySelectorAll('a') : document.body.querySelectorAll('a');

    links.forEach(link =>
    {
        var targetAttr = link.getAttribute('target');
        var hrefAttr = link.getAttribute('href');

        if (hrefAttr && !hrefAttr.includes('://') && !hrefAttr.startsWith('mailto:') && !hrefAttr.startsWith('tel:') && (!targetAttr || targetAttr === '_self'))
            link.setAttribute('onclick', `PreventDefault(event);GetBack(event, '${hrefAttr}');`);
    });
}

/* End Event */

/* Start Post-Back */

function PostBack(evt, ViewState)
{
    evt = evt || window.event;
    evt = cb_PreServedEevent(evt);

    var obj = evt.currentTarget;

    // Set Form Value
    var Form = obj;
    do
    {
        if (!Form.parentNode)
            return;

        Form = Form.parentNode;
    }
    while (Form.nodeName.toLowerCase() != ""form"");

    if (Form.nodeName.toLowerCase() != ""form"")
        return;

    var FormMethod = (PostBackOptions.SendDataOnlyByPostMethod) ? ""POST"" : (Form.hasAttribute(""method"") ? Form.getAttribute(""method"") : ""GET"");
    var FormAction = Form.hasAttribute(""action"")? Form.getAttribute(""action"") : """";

    // Chek Form Multi Part
    var FormIsMultiPart = false;
    if (Form.hasAttribute(""enctype"") && (FormMethod.toLowerCase() == ""post"" || FormMethod.toLowerCase() == ""put""))
        if (Form.getAttribute(""enctype"") == ""multipart/form-data"")
            FormIsMultiPart = true;


    // Set Progress Tag
    if (PostBackOptions.UseProgressBar)
        cb_SetProgressTag(obj, Form);


    // Set Input Value
    var TagSubmitValue = null;
    switch (obj.nodeName.toLowerCase())
    {
        case ""input"": TagSubmitValue = (obj.getAttribute(""value"")) ? obj.getAttribute(""value"") : """"; break;
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
    var RequestNameForCache = ""<"";
    var RequestName = (FormAction == """") ? window.location.pathname : FormAction;
    if (FormAction.length > 0)
    {
        if (FormAction.substring(0, 1) == '#')
            RequestName = window.location.pathname + FormAction;

        if (FormAction.Contains(""#""))
            RequestNameForCache = ""#"" + FormAction.GetTextAfter(""#"");
    }
    if (obj.getAttribute(""name""))
        RequestName = obj.getAttribute(""name"") + ""|"" + TagSubmitValue + ""|"" + RequestName;

    // Check Cache
    if (cb_UsedCache(evt, RequestName, RequestNameForCache))
    {
        // Reset Input Type
        setTimeout(function () { (OldObjectType == ""submit"") ? obj.type = ""submit"" : obj.type; }, 1);

        if (obj.hasAttribute(""main-type""))
            obj.removeAttribute(""main-type"");

        return;
    }

    // Using WebSocket Protocol
    if (window.WebSocket && (cb_UseWebSocket || Form.hasAttribute(""usewebsocket"") || (cb_UseWebSocketPath.indexOf(FormAction) >= 0)))
    {
        if (cb_UseWebSocket == '@')
            cb_UseWebSocket = false;

        cb_WebSocketSet(FormAction);

        if (cb_WebSockets[FormAction])
        {
            var formDataSerialize = cb_FormDataSerialize(Form, obj.getAttribute(""name""), TagSubmitValue, OldObjectType, false);

            if (cb_WebSockets[FormAction].readyState === WebSocket.OPEN)
                cb_WebSocketDoSend(formDataSerialize);
            else
            {
                cb_WebSockets[FormAction].onopen = function ()
                {
                    cb_WebSocketDoSend(formDataSerialize);
                };
            }

            cb_WebSockets[FormAction].onmessage = function (event)
            {
                var WebSocketResult = event.data;
                cb_SetResponse(evt, WebSocketResult, ViewState, RequestName);

                Form.focus();

                // Reset Input Type
                setTimeout(function () { (OldObjectType == ""submit"") ? obj.type = ""submit"" : obj.type; }, 1);

                if (obj.hasAttribute(""main-type""))
                    obj.removeAttribute(""main-type"");

                if (PostBackOptions.AddLogForWebSockets)
                    console.log(""WebSocket server response:\n"" + event.data);
            };
        }

        // Reset Input Type
        setTimeout(function () { (OldObjectType == ""submit"") ? obj.type = ""submit"" : obj.type; }, 1);

        if (obj.hasAttribute(""main-type""))
            obj.removeAttribute(""main-type"");

        return;
    }

    // Using Http Protocol
    var XMLHttp = new XMLHttpRequest();
    XMLHttp.onreadystatechange = function ()
    {
        if (XMLHttp.readyState == 4 && XMLHttp.status == 200)
        {
            var HttpResult = XMLHttp.responseText;
            cb_SetResponse(evt, HttpResult, ViewState, RequestName);

            Form.focus();

            // Reset Input Type
            setTimeout(function () { (OldObjectType == ""submit"") ? obj.type = ""submit"" : obj.type; }, 1);

            if (obj.hasAttribute(""main-type""))
                obj.removeAttribute(""main-type"");
        }
    }

    XMLHttp.onerror = function ()
    {
        if (XMLHttp.status != 0 && (XMLHttp.readyState == 0 || XMLHttp.status > 200))
        {
            if (PostBackOptions.UseConnectionErrorMessage)
            {
                var BErrorTag = document.createElement(""b"");
                BErrorTag.innerText = ""Connection Error"";
                document.body.prepend(BErrorTag);
            }

            // Clean Progress Value
            if (PostBackOptions.UseProgressBar)
                cb_CleanProgressValue();
        }

        // Reset Input Type
        setTimeout(function () { (OldObjectType == ""submit"") ? obj.type = ""submit"" : obj.type; }, 1);

        if (obj.hasAttribute(""main-type""))
            obj.removeAttribute(""main-type"");
    }

    var formDataSerialize = cb_FormDataSerialize(Form, obj.getAttribute(""name""), TagSubmitValue, OldObjectType, FormIsMultiPart);
    if ((FormMethod.toLowerCase() != ""post"") && (FormMethod.toLowerCase() != ""put""))
    {
        FormAction = cb_AddQueryToUrl(FormAction, formDataSerialize);
        formDataSerialize = """";
    }
        
    XMLHttp.open(FormMethod, FormAction, true);

    if (PostBackOptions.UseProgressBar && cb_HasFileInput(Form))
        XMLHttp.upload.addEventListener(""progress"", cb_ProgressHandler, false);

    if (!FormIsMultiPart)
        XMLHttp.setRequestHeader(""Content-Type"", ""application/x-www-form-urlencoded"");

    XMLHttp.setRequestHeader(""Post-Back"", ""true"");

    formDataSerialize ? XMLHttp.send(formDataSerialize) : XMLHttp.send();
}

/* End Post-Back */

/* Start Request And Response */

function cb_RequestAndResponse(evt, FormAction, ViewState, Method)
{
    evt = evt || window.event;
    evt = cb_PreServedEevent(evt);

    var FormMethod = (PostBackOptions.SendDataOnlyByPostMethod) ? ""POST"" : Method;

    // Set Form Value
    var Form = FormAction;

    var OldFormAction = FormAction;

    if (FormAction)
    {
        if (typeof FormAction === ""object"")
        {
            do
            {
                if (!Form.parentNode)
                    return;

                Form = Form.parentNode;
            }
            while (Form.nodeName.toLowerCase() != ""form"");

            if (Form.nodeName.toLowerCase() != ""form"")
                if (body.getElementsByTagName(""form"").length > 0)
                    Form = body.getElementsByTagName(""form"")[0];

            FormMethod = (PostBackOptions.SendDataOnlyByPostMethod) ? ""POST"" : (Form.hasAttribute(""method"") ? Form.getAttribute(""method"") : ""GET"");
            FormAction = Form.getAttribute(""action"");
        }
    }
    else
        FormAction = """";

    // Set Input Value
    var TagSubmitValue = null;
    if (typeof OldFormAction === ""object"")
        switch (OldFormAction.nodeName.toLowerCase())
        {
            case ""input"": TagSubmitValue = (OldFormAction.getAttribute(""value"")) ? OldFormAction.getAttribute(""value"") : """"; break;
            case ""select"": TagSubmitValue = (OldFormAction.options[OldFormAction.selectedIndex].value) ? OldFormAction.options[OldFormAction.selectedIndex].value : """";
        }

    var OldObjectType;
    if (typeof OldFormAction === ""object"")
        if (OldFormAction.getAttribute(""type""))
            if (OldFormAction.getAttribute(""type"").toLowerCase() == ""submit"")
            {
                OldObjectType = OldFormAction.type.toLowerCase();
                OldFormAction.setAttribute(""type"", ""button"");
                OldFormAction.setAttribute(""main-type"", ""submit"");
            }

    // Create Request Name
    var RequestNameForCache = ""<"";
    var RequestName = (FormAction == """") ? window.location.pathname : FormAction;
    if (FormAction.length > 0)
    {
        if (FormAction.substring(0, 1) == '#')
            RequestName = window.location.pathname + FormAction;

        if (FormAction.Contains(""#""))
            RequestNameForCache = ""#"" + FormAction.GetTextAfter(""#"");
    }

    // Check Cache
    if (cb_UsedCache(evt, RequestName, RequestNameForCache))
        return;

    var formHasWebSocketAttribute = false;
    if (typeof OldFormAction === ""object"")
        if (Form.hasAttribute(""usewebsocket""))
            formHasWebSocketAttribute = true;

    // Using WebSocket Protocol
    if (window.WebSocket && (cb_UseWebSocket || formHasWebSocketAttribute || (cb_UseWebSocketPath.indexOf(FormAction) >= 0)))
    {
        if (cb_UseWebSocket == '@')
            cb_UseWebSocket = false;

        cb_WebSocketSet(FormAction);

        if (cb_WebSockets[FormAction])
        {
            var formDataSerialize = cb_FormDataSerialize(Form, OldFormAction.getAttribute(""name""), TagSubmitValue, OldObjectType, false);

            if (cb_WebSockets[FormAction].readyState === WebSocket.OPEN)
                cb_WebSocketDoSend(formDataSerialize);
            else
            {
                cb_WebSockets[FormAction].onopen = function ()
                {
                    cb_WebSocketDoSend(formDataSerialize);
                };
            }

            cb_WebSockets[FormAction].onmessage = function (event)
            {
                var WebSocketResult = event.data;
                cb_SetResponse(evt, WebSocketResult, ViewState, RequestName);

                if (typeof OldFormAction === ""object"")
                {
                    // Reset Input Type
                    setTimeout(function () { (OldObjectType == ""submit"") ? OldFormAction.type = ""submit"" : OldFormAction.type; }, 1);

                    if (OldFormAction.hasAttribute(""main-type""))
                        OldFormAction.removeAttribute(""main-type"");
                }

                if (PostBackOptions.AddLogForWebSockets)
                    console.log(""WebSocket server response:\n"" + event.data);
            };
        }

        if (typeof OldFormAction === ""object"")
        {
            // Reset Input Type
            setTimeout(function () { (OldObjectType == ""submit"") ? OldFormAction.type = ""submit"" : OldFormAction.type; }, 1);

            if (OldFormAction.hasAttribute(""main-type""))
                OldFormAction.removeAttribute(""main-type"");
        }

        return;
    }

    // Using Http Protocol
    var XMLHttp = new XMLHttpRequest();
    XMLHttp.onreadystatechange = function ()
    {
        if (XMLHttp.readyState == 4 && XMLHttp.status == 200)
        {
            var HttpResult = XMLHttp.responseText;
            cb_SetResponse(evt, HttpResult, ViewState, RequestName);

            if (evt.currentTarget.tagName)
            {
                var IsSPALink = evt.currentTarget.tagName.toLowerCase() == 'a';
                if (IsSPALink)
                {
                    const pathname = evt.currentTarget.getAttribute(""href"");

                    // Add New Page State
                    cb_PageManager.Add(pathname, document.title, document.body.innerHTML, window.scrollX, window.scrollY);

                    // Change URL
                    window.history.pushState({ url: pathname }, null, pathname);
                }
            }

            if (typeof OldFormAction === ""object"")
            {
                // Reset Input Type
                setTimeout(function () { (OldObjectType == ""submit"") ? OldFormAction.type = ""submit"" : OldFormAction.type; }, 1);

                if (OldFormAction.hasAttribute(""main-type""))
                    OldFormAction.removeAttribute(""main-type"");
            }
        }
    }

    XMLHttp.onerror = function ()
    {
        if (XMLHttp.status != 0 && (XMLHttp.readyState == 0 || XMLHttp.status > 200))
        {
            if (PostBackOptions.UseConnectionErrorMessage)
            {
                var BErrorTag = document.createElement(""b"");
                BErrorTag.innerText = ""Connection Error"";
                document.body.prepend(BErrorTag);
            }
        }

        if (typeof OldFormAction === ""object"")
        {
            // Reset Input Type
            setTimeout(function () { (OldObjectType == ""submit"") ? OldFormAction.type = ""submit"" : OldFormAction.type; }, 1);

            if (OldFormAction.hasAttribute(""main-type""))
                OldFormAction.removeAttribute(""main-type"");
        }
    }

    XMLHttp.open(FormMethod, FormAction, true);

    XMLHttp.setRequestHeader(""Content-Type"", ""application/x-www-form-urlencoded"");

    XMLHttp.setRequestHeader(""Post-Back"", ""true"");

    XMLHttp.send();
}
function GetBack(evt, FormAction, ViewState)
{
    cb_RequestAndResponse(evt, FormAction, ViewState, ""GET"");
}

function PutBack(evt, FormAction, ViewState)
{
    cb_RequestAndResponse(evt, FormAction, ViewState, ""PUT"");
}

function PatchBack(evt, FormAction, ViewState)
{
    cb_RequestAndResponse(evt, FormAction, ViewState, ""PATCH"");
}

function DeleteBack(evt, FormAction, ViewState)
{
    cb_RequestAndResponse(evt, FormAction, ViewState, ""DELETE"");
}

function HeadBack(evt, FormAction, ViewState)
{
    cb_RequestAndResponse(evt, FormAction, ViewState, ""HEAD"");
}

function OptionsBack(evt, FormAction, ViewState)
{
    cb_RequestAndResponse(evt, FormAction, ViewState, ""OPTIONS"");
}

function TraceBack(evt, FormAction, ViewState)
{
    cb_RequestAndResponse(evt, FormAction, ViewState, ""TRACE"");
}

function ConnectBack(evt, FormAction, ViewState)
{
    cb_RequestAndResponse(evt, FormAction, ViewState, ""CONNECT"");
}

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
        TmpDiv.innerHTML = ResponseResult.toDOM();
        cb_AppendJavaScriptTag(ResponseResult);

		if (ViewState)
		{
			if (typeof ViewState === ""string"")
			{
				var ViewStateObject = cb_GetElementByElementPlace(ViewState);
                ViewStateObject.replaceChildren(TmpDiv);
				cb_Initialization(ViewStateObject.getElementsByTagName(""div"")[0]);
                if (!PostBackOptions.SetResponseInsideDivTag)
                {
                    var DivElement = ViewStateObject.getElementsByTagName(""div"")[0];
                    DivElement.replaceChildren(...DivElement.childNodes);
                }
			}
			else if (typeof ViewState === ""object"")
			{
                ViewState.replaceChildren(TmpDiv);
				cb_Initialization(ViewState.getElementsByTagName(""div"")[0]);
                if (!PostBackOptions.SetResponseInsideDivTag)
                {
                    var DivElement = ViewState.getElementsByTagName(""div"")[0];
                    DivElement.replaceChildren(...DivElement.childNodes);
                }
			}
			else
			{
				cb_GetResponseLocation().prepend(TmpDiv);
				cb_Initialization(cb_GetResponseLocation().getElementsByTagName(""div"")[0]);
                if (!PostBackOptions.SetResponseInsideDivTag)
                {
                    var DivElement = cb_GetResponseLocation().getElementsByTagName(""div"")[0];
                    DivElement.replaceChildren(...DivElement.childNodes);
                }
			}
		}
		else
        {
            cb_GetResponseLocation().replaceChildren(...(PostBackOptions.SetResponseInsideDivTag ? [TmpDiv] : TmpDiv.childNodes));
			cb_Initialization(cb_GetResponseLocation());
        }
	}
}

/* End Set Response Value */

/* Start Tag-Back */

function TagBack(evt, OutputPlace)
{
    evt = evt || window.event;

    var ElementPlace = cb_GetElementByElementPlace(OutputPlace);
    var ActionControls = ElementPlace.getAttribute(""ac"");
    cb_SetWebFormsValues(evt, """", ActionControls, false, true);
}

/* End Tag-Back */

/* Start WebSocket-Back */

function WebSocketBack(evt, Path)
{
    cb_AddWebSocketPath(Path);
    GetBack(evt, Path);
}

/* End WebSocket-Back */

/* Start Form Data Serialize */

function cb_FormDataSerialize(form, TagSubmitName, TagSubmitValue, TagSubmitType, FormIsMultiPart)
{   
    var FormString = """";
    var TmpFormData = new FormData();

    if (!form || form.nodeName.toLowerCase() != ""form"")
        return;

    var i, j;
    for (i = form.elements.length - 1; i >= 0; i = i - 1)
    {
        if (form.elements[i].name === """")
            continue;

        switch (form.elements[i].nodeName.toLowerCase())
        {
            case ""input"":
                switch (form.elements[i].type.toLowerCase())
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
                            if (FormIsMultiPart)
                                TmpFormData.append(form.elements[i].name, form.elements[i].value);
                            else
                                FormString += form.elements[i].name + ""="" + form.elements[i].value + ""&"";
                        }
                        break;
                    case ""checkbox"":
                    case ""radio"":
                        if (form.elements[i].checked)
                        {
                            if (FormIsMultiPart)
                                TmpFormData.append(form.elements[i].name, form.elements[i].value);
                            else
                                FormString += form.elements[i].name + ""="" + form.elements[i].value + ""&"";
                        }
                        break;
                    case ""file"":
                        {
                            var files = form.elements[i].files;

                            if (files.length == 0)
                                break;

                            var file = files[0];

                            if (FormIsMultiPart)
                                TmpFormData.append(form.elements[i].name, file, file.name);
                            else
                                FormString += form.elements[i].name + ""="" + file, file.name + ""&"";
                        }
                        break;
                }
                break;
            case ""file"":
                break;
            case ""textarea"":
                {
                    if (FormIsMultiPart)
                        TmpFormData.append(form.elements[i].name, form.elements[i].value);
                    else
                        FormString += form.elements[i].name + ""="" + form.elements[i].value + ""&"";
                }
                break;
            case ""select"":
                switch (form.elements[i].type.toLowerCase())
                {
                    case ""select-one"":
                        {
                            if (FormIsMultiPart)
                                TmpFormData.append(form.elements[i].name, form.elements[i].value);
                            else
                                FormString += form.elements[i].name + ""="" + form.elements[i].value + ""&"";
                        }
                        break;
                    case ""select-multiple"":
                        for (j = form.elements[i].options.length - 1; j >= 0; j = j - 1)
                        {
                            if (form.elements[i].options[j].selected)
                            {
                                if (FormIsMultiPart)
                                    TmpFormData.append(form.elements[i].name, form.elements[i].options[j].value);
                                else
                                    FormString += form.elements[i].name + ""="" + form.elements[i].options[j].value + ""&"";
                            }
                        }
                        break;
                }
                break;
            case ""button"":
                switch (form.elements[i].type.toLowerCase())
                {
                    case ""reset"":
                    case ""submit"":
                    case ""button"":
                        {
                            if (FormIsMultiPart)
                                TmpFormData.append(form.elements[i].name, form.elements[i].value);
                            else
                                FormString += form.elements[i].name + ""="" + form.elements[i].value + ""&"";
                        }
                        break;
                }
                break;
        }
    }

    if (TagSubmitType == ""submit"")
    {
        if (FormIsMultiPart)
            TmpFormData.append(TagSubmitName, TagSubmitValue);
        else
            FormString += TagSubmitName + ""="" + TagSubmitValue;
    }
    else if (!FormIsMultiPart)
        if (FormString.length > 0)
            FormString = FormString.substring(0, FormString.length - 1);

    return (FormIsMultiPart) ? TmpFormData : FormString;
}

/* End Form Data Serialize */

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

function cb_AppendJavaScriptTag(HtmlSource)
{
    var ScriptList = cb_ExtractScriptTags(HtmlSource);

    for (var i = 0; i < ScriptList.length; i++)
        document.body.appendChild(ScriptList[i]);
}

/* End Append Java Script */

/* Start Progress Bar */
function cb_ProgressHandler(event)
{
    var Percent = (event.loaded / event.total) * 100;

    if (event.total >= 1048576)
        document.getElementById(""div_ProgressPercentLoaded"").textContent = (event.loaded / 1048576).toFixed(1) + ""("" + Math.round(Percent) + ""%)"" + "" / "" + (event.total / 1048576).toFixed(1) + "" MB"";
    else
        document.getElementById(""div_ProgressPercentLoaded"").textContent = (event.loaded / 1024).toFixed(1) + ""("" + Math.round(Percent) + ""%)"" + "" / "" + (event.total / 1024).toFixed(1) + "" KB"";

    document.getElementById(""div_ProgressUploadValue"").style.width = Math.round(Percent) + ""%"";
}

function cb_SetProgressTag(obj, form)
{
    if (!cb_HasFileInput(form))
        return;

    if (!document.getElementById(""div_ProgressUpload""))
    {
        var DivProgressUpload = document.createElement(""div"");
        DivProgressUpload.id = ""div_ProgressUpload"";
        DivProgressUpload.setAttribute(""style"", PostBackOptions.ProgressBarStyle);

        var DivProgressPercentLoaded = document.createElement(""div"");
        DivProgressPercentLoaded.id = ""div_ProgressPercentLoaded"";
        DivProgressPercentLoaded.setAttribute(""style"", PostBackOptions.ProgressBarPercentLoadedStyle);

        var DivProgressUploadValue = document.createElement(""div"");
        DivProgressUploadValue.id = ""div_ProgressUploadValue"";
        DivProgressUploadValue.setAttribute(""style"", PostBackOptions.ProgressBarValueStyle);

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
    const WebFormsTags = (obj) ? obj.querySelectorAll('web-forms') : document.querySelectorAll('web-forms');

    WebFormsTags.forEach(function (WebForms)
    {
        if (WebForms.hasAttribute(""done""))
            return;

        WebForms.setAttribute(""done"", ""true"");

        if (WebForms.hasAttribute(""src""))
        {
            WebForms.style.backgroundColor = PostBackOptions.WebFormsTagsBackgroundColor;
            if (WebForms.hasAttribute(""width""))
                WebForms.style.width = WebForms.getAttribute(""width"");
            if (WebForms.hasAttribute(""height""))
                WebForms.style.height = WebForms.getAttribute(""height"");

            var Src = WebForms.getAttribute(""src"");
            if (Src)
                GetBack(document, Src, WebForms);

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

/* Start Fetch Web-Forms */

function cb_SetWebFormsValues(evt, RequestName, WebFormsValues, UsePostBack, WithoutWebFormsSection)
{
    // Initialization to Index
    var StartIndex = RequestName.Contains(""#"") ? RequestName.GetTextAfter(""#"") : """";
    var IndexHasStarted = ((StartIndex == """") || (StartIndex == ""0""));
    var StartIndexIsNumber = StartIndex.IsNumber();
    var StartIndexIndex = StartIndexIsNumber ? parseInt(StartIndex) : 0;
    var IndexForStartIndex = 1;

    // Condition
    var ConditionHasStart = false;
    var ConditionIsTrue = false;
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

    for (var i = 0; i < WebFormsList.length; i++)
    {
        WebFormsList[i] = WebFormsList[i].FullTrim();

        if (!WebFormsList[i])
            continue;

        // Checking Condition
        if (ConditionPeriodMiliSecond > 0)
        {
            // Add Condition
            if (ConditionAsyncList.length == 0)
                ConditionAsyncList.push(WebFormsList[i - 1].GetTextAfter("")""));

            if (WebFormsList[i] == '{')
            {
                ConditionAsyncList.push(WebFormsList[i]);
                ConditionBracketHasStart = true;
                continue;
            }

            if (ConditionBracketHasStart)
            {
                if (WebFormsList[i] == '}')
                {
                    ConditionAsyncList.push(WebFormsList[i]);

                    const TmpConditionAsyncList = ConditionAsyncList;

                    // Is Async
                    cb_WaitForCondition(ConditionPeriodMiliSecond, cb_CheckCondition, evt, ConditionAsyncList[0]).then(() =>
                    {
                        TmpConditionAsyncList.shift();
                        cb_SetWebFormsValues(evt, """", TmpConditionAsyncList.join('\n'), true, true);
                    }).catch(() => { });

                    ConditionBracketHasStart = false;
                    ConditionPeriodMiliSecond = -1;
                    ConditionAsyncList = new Array();
                }
                else
                    ConditionAsyncList.push(WebFormsList[i]);
            }
            else
            {
                ConditionAsyncList.push(WebFormsList[i]);

                const TmpConditionAsyncList = ConditionAsyncList;

                // Is Async
                cb_WaitForCondition(ConditionPeriodMiliSecond, cb_CheckCondition, evt, ConditionAsyncList[0]).then(() =>
                {
                    TmpConditionAsyncList.shift();
                    cb_SetWebFormsValues(evt, """", TmpConditionAsyncList.join('\n'), true, true);
                }).catch(() => { });

                ConditionPeriodMiliSecond = -1;
                ConditionAsyncList = new Array();
            }
            continue;
        }

        if (ConditionHasStart)
        {
            if (WebFormsList[i] == '{')
            {
                ConditionBracketHasStart = true;
                continue;
            }

            if (ConditionBracketHasStart)
            {
                if (WebFormsList[i] == '}')
                {
                    ConditionBracketHasStart = false;
                    ConditionHasStart = false;
                    ConditionIsTrue = false;
                    continue;
                }
            }

            if (!ConditionIsTrue)
            {
                if (!ConditionBracketHasStart)
                {
                    ConditionHasStart = false;
                    ConditionIsTrue = false;
                    ConditionBracketHasStart = false;
                }
                continue;
            }           
        }

        // Checking Index Process
        if (IndexHasStarted)
        {
            if (WebFormsList[i].substring(0, 1) == '#')
                break;
        }
        else
        {
            if (StartIndexIsNumber)
            {
                if (WebFormsList[i].substring(0, 1) == '#')
                    if (StartIndexIndex == IndexForStartIndex)
                        IndexHasStarted = true;
                    else
                        IndexForStartIndex++;
            }   
            else
                if (WebFormsList[i] == (""#="" + StartIndex))
                    IndexHasStarted = true;

                continue;
        }

        var PreRunner = new Array();
        var FirstChar = WebFormsList[i].substring(0, 1);
        var PreRunnerIndexer = 0;
        while ((FirstChar == ':') || (FirstChar == '(') || (FirstChar == ','))
        {
            PreRunner[PreRunnerIndexer++] = WebFormsList[i].GetTextBefore("")"");
            WebFormsList[i] = WebFormsList[i].GetTextAfter("")"");
            FirstChar = WebFormsList[i].substring(0, 1);
        }

        if (FirstChar == ';')
            break;

        var SecondChar = WebFormsList[i].substring(1, 2);
        switch (FirstChar)
        {
            case '{':
                if (SecondChar == '(')                   
                    ConditionPeriodMiliSecond = WebFormsList[i].GetTextAfter(""("").GetTextBefore("")"");
                else
                    ConditionHasStart = true;

                if (ConditionPeriodMiliSecond == 0)
                {
                    while (!cb_CheckCondition(evt, WebFormsList[i]))
                    { 
                    }
                    ConditionIsTrue = true;
                }
                else if (ConditionPeriodMiliSecond == -1)
                    ConditionIsTrue = cb_CheckCondition(evt, WebFormsList[i]);
                continue;

            case '_':
                var ScriptValue = WebFormsList[i].GetTextAfter(""="").Replace(""$[ln];"", ""\n"").FullTrim();
                cb_SetPreRunnerQueueForEval(PreRunner, ScriptValue);
                continue;

            case `@`:
                cb_SaveValue(evt, WebFormsList[i].substring(1, 2), WebFormsList[i].substring(2, 3), WebFormsList[i].substring(3), LastElementPlaceList, TransientDOM);
                continue;

            case '&':
                var GoToValue = WebFormsList[i].GetTextAfter(""="");
                var LineIndex = GoToValue.GetTextBefore(""|"");
                var Repeat = GoToValue.GetTextAfter(""|"");
                var InitialRepeat = Repeat;

                if (Repeat.Contains(""|""))
                {
                    InitialRepeat = Repeat.GetTextAfter(""|"");
                    Repeat = GoToValue.GetTextBefore(""|"");
                }

                if (parseInt(Repeat, 10) == 0)
                {
                    WebFormsList[i] = ""&="" + LineIndex + ""|"" + InitialRepeat;
                    continue;
                }
                Repeat = parseInt(Repeat, 10) - 1;

                WebFormsList[i] = ""&="" + LineIndex + ""|"" + Repeat + ""|"" + InitialRepeat;

                if (LineIndex.substring(0,1) == '#')
                {
                    i = 0;
                    IndexHasStarted = false;
                    StartIndex = LineIndex.GetTextAfter(""#"");
                    StartIndexIsNumber = StartIndex.IsNumber();
                    StartIndexIndex = StartIndexIsNumber ? parseInt(StartIndex) : 0;
                    IndexForStartIndex = 1;
                }
                else
                {
                    WebFormsList[i] = ""&="" + LineIndex + ""|"" + Repeat;
                    var LineIndexInt = parseInt(LineIndex, 10);
                    if (LineIndexInt >= 0)
                        i = LineIndexInt;
                    else
                        i = i + LineIndexInt;
                }
                continue;

            case 'r':
                var CacheKeyValue = WebFormsList[i].GetTextAfter(""="");
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
                        var DurationValue = WebFormsList[i].GetTextAfter(""="");

                        if (DurationValue != '*')
                        {
                            var UntilDate = new Date();
                            UntilDate.setSeconds(UntilDate.getSeconds() + parseInt(DurationValue));

                            localStorage.setItem(RequestName + ""-date"", UntilDate);
                        }
                        continue;
                    case 'u':
                        window.history.replaceState({ url: WebFormsList[i].GetTextAfter(""="") }, null, WebFormsList[i].GetTextAfter(""=""));
                        continue;
                }
                break;

            case 'e':
                switch (SecondChar)
                {
                    case 'w':
                        if (WebFormsList[i].GetTextAfter(""="") == ""@"")
                            cb_UseWebSocket = ""@"";
                        else
                            cb_UseWebSocket = (WebFormsList[i].GetTextAfter(""="") == ""1"");
                        continue;
                }
                break;

            case 'u':
                switch (SecondChar)
                {
                    case 'w':
                        cb_AddWebSocketPath(WebFormsList[i].GetTextAfter(""=""));
                        continue;
                }
                break;

            case 'h':
                switch (SecondChar)
                {
                    case 't':
                        document.title = WebFormsList[i].GetTextAfter(""="");
                        continue;
                }
                break;

            case 'a':
                switch (SecondChar)
                {
                    case 'l':
                        var [text, type, title, okText] = WebFormsList[i].GetTextAfter(""="").split(""|"");

                        if (!type)
                            type = ""none"";
                        if (!title)
                            title = ""Alert"";
                        if (!okText)
                            okText = ""OK"";

                        cb_ShowAlert(text, type, title, okText);
                        continue;
                }
                break;

            case 'm':
                switch (SecondChar)
                {
                    case 'e':
                        var [text, type, duration] = WebFormsList[i].GetTextAfter(""="").split(""|"");

                        if (!type)
                            type = ""none"";
                        if (!duration)
                            duration = 0;

                        cb_ShowMessage(text, type, duration);
                        continue;
                }
                break;

            case 't':
                switch (SecondChar)
                {
                    case 'd':
                        if (WebFormsList[i].GetTextAfter(""="") == ';')
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
                            TransientDOMPlace = WebFormsList[i].GetTextAfter(""="");
                            var HtmlDOM = cb_GetElementByElementPlace(TransientDOMPlace)

                            TransientDOM = HtmlDOM.cloneNode(true);

                            // State Preservation
                            TransientDOM = cb_SetStatePreservation(HtmlDOM, TransientDOM);
                        }
                }
        }

        // Extension
        if (cb_SetWebFormsValuesExtension(evt, FirstChar, SecondChar, WebFormsList[i].GetTextAfter(""=""), LastElementPlaceList, TransientDOM))
            continue;

        var ActionName = WebFormsList[i].substring(0, 2);
        var ActionValue = WebFormsList[i].substring(2);

        var ActionOperation = ActionName.substring(0, 1);
        var ActionFeature = ActionName.substring(1, 2);

        LastElementPlaceList = cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue, LastElementPlaceList, TransientDOM);
    }
}

function cb_SetValueToInput(evt, ActionOperation, ActionFeature, ActionValue, LastElementPlaceList, TransientDOM)
{
    var ElementPlace = ActionValue.GetTextBefore(""="");
    var Value = ActionValue.GetTextAfter(""="").FullTrim();

    // Set Dynamic Value
    Value = cb_SetDynamicValue(evt, Value, '|');

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
            if (ElementPlace.substring(0, 1) == '[')
            {
                var QueryAll = ElementPlace.substring(1);

                if (HasRequester)
                    ElementPlaceList = Requester.querySelectorAll(QueryAll.Replace(""$[eq];"", ""=""));
                else
                    ElementPlaceList = CurrentDocument.querySelectorAll(QueryAll.Replace(""$[eq];"", ""=""));
            }
            else
            {
                ElementPlaceList = new Array();

                if (HasRequester)
                    ElementPlaceList[0] = cb_GetElementByElementPlace(ElementPlace, Requester, TransientDOM);
                else
                    ElementPlaceList[0] = cb_GetElementByElementPlace(ElementPlace, null, TransientDOM);
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
        CurrentElement = ElementPlaceList[i];

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
                        var OptionValue = Value.GetTextBefore(""|"");
                        var OptionText = Value.GetTextAfter(""|"");
                        if (OptionText.Contains(""|""))
                        {
                            OptionTag.selected = (OptionText.GetTextAfter(""|"") == ""1"");
                            OptionText = OptionText.GetTextBefore(""|"");
                        }

                        OptionTag.value = OptionValue;
                        OptionTag.text = OptionText;

                        CurrentElement.appendChild(OptionTag);
                        break;
                    case 'k':
                        var CheckBoxTag = document.createElement(""input"");
                        CheckBoxTag.setAttribute(""type"", ""checkbox"");

                        var CheckBoxValue = Value.GetTextBefore(""|"");
                        var CheckBoxText = Value.GetTextAfter(""|"");
                        if (CheckBoxText.Contains(""|""))
                        {
                            CheckBoxTag.checked = (CheckBoxText.GetTextAfter(""|"") == ""1"");
                            CheckBoxText = CheckBoxText.GetTextBefore(""|"");
                        }

                        CheckBoxTag.setAttribute(""value"", CheckBoxValue);
                        var CeckBoxIndex = CurrentElement.querySelectorAll('input[type=""checkbox""]').length;

                        var CheckBoxNameAndText = ""cblst_NoneSet"";
                        if (CurrentElement.id)
                            CheckBoxNameAndText = CurrentElement.id;
                        else
                            if (CeckBoxIndex > 0)
                                CheckBoxNameAndText = CurrentElement.querySelectorAll('input[type=""checkbox""]')[0].name.GetTextBefore(""$"");

                        CheckBoxTag.id = CheckBoxNameAndText + ""_"" + CeckBoxIndex;
                        CheckBoxTag.name = CheckBoxNameAndText + ""$"" + CeckBoxIndex;

                        CurrentElement.appendChild(document.createElement(""br""));

                        CurrentElement.appendChild(CheckBoxTag);

                        var LabelTag = document.createElement(""label"");
                        LabelTag.setAttribute(""for"", CheckBoxTag.id);
                        LabelTag.innerText = CheckBoxText;
                        CurrentElement.appendChild(LabelTag);

                        break;
                    case 'l':
                        if (!CurrentElement.tagName.IsInput())
                        {
                            if (CurrentElement.hasAttribute(""title""))
                            {
                                var TitleAttr = CurrentElement.getAttribute(""title"");
                                CurrentElement.setAttribute(""title"", TitleAttr + Value);
                            }
                            else
                                CurrentElement.setAttribute(""title"", Value);
                            break;
                        }

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
                            CurrentElement.insertAdjacentElement(""afterend"", LabelTag);
                        }
                        break;
                    case 't':
                        Value = Value.Replace(""$[ln];"", ""\n"");
                        if (Value.HasTag())
                        {
                            cb_AppendJavaScriptTag(Value);

                            CurrentElement.insertAdjacentHTML(""beforeend"", Value.toDOM());
                            cb_Initialization(CurrentElement);
                        }
                        else
                            CurrentElement.insertAdjacentHTML(""beforeend"", Value);
                        break;
                    case 'a':
                        var AttrName = Value.GetTextBefore(""|"");
                        var Splitter = Value.GetTextAfter(""|"");
                        var AttrValue = """";
                        if (Splitter.Contains(""|""))
                        {
                            AttrValue = Splitter.GetTextAfter(""|"");
                            Splitter = Splitter.GetTextBefore(""|"");
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
                        if (Value.Contains(""|""))
                        {
                            var TagValue = Value.GetTextBefore(""|"");
                            var TagId = Value.GetTextAfter(""|"");
                            TmpTag.value = TagValue;
                            TmpTag.setAttribute(""id"", TagId);
                        }
                        else
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

                            if ((ActionOperation == 'i') && (ClassAttr.ContainsWithSpliter(Value, "" "")))
                                break;

                            CurrentElement.setAttribute(""class"", ClassAttr + ' ' + Value);
                        }
                        else
                            CurrentElement.setAttribute(""class"", Value);
                        break;
                    case 's':
                        if (CurrentElement.hasAttribute(""style""))
                        {
                            var StyleAttr = CurrentElement.getAttribute(""style"");

                            if ((ActionOperation == 'i') && (StyleAttr.ContainsWithSpliter(Value, "";"")))
                                break;

                            if (StyleAttr.charAt(StyleAttr.length - 1) == ';')
                                CurrentElement.setAttribute(""style"", StyleAttr + Value);
                            else
                                CurrentElement.setAttribute(""style"", StyleAttr + ';' + Value);
                        }
                        else
                            CurrentElement.setAttribute(""style"", Value);
                        break;
                    case 'o':
                        if ((ActionOperation == 'i') && (CurrentElement.querySelectorAll('option[value=""' + Value.GetTextBefore(""|"") + ' ""]').length > 0))
                            break;

                        var OptionTag = document.createElement(""option"");
                        var OptionValue = Value.GetTextBefore(""|"");
                        var OptionText = Value.GetTextAfter(""|"");
                        if (OptionText.Contains(""|""))
                        {
                            OptionTag.selected = (OptionText.GetTextAfter(""|"") == ""1"");
                            OptionText = OptionText.GetTextBefore(""|"");
                        }

                        OptionTag.value = OptionValue;
                        OptionTag.text = OptionText;

                        CurrentElement.appendChild(OptionTag);
                        break;
                    case 'k':
                        if ((CurrentElement.tagName.toLowerCase() == ""input"") && ((CurrentElement.type.toLowerCase() == ""checkbox"") || (CurrentElement.type.toLowerCase() == ""radio"")))
                        {
                            CurrentElement.checked = (Value == ""1"");
                            break;
                        }

                        if ((ActionOperation == 'i') && (CurrentElement.querySelectorAll('input[type=""checkbox""][value=""' + Value.GetTextBefore(""|"") + '""]').length > 0))
                            break;

                        var CheckBoxTag = document.createElement(""input"");
                        CheckBoxTag.setAttribute(""type"", ""checkbox"");

                        var CheckBoxValue = Value.GetTextBefore(""|"");
                        var CheckBoxText = Value.GetTextAfter(""|"");
                        if (CheckBoxText.Contains(""|""))
                        {
                            CheckBoxTag.checked = (CheckBoxText.GetTextAfter(""|"") == ""1"");
                            CheckBoxText = CheckBoxText.GetTextBefore(""|"");
                        }

                        CheckBoxTag.setAttribute(""value"", CheckBoxValue);
                        var CeckBoxIndex = CurrentElement.querySelectorAll('input[type=""checkbox""]').length;

                        var CheckBoxNameAndText = ""cblst_NoneSet"";
                        if (CurrentElement.id)
                            CheckBoxNameAndText = CurrentElement.id;
                        else
                            if (CeckBoxIndex > 0)
                                CheckBoxNameAndText = CurrentElement.querySelectorAll('input[type=""checkbox""]')[0].name.GetTextBefore(""$"");

                        CheckBoxTag.id = CheckBoxNameAndText + ""_"" + CeckBoxIndex;
                        CheckBoxTag.name = CheckBoxNameAndText + ""$"" + CeckBoxIndex;

                        CurrentElement.appendChild(document.createElement(""br""));

                        CurrentElement.appendChild(CheckBoxTag);

                        var LabelTag = document.createElement(""label"");
                        LabelTag.setAttribute(""for"", CheckBoxTag.id);
                        LabelTag.innerText = CheckBoxText;
                        CurrentElement.appendChild(LabelTag);

                        break;
                    case 'l':
                        if (!CurrentElement.tagName.IsInput())
                        {
                            if (CurrentElement.hasAttribute(""title""))
                            {
                                if ((ActionOperation == 'i') && CurrentElement.getAttribute(""title""))
                                    break;

                                var TitleAttr = CurrentElement.getAttribute(""title"");
                                CurrentElement.setAttribute(""title"", TitleAttr + Value);
                            }
                            else
                                CurrentElement.setAttribute(""title"", Value);
                            break;
                        }

                        if (!CurrentElement.id)
                            CurrentElement.id = ""tmp_Element"" + LabelForIndexer++;

                        var LabelTag = CurrentDocument.querySelector('label[for=""' + CurrentElement.id + '""]');

                        if (LabelTag)
                        {
                            if ((ActionOperation == 'i') && CurrentElement.innerText)
                                break;

                            LabelTag.innerText = Value;
                        }
                        else
                        {
                            LabelTag = document.createElement(""label"");
                            LabelTag.setAttribute(""for"", CurrentElement.id);
                            LabelTag.innerText = Value;
                            CurrentElement.insertAdjacentElement(""afterend"", LabelTag);
                        }
                        break;
                    case 't':
                        if ((ActionOperation == 'i') && (CurrentElement.innerHTML || CurrentElement.innerText))
                            break;

                        Value = Value.Replace(""$[ln];"", ""\n"");
                        if (Value.HasTag())
                        {
                            cb_AppendJavaScriptTag(Value);

                            CurrentElement.replaceChildren();
                            CurrentElement.insertAdjacentHTML(""beforeend"", Value.toDOM());
                            cb_Initialization(CurrentElement);
                        }
                        else
                            CurrentElement.textContent = Value;
                        break;
                    case 'a':
                        var AttrName = Value.GetTextBefore(""|"");
                        var Splitter = Value.GetTextAfter(""|"");
                        var AttrValue = """";
                        if (Splitter.Contains(""|""))
                        {
                            AttrValue = Splitter.GetTextAfter(""|"");
                            Splitter = Splitter.GetTextBefore(""|"");
                        }
                        if (CurrentElement.hasAttribute(AttrName))
                        {
                            var CurrentAttr = CurrentElement.getAttribute(AttrName);

                            if ((ActionOperation == 'i') && (CurrentAttr.ContainsWithSpliter(AttrValue, Splitter)))
                                break;

                            if (CurrentAttr.charAt(CurrentAttr.length - 1) == Splitter)
                                CurrentElement.setAttribute(AttrName, CurrentAttr + AttrValue);
                            else
                                CurrentElement.setAttribute(AttrName, CurrentAttr + Splitter + AttrValue);
                        }
                        else
                            CurrentElement.setAttribute(AttrName, AttrValue);
                }
                break;

            case 'd':
                switch (ActionFeature)
                {
                    case 'i':
                        if (CurrentElement.id && Value == ""1"")
                            CurrentElement.removeAttribute(""id"");
                        break;
                    case 'n':
                        if (CurrentElement.name && Value == ""1"")
                            CurrentElement.removeAttribute(""name"");
                        break;
                    case 'v':
                        if (CurrentElement.value && Value == ""1"")
                            CurrentElement.value = """";
                        break;
                    case 'c':
                        if (CurrentElement.className)
                            CurrentElement.className = CurrentElement.className.DeleteHtmlClass(Value);
                        break;
                    case 's':
                        if (CurrentElement.hasAttribute(""style""))
                        {
                            var StyleAttr = CurrentElement.getAttribute(""style"").DeleteHtmlStyle(Value);
                            CurrentElement.setAttribute(""style"", StyleAttr);
                        }
                        break;
                    case 'o':
                        if (Value == '*')
                        {
                            var OptionList = CurrentElement.querySelectorAll('option');
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
                                CheckBoxList[CheckBoxTagIndex].remove();
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
                        if (!CurrentElement.tagName.IsInput())
                        {
                            if (CurrentElement.hasAttribute(""title"") && Value == ""1"")
                                CurrentElement.removeAttribute(""title"");

                            break;
                        }
                        if (CurrentElement.id)
                        {
                            var LabelTag = CurrentDocument.querySelector('label[for=""' + CurrentElement.id + '""]');
                            if (LabelTag)
                                LabelTag.remove();
                        }
                        break;
                    case 't':
                        if (Value == ""1"")
                            CurrentElement.replaceChildren();
                        break;
                    case 'a':
                        if (CurrentElement.hasAttribute(Value))
                            CurrentElement.removeAttribute(Value);
                        break;
                    case 'e':
                        if (Value == ""1"")
                            CurrentElement.remove();
                        break;
                    case 'p':
                        if (Value == ""1"")
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

            case 'E':
                switch (ActionFeature)
                {
                    case 'p':
                        if (Value.Contains(""|""))
                        {
                            var HtmlEvent = Value.GetTextBefore(""|"");
                            
                            if (Value.GetTextAfter(""|"") == '+')
                                cb_AddEvent(CurrentElement, HtmlEvent, ""PostBack(event, true)"");
                            else
                                cb_AddEvent(CurrentElement, HtmlEvent, ""PostBack(event, '"" + Value.GetTextAfter(""|"") + ""')"");
                        }
                        else
                            cb_AddEvent(CurrentElement, Value, ""PostBack(event)"");
                        break;
                    case 'P':
                        if (Value.Contains(""|""))
                        {
                            var HtmlEvent = Value.GetTextBefore(""|"");
                            
                            if (Value.GetTextAfter(""|"") == '+')
                                cb_AddEventListener(CurrentElement, HtmlEvent, PostBack, [true]);
                            else
                                cb_AddEventListener(CurrentElement, HtmlEvent, PostBack, [Value.GetTextAfter(""|"")]);
                            break;
                        }
                        else
                            cb_AddEventListener(CurrentElement, Value, PostBack, []);
                        break;
                    case 'g':
                    case 'u':
                    case 'a':
                    case 'l':
                    case 'h':
                    case 'o':
                    case 'r':
                    case 'c':
                        var FunctionName = ""GetBack"";
                        switch (ActionFeature)
                        {
                            case 'u': FunctionName = ""PutBack""; break;
                            case 'a': FunctionName = ""PatchBack""; break;
                            case 'l': FunctionName = ""DeleteBack""; break;
                            case 'h': FunctionName = ""HeadBack""; break;
                            case 'o': FunctionName = ""OptionsBack""; break;
                            case 'r': FunctionName = ""TraceBack""; break;
                            case 'c': FunctionName = ""ConnectBack""; break;
                        }
                        if (Value.Contains(""|""))
                        {
                            var HtmlEvent = Value.GetTextBefore(""|"");
                            var Path = Value.GetTextAfter(""|"");

                            if (Path.Contains(""|""))
                            {
                                if (Path.GetTextBefore(""|"") == '#')
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + ""(event, '', '"" + Path.GetTextAfter(""|"") + ""')"");
                                else
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + ""(event, '"" + Path.GetTextBefore(""|"") + ""', '"" + Path.GetTextAfter(""|"") + ""')"");
                            }
                            else
                            {
                                if (Path == '#')
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + ""(event)"");
                                else
                                    cb_AddEvent(CurrentElement, HtmlEvent, FunctionName + ""(event, '"" + Path + ""')"");
                            }
                        }
                        else
                            cb_AddEvent(CurrentElement, Value, FunctionName + ""(event, this)"");
                        break;
                    case 'G':
                    case 'U':
                    case 'A':
                    case 'L':
                    case 'H':
                    case 'O':
                    case 'R':
                    case 'C':
                        var FunctionValue = GetBack;
                        switch (ActionFeature)
                        {
                            case 'U': FunctionValue = PutBack; break;
                            case 'A': FunctionValue = PatchBack; break;
                            case 'L': FunctionValue = DeleteBack; break;
                            case 'H': FunctionValue = HeadBack; break;
                            case 'O': FunctionValue = OptionsBack; break;
                            case 'R': FunctionValue = TraceBack; break;
                            case 'C': FunctionValue = ConnectBack; break;
                        }
                        if (Value.Contains(""|""))
                        {
                            var HtmlEvent = Value.GetTextBefore(""|"");
                            var Path = Value.GetTextAfter(""|"");

                            if (Path.Contains(""|""))
                            {
                                if (Path.GetTextBefore(""|"") == '#')
                                    cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, ["""", Path.GetTextAfter(""|"")]);
                                else
                                    cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, [Path.GetTextBefore(""|""), Path.GetTextAfter(""|"")]);
                            }
                            else
                            {
                                if (Path == '#')
                                    cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, []);
                                else
                                    cb_AddEventListener(CurrentElement, HtmlEvent, FunctionValue, [Path]);
                            }
                        }
                        else
                            cb_AddEventListener(CurrentElement, Value, FunctionValue, [this]);
                        break;
                    case 't': cb_AddEvent(CurrentElement, Value.GetTextBefore(""|""), ""TagBack(event, '"" + Value.GetTextAfter(""|"") + ""')""); break;
                    case 'T': cb_AddEventListener(CurrentElement, Value.GetTextBefore(""|""), TagBack, [Value.GetTextAfter(""|"")]); break;
                    case 'w': cb_AddEvent(CurrentElement, Value.GetTextBefore(""|""), ""WebSocketBack(event, '"" + Value.GetTextAfter(""|"") + ""')""); break;
                    case 'W': cb_AddEventListener(CurrentElement, Value.GetTextBefore(""|""), WebSocketBack, [Value.GetTextAfter(""|"")]); break;
                    case 'd': cb_AddEvent(CurrentElement, Value, ""PreventDefault(event)""); break;
                    case 'D': CurrentElement.addEventListener(Value, PreventDefault); break;
                    case 's': cb_AddEvent(CurrentElement, Value, ""StopPropagation(event)""); break;
                    case 'S': CurrentElement.addEventListener(Value, StopPropagation); break;
                    case 'm':
                        var [text, type, title, okText, cancelText] = Value.GetTextAfter(""|"").split(""|"");

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
                        
                        var CurrentEvent = Value.GetTextBefore(""|"");

                        if (!CurrentElement.hasAttribute(CurrentEvent))
                            break;

                        var CurrentAttributeValue = CurrentElement.getAttribute(CurrentEvent);

                        CurrentAttributeValue = ""cb_ShowConfirm('"" + text + ""', '"" + type + ""', '"" + title + ""', '"" + okText + ""', '"" + cancelText + ""').then(() => {"" + CurrentAttributeValue + ""}).catch(() => { });"";

                        CurrentElement.setAttribute(CurrentEvent, CurrentAttributeValue);

                        break;
                }
                break;

            case 'R':
                switch (ActionFeature)
                {
                    case 'p': cb_RemoveEvent(CurrentElement, Value, ""PostBack""); break;
                    case 'g': cb_RemoveEvent(CurrentElement, Value, ""GetBack""); break;
                    case 'u': cb_RemoveEvent(CurrentElement, Value, ""PutBack""); break;
                    case 'a': cb_RemoveEvent(CurrentElement, Value, ""PatchBack""); break;
                    case 'l': cb_RemoveEvent(CurrentElement, Value, ""DeleteBack""); break;
                    case 'h': cb_RemoveEvent(CurrentElement, Value, ""HeadBack""); break;
                    case 'o': cb_RemoveEvent(CurrentElement, Value, ""OptionsBack""); break;
                    case 'r': cb_RemoveEvent(CurrentElement, Value, ""TraceBack""); break;
                    case 'c': cb_RemoveEvent(CurrentElement, Value, ""ConnectBack""); break;
                    case 't': cb_RemoveEvent(CurrentElement, Value, ""TagBack""); break;
                    case 'w': cb_RemoveEvent(CurrentElement, Value, ""WebSocketBack""); break;
                    case 'd': cb_RemoveEvent(CurrentElement, Value, ""PreventDefault""); break;
                    case 's': cb_RemoveEvent(CurrentElement, Value, ""StopPropagation""); break;
                    case 'm':
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
                    case 'U': cb_RemoveEventListener(CurrentElement, Value, PutBack); break;
                    case 'A': cb_RemoveEventListener(CurrentElement, Value, PatchBack); break;
                    case 'L': cb_RemoveEventListener(CurrentElement, Value, DeleteBack); break;
                    case 'H': cb_RemoveEventListener(CurrentElement, Value, HeadBack); break;
                    case 'O': cb_RemoveEventListener(CurrentElement, Value, OptionsBack); break;
                    case 'R': cb_RemoveEventListener(CurrentElement, Value, TraceBack); break;
                    case 'C': cb_RemoveEventListener(CurrentElement, Value, ConnectBack); break;
                    case 'T': cb_RemoveEventListener(CurrentElement, Value, TagBack); break;
                    case 'W': cb_RemoveEventListener(CurrentElement, Value, WebSocketBack); break;
                    case 'D': cb_RemoveEventListener(CurrentElement, Value, PreventDefault); break;
                    case 'S': cb_RemoveEventListener(CurrentElement, Value, StopPropagation); break;
                }
                break;
        }

        switch (ActionOperation + ActionFeature)
        {
            case ""sw"": CurrentElement.style.width = Value; break;
            case ""sh"": CurrentElement.style.height = Value; break;
            case ""bc"": CurrentElement.style.backgroundColor = Value; break;
            case ""tc"": CurrentElement.style.color = Value; break;
            case ""fn"": CurrentElement.style.fontFamily = Value; break;
            case ""fs"": CurrentElement.style.fontSize = Value; break;
            case ""fb"": CurrentElement.style.fontWeight = (Value == ""1"") ? ""bold"" : ""unset""; break;
            case ""vi"": CurrentElement.style.visibility = (Value == ""1"") ? ""visible"" : ""hidden""; break;
            case ""ta"": CurrentElement.style.textAlign = Value; break;
            case ""sr"": (Value == ""1"") ? CurrentElement.setAttribute(""readonly"", """") : CurrentElement.removeAttribute(""readonly""); break;
            case ""sd"": (Value == ""1"") ? CurrentElement.setAttribute(""disabled"", """") : CurrentElement.removeAttribute(""disabled""); break;
            case ""sf"": (Value == ""1"") ? CurrentElement.focus() : CurrentElement.blur(); break;
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
                var CheckBoxValue = Value.GetTextBefore(""|"");
                var CheckBoxChecked = Value.GetTextAfter(""|"");
                var CheckBoxTagLength = CurrentElement.querySelectorAll('input[type=""checkbox""][value=""' + CheckBoxValue + '""]').length;
                if (CheckBoxTagLength > 0)
                    CurrentElement.querySelectorAll('input[type=""checkbox""][value=""' + CheckBoxValue + '""]')[0].checked = (CheckBoxChecked == ""1"");
                break;
            case ""ki"":
                var CheckBoxIndex = parseInt(Value.GetTextBefore(""|""));
                var CheckBoxChecked = Value.GetTextAfter(""|"");
                var CheckBoxTags = CurrentElement.querySelectorAll('input[type=""checkbox""]');
                var CheckBoxTag = (ClassIndex >= 0) ? CheckBoxTags[CheckBoxIndex] : CheckBoxTags[CheckBoxTags.length + CheckBoxIndex];
                if (CheckBoxTag)
                    CheckBoxTag.checked = (CheckBoxChecked == ""1"");
                break;
            case ""nt"":
                if (Value.Contains(""|""))
                {
                    var TagName = Value.GetTextBefore(""|"");
                    var TagId = Value.GetTextAfter(""|"");
                    var TmpTag = document.createElement(TagName);
                    TmpTag.id = TagId;
                    CurrentElement.appendChild(TmpTag);
                }
                else
                    CurrentElement.appendChild(document.createElement(Value));
                break;
            case ""ut"":
                if (Value.Contains(""|""))
                {
                    var TagName = Value.GetTextBefore(""|"");
                    var TagId = Value.GetTextAfter(""|"");
                    var TmpTag = document.createElement(TagName);
                    TmpTag.id = TagId;
                    CurrentElement.prepend(TmpTag);
                }
                else
                    CurrentElement.prepend(document.createElement(Value));
                break;
            case ""bt"":
                if (Value.Contains(""|""))
                {
                    var TagName = Value.GetTextBefore(""|"");
                    var TagId = Value.GetTextAfter(""|"");
                    var TmpTag = document.createElement(TagName);
                    TmpTag.id = TagId;
                    CurrentElement.insertAdjacentElement(""beforebegin"", TmpTag);
                }
                else
                    CurrentElement.insertAdjacentElement(""beforebegin"", document.createElement(Value));
                break;
            case ""ft"":
                if (Value.Contains(""|""))
                {
                    var TagName = Value.GetTextBefore(""|"");
                    var TagId = Value.GetTextAfter(""|"");
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
                    cb_AppendJavaScriptTag(Value);

                    CurrentElement.insertAdjacentHTML(""afterbegin"", Value.toDOM());
                    cb_Initialization(CurrentElement);
                }
                else
                    CurrentElement.insertAdjacentHTML(""afterbegin"", Value);
                break;
            case ""lu"": GetBack(evt, Value, ElementPlace); break;
            case ""sp"":
                var OutputPlace = cb_GetElementByElementPlace(Value);
                const placeHolder = document.createElement(""div"");
                CurrentElement.parentNode.insertBefore(placeHolder, CurrentElement);
                OutputPlace.replaceWith(CurrentElement);
                placeHolder.replaceWith(OutputPlace);
        }

        // Extension
        cb_SetValueToInputExtension(evt, ActionOperation, ActionFeature, CurrentElement, Value);
    }

    return ElementPlaceList;
}

function cb_GetElementByElementPlace(ElementPlace, obj, TransientDOM)
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
        if (ElementPlace.Contains(""|""))
            ElementPlace = '>' + ElementPlace

    var ElementPlaceFirstChar = ElementPlace.substring(0, 1);

    const CurrentDocument = TransientDOM ?? document;

    const FromPlace = (obj) ? obj : CurrentDocument;

    switch (ElementPlaceFirstChar)
    {
        case '<':
            var TagName = ElementPlace.substring(1).GetTextBefore("">"");
            var TagIndex = (ElementPlace.length > (TagName.length + 2)) ? parseInt(ElementPlace.substring(TagName.length + 2)) : 0;
            if (TagIndex >= 0)
                return FromPlace.getElementsByTagName(TagName)[TagIndex];
            else
                return FromPlace.getElementsByTagName(TagName)[FromPlace.getElementsByTagName(TagName).length + TagIndex];

        case '(':
            var TagNameAttr = ElementPlace.substring(1).GetTextBefore("")"");
            var TagNameIndex = (ElementPlace.length > (TagNameAttr.length + 2)) ? parseInt(ElementPlace.substring(TagNameAttr.length + 2)) : 0;
            if (TagNameIndex >= 0)
                return FromPlace.getElementsByName(TagNameAttr)[TagNameIndex];
            else
                return FromPlace.getElementsByName(TagNameAttr)[FromPlace.getElementsByName(TagNameAttr).length + TagNameIndex];

        case '{':
            var ClassName = ElementPlace.substring(1).GetTextBefore(""}"");
            var ClassIndex = (ElementPlace.length > (ClassName.length + 2)) ? parseInt(ElementPlace.substring(ClassName.length + 2)) : 0;
            if (ClassIndex >= 0)
                return FromPlace.getElementsByClassName(ClassName)[ClassIndex];
            else
                return FromPlace.getElementsByClassName(ClassName)[FromPlace.getElementsByClassName(ClassName).length + ClassIndex];

        case '*':
            var Query = ElementPlace.substring(1);
            return FromPlace.querySelector(Query.Replace(""$[eq];"", ""=""));

        case '~': return FromPlace;

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

function cb_FetchValue(evt, Value)
{
    Value = Value.substring(1);
      
    if (!Value)
        return Value;

    var ActionOperation = Value.substring(0, 1);
    var ActionFeature = Value.substring(1, 2);

    if (ActionOperation == '_')
        return eval(Value.substring(1).Replace(""$[ln];"", ""\n"").FullTrim());

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
            }

        case 'd':
            var CurrentDate = new Date();
            switch (ActionFeature)
            {
                case 'y': return CurrentDate.getFullYear();
                case 'm': return CurrentDate.getMonth();
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
                            return lines[i];
            }

        case 'c':
            switch (ActionFeature)
            {
                case 'o': return cb_GetCookie(Value);
                case 's':
                case 'l':
                    if (Value.Contains(','))
                    {
                        if (sessionStorage.getItem(Value.GetTextBefore(',')))
                        {
                            var TmpValue = sessionStorage.getItem(Value.GetTextBefore(','));
                            if (ActionFeature == 'l')
                                sessionStorage.removeItem(Value.GetTextBefore(','));

                            return TmpValue;
                        }
                        else
                            return sessionStorage.getItem(Value.GetTextAfter(','));
                    }
                    else
                    {
                        var TmpValue =  sessionStorage.getItem(Value);
                        if (ActionFeature == 't')
                            sessionStorage.removeItem(Value);

                        return TmpValue;
                    }
                case 'd':
                case 't':
                    if (Value.Contains(','))
                    {
                        if (localStorage.getItem(Value.GetTextBefore(',')))
                        {
                            var TmpValue = localStorage.getItem(Value.GetTextBefore(','));
                            if (ActionFeature == 't')
                                localStorage.removeItem(Value.GetTextBefore(','));

                            return TmpValue;
                        }
                        else
                            return localStorage.getItem(Value.GetTextAfter(','));
                    }
                    else
                    {
                        var TmpValue = localStorage.getItem(Value);
                        if (ActionFeature == 't')
                            localStorage.removeItem(Value);

                        return TmpValue;
                    }
                case 'm':
                    if (Value.Contains('|'))
                    {
                        var funcName = Value.GetTextBefore('|');
                        const [...args] = Value.GetTextAfter('|').split(',');

                        for (let i = 0; i < args.length; i++)
                            args[i] = args[i].Replace(""$[co];"", "","");

                        return cb_RunMethod(funcName, args);
                    }
                    else
                        return cb_RunMethod(Value);
            }

        case 'l':
            switch (ActionFeature)
            {
                case 'u':
                    var fetchScript = false;

                    if (Value.contains('|'))
                    {
                        fetchScript = Value.GetTextAfter('|') == '1';
                        Value = Value.GetTextBefore('|');
                    }

                    return cb_GetUrl(Value, fetchScript);
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
                            return lines[i];
            }

        case 's':
            switch (ActionFeature)
            {
                case 'c': return Value.GetTextAfter(',').Replace(' ', Value.GetTextBefore(','));
            }

        case 'e':
            switch (ActionFeature)
            {
                case 'k': return evt.key;
                case 'w': return evt.which;
                case 'x': return evt.clientX;
                case 'y': return evt.clientY;
                case 'X': return evt.pageX;
                case 'Y': return evt.pageY;
            }

        case 'E':
            switch (ActionFeature)
            {
                case 'x': return evt.offsetX;
                case 'y': return evt.offsetY;
            }
    }

    // Extension
    return cb_FetchValueExtension(evt, ActionOperation, ActionFeature, Value);
}

function cb_SaveValue(evt, ActionOperation, ActionFeature, ActionValue, LastElementPlaceList, TransientDOM)
{
    var Name = ActionValue.GetTextAfter('=');
    var ElementPlace = ActionValue.GetTextBefore('=');

    if (!ElementPlace)
        ElementPlace = ""<body>"";

    var CurrentElement;

    if (ElementPlace.substring(0, 1) == '$')
        CurrentElement = (ElementPlace.length > 1) ? cb_GetElementByElementPlace(ElementPlace.substring(1), evt.currentTarget, TransientDOM) : evt.currentTarget;
    else if (ElementPlace.substring(0, 1) == '!')
        CurrentElement = (ElementPlace.length > 1) ? cb_GetElementByElementPlace(ElementPlace.substring(1), evt.target, TransientDOM) : evt.target;
    else
    {
        if (ElementPlace == '-')
            CurrentElement = LastElementPlaceList;
        else
            CurrentElement = cb_GetElementByElementPlace(ElementPlace, null, TransientDOM);
    }

    IsCache = (ActionOperation == 'c');

    switch (ActionOperation)
    {
        case 'g':
        case 'c':
            switch (ActionFeature)
            {
                case 'i': cb_SetStorage(IsCache, Name, CurrentElement.id); break;
                case 'n': cb_SetStorage(IsCache, Name, CurrentElement.name); break;
                case 'v': cb_SetStorage(IsCache, Name, CurrentElement.value); break;
                case 'e': cb_SetStorage(IsCache, Name, CurrentElement.value.length); break;
                case 'c': cb_SetStorage(IsCache, Name, CurrentElement.className); break;
                case 's': cb_SetStorage(IsCache, Name, CurrentElement.style); break;
                case 'l':
                    if (!CurrentElement.tagName.IsInput())
                    {
                        if (CurrentElement.hasAttribute(""title""))
                            cb_SetStorage(IsCache, Name, CurrentElement.getAttribute(""title""));
                        break;
                    }
                    if (CurrentElement.id)
                    {
                        var LabelTag = document.querySelector('label[for=""' + CurrentElement.id + '""]');
                        if (LabelTag)
                            cb_SetStorage(IsCache, Name, CurrentElement.getAttribute(LabelTag.textContent));
                    }
                    break;
                case 't': cb_SetStorage(IsCache, Name, CurrentElement.innerHTML); break;
                case 'o': cb_SetStorage(IsCache, Name, CurrentElement.outerHTML); break;
                case 'g': cb_SetStorage(IsCache, Name, CurrentElement.innerHTML.length); break;
                case 'a': cb_SetStorage(IsCache, Name.GetTextBefore('|'), CurrentElement, getAttribute(Name.GetTextAfter('|'))); break;
                case 'w': cb_SetStorage(IsCache, Name, CurrentElement.style.width); break;
                case 'h': cb_SetStorage(IsCache, Name, CurrentElement.style.height); break;
                case 'r': cb_SetStorage(IsCache, Name, (CurrentElement.hasAttribute(""readonly"")? ""true"" : ""false"")); break;
                case 'x': cb_SetStorage(IsCache, Name, CurrentElement.selectedIndex); break;
                case 'u':
                    var url = Name.GetTextAfter('|');
                    var fetchScript = false;

                    if (url.contains('|'))
                    {
                        fetchScript = url.GetTextAfter('|') == '1';
                        url = url.GetTextBefore('|');
                    }

                    cb_SetStorage(IsCache, Name.GetTextBefore('|'), cb_GetUrl(url, fetchScript));
                    break;
                case 'I': cb_SetStorage(IsCache, Name, Array.from(CurrentElement.parentElement.children).indexOf(CurrentElement)); break;
                case 'A':
                    // Is Only Work Async
                    const [WasmLanguage, wasmUrl, funcName, ...args] = Name.GetTextAfter(""|"").split(',');

                    for (let i = 0; i < args.length; i++)
                        args[i] = args[i].Replace(""$[co];"", "","");

                    switch (WasmLanguage)
                    {
                        case ""c"": cb_RunWasmMethod_C(wasmUrl, funcName, args).then(({ result }) => cb_SetStorage(IsCache, Name.GetTextBefore('|'), result)); break;
                        case ""rust"": cb_RunWasmMethod_Rust(wasmUrl, funcName, args).then(({ result }) => cb_SetStorage(IsCache, Name.GetTextBefore('|'), result)); break;
                        case ""csharp"": cb_RunWasmMethod_CSharp(wasmUrl, funcName, args).then(({ result }) => cb_SetStorage(IsCache, Name.GetTextBefore('|'), result)); break;
                        case ""go"": cb_RunWasmMethod_Go(wasmUrl, funcName, args).then(({ result }) => cb_SetStorage(IsCache, Name.GetTextBefore('|'), result)); break;
                        case ""java"": cb_RunWasmMethod_Java(wasmUrl, funcName, args).then(({ result }) => cb_SetStorage(IsCache, Name.GetTextBefore('|'), result)); break;
                        case ""as"": cb_RunWasmMethod_AS(wasmUrl, funcName, args).then(({ result }) => cb_SetStorage(IsCache, Name.GetTextBefore('|'), result)); break;
                    }
                    
                    break;
            }
    }

    switch (ActionOperation + ActionFeature)
    {
        case ""ta"": cb_SetStorage(false, Name, CurrentElement.style.textAlign); break;
        case ""nl"": cb_SetStorage(false, Name, CurrentElement.childNodes.length); break;
        case ""vi"": cb_SetStorage(false, Name, ((CurrentElement.style.visibility == ""hidden"") ? ""true"" : ""false""));
        case ""Ta"": cb_SetStorage(true, Name, CurrentElement.style.textAlign); break;
        case ""Nl"": cb_SetStorage(true, Name, CurrentElement.childNodes.length); break;
        case ""Vi"": cb_SetStorage(true, Name, ((CurrentElement.style.visibility == ""hidden"") ? ""true"" : ""false""));
    }

    // Extension
    cb_SaveValueExtension(evt, ActionOperation, ActionFeature, Name, CurrentElement);
}

function cb_SetStorage(IsCache, Name, Value)
{
    if (IsCache)
        localStorage.setItem(Name, Value);
    else
        sessionStorage.setItem(Name, Value);
}

function cb_SetDynamicValue(evt, Value, Spliter)
{
    var ValueArray = Value.split(Spliter);
    for (var ValueArrayIndex = 0; ValueArrayIndex < ValueArray.length; ValueArrayIndex++)
        if (ValueArray[ValueArrayIndex].length > 0)
            if (ValueArray[ValueArrayIndex].substring(0, 1) == '@')
                ValueArray[ValueArrayIndex] = cb_FetchValue(evt, ValueArray[ValueArrayIndex]);

    return ValueArray.join(Spliter);
}

function cb_SetDynamicForValue(evt, Value)
{
    if (Value.substring(0, 1) == '@')
        Value = cb_FetchValue(evt, Value);

    return Value;
}

/* End Fetch Web-Forms */

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

/* End Cache */

/* Start URL */

function cb_GetUrl(Url, FetchScript)
{
    var XMLHttp = new XMLHttpRequest();
    XMLHttp.open(""GET"", Url, false);
    XMLHttp.send();

    if (XMLHttp.status === 200)
    {
        var responseText = XMLHttp.responseText

        if (FetchScript)
            cb_AppendJavaScriptTag(responseText);

        return responseText;
    }
}

function cb_ConvertToWebSocketUrl(url)
{
    const currentUrl = window.location.href;
    const protocol = window.location.protocol === 'https:' ? 'wss:' : 'ws:';
    const host = window.location.host;

    if (url.startsWith('?'))
        return `${protocol}//${host}${currentUrl.split(host)[1]}${url}`;

    if (url.startsWith('http://') || url.startsWith('https://'))
        return url.replace(/^http/, 'ws');

    if (url.startsWith('ws://') || url.startsWith('wss://'))
        return url;

    if (!url.includes('://'))
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

/* End URL */

/* Start Cookie */

function cb_GetCookie(Key)
{
    const Cookies = document.cookie.split(';');
    for (let cookie of Cookies)
    {
        cookie = cookie.trim();
        if (cookie.startsWith(Key + '='))
            return cookie.substring(Key.length + 1);
    }

    return """";
}

/* End Cookie */

/** Start Condition **/

async function cb_WaitForCondition(interval, checkFunc, ...args)
{
    return new Promise((resolve, reject) =>
    {
        const timer = setInterval(() =>
        {
            var Result = checkFunc(...args);
            if (Result)
            {
                clearInterval(timer);
                resolve();
            }
            else if (Result === null)
            {
                clearInterval(timer);
                reject();
            }
        }, interval);
    });
}

// A Value Of True Satisfies The Time Condition, And A Value Of Null Escapes The Time Condition
function cb_CheckCondition(evt, ActionControl)
{
    var Action = ActionControl.GetTextBefore(""="");
    var Control = ActionControl.GetTextAfter(""="");

    // Set Dynamic Value
    Control = cb_SetDynamicValue(evt, Control, '|');

    switch (Action)
    {
        case ""gt"": return (Control.GetTextBefore(""|"") > Control.GetTextAfter(""|""));
        case ""lt"": return (Control.GetTextBefore(""|"") < Control.GetTextAfter(""|""));
        case ""et"": return (Control.GetTextBefore(""|"") == Control.GetTextAfter(""|""));
        case ""tr"": return (Control == true);
        case ""fa"": return (Control != true);
        case ""re"":
            {
                var value = Control.GetTextBefore(""|"");
                var pattern = Control.GetTextAfter(""|"");
                try
                {
                    var regex = new RegExp(pattern);
                    return regex.test(value);
                }
                catch (e)
                {
                    if (PostBackOptions.AddLog)
                        console.error(""Invalid regex pattern:"", pattern);
                    return null;
                }
            }
        case ""ct"":
        case ""cf"":
            {
                if (cb_ConfirmIsAccept === undefined) 
                {
                    var [text, type, title, okText, cancelText] = Control.split(""|"");

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

                if (cb_ConfirmIsAccept === true)
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
            }
    }

    // Extension
    return cb_CheckConditionExtension(evt, Action, Control);
}

/** End Condition **/

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
    return this.trim().replace(/^\s\n+|\s\n+$/g, '');
};

String.prototype.TrimStart = function ()
{
    return this.replace(/^[\s\n]+/, '');
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

String.prototype.GetTextAfter = function (Text)
{
    if (!Text)
        return this;

    var index = this.indexOf(Text);
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

    var Space = (ClassNameIndex == 0) ? """" : "" "";
        
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
    if (StyleText[EndIndex] == "";"")
        EndIndex++;

    return StyleText.substring(0, StartIndex) + StyleText.substring(EndIndex);
};

String.prototype.Contains = function (Text)
{
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

    if (Value.EndsWith(""%""))
        return ""%"";
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

/* Start Pre Runner Queue Methods */

function cb_SetPreRunnerQueueForEval(PreRunner, ScriptValue)
{
    if (PreRunner.length < 1)
    {
        eval(ScriptValue);
        return;
    }

    var FirstChar = PreRunner[0].substring(0, 1);

    switch (FirstChar)
    {
        case '(':
            PeriodMiliSecond = parseFloat(PreRunner[0].GetTextAfter(""("")) * 1000;
            PreRunner.shift();
            setInterval(function () { cb_SetPreRunnerQueueForEval(PreRunner, ScriptValue); }, PeriodMiliSecond);
            break;
        case ':':
            DelayMiliSecond = parseFloat(PreRunner[0].GetTextAfter("":"")) * 1000;
            PreRunner.shift();
            setTimeout(function () { cb_SetPreRunnerQueueForEval(PreRunner, ScriptValue); }, DelayMiliSecond);
            break;
        case ',':
            NumberOfRepetitions = PreRunner[0].GetTextAfter("","");
            PreRunner.shift();
            for (var i = 0; i < NumberOfRepetitions; i++)
                cb_SetPreRunnerQueueForEval(PreRunner, ScriptValue);
    }
}

function cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue, LastElementPlaceList, TransientDOM)
{
    if (PreRunner.length < 1)
    {
        // Return Element Place. Is Array Object List For QueryAll, And Array Object List With One Item For Other
        return cb_SetValueToInput(evt, ActionOperation, ActionFeature, ActionValue, LastElementPlaceList, TransientDOM);
    }

    var FirstChar = PreRunner[0].substring(0, 1);

    switch (FirstChar)
    {
        case '(':
            PeriodMiliSecond = parseFloat(PreRunner[0].GetTextAfter(""("")) * 1000;
            PreRunner.shift();
            setInterval(function () { cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue); }, PeriodMiliSecond);
            break;
        case ':':
            DelayMiliSecond = parseFloat(PreRunner[0].GetTextAfter("":"")) * 1000;
            PreRunner.shift();
            setTimeout(function () { cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue); }, DelayMiliSecond);
            break;
        case ',':
            NumberOfRepetitions = PreRunner[0].GetTextAfter("","");
            PreRunner.shift();
            for (var i = 0; i < NumberOfRepetitions; i++)
                cb_SetPreRunnerQueueForSetValueToInput(evt, PreRunner, ActionOperation, ActionFeature, ActionValue);
    }
}

/* End Pre Runner Queue Methods */

/* Start State Management */

class cb_PageClass
{
    constructor(url, title, body, scrollX, scrollY)
    {
        this.url = url;
        this.title = title;
        this.body = body;
        this.scrollX = scrollX;
        this.scrollY = scrollY;
    }
}

class cb_PageManager
{
    static pages = new Map();
    static currentUrl = window.location.pathname;

    static GetFirst()
    {
        return cb_PageManager.pages.values().next().value || null;
    }

    static Add(url, title, body, scrollX, scrollY)
    {
        const page = new cb_PageClass(url, title, body, scrollX, scrollY);
        cb_PageManager.pages.set(url, page);
        return page;
    }

    static Delete(url)
    {
        return cb_PageManager.pages.delete(url);
    }

    static Edit(url, newTitle, newBody, newScrollX, newScrollY)
    {
        if (cb_PageManager.pages.has(url))
        {
            const page = cb_PageManager.pages.get(url);
            page.title = newTitle;
            page.body = newBody;
            page.scrollX = newScrollX;
            page.scrollY = newScrollY;
            return true;
        }
        return false;
    }

    static Get(url)
    {
        return cb_PageManager.pages.get(url) || null;
    }

    static List()
    {
        return Array.from(cb_PageManager.pages.values());
    }

    static SetState(url, push = true)
    {
        if (cb_PageManager.pages.has(url))
        {
            const page = cb_PageManager.pages.get(url);
            document.title = page.title;
            document.body.replaceChildren();
            document.body.insertAdjacentHTML(""beforeend"", page.body);

            window.scrollTo(page.scrollX, page.scrollY);

            if (push)
                window.history.pushState({ url: url }, page.title, url);

            return true;
        }
        return false;
    }

    static InitPopStateHandler()
    {
        window.addEventListener('popstate', (event) =>
        {
            const url = event.state?.url;
            if (url)
                cb_PageManager.SetState(url, false);
        });
    }
}

cb_PageManager.InitPopStateHandler();

function cb_AddFirstPageSPA()
{
    const initialUrl = window.location.pathname;
    if (!cb_PageManager.pages.has(initialUrl))
    {
        cb_PageManager.Add(initialUrl, document.title, document.body.innerHTML, window.scrollX, window.scrollY);
        window.history.replaceState({ url: initialUrl }, null, initialUrl);
    }
}

function cb_SetMainSubmitTypeToButtons(obj)
{
    const buttons = obj.querySelectorAll('input[type=""button""]');

    buttons.forEach(button =>
    {
        if (button.getAttribute('main-type') === 'submit')
        {
            button.setAttribute('type', 'submit');
            button.removeAttribute('main-type');
        }
    });
}

function cb_SetStatePreservation(HtmlDOM, TransientDOM)
{
    // Save Current DOM state Including Select Values
    const selectValues = {};
    HtmlDOM.querySelectorAll('select').forEach((select, index) =>
    {
        selectValues[`select-${index}`] = select.value;
    });

    // Save And Transfer Event Listeners
    const elementsWithEvents = Object.keys(cb_EventRegistry);

    // Restore Select Values To TransientDOM
    TransientDOM.querySelectorAll('select').forEach((select, index) =>
    {
        if (selectValues[`select-${index}`])
            select.value = selectValues[`select-${index}`];
    });

    // Transfer Event Listeners From Old Elements To New Elements
    elementsWithEvents.forEach(objId =>
    {
        const events = cb_EventRegistry[objId];

        let originalElement = null;
        if (objId.startsWith('cb_'))
            originalElement = document.querySelector(`[data-cb-id=""${objId}""]`);
        else
            originalElement = document.getElementById(objId);

        if (originalElement && HtmlDOM.contains(originalElement))
        {
            let newElement = null;
            if (objId.startsWith('cb_'))
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

/* End State Management */

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
    if (!document.getElementById('alertAnimations'))
    {
        const style = document.createElement('style');
        style.id = 'alertAnimations';
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
        case ""warning"": return PostBackOptions.MessageWarningStyle;
        case ""problem"": return PostBackOptions.MessageProblemStyle;
        case ""help"": return PostBackOptions.MessageHelpStyle;
        case ""success"": return PostBackOptions.MessageSuccessStyle;
        case ""none"": return PostBackOptions.MessageNoneStyle;
    }
}

function cb_ShowAlert(text, type = ""none"", title = ""Alert"", okText = ""OK"")
{
    const overlay = document.createElement('div');
    overlay.setAttribute('style', cb_OverlayStyle);

    const alertBox = document.createElement('div');
    alertBox.setAttribute('style', cb_AlertBoxStyle);

    const alertHeader = document.createElement('h2');
    alertHeader.textContent = title;
    alertHeader.setAttribute('style', cb_HeaderStyle + cb_MessageTypeStyle(type));

    const alertText = document.createElement('p');
    alertText.textContent = text;
    alertText.setAttribute('style', cb_TextStyle);

    const okButton = document.createElement('button');
    okButton.textContent = okText;
    okButton.setAttribute('style', cb_ButtonStyle);

    alertBox.appendChild(alertHeader);
    alertBox.appendChild(alertText);
    alertBox.appendChild(okButton);

    overlay.appendChild(alertBox);

    document.body.appendChild(overlay);

    cb_AddAnimationStyles();

    okButton.addEventListener('click', function ()
    {
        document.body.removeChild(overlay);
    });

    // Close With Escape Key
    document.addEventListener('keydown', function closeOnEscape(e)
    {
        if (e.key === 'Escape' && document.body.contains(overlay))
        {
            document.body.removeChild(overlay);
            document.removeEventListener('keydown', closeOnEscape);
        }
    });
}

var cb_ConfirmIsAccept = undefined;

function cb_ShowConfirm(text = ""Are you sure you want to proceed?"", type = ""none"", title = ""Confirm"", okText = ""OK"", cancelText = ""Cancel"")
{
    cb_ConfirmIsAccept = null;

    return new Promise((resolve, reject) =>
    {

        const overlay = document.createElement('div');
        overlay.setAttribute('style', cb_OverlayStyle);

        const confirmBox = document.createElement('div');
        confirmBox.setAttribute('style', cb_AlertBoxStyle);

        const confirmHeader = document.createElement('h2');
        confirmHeader.textContent = title;
        confirmHeader.setAttribute('style', cb_HeaderStyle + cb_MessageTypeStyle(type));

        const confirmText = document.createElement('p');
        confirmText.textContent = text;
        confirmText.setAttribute('style', cb_TextStyle);

        const buttonContainer = document.createElement('div');

        const cancelButton = document.createElement('button');
        cancelButton.textContent = cancelText;
        cancelButton.setAttribute('style', cb_CancelButtonStyle);

        const okButton = document.createElement('button');
        okButton.textContent = okText;
        okButton.setAttribute('style', cb_ButtonStyle);

        buttonContainer.appendChild(cancelButton);
        buttonContainer.appendChild(okButton);

        confirmBox.appendChild(confirmHeader);
        confirmBox.appendChild(confirmText);
        confirmBox.appendChild(buttonContainer);

        overlay.appendChild(confirmBox);
        document.body.appendChild(overlay);

        cb_AddAnimationStyles();

        // OK
        okButton.addEventListener('click', function handleOK()
        {
            document.body.removeChild(overlay);
            cb_ConfirmIsAccept = true;
            resolve();
        });

        // Cancel
        cancelButton.addEventListener('click', function handleCancel()
        {
            document.body.removeChild(overlay);
            cb_ConfirmIsAccept = false;
            reject();
        });

        // ESC
        const escListener = (e) =>
        {
            if (e.key === 'Escape' && document.body.contains(overlay))
            {
                document.body.removeChild(overlay);
                document.removeEventListener('keydown', escListener);
                cb_ConfirmIsAccept = false;
                reject();
            }
        };
        document.addEventListener('keydown', escListener);
    });
}

function cb_ShowMessage(text, type, duration = 0)
{
    const message = document.createElement('div');
    message.setAttribute('style', cb_MessageStyle + cb_MessageTypeStyle(type));

    const messageText = document.createElement('span');
    messageText.textContent = text;

    const closeButton = document.createElement('button');
    closeButton.textContent = '×';
    closeButton.setAttribute('style', cb_MessageButtonStyle);
    closeButton.setAttribute('title', 'Close');

    message.appendChild(messageText);
    message.appendChild(closeButton);

    var messageContainer;
    if (document.getElementById(""cb_MessageContainer""))
        messageContainer = document.getElementById(""cb_MessageContainer"")
    else
    {
        messageContainer = document.createElement('div');
        messageContainer.id = ""cb_MessageContainer"";
        messageContainer.setAttribute('style', cb_MessageContainerStyle);
    }

    messageContainer.appendChild(message);

    document.body.appendChild(messageContainer);

    cb_AddAnimationStyles();

    // Add Event Listener To Close Button
    closeButton.addEventListener('click', function ()
    {
        message.style.animation = 'messageFadeOut 0.3s ease-out forwards';
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
                    message.style.animation = 'messageFadeOut 0.3s ease-out forwards';
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

/* End Message */

/* Start Call Method */

function cb_RunMethod(funcName, args)
{
    // Set Dynamic Value For Arguments
    if (args)
        for (let i = 0; i < args.length; i++)
            args[i] = cb_SetDynamicForValue(evt, args[i]);

    window[funcName](...args);
}

// RUST
async function cb_RunWasmMethod_Rust(wasmUrl, funcName, args = [])
{
    let instance;
    let memory;

    const imports = {
        env: {
            memory: new WebAssembly.Memory({ initial: 256 }),
            table: new WebAssembly.Table({ initial: 0, element: 'anyfunc' }),
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
    catch (e)
    {
        throw new Error(`Failed to instantiate WASM module: ${e.message}`);
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
                if (PostBackOptions.AddLog)
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
    catch (e)
    {
        throw new Error(`Failed to instantiate WASM module: ${e.message}`);
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
            if (PostBackOptions.AddLog)
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
    catch (e)
    {
        throw new Error(`C# WASM init failed: ${e.message}`);
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
    catch (e)
    {
        throw new Error(`Go WASM init failed: ${e.message}`);
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
    catch (e)
    {
        throw new Error(`Java WASM init failed: ${e.message}`);
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
        }
    }

    return { result, memory };
}

/* End Call Method */

/* Start Extension */

// In this Section You Can Extend the WebForms Core Technology and Modify the Following Examples. Please Note that Only Use Numbers for Actions, Because Using String Abbreviations for Actions is a Risk due to Possible Conflicts.

function cb_SetWebFormsValuesExtension(evt, ActionOperation, ActionFeature, Value, LastElementPlaceList, TransientDOM)
{
    switch (ActionOperation)
    {
        case '0':
            switch (ActionFeature)
            {
                case '0': alert(""Hello "" + Value); return true;
            }
    }
}

function cb_SetValueToInputExtension(evt, ActionOperation, ActionFeature, CurrentElement, Value)
{
    switch (ActionOperation)
    {
        case '1':
            switch (ActionFeature)
            {
                case '0': console.log(CurrentElement.outerHTML + ""|"" + Value);
            }
    }
}

function cb_FetchValueExtension(evt, ActionOperation, ActionFeature, Value)
{
    switch (ActionOperation)
    {
        case '2':
            switch (ActionFeature)
            {
                case '0': return ""Hello "" + Value;
            }
    }
}

function cb_SaveValueExtension(evt, ActionOperation, ActionFeature, Name, CurrentElement)
{
    switch (ActionOperation)
    {
        case '3':
            switch (ActionFeature)
            {
                case '0': cb_SetStorage(true, Name, ""Hello saved in local storage""); break;
                case '1': cb_SetStorage(false, Name, ""Hello saved in session storage""); break;
            }
    }
}

function cb_CheckConditionExtension(evt, Action, Control)
{
    switch (Action)
    {
        case ""40"": return (Control == ""Hello"");
    }
}

/* End Extension */");

            file.Dispose();
            file.Close();
        }
    }
}
