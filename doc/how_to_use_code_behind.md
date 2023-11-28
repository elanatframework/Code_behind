## How to use CodeBehind?

### Add CodeBehind framework in Visual Studio 2022

**Step 1:**

Open Visual Studio 2022, and Click on the File menu and then select the New and then click on Project in the opened box.

**Step 2:**

In the box that opens with the name Create a new project, select the ASP.NET Core Empty option and click Next.

**Step 3:**

Then choose a name for the project and click on the next option again.

**Step 4:**

In this section, in the drop-down list with the name of the framework, select the option **.NET 7.0 (Standard team support)** and then click on the Create button.

**Step 5:**

On the Project menu, select Manage NuGet Package. Then, in the opened box, select the Browse tab. Then enter CodeBehind in the search field.

**Step 6:**

Select CodeBehind among the options and select Install on the right side of the option to install the latest version of the CodeBehind framework.

### CodeBehind configuration

To configure CodeBehind, it is necessary to set the Program.cs class file according to the following codes.

Program File: Program.cs
```csharp
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

### Use CodeBehind

**Step 1:**

In the Solution Explore section, right-click on the project name and then select Add and then New Folder in the opened menu and name the folder **wwwroot**.

**Step 2:**
In the Solution Explore section, right-click on the name of the wwwroot directory and then select Add and then New Item in the opened menu, and in the opened box, regardless of the list, put the file name as Default.aspx and select the Add button.

**Step 3:**
Open the Default.aspx file and delete the values inside it and put the following codes in it.

```cshtml
@page
@{
    string HelloWorld = "Hello CodeBehind framework!";
}

<div>
    <h1>Text value is: @HelloWorld</h1>
</div>
```

Press F5 to test the project.
