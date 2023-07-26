# Code_behind
Code-Behind is Back-End framework. This library is a programming model based on the MVC structure, which provides the possibility of creating dynamic aspx files (similar to .NET Standard) in .NET Core and has high serverside independence.
Soon we will expand this project so that in future versions you can experience both MVC and Code-Behind without coding in the view.

![aspx file in .NET Core](https://github.com/elanatframework/Code_behind/assets/111444759/e5375793-31b2-4465-966a-1c3f5d7d03a1)

Big surprise, the new version of [Elanat](https://elanat.net) is prepared on .NET Core and the new source will be released in August 2023. Elanat is the largest system ever migrated from .NET Standard to .NET Core. [Elanat](https://github.com/elanatframework/Elanat) migration is done by using Code_behind infrastructure.

We added Code_behind in Nuget so that you can access it easily.
You can use it in:
https://www.nuget.org/packages/CodeBehind

---
View File: Default.aspx
```aspx
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

---
Model File: Default.aspx.Model.cs
```csharp
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

---
Controler File: Default.aspx.Controller.cs
```csharp
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

---
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

You can use the Write method in the model and controller classes; the Write method adds a string value to the ResponseText attribute; you can also change the values of the ResponseText attribute by accessing them directly.

In the controller class, there is an attribute named IgnoreViewAndModel attribute, and if you activate the IgnoreViewAndModel attribute, it will ignore the values of model and view and you will only see a blank page; this feature allows you to display the values you need to the user and avoid multiple redirects and transfers.

---

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

---

The following example shows the power of Code-Behind:

aspx page
```html
<%@ Page Controller="YourProjectName.wwwroot.DefaultController" Model="YourProjectName.wwwroot.DefaultModel" %><!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title><%=model.PageTitle%></title>
</head>
<body>
    <%=model.LeftMenuValue%>
    <div class="main_content">
        <%=model.MainContentValue%>
    </div>
    <%=model.RightMenuValue%>
</body>
</html>
```

Controller class
```csharp
using CodeBehind;

namespace YourProjectName.wwwroot
{
    public partial class DefaultController : CodeBehindController
    {
        public DefaultModel model = new DefaultModel();

        public void PageLoad(HttpContext context)
        {
            model.PageTitle = "My Title";

            CodeBehindExecute execute = new CodeBehindExecute();

            // Add Left Menu Page
            context.Request.Path = "/menu/left.aspx";
            model.LeftMenuValue = execute.Run(context);


            // Add Right Menu Page
            context.Request.Path = "/menu/right.aspx";
            model.RightMenuValue = execute.Run(context);


            // Add Main Content Page
            context.Request.Path = "/pages/main.aspx";
            model.MainContentValue = execute.Run(context);

            View(model);
        }
    }
}
```
Each of the pages left.aspx, right.aspx and main.aspx can also call several other aspx files; these calls can definitely be dynamic and an add-on can be executed that the kernel programmers don't even know about.

Enjoy Code-Behind, but be careful not to loop the program! (Don't call pages that call the current page)
