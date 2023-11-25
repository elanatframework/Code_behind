## Razor syntax reference for CodeBehind framework

In CodeBehind framework the Razor syntax will also be created in the aspx files and the Razor syntax will be automatically determined from the standard syntax. In addition, it is not possible to combine Razor syntax and standard syntax.

 Razor syntax in CodeBehind is very similar to Razor syntax in cshtml pages in .NET Core, but in some cases there may be slight differences. Also note that if there is an error in aspx pages that are created with Razor syntax, it is different from Razor syntax errors in .NET Core cshtml pages. The Elanat team doesn't know the Microsoft approach, and the support for Razor pages in CodeBehind was created from the ground up by the Elanat team.

 Razor syntax example for page attribute

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

Razor syntax example for code block
```cshtml
@{
    string Note = "Elanat CMS was created to be a reliable system in .NET and an honor for .NET programmers and can be compared to other systems under PHP and JAVA.";
}

<p>@Note</p>
```

Razor syntax example for foreach loop
```cshtml
@foreach (NameValue nv in NameValues)
{
    <b>Name: @nv.Name</b>
    <p>Value: @nv.Value</p>
}
```
