using ex3.Data;
using ex3.Models;
using ex3.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ex3
{
    class Program
    {
        private static ServiceProvider _serviceProvider;
        private static IRepository _taskRepository;

        static void Main(string[] args)
        {
          
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();


            var dbInit = _serviceProvider.GetService<IInitDataBase>();
            dbInit.InitializeDatabase();

            _taskRepository = _serviceProvider.GetService<IRepository>();
          
            ShowMenu();

            while (true)
            {
                Console.Write("\nВыберите действие: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ShowAllTasks();
                        break;
                    case "3":
                        UpdateTaskStatus();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "5":                       
                        return;
                    default:                       
                        ShowMenu();
                        break;
                }
            }
        }

        static void ConfigureServices(IServiceCollection services)
        {
            var connectionString = @"Server=Eva\PRIMARYEVA;Database=master;Trusted_Connection=true;TrustServerCertificate=true;";

            services.AddSingleton<IConnection>(new Connection(connectionString));
            services.AddTransient<IRepository, TaskRepository>();
            services.AddTransient<IInitDataBase, DatabaseInit>();
        }

        static void ShowMenu()
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Просмотреть все задачи");
            Console.WriteLine("3. Обновить статус задачи");
            Console.WriteLine("4. Удалить задачу");
            Console.WriteLine("5. Выход");
        }

        static void AddTask()
        {
            Console.Write("Введите название задачи: ");
            var title = Console.ReadLine();

            Console.Write("Введите описание задачи: ");
            var description = Console.ReadLine();

            var task = new TaskItem
            {
                Title = title,
                Description = description,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            _taskRepository.AddTask(task);
        }

        static void ShowAllTasks()
        {
            var tasks = _taskRepository.GetAllTasks();

            if (!tasks.Any())
            {
                Console.WriteLine("Список задач пуст!");
                return;
            }

            Console.WriteLine($"\nВсе задачи ({tasks.Count}):");
            Console.WriteLine(new string('-', 60));

            foreach (var task in tasks)
            {
                var status = task.IsCompleted ? "Выполнена" : "Не выполнена";
                Console.WriteLine($"ID: {task.Id}");
                Console.WriteLine($"Название: {task.Title}");
                Console.WriteLine($"Описание: {task.Description}");
                Console.WriteLine($"Статус: {status}");
                Console.WriteLine($"Создана: {task.CreatedAt:dd.MM.yyyy HH:mm}");
                Console.WriteLine(new string('-', 60));
            }
        }

        static void UpdateTaskStatus()
        {
            Console.Write("Введите ID задачи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID!");
                return;
            }

            Console.Write("Задача выполнена? (y/n): ");
            var completed = Console.ReadLine().ToLower() == "y";

            _taskRepository.UpdateStatusTask(id, completed);
        }

        static void DeleteTask()
        {
            Console.Write("Введите ID задачи для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID!");
                return;
            }

            _taskRepository.DeleteTask(id);
        }
    }
}