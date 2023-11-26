## Namespace and dll for CodeBehind view class

You can add namespaces to the view class aggregating aspx files. Add the namespaces to the namespace_import_list file at this path: `code_behind/namespace_import_list.ini`.

The default content of the namespace_import_list.ini file
```ini
[CodeBehind namespace import list]
namespace=System.IO
namespace=System.Collections
namespace=System.Collections.Generic
namespace=System.Linq
namespace=System.Threading
namespace=System.Threading.Tasks
```

You can also add necessary dlls to the view class that aggregates aspx files. This file is located in: `code_behind/dll_import_list.ini path`.

The default content of the dll_import_list.ini file
```ini
[CodeBehind dll import list]
dll_path={run_time_path}/System.IO.dll
dll_path={run_time_path}/System.Collections.dll
dll_path={run_time_path}/System.Linq.dll
dll_path={run_time_path}/System.Threading.dll
```
You can use the variables `{run_time_path}`, `{asp_run_time_path}` and `{base_directory_path}` to determine the path.

Note: The dll_import_list.ini file is only for the dlls that the view class aggregating aspx files need, and these dlls have nothing to do with the wwwroot/dll path.
