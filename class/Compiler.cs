using CodeBehind;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.Runtime.Loader;

namespace SetCodeBehind
{
    public static class CodeBehindCompiler
    {
        private static Assembly CompiledAssembly;
        
        internal static Assembly CompileAspx(bool UseLastSuccessCompiled = false, List<string> CurrentErrorList = null)
        {
            if (CompiledAssembly != null)
            {
                return CompiledAssembly;
            }

            List<string> ErrorList = (CurrentErrorList != null)? CurrentErrorList : new List<string>();

            CodeBehindLibraryCreator la = new CodeBehindLibraryCreator();
            string code = (UseLastSuccessCompiled) ? la.GetLastSuccessCompiledViewClass() : la.GetCodeBehindViews();

            if (string.IsNullOrEmpty(code))
            {
                ErrorList.Add("Failed to load last successful compilation.");
                SaveError(ErrorList);

                return null;
            }

            CodeBehind.API.Path path = new CodeBehind.API.Path();

            const string assemblyName = "CodeBehindViews";
            string CurrentProjectName = Assembly.GetEntryAssembly().GetName().Name;


            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            List<MetadataReference> ReferencesList = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(path.BaseDirectory + "/" + CurrentProjectName + ".dll"), // When You Run Your Program Under CodeBehind Source, Remove This Line
                MetadataReference.CreateFromFile(path.BaseDirectory + "/CodeBehind.dll"),
                MetadataReference.CreateFromFile(path.AspRunTimePath + "/Microsoft.AspNetCore.Http.Abstractions.dll"),
                MetadataReference.CreateFromFile(path.AspRunTimePath + "/Microsoft.AspNetCore.Http.Features.dll"),
                MetadataReference.CreateFromFile(path.AspRunTimePath + "/Microsoft.Extensions.Primitives.dll"),
                MetadataReference.CreateFromFile(path.RunTimePath + "/System.Runtime.dll")
            };

            List<string> DllList = SetImportDllList();

            foreach(string dll in DllList)
                ReferencesList.Add(MetadataReference.CreateFromFile(dll));


            // Add All dll In bin Directory
            if (Directory.Exists(StaticObject.ViewPath + "/bin"))
            {
                List<string> BinFileList = new List<string>();
                DirectoryInfo BinDir = new DirectoryInfo(StaticObject.ViewPath + "/bin");

                foreach (FileInfo file in BinDir.GetFiles("*.dll"))
                {
                    try
                    {
                        File.Copy(file.FullName, AppContext.BaseDirectory + "/" + file.Name, true);
                    }
                    catch
                    {
                        ErrorList.Add("Failed to copy or over write assembly in bin/" + file.Name + " path.");
                    }

                    ReferencesList.Add(MetadataReference.CreateFromFile(AppContext.BaseDirectory + "/" + file.Name));
                    BinFileList.Add(file.Name);

                    try
                    {
                        AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + "/" + file.Name);
                    }
                    catch
                    {
                        ErrorList.Add("Failed to load the assembly in bin/" + file.Name + " path.");
                    }
                }

                foreach (DirectoryInfo dir in BinDir.GetDirectories("*" , SearchOption.AllDirectories))
                {
                    foreach (FileInfo file in dir.GetFiles("*"))
                    {
                        string DirectoryPath = file.DirectoryName.GetTextAfterValue(BinDir.FullName);

                        Directory.CreateDirectory(AppContext.BaseDirectory + "/" + DirectoryPath);

                        File.Copy(file.FullName, AppContext.BaseDirectory + "/" + DirectoryPath + "/" + file.Name);
                        ReferencesList.Add(MetadataReference.CreateFromFile(AppContext.BaseDirectory + "/" + DirectoryPath + "/" + file.Name));
                        BinFileList.Add(DirectoryPath + "/" + file.Name);

                        try
                        {
                            AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + "/" + DirectoryPath + "/" + file.Name);
                        }
                        catch
                        {
                            ErrorList.Add("Failed to load the assembly in bin/" + DirectoryPath + "/" + file.Name + " path.");
                        }
                    }
                }

