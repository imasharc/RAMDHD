using RAMDHD.Database;
using RAMDHD.Models;

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
        public async Task<bool> DeleteGraphTaskByIdAsync(int id)
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
        DELETE FROM GraphTask
        WHERE Id = $Id
    ";
            command.Parameters.AddWithValue("$Id", id);

            var affectedRows = await command.ExecuteNonQueryAsync();

            return affectedRows > 0; // Return true if at least one row was deleted
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

        public async Task InsertOrUpdateNoteByIdAsync(Note note, int? id = null)
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            var insertCommand = connection.CreateCommand();

            // If an ID is provided, use it; otherwise, AUTOINCREMENT will handle it
            if (id.HasValue)
            {
                insertCommand.CommandText = @"
        INSERT OR REPLACE INTO Note (Id, Headline, Description)
        VALUES ($Id, $Headline, $Description)
        ";
                insertCommand.Parameters.AddWithValue("$Id", id.Value);
            }
            else
            {
                insertCommand.CommandText = @"
        INSERT OR REPLACE INTO Note (Id, Headline, Description)
        VALUES ($Id, $Headline, $Description)
        ";
            }

            insertCommand.Parameters.AddWithValue("$Headline", note.Headline);
            insertCommand.Parameters.AddWithValue("$Description", note.Description);

            await insertCommand.ExecuteNonQueryAsync();

            transaction.Commit();
        }
        public async Task<bool> DeleteNoteByIdAsync(int id)
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
        DELETE FROM Note
        WHERE Id = $Id
    ";
            command.Parameters.AddWithValue("$Id", id);

            var affectedRows = await command.ExecuteNonQueryAsync();

            return affectedRows > 0; // Return true if at least one row was deleted
        }
        public async Task<Note> GetNoteByIdAsync(int id)
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT Id, Headline, Description
        FROM Note
        WHERE Id = $Id
    ";
            command.Parameters.AddWithValue("$Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var note = new Note
                {
                    Id = reader.GetInt32(0),
                    Headline = reader.GetString(1),
                    Description = reader.GetString(2)
                };
                return note;
            }

            return null; // Return null or handle it if no task is found
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT Id, Headline, Description
        FROM Note
    ";

            var notes = new List<Note>();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var note = new Note
                {
                    Id = reader.GetInt32(0),
                    Headline = reader.GetString(1),
                    Description = reader.GetString(2)
                };
                notes.Add(note);
            }

            return notes;
        }

        public async Task InsertOrUpdateRoutineByIdAsync(Routine routine, int? id = null)
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            var insertCommand = connection.CreateCommand();

            // If an ID is provided, use it; otherwise, AUTOINCREMENT will handle it
            if (id.HasValue)
            {
                insertCommand.CommandText = @"
        INSERT OR REPLACE INTO Routine (Id, Title, Description, Step1, Step2, Step3, Step4)
        VALUES ($Id, $Title, $Description, $Step1, $Step2, $Step3, $Step4)
        ";
                insertCommand.Parameters.AddWithValue("$Id", id.Value);
            }
            else
            {
                insertCommand.CommandText = @"
        INSERT OR REPLACE INTO Routine (Id, Title, Description, Step1, Step2, Step3, Step4)
        VALUES ($Id, $Title, $Description, $Step1, $Step2, $Step3, $Step4)
        ";
            }

            insertCommand.Parameters.AddWithValue("$Title", routine.Title);
            insertCommand.Parameters.AddWithValue("$Description", routine.Description);
            insertCommand.Parameters.AddWithValue("$Step1", routine.Step1);
            insertCommand.Parameters.AddWithValue("$Step2", routine.Step2);
            insertCommand.Parameters.AddWithValue("$Step3", routine.Step3);
            insertCommand.Parameters.AddWithValue("$Step4", routine.Step4);

            await insertCommand.ExecuteNonQueryAsync();

            transaction.Commit();
        }
        public async Task<bool> DeleteRoutineByIdAsync(int id)
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
        DELETE FROM Routine
        WHERE Id = $Id
    ";
            command.Parameters.AddWithValue("$Id", id);

            var affectedRows = await command.ExecuteNonQueryAsync();

            return affectedRows > 0; // Return true if at least one row was deleted
        }
        public async Task<Routine> GetRoutineByIdAsync(int id)
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT Id, Title, Description, Step1, Step2, Step3, Step4
                FROM Routine
                WHERE Id = $Id
            ";
            command.Parameters.AddWithValue("$Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var routine = new Routine
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Step1 = reader.GetString(3),
                    Step2 = reader.GetString(4),
                    Step3 = reader.GetString(5),
                    Step4 = reader.GetString(6),

                };
                return routine;
            }

            return null; // Return null or handle it if no task is found
        }
        public async Task<List<Routine>> GetAllRoutinesAsync()
        {
            using var connection = _dbConnection.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT Id, Title, Description
        FROM Routine
    ";

            var routines = new List<Routine>();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var routine = new Routine
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2)
                };
                routines.Add(routine);
            }

            return routines;
        }
    }
}
