## Using Web-Forms

Web-Forms are a new technology in the CodeBehind framework. Web-Forms allow developers to easily manage HTML tags on the server side. To create this new technology, a library called [WebFormsJS](https://github.com/elanatframework/Web_forms) has been created.

WebFormsJS is a JavaScript library that provides the infrastructure for interacting with web controls in the CodeBehind framework. WebFormsJS has a two-way communication with the CodeBehind framework. The communication between the two is done automatically based on a new protocol.

Using WebFormsJS allows the developers to focus on the server response and therefore there is no need to develop the front side and the developers set the controls on the server-side. WebFormsJS can also be used outside of the CodeBehind framework.

The bandwidth consumption when using WebFormsJS is very low. WebFormsJS is like a gasoline car that absorbs carbon pollution as much as it pollutes the air.

Advantages of using WebFormsJS:

- WebFormsJS provides features like postback, progress bar and script extraction.
- WebForms is an advanced system that can be run with simple HTML pages without View or server script pages.
- WebFormsJS automatically sends form data through Ajax. WebFormsJS serializes form data as a string or a FormData object, depending on whether the form is multipart or not.
- Using WebFormsJS reduces the complexity of web development.

### How to use Web-Forms in CodeBehind framework

We create a new View in which there is an input of select type; we want to add new option values ​​in select, so we put two textbox input for the name and value of the new option in the View, and we also create a checkbox input for whether the new option is selected or not in this View.

View (Form.aspx)
```html
@page
@controller FormController
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Send Form Data</title>
    <script type="text/javascript" src="/script/web-forms.js"></script>
</head>
<body>
    <form method="post" action="/Form.aspx">

        <label for="txt_SelectName">Select name</label>
        <input name="txt_SelectName" id="txt_SelectName" type="text" />
        <br>
        <label for="txt_SelectValue">Select value</label>
        <input name="txt_SelectValue" id="txt_SelectValue" type="text" />
        <br>
        <label for="cbx_SelectIsSelected">Select Is Selected</label>
        <input name="cbx_SelectIsSelected" id="cbx_SelectIsSelected" type="checkbox" />
        <br>
        <label for="ddlst_Select">Select</label>
        <select name="ddlst_Select" id="ddlst_Select">
            <option value="1">One 1</option>
            <option value="2">Two 2</option>
            <option value="3">Three 3</option>
            <option value="4">Four 4</option>
            <option value="5">Five 5</option>
        </select>
        <br>
        <input name="btn_Button" type="submit" value="Click to send data" />

    </form>
</body>
</html>
```

We first activate the `IgnoreViewAndModel` attribute; by doing this, we prevent the View page from returning. Then we create an instance of the `WebForms` class and add a new value in the drop-down list according to the values ​​sent through the Form method. Finally, we must place the created instance of the `WebForms` class inside the `Control` method.

Controller (FormController)
```csharp
using CodeBehind;

public partial class FormController : CodeBehindController
{
    public void PageLoad(HttpContext context)
    {
        if (!string.IsNullOrEmpty(context.Request.Form["btn_Button"]))
            btn_Button_Click(context);
    }

    private void btn_Button_Click(HttpContext context)
    {
        IgnoreViewAndModel = true;

        Random rand = new Random();
        string RandomColor = "#" + rand.Next(16).ToString("X") + rand.Next(16).ToString("X") + rand.Next(16).ToString("X") + rand.Next(16).ToString("X") + rand.Next(16).ToString("X") + rand.Next(16).ToString("X");

        WebForms Form = new WebForms();

        string SelectValue = context.Request.Form["txt_SelectValue"];
        string SelectName = context.Request.Form["txt_SelectName"];
        bool SelectIsChecked = context.Request.Form["cbx_SelectIsSelected"] == "on";

        Form.AddOptionTag(InputPlace.Id("ddlst_Select"), SelectName, SelectValue, SelectIsChecked);
        Form.SetBackgroundColor(InputPlace.Tag("body"), RandomColor);

        Control(Form);
    }
}
```

Each time the button is clicked, new values ​​are added to the drop-down list and the background changes to a random color.

This is a simple example of CodeBehind framework interaction with WebFormsJS.
