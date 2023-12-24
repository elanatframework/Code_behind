## Template

100% Code-Behind support guarantee!

The Elanat team has introduced a wonderful Template structure. Templates form parts of an aspx page.

Please consider the following example:

This example is for standard syntax; we will also give an example for Razor syntax.

aspx file (standard syntax)
```diff
<%@ Page Controller="YourProjectName.DefaultController" Model="YourProjectName.DefaultModel" %>
<!DOCTYPE html>
<html>
<head>
+   <#GlobalTags#>
    <title><%=model.PageTitle%></title>
</head>
<body>
    <%=model.BodyValue%>
</body>
</html>

+<#GlobalTags
+<meta charset="utf-8" />
+<meta name="viewport" content="width=device-width, initial-scale=1.0" />
+<meta http-equiv="Content-Type" content="text/html; charset=utf-16" />
+<meta http-equiv="content-language" content="en">
+<script type="text/javascript" src="/client/script/global.js" ></script>
+<link rel="alternate" type="application/rss+xml" title="rss feed" href="/rss/" />
+<link rel="shortcut icon" href="/favicon.ico" />
+<link rel="stylesheet" type="text/css" href="/client/style/global.css" />
+#>
```

In this example, a Template section is called in the Template variable and the output is converted as follows.

```html
<%@ Page Controller="YourProjectName.DefaultController" Model="YourProjectName.DefaultModel" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-16" />
    <meta http-equiv="content-language" content="en">
    <script type="text/javascript" src="/client/script/global.js" ></script>
    <link rel="alternate" type="application/rss+xml" title="rss feed" href="/rss/" />
    <link rel="shortcut icon" href="/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="/client/style/global.css" />
    <title><%=model.PageTitle%></title>
</head>
<body>
    <%=model.BodyValue%>
</body>
</html>
```

But this structure seems to be very simple and basic! Why did we talk about a wonderful structure?

In a **revolutionary initiative**, the Elanat team provides a return templating structure that guarantees 100% separation of server-side code from the design part (like html).

Usually, for repetitive parts such as loops and conditions, you should either add the codes of the design part in the server side codes or add the server side codes in the design part.

An example of server side coding in the design section

```html
<table>
    <thead>
        <tr>
            <th>Column1</th>
            <th>Column2</th>
            <th>Column3</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>@item.Column1</td>
                <td>@item.Column2</td>
                <td>@item.Column3</td>
            </tr>
        }
    </tbody>
</table>
```

Example of integration of the design part in the server side codes
```csharp
model.ListValue = "<ul>";

foreach(string s in List)
    model.ListValue += "<li>" + s + "</li>";

model.ListValue += "</ul>";
```

Please note that things like the presence of a variable between the codes of the design section are still considered Code-Behind, unless the codes of the server section are added.

Example for Razor syntax
```html
<h1>@model.PageName</h1>
```

Example for standard syntax
```html
<h1><%=model.PageName%></h1>
```

In the two examples above, the @model.PageName and <%=model.PageName%> values are easily recognized in the html tags; So the Code-Behind is still in place and designers or client-side developers can easily recognize these values.

One of the most famous frameworks that guaranteed the separation of server-side codes from the design department was Microsoft's web-form; but this guarantee was a heavy price, such as additional codes and lack of full control of the designers over the design department.

Example (Razor syntax)
View section before pasting the template
```html
<div class="header">
  <a href="#default">@model.Title</a>
  <div class="header-right">
    @#Tags={<a href="@#Href">@#PageName</a>}
  </div>
</div>

@#Tags{
@foreach (PageItem page in model.PageItemList)
{
    @#Tags
}
}

@#PageName{@page.Title}
@#Href{@(((page.Path == "main")? "/" : page.Path))}
```

pasting template auto step 1
```html
<div class="header">
  <a href="#default">@model.Title</a>
  <div class="header-right">
    @#Tags={<a href="@#Href">@#PageName</a>}
  </div>
</div>

@#Tags{
@foreach (PageItem page in model.PageItemList)
{
    <a href="@#Href">@#PageName</a>
}
}

@#PageName{@page.Title}
@#Href{@(((page.Path == "main")? "/" : page.Path))}
```

pasting template auto step 2
```html
<div class="header">
  <a href="#default">@model.Title</a>
  <div class="header-right">
    @foreach (PageItem page in model.PageItemList)
    {
        <a href="@#Href">@#PageName</a>
    }
  </div>
</div>

@#PageName{@page.Title}
@#Href{@(((page.Path == "main")? "/" : page.Path))}
```

pasting template auto step 3 (finally)
```html
<div class="header">
  <a href="#default">@model.Title</a>
  <div class="header-right">
    @foreach (PageItem page in model.PageItemList)
    {
        <a href="@(((page.Path == "main")? "/" : page.Path))">@page.Title</a>
    }
  </div>
</div>
```

Enjoy beautiful structer in returned template in CodeBehind framework.

You can introduce the template externally.
Elanat team has considered astx file extension for external files of templates.

**Elanat Framework company provides astx extension**

Example for set external template

Razor syntax example
```html
@page
@template "/page/template/page_template.astx"
```

Standard syntax example
```html
<%@ Page Template="/page/template/page_template.astx" %>
```

There is no need to add the astx file extension and the following path is also correct.

`"/page/template/page_template"`

In the template placement example, if you add the template externally, you will have the following html in the design section.

View section before pasting the template
```diff
<div class="header">
  <a href="#default">@model.Title</a>
  <div class="header-right">
+    @#Tags={<a href="@#Href">@#PageName</a>}
  </div>
</div>
```

Without a return template, at best you will have the following html in the design section.

View section without use return template
```diff
<div class="header">
  <a href="#default">@model.Title</a>
  <div class="header-right">
+ @foreach (PageItem page in model.PageItemList)
+ {
+    <a href="@(((page.Path == "main")? "/" : page.Path))">@page.Title</a>
+ }
  </div>
</div>
```

**Do you understand the beauty of working with a return template?**

You can also use template for standard syntax. The previous example is unclear in the standard syntax in the following codes.

```html
<div class="header">
  <a href="#default"><%=model.Title%></a>
  <div class="header-right">
    <#Tags=<a href="<#Href#>"><#PageName#></a>#>
  </div>
</div>

<#Tags 
<% foreach (PageItem page in model.PageItemList)
{
    <#Tags#>
}
%>
#>

<#PageName <%=page.Title%>#>
<#Href <%=((page.Path == "main")? "/" : page.Path)%>#>
```

If the return value is placed between the standard syntax tags, the syntax will be automatically closed before the return value and the syntax will be opened after it.

pasting template auto step finally for standard syntax
```html
<div class="header">
  <a href="#default"><%=model.Title%></a>
  <div class="header-right">
    <% foreach (PageItem page in model.PageItemList)
    {
        %><a href="<%=((page.Path == "main")? "/" : page.Path)%>"><%=page.Title%></a><%
    }
    %>
  </div>
</div>
```
**Global template**

Global template are a new feature of the CodeBehind framework. All view pages call this template.

It is recommended to use only template blocks in the global format.

This template is called in `code_behind/global_template.astx` path.
