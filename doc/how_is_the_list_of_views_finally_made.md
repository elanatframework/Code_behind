This article tells how to create the final class of views in the CodeBehind framework.

In this example, an MVC page named Default.aspx, and a single page named Random.aspx are created. The Default.aspx page is located in the root and the Random.aspx page is located in the test directory located in the root.

# Pages

**Default.aspx**

View 1 - Default.aspx in wwwroot (razor syntax)
```cshtml
@page
@controller YourProjectName.DefaultController
@model YourProjectName.DefaultModel
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>@model.PageTitle</title>
</head>
<body>
    @model.BodyValue
</body>
</html>
```

View 1 - Default.aspx in wwwroot (standard syntax)
```aspx
<%@ Page Controller="YourProjectName.DefaultController" Model="YourProjectName.DefaultModel" %>
<!DOCTYPE html>
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

Model for View 1
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultModel : CodeBehindModel
    {
        public string PageTitle { get; set; }
        public string BodyValue { get; set; }
    }
}
```

Controller for View 1
```csharp
using CodeBehind;

namespace YourProjectName
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

**Random.aspx**

View 2 - Random.aspx in wwwroot/test (razor syntax)
```cshtml
@page
@{
    Random rand = new Random();
}

<div>
    <h1>Random value: @rand.Next(1000000)</h1>
</div>
```

View 2 - Random.aspx in wwwroot/test (standard syntax)
```aspx
<%@ Page %>
<%Random rand = new Random();%>

<div>
    <h1>Random value: <%=rand.Next(1000000)%></h1>
</div>
```

# Views Class

**Views Class finally**
```csharp
using YourProjectName;
using CodeBehind;
using System;
using System.Runtime;
using Microsoft.AspNetCore.Http;

namespace CodeBehindViews
{
    public class CodeBehindViewsList
    {
        public string SetPageLoadByPath(string path, HttpContext context)
        {
            switch (path)
            {
                case "/Default.aspx": return _Default_aspx_YourProjectName_DefaultController_PageLoad1(context);
                case "/test/Random.aspx": return _test_Random_aspx__PageLoad2(context);

            }
            return "";
        }

        protected string _Default_aspx_YourProjectName_DefaultController_PageLoad1(HttpContext context)
        {
            YourProjectName.DefaultController CurrentController = new YourProjectName.DefaultController();
            CurrentController.PageLoad(context);
            if (!CurrentController.IgnoreViewAndModel)
            {
                YourProjectName.DefaultModel model = (YourProjectName.DefaultModel)CurrentController.CodeBehindModel;
                CurrentController.ResponseText += model.ResponseText;
                CurrentController.ResponseText += "<!DOCTYPE html>\n<html>\n<head>\n    <meta charset=\"utf-8\" />\n    <title>";
                CurrentController.ResponseText += model.PageTitle;
                CurrentController.ResponseText += "</title>\n</head>\n<body>\n    ";
                CurrentController.ResponseText += model.BodyValue;
                CurrentController.ResponseText += "\n</body>\n</html>";
            }
            return CurrentController.ResponseText;
        }

        protected string _test_Random_aspx__PageLoad2(HttpContext context)
        {
            string ReturnValue = "";
            ReturnValue += "\n";
            Random rand = new Random();
            ReturnValue += "\n\n<div>\n    <h1>Random value: ";
            ReturnValue += rand.Next(1000000);
            ReturnValue += "</h1>\n</div>";
            return ReturnValue;
        }

    }
}

namespace YourProjectName{public partial class CodeBehindEmptyClass{}}
```

In the views class, there is a switch case that calls methods based on the path of the aspx file; the internal values of these methods correspond to the same aspx file that existed in that path.
I think you must have noticed why CodeBehind doesn't even refer to the aspx file!

Please note that Controller and Model have already been compiled in the dll file of the project and are only called in the methods of the views class.

How to call the Default.aspx file is known in the `_Default_aspx_YourProjectName_DefaultController_PageLoad1` method.

Also how to call the Random.aspx file is known in the `_test_Random_aspx__PageLoad2` method.

According to the code below, in the last line, a namespace with the name of the project has been added. This namespace has an empty class inside it. The reason for adding this namespace is that if the developer does not use the same namespace as the project name, there will be no problem in calling the namespace with the same name as the project name.

`namespace YourProjectName{public partial class CodeBehindEmptyClass{}}`
