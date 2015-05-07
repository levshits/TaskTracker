using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using TaskTracker.Models;

namespace TaskTracker.DAL.SQL_Server
{
    public class SqlServerPersonAccessComponent:IPersonAccessComponent
    {
        private readonly string connectionString;

        public SqlServerPersonAccessComponent()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TaskTrackerDbConnection"].ConnectionString;
        }

        public IEnumerable<Person> GetPersons()
        {
            List<Person> result = new List<Person>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Select [Id], [FirstName], [LastName], [MiddleName] From [Person]";
                var command = connection.CreateCommand();
                command.CommandText = query;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(ParseDataSetRow(reader));
                        }
                    }
                }
            }
            return result;
        }

        private Person ParseDataSetRow(SqlDataReader reader)
        {
            var person = new Person
            {
                Id=reader.GetInt32(0), 
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2)
                };
            if (!reader.IsDBNull(3))
            {
                person.MiddleName = reader.GetString(3);
            }
            return person;
        }

        public Person GetPerson(int id)
        {
            Person person = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Select [Id], [FirstName], [LastName], [MiddleName] From [Person] Where [Id]=@Id";
                var command = connection.CreateCommand();
                command.CommandText = query;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = id;
                parameter.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(parameter);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        person = ParseDataSetRow(reader);
                    }
                }
            }
            return person;
        }

        public void AddPerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Insert Into [Person]  ( [FirstName], [LastName], [MiddleName]) Values (@FirstName, @LastName, @MiddleName)";
                var command = connection.CreateCommand();
                command.CommandText = query;
                FillQueryParameters(person, command);
                command.ExecuteNonQuery();
            }
        }

        private static void FillQueryParameters(Person person, SqlCommand command)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@FirstName";
            parameter.Value = person.FirstName;
            parameter.SqlDbType = System.Data.SqlDbType.VarChar;
            command.Parameters.Add(parameter);
            parameter = new SqlParameter();
            parameter.ParameterName = "@LastName";
            parameter.Value = person.FirstName;
            parameter.SqlDbType = System.Data.SqlDbType.VarChar;
            command.Parameters.Add(parameter);
            parameter = new SqlParameter();
            parameter.ParameterName = "@MiddleName";
            parameter.Value = person.FirstName;
            parameter.SqlDbType = System.Data.SqlDbType.VarChar;
            command.Parameters.Add(parameter);
            foreach (SqlParameter param in command.Parameters)
            {
                if (param.Value == null)
                {
                    param.Value = DBNull.Value;
                }
            }
        }

        public void UpdatePerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Update [Person] Set [FirstName]=@FirstName, [LastName]=@LastName, [MiddleName]=@MiddleName Where [Id]=@Id";
                var command = connection.CreateCommand();
                command.CommandText = query;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = person.Id;
                parameter.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(parameter);
                FillQueryParameters(person, command);
                command.ExecuteNonQuery();
            }
        }

        public void DeletePerson(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Delete From [Person] Where [Id]=@Id";
                var command = connection.CreateCommand();
                command.CommandText = query;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = id;
                parameter.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }
}