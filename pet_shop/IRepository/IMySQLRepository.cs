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
    public interface IShopMySQLRepository
    {
        public List<Shop> GetShops();
        public int GetShopIdByAdress(string adress);
        public string GetShopAdressByShopId(int shopId);
    }
}
