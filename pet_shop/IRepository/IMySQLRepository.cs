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

    public interface IUserMySQLRepository
    {
        public List<User> GetUsers(); //получение всех пользователей
        public void AddUser(User user); // добавление пользователя
        public User GetUserByLogin(string login); // получение пользователя по его логину
        public void ChangeRole(int id, string role); // смена роли пользователя
        public User GetUserById(int id); // получение пользователя по его ID
        public void DeleteUserById(int id); // удаление пользователя по его ID
        
    }

    public interface IPetMySQLRepository
    {
        public void AddPetInfo(PetInfo PI);
        public void AddPetPrice(Pet pet);
        public int GetPetNewId();
        public List<PetInfo> GetPets(); //получение всех пользователей
        public List<int> GetShopIdByPetId(int petId);
        public void DeletePetById(int id);
        public void ChangePetAvailability(int petId, string availability);
        public Pet GetPetById(int id);
        public PetInfo GetPetInfoById(int id);
        public List<DisplayInfo> FilterResult(string petType, string gender, int canSwim, int reproduceAbility, int priceFrom, int priceTo, string adress);
    }

    public interface IOrderMySqlRepository
    {
        public void CreateOrder(Order order);
        public List<List<int>> GetOrderedPets(int userId);
        public void DeleteOrderByNumber(int num);
        public List<Order> GetOrders();
        
    }
}
