using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace RAMDHD.Database
{
    public class DatabaseConnection
    {
        private readonly string _dbPath;

        public DatabaseConnection(string dbPath)
        {
            _dbPath = dbPath;
        }

        public SqliteConnection GetDbConnection()
        {
            return new SqliteConnection($"Filename={_dbPath}");
        }

        public void InitializeDatabase()
        {
            using var connection = GetDbConnection();
            connection.Open();

            //try
            //{
            //    // Attempt to drop the table if it exists.
            //    var dropTableCommandText = "DROP TABLE GraphTask";
            //    var dropTableCommand = new SqliteCommand(dropTableCommandText, connection);
            //    dropTableCommand.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    // If an error occurs, it might be because the table does not exist.
            //    // Handle the exception as needed.
            //    Console.WriteLine("An error occurred: " + ex.Message);
            //}

            var tableCommand = @"
            CREATE TABLE IF NOT EXISTS GraphTask (
                Id INTEGER PRIMARY KEY,
                Title TEXT NOT NULL,
                Activity1 TEXT NOT NULL,
                Activity2 TEXT NOT NULL,
                Activity3 TEXT NOT NULL,
                Activity4 TEXT NOT NULL,
                Procrastination INTEGER NOT NULL
            )";

            var createTable = new SqliteCommand(tableCommand, connection);
            createTable.ExecuteNonQuery();
        }
    }
}
