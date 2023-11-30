## Layout
A layout is a top-level template for views in an application, which defines a common layout for pages, providing a consistent user experience as users navigate between pages. Layouts are particularly useful for web applications with shared UI elements, such as headers, navigation menus, and footers. By using layouts, you can reduce duplicate code in views and maintain a consistent look and feel across multiple pages in your application.

View
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

Layout page (layout.aspx)
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

Result
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
