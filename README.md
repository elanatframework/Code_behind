![](https://github.com/elanatframework/Code_behind/assets/111444759/986799af-538a-4aca-b7fc-a5b8153c5a24)
# Code_behind
CodeBehind library is a backend framework. This library is a programming model based on the MVC structure, which provides the possibility of creating dynamic aspx files (similar to .NET Standard) in .NET Core and has high serverside independence.
CodeBehind framework supports standard syntax and Razor syntax. This framework guarantees the separation of server-side codes from the design part (html) and there is no need to write server-side codes in the view.

**CodeBehind is .NET Diamond!**

In every scenario, CodeBehind performs better than the default structure in ASP.NET Core.

![ASP.NET Core VS CodeBehind table](https://github.com/elanatframework/Code_behind/assets/111444759/a93312da-65da-436d-85e3-b920872208d7)

Programming in CodeBehind is simple. The simplicity of the CodeBehind project is the result of two years of study and research on back-end frameworks and how they support web parts.

###  CodeBehind story 
First, CodeBehind was supposed to be a back-end framework for the C++ programming language; our project in C++ was going well, we built the listener structure and we were even able to implement fast-cgi in the coding phase for the Windows operating system. Windows operating system test with nginx web server was very stable and fast; but for some reason, we stopped working and implemented CodeBehind on .NET Core version 7.

### Documents

[How is the list of views finally made?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/how_is_the_list_of_views_finally_made.md)

[Performance test in only view section in version 1.5.2 (ASP.NET Core VS CodeBehind)](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/performance_test_in_only_view_section_version_1.5.2.md)

[ASP.NET Core VS CodeBehind; why should we use CodeBehind?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/asp_dot_net_core_vs_code_behind)

### CodeBehind training (On YouTube)

[Video 1- Hello World!](https://www.youtube.com/watch?v=lxQhDXJ0WcI)

[Video 2- Set dynamic header](https://www.youtube.com/watch?v=2kLgI0Uf8sU)

[Video 3 - Page list in default page](https://www.youtube.com/watch?v=tUujTKOHFq8)

### Download CodeBehind

We added CodeBehind in Nuget so that you can access it easily. You can use it in:

[https://www.nuget.org/packages/CodeBehind](https://www.nuget.org/packages/CodeBehind)

### Elanat was created using CodeBehind

CodeBehind is a stable and reliable framework; [Elanat](https://elanat.net) is the most powerful .NET system implemented using the CodeBehind framework.

[https://github.com/elanatframework/Elanat](https://github.com/elanatframework/Elanat)

![Elanat is based on CodeBehind](https://github.com/elanatframework/Code_behind/assets/111444759/ca6f8d80-65ae-4b4c-b2e2-c8d4b1270b46)

 ### CodeBehind advantages

CodeBehind is a flexible framework. CodeBehind inherits all the advantages of ASP.NET Core and gives it more simplicity, power and flexibility.

CodeBehind, like the default ASP.NET Core, supports multiple platforms, and in the test conducted by the Elanat team, it also has high stability on Linux.

CodeBehind occupies less memory resources (ram) than ASP.NET Core.

aspx pages are compiled in CodeBehind and their calling is done at a very high speed, so that the path of the aspx file is not even referred to during the calling.

![aspx file in ASP.NET Core](https://github.com/elanatframework/Code_behind/assets/111444759/323e70e8-b90b-4ed1-a7f4-67c4814d7a3b)

One of the great features that CodeBehind gives you is the support for DLL libraries. You can add all the .NET Core DLL libraries that you have created into the bin directory located in wwwroot so that the CodeBehind will call all of them.

![A project created under CodeBehind](https://github.com/elanatframework/Code_behind/assets/111444759/eac0e767-993e-4e46-a811-1a0702dbe94d)

How to add web part?
First, copy your compiled project files to the desired path in wwwroot; then copy the main dll file to wwwroot/bin path. You can do the copy while the process is running in the method and then call the code below to compile without restarting the program.

```csharp
// Recompile
CodeBehindCompiler.ReCompile();
```

### Error detection

After running the project, CodeBehind will create a directory called `code_behind` next to the `wwwroot` directory. In this directory, the view class, which is made of aspx files, is kept. If there is any error in the aspx files, it will also be displayed in the `views_compile_error.log` file.

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

### It is not necessary to follow the MVC pattern

In addition to the MVC pattern, you can expand your systems in the form of only View or Controller and View or Model and View.

MVC and V and VC and MV patterns are supported in CodeBehind.

It is not necessary to have a controller and a model, you can code in an aspx page.

**Only View example**

View (standard syntax)
```aspx
<%@ Page %>
<%Random rand = new Random();%>

<div>
    <h1>Random value: <%=rand.Next(1000000)%></h1>
</div>
```

**View and Model without Controller example**

View (razor syntax)
```aspx
@page
@model YourProjectName.DefaultModel

<div>
    <b>@model.Value1</b>
    <br>
    <b>@model.Value2</b>
</div>
```

Model
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultModel : CodeBehindModel
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public DefaultModel()
        {
            Value1 = "text1";
            Value2 = "text2";
        }
    }
}
```

### Examples of development

In aspx pages, you will access HttpContext with context. (standard syntax)
```aspx
<%@ Page %>
<% string HasValue = (!string.IsNullOrEmpty(context.Request.Query["value"]))? "Yes" : "No"; %>

<div>
    <h1>Exist value in querystring? <%=HasValue%></h1>
    <hr>
    <b>value is: <%=context.Request.Query["value"].ToString()%></b>
</div>
```

To receive the information sent through the form, you can follow the instructions below:
```csharp
public DefaultModel model = new DefaultModel();
public void PageLoad(HttpContext context)
{
    if (!string.IsNullOrEmpty(context.Request.Form["btn_Add"]))
        btn_Add_Click();

    View(model);
}

private void btn_Add_Click()
{
    model.PageTitle = "btn_Add Button Clicked";
}
```

The following example shows the power of CodeBehind:

aspx page (razor syntax)
```html
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
    @model.LeftMenuValue
    <div class="main_content">
        @model.MainContentValue
    </div>
    @model.RightMenuValue
</body>
</html>
```

Controller class
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

            CodeBehindExecute execute = new CodeBehindExecute();

            // Add Left Menu Page
            model.LeftMenuValue = execute.Run(context, "/menu/left.aspx");


            // Add Right Menu Page
            model.RightMenuValue = execute.Run(context, "/menu/right.aspx");


            // Add Main Content Page
            model.MainContentValue = execute.Run(context, "/pages/main.aspx");

            View(model);
        }
    }
}
```

Each of the pages left.aspx, right.aspx and main.aspx can also call several other aspx files; these calls can definitely be dynamic and an add-on can be executed that the kernel programmers don't even know about.

You can also call a page without specifying an HttpContext. You should note that query string and HttpContext data are not supported in this method.

```csharp
CodeBehindExecute execute = new CodeBehindExecute();
model.MainContentValue = execute.Run("/pages/main.aspx");
```

You can even call pages with query strings.

```csharp
model.MainContentValue = execute.Run(context, "/pages/main.aspx?template=1");
```

You can also call a path that is determined at runtime and may change over time.

```csharp
string MainPage = Pages.GetDefaultPage();
model.MainContentValue = execute.Run(context, MainPage);
```

Enjoy CodeBehind, but be careful not to loop the program! (Don't call pages that call the current page)

### Web part in CodeBehind

In CodeBehind, the physical executable pages (aspx) are placed in the root path, and this makes the program structured.

CodeBehind supports web parts; web parts are like other parts of the project and include aspx files.

![Web part in CodeBehind](https://github.com/elanatframework/Code_behind/assets/111444759/68a89f70-3a47-4170-8bb5-f844ea2beec2)

To add the web part in CodeBehind, just put the project files in the root.

In CodeBehind, you can run web parts that make changes to aspx files. You can edit all aspx files during project execution and responding to users.

In CodeBehind, the structure of web parts is the same as the structure of the main project; your main project includes aspx pages, dll files, and other client-side files (css, js, images, etc.); web parts in CodeBehind also include aspx pages, dll files and other client side files.

![Web part structer in CodeBehind](https://github.com/elanatframework/Code_behind/assets/111444759/6058b117-6d6c-4c54-8515-7c34efefb6c5)

The project created by using CodeBehind is automatically a modular project, that is, it has the ability to add web parts. In addition, each web part can be used in other projects.

The system built with CodeBehind is also a web part itself. Each web part can also be a separate system! The web part that adds the configuration of the Program.cs class is considered the main system.

CodeBehind stores the final values of its pages outside of the Response in the HttpContext; you can edit the output of the final values in the aspx pages before the answer. This gives you more control than ASP.NET Core.

CodeBehind produces understandable code, while the Controller part of ASP.NET Core is a messy and complex situation.

You will never experience the power that the CodeBehind framework gives you in ASP.NET Core.

.NET developers accept CodeBehind as part of the larger .NET ecosystem. Whatever benefits CodeBehind has belongs to the .NET community.

CodeBehind is similar to interpreted frameworks such as Django and Laravel, and programmers of interpreted programming language projects can easily program with CodeBehind.

Developers of interpretative frameworks can consider CodeBehind as an alternative.
