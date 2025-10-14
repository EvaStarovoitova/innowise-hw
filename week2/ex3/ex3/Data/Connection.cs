using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ex3.Data
{
    public class Connection : IConnection
    {
        private readonly string _connectionString;

        public  Connection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {

            var connection = new SqlConnection(_connectionString);

            connection.Open();

            return connection;
        }
    }
}
