using Microsoft.Maui.Controls;
using RAMDHD.Database;
using RAMDHD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.DataAccess
{
    public class DatabaseDataAccess
    {
        private readonly DatabaseConnection _dbConnection;

        public DatabaseDataAccess(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.InitializeDatabase();
        }

        public async Task InsertOrUpdateGraphTaskByIdAsync(GraphTask graphTask, int? id = null)
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            var insertCommand = connection.CreateCommand();

            // If an ID is provided, use it; otherwise, AUTOINCREMENT will handle it
            if (id.HasValue)
            {
                insertCommand.CommandText = @"
        INSERT OR REPLACE INTO GraphTask (Id, Title, Activity1, Activity2, Activity3, Activity4, Procrastination)
        VALUES ($Id, $Title, $Activity1, $Activity2, $Activity3, $Activity4, $ProcrastinationActivity)
        ";
                insertCommand.Parameters.AddWithValue("$Id", id.Value);
            }
            else
            {
                insertCommand.CommandText = @"
        INSERT OR REPLACE INTO GraphTask (Title, Activity1, Activity2, Activity3, Activity4, Procrastination)
        VALUES ($Title, $Activity1, $Activity2, $Activity3, $Activity4, $ProcrastinationActivity)
        ";
            }

            insertCommand.Parameters.AddWithValue("$Title", graphTask.Title);
            insertCommand.Parameters.AddWithValue("$Activity1", graphTask.Activity1);
            insertCommand.Parameters.AddWithValue("$Activity2", graphTask.Activity2);
            insertCommand.Parameters.AddWithValue("$Activity3", graphTask.Activity3);
            insertCommand.Parameters.AddWithValue("$Activity4", graphTask.Activity4);
            insertCommand.Parameters.AddWithValue("$ProcrastinationActivity", graphTask.ProcrastinationActivity);

            await insertCommand.ExecuteNonQueryAsync();

            transaction.Commit();
        }

        public async Task<GraphTask> GetGraphTaskByIdAsync(int id)
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT Id, Title, Activity1, Activity2, Activity3, Activity4, Procrastination
        FROM GraphTask
        WHERE Id = $Id
    ";
            command.Parameters.AddWithValue("$Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var graphTask = new GraphTask
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Activity1 = reader.GetString(2),
                    Activity2 = reader.GetString(3),
                    Activity3 = reader.GetString(4),
                    Activity4 = reader.GetString(5),
                    ProcrastinationActivity = reader.GetInt32(6)
                };
                return graphTask;
            }

            return null; // Return null or handle it if no task is found
        }


        public async Task<List<GraphTask>> GetAllGraphTasksAsync()
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT Id, Title, Activity1, Activity2, Activity3, Activity4, Procrastination
        FROM GraphTask
    ";

            var graphTasks = new List<GraphTask>();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var graphTask = new GraphTask
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Activity1 = reader.GetString(2),
                    Activity2 = reader.GetString(3),
                    Activity3 = reader.GetString(4),
                    Activity4 = reader.GetString(5),
                    ProcrastinationActivity = reader.GetInt32(6)
                };
                graphTasks.Add(graphTask);
            }

            return graphTasks;
        }
    }
}
