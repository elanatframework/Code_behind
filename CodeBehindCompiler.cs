using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;

namespace SetCodeBehind
{
    public static class CodeBehindCompiler
    {
        private static Assembly CompiledAssembly;
        
        public static Assembly CompileAspx()
        {
            if (CompiledAssembly != null)
            {
                return CompiledAssembly;
            }

            List<string> ErrorList = new List<string>();

            CodeBehindLibraryCreator la = new CodeBehindLibraryCreator();
            string code = la.GetCodeBehindViews();

            string assemblyName = "CodeBehindViews";

            string CurrentProjectName = Assembly.GetEntryAssembly().GetName().Name;
            string RunTimePath = Path.GetDirectoryName(typeof(object).GetTypeInfo().Assembly.Location);
            RunTimePath = RunTimePath.Replace("\\", "/");
            string SharedPath = RunTimePath.GetTextBeforeLastValue("/").GetTextBeforeLastValue("/");
            string Version = RunTimePath.GetTextAfterLastValue("/");
            string AspRunTimePath = SharedPath + "/Microsoft.AspNetCore.App/" + Version;


            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            MetadataReference[] references = new MetadataReference[]
            {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(AppContext.BaseDirectory + "/" + CurrentProjectName + ".dll"),
            MetadataReference.CreateFromFile(AppContext.BaseDirectory + "/CodeBehind.dll"),
            MetadataReference.CreateFromFile(AspRunTimePath + "/Microsoft.AspNetCore.Http.Abstractions.dll"),
            MetadataReference.CreateFromFile(RunTimePath + "/System.Runtime.dll")
            };

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
                    {
                        ErrorList.Add(diagnostic.ToString());
                    }

                    SaveError(ErrorList);
                    return null;
                }

                ms.Seek(0, SeekOrigin.Begin);

                byte[] bytes = ms.ToArray();
                CompiledAssembly = Assembly.Load(ms.ToArray());

                SaveError(ErrorList);
                return CompiledAssembly;
            }
        }

        private static void SaveError(List<string> ErrorList)
        {
            if (!Directory.Exists("code_behind"))
                Directory.CreateDirectory("code_behind");

            // Create views_compile_error.log File
            if (ErrorList.Count > 0)
            {
                string FileName = "code_behind/views_compile_error.log";

                using (StreamWriter writer = File.CreateText(FileName))
                {
                    writer.WriteLine("date_and_time:" + DateTime.Now.ToString());

                    foreach (string line in ErrorList)
                    {
                        writer.WriteLine(line);
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

            string FileName = "code_behind/views_class.cs.tmp";
            if (File.Exists(FileName))
                File.Delete(FileName);

            FileName = "code_behind/views_compile_error.log";
            if (File.Exists(FileName))
                File.Delete(FileName);

            FileName = "code_behind/views_class_aggregation_error.log";
            if (File.Exists(FileName))
                File.Delete(FileName);
        }
    }
}