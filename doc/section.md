## Section

Section is a attribute that applies to aspx pages. Section is a feature whose activation makes all paths after the aspx path refer to the current aspx path. Section is one of the revolutionary ideas of the Elanat team. Enabling section in aspx pages gives you full control over the paths.

Example of section activation in Razor syntax
```diff
@page
+@section
<!DOCTYPE html>
<html>
...
```

Example of section activation in standard syntax
```html
<%@ Page Section="true" %>
<!DOCTYPE html>
<html>
...
```

If you enable section in the `/page/about.aspx` path, any path added after the current path will be considered a section and the executable file in the `/page/about.aspx` path will still be executed.

Example

`/page/about.aspx/section1/section2/.../sectionN`

If you enable the section in an executable file called Default.aspx, you will still have access to the default path.

Example

`/page/about/Default.aspx/section1/section2/.../sectionN`

or

`/page/about/section1/section2/.../sectionN`

You will have access to section in all three sections, view, controller and model.

Example section in view (Razor syntax)
```cshtml
<b>section 1 is: @Section.GetValue(0)</b>
```

Example section in view (standard syntax)
```aspx
<b>section 1 is: <%=Section.GetValue(0)%></b>
```

Example section in controller
```diff
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultController : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {
+           Write(Section.GetValue(0));
        }
    }
}
```

Example section in model
```diff
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultModel : CodeBehindModel
    {
        public void CodeBehindConstructor()
        {
+           Write(Section.GetValue(0));
        }
    }
}
```

Activating the section makes it no longer necessary to have a query string.

Please note that if you activate the section in a path, none of the view files (aspx) in the subdirectories of that path will be executed.

Example

`/page/about/Default.aspx`

If you enable the section in the path above, the path below will no longer be accessible.

`/page/about/license/Default.aspx`

You can use Exist method and check the existence of section values.

Example
```html
@if (!Section.Exist(0))
{
    <b>Value not exist</b>
}
```
