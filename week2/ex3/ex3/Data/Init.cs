using Dapper;
using System.Data;

namespace ex3.Data
{
    public interface IInitDataBase
    {
        void InitializeDatabase();
    }

    public class DatabaseInit : IInitDataBase
    {
        private readonly IConnection _connectionFactory;

        public DatabaseInit(IConnection connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void InitializeDatabase()
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();

                string sql = @"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Tasks') AND type in (N'U'))
                    BEGIN
                        CREATE TABLE Tasks (
                            Id INT PRIMARY KEY IDENTITY(1,1),
                            Title NVARCHAR(100) NOT NULL,
                            Description NVARCHAR(500),
                            IsCompleted BIT DEFAULT 0,
                            CreatedAt DATETIME2 DEFAULT GETDATE()
                        )
                    END";

                connection.Execute(sql);
                Console.WriteLine("База данных инициализирована успешно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации базы данных: {ex.Message}");
            }
        }
    }
}