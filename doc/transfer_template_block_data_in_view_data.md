## Transfer template block data in ViewData

The ability to send template blocks through ViewData is a feature of the CodeBehind framework. You can now enclose template variables within `{@#TempName}` brackets so that double-quote (`"{@#TempName}"`) characters do not cause problems in code blocks.

Template variables placed between open and closed brackets make the double-quote and `\n` characters of the block suitable for placement in a string. This will help you add template blocks in ViewData and call them in other pages (like layout).

Example:

```html
@page
@layout "/layout.aspx"
@{
    @ViewData.Add("title", "Company name");
    @ViewData.Add("script", "{@#TempName}");
}
...
@#TempName{
    <script>
        $(window).load(function() {
            // init Isotope
            var $projects = $('.projects').isotope({
                itemSelector: '.project',
                layoutMode: 'fitRows'
            });
            $(".filter-btn").click(function() {
                var data_filter = $(this).attr("data-filter");
                $projects.isotope({
                    filter: data_filter
                });
                $(".filter-btn").removeClass("active");
                $(".filter-btn").removeClass("shadow");
                $(this).addClass("active");
                $(this).addClass("shadow");
                return false;
            });
        });
    </script>
}
```

After placing the template blocks in the template variables, the values of the `@#TempName` block in the above codes are added to the ViewData as below.

```csharp
@ViewData.Add("script", "\n    <script>\n        $(window).load(function() {\n            // init Isotope\n            var $projects = $('.projects').isotope({\n                itemSelector: '.project',\n                layoutMode: 'fitRows'\n            });\n            $(\".filter-btn\").click(function() {\n                var data_filter = $(this).attr(\"data-filter\");\n                $projects.isotope({\n                    filter: data_filter\n                });\n                $(\".filter-btn\").removeClass(\"active\");\n                $(\".filter-btn\").removeClass(\"shadow\");\n                $(this).addClass(\"active\");\n                $(this).addClass(\"shadow\");\n                return false;\n            });\n        });\n    </script>\n");
```
