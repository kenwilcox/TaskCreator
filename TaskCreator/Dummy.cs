//using System;
//using Microsoft.Win32.TaskScheduler;
//using Microsoft.Win32.TaskScheduler.Fluent;
//using TaskCreator;

//public class Dummy : IMakerLocalTask
//{

//    public void CreateTask(TaskService taskService, out string message)
//    {
//        message = String.Empty;

//        // Fluent Example
//        var taskName = @"MBSI\CompiledFluentTest";

//        taskService.Execute("notepad.exe")
//            .InWorkingDirectory(@"C:\temp\")
//            .WithArguments(@"C:\temp\KenTask.txt")
//            .OnAll(DaysOfTheWeek.Sunday | DaysOfTheWeek.Monday)
//            .In(WhichWeek.ThirdWeek)
//            .Of(MonthsOfTheYear.May | MonthsOfTheYear.June)
//            .Starting(DateTime.Now)
//            .Ending(DateTime.Now.AddYears(30))
//            .AsTask(taskName).Enabled = false;

//        if (message == String.Empty) message = "Task Created Successfully";

//    }
//}
