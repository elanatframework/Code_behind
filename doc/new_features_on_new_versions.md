## New features on new versions

### Version 1.7
 - The possibility of creating a page view without having to follow the MVC pattern
   - Possibility to create only view without controller and model
   - Possibility to create model and view without controller

### Version 1.8
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

**Problems that were solved**
 - The problem of executing the path with extra characters after the slash character was solved.
 - Fixed the problem of replacing the class file with failed compilation in the last successful compilation.

### Version 1.8.1

**Problems that were solved**
 - Solving the problem of removing one or two characters after Razor syntax.

### Version 1.9
 - Ability to add layout page
   - Ability to send data from the current page to the layout page
   - The possibility of calling external pages from the view section
 - The possibility of preventing the direct execution of some pages (such as separate header and footer)
 - Ability to call aspx files in their own path, after rewriting
 - Improvements in the trim operation at the beginning of the aspx file

**Problems that were solved**
 - The problem of loading the constructor model without a controller was solved.
 - Fixed else detection problem for if in Razor syntax.

### Version 1.9.1

**Problems that were solved**
 - A mistake caused the arguments of the model constructor to be wrongly placed in the controller constructor; this problem has been fixed now.
 - In this version, if the CodeBehind framework is activated for the first time, it will no longer give the wwwroot directory missing error and a default welcome file will be placed in it.
 - The error that occurred when activating the set break for layout page (`set_break_for_layout_page=true`) option was resolved.
 - The problem of not automatically moving from the wwwroot path to the view path has been solved.
 - The problem of not applying, ignoring the default Default.aspx files to rewrite as a directory, was solved.

### Version 1.9.2
 - In this version and later, in the methods of the final view class, when creating a new instance of the controller class, the term controller is used instead of the term CurrentController.
 - In this version and later, the context inside the astx files are added at the beginning of the aspx file.

**Problems that were solved**
 - In the standard syntax, the problem of identifying template blocks that have the next line character or Tab character after the template name was solved.

### Version 1.9.3

**Problems that were solved**
 - Fixed the problem of naming templates with numbers in the standard syntax.
 - Solving the problem of not ignoring two consecutive at sign (@) in conditional blocks and loop blocks.

### Version 2.0
 - Ability to add data to ViewData in controller and model.
 - The addition of download API and the possibility of downloading files from executive pages in all three sections, view, model and controller.
 - The addition of a global template file to support all view pages.
 - The possibility of adding more templates, by separating the semicolon character (;).
 - New option to support cshtml files in the options file.
 - Added default pages (include layout) after first run.

### Version 2.1
 - Ability to change the view in the controller.
 - Ability to transfer template block data in ViewData.
 - Complete rewriting of codes related to new lines and backslash of executable files.
 - Complete rewriting of the codes related to creating files.
 - And a series of minor changes and improvements.

**Problems that were solved**
 - Deleting unused ex variable from the final view class.
