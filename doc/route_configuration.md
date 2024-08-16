## Route configuration

CodeBehind is created based on a unique MVC architecture where there is no need to configure the Controller in the Route. In this architecture, the Controller is determined like a Model in the View pages, then the requests process first reach the View and the View creates an instance of the Controller class.

Because some developers may still be interested in configuring the Controller in the Route, it is still possible to configure the Controller in the Route within the CodeBehind framework.

Compared to ASP.NET Core, the CodeBehind framework provides a dynamic and modular configuration Controller in route.

Example:

Route configuration in the CodeBehind framework
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

The code above shows the Route configuration in the CodeBehind framework in the `Program.cs` class. The `RunRoute` method takes two arguments. The first argument is the context and the second is the section that specifies the Controller.

Note: Section means the strings between slash characters.
Example: `example.com/section0/section1/section2`

If we set the section value to 0, if there is a Controller with the same name as the value of the first section, the `RunRoute` method will execute the Controller.

Example:

CodeBehind Controller
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class home : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {
            Write("Route work fine");
        }
    }
}
```

> Note: When the Controller is executed, the sections are created after the Controller path.

According to the Controller class above, if the path `example.com/home` is requested, the Controller class above is executed and the `Route work fine` string is returned.

If the name of the Controller class matches the section in the url, Regardless of the namespace, the Controller class is executed.

The Controller class name is case sensitive. Therefore, the path `example.com/Home` cannot execute the Controller with the class name home.

This process is modular, so if you have an external dll that contains the Controller class of the CodeBehind framework, you can copy it to `wwwroot/bin` to call the Controller class.

Unlike the weak default structure of MVC in ASP.NET Core, the process of executing the Controller is dynamic and there is no need to create methods with `IActionResult` output; as a result, instead of a hard connection and full dependency, you will have a loose connection and little dependency.

Example:

Dynamic Controller in CodeBehind
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class home : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {
            if (Section.Count() == 0)
            {
                Write("This is main page");
                return;
            }

            switch (Section.GetValue(0))
            {
                case "first": View("/page1.aspx"); break;
                case "second": View("/page2.aspx"); break;
                case "third": View("/page3.aspx"); break;
                case "fourth": View("/page4.aspx"); break;
            }
        }
    }
}
```

The code above shows a Controller that returns the string `This is main page` if there is no section. If there is section named first, second, third, and fourth is requested after the main path, the pages `page1.aspx`, `page2.aspx`, `page3.aspx` and `page4.aspx` will be returned respectively.

Example:

Requesting `example.com/main/first` returns the page `page1.aspx`.
