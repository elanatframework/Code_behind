## Modularity in the default mode

In this tutorial, we want to teach how to add a module to the project compiled under the CodeBehind framework.

We first explain the concept of a module, then teach how to create a module in a project built using the CodeBehind framework.

Note: Any project built with the CodeBehind framework is a modular system in itself.

What is the concept of the module according to the [Elanat team](https://elanat.net)?
>Any interpreted programming language used on the server side is itself modular. That is, it is enough to copy executable files (py, php, rb, etc.) and other files (css, js, image, etc.) to the current project; A set of executable files and other related files are a module. In compiled programming languages or compiled frameworks such as C++, Java and .NET, creating a modular system is complex. Back-end framework developers should provide solutions for creating a modular system for web-based system developers.

**How does the modular structure of the CodeBehind framework work?**

You can understand this theoretically by referring to the link below.

[Web part in CodeBehind](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/web_part_in_code_behind.md)

But let us show this matter in a practical way with an example.

### Steps to create the main project

**Step 1:** First, in Visual Studio, we create a new empty project under ASP.NET Core version 7.0.

**Step 2:** We install the latest version of CodeBehind framework through NuGet packages.

**Step 3:** Configure the Program.cs class as follows.
```csharp
using CodeBehind;
using SetCodeBehind;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

CodeBehindCompiler.Initialization();

app.Run(async context =>
{
    CodeBehindExecute execute = new CodeBehindExecute();
    await context.Response.WriteAsync(execute.Run(context));
});

app.Run();
```

**Step 4:** Run the project to add the default pages.

When you run a project configured under CodeBehind for the first time, default execution pages will be built into it. The image below is a screenshot of the main page.

![CodeBehind framework default page](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/mkfnhhs7w5y88u3okuo4.png)

**Step 5:** Open the `layout.aspx` file in the `wwwroot` path for editing and set the about page link as below.
```html
<li><a href="/about/">About</a></li>
```

### Steps to create a module project

**Step 1:** First, in Visual Studio, we create a new empty project under ASP.NET Core version 7.0.

**Step 2:** We install the latest version of CodeBehind framework through NuGet packages.

**Step 3:** Configure the Program.cs class as follows.

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Run();
```

**Step 4:** Create a D`efault.aspx` file in `wwwroot/about` path and add the following values to it.

```html
@page
@layout "/layout.aspx"
@controller ModuleProject.AboutController
<p>An "About Us" page is a section on a website that provides information about a company, organization, or individual. It is an opportunity to tell the brand’s story, share its vision, history, values, and achievements, and introduce team members. The primary purpose of an About Us page is to inform the reader about the company and its operations, and it is also used to build trust and credibility with customers. This page is where site users go to learn more about the site they’re on, and it is helpful to define the audience for whom the page is being written, such as first-time visitors and regular users.</p>
```

**Step 5:** We add a new controller class with the following values in the project.

```csharp
using CodeBehind;

namespace ModuleProject
{
    public partial class AboutController : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {
            ViewData.Add("title", "About page");
        }
    }
}
```

**Step 6:** We publish the project.

### Steps to add the module project to the main project

**Step 1:** Copy the `Default.aspx` file from the module project to `wwwroot/about` in the main project.

**Step 2:** We copy the DLL file of the module project to `wwwroot/bin` path in the main project.

![Copy module project to main project](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/naf7qvm74sbyzz8wvbt4.png)

**This is just an example! Please note that you can add the module project files in the web server and the result will be the same. For more practice, you can put the main project on the web server and then add the `Default.aspx` and DLL files related to the module project in the web server and see the result.**

**Step 3:** We run the main project and click on the about link.

The image below is a screenshot of the About page.

![CodeBehind framework about page](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/nn6ehefgeawhxteuxolv.png)

As you can see, we introduced the modular structure of the powerful CodeBehind framework in practice.
