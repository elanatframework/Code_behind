## Examples of development

**In aspx pages, you will access HttpContext with context**

View file (razor syntax)
```aspx
@page
@{
    string HasValue = (!string.IsNullOrEmpty(context.Request.Query["value"]))? "Yes" : "No";
}

<div>
    <h1>Exist value in querystring? @HasValue</h1>
    <hr>
    <b>value is: @(context.Request.Query["value"].ToString())</b>
</div>
```

View file (standard syntax)
```aspx
<%@ Page %>
<% string HasValue = (!string.IsNullOrEmpty(context.Request.Query["value"]))? "Yes" : "No"; %>

<div>
    <h1>Exist value in querystring? <%=HasValue%></h1>
    <hr>
    <b>value is: <%=context.Request.Query["value"].ToString()%></b>
</div>
```

**To receive the information sent through the form, you can follow the instructions below**
```csharp
public DefaultModel model = new DefaultModel();
public void PageLoad(HttpContext context)
{
    if (!string.IsNullOrEmpty(context.Request.Form["btn_Add"]))
        btn_Add_Click();

    View(model);
}

private void btn_Add_Click()
{
    model.PageTitle = "btn_Add Button Clicked";
}
```