                RemovingUnusedDll();
                SaveBinFileList(BinFileList);
            }

            MetadataReference[] references = ReferencesList.ToArray();


            Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList().ForEach(asm => references.Append(MetadataReference.CreateFromFile(Assembly.Load(asm).Location)));

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                CodeBehindOptions options = new CodeBehindOptions();

                if (!result.Success)
                {
                    foreach (var diagnostic in result.Diagnostics)
                        if (diagnostic.WarningLevel == 0)
                        {
                            if (UseLastSuccessCompiled)
                                ErrorList.Add("views_class_last_success_compiled.cs.tmp - " + diagnostic.ToString());
                            else
                                ErrorList.Add("views_class.cs.tmp - " + diagnostic.ToString());
                        }

                    if (options.ShowMinorErrors)
                        foreach (var diagnostic in result.Diagnostics)
                            if (diagnostic.WarningLevel > 0)
                            {
                                if (UseLastSuccessCompiled)
                                    ErrorList.Add("views_class_last_success_compiled.cs.tmp - " + diagnostic.ToString());
                                else
                                    ErrorList.Add("views_class.cs.tmp - " + diagnostic.ToString());
                            }

                    SaveError(ErrorList);

                    if (UseLastSuccessCompiled)
                        return null;
                    else
                        return CompileAspx(true, ErrorList); // Set Recursive
                }

                ms.Seek(0, SeekOrigin.Begin);

                byte[] bytes = ms.ToArray();
                CompiledAssembly = Assembly.Load(bytes);

                if (options.ShowMinorErrors)
                    foreach (var diagnostic in result.Diagnostics)
                        if (UseLastSuccessCompiled)
                            ErrorList.Add("views_class_last_success_compiled.cs.tmp - " + diagnostic.ToString());
                        else
                            ErrorList.Add("views_class.cs.tmp - " + diagnostic.ToString());

                if (UseLastSuccessCompiled)
                    ErrorList.Add("A problem occurred in the compilation and the last successful compilation was recompiled.");
                else
                {
                    CreateLastSuccessCompiledViewClass();
                    CreateLastSuccessCompiledAssemblyFile(bytes);
                }

                SaveError(ErrorList);

                return CompiledAssembly;
            }
        }

        private static void SaveError(List<string> ErrorList, bool UseLastLastSuccessCompiled = false)
        {
            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            // Create views_compile_error.log File
            if (ErrorList.Count > 0)
            {
                string FilePath = (UseLastLastSuccessCompiled) ? "code_behind/views_compile_error_last_success_compiled.log" : "code_behind/views_compile_error.log";

                var file = File.CreateText(FilePath);

                file.WriteLine("date_and_time:" + DateTime.Now.ToString());

                foreach (string line in ErrorList)
                {
                    file.WriteLine(line);
                }

                file.Dispose();
                file.Close();
            }
        }

        private static void SaveBinFileList(List<string> BinFileList)
        {
            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            // Create bin_file_list.ini File
            if (BinFileList.Count > 0)
            {
                const string FilePath = "code_behind/bin_file_list.ini";

                var file = File.CreateText(FilePath);

                file.WriteLine("date_and_time=" + DateTime.Now.ToString());

                foreach (string line in BinFileList)
                {
                    file.WriteLine("file=" + line);
                }

                file.Dispose();
                file.Close();
            }
        }

        private static void RemovingUnusedDll()
        {
            if (!Directory.Exists(StaticObject.ViewPath + "/bin"))
                return;

            const string FilePath = "code_behind/bin_file_list.ini";

            if (File.Exists(FilePath))
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Substring(0, 4) != "file")
                            continue;


                        string FileName = line.GetTextAfterValue("file=");

                        if (!File.Exists(StaticObject.ViewPath + "/bin/" + FileName))
                            if (File.Exists(AppContext.BaseDirectory + "/" + FileName))
                                File.Delete(AppContext.BaseDirectory + "/" + FileName);
                    }
                }
            }
        }

        public static void Initialization(bool BreakExist = false)
        {
            StaticObject.SetValue();

            if (BreakExist && (CompiledAssembly != null))
                return;

            if (BreakExist && (CompiledAssembly == null))
            {
                if (ExistLastSuccessCompiledAssemblyFile())
                {
                    LoadLastSuccessCompiledAssemblyFile();

                    return;
                }
            }
            
            CompiledAssembly = null;

            string FilePath = AppContext.BaseDirectory + "/CodeBehindLastSuccessCompiled.dll.tmp";
            if (File.Exists(FilePath))
                File.Delete(FilePath);

            if (!Directory.Exists("code_behind"))
            {
                Directory.CreateDirectory("code_behind");
                return;
            }

            FilePath = "code_behind/views_class.cs.tmp";
            if (File.Exists(FilePath))
                File.Delete(FilePath);

            FilePath = "code_behind/views_compile_error.log";
            if (File.Exists(FilePath))
                File.Delete(FilePath);

            FilePath = "code_behind/views_class_aggregation_error.log";
            if (File.Exists(FilePath))
                File.Delete(FilePath);

            FillRoles();

            FillCache();
        }

        public static void ReCompile()
        {
            Initialization();
            CompileAspx();
        }

        private static void CreateLastSuccessCompiledViewClass()
        {
            const string ViewClassFilePath = "code_behind/views_class.cs.tmp";
            const string ViewClassLastSuccessCompildeFilePath = "code_behind/views_class_last_success_compiled.cs.tmp";

            File.Copy(ViewClassFilePath, ViewClassLastSuccessCompildeFilePath, true);
        }

        // Compiled Assembly File
        private static void CreateLastSuccessCompiledAssemblyFile(byte[] AssemblyBytes)
        {
            string AssemblyFilePath = AppContext.BaseDirectory + "/CodeBehindLastSuccessCompiled.dll.tmp";

            File.WriteAllBytes(AssemblyFilePath, AssemblyBytes);
        }

        private static bool ExistLastSuccessCompiledAssemblyFile()
        {
            string AssemblyFilePath = AppContext.BaseDirectory + "/CodeBehindLastSuccessCompiled.dll.tmp";

            return File.Exists(AssemblyFilePath);
        }

        private static void LoadLastSuccessCompiledAssemblyFile()
        {
            string AssemblyFilePath = AppContext.BaseDirectory + "/CodeBehindLastSuccessCompiled.dll.tmp";


            List<MetadataReference> ReferencesList = new List<MetadataReference>();

            // Add All dll In bin Directory
            if (Directory.Exists(StaticObject.ViewPath + "/bin"))
            {
                List<string> BinFileList = new List<string>();
                DirectoryInfo BinDir = new DirectoryInfo(StaticObject.ViewPath + "/bin");

                foreach (FileInfo file in BinDir.GetFiles("*.dll"))
                {
                    if (!File.Exists(AppContext.BaseDirectory + "/" + file.Name))
                        continue;

                    ReferencesList.Add(MetadataReference.CreateFromFile(AppContext.BaseDirectory + "/" + file.Name));

                    AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + "/" + file.Name);
                }

                foreach (DirectoryInfo dir in BinDir.GetDirectories("*", SearchOption.AllDirectories))
                {
                    foreach (FileInfo file in dir.GetFiles("*"))
                    {
                        string DirectoryPath = file.DirectoryName.GetTextAfterValue(BinDir.FullName);

                        if (!Directory.Exists(AppContext.BaseDirectory + "/" + DirectoryPath))
                            break;

                        if (!File.Exists(AppContext.BaseDirectory + "/" + DirectoryPath + "/" + file.Name))
                            continue;

                        ReferencesList.Add(MetadataReference.CreateFromFile(AppContext.BaseDirectory + "/" + DirectoryPath + "/" + file.Name));

                        AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + "/" + DirectoryPath + "/" + file.Name);
                    }
                }
            }

            MetadataReference[] references = ReferencesList.ToArray();


            Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList().ForEach(asm => references.Append(MetadataReference.CreateFromFile(Assembly.Load(asm).Location)));


            CompiledAssembly = Assembly.Load(File.ReadAllBytes(AssemblyFilePath));
        }

        private static List<string> SetImportDllList()
        {
            const string DllImportListPath = "code_behind/dll_import_list.ini";
            List<string> ImportDllList = new List<string>();

            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            if (!File.Exists(DllImportListPath))
            {
                var file = File.CreateText(DllImportListPath);

                file.Write("[CodeBehind dll import list]" + Environment.NewLine);
                file.Write("dll_path={run_time_path}/System.IO.dll" + Environment.NewLine);
                file.Write("dll_path={run_time_path}/System.Collections.dll" + Environment.NewLine);
                file.Write("dll_path={run_time_path}/System.Linq.dll" + Environment.NewLine);
                file.Write("dll_path={run_time_path}/System.Threading.dll");

                file.Dispose();
                file.Close();
            }

            using (StreamReader reader = new StreamReader(DllImportListPath))
            {
                CodeBehind.API.Path path = new CodeBehind.API.Path();

                reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                    ImportDllList.Add(line.GetTextAfterValue("=").Replace("{run_time_path}", path.RunTimePath).Replace("{asp_run_time_path}", path.AspRunTimePath).Replace("{base_directory_path}", path.BaseDirectory));
            }

            return ImportDllList;
        }

        private static void FillRoles()
        {
            const string RolePath = "code_behind/role.xml";

            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            if (!File.Exists(RolePath))
            {
                var file = File.CreateText(RolePath);

                file.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine);
                file.Write("<role_list>" + Environment.NewLine);
                file.Write("    <role name=\"guest\" active=\"false\">" + Environment.NewLine);
                file.Write("        <deny active=\"false\" reason=\"only administrators have access to the admin path\">" + Environment.NewLine);
                file.Write("            <path match_type=\"start\">/admin</path>" + Environment.NewLine);
                file.Write("        </deny>" + Environment.NewLine);
                file.Write("        <action type=\"static\" name=\"write_html\" value=\"true\" active=\"false\" reason=\"inability to write html tags\" />" + Environment.NewLine);
                file.Write("        <action type=\"session\" name=\"maximum_login_try\" value=\"10\" active=\"false\" reason=\"the maximum possible number of login attempts has been reached\" />" + Environment.NewLine);
                file.Write("    </role>" + Environment.NewLine);
                file.Write("    <role name=\"admin\" active=\"false\"></role>" + Environment.NewLine);
                file.Write("</role_list>");

                file.Dispose();
                file.Close();

                return;
            }

            new FillRoleList().Set();
        }

        private static void FillCache()
        {
            const string CachePath = "code_behind/cache.xml";

            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            if (!File.Exists(CachePath))
            {
                var file = File.CreateText(CachePath);

                file.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine);
                file.Write("<cache_list>" + Environment.NewLine);
                file.Write("    <cache duration=\"60\" active=\"false\">" + Environment.NewLine);
                file.Write("        <controller>main</controller>" + Environment.NewLine);
                file.Write("        <view>/file_and_directory/EditFile.aspx</view>" + Environment.NewLine);
                file.Write("        <path match_type=\"start\">/page/book</path>" + Environment.NewLine);
                file.Write("        <query match_type=\"exist\">value=true</query>" + Environment.NewLine);
                file.Write("        <form match_type=\"exist\">hdn_HiddenValue=0</form>" + Environment.NewLine);
                file.Write("    </cache>" + Environment.NewLine);
                file.Write("</cache_list>");

                file.Dispose();
                file.Close();

                return;
            }

            new FillCacheList().Set();
        }
    }
}
