## Used with Razor Pages and ASP.NET Core MVC

In this tutorial, we want to teach how to configure the CodeBehind framework along with Razor pages and ASP.NET Core MVC.

The following codes show how to configure the CodeBehind framework in the Program.cs class:
```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

SetCodeBehind.CodeBehindCompiler.Initialization();

app.Run(async context =>
{
    CodeBehind.CodeBehindExecute execute = new CodeBehind.CodeBehindExecute();
    await context.Response.WriteAsync(execute.Run(context));
});

app.Run();
```

### Razor Pages and CodeBehind config in Program.cs

The codes below are configuration for Razor pages and CodeBehind framework. Using this configuration allows you to use Razor pages and CodeBehind in ASP.NET Core at the same time.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapRazorPages();

SetCodeBehind.CodeBehindCompiler.Initialization();

app.Use(async (context, next) =>
{
    CodeBehind.CodeBehindExecute execute = new CodeBehind.CodeBehindExecute();

    string PageResult = execute.Run(context);

    if (execute.FoundPage)
        await context.Response.WriteAsync(PageResult);
    else
        await next();
});

app.Run();
```

### MVC and CodeBehind config in Program.cs

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

app.Use(async (context, next) =>
{
    CodeBehind.CodeBehindExecute execute = new CodeBehind.CodeBehindExecute();

    string PageResult = execute.Run(context);

    if (execute.FoundPage)
        await context.Response.WriteAsync(PageResult);
    else
        await next();
});

app.Run();
```

### Razor Pages and MVC and CodeBehind config in Program.cs

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

app.Use(async (context, next) =>
{
    CodeBehind.CodeBehindExecute execute = new CodeBehind.CodeBehindExecute();

    string PageResult = execute.Run(context);

    if (execute.FoundPage)
        await context.Response.WriteAsync(PageResult);
    else
        await next();
});

app.Run();
```
