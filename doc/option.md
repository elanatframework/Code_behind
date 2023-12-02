## Option

There is an options file in the code_behind/options.ini path. This file will contain many options for customization.

The options file in CodeBehind is as follows:

```ini
[CodeBehind options]; do not change order
view_path=wwwroot
move_view_from_wwwroot=true
rewrite_aspx_file_to_directory=false
access_aspx_file_after_rewrite=false
ignore_default_after_rewrite=true
start_trim_in_aspx_file=true
inner_trim_in_aspx_file=true
end_trim_in_aspx_file=true
set_break_for_layout_page=true
```

**The possibility to load aspx page names as directory names**

**Change view Path**

(`view_path=wwwroot`)

CodeBehind framework users can remove the path of aspx files from wwwroot and add them to their desired directory. If you use server-side code in view (aspx) pages, this option leads to more security of your applications.

**Move views from wwwroot path**

(`move_view_from_wwwroot=true`)

If you have selected the path of the view file in a path other than the wwwroot path, if the option to move from the wwwroot path is enabled, automatically after recompile, all aspx files will be moved from the wwwroot path to your chosen path.

**Rewrite aspx path as directory**

(`rewrite_aspx_file_to_directory=false`)

One of the interesting ideas of the Elanat team for the CodeBehind framework is the possibility of rewriting the path of aspx files as a directory name. If you enable this option, paths leading to an aspx file without the aspx extension will be treated as the name of a directory.

Note: You can safely activate this option because this rewrite will not create any extra load for processing.

Example:
access `/page/aboue.aspx` in `/page/about` path

Also access aspx file after rewrite as directory
(`access_aspx_file_after_rewrite=false`)
If you have enabled rewriting the path of aspx files as a directory name, enabling this option will allow you to still access the path of the aspx file.

**Ignore Default.aspx after rewrite**

(`ignore_default_after_rewrite=true`)

Naming the aspx file as Default.aspx makes it unnecessary to add the name of this file after the last directory, and this file is recognized automatically; If you have enabled rewriting the path of aspx files as a directory name, enabling this option will make the Default.aspx files be considered as the default file of the previous directory path. Therefore, the Default directory will not be executed.

**Don't worry about new lines and extra tabs and spaces.**

**Trim in start**

(`start_trim_in_aspx_file=true`)

If this option is active, every new line and extra tab and space will be deleted from the top of the aspx files.
No need to worry about <!DOCTYPE html> (or <html>) from now on! Add it further down and feel free to have an extra line.

Example:

You don't have to do this anymore
standard syntax
```html
<%@ Page %><!DOCTYPE html>
```

This way there will be no extra lines
```html
<%@ Page %>
<!DOCTYPE html>
```

You don't have to do this in Razor syntax
```html
@page<!DOCTYPE html>
```

This way there will be no extra lines
```html
@page
<!DOCTYPE html>
```

**Trim in inner aspx**

(`inner_trim_in_aspx_file=true`)

If this option is enabled, your coding that is placed after the next line will also delete the next line and there will be no extra line between the html tags in the design section.

Code blocks will not leave an extra line after the new line.

Example for Razor syntax
`@{ code }`

Example for standard syntax
`<% code %>`

**Trim in end**

(`end_trim_in_aspx_file=true`)

If this option is active, every new line and extra tab and space will be deleted from the bottom of the aspx files.

**Break for layout**

(`set_break_for_layout_page=true`)

If this option is enabled, layout files are automatically ignored from direct access and cannot be accessed via url.

In the future, more customization options will be added to the options file.
