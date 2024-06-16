## Manage roles in CodeBehind

To manage roles and determine access, it is necessary to configure the `Program.cs` class as follows:

Config CodeBehind role access in ASP.NET Core
```diff
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

app.UseSession();

SetCodeBehind.CodeBehindCompiler.Initialization();

+app.UseRollAccess(true);

app.UseCodeBehind(true);

app.Run();
```

To activate the CodeBehind roles, you must also activate the session service.

> Please note that the `UseRollAccess` middleware must be added before the `UseCodeBehind` or `UseCodeBehindRoute` middleware. Also, if you want the access of static files to be checked, you must add the `UseStaticFiles` middleware after the `UseRollAccess` middleware.

As you can see, `UseRollAccess` method is initialized with `true` input argument; this means that automatically if access to the path is not possible, the error page with code `403` will be displayed to the user.

## role.xml file

If you are using CodeBehind version 2.7 and later, if you start a new project or restart an existing project, a `role.xml` file will be created for you in the `code_behind` directory.

The contents of the default role.xml file are as follows:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<role_list>
  <role name="guest" active="false">
    <deny active="false" reason="only administrators have access to the admin path">
      <path match_type="start">/admin</path>
    </deny>
      <action type="static" name="write_html" value="true" active="false" reason="inability to write html tags" />
      <action type="session" name="maximum_login_try" value="10" active="false" reason="the maximum possible number of login attempts has been reached" />
    </role>
    <role name="admin" active="false"></role>
</role_list>
```

The role.xml file is the user role access configuration. In this file, you can create all kinds of roles and prevent roles from accessing different paths. This file is read only once in the first run of the program; therefore, the changes in this file during the execution of the program have no effect and the program needs to be restarted.

For better understanding, let's change this file a little and make it more concise.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<role_list>
  <role name="guest">
    <deny>
      <path match_type="start">/admin</path>
    </deny>
  </role>
</role_list>
```

According to the code above, the roles are added inside the `role_list` tag. To add a new role, we add a tag named `role` and put the name of the role in the `name` attribute. In the above codes, there is a role named `guest`. Inside the `role` tag is the `deny` tag and inside the `deny` tag is the `path` tag which has an attribute named `match_type` whose value is `start` and the content inside this tag is the `/admin` path. This means that the guest role does not have access to the path that starts with `/admin`.

## Path, Query, Form

You can define 3 tags inside the deny tag:

- path tag
- query tag
- form tag

Each of the above tags must have an attribute named match_type that has one of the following values:

- **start**: Matches when the requested path starts with the specified string
- **end**: Matches when the requested path ends with the specified string
- **exist**: Matches when the specified path exists, regardless of its position in the requested path
- **regex**: The regex match type is used to match the requested path using a regular expression pattern
- **full_match**: The regex match type is used to match the requested path using a regular expression pattern

Example:

Requested route: `example.com/admin/settings`

- **start**: `/admin` Matches because the requested path starts with "/admin"
- **end**: `/settings` Matches because the requested path ends with "/settings"
- **exist**: /admin Matches because "/admin" exists in the requested path
- **regex**: `/admin/[a-z]+` Matches because the requested path matches the regular expression pattern "/admin/[a-z]+"
- **full_match**: `/admin/settings` Matches because the requested path exactly matches "/admin/settings"

The path tag is for the requested path.

Example:

example.com/admin

The query tag is for querystring.

Example:

`example.com/?value=active`

The form tag is also for form data.

Example:

Form data is sent when the `post` method is used in the `form` tag in HTML.

```html
<form action="/" method="post">
  <label for="fname">First name:</label>
  <input type="text" id="fname" name="fname"><br><br>
  <label for="lname">Last name:</label>
  <input type="text" id="lname" name="lname"><br><br>
  <input type="submit" value="Submit">
</form>
```

The above form submit sends values ​​similar to the below in the form data:
`fname=Cristiano&lname=Ronaldo`


## Simultaneous use of path, query and form tags

The simultaneous use of each of these tags along with one or two other tags means that the request must meet all the conditions at the same time.

Example:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<role_list>
  <role name="guest">
    <deny>
      <path match_type="start">/admin</path>
      <query match_type="exist">value=active</path>
    </deny>
  </role>
