using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace pet_shop.DB
{
    public class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "192.168.1.69";
            int port = 3306;
            string database = "pet_shop";
            string username = "root";
            string password = "root";

            return DBMySQLUtiles.GetDBConnection(host, port, database, username, password);
        }

        public static MySqlConnection GetDBConnection(string connString)
        {
            return DBMySQLUtiles.GetDBConnection(connString);
        }
    }
}
