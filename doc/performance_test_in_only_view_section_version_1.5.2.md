In this performance test, we examine the performance of the default ASP.NET Core structure compared to CodeBehind. This review was done on .NET Core version 7.0 and CodeBehind version 1.5.2. This review is only focused on view section in MVC; In version 1.5.2 of CodeBehind, we need to specify the Controller class in the view section.

## Classes and codes of the examined frameworks

### ASP.NET Core

cshtml
```cshtml
@page
@{
    Random rand = new Random();
}

<div>
    <h1>@rand.Next(1000000)</h1>
</div>
```
The above codes are repeated in 10 pages (page1, page2, page3, ..., page10)

Program.cs class
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapRazorPages();

app.Run();
```

### CodeBehind

aspx
```aspx
<%@ Page Controller="PerformanceTestCodeBehind.DefaultController" %>
<%Random rand = new Random();%>

<div>
    <h1><%=rand.Next(1000000)%></h1>
</div>
```
The above codes are repeated in 10 pages (page1.aspx, page2.aspx, page3.aspx, ..., page10.aspx)

Controller
```csharp
using CodeBehind;

namespace PerformanceTestCodeBehind
{
    public partial class DefaultController : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {

        }
    }
}
```
Note: Controller is required in CodeBehind framework version 1.5.2

Program.cs class
```csharp
using CodeBehind;
using SetCodeBehind;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

CodeBehindCompiler.Initialization(true);

app.Run(async context =>
{
    CodeBehindExecute execute = new CodeBehindExecute();
    await context.Response.WriteAsync(execute.Run(context));
});

app.Run();
```

### Test methods

 - Almost 2 minutes before running the tests, we checked the systems installed on the web server at least once to make sure that the systems are not sleeping.
 - In this test, we also checked the number of answers in a fixed time and the elapsed time for the number of fixed answers.


## Performance test based on the time elapsed after 10,000 responses
```diff
using CodeBehind;

namespace PerformanceTestCsHtmlVSCodeBehind
{
    public partial class DefaultController : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {
            DateTime startTime = DateTime.Now;
            Random rand = new Random();
            HttpClient webClient = new HttpClient();

            string DataValue = "";

            for (int i = 0; i < 10000; i++)
            {
+                DataValue = webClient.GetStringAsync("http://192.168.1.4/page" + rand.Next(1,10) + ".aspx").Result; // CodeBehind aspx
+                DataValue = webClient.GetStringAsync("http://192.168.56.1/page" + rand.Next(1,10)).Result; // ASP.NET Core Defualt cshtml
            }

            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime.Subtract(startTime);

            Write("Duration: " + duration.TotalMilliseconds + " ms - LastDataValue: " + DataValue);
        }
    }
}
```
Please note that each line of code specified in the class above has been tested separately.

**Performance table by miliseconds (Lower is better)**

10,000 responses
| Framework | ASP.NET Core | CodeBehind |
| - | - | - |
| Attemp 1 | 7131 | 6584 |
| Attemp 2 | 5811 | 5687 |
| Attemp 3 | 5792 | 5667 |
| Attemp 4 | 5839 | 5866 |
| Attemp 5 | 6087 | 5608 |
| Attemp 6 | 5735 | 5571 |
| Attemp 7 | 5867 | 5568 |
| Attemp 8 | 5727 | 5550 |
| Attemp 9 | 5714 | 5628 |
| Attemp 10 | 5779 | 5597 |
| Average | **5948** | **5733** |

CodeBehind is 3.64% better

Average for 20,000 responses
| Framework | ASP.NET Core | CodeBehind |
| - | - | - |
| Average | **11962** | **10988** |

CodeBehind is 8.1% better

## Performance test based on the number of responses after 10 seconds
```diff
using CodeBehind;

namespace PerformanceTestCsHtmlVSCodeBehind
{
    public partial class DefaultController : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {
            DateTime startTime = DateTime.Now;
            Random rand = new Random();
            HttpClient webClient = new HttpClient();

            string DataValue = "";
            int i = 0;

            while ((DateTime.Now - startTime).TotalMilliseconds < 10000)
            {
+                DataValue = webClient.GetStringAsync("http://192.168.1.4/page" + rand.Next(1, 10) + ".aspx").Result; // CodeBehind
+                DataValue = webClient.GetStringAsync("http://192.168.56.1/page" + rand.Next(1,10)).Result; // ASP.NET Core Defualt

                i++;
            }

            Write("RunCount: " + i + " - LastDataValue: " + DataValue);
        }
    }
}
```
Please note that each line of code specified in the class above has been tested separately.

**Performance table by number of responses (Higher is better)**

10 seconds
| Framework | ASP.NET Core | CodeBehind |
| - | - | - |
| Attemp 1 | 16009 | 17134 |
| Attemp 2 | 17212 | 18401 |
| Attemp 3 | 17129 | 18269 |
| Attemp 4 | 17109 | 17961 |
| Attemp 5 | 17219 | 18816 |
| Attemp 6 | 17364 | 18504 |
| Attemp 7 | 17454 | 18544 |
| Attemp 8 | 17189 | 18457 |
| Attemp 9 | 17292 | 18374 |
| Attemp 10 | 17199 | 18319 |
| Average | **17117** | **18278** |

CodeBehind is 6.78% better

Average for 20 seconds
| Framework | ASP.NET Core | CodeBehind |
| - | - | - |
| Average | **32749** | **35220** |

CodeBehind is 8.1% better

## Conclusion

As it turns out, CodeBehind outperforms the default ASP.NET Core architecture.

Interestingly, the superiority of CodeBehind over the default structure of ASP.NET Core is not a linear graph, and the higher the number of requests over time, the greater the graph of superiority is drawn towards CodeBehind.
