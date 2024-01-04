### Error detection

After running the project, CodeBehind will create a directory called `code_behind` next to the `wwwroot` directory. In this directory, the view class, which is made of aspx files, is kept. If there is any compile error in the aspx files, it will be displayed in the `views_compile_error.log` file.

If there is an error related to the structure of the aspx files or their page attributes, a file named views_class_aggregation_error.log will be displayed in the wwwroot folder and it will display the errors.
