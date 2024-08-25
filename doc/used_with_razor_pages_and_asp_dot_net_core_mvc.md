# Used with Razor Pages and ASP.NET Core MVC

In this tutorial, we want to teach how to configure the CodeBehind framework along with Razor pages and ASP.NET Core MVC.

The following codes show how to configure the CodeBehind framework in the Program.cs class:
```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

SetCodeBehind.CodeBehindCompiler.Initialization();

app.UseCodeBehind();

app.Run();
```

According to the above codes, the `UseCodeBehind` middleware answers the requests and the process of the request and response of the program is terminated.

Using the `UseCodeBehindNextNotFound` middleware will respond if the path matches and otherwise continue the process. Using this middleware allows you to configure CideBehind simultaneously with Razor pages and ASP.NET Core MVC.

## Razor Pages and CodeBehind config in Program.cs

The codes below are configuration for Razor pages and CodeBehind framework. Using this configuration allows you to use Razor pages and CodeBehind in ASP.NET Core at the same time.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapRazorPages();

SetCodeBehind.CodeBehindCompiler.Initialization();

app.UseCodeBehindNextNotFound();

app.Run();
```

## MVC and CodeBehind config in Program.cs

The code below is the configuration for MVC and the CodeBehind framework. Applying this configuration allows you to use the default MVC and CodeBehind in ASP.NET Core at the same time.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "MVC/{controller=Home}/{action=Index}");

SetCodeBehind.CodeBehindCompiler.Initialization();

app.UseCodeBehindNextNotFound();

app.Run();
```

## Razor Pages and MVC and CodeBehind config in Program.cs

The codes below are a super config! MVC and Razor Pages and CodeBehind work side by side without interference.
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "MVC/{controller=Home}/{action=Index}");

app.MapRazorPages();

SetCodeBehind.CodeBehindCompiler.Initialization();

app.UseCodeBehindNextNotFound();

app.Run();
```
