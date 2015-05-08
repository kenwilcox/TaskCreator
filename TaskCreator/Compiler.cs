using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;

namespace TaskCreator
{
    internal class Compiler
    {
        public ArrayList References { get; private set; }
        public CompilerErrorCollection Errors { get; private set; }

        public Compiler()
        {
            References = new ArrayList()
            {
                "System.dll",
                "TaskCreator.dll",
                "Microsoft.Win32.TaskScheduler.dll"
            };
        }

        public void AddReference(string reference)
        {
            References.Add(reference);
        }

        public void AddReferences(string[] references)
        {
            References.AddRange(references);
        }

        public Assembly Compile(string source)
        {
            CodeDomProvider provider = new CSharpCodeProvider();
            var type = Type.GetType("System.String");
            if (type != null)
            {
                var references = References.ToArray(type) as string[];
                var results = CompileScript(source, references, provider);
                Errors = results.Errors;
                return results.Errors.Count == 0 ? results.CompiledAssembly : null;
            }
            else
            {
                return null;
            }
        }

        private static CompilerResults CompileScript(string source, string[] references, CodeDomProvider provider)
        {
            // Configure parameters
            var parms = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                IncludeDebugInformation = false
            };
            
            parms.ReferencedAssemblies.AddRange(references);

            var results = provider.CompileAssemblyFromSource(parms, source);

            return results;
        }

        public object FindInterface(Assembly assembly, string interfaceName)
        {
            // Loop through types looking for one that implements the given interface
            return (from t in assembly.GetTypes() where t.GetInterface(interfaceName, true) != null select assembly.CreateInstance(t.FullName)).FirstOrDefault();
        }
    }
}