</role_list>
```

In the example above, when the path `/admin` is requested and the `value=active` query is established, there is no access for the author role.

In the `role` tag, in addition to the `deny` tag, you can also use the `action` tag. This tag is in two types, `stati`c type and `session` type. You can change `session` actions based on user behavior. But `static` action tags are used for all users of the same role.

Note: unlike the `deny` tag, the `action` tag is not activated automatically and you must check its value for different actions in the program.

## Example of use for action of static type

**Prevent access to the stored procedure for a role**

In this example, two `action` tag of `static` type has been added in the role of `guest`, whose the name of one of them is `update_comment` and the other one is `add_content_image` and their values ​​are `stored_procedure`.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<role_list>
  <role name="guest">
  <action type="static" name="update_comment" value="deny_stored_procedure" />
  <action type="static" name="add_content_image" value="deny_stored_procedure" />
  </role>
</role_list>
```

The code below is an example of the database class, which has two methods, `GetProcedure` and `SetProcedure`. These two methods automatically check that the `action` is of the `static` type with the same name as the procedure, which has the value `deny_stored_procedure`; so that it does not execute the procedure if it exists.

DataBase class
```csharp
using CodeBehind;

public class DataBase
{
    private readonly ISession _Session;

    public DataBase(ISession Session)
    {
        _Session = Session;
    }

    public SqlDataReader GetProcedure(string ProcedureName, List<string> ParametersName = null, List<string> ParametersValue = null)
    {
        RoleAccess access = new RoleAccess(_Session);
        bool AccessToPocedure = (access.GetStaticAction(PocedureName) == "deny_stored_procedure");

        if (!AccessToPocedure)
            return null;

        SqlDataReader dr = dbc.GetProcedure(ProcedureName, ParametersName, ParametersValue);
        return dr;
    }

    public void SetProcedure(string ProcedureName, List<string> ParametersName = null, List<string> ParametersValue = null)
    {
        RoleAccess access = new RoleAccess(_Session);
        bool AccessToPocedure = (access.GetStaticAction(PocedureName) == "stored_procedure");

        if (!AccessToPocedure)
            return;

        dbc.SetProcedure(ProcedureName, ParametersName, ParametersValue);
    }
}
```

> Note: with some creativity, the above example can be changed so that the roles only execute `actions` with the `allow_stored_procedur`e value.

## Example of use for action of session type

**Comment sending limit for a role**

In this example, an `action` tag of `session` type has been added in the role of guest, whose name is `comment_send_limitation` and its value is `3`.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<role_list>
  <role name="guest">
    <action type="session" name="comment_send_limitation" value="3" reason="the maximum possible number of send comments has been reached" />
  </role>
</role_list>
```

In the Controller, we check the value of this `action`, if it is greater than 0, it sends the comment and then subtracts one from the value of the `action`. In this example, if the user posts a comment 3 times, the `action` value will be 0 and he will no longer be able to post more comments.

Controller
```csharp
using CodeBehind;

public partial class CommentController : CodeBehindController
{
    public void PageLoad(HttpContext context)
    {
        RoleAccess access = new RoleAccess(context.Session);
        int CommentSendLimitation = access.GetSessionAction("comment_send_limitation").ToNumber();

        if (CommentSendLimitation > 0)
        {
            // ...
            // Codes For Sending Comment
            // ...

            access.SetSessionAction("comment_send_limitation", CommentSendLimitation - 1);
        }
        else
            Write("you can no longer send comment");
    }
}
```

## Retrieving the values ​​of the role.xml file

The code below re-initializes the values ​​of the `role.xml` file in the program.

Example:
```csharp
new FillRoleList().Set();
```

Running the above code has no effect on `actions` by `session` type. Therefore, please be careful when using this code and consider security issues.

> Note: Calling the Set method from the `FillRoleList` class updates the changes in the `role.xml` file, but if you have used the `session` actions, the user is in sync with the `session` actions until the session is active. So it is better to restart the program for more security.

## New option

In version 2.7 of the CodeBehind framework, the default role option has been added to the `options` file, the default value of which is `guest`. By default, any user who enters your web application will have the role of guest.

Options file
```diff
[CodeBehind options]; do not change order
view_path=wwwroot
move_view_from_wwwroot=true
rewrite_aspx_file_to_directory=false
access_aspx_file_after_rewrite=false
ignore_default_after_rewrite=true
start_trim_in_aspx_file=true
inner_trim_in_aspx_file=true
end_trim_in_aspx_file=true
set_break_for_layout_page=true
convert_cshtml_to_aspx=false
show_minor_errors=false
error_page_path=/error.aspx/{value}
prevent_access_default_aspx=false
+default_role=guest
```

## Change role

You can change the user role in your application. Changing the user role is usually done when registering username and password information on the login page.

The following code changes the default role (`guest`) to the `admin` role:

Set new role
```csharp
new RoleAccess(context.Session).SetUserNewRole("admin");
```

To exit the user from the current role to the default role (`guest`), use the following code:

Exit role to default role
```csharp
new RoleAccess(context.Session).ExitRoleToDefault();
```
