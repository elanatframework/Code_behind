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

### Version 1.9
 - Ability to add layout page
   - Ability to send data from the current page to the layout page
   - The possibility of calling external pages from the view section
 - The possibility of preventing the direct execution of some pages (such as separate header and footer)
