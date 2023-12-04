## How to create dynamic middleware system by CodeBehind framework?

### Dynamic middleware after request and before response

First, create an xml file (or json or ini or etc) similar to the following file:
```xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<before_load_path_reference_root>

  <before_load_path_reference_list>

    <reference type="url" check_type="page" exist="true" start_by="false" end_by="false" regex_match="false" active="false" reason="login_try_count_limitation_is_active">
      <path_value>btn_Login=</path_value>
      <load_value><![CDATA[/action/system_access/reference/check_login_try_count_limitation/Default.aspx]]></load_value>
    </reference>

    <reference type="form" check_type="page" exist="true" start_by="false" end_by="false" regex_match="false" active="false" reason="login_try_count_limitation_is_active">
      <path_value>btn_Login=</path_value>
      <load_value><![CDATA[/action/system_access/reference/check_login_try_count_limitation/Default.aspx]]></load_value>
    </reference>

    <reference type="url" check_type="page" exist="true" start_by="false" end_by="false" regex_match="false" active="true" reason="lock_login_is_active">
      <path_value>btn_Login=</path_value>
      <load_value><![CDATA[/action/system_access/reference/check_lock_login/Default.aspx]]></load_value>
    </reference>

    <reference type="form" check_type="page" exist="true" start_by="false" end_by="false" regex_match="false" active="true" reason="lock_login_is_active">
      <path_value>btn_Login=</path_value>
      <load_value><![CDATA[/action/system_access/reference/check_lock_login/Default.aspx]]></load_value>
    </reference>

    <reference type="url" check_type="page" exist="true" start_by="false" end_by="false" regex_match="false" active="true" reason="next_search_time_interval_limitation_is_active">
      <path_value>btn_Search=</path_value>
      <load_value><![CDATA[/action/system_access/reference/check_next_search_time_interval_limitation/Default.aspx]]></load_value>
    </reference>

    <reference type="form" check_type="page" exist="true" start_by="false" end_by="false" regex_match="false" active="true" reason="next_search_time_interval_limitation_is_active">
      <path_value>btn_Search=</path_value>
      <load_value><![CDATA[/action/system_access/reference/check_next_search_time_interval_limitation/Default.aspx]]></load_value>
    </reference>

  </before_load_path_reference_list>

</before_load_path_reference_root>
```

You can control the url paths before execution. The above example shows that you can execute aspx pages before executing the route and deny access to the routes or change or add to the output hypertext. The first reference in the xml file above shows that there is a restriction to enter the login page; every time the user clicks on the login button, the aspx page is executed and the number of login attempts is reduced by one from the session; if this value becomes 0, it will not allow entry.

you can run the BeforeLoadPathReference method in the Run method in the builder (before execute page) located in the Program.cs class.

```diff
app.Run(async context =>
{
+  BeforeLoadPathReference.Run(context);

   CodeBehindExecute execute = new CodeBehindExecute();
   await context.Response.WriteAsync(execute.Run(context));
   await context.Response.CompleteAsync();
});
```

### Dynamic middleware after response

 First, create an xml file (or json or ini or etc) similar to the following file:
 ```xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<after_load_path_reference_root>

  <after_load_path_reference_list>

    <reference type="url" check_type="page" exist="false" start_by="true" end_by="false" regex_match="false" active="true">
      <path_value>/upload/attachment/</path_value>
      <load_value><![CDATA[/action/system_access/reference/increase_attachment_visit/Default.aspx]]></load_value>
    </reference>

  </after_load_path_reference_list>

</after_load_path_reference_root>
```

You also have the possibility to control the url paths after execution. The example above shows that you can run aspx pages after running the route. In the xml file above, you can see that after the requests, an aspx page is executed in the upload/attachment path, and if the path is an existing file in the path and database, the download value of the attachment file in the database is added by one number.

you can run the AfterLoadPathReference method in the Run method in the builder (before execute page) located in the Program.cs class.

```diff
app.Run(async context =>
{
   CodeBehindExecute execute = new CodeBehindExecute();
   await context.Response.WriteAsync(execute.Run(context));

+  AfterLoadPathReference.Run(context);

   await context.Response.CompleteAsync();
});
```
