## How to create startup system by CodeBehind framework?

 First, create an xml file (or json or ini or etc) similar to the following file:
 ```xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<start_up_root>
  <start_up_list>
    <start_up name="start_timer" path="/action/system_access/start_up/start_timer/Default.aspx" active="true" />
    <start_up name="send_start_project_email_to_provider" path="/action/system_access/start_up/send_start_project_email_to_provider/Default.aspx" active="true" />
  </start_up_list>
</start_up_root>
```

Startup is a structure that allows you to initialize some things before the program is activated; you can run the Startup method before the Run method in the builder located in the Program.cs class.
```diff
+  Startup.Run();

app.Run(async context =>
{
   CodeBehindExecute execute = new CodeBehindExecute();
   await context.Response.WriteAsync(execute.Run(context));
   await context.Response.CompleteAsync();
});
```

Note: In CodeBehind framework version 1.5.1 (and later versions) you can call Run method in CodeBehindExecute without needing HttpContext.

```csharp
CodeBehindExecute execute = new CodeBehindExecute();
execute.Run(StartupNodePath);
```

In the code above, StartupNodePath can be a path like below:

`/action/system_access/start_up/send_start_project_email_to_provider/Default.aspx`
