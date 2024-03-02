## Razor syntax reference for CodeBehind framework

In CodeBehind framework the Razor syntax will also be created in the aspx files and the Razor syntax will be automatically determined from the standard syntax. In addition, it is not possible to combine Razor syntax and standard syntax.

Razor syntax in CodeBehind is very similar to Razor syntax in cshtml pages in .NET Core, but in some cases there may be slight differences. Also note that if there is an error in aspx pages that are created with Razor syntax, it is different from Razor syntax errors in .NET Core cshtml pages. The Elanat team doesn't know the Microsoft approach, and the support for Razor pages in CodeBehind was created from the ground up by the Elanat team.

**An HTML page combined with Razor syntax**
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

### Razor syntax

**Escape Razor syntax**

To escape the @ symbol in Razor markup, use a second @ symbol:
```cshtml
<p>@@user</p>
```

The above code will be displayed as follows after compilation:
```cshtml
<p>@user</p>
```

**Email**

If the characters before and after the @ symbol are letters or numbers, it is not considered as syntax. So the email remains intact:
```cshtml
<p>Example of an email: yourname@example.com</p>
```

### Implicit Razor expressions

Implicit Razor expressions start with @ followed by C# code:
```cshtml
<p>@DateTime.Now</p>
<p>@DateTime.IsLeapYear(2024)</p>
```

### Razor syntax determination

To determine the razor syntax in aspx pages, there must be @page at the beginning of the page.

Example

```diff
+@page
<!DOCTYPE html>
...
```

### Page attributes in Razor syntax

The CodeBehind framework framework supports several attributes for view pages. Each of the adjectives are placed at the top of the page. Page attributes and their values must be specified on one line only.

**Model attribute**

To specify the model attribute, the string @model must be written and then added after the space character of the model class.

Example
```diff
@page
+@model YourProjectName.DefaultModel
<!DOCTYPE html>
...
```

**Controller attribute**

To specify the controller attribute, the string @controller must be written and then added after the space character of the controller class.

Example
```diff
@page
+@controller YourProjectName.DefaultController
<!DOCTYPE html>
...
```

**Layout attribute**

To specify the layout attribute, the string @layout must be written, and then after the space character, the path of the layout file should be placed between two double quotes.

Example
```diff
@page
+@layout "/main-layout.aspx"
<!DOCTYPE html>
...
```

**Template attribute**

To specify the template attribute, the string @template must be written, and then after the space character, the path of the template file should be placed between two double quotes.

Example
```diff
@page
+@template "/templates/template1.aspx"
<!DOCTYPE html>
...
```

**Break attribute**

To specify the break attribute, only the string @break should be written.

Example
```diff
@page
+@break
<!DOCTYPE html>
...
```

**Islayout attribute**

To specify the islayout attribute, only the string @islayout should be written.

Example
```diff
@page
+@islayout
<!DOCTYPE html>
...
```

### Code block

**Razor syntax example for code block**
```cshtml
@{
    string Note = "Elanat CMS was created to be a reliable system in .NET and an honor for .NET programmers and can be compared to other systems under PHP and JAVA.";
}

<p>@Note</p>
```
## Extension block
### Control structures

**Loop**

**Razor syntax example for foreach loop**
```cshtml
@foreach (NameValue nv in NameValues)
{
    <b>Name: @nv.Name</b>
    <p>Value: @nv.Value</p>
}
```

### Special cases

**Escape apostrophe**

Note: If you use quote ('), double quote ("), and backtick (`) characters, you must either re-use these characters before reaching the closing bracket (}), or write the closing bracket on a lower line, or the closing bracket should end on the next line.

```cshtml
@if (IsTrue)
{
	<p>You don't do it.</p>}<b>bold text</b>
}
```

In the code above, there is a character quote (') and closing bracket (}) is closed in the same line. After that, the html character is written; the above code may give an unexpected error, so it should be written as below.

```cshtml
@if (IsTrue)
{
	<p>You don't do it.</p>
}
<b>bold text</b>
```

Note: You cannot code in conditional blocks and loops in default cshtml pages in ASP.NET Core, but in the CodeBehind framework you will be allowed to code; therefore, for JavaScript codes, it is necessary to use @ and : characters before each character at the beginning of the lines.
