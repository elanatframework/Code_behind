## CodeBehind framework data

CodeBehind framework creates a directory named code_behind in the project directory after the first execution.

There are the following files in the code_behind directory:
 - cache.xml
 - dll_import_list.ini
 - global_template.astx
 - namespace_import_list.ini
 - options.ini
 - role.xml
 - views_class.cs.tmp
 - views_class_last_success_compiled.cs.tmp
 - views_class_aggregation_error.log (maybe)
 - views_compile_error.log (maybe)

Apart from the list above, a file called `CodeBehindLastSuccessCompiled.dll.tmp` is added next to the `CodeBehind.dll` library and the main project dll.

### cache.xml

In this file, the cache is determined on the View pages and Controller classes.

### dll_import_list.ini

This file set dlls path in the view class aggregating aspx files.

### global_template.astx

This file is a global template that is applied to all aspx files.

### namespace_import_list.ini

This file adds namespaces to the view class aggregating aspx files.

### options.ini

This file will contain many options for customization.

### role.xml

In this file, user roles and their access are determined.

### views_class.cs.tmp

This file is the final class of views that is made from aspx files.

### views_class_last_success_compiled.cs.tmp

This file is a copy of the final view class that was compiled without problems.

### views_compile_error.log (maybe)

If the compiler gives an error while compiling the final view class, this file displays the errors; If the compilation is successful, this file will not be displayed.

### views_class_aggregation_error.log (maybe)

If there is a problem while collecting data from the views, this file will display the problems; Otherwise, this file will not be displayed.

### CodeBehindLastSuccessCompiled.dll.tmp

This file is a dll that stores the last successful compilation of the view class.
