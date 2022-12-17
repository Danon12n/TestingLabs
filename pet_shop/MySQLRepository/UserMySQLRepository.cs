using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;
using pet_shop.DB;
using pet_shop.Models;

namespace pet_shop.MySQLRepository
{
    public class UserMySQLRepository
    {
        private MySqlCommand cmd = new MySqlCommand(); // команда для совершения sql-запроса
        private MySqlConnection conn = DBUtils.GetDBConnection(); //подключение базы

        public UserMySQLRepository()
        {
            try
            {
                this.conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        ~UserMySQLRepository() //чтобы деструктор завершал соединение при закрытии программы
        {
            conn.Close();
            conn.Dispose();
        }

        public List<User> GetUsers() //получение всех пользователей
        {
            string sql = "SELECT * FROM `users`";

            cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            var users = new List<User>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int userId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("user_id")));
                        string login = reader.GetString(reader.GetOrdinal("login"));
                        string password = reader.GetString(reader.GetOrdinal("password"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        string surname = reader.GetString(reader.GetOrdinal("surname"));
                        string role = reader.GetString(reader.GetOrdinal("role"));

                        users.Add(new User(userId, login, password, name, surname, role));

                    }
                    return users;
                }
            }
            return users;
        }

        public void AddUser(User user)
        {
            // Команда Insert.
            string sql = "Insert into `users` (login, password, name, surname, role) "
                                             + " values (@login, @password, @name, @surname, @role) ";

            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметры @login @password @name @surname
            cmd.Parameters.Add("@login", MySqlDbType.String).Value = user.login;
            cmd.Parameters.Add("@password", MySqlDbType.String).Value = user.password;
            cmd.Parameters.Add("@name", MySqlDbType.String).Value = user.name;
            cmd.Parameters.Add("@surname", MySqlDbType.String).Value = user.surname;
            cmd.Parameters.Add("@role", MySqlDbType.String).Value = user.role;

            // Выполнить Command (использованная для  delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();

            Console.WriteLine("Row Count affected = " + rowCount);
        }

        public User GetUserByLogin(string login)
        {
            // Команда select.
            string sql = "Select * from `users` where login like @login";


            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметр @login в запрос
            cmd.Parameters.Add("@login", MySqlDbType.String).Value = login;

            var user = new User();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        user.id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("user_id")));
                        user.login = reader.GetString(reader.GetOrdinal("login"));
                        user.password = reader.GetString(reader.GetOrdinal("password"));
                        user.name = reader.GetString(reader.GetOrdinal("name"));
                        user.surname = reader.GetString(reader.GetOrdinal("surname"));
                        user.role = reader.GetString(reader.GetOrdinal("role"));
                        return user;
                    }
                }
                return null;
            }
        }

        public void ChangeRole(int id, string role)
        {
            // Команда update.
            string sql = "Update `users` set role = @role where user_id = @id";

            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            

            // Добавляем параметр @login в запрос

            cmd.Parameters.Add("@role", MySqlDbType.String).Value = role;
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            int rowCount = cmd.ExecuteNonQuery();

            Console.WriteLine("Row Count affected = " + rowCount);
            Console.WriteLine("Роль успешно изменена!");

        }

        public User GetUserById(int id)
        {
            // Команда select.
            string sql = "Select * from `users` where user_id = @id";

            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметр @id в запрос
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            var user = new User();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        user.id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("user_id")));
                        user.login = reader.GetString(reader.GetOrdinal("login"));
                        user.password = reader.GetString(reader.GetOrdinal("password"));
                        user.name = reader.GetString(reader.GetOrdinal("name"));
                        user.surname = reader.GetString(reader.GetOrdinal("surname"));
                        user.role = reader.GetString(reader.GetOrdinal("role"));
                        Console.WriteLine("Пользователь успешно найден!");
                        return user;
                    }
                }
                Console.WriteLine("There is no such user!");
                return null;
            }
            
        }

        public void DeleteUserById(int id)
        {
            // Команда select.
            string sql = "Delete from `users` where user_id = @id";

            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметр @id в запрос
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            try
            {
                int rowCount = cmd.ExecuteNonQuery();
                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            
            Console.WriteLine("Пользователь успешно удален!");

        }
    }
}
