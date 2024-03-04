## Standrd syntax reference for CodeBehind framework

In CodeBehind framework the standrd syntax will also be created in the aspx files and the standrd syntax will be automatically determined from the Razor syntax. In addition, it is not possible to combine standard syntax and Razor syntax.

The standard syntax in CodeBehind is very similar to the aspx page syntaxes of Microsoft's former Web-Forms in ASP.NET Standard and classic asp pages, but in some cases there may be slight differences.

**An HTML page combined with standard syntax**
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

### Standard syntax

#### Implicit standard expressions

Implicit standard expressions start with `<%=` and end with `%>` followed by C# code:
```aspx
<p><%=DateTime.Now%></p>
<p><%=DateTime.IsLeapYear(2024)%></p>
```

#### Code block

Code block start with `<%` and end with `%>` followed by C# code:

**Standard syntax example for code block**
```aspx
<%
    string Note = "Elanat CMS was created to be a reliable system in .NET and an honor for .NET programmers and can be compared to other systems under PHP and JAVA.";
%>

<p><%=Note%></p>
```

### Page attributes in standard syntax

The CodeBehind framework supports several attributes for view pages. Each of the adjectives are placed between `<%@` and `%>` at the top of the page.

**Model attribute**

To determine the model attribute, the model string must be written and then the equals character must be added, and the name of the model class must be placed between the double quotation marks (").

Example
```aspx
<%@ page model="YourProjectName.DefaultModel" %>
<!DOCTYPE html>
...
```

**Controller attribute**

To determine the controller attribute, the controller string must be written then the equals character must be added, and the name of the controller class must be placed between the double quotation marks (").

Example
```aspx
<%@ page controller="YourProjectName.DefaultController" %>
<!DOCTYPE html>
...
```

**Layout attribute**

To determine the layout attribute, the layout string must be written and then the equals character must be added, and the path of the layout must be placed between the double quotation marks (").

Example
```aspx
<%@ page layout="/main-layout.aspx" %>
<!DOCTYPE html>
...
```

**Template attribute**

To determine the template attribute, the template string must be written and then the equals character must be added, and the path of the template must be placed between the double quotation marks (").

Example
```aspx
<%@ page template="/templates/template1.aspx" %>
<!DOCTYPE html>
...
```

**Break attribute**

To determine the break attribute, the break string must be written and then the equals character must be added, and the true string must be placed between the double quotation marks (").

Example
```aspx
<%@ page break="true" %>
<!DOCTYPE html>
...
```

**Islayout attribute**

To determine the Islayout attribute, the Islayout string must be written and then the equals character must be added, and the true string must be placed between the double quotation marks (").

Example
```aspx
<%@ page islayout="true" %>
<!DOCTYPE html>
...
```
