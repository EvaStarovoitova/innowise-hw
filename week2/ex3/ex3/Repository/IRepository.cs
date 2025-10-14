using ex3.Models;

namespace ex3.Repository
{
    public interface IRepository
    {
        void AddTask(TaskItem task);
        List<TaskItem> GetAllTasks();
        void DeleteTask(int id);
        void UpdateStatusTask(int id, bool IsCompleted);

    }
}
