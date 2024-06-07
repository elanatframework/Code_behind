![](https://github.com/elanatframework/Code_behind/assets/111444759/986799af-538a-4aca-b7fc-a5b8153c5a24)
# CodeBehind Framework

![](https://github.com/elanatframework/Code_behind/assets/111444759/35fbac60-55fa-44bc-97f4-0d0be04e3cc5)

CodeBehind library is a modern back-end framework and is an alternative to ASP.NET Core. This library is a programming model based on the MVC structure, which provides the possibility of creating dynamic aspx files in .NET Core and has high serverside independence.
The aspx extension is the files of the view section in the CodeBehind framework and they supports standard syntax (<%=Standard%>) and Razor syntax (@Razor). This framework guarantees the separation of server-side codes from the design part (html) and there is no need to write server-side codes in the view.

Code Behind framework inherits every advantage of ASP.NET Core and gives it more simplicity, power and flexibility.

**CodeBehind framework is an alternative to ASP.NET Core.**

### Why use CodeBehind?
 - **Fast**: The CodeBehind framework is faster than the default structure of cshtml pages in ASP.NET Core.
 - **Simple**: Developing with CodeBehind is very simple. You can use mvc pattern or model-view or controller-view or only view.
 - **Modular**: It is modular. Just copy the new project files, including dll and aspx, into the current active project.
 - **Get output**: You can call the output of the aspx page in another aspx page and modify its output.
 - **Under .NET Core**: Your project will still be under ASP.NET Core and you will benefit from all the benefits of .NET Core.
 - **Code-Behind**: Code-Behind pattern will be fully respected.
 - **Modern**: CodeBehind is a modern framework with revolutionary ideas.
 - **Understandable**: View is preferable to controller and there is no need to set controllers in route.
 - **Adaptable**: The CodeBehind framework can even be used with Razor Pages and ASP.NET Core MVC.

**CodeBehind is .NET Diamond!**

In every scenario, CodeBehind performs better than the default structure in ASP.NET Core.

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
@controller DefaultController
@model DefaultModel
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
<%@ Page Controller="DefaultController" Model="DefaultModel" %>
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

public partial class DefaultModel : CodeBehindModel
{
    public string PageTitle { get; set; }
    public string BodyValue { get; set; }
}
```

Controler File: Default.aspx.Controller.cs
```csharp
using CodeBehind;

public partial class DefaultController : CodeBehindController
{
    public void PageLoad(HttpContext context)
    {
        DefaultModel model = new DefaultModel();
        model.PageTitle = "My Title";
        model.BodyValue = "HTML Body";
        View(model);
    }
}
```

### CodeBehind Configure in ASP.NET Core
Program File: Program.cs
```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

SetCodeBehind.CodeBehindCompiler.Initialization();

app.UseCodeBehind();

app.Run();
```

### Documents

#### Programming
 - [Simple and structured MVC in CodeBehind](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/simple_and_structured_mvc_in_code_behind.md)
 - [It is not necessary to follow the MVC pattern](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/it_is_not_necessary_to_follow_the_mvc_pattern.md)
 - [Load aspx page finally result in another aspx page](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/load_aspx_page_finally_result_in_another_aspx_page.md)
 - [Examples of development](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/examples_of_development.md)
 - [Send data](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/send_data.md)
 - [Web part in CodeBehind](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/web_part_in_code_behind.md)
 - [Razor syntax reference for CodeBehind framework](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/razor_syntax_reference_for_code_behind_framework.md)
 - [Standard syntax reference for CodeBehind framework](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/standard_syntax_reference_for_code_behind_framework.md)
 - [Constructor method](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/constructor_method.md)
 - [HtmlData classes](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/html_data_classes.md)
 - [Template](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/template.md)
 - [Transfer template block data in ViewData](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/transfer_template_block_data_in_view_data.md)
 - [Layout](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/layout.md)
 - [Section](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/section.md)
 - [Error handling](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/error_handling.md)
 - [Options](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/options.md)
 - [Namespace and dll for CodeBehind view class](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/namespace_and_dll_for_code_behind_view_class.md)
 - [Error detection](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/error_detection.md)
 - [How to use CodeBehind?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/how_to_use_code_behind.md)
 - [Route configuration](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/route_configuration.md)
 - [Used with Razor Pages and ASP.NET Core MVC](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/used_with_razor_pages_and_asp_dot_net_core_mvc.md)
 - [Modularity in the default mode](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/modularity_in_the_default_mode.md)
 - [Modularity in the configuration of the controller in the route](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/modularity_in_the_configuration_of_the_controller_in_the_route.md)

#### API and applied methods
 - [Download file](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/download_file.md)
 - [Dynamic Model](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/dynamic_model.md)

#### Information
 - [New features on new versions](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/new_features_on_new_versions.md)
 - [How is the list of views finally made?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/how_is_the_list_of_views_finally_made.md)
 - [CodeBehind framework data](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/code_behind_framework_data.md)
 - [MVC architecture in CodeBehind](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/mvc_architecture_in_code_behind.md)
 - [Performance test, ASP.NET Core MVC and Razor Pages vs CodeBehind Framework in version 2.2](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/performance_test_asp_dot_net_core_mvc_and_razor_pages_vs_code_behind_framework_version_2.2.md)
 - [Performance test in only view section in version 1.5.2 (ASP.NET Core VS CodeBehind)](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/performance_test_in_only_view_section_version_1.5.2.md)
 - [CodeBehind story](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/code_behind_story.md)
 - [ASP.NET Core VS CodeBehind; why should we use CodeBehind?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/asp_dot_net_core_vs_code_behind.md)
 - [CodeBehind framework vs Code-Behind pattern](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/code_behind_framework_vs_code_behind_pattern.md)

#### Creating high-level systems
 - [How to create modular systems by CodeBehind framework?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/how_to_create_modular_systems_by_code_behind_framework.md)
 - [How to create scheduled task system by CodeBehind framework?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/how_to_create_scheduled_task_by_code_behind_framework.md)
 - [How to create startup system by CodeBehind framework?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/how_to_create_startup_system_by_code_behind_framework.md)
 - [How to create dynamic middleware system by CodeBehind framework?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/how_to_create_dynamic_middleware_by_code_behind_framework.md)

#### CodeBehind training video (On YouTube)
 - [Video 1- Hello World!](https://www.youtube.com/watch?v=lxQhDXJ0WcI)
 - [Video 2- Set dynamic header](https://www.youtube.com/watch?v=2kLgI0Uf8sU)
 - [Video 3 - Page list in default page](https://www.youtube.com/watch?v=tUujTKOHFq8)
 - [Video 4 - How to use CodeBehind framework?](https://www.youtube.com/watch?v=wb57rGL3HLc)
 - [Video 5 - Advanced programming with return template](https://www.youtube.com/watch?v=zUftrftUCtw)

### Download CodeBehind

Get from Elanat website:

[https://elanat.net/category/download_code_behind/](https://elanat.net/category/download_code_behind/)

Get from GitHub:

[https://github.com/elanatframework/Code_behind/releases](https://github.com/elanatframework/Code_behind/releases)

Get from Nuget (We added CodeBehind in Nuget so that you can access it easily):

[https://www.nuget.org/packages/CodeBehind](https://www.nuget.org/packages/CodeBehind)

### Ready project

[Get ready project](https://github.com/elanatframework/Code_behind/tree/elanat_framework/ready_project)
