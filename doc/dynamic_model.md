## Dynamic Model

In this tutorial, we want to teach you how to create a dynamic Model in the CodeBehind framework.

This is the MVC example that we usually use to describe the CodeBehind framework:

View File: Default.aspx (razor syntax)
```html
@page
@controller YourProjectName.DefaultController
@model {YourProjectName.DefaultModel}
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

Model Class: Default.aspx.Model.cs
```csharp
namespace YourProjectName
{
    public class DefaultModel
    {
        public string PageTitle { get; set; }
        public string BodyValue { get; set; }
    }
}
```

Controler Class: Default.aspx.Controller.cs
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultController : CodeBehindController
    {
        public DefaultModel model = new DefaultModel();
        public void PageLoad(HttpContext context)
        {
            model.PageTitle = "My Title";
            model.BodyValue = "HTML Body";
            View(model);
        }
    }
}
```

In this example, we can set an instance of the Model class in the controller as we want, so that its values are placed in the View.

Consider a situation where we want to place Models whose class names we do not know in the View. In the usual case, we cannot call a Model class other than the Model we specified in the View page in the controller page.

But don't worry!, you can call a dynamic Model class in the CodeBehind framework.

We will modify the same MVC example that we introduced earlier so that you can better understand how to create a dynamic Model.

View File: Default.aspx (razor syntax)
```diff
@page
@controller YourProjectName.DefaultController
-@model {YourProjectName.DefaultModel}
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
-   <title>@model.PageTitle</title>
+   <title>@controller.model.PageTitle</title>
</head>
<body>
-   @model.BodyValue
+   @controller.model.BodyValue
</body>
</html>
```

According to the View codes above, the Model attribute is omitted in the second line. Also, the controller prefix has been added to the models in the View page.

New Model Class: MyClass.cs
```csharp
namespace YourProjectName
{
    public class MyClass
    {
        public string PageTitle { get; set; }
        public bool BodyValue { get; set; }
    }
}
```

The above code shows the same Model class with the name `DefaultModel`, whose name has been changed to `MyClass`, and the value of the `BodyValue` attribute has been changed to boolean.

Controler Class: Default.aspx.Controller.cs
```csharp
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultController : CodeBehindController
    {
        public MyClass model = new MyClass();
        public void PageLoad(HttpContext context)
        {
            model.PageTitle = "My Title";
            model.BodyValue = true;
        }
    }
}
```

According to the Controller class above, an instance of the `MyClass` class named model is created inside the `DefaultController` class and its values are set in the `PageLoad` method.

In this example, the type of `BodyValue` attribute in Model has changed from string to boolean; this is not the only advantage of creating a dynamic Model.

### Advantages of using a dynamic Model in the CodeBehind framework:

 - **Flexibility:** With a dynamic Model, you can easily change the type or structure of your Model without impacting the rest of your application. This allows for more flexibility and adaptability in your development process. Dynamic Models can easily incorporate new data and changes in variables, making them adaptable to changing circumstances.

 - **Reusability:** By using dynamic Models, you can reuse the same controller logic for different Model classes. This can save time and effort in coding and testing, as you do not need to duplicate code for each Model class.

 - **Scalability:** Dynamic Models allow you to scale your application more easily, as you can quickly add new Model classes or modify existing ones without having to make extensive changes to your codebase.

 - **Simplified maintenance:** Having dynamic Models can make your codebase easier to maintain, as changes to one Model class do not necessarily impact other parts of your application. This can result in cleaner and more manageable code.

 - **Improved predictive power:** Dynamic Models can be used to predict future outcomes based on current conditions and how they are likely to evolve over time.

 - **Better representation of complex systems:** Dynamic Models allow for a more accurate representation of how variables interact and change over time in complex systems.

### Disadvantages of using a dynamic Model in the CodeBehind framework:

 - **Code errors:** Dynamic Models can make it easier to introduce coding errors, as there is less compile-time checking to catch mistakes.

 - **Limited tooling support:** Some development tools may not fully support dynamic Models, leading to potential limitations in IDE features such as code completion and refactoring.

### Conclusion

Overall, using dynamic Models in the CodeBehind framework can lead to a more efficient and flexible development process, allowing you to easily adapt to changes in requirements or business needs. Using the dynamic Model is very useful in some situations, However, too much dynamism for the Model may break the structural coherence of MVC. Our recommendation is be careful in using dynamic Model and to use dynamic Model in a limited way only in situations where you have to.
