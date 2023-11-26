## HtmlData classes

You can use classes located in the HtmlData namespace.

The Elanat team currently supports three commonly used and recurring data types and will add more data types to the HtmlData namespace in the future.

Attribute and AttributeCollection
This data type adds one or more attributes to the html tag.

OptionTag and OptionTagCollection
This type of data is used for drop-down and fixed lists, and you can call and add this type of data inside the select tag.

**Example**

View (aspx page) (Razor syntax)
```cshtml
<select name="framework">
    @model.FrameworkOptionTags
</select>
```

View (aspx page) (standard syntax)
```aspx
<select name="framework">
    <%=model.FrameworkOptionTags%>
</select>
```

Controller
```csharp
using CodeBehind;
using CodeBehind.HtmlData;

namespace YourProjectName
{
    public partial class DefaultController : CodeBehindController
    {
        public DefaultModel model = new DefaultModel();

        public void PageLoad(HttpContext context)
        {
            OptionTagCollection options = new OptionTagCollection();
            options.Add("code_behind", "CodeBehind", true);
            options.Add("asp_dot_net_core", "ASP.NET Core");
            options.Add("django", "Django");
            options.Add("laravel", "Laravel");
            options.Add("spring_boot", "Spring Boot");
            options.Add("ruby_on_rails", "Ruby on Rails");

            model.FrameworkOptionTags = options.GetString();

            View(model);
        }
    }
}
```

Result after response
```html
<select name="framework">
    <option value="code_behind" selected>CodeBehind</option>
    <option value="asp_dot_net_core">ASP.NET Core</option>
    <option value="django">Django<option>
    <option value="laravel">Laravel</option>
    <option value="spring_boot">Spring Boot</option>
    <option value="ruby_on_rails">Ruby on Rails</option>
</select>
```

CheckBoxItem and CheckBoxItemCollection
Sometimes it happens that you need a single list where the user can select more than one data; this data type adds lists of checkbox tags.
