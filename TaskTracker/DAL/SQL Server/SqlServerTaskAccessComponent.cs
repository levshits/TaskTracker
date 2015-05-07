using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TaskTracker.Models;

namespace TaskTracker.DAL.SQL_Server
{
    public class SqlServerTaskAccessComponent:ITaskAccessComponent
    {
        private readonly string connectionString;

        public SqlServerTaskAccessComponent()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TaskTrackerDbConnection"].ConnectionString; 
        }
        
        public IEnumerable<Task> GetTasks()
        {
            List<Task> tasks = new List<Task>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                string query = "Select [Task].[Id], [Name], [Volume], [StartDate], [EndDate], [Status], [FirstName], [LastName], [MiddleName] From [Task] Left Join [Person] on [Task].[ExecutorId]=[Person].[Id]";
                command.CommandText = query;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var task = ParseFullDescriptionDatasetRow(reader);
                            tasks.Add(task);
                        }
                    }
                }

            }
            return tasks;
        }

        public Task GetTask(int id)
        {
            Task task = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                string query = "Select [Task].[Id], [Name], [Volume], [StartDate], [EndDate], [Status], [FirstName], [LastName], [MiddleName] From [Task] Left Join [Person] On [Task].[ExecutorId]=[Person].[Id] Where [Task].[Id]=@Id";
                command.CommandText = query;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = id;
                parameter.SqlDbType = SqlDbType.Int;
                command.Parameters.Add(parameter);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        task = ParseFullDescriptionDatasetRow(reader);
                    }
                }

            }
            return task;
        }

        private static Task ParseFullDescriptionDatasetRow(SqlDataReader reader)
        {
            var task = new Task
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Volume = (float) reader.GetDouble(2),
                Status = (TaskStatus) reader.GetInt16(5)
            };
            if (!reader.IsDBNull(3))
            {
                task.StartDate = (DateTime) reader.GetValue(3);
            }
            if (!reader.IsDBNull(4))
            {
                task.EndDate = (DateTime) reader.GetValue(4);
            }
            if (!reader.IsDBNull(6))
            {
                task.Executor = new Person
                {
                    FirstName = reader.GetString(6),
                    LastName = reader.GetString(7),
                    MiddleName = reader.GetString(8)
                };
            }
            return task;
        }

        public void AddTask(Task task)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query =
                    "Insert Into [Task] ([Name], [Volume], [StartDate], [EndDate], [Status], [ExecutorId]) Values (@Name, @Volume, @StartDate, @EndDate, @Status, @ExecutorId)";
                var command = connection.CreateCommand();
                command.CommandText = query;
                FillQueryParameters(task, command);
                command.ExecuteNonQuery();
            }

        }

        public void UpdateTask(Task task)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Update [Task] Set [Name]=@Name, [Volume]=@Volume, [StartDate]=@StartDate, [EndDate]=@EndDate, [Status]=@Status, [ExecutorId]=@ExecutorId Where [Id]=@Id";
                var command = connection.CreateCommand();
                command.CommandText = query;
                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = task.Id,
                    SqlDbType = SqlDbType.Int
                };
                command.Parameters.Add(parameter);
                FillQueryParameters(task, command);
                command.ExecuteNonQuery();
            }
        }

        private static void FillQueryParameters(Task task, SqlCommand command)
        {
            SqlParameter parameter = new SqlParameter
            {
                ParameterName = "@Name",
                Value = task.Name,
                SqlDbType = SqlDbType.VarChar
            };
            command.Parameters.Add(parameter);
            parameter = new SqlParameter
            {
                ParameterName = "@Volume",
                Value = task.Volume,
                SqlDbType = SqlDbType.Float
            };
            command.Parameters.Add(parameter);
            parameter = new SqlParameter
            {
                IsNullable = true,
                ParameterName = "@StartDate",
                Value = task.StartDate,
                SqlDbType = SqlDbType.Date
            };
            command.Parameters.Add(parameter);
            parameter = new SqlParameter
            {
                IsNullable = true,
                ParameterName = "@EndDate",
                Value = task.EndDate,
                SqlDbType = SqlDbType.Date
            };
            command.Parameters.Add(parameter);
            parameter = new SqlParameter
            {
                ParameterName = "@Status",
                Value = task.Status,
                SqlDbType = SqlDbType.TinyInt
            };
            command.Parameters.Add(parameter);
            parameter = new SqlParameter
            {
                IsNullable = true,
                ParameterName = "@ExecutorId",
                Value = task.ExecutorId,
                SqlDbType = SqlDbType.Int
            };
            command.Parameters.Add(parameter);
            foreach (SqlParameter param in command.Parameters)
            {
                if (param.Value == null)
                {
                    param.Value = DBNull.Value;
                }
            }
        }

        public void DeleteTask(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Delete From [Task] Where Id=@Id";
                var command = connection.CreateCommand();
                command.CommandText = query;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = id;
                parameter.SqlDbType = SqlDbType.Int;
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}