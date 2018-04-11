using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace Test
{
    public class PersonService : BaseService
    {
        public PersonService(ConnectionStringSettings connectionString) : base(connectionString) { }

        public void Create(Person person)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var createCommand = connection.CreateCommand();

                        //Параметры--------------------------------------------------------------
                        var loginParameter = createCommand.CreateParameter();
                        loginParameter.DbType = DbType.String;
                        loginParameter.IsNullable = false;
                        loginParameter.ParameterName = "@Login";
                        loginParameter.Value = person.Login_person;
                        createCommand.Parameters.Add(loginParameter);

                        var passwordParameter = createCommand.CreateParameter();
                        passwordParameter.DbType = DbType.String;
                        passwordParameter.IsNullable = false;
                        passwordParameter.ParameterName = "@Password";
                        passwordParameter.Value = person.Password_person;
                        createCommand.Parameters.Add(passwordParameter);

                        var nicknameParameter = createCommand.CreateParameter();
                        nicknameParameter.DbType = DbType.String;
                        nicknameParameter.IsNullable = false;
                        nicknameParameter.ParameterName = "@Name";
                        nicknameParameter.Value = person.Name_person;
                        createCommand.Parameters.Add(nicknameParameter);

                        //-----------------------------------------------------------------------

                        createCommand.Transaction = transaction;

                        createCommand.CommandText = $@"INSERT INTO Person (login_person,password_person,name_person) values(@Login,@Password,@Name)";
                        createCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (DbException exception)
                    {
                        Console.WriteLine(exception.Message);
                        transaction.Rollback();
                    }
                }
            }
        }
        public void Delete(int id)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $@"DELETE FROM Person where id_person = {id}";
                command.ExecuteNonQuery();
            }
        }
        public Person Parse(DbDataReader reader)
        {
            Person person = new Person();

            person.Id_person = Convert.ToInt32(reader["id_person"]);

            person.Login_person= reader["login_person"] as string;
            person.Password_person = reader["password_person"] as string;
            person.Name_person = reader["name_person"] as string;

            return person;
        }
        public Person Select(int id)
        {
            Person person = null;
            using (var connection = CreateConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $@"SELECT * FROM Person where Id = {id}";

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    person = Parse(reader);
                }
            }
            return person;
        }
        public Person Login(string login, string password)
        {
            Person person = null;
            using (var connection = CreateConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();

                //var loginParameter = command.CreateParameter();
                //loginParameter.DbType = DbType.String;
                //loginParameter.IsNullable = false;
                //loginParameter.ParameterName = "@Login";
                //loginParameter.Value = person.Login_person;
                //command.Parameters.Add(loginParameter);

                //var passwordParameter = command.CreateParameter();
                //passwordParameter.DbType = DbType.String;
                //passwordParameter.IsNullable = false;
                //passwordParameter.ParameterName = "@Password";
                //passwordParameter.Value = person.Password_person;
                //command.Parameters.Add(passwordParameter);

                command.CommandText = $@"SELECT * FROM Person where login_person  = '{login}' and password_person = '{password}'";

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    person = Parse(reader);
                }
            }
            return person;
        }
    }
}