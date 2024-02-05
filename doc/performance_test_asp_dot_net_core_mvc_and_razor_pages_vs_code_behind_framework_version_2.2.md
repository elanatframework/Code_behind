## Performance Test, ASP.NET Core MVC and Razor Pages vs CodeBehind Framework

In this article, we want to test the performance of the CodeBehind Framework with the default frameworks of Razor Pages and ASP.NET Core MVC.

Before continuing the article, it is necessary to explain that in the CodeBehind framework, it is not mandatory to create MVC or only view, and every part of the system can be created as MVC or Model-View or Controller-View or only View and only one configuration in the Program.cs class is enough; unlike ASP.NET Core, Razor pages and MVC work separately and to use each one, a different configuration is needed in the Program.cs class. In this test, we created two projects from Razor pages and the CodeBehind framework in View only mode and put them on IIS; we also created two projects of ASP.NET Core MVC and CodeBehind framework in full MVC mode and placed them on IIS.

**Test methods**

Almost 2 minutes before running the tests, we checked the systems installed on the web server at least once to make sure that the systems were not sleeping. In this test, the requester and the responder are executed in the same system. In the future, if we have the necessary access to a network, we will spread the requester among several systems and place the responder in only one system to have reliable results.

**Items used in the test**

 - Ryzen 5 PRO 4650G processor
 - Windows 10
 - IIS 10
 - .Net Core 7.0
 - CodeBehind framework 2.2

To create the requester, we created a console application that makes requests to the IIS IP using WebClient. The program requests IIS IP for 10 seconds and the number of responses is displayed in the output. Using a random class, this program requests a page between 1 and 10 per attempt. We built 20 executable files of this program; 10 built outputs for pageX routes (for ASP.NET Core) and 10 built outputs for pageX.aspx routes (for CodeBehind Framework).

Requester
```diff
Console.WriteLine("Start Program");

Random rand = new Random();
HttpClient webClient = new HttpClient();

string DataValue = "";
int i = 0;

DateTime startTime = DateTime.Now;

while ((DateTime.Now - startTime).TotalMilliseconds < 10000)
{
+   DataValue = webClient.GetStringAsync("localhost/page" + rand.Next(1, 11) + ".aspx").Result; // CodeBehind Framework
+   DataValue = webClient.GetStringAsync("localhost/page" + rand.Next(1, 11)).Result; // ASP.NET Core

    i++;
}

Console.WriteLine("Response Count: " + i + " - LastDataValue: " + DataValue);
Console.ReadKey();
```

Please note that each line of code specified in the class above has been tested separately.

### Razor Pages VS CodeBehind Framework

In this test, we created Razor pages without models, and View pages in the CodeBehind framework were created without controllers and models.

CodeBehind Framework config in Program.cs class

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

SetCodeBehind.CodeBehindCompiler.Initialization(true);

app.Run(async context =>
{
    CodeBehind.CodeBehindExecute execute = new CodeBehind.CodeBehindExecute();
    await context.Response.WriteAsync(execute.Run(context));
});

app.Run();
```

The code above is for configuring the CodeBehind framework in ASP.NET Core.

Razor pages config in Program.cs class
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapRazorPages();

app.Run();
```

The code above is for configuring the Razor pages in ASP.NET Core.

View file (View file is the same in Razor pages and CodeBehind framework)

```cshtml
@page
@{
    string PageTitle = "Title";
    string BodyValue = "Body";
    Random rand = new Random();
}
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>@PageTitle</title>
</head>
<body>
    <div>
        @BodyValue
    </div>
    <div>
        <h1>@rand.Next(1000000)</h1>
    </div>
</body>
</html>
```

The above codes are repeated in 10 pages (page1.cshtml, page2.cshtml, page3.cshtml, ..., page10.cshtml in Razor pages and page1.aspx, page2.aspx, page3.aspx, ..., page10.aspx in CodeBehind framework).

As you can see, the View file is the same in both frameworks. Please note that Razor syntax in cshtml pages in ASP.NET Core is somewhat similar to Razor syntax in aspx pages in the CodeBehind framework, and the structure of fetching codes and compiling them in both frameworks is specific and different.

We tested each framework 10 times for this evaluation. Evaluation with 1 requester, evaluation with 2 requesters, ..., evaluation with 10 requesters.

**Test results**

The numbers above show the number of responses per requester.

**Chart of results**

As shown in the chart, Razor pages get the highest number of responses (140,240) when running 8 requesters, and the CodeBehind framework gets the highest number of responses (196,433) with 10 requesters running. These results show us that the CodeBehind framework is up to 40% faster than Razor Pages.

