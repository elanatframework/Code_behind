![](https://github.com/elanatframework/Code_behind/assets/111444759/986799af-538a-4aca-b7fc-a5b8153c5a24)
# Code_behind
CodeBehind library is a backend framework. This library is a programming model based on the MVC structure, which provides the possibility of creating dynamic aspx files (similar to .NET Standard) in .NET Core and has high serverside independence.
CodeBehind framework supports standard syntax and Razor syntax. This framework guarantees the separation of server-side codes from the design part (html) and there is no need to write server-side codes in the view.

**The CodeBehind framework is faster than the default structure of cshtml pages in ASP.NET Core.**

**CodeBehind is .NET Diamond!**

In every scenario, CodeBehind performs better than the default structure in ASP.NET Core.

![ASP.NET Core VS CodeBehind table](https://github.com/elanatframework/Code_behind/assets/111444759/a93312da-65da-436d-85e3-b920872208d7)

Programming in CodeBehind is simple. The simplicity of the CodeBehind project is the result of two years of study and research on back-end frameworks and how they support web parts.

###  CodeBehind story 
First, CodeBehind was supposed to be a back-end framework for the C++ programming language; our project in C++ was going well, we built the listener structure and we were even able to implement fast-cgi in the coding phase for the Windows operating system. Windows operating system test with nginx web server was very stable and fast; but for some reason, we stopped working and implemented CodeBehind on .NET Core version 7.

### Documents

[Simple and structured MVC in CodeBehind](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/simple_and_structured_mvc_in_code_behind.md)

[It is not necessary to follow the MVC pattern](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/it_is_not_necessary_to_follow_the_mvc_pattern.md)

[Load aspx page finally result in another aspx page](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/load_aspx_page_finally_result_in_another_aspx_page.md)

[Examples of development](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/examples_of_development.md)

[Web part in CodeBehind](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/web_part_in_code_behind.md)

[How is the list of views finally made?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/how_is_the_list_of_views_finally_made.md)

[Performance test in only view section in version 1.5.2 (ASP.NET Core VS CodeBehind)](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/performance_test_in_only_view_section_version_1.5.2.md)

[ASP.NET Core VS CodeBehind; why should we use CodeBehind?](https://github.com/elanatframework/Code_behind/blob/elanat_framework/doc/asp_dot_net_core_vs_code_behind.md)

### CodeBehind training (On YouTube)

[Video 1- Hello World!](https://www.youtube.com/watch?v=lxQhDXJ0WcI)

[Video 2- Set dynamic header](https://www.youtube.com/watch?v=2kLgI0Uf8sU)

[Video 3 - Page list in default page](https://www.youtube.com/watch?v=tUujTKOHFq8)

### Download CodeBehind

We added CodeBehind in Nuget so that you can access it easily. You can use it in:

[https://www.nuget.org/packages/CodeBehind](https://www.nuget.org/packages/CodeBehind)

### Elanat was created using CodeBehind

CodeBehind is a stable and reliable framework; [Elanat](https://elanat.net) is the most powerful .NET system implemented using the CodeBehind framework.

[https://github.com/elanatframework/Elanat](https://github.com/elanatframework/Elanat)

![Elanat is based on CodeBehind](https://github.com/elanatframework/Code_behind/assets/111444759/ca6f8d80-65ae-4b4c-b2e2-c8d4b1270b46)

 ### CodeBehind advantages

CodeBehind is a flexible framework. CodeBehind inherits all the advantages of ASP.NET Core and gives it more simplicity, power and flexibility.

CodeBehind, like the default ASP.NET Core, supports multiple platforms, and in the test conducted by the Elanat team, it also has high stability on Linux.

CodeBehind occupies less memory resources (ram) than ASP.NET Core.

aspx pages are compiled in CodeBehind and their calling is done at a very high speed, so that the path of the aspx file is not even referred to during the calling.

![aspx file in ASP.NET Core](https://github.com/elanatframework/Code_behind/assets/111444759/323e70e8-b90b-4ed1-a7f4-67c4814d7a3b)

One of the great features that CodeBehind gives you is the support for DLL libraries. You can add all the .NET Core DLL libraries that you have created into the bin directory located in wwwroot so that the CodeBehind will call all of them.

![A project created under CodeBehind](https://github.com/elanatframework/Code_behind/assets/111444759/eac0e767-993e-4e46-a811-1a0702dbe94d)

How to add web part?
First, copy your compiled project files to the desired path in wwwroot; then copy the main dll file to wwwroot/bin path. You can do the copy while the process is running in the method and then call the code below to compile without restarting the program.

```csharp
// Recompile
CodeBehindCompiler.ReCompile();
```

### Error detection

After running the project, CodeBehind will create a directory called `code_behind` next to the `wwwroot` directory. In this directory, the view class, which is made of aspx files, is kept. If there is any error in the aspx files, it will also be displayed in the `views_compile_error.log` file.
