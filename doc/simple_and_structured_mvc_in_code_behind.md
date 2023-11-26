### Simple and structured MVC in CodeBehind

***Note:*** All tutorials are updated based on the latest version of CodeBehind. Avoid installing previous versions and install the latest version. Version 1.0.0 does not support Default.aspx files for directories!

View File: Default.aspx (razor syntax)
```aspx
@page
@controller YourProjectName.DefaultController
@model YourProjectName.DefaultModel
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>@model.PageTitle</title>
</head>
<body>
    @model.BodyValue
</body>
</html>
```

View File: Default.aspx (standard syntax)
```aspx
<%@ Page Controller="YourProjectName.DefaultController" Model="YourProjectName.DefaultModel" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title><%=model.PageTitle%></title>
</head>
<body>
    <%=model.BodyValue%>
</body>
</html>
```

Model File: Default.aspx.Model.cs
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultModel : CodeBehindModel
    {
        public string PageTitle { get; set; }
        public string BodyValue { get; set; }
    }
}
```

Controler File: Default.aspx.Controller.cs
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultController : CodeBehindController
    {
        public DefaultModel model = new DefaultModel();
        public void PageLoad(HttpContext context)
        {
            model.PageTitle = "My Title";
            model.BodyValue = "HTML Body";
            View(model);
        }
    }
}
```

CodeBehind Configure in ASP.NET Core
Program File: Program.cs
```diff
using CodeBehind;
using SetCodeBehind;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

+ CodeBehindCompiler.Initialization();

app.Run(async context =>
{
+    CodeBehindExecute execute = new CodeBehindExecute();
+    await context.Response.WriteAsync(execute.Run(context));
});

app.Run();
```

If you enter the value true in CodeBehindCompiler.Initialization(), as long as the CodeBehindLastSuccessCompiled.dll.tmp file exists next to the main dll files of the program, recompilation will not be done. Doing this makes the response speed of the requests high after the first request since the program goes to sleep.

```csharp
CodeBehindCompiler.Initialization(true);
```

Note : If you configure the Program.cs class like this, any changes in the aspx files, or adding new web parts or removing web parts, requires deleting the CodeBehindLastSuccessCompiled.dll.tmp file.

You can use the Write method in the model and controller classes; the Write method adds a string value to the ResponseText attribute; you can also change the values of the ResponseText attribute by accessing them directly.

In the controller class, there is an attribute named IgnoreViewAndModel attribute, and if you activate the IgnoreViewAndModel attribute, it will ignore the values of model and view and you will only see a blank page; this feature allows you to display the values you need to the user and avoid multiple redirects and transfers.

Note: If you have set the name of a model in the aspx file, You must make sure to call View(ModelName) in the controller class at the end of the method or set the value of IgnoreViewAndModel to true.
