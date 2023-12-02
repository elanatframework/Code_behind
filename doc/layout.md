## Layout
A layout is a top-level template for views in an application, which defines a common layout for pages, providing a consistent user experience as users navigate between pages. Layouts are particularly useful for web applications with shared UI elements, such as headers, navigation menus, and footers. By using layouts, you can reduce duplicate code in views and maintain a consistent look and feel across multiple pages in your application.

Layout page (layout.aspx) (Razor syntax)
```cshtml
@page
@islayout
<!DOCTYPE html>
<html>
	<head>
		<title>@ViewData.GetValue("title")</title>
	</head>
	<body>
@PageReturnValue
	</body>
</html>
```

In the example above, an aspx file (layout.aspx) has been added to the project in the wwwroot path.
Here we have specified that this page is a layout by adding the `@islayout` variable to the page attributes section. `PageReturnValue` variable will add final values from aspx files in which this layout is introduced. Between the title tags, there is a NameValueCollection attribute (`ViewData`) that all aspx files have access to.

View (hello-world.aspx) (Razor syntax)
```cshtml
@page
@layout "/layout.aspx"
@{
    string HelloWorld = "Hello CodeBehind framework!";
    ViewData.Add("title", "Hello World!");
}
        <div>
            <h1>Text value is: @HelloWorld</h1>
        </div>
```

The above example shows an aspx file (hello-world.aspx) in which a layout is introduced.
On this page, `@layout` and the text inside the double quotes indicate that the page has a layout in the path wwwroot/layout.aspx. According to the above codes, a NameValue is added to the ViewData attribute with the name title and the value Hello World!.

Result in hello-world.aspx path
```cshtml
<!DOCTYPE html>
<html>
	<head>
		<title>Hello World!</title>
	</head>
	<body>
		<div>
			<h1>Text value is: Hello CodeBehind framework!</h1>
		</div>
	</body>
</html>
```

As you can see, the above result is obtained by calling the hello-world.aspx path.

You can add other pages in the view section.

Layout page (layout.aspx) (Razor syntax)
```cshtml
@page
@islayout
<!DOCTYPE html>
<html>
	<head>
		<title>@ViewData.GetValue("title")</title>
	</head>
	<body>
@LoadPage("/header.aspx")
@PageReturnValue
	</body>
</html>
```

According to the above code, only one LoadPage function has been added to the previous example.

The `LoadPage("/header.aspx")` function also adds a page at the path `wwwroot/header.aspx` to the same section.

Please note that if you want to access the HttpContet in the header.aspx file, you must write a method similar to the following:
`@LoadPage("/header.aspx", context)`

Header page (header.aspx) (Razor syntax)
```cshtml
@page
@break
@{
    string WebsiteName = "My Company";
}
        <header>
            </b>Website name: @WebsiteName</b>
        </header>
        <br>
```

The header file is clear in the example above. The presence of the `@break` attribute means that the page will no longer be directly accessible. This file cannot be accessed in the browser.

Result in hello-world.aspx path
```html
<!DOCTYPE html>
<html>
	<head>
		<title>Hello World!</title>
	</head>
	<body>
		<header>
			<b>Website name: My Company</b>
		</header>
		<br>
		<div>
			<h1>Text value is: Hello CodeBehind framework!</h1>
		</div>
	</body>
</html>
```

As you can see, the output of the header.aspx file is added to the output of the hello-world.aspx file.

**you can use standard syntax for layout**

Layout page (layout.aspx) (standard syntax)
```html
<%@ Page IsLayout="true" %>
<!DOCTYPE html>
<html>
	<head>
		<title><%=ViewData.GetValue("title")%></title>
	</head>
	<body>
<%=LoadPage("/header.aspx")%>
<%=PageReturnValue%>
	</body>
</html>
```

View (standard syntax)
```html
<%@ Page Layout="/layout.aspx" %>
<%
    string HelloWorld = "Hello CodeBehind framework!";
    ViewData.Add("title", "Hello World!");
%>
        <div>
            <h1>Text value is: <%=HelloWorld%></h1>
        </div>
```

Header page (header.aspx) (standard syntax)
```html
<%@ Page Break="true" %>
<% string WebsiteName = "My Company"; %>
        <header>
            <b>Website name: <%=WebsiteName%></b>
        </header>
        <br>
```
