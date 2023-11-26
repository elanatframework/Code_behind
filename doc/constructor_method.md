## Constructor method

One of the initiatives of the Elanat team in the CodeBehind framework is to add support for the CodeBehind constructor method for models and controllers. You can open parentheses in front of the model and controller class names and add the desired input arguments that you created in the CodeBehind constructor method.

Example for standard syntax
```html
<%@ Page Controller="YourProjectName.DefaultController(1)" Model="YourProjectName.DefaultModel(context)" %>
```

Example for Razor syntax

```csharp
@page
@controller YourProjectName.DefaultController(1)
@model YourProjectName.DefaultModel(context)
```

Controller class
```csharp
using CodeBehind;

namespace YourProjectName
{
    public DefaultModel model = new DefaultModel();
    public partial class DefaultController : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {
            View(model);
        }

        public void CodeBehindConstructor(int Index)
        {
            Write(Index.ToString());
        }
    }
}
```

Model class
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultModel : CodeBehindModel
    {
        public void CodeBehindConstructor(HttpContext context)
        {
            Write(context.Request.Query["name"].ToString());
        }
    }
}
```

As shown in the above example, from now on you can access the HttpContext in the model without a controller, just add the context value as an input argument in front of the model class name. Then you can add the CodeBehindConstructor method in the model class.
