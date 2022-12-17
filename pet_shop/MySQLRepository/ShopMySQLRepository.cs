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
    public class ShopMySQLRepository
    {

        private MySqlCommand cmd = new MySqlCommand(); // команда для совершения sql-запроса
        private MySqlConnection conn = DBUtils.GetDBConnection(); //подключение базы

        public ShopMySQLRepository()
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

        public int GetShopIdByAdress(string adress)
        {
            // Команда select.
            string sql = "Select * from `shops` where adress like @adress";

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
