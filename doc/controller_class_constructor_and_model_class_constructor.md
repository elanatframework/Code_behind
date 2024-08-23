## Controller class constructor and Model class constructor

The constructor of the Controller class and the Model class has nothing to do with the CodeBehind constructor.

Before we explain the details about the constructor of the Controller class and the Model class, we must say that the constructor of the Controller class and the Model class has nothing to do with the CodeBehind constructor.

Please read the following article about the CodeBehind constructor:
[CodeBehind Constructor](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/constructor_method.md)

As you know, in the modern MVC architecture in the CodeBehind framework, there is no need to configure the Controller in the Route, and the requests reach the View first. Now, in the CodeBehind framework, a possibility has been added to be able to set values ​​of Controller classes and Model classes in the constructor methods of the View.

Example

View (Default.aspx)
```html
@page
@controller MyController()
@controllerconstructor(context.Request.Form)
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Controller class constructor</title>
</head>
<body>
    <form method="post" action="/">
        <label for="txt_TextBox">TextBox</label>
        <input name="txt_TextBox" id="txt_TextBox" type="text" />
        <br>
        <input name="btn_Button" type="submit" value="Click to send data" />
    </form>
</body>
</html>
```

The HTML code above is a CodeBehind Framework View page that has a button that submits a textbox. The `@controllerconstructor` variable passes the `context.Request.Form` value to the constructor of the Controller class.

Controller
```csharp
using CodeBehind;

public partial class MyController : CodeBehindController
{
    private readonly IFormCollection _Form;

    public MyController(IFormCollection Form = null)
    {
        _Form = Form;
    }
    
    public void CodeBehindConstructor()
    {
        if (!string.IsNullOrEmpty(_Form["btn_Button"]))
            btn_Button_Click();
    }

    private void btn_Button_Click()
    {
        string TextBoxValue = _Form["txt_TextBox"];

        Write(TextBoxValue);

        IgnoreViewAndModel = true;
    }
}
```

The `MyController` class has a constructor that accepts an `IFrmCollection` object that is used to access form data. When the Controller is called, the `_Form` field, which is private and read-only, is initialized in the constructor method of `MyController`.

The `_Form` field is initialized with the `context.Request.Form` input argument from the View page. This is a dependency injection.

The `CodeBehindConstructor` method is called when the page loads and checks if the button has been clicked. If the button is clicked, it calls the `btn_Button_Click` method, which reads the value of the text box and writes it to the page. The `IgnoreViewAndModel` property is set to true, which clears the contents of the View page and displays only the textbox string in the output.

> Note: Considering that in the CodeBehind framework it is possible to configure the Controller in the Route and also to call a controller, the controller class must have a constructor without arguments. For this reason, we set the IFormCollection parameter in the constructor method equal to null; of course, we can also create a constructor class without arguments value (`public MyController()`). 

### Define constructor method class by Attribute in View

You can call constructor method of Controller class and Model class on View pages.

**Razor syntax**

To call the constructor method of the Controller class in View, the string `@controllerconstructor` must be written and then the input arguments should be placed between parentheses.

Example:
`@controllerconstructor(26, "my text", 'c')`

To call the constructor method of the Controller class in View, the string `@modelconstructor` must be written and then the input arguments should be placed between parentheses.

Example:
`@modelconstructor(26, "my text", 'c')`

**Standard syntax**

To call the constructor method of the Controller class in View, the `controllerconstructor` string must be written then the equals character must be added, and the input arguments should be placed between parentheses and must be placed between the double quotation marks (`"`). If the input arguments contain quotation marks (`"`), you must use the code `&quot;` instead of the quotation marks.

Example:
`<%@ page ... controllerconstructor="(26, &quot;my text&quot;, 'c')" ... %>`

To call the constructor method of the Model class in View, the `modelconstructor` string must be written then the equals character must be added, and the input arguments should be placed between parentheses and must be placed between the double quotation marks (`"`). If the input arguments contain quotation marks (`"`), you must use the code `&quot;` instead of the quotation marks.

Example:
`<%@ page ... modelconstructor="(26, &quot;my text&quot;, 'c')" ... %>`
