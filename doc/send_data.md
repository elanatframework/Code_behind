## Send data

In this article, we want to teach you how to submit form data in the CodeBehind framework. To keep it simple, we just added a View page with a Controller class and didn't use a Model.

### View

We created a View page that contains several inputs that are inside the form tag. The form tag uses the post method; in order to upload a file, the `enctype` attribute is added along with the `multipart/form-data` value in the form tag.

**View page is a form that includes the following inputs:**

 - TextBox
 - Select
 - Hidden
 - CheckBox
 - RadioButton
 - File
 - Submit

View (Form.aspx)
```html
@page
@controller YourProjectName.FormController
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Send Form Data</title>
</head>
<body>
    <form method="post" action="/Form.aspx" enctype="multipart/form-data">

        <label for="txt_TextBox">TextBox</label>
        <input name="txt_TextBox" id="txt_TextBox" type="text" />
        <br>
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
        <br>
        <label for="cbx_CheckBox">CheckBox</label>
        <input name="cbx_CheckBox" id="cbx_CheckBox" type="checkbox" />
        <br>
        <br>
        <input value="1" name="rdbtn_RadioButton" id="rdbtn_RadioButton1" type="radio" /><label for="rdbtn_RadioButton1">RadioButton 1</label>
        <input value="2" name="rdbtn_RadioButton" id="rdbtn_RadioButton2" type="radio" /><label for="rdbtn_RadioButton2">RadioButton 2</label>
        <input value="3" name="rdbtn_RadioButton" id="rdbtn_RadioButton3" type="radio" /><label for="rdbtn_RadioButton3">RadioButton 3</label>
        <br>
        <br>
        <label for="upd_File">File Upload</label>
        <input name="upd_File" id="upd_File" type="file" />
        <br>
        <br>
        <input name="hdn_Hidden" type="hidden" value="This is hide value" />

        <input name="btn_Button" type="submit" value="Click to send data" />

    </form>
</body>
</html>
```

This is the image of the View screen after execution.

![Send form data](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/qmjaxf7m6huufhe6v9p2.png)

### Controller

Adding the Controller to the View page causes the `PageLoad` method of the Controller class to be executed every time the View page is called. Submit type inputs are buttons that send their data only when they are clicked; In other words, if you have several buttons in a form, on each button that is clicked, the data of that button and all the data of other inputs will be sent to the server, except for the data of other buttons.

Controller class
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class FormController : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.Form["btn_Button"]))
                btn_Button_Click(context);
        }

        private void btn_Button_Click(HttpContext context)
        {
            string TextBoxValue = context.Request.Form["txt_TextBox"];
            string HiddenValue = context.Request.Form["hdn_Hidden"];
            string SelectValue = context.Request.Form["ddlst_Select"];

            bool CheckBoxValue = context.Request.Form["cbx_CheckBox"] == "on";

            string RadioButtonValue = context.Request.Form["rdbtn_RadioButton"];


            // Get File
            IFormFile FileUploadValue = context.Request.Form.Files["upd_File"];

            if ((FileUploadValue != null) && (FileUploadValue.Length > 0))
            {
                CodeBehind.API.Path path = new CodeBehind.API.Path();

                using (Stream stream = new FileStream(path.BaseDirectory + "/" + FileUploadValue.FileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    FileUploadValue.CopyTo(stream);
                }

                Write("<p>File was uploaded" + "</p>");
            }

            Write("<p>TextBoxValue=" + TextBoxValue + "</p>");
            Write("<p>HiddenValue=" + HiddenValue + "</p>");
            Write("<p>SelectValue=" + SelectValue + "</p>"); ;
            Write("<p>CheckBoxValue=" + (CheckBoxValue? "true" : "false") + "</p>");
            Write("<p>RadioButtonValue=" + RadioButtonValue + "</p>");

            IgnoreViewAndModel = true;
        }
    }
}
```

We added a Controller class and in the `PageLoad` method we check the presence of data named `btn_Button` in the form data.

Checking the existence of data with the name `btn_Button`
```csharp
if (!string.IsNullOrEmpty(context.Request.Form["btn_Button"]))
    btn_Button_Click(context);
```

According to the above codes, if there is a data named `btn_Button`, the `btn_Button_Click` method is called with the context argument.

Clicking on the `Click to send data` button causes the data of the form named `btn_Button` to be sent to the server.

The HTML tag below shows the btn_Button button
```html
<input name="btn_Button" type="submit" value="Click to send data" />
```

The text, hidden, select, and radio inputs have text values and their data is retrieved as follows.

```csharp
string InputValue = context.Request.Form["InputName"];
```

The `InputName` value is an example and the name of the input tag should be replased.

Example:
```csharp
string TextBoxValue = context.Request.Form["txt_TextBox"];
```

The checkbox input sends a text data with the value on if its tick value is active.

The code below shows how to read checkbox data.
```csharp
bool CheckBoxValue = context.Request.Form["cbx_CheckBox"] == "on";
```

To receive the file, we used the IFormFile data type.

How to get the file is specified in the codes below.

```csharp
IFormFile FileUploadValue = context.Request.Form.Files["upd_File"];

if ((FileUploadValue != null) && (FileUploadValue.Length > 0))
{
	CodeBehind.API.Path path = new CodeBehind.API.Path();

	using (Stream stream = new FileStream(path.BaseDirectory + "/" + FileUploadValue.FileName, FileMode.Create, FileAccess.ReadWrite))
	{
		FileUploadValue.CopyTo(stream);
	}

	Write("<p>File was uploaded" + "</p>");
}
```

As it is clear in the codes above, first we create a data value of `IFormFile` type and value it with the input file data.

Then we check whether the input file is initialized on the user side or not. If the initialization is done on the user side, the file will be uploaded.

To check that we have received the data correctly, we add the received data on the page using the `Write` method.

We active the `IgnoreViewAndModel` attribute; enabling the `IgnoreViewAndModel` attribute causes the View to be completely ignored and only the Controller values are added to the page.

The values of the text below are displayed after clicking on the Click to send data button.

```
File was uploaded

TextBoxValue=my text box value

HiddenValue=This is hide value

SelectValue=3

CheckBoxValue=true

RadioButtonValue=2
```
