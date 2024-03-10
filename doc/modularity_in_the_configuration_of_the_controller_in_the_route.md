## Modularity in the configuration of the controller in the route

In this tutorial, we are going to invoke a Controller class from another DLL in a main project. The calling method is completely modular and the DLL is not added to the main project.

In this tutorial, we will first create a main project and compile it and run it on the web server. Then we create a module project and put its DLL in `wwwroot/bin` path in the main project.

### Steps to create the main project

**Step 1:** First, in Visual Studio, we create a new empty project under ASP.NET Core version 7.0.

**Step 2**: We install the latest version of CodeBehind framework through NuGet packages.

**Step 3:** Configure the Program.cs class as follows.
```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

SetCodeBehind.CodeBehindCompiler.Initialization();

app.Run(async context =>
{
    CodeBehind.CodeBehindExecute execute = new CodeBehind.CodeBehindExecute();
    await context.Response.WriteAsync(execute.RunRoute(context, 0));
});

app.Run();
```

The code above is the Route configuration to implement the controller class

**Step 4:** Create new CodeBehind Controller class and put `main` in the name of the Controller class.

CodeBehind Controller
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class main : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {
            MainModel model = new MainModel();
            model.Value = "My text in main project";
            View("/main.aspx", model);
        }
    }
}
```

According to the Controller class above, if the path `example.com/main` is requested, the Controller class above is executed and the `main.aspx` page is initialized with a Model value named `MainModel` and then called.

Note : Because we want the Controller class codes to be displayed in the Program.cs class after the Route configuration, we teach how to create a View and Model below.

**Step 5:** Create new Model class as follows.

Model class
```csharp
namespace YourProjectName
{
    public class MainModel
    {
        public string Value { get; set; }
    }
}
```

**Step 6:** Create new View file as follows.

View (main.aspx)
```html
@page
@model {YourProjectName.MyModel}
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Main project</title>
</head>
<body>
    @model.Value
</body>
</html>
```

**Step 7:** Run the project and then request the path `example.com/main` to see the result.

Result for `example.com/main` request
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Main project</title>
</head>
<body>
    My value text in main project
</body>
</html>
```

Note: Please compile the project and put it on the web server to have a better understanding of the modularity of the CodeBehind framework.

### Steps to create a module project

**Step 1:** First, in Visual Studio, we create a new empty project under ASP.NET Core version 7.0.

**Step 2:** We install the latest version of CodeBehind framework through NuGet packages.

**Step 3:** Configure the Program.cs class as follows.

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Run();
```

**Step 4:** Create new CodeBehind Controller class and put `module` in the name of the Controller class.

CodeBehind Controller
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class module : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {
            ModuleModel model = new ModuleModel();
            model.Value = "My text in module project";
            View("/module.aspx", model);
        }
    }
}
```

According to the Controller class above, if the path `example.com/module ` is requested, the Controller class above is executed and the `module.aspx` page is initialized with a Model value named `ModuleModel` and then called.

**Step 5:** Create new Model class as follows.

Model class
```csharp
namespace YourProjectName
{
    public class ModuleModel
    {
        public string Value { get; set; }
    }
}
```

**Step 6:** Create new View file as follows.

View (module.aspx)
```html
@page
@model {YourProjectName.ModuleModel}
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Module project</title>
</head>
<body>
    @model.Value
</body>
</html>
```

**Step 7:** Publish the module project.

### Steps to add the module project to the main project

**Step 1:** Copy the `module.aspx` file from the module project to `wwwroot` directory  in the main project.

**Step 2:** We copy the DLL file of the module project to `wwwroot/bin` path in the main project.

![Modularity in the CodeBehind framework](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/318mh5no2mznrq9bkp78.png)

**Step 3:** We run the main project and then we request the `example.com/module` path.

Result for `example.com/module` request
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Module project</title>
</head>
<body>
    My text in module project
</body>
</html>
```

As you can see, we introduced the modular structure of the powerful [CodeBehind framework](https://elanat.net/page_content/code_behind) in practice.
