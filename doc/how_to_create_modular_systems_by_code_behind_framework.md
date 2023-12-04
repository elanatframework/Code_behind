## How to create modular systems by CodeBehind framework?

The project created by using CodeBehind is automatically a modular project, that is, it has the ability to add web parts. In addition, each web part can be used in other projects.

You can add a page to insert a module (web part) on the admin page of your project; this page should include an input for uploading, and after copying, run the CodeBehind compilation methods again; according to the following codes:

```csharp
CodeBehindCompiler.ReCompile();
```
