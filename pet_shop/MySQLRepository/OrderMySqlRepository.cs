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
    public class OrderMySqlRepository
    {
        private MySqlCommand cmd = new MySqlCommand(); // команда для совершения sql-запроса
        private MySqlConnection conn = DBUtils.GetDBConnection(); //подключение базы

        public OrderMySqlRepository()
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

        ~OrderMySqlRepository() //чтобы деструктор завершал соединение при закрытии программы
        {
            conn.Close();
            conn.Dispose();
        }

        public void CreateOrder(Order order)
        {
            string sql = "Insert into `orders` (user_id, pet_id) "
                                            + " values (@userId, @petId) ";
            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            cmd.Parameters.Add("@userId", MySqlDbType.Int32).Value = order.user_id;
            cmd.Parameters.Add("@petId", MySqlDbType.Int32).Value = order.pet_id;

            try
            {
                int rowCount = cmd.ExecuteNonQuery();
                Console.WriteLine("Row Count affected = " + rowCount);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }

            Console.WriteLine("Заказ успешно добавлен");
        }

        public List<List<int>> GetOrderedPets(int userId)
        {
            string sql = "Select pet_id, order_number from `orders` where user_id = @userId ";

            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            cmd.Parameters.Add("@userId", MySqlDbType.Int32).Value = userId;

            var orders = new List<List<int>>();
            int petId, orderNumber;


            try
            {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            petId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pet_id")));
                            orderNumber = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("order_number")));
                            orders.Add(new List<int>() {petId, orderNumber });
                        }
                    }
                    return orders;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }


            Console.WriteLine("Вы не заказали ни одного животного");
            return null;
        }

        public void DeleteOrderByNumber(int num)
        {
            // Команда select.
            string sql = "Delete from `orders` where order_number = @num";

            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметр @num в запрос
            cmd.Parameters.Add("@num", MySqlDbType.Int32).Value = num;

            try
            {
                int rowCount = cmd.ExecuteNonQuery();
                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }

            Console.WriteLine("Заказ успешно отменен или закрыт!");
        }

        public List<Order> GetOrders()
        {

            string sql = "SELECT * FROM `orders`";

            cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            var orders = new List<Order>();

            try
            {

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int userId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("user_id")));
                            int petId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pet_id")));
                            int orderNumber = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("order_number")));

                            orders.Add(new Order(orderNumber, userId, petId));

                        }
                        return orders;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            Console.WriteLine("Нет заказов в бд!");
            

            return orders;
        }
    }
}
