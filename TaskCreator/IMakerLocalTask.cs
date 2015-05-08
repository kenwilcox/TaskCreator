using Microsoft.Win32.TaskScheduler;

namespace TaskCreator
{
    public interface IMakerLocalTask
    {
        void CreateTask(TaskService taskService, out string message);
    }
}