## ASP.NET Core MVC VS CodeBehind Framework

In this test, we created ASP.NET Core MVC along with the Controller and Model, and we also created View pages in the CodeBehind framework along with the Controller and Model.

As we said before, we don't need to reconfigure the CodeBehind framework.

CodeBehind Framework View file
```cshtml
@page
@controller MVCTest.HomeController
@model {MVCTest.HomeModel}
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>@model.PageTitle</title>
</head>
<body>
    <div>
        @model.BodyValue
    </div>
    <div>
        <h1>@model.RandomValue</h1>
    </div>
</body>
</html>
```

The above codes are repeated in 10 pages (page1.aspx, page2.aspx, page3.aspx, ..., page10.aspx).

In the CodeBehind framework, there is no need to set the controller in the route, and the controller is determined like the model on the View page. In the second line of the code above, the controller is specified.

In the third line of the code above, the model class is placed inside the open and closed brackets; in the CodeBehind framework, by default, the model must have the CodeBehindModel abstract; in order to have a non-abstract class, the model class must be placed inside open and closed brackets.

CodeBehind Framework Controller class

```csharp
using CodeBehind;

namespace MVCTest
{
    public class HomeController : CodeBehindController
    {
        public HomeModel model = new HomeModel();

        public HomeController()
        {
            Random rand = new Random();

            model.PageTitle = "Title";
            model.BodyValue = "Body";
            model.RandomValue = rand.Next(1000000);
        }

        public void PageLoad(HttpContext context)
        {
            View(model);
        }
    }
}
```

ASP.NET Core MVC config in Program.cs class
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{action}",
    defaults: new { controller = "Home" });

app.Run();
```

The code above is for configuring the MVC in ASP.NET Core.

ASP.NET Core MVC View file

```cshtml
@model MVCTest.HomeModel
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>@Model.PageTitle</title>
</head>
<body>
    <div>
        @Model.BodyValue
    </div>
    <div>
        <h1>@Model.RandomValue</h1>
    </div>
</body>
</html>
```

The above codes are repeated in 10 pages (page1.cshtml, page2.cshtml, page3.cshtml, ..., page10.cshtml).

ASP.NET Core MVC Controller class

```csharp
using Microsoft.AspNetCore.Mvc;

namespace MVCTest
{
    public class HomeController : Controller
    {
        public HomeModel model = new HomeModel();

        public HomeController()
        {
            Random rand = new Random();

            model.PageTitle = "Title";
            model.BodyValue = "Body";
            model.RandomValue = rand.Next(1000000);
        }

        public IActionResult page1()
        {
            return View(model);
        }

        public IActionResult page2()
        {
            return View(model);
        }
        public IActionResult page3()
        {
            return View(model);
        }

        public IActionResult page4()
        {
            return View(model);
        }

        public IActionResult page5()
        {
            return View(model);
        }

        public IActionResult page6()
        {
            return View(model);
        }

        public IActionResult page7()
        {
            return View(model);
        }

        public IActionResult page8()
        {
            return View(model);
        }

        public IActionResult page9()
        {
            return View(model);
        }

        public IActionResult page10()
        {
            return View(model);
        }
    }
}
```

Model class (Model class is the same in ASP.NET Core MVC and CodeBehind framework)
```csharp
namespace MVCTest
{
    public class HomeModel
    {
        public string PageTitle { get; set; }
        public string BodyValue { get; set; }
        public int RandomValue { get; set; }
    }
}
```

To test the performance of MVC, we created the Controller class and Model class in the CodeBehind framework and ASP.NET Core MVC similar to each other.

We tested each framework 10 times for this evaluation. Evaluation with 1 requester, evaluation with 2 requesters, ..., evaluation with 10 requesters.

**Test results**

The numbers above show the number of responses per requester.

**Chart of results**

As shown in the chart, Razor pages get the highest number of responses (141,260) when running 8 requesters, and the CodeBehind framework gets the highest number of responses (187,387) with 9 requesters running. These results show us that the CodeBehind framework is up to 33% faster than ASP.NET Core MVC.

### Conclusion

According to these tests, the CodeBehind framework is up to 40% faster than the default frameworks in ASP.NET Core. Interestingly, the superiority of CodeBehind over the default structure of ASP.NET Core is not a linear graph, and the more requests, the more the superiority graph is drawn towards CodeBehind.

**In short, the results of the tests show us:**

 - In ASP.NET Core there is no tangible difference between Razor pages and MVC
 - In the CodeBehind framework, there is no tangible difference between View and MVC
 - The response rate of the CodeBehind framework is up to 40% higher than both Razor Pages and MVC structures in ASP.NET Core
