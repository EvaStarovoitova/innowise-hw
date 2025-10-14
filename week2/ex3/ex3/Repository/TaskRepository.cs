using System;
using Dapper;
using ex3.Data;
using ex3.Models;

namespace ex3.Repository
{
    public class TaskRepository : IRepository
    {
        private readonly IConnection _connectionFactory;

        public TaskRepository(IConnection connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public void AddTask(TaskItem task)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt) VALUES (@Title, @Description, @IsCompleted, @CreatedAt)";
            int rowAdd = connection.Execute(sql, task);

            if (rowAdd > 0)
                Console.WriteLine("Запись добавлена");
            else
                Console.WriteLine("Произошла ошибка при добавлении записи");           

        }

        public void UpdateStatusTask(int id, bool IsCompleted)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "UPDATE Tasks SET IsCompleted=@IsCompleted WHERE Id=@Id";
            int rowStatusUpdate = connection.Execute(sql, new { id, IsCompleted });

            if (rowStatusUpdate > 0)
                Console.WriteLine("Статус обновлен!");
            else
                Console.WriteLine("Задача не найдена!");
        }

        public  List<TaskItem> GetAllTasks()
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM Tasks ORDER BY CreatedAt DESC";
            return connection.Query<TaskItem>(sql).ToList();
        }

        public void DeleteTask(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "DELETE FROM Tasks WHERE Id=@Id";
            int rowDelete = connection.Execute(sql, new { id });

            if (rowDelete > 0)
                Console.WriteLine("Задача удалена!");
            else
                Console.WriteLine("Задача не найдена!");
        }

    }
}
