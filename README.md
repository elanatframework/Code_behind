# Code_behind
This library is a programming model based on the MVC structure, which provides the possibility of creating dynamic aspx files (similar to standard .NET) in .NET Core and has high serverside independence.
<b>Soon we will expand this project so that in future versions you can experience both MVC and Code-Behind without coding in the view.</b>

By using Code_behind, we will soon migrate Elanat framework from .NET Standard to .NET Core; during the migration, if we need to have simpler coding and need more maneuvers to do coding, we will add new features to Code_behind.

We added Code_behind in Nuget so that you can access it easily.
You can use it in:
https://www.nuget.org/packages/CodeBehind

------------------------------------------
View File: Default.aspx
```
<%@ Page Controller="YourProjectName.wwwroot.DefaultController" Model="YourProjectName.wwwroot.DefaultModel" %><!DOCTYPE html>
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

------------------------------------------
Model File: Default.aspx.Model.cs
```
using CodeBehind;

namespace YourProjectName.wwwroot
{
    public partial class DefaultModel : CodeBehindModel
    {
        public string PageTitle { get; set; }
        public string BodyValue { get; set; }
    }
}
```

------------------------------------------
Controler File: Default.aspx.Controller.cs
```
using CodeBehind;

namespace YourProjectName.wwwroot
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

------------------------------------------
Program File: Program.cs
```diff
using CodeBehind;
using SetCodeBehind;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

+ CodeBehindCompiler.Initialization();

app.Run(async context =>
{
+    CodeBehindExecute execute = new CodeBehindExecute();
+    await context.Response.WriteAsync(execute.Run(context));
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.Run();
```
