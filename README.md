![](https://github.com/elanatframework/Code_behind/assets/111444759/986799af-538a-4aca-b7fc-a5b8153c5a24)
# Code_behind
CodeBehind library is a modern backend framework. This library is a programming model based on the MVC structure, which provides the possibility of creating dynamic aspx files (similar to .NET Standard) in .NET Core and has high serverside independence.
CodeBehind framework supports standard syntax and Razor syntax. This framework guarantees the separation of server-side codes from the design part (html) and there is no need to write server-side codes in the view.

### Why use CodeBehind?
 - The CodeBehind framework is faster than the default structure of cshtml pages in ASP.NET Core.
 - Developing with CodeBehind is very simple. You can use mvc pattern or model-view or controller-view or only view.
 - It is modular. Just copy the new project files, including dll and aspx, into the current active project.
 - You can call the output of the aspx page in another aspx page and modify its output.
 - Your project will still be under ASP.NET Core and you will benefit from all the benefits of .NET Core.
 - Code-behind pattern will be fully respected.

**CodeBehind is .NET Diamond!**

In every scenario, CodeBehind performs better than the default structure in ASP.NET Core.

![ASP.NET Core VS CodeBehind table](https://github.com/elanatframework/Code_behind/assets/111444759/fa78b90a-f404-4cdc-81c1-d101c920c00c)

Programming in CodeBehind is simple. The simplicity of the CodeBehind project is the result of two years of study and research on back-end frameworks and how they support web parts.

###  CodeBehind story 
First, CodeBehind was supposed to be a back-end framework for the C++ programming language; our project in C++ was going well, we built the listener structure and we were even able to implement fast-cgi in the coding phase for the Windows operating system. Windows operating system test with nginx web server was very stable and fast; but for some reason, we stopped working and implemented CodeBehind on .NET Core version 7.

### Only view example

View section: aspx page (razor syntax)
```cshtml
@page
@{
    Random rand = new Random();
}

<div>
    <h1>Random value: @rand.Next(1000000)</h1>
</div>
```

View section: aspx page (standard syntax)
```cshtml
<%@ Page %>
<%
    Random rand = new Random();
%>

<div>
    <h1>Random value: <%=rand.Next(1000000)%></h1>
</div>
```

### MVC example

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

### CodeBehind Configure in ASP.NET Core
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

### Documents

[Simple and structured MVC in CodeBehind](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/simple_and_structured_mvc_in_code_behind.md)

[It is not necessary to follow the MVC pattern](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/it_is_not_necessary_to_follow_the_mvc_pattern.md)

[Load aspx page finally result in another aspx page](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/load_aspx_page_finally_result_in_another_aspx_page.md)

[Examples of development](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/examples_of_development.md)

[Web part in CodeBehind](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/web_part_in_code_behind.md)

[Razor syntax reference for CodeBehind framework](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/razor_syntax_reference_for_code_behind_framework.md)

[Constructor method](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/constructor_method.md)

[HtmlData classes](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/html_data_classes.md)

[Template](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/template.md)

[Option](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/option.md)

[Namespace and dll for CodeBehind view class](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/namespace_and_dll_for_code_behind_view_class.md)

[Error detection](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/error_detection.md)

[New features on new versions](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/new_features_on_new_versions.md)

[How is the list of views finally made?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/how_is_the_list_of_views_finally_made.md)

[Performance test in only view section in version 1.5.2 (ASP.NET Core VS CodeBehind)](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/performance_test_in_only_view_section_version_1.5.2.md)

[ASP.NET Core VS CodeBehind; why should we use CodeBehind?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/asp_dot_net_core_vs_code_behind.md)

[CodeBehind framework vs Code-Behind pattern](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/code_behind_framework_vs_code_behind_pattern.md)

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
