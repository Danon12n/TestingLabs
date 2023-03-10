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
    public class ShopMySQLRepository : IShopMySQLRepository
    {

        private MySqlCommand cmd; // команда для совершения sql-запроса
        private MySqlConnection conn; //подключение базы

        public ShopMySQLRepository()
        {
            cmd = new MySqlCommand();
            conn = DBUtils.GetDBConnection();
            try
            {
                this.conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        public ShopMySQLRepository(string connString)
        {
            cmd = new MySqlCommand();
            conn = DBUtils.GetDBConnection(connString);
            try
            {
                this.conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        ~ShopMySQLRepository() //чтобы деструктор завершал соединение при закрытии программы
        {
            conn.Close();
            conn.Dispose();
        }

        public List<Shop> GetShops() //получение всех пользователей
        {
            string sql = "SELECT * FROM `shops`";

            cmd.Connection = conn;
            cmd.CommandText = sql;

            var shops = new List<Shop>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int shopId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("shop_id")));
                        string city = reader.GetString(reader.GetOrdinal("city"));
                        string adress = reader.GetString(reader.GetOrdinal("adress"));
                        string owner = reader.GetString(reader.GetOrdinal("owner"));

                        shops.Add(new Shop(shopId, adress, city, owner));
                        /*Console.WriteLine("Shop Id:" + shopId);
                        Console.WriteLine("Shop city:" + city);
                        Console.WriteLine("Shop adress:" + adress);
                        Console.WriteLine("Shop owner:" + owner);*/
                    }
                }
            }
            return shops;
        }

        public void AddNewShop(Shop shop)
        {
            string sql = "Insert into shops (shop_id, adress, city, owner) values (@shop_id, @adress, @city, @owner) ";
            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add("@shop_id", MySqlDbType.Int32).Value = shop.id;
            cmd.Parameters.Add("@adress", MySqlDbType.String).Value = shop.adress;
            cmd.Parameters.Add("@city", MySqlDbType.String).Value = shop.city;
            cmd.Parameters.Add("@owner", MySqlDbType.String).Value = shop.owner;
            cmd.Connection = conn;
            int rowCount = cmd.ExecuteNonQuery();

        }

        public void DeleteShopById(int id)
        {
            string sql = "Delete from `shops` where shop_id = " + id;
            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            int rowCount = cmd.ExecuteNonQuery();

        }

        public int GetShopIdByAdress(string adress)
        {
            cmd = new MySqlCommand();
            string sql = "Select * from shops where adress like @adress ";

            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметр @login в запрос
            cmd.Parameters.Add("@adress", MySqlDbType.String).Value = adress;
            int shopId;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        shopId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("shop_id")));
                        return shopId;
                    }
                }
                return -10;// такого магазина нет
            }
        }

        public string GetShopAdressByShopId(int shopId)
        {
            // Команда select.
            string sql = "Select * from `shops` where shop_id = @shopId";

            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметр @login в запрос
            cmd.Parameters.Add("@shopId", MySqlDbType.String).Value = shopId;
            string adress = "";

            try
            {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            adress = reader.GetString(reader.GetOrdinal("adress"));
                            return adress; // info[0] = shop_id || info[1] = price of the pet
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There is no adress or error");
                Console.WriteLine("Error: " + e);
            }
            return adress;
        }
    }
}
