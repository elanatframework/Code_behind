## New features on new versions

**New releases policy:** Our effort at Elanat team is to add new features to new releases. We put a lot of effort into testing the CodeBehind framework before releasing new versions, but usually adding new features causes a series of new bugs. We receive bug reports very quickly and provide new sub-versions with bug fixes. So try to always use the latest versions of the CodeBehind framework.

### Early versions

The first version of CodeBehind is based on .NET Core version 7.0; if the version of .NET Core is updated in a version compared to the previous one, it will be explained in the description section of that new version.

### Version 1.7

**New features:**
 - The possibility of creating a page view without having to follow the MVC pattern
   - Possibility to create only view without controller and model
   - Possibility to create model and view without controller

### Version 1.8

**New features:**
 - Razor syntax support 
 - Template support
   - Added return template
   - External template
 - Added option
   - The option to specify the path of aspx files
   - possibility of rewriting the path of aspx files as a directory name
   - Ability to remove additional lines, tabs, and spaces
 - Namespace and dll for CodeBehind view class
 - Added HtmlData classes
 - Constructor method 

**This version guarantees 100% Code-Behind support**

**Problems that were solved:**
 - The problem of executing the path with extra characters after the slash character was solved.
 - Fixed the problem of replacing the class file with failed compilation in the last successful compilation.

### Version 1.8.1

**Problems that were solved:**
 - Solving the problem of removing one or two characters after Razor syntax.

### Version 1.9

**New features:**
 - Ability to add layout page
   - Ability to send data from the current page to the layout page
   - The possibility of calling external pages from the view section
 - The possibility of preventing the direct execution of some pages (such as separate header and footer)
 - Ability to call aspx files in their own path, after rewriting
 - Improvements in the trim operation at the beginning of the aspx file

**Problems that were solved:**
 - The problem of loading the constructor model without a controller was solved.
 - Fixed else detection problem for if in Razor syntax.

### Version 1.9.1

**Problems that were solved:**
 - A mistake caused the arguments of the model constructor to be wrongly placed in the controller constructor; this problem has been fixed now.
 - In this version, if the CodeBehind framework is activated for the first time, it will no longer give the wwwroot directory missing error and a default welcome file will be placed in it.
 - The error that occurred when activating the set break for layout page (`set_break_for_layout_page=true`) option was resolved.
 - The problem of not automatically moving from the wwwroot path to the view path has been solved.
 - The problem of not applying, ignoring the default Default.aspx files to rewrite as a directory, was solved.

### Version 1.9.2

**New features:**
 - In this version and later, in the methods of the final view class, when creating a new instance of the controller class, the term controller is used instead of the term CurrentController
 - In this version and later, the context inside the astx files are added at the beginning of the aspx file

**Problems that were solved:**
 - In the standard syntax, the problem of identifying template blocks that have the next line character or Tab character after the template name was solved.

### Version 1.9.3

**Problems that were solved:**
 - Fixed the problem of naming templates with numbers in the standard syntax.
 - Solving the problem of not ignoring two consecutive at sign (@) in conditional blocks and loop blocks.

### Version 2.0

**New features:**
 - Ability to add data to ViewData in controller and model
 - The addition of download API and the possibility of downloading files from executive pages in all three sections, view, model and controller
 - The addition of a global template file to support all view pages
 - The possibility of adding more templates, by separating the semicolon character (;)
 - New option to support cshtml files in the options file
 - Added default pages (include layout) after first run

### Version 2.1

**New features:**
 - Ability to change the view in the controller
 - Ability to transfer template block data in ViewData
 - Complete rewriting of codes related to new lines and backslash of executable files
 - Complete rewriting of the codes related to creating files
 - And a series of minor changes and improvements

**Problems that were solved:**
 - Deleting unused ex variable from the final view class.

### Version 2.1.1

**Problems that were solved:**
 - Resolving the problem of Razor syntax page attributes ending with the less-than (<) character.

### Version 2.1.2

**New features:**
 - Complete rewrite codes related of page attribute recognition in Razor syntax
 - Adding the view file path comment above their methods in the view class

### Version 2.2

**New features:**
 - Added CallerViewPath and CallerViewDirectoryPath to view, model and controller
 - New option to display minor errors in the options file
 - Improved debugging and improved `views_compile_error.log` error file
 - The possibility of creating a controller without requiring the existence of the PageLoad method
 - Added error page to default pages
 - Added the path of the error page in the options file
 - Added FoundPage attribute to detect page execution
 - Improved detection of closed brackets related to server codes, after apostrophes
 - Added PageLoad method to Controller abstract class
 - Added new feature section for better route control
 - The ability to create model, without the need to add an abstract
 - The ability to create a CodeBehindConstructor method without the need for input arguments
 - And a series of minor changes and improvements

### Version 2.3

**New features:**
 - Ability to specify View along with Model from all Controllers
 - - The possibility of loading pages with the model in the LoadPage method in View pages
 - Support strings written from the previous Controller
 - Added possible to prevent access to Default.aspx
 - - Added the prevent access to Default.aspx in the options file
 - Added StaticObject class
 - And a series of minor changes and improvements

**Problems that were solved:**
 - In cases where the current View is wrongly requested from the Controller, the loop is avoided.

### Version 2.4

**New features:**
 - New feature for route configuration
 - The possibility of running the controller with the text name of the controller
 - Applying multi-threaded processing to create the View class
 - Marking the View class for when new View pages are added
 - Ability to set page attributes with lowercase letters in standard syntax
 - Ability to add text tag with multiple lines in Razor syntax
 - And a series of minor changes and improvements

**In this version, it is possible to give preference to the controller**

**Problems that were solved:**
 - Fixed the problem of finding the `Microsoft.AspNetCore.App` directory for some operating systems.
 - Fixed problem matching projects whose name does not match for namespace name.

### Version 2.4.1

**Problems that were solved:**
 - Fixing the problem of calling the pages that were requested with the query string.

### Version 2.4.2

**Problems that were solved:**
 - Fixed the problem of not creating a query string after calling the pages in the `Run` method.

### Version 2.4.3

**New features:**
 - Adding Name and NameCollection classes in HtmlData namespase

**Problems that were solved:**
 - Avoiding adding the same query.

### Version 2.5

**New features:**
 - Adding middleware for easier configuration

### Version 2.5.1

**New features:**
 - Default.aspx not being added in Section when `prevent access default aspx` is enabled
 - And a series of minor changes and improvements

### Version 2.6

**New features:**
 - Support for constructor method of Controller class and Model class
 - Improved detection of View page attributes in standard syntax

### Version 2.7

**New features:**
 - Adding CodeBehind roles
 - - Adding role access middleware
 - - The possibility of preventing the access of the rolls to the routes
 - - Ability to define action and give action access for roles
