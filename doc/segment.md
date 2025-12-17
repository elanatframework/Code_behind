## Segment

Segment is a attribute that applies to aspx pages. Segment is a feature whose activation makes all paths after the aspx path refer to the current aspx path. Segment is one of the revolutionary ideas of the Elanat team. Enabling segment in aspx pages gives you full control over the paths.

Example of segment activation in Razor syntax
```diff
@page
+@segment
<!DOCTYPE html>
<html>
...
```

Example of segment activation in standard syntax
```html
<%@ Page Segment="true" %>
<!DOCTYPE html>
<html>
...
```

If you enable segment in the `/page/about.aspx` path, any path added after the current path will be considered a segment and the executable file in the `/page/about.aspx` path will still be executed.

Example

`/page/about.aspx/segment1/segment2/.../segmentN`

If you enable the segment in an executable file called Default.aspx, you will still have access to the default path.

Example

`/page/about/Default.aspx/segment1/segment2/.../segmentN`

or

`/page/about/segment1/segment2/.../segmentN`

You will have access to segment in all three segments, view, controller and model.

Example segment in view (Razor syntax)
```cshtml
<b>segment 1 is: @Segment.GetValue(0)</b>
```

Example segment in view (standard syntax)
```aspx
<b>segment 1 is: <%=Segment.GetValue(0)%></b>
```

Example segment in controller
```diff
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultController : CodeBehindController
    {
        public void PageLoad(HttpContext context)
        {
+           Write(Segment.GetValue(0));
        }
    }
}
```

Example segment in model
```diff
using CodeBehind;

namespace YourProjectName
{
    public partial class DefaultModel : CodeBehindModel
    {
        public void CodeBehindConstructor()
        {
+           Write(Segment.GetValue(0));
        }
    }
}
```

Activating the segment makes it no longer necessary to have a query string.

Please note that if you activate the segment in a path, none of the view files (aspx) in the subdirectories of that path will be executed.

Example

`/page/about/Default.aspx`

If you enable the segment in the path above, the path below will no longer be accessible.

`/page/about/license/Default.aspx`

You can use Exist method and check the existence of segment values.

Example
```html
@if (!Segment.Exist(0))
{
    <b>Value not exist</b>
}
```
