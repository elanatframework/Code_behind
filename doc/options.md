## Options

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
convert_cshtml_to_aspx=false
show_minor_errors=false
error_page_path=/error.aspx/{value}
prevent_access_default_aspx=false
default_role=guest
web_forms_script_path=/script
auto_create_web_forms_script=true
recreate_web_forms_script_after_recompile=false
web_forms_view_place=<body>
use_default_controller=true
default_controller=DefaultController
use_section_in_default_controller=true
set_break_for_default_controller=true
access_controller_by_lower_case=true
just_access_controller_by_lower_case=true
ignore_prefix_controller=.
ignore_suffix_controller=.
put_two_underlines_equal_to_dash_for_controller=false
set_default_pages=true
```

**The possibility to load aspx page names as directory names**

**Change view Path**

(`view_path=wwwroot`)

CodeBehind framework users can remove the path of aspx files from wwwroot and add them to their desired directory. If you use server-side code in view (aspx) pages, this option leads to more security of your applications.

Operation location: Compiling view files

**Move views from wwwroot path**

(`move_view_from_wwwroot=true`)

If you have selected the path of the view file in a path other than the wwwroot path, if the option to move from the wwwroot path is enabled, automatically after recompile, all aspx and astx files will be moved from the wwwroot path to your chosen path.

Operation location: Compiling view files

**Rewrite aspx path as directory**

(`rewrite_aspx_file_to_directory=false`)

One of the interesting ideas of the Elanat team for the CodeBehind framework is the possibility of rewriting the path of aspx files as a directory name. If you enable this option, paths leading to an aspx file without the aspx extension will be treated as the name of a directory.

Note: You can safely activate this option because this rewrite will not create any extra load for processing.

Example:
access `/page/aboue.aspx` in `/page/about` path

Also access aspx file after rewrite as directory
(`access_aspx_file_after_rewrite=false`)
If you have enabled rewriting the path of aspx files as a directory name, enabling this option will allow you to still access the path of the aspx file.

Operation location: Compiling view files

**Ignore Default.aspx after rewrite**

(`ignore_default_after_rewrite=true`)

Naming the aspx file as Default.aspx makes it unnecessary to add the name of this file after the last directory, and this file is recognized automatically; If you have enabled rewriting the path of aspx files as a directory name, enabling this option will make the Default.aspx files be considered as the default file of the previous directory path. Therefore, the Default directory will not be executed.

Operation location: Compiling view files

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

Operation location: Compiling view files

**Trim in inner aspx**

(`inner_trim_in_aspx_file=true`)

If this option is enabled, your coding that is placed after the next line will also delete the next line and there will be no extra line between the html tags in the design section.

Code blocks will not leave an extra line after the new line.

Example for Razor syntax
`@{ code }`

Example for standard syntax
`<% code %>`

Operation location: Compiling view files

**Trim in end**

(`end_trim_in_aspx_file=true`)

If this option is active, every new line and extra tab and space will be deleted from the bottom of the aspx files.

Operation location: Compiling view files

**Break for layout**

(`set_break_for_layout_page=true`)

If this option is enabled, layout files are automatically ignored from direct access and cannot be accessed via url.

Operation location: Compiling view files

**Support cshtml extension**

(`convert_cshtml_to_aspx=false`)

In the options file, exist an option to support cshtml files so that users of the CodeBehind framework can easily distinguish the codes of the view section with code highlighters.

You can code in these files with razor syntax and standard syntax.
cshtml files will be available with aspx extension after compilation.
For default cshtml pages, the name should be Default.cshtml; so pages named Index.cshtml will not be the default route.

Please note that these files must not be added to projects in Visual Studio; the reason for this is that, in addition to the CodeBehind framework, these pages are also compiled in the default .NET mode and can create unstable situation and security risk conditions.
Also note that the default code highlighter of Visual Studio may in some cases have unnecessary errors from cshtml pages based on the CodeBehind framework.

Operation location: Compiling view files

**Show minor errors**

(`show_minor_errors=false`)

By enabling the show minor errors option, when compiling, errors that do not cause problems in the compilation process but are not optimal will be displayed in the `views_compile_error.log` file in the code_behind directory.

In the future, more customization options will be added to the options file.

Operation location: Compiling view files

**Error page path**

(`error_page_path=/error.aspx/{value}`)

The default CodeBehind template includes an error page. In the options file, there is an option that determines the path of the error file; the path of the error page is set by default in this option. In the error page, we activated the page section attribute by default. If you look carefully at the path of the error page in the options file, you will see the value value surrounded by two brackets. This is a variant and the numeric value of the error replaces this variant.

Example
`/error.aspx/500`

According to the path above, the value 500 is substituted for the {value} variant.

You can call up the error page according to the type of error.

The link below is a tutorial on how to configure the error page.

[Error handling](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/error_handling.md)

Operation location: Every request

**Prevent access for default.aspx file**

(`prevent_access_default_aspx=false`)

View files in CodeBehind framework have aspx extension; if you create a View file named Default.aspx in a directory, accessing the directory will execute that file. So Default.aspx is a default View for a directory path.

Example:
If there is a `Default.aspx` file in the root, the `example.com` request executes the `example.com/Default.aspx` path.
Similarly, if there is a `Default.aspx` file in `root/dir`, requesting `example.com/dir` will execute the `example.com/dir/Default.aspx` path.

However, the `Default.aspx` file path will still be accessible. Enabling this option disables access to the `Default.aspx` path.

Example:
The path `example.com/dir/Default.aspx` will not be available, but the path `example.com/dir/` will still be accessible.

By activating this option, additional urls are prevented and thus SEO is improved.

Operation location: Every request

**Default user role**

(`default_role=guest`)

This option is used to set the default role, the default value of which is `guest`. By default, any user who logs into your web application will have the `guest` role.

**Using Web-Forms**

**Web Forms Script Path**

(`web_forms_script_path=/script`)

The `web_forms_script_path` option specifies the path to the WebFormsJS file. According to the `/script` value, the WebFormsJS file is created in the following path:
`/script/web-forms.js`

**Should the Web-Forms script be created automatically?**

(`auto_create_web_forms_script=true`)

If the `auto_create_web_forms_script` option is enabled, the WebFormsJS file will be created automatically.

**Rebuilding the Web-Forms script after each compilation**

(`recreate_web_forms_script_after_recompile=false`)

If the `recreate_web_forms_script_after_recompile` option is enabled, the WebFormsJS file will be created after each compile.

**Determining the location of the server response tag**

(`web_forms_view_place=<body>`)

The `web_forms_view_place` option is for situations where the Action Control is not present in the response and determines the tag location of the server's response.

**Specify the default Controller**

(`use_default_controller=true`)

If this option is enabled, accessing the root path will cause the default Controller to run.

**Default Controller Name**

(`default_controller=DefaultController`)

This option specifies the name of the default Controller class.

**Activation of Sections in the default Controller class**

(`use_section_in_default_controller=true`)

If this option is enabled, there will be access to Sections in the Controller class.

> Note: If another controller matching the route is found, that controller will be executed and the default controller class will not be executed.

**Overriding the default Controller from route configuration**

(`set_break_for_default_controller=true`)

If this option is enabled, the route configuration will not add the default Controller to the list of Controllers, and only the root path request will execute the default Controller.

**Access Controller class in lower case**

(`access_controller_by_lower_case=true`)

If this setting is enabled, it will be possible to access Controller class names in lower case.

**Access the Controller class in lowercase only**

(`just_access_controller_by_lower_case=true`)

If this setting is enabled, Controller class names will only be accessible in lowercase letters.

**Ignoring prefix for Controller class**

(`ignore_prefix_controller=.`)

Specifying a string in this setting causes the name of this string to be ignored from accessing the Controller class if the name of this string is present in the prefix of the name of the Controller class.

Example:

If we specify the `Route` string in this setting, it is possible to access the `RouteContent` Controller class with the following path.

`example.com/Content`

**Ignoring suffix for Controller class**

(`ignore_suffix_controller=.`)

Specifying a string in this setting causes the name of this string to be ignored from accessing the Controller class if the name of this string is present in the suffix of the name of the Controller class.

Example:

If we specify the `Controller` string in this setting, it is possible to access the `ContentController` Controller class with the following path.

`example.com/Content`

**Ability to set two underlines to one dash dash to access the controller class**

(`put_two_underlines_equal_to_dash_for_controller=false`)

Activating this setting causes the access to the Controller class to be considered a dash when there are two consecutive underline in the name of the Controller classes.

Example:

If we have a controller class whose name is `active__page`, the route `example.com/active-page` will execute this controller (if you have configured Route).

**Ability to add default template**

(`set_default_pages=true`)

Enabling this setting causes the default CodeBehind template to be created automatically.
