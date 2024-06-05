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

            CreateGraphTaskTable(connection); // Create GraphTask table
            CreateNotesTable(connection); // Create Note table
            CreateRoutineTable(connection); // Create Routine table
        }

        private void CreateGraphTaskTable(SqliteConnection connection)
        {
            var tableCommandText = @"
                CREATE TABLE IF NOT EXISTS GraphTask (
                    Id INTEGER PRIMARY KEY,
                    Title TEXT NULLABLE,
                    Activity1 TEXT NULLABLE,
                    Activity2 TEXT NULLABLE,
                    Activity3 TEXT NULLABLE,
                    Activity4 TEXT NULLABLE,
                    Procrastination INTEGER NULLABLE
                )";
            var createTableCommand = new SqliteCommand(tableCommandText, connection);
            createTableCommand.ExecuteNonQuery();
        }

        private void CreateNotesTable(SqliteConnection connection)
        {
            var tableCommandText = @"
                CREATE TABLE IF NOT EXISTS Note (
                    Id INTEGER PRIMARY KEY,
                    Headline TEXT NULLABLE,
                    Description TEXT NULLABLE
                )";
            var createTableCommand = new SqliteCommand(tableCommandText, connection);
            createTableCommand.ExecuteNonQuery();
        }
        private void CreateRoutineTable(SqliteConnection connection)
        {
            var tableCommandText = @"
                CREATE TABLE IF NOT EXISTS Routine (
                    Id INTEGER PRIMARY KEY,
                    Title TEXT NULLABLE,
                    Description TEXT NULLABLE,
                    Step1 TEXT NULLABLE,
                    Step2 TEXT NULLABLE,
                    Step3 TEXT NULLABLE,
                    Step4 TEXT NULLABLE
                )";
            var createTableCommand = new SqliteCommand(tableCommandText, connection);
            createTableCommand.ExecuteNonQuery();
        }
    }
}
