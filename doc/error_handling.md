## Error handling

The default CodeBehind template includes an error page. In the options file, there is an option that determines the path of the error file; The path of the error page is set by default in this option. In the error page, we activated the page section attribute by default. If you look carefully at the path to the error page in the options file, you will see the value value surrounded by two brackets. This is a type and the numeric value of the error replaces this type.

Options file
```ini
...
error_page_path=/error.aspx/{value}
...
```

Example

`/error.aspx/500`

According to the path above, the value 500 is substituted for the {value} variant.

You can call up the error page according to the type of error. The following example is an implemented example of error handling in the CodeBehind framework.

Program.cs class
```csharp
using CodeBehind;
using SetCodeBehind;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

CodeBehindCompiler.Initialization();

app.Run(async context =>
{
    CodeBehindExecute execute = new CodeBehindExecute();

    string PageResult = execute.Run(context);

    if (execute.FoundPage)
        await context.Response.WriteAsync(PageResult);
    else
        await context.Response.WriteAsync(execute.RunErrorPage(404));
});

app.Run();
```


The example above shows a not found error. `FoundPage` attribute and `RunErrorPage` method have been added in the `CodeBehindExecute` class. According to the above codes, the `Run(context)` method puts the executable file string in the PageResult variable. If a page is not found, the FoundPage attribute is set to false. The `RunErrorPage(404, context)` method also calls the error page.

Note: If you do not need to use context in the error page, you can call the RunErrorPage method without context.

`RunErrorPage(404)`

Please note that to use the RunErrorPage method, you must create the error view file and set its path in the options file. Of course, if you create a new project under ASP.NET Core 7.0 Empty for the first time and there is no wwwroot directory in your project, if you run the project once, the default CodeBehind framework template along with the view error file will be added.
