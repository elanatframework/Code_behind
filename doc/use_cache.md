## Use cache

### What is the definition of cache?
In software development, a cache is a hardware or software component that stores data so that future requests for that data can be served faster. Caching is used to reduce load times and improve performance by storing frequently accessed or recently used data in a more easily accessible location. This can help reduce the need to repeatedly access slower storage mediums, like databases, and can improve overall system efficiency. Cache prevents heavy reprocessing, so a system that uses a cache only performs complex requests once and saves the processing performed for subsequent requests.

### Enable cache service in ASP.NET Core

To enable the cache in CodeBehind, you need to enable the cache service in ASP.NET Core in the `Program.cs` class.

Enable memory cache in ASP.NET Core
```diff
var builder = WebApplication.CreateBuilder(args);

+builder.Services.AddMemoryCache();

var app = builder.Build();

SetCodeBehind.CodeBehindCompiler.Initialization();

app.UseCodeBehind();

app.Run();
```

> Note: The cache memory service is related to ASP.NET Core and the cache mechanism in the CodeBehind framework is based on the cache memory service.

### cache.xml file

If you are using CodeBehind version 2.8 and later, if you start a new project or restart an existing project, a `cache.xml` file will be created for you in the `code_behind` directory.

The contents of the default cache.xml file are as follows:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<cache_list>
    <cache duration="60" active="false">
        <controller>main</controller>
        <view>/file_and_directory/EditFile.aspx</view>
        <path match_type="start">/page/book</path>
        <query match_type="exist">value=true</query>
        <form match_type="exist">hdn_HiddenValue=0</form>
    </cache>
</cache_list>
```

The cache.xml file is the cache configuration of CodeBehind pages and controllers. In this file, you can cache the pages and controllers you want for as long as you want. This file is read only once in the first run of the program; therefore, the changes in this file during the execution of the program have no effect and the program needs to be restarted.

For better understanding, let's change this file a little and make it more concise.
```xml
<?xml version="1.0" encoding="utf-8" ?>
<cache_list>
    <cache duration="60">
        <controller>SeriesController</controller>
    </cache>
    <cache duration="3">
        <view>/main.aspx</view>
        <query match_type="full_match">?value=true</query>
    </cache>
</cache_list>
```

According to the code above, the caches are added inside the `cache_list` tag. To add a new cache, we add a tag named `cache` and put the `duration` of the cache (in seconds) in the `duration` attribute. In the above code, there is a tag named `controller` inside the first cache tag, and the text inside it is the name of `SeriesController`; the cache tag caches the first Controller named `SeriesController` for `60` seconds. The second cache tag works for `3` seconds and there are two tags inside it. A `view` name tag whose text inside is the value of `/main.aspx`. A tag with the name `query` is also inside the cache tag, which has the `match_type` attribute, the value of which is `full_match`, and the text inside it is `?value=true`; the second cache tag only works if the View is requested with the `/main.aspx` path and the query string only has the value `?value=true`.

Internal tags in each tag cache are actually filters; that is, the cache is activated only when the request is equal to all these filters.

Please note that the cache on the controller is done only when you have configured the controller in the route; otherwise, in the default MVC architecture of the CodeBehind framework, the View section is preferred over the Controller, and the cache will be effective on the View path.

Configuration of the controller in the route is done by calling the `UseCodeBehindRoute` middleware.
`app.UseCodeBehindRoute();`

Configuring the default MVC architecture of the CodeBehind framework is also done by calling the `UseCodeBehind` middleware.
`app.UseCodeBehind();`

### Path, Query, Form

You can define 3 tags inside the cache tag:

- path tag
- query tag
- form tag

Each of the above tags must have an attribute named `match_type` that has one of the following values:

- **start**: Matches when the requested path starts with the specified string
- **end**: Matches when the requested path ends with the specified string
- **exist**: Matches when the specified path exists, regardless of its position in the requested path
- **regex**: The regex match type is used to match the requested path using a regular expression pattern
- **full_match**: The regex match type is used to match the requested path using a regular expression pattern

Example:

Requested route: `example.com/page/book`

- **start**: `/page` Matches because the requested path starts with "/page"
- **end**: `/book` Matches because the requested path ends with "/book"
- **exist**: /page Matches because "/page" exists in the requested path
- **regex**: `/page/[a-z]+` Matches because the requested path matches the regular expression pattern "/page/[a-z]+"
- **full_match**: `/page/book` Matches because the requested path exactly matches "/page/book"

The path tag is for the requested path.

Example:

`example.com/admin`

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

### Simultaneous use of path, query and form tags

The simultaneous use of each of these tags along with one or two other tags means that the request must meet all the conditions at the same time.

Example:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<cache_list>
    <cache duration="60">
        <view>/series_page/main.aspx</view>
        <query match_type="exist">value=true</query>
        <form match_type="exist">hdn_HiddenValue=0</form>
    </cache>
</cache_list>
```

In the above example, the cache is applied only if the View is requested with the path `/series_page/main.aspx`; and the query `value=true` exists in the query string; and also the data value of the form `hdn_HiddenValue=0` should also exist in the user's request.

### Example of cache in CodeBehind Framework

First, we replace the following contents in the `cache.xml` file (located in the `code_behind` directory). According to the previous explanation, we cache a file named `random.aspx` in `the wwwroot` directory for `10` seconds; caching is done only in the condition that the query string matches the value `?value=true`.

cache.xml file
```xml
<?xml version="1.0" encoding="utf-8" ?>
<cache_list>
    <cache duration="10">
        <view>/random.aspx</view>
        <query match_type="full_match">?value=true</query>
    </cache>
</cache_list>
```

We add the `random.aspx` file in the `wwwroot` directory and place the following codes in it.

View (random.aspx)
```html
@page
@{ int RandomValue = new Random().Next(1000000); }
<b>@RandomValue</b>
```

After running the project, if you request the following path, the random response will remain constant for 10 seconds.
`example.com/random.aspx?value=true`

Consequently, if you request the following paths, the cache will not be activated:

- `example.com/random.aspx`
- `example.com/random.aspx?value=true2`
- `example.com/random.aspx?value=true&query2=value2`
