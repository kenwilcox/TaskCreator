using System;
using System.CodeDom.Compiler;
using Microsoft.Win32.TaskScheduler;

namespace TaskCreator
{
    public class Maker
    {
        public static string Message { get; set; }
        public static bool Error { get; set; }

        public static void MakeLocalTaskFromSource(string stub)
        {
            Message = "";
            Error = false;

            var source = LocalTemplate.Replace("%STUB%", stub);
            var comp = new Compiler();
            var assembly = comp.Compile(source);
            if (assembly != null)
            {
                var script = (IMakerLocalTask)comp.FindInterface(assembly, "IMakerLocalTask");
                if (script != null)
                {
                    using (var ts = new TaskService())
                    {
                        try
                        {
                            var message = "";
                            script.CreateTask(ts, out message);
                            Message = message;
                        }
                        catch (Exception e)
                        {
                            Error = true;
                            Message = e.Message;
                        }
                    }
                }
                else
                {
                    Error = true;
                    Message = "Could not find an instance of IMakerLocalTaks";
                }

            }
            else
            {
                Error = true;

                foreach (CompilerError err in comp.Errors)
                {
                    // I'm subtracting 11 from the line number because of the code I add.
                    Message += String.Format("{0}: {1} | {2} | {3}:{4}", err.ErrorNumber, err.IsWarning ? "warning" : "error", err.ErrorText, err.Line - 12, err.Column) + Environment.NewLine;
                    //Message += err.ToString() + Environment.NewLine;
                }
            }
        }


        private const string LocalTemplate = @"
using System;
using Microsoft.Win32.TaskScheduler;
using Microsoft.Win32.TaskScheduler.Fluent;
using TaskCreator;

public class SimpleLocalTask : IMakerLocalTask
{

    public void CreateTask(TaskService taskService, out string message)
    {
        message = String.Empty;
        %STUB%
        if (message == String.Empty) message = ""Task Created Successfully"";
    }
}
";
    }
}
