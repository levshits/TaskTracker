using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.DAL
{
    public interface ITaskAccessComponent
    {
        IEnumerable<Task> GetTasks();
        Task GetTask(int id);
        void AddTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(int id);
    }
}
