### It is not necessary to follow the MVC pattern

In addition to the MVC pattern, you can expand your systems in the form of only View or Controller and View or Model and View.

MVC and V and VC and MV patterns are supported in CodeBehind.

It is not necessary to have a controller and a model, you can code in an aspx page.

**Only View example**

View (razor syntax)
```cshtml
@page
@{
    Random rand = new Random();
}

<div>
    <h1>Random value: @rand.Next(1000000)</h1>
</div>
```

View (standard syntax)
```aspx
<%@ Page %>
<%Random rand = new Random();%>

<div>
    <h1>Random value: <%=rand.Next(1000000)%></h1>
</div>
```

**View and Model without Controller example**

View (razor syntax)
```cshtml
@page
@model YourProjectName.DefaultModel

<div>
    <b>@model.Value1</b>
    <br>
    <b>@model.Value2</b>
</div>
```

View (standard syntax)
```aspx
<%@ Page Model="YourProjectName.DefaultModel" %>

<div>
    <b><%=model.Value1%></b>
    <br>
    <b><%=model.Value2%></b>
</div>
```

Model
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultModel : CodeBehindModel
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public DefaultModel()
        {
            Value1 = "text1";
            Value2 = "text2";
        }
    }
}
```
