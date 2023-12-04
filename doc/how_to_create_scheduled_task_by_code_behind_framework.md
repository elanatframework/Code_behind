## How to create scheduled task system by CodeBehind framework?

 First, create an xml file (or json or ini or etc) similar to the following file:

```xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<scheduled_tasks_root>
  <scheduled_tasks_list>
    <scheduled_task name="empty_tmp_directory" path="/action/system_access/scheduled_tasks/empty_tmp_directory/Default.aspx" active="true" corn_hour="86400" type="load" check_type="page" last_run="20230830010903" />
    <scheduled_task name="empty_session_data_directory" path="/action/system_access/scheduled_tasks/empty_session_data_directory/Default.aspx" active="true" corn_hour="86400" type="load" check_type="page" last_run="20230830010903" />
    <scheduled_task name="active_delay_content" path="/action/system_access/scheduled_tasks/active_delay_content/Default.aspx" active="true" corn_hour="600" type="load" check_type="page" last_run="20230830010903" />
    <scheduled_task name="delete_robot_blocked_ip" path="/action/system_access/scheduled_tasks/delete_robot_blocked_ip/Default.aspx" active="true" corn_hour="600" type="load" check_type="page" last_run="20230830010903" />
  </scheduled_tasks_list>
</scheduled_tasks_root>
```

Scheduled task is one of the most important parts of a high-level project; you can run the Scheduled task method in the Run method in the builder located in the Program.cs class.

```diff
app.Run(async context =>
{
+  ScheduledTask.Run(context);

   CodeBehindExecute execute = new CodeBehindExecute();
   await context.Response.WriteAsync(execute.Run(context));
   await context.Response.CompleteAsync();
});
```

You can perform the scheduled task either by request or by calling a timer (of course, if you don't allow the system to sleep!).

**Note:** You can implement both of them in a combination.
