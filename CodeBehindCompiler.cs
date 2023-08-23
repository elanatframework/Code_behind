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
        
        public static Assembly CompileAspx(bool UseLastLastSuccessCompiled = false)
        {
            if (CompiledAssembly != null)
            {
                return CompiledAssembly;
            }

            List<string> ErrorList = new List<string>();

            CodeBehindLibraryCreator la = new CodeBehindLibraryCreator();
            string code = (UseLastLastSuccessCompiled) ? la.GetLastSuccessCompiledViewClass() : la.GetCodeBehindViews();

            if (string.IsNullOrEmpty(code))
            {
                ErrorList.Add("Failed to load last successful compilation.");
                SaveError(ErrorList);

                return null;
            }

            string assemblyName = "CodeBehindViews";

            string CurrentProjectName = Assembly.GetEntryAssembly().GetName().Name;
            string RunTimePath = Path.GetDirectoryName(typeof(object).GetTypeInfo().Assembly.Location);
            RunTimePath = RunTimePath.Replace("\\", "/");
            string SharedPath = RunTimePath.GetTextBeforeLastValue("/").GetTextBeforeLastValue("/");
            string Version = RunTimePath.GetTextAfterLastValue("/");
            string AspRunTimePath = SharedPath + "/Microsoft.AspNetCore.App/" + Version;


            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            List<MetadataReference> ReferencesList = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(AppContext.BaseDirectory + "/" + CurrentProjectName + ".dll"),
                MetadataReference.CreateFromFile(AppContext.BaseDirectory + "/CodeBehind.dll"),
                MetadataReference.CreateFromFile(AspRunTimePath + "/Microsoft.AspNetCore.Http.Abstractions.dll"),
                MetadataReference.CreateFromFile(RunTimePath + "/System.Runtime.dll")
            };


            // Add All dll In bin Directory
            if (Directory.Exists("wwwroot/bin"))
            {

                List<string> BinFileList = new List<string>();
                DirectoryInfo BinDir = new DirectoryInfo("wwwroot/bin");

                foreach (FileInfo file in BinDir.GetFiles("*.dll"))
                {
                    File.Copy(file.FullName, AppContext.BaseDirectory + "/" + file.Name, true);
                    ReferencesList.Add(MetadataReference.CreateFromFile(AppContext.BaseDirectory + "/" + file.Name));
                    BinFileList.Add(file.Name);

                    try
                    {
                        AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + "/" + file.Name);
                    }
                    catch (Exception ex) 
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
                        catch (Exception ex)
                        {
                            ErrorList.Add("Failed to load the assembly in bin/" + DirectoryPath + "/" + file.Name + " path.");
                        }
                    }
                }

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

                if (!result.Success)
                {
                    foreach (var diagnostic in result.Diagnostics)
                        ErrorList.Add(diagnostic.ToString());

                    SaveError(ErrorList);

                    if (UseLastLastSuccessCompiled)
                        return null;
                    else
                        return CompileAspx(true); // Set Recursive
                }

                ms.Seek(0, SeekOrigin.Begin);

                byte[] bytes = ms.ToArray();
                CompiledAssembly = Assembly.Load(ms.ToArray());

                if (UseLastLastSuccessCompiled)
                    ErrorList.Add("A problem occurred in the compilation and the last successful compilation was recompiled.");

                SaveError(ErrorList);
                CreateLastSuccessCompiledViewClass();

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

                using (StreamWriter writer = File.CreateText(FilePath))
                {
                    writer.WriteLine("date_and_time:" + DateTime.Now.ToString());

                    foreach (string line in ErrorList)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }

        private static void SaveBinFileList(List<string> BinFileList)
        {
            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            // Create bin_file_list.ini File
            if (BinFileList.Count > 0)
            {
                string FilePath = "code_behind/bin_file_list.ini";

                using (StreamWriter writer = File.CreateText(FilePath))
                {
                    writer.WriteLine("date_and_time=" + DateTime.Now.ToString());

                    foreach (string line in BinFileList)
                    {
                        writer.WriteLine("file=" + line);
                    }
                }
            }
        }

        public static void Initialization()
        {
            CompiledAssembly = null;

            if (!Directory.Exists("code_behind"))
            {
                Directory.CreateDirectory("code_behind");
                return;
            }

            string FilePath = "code_behind/views_class.cs.tmp";
            if (File.Exists(FilePath))
                File.Delete(FilePath);

            FilePath = "code_behind/views_compile_error.log";
            if (File.Exists(FilePath))
                File.Delete(FilePath);

            FilePath = "code_behind/views_class_aggregation_error.log";
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }

        public static void CreateLastSuccessCompiledViewClass()
        {
            const string ViewClassFilePath = "code_behind/views_class.cs.tmp";
            const string ViewClassLastSuccessCompildeFilePath = "code_behind/views_class_last_success_compiled.cs.tmp";

            File.Copy(ViewClassFilePath, ViewClassLastSuccessCompildeFilePath, true);
        }
    }
}
