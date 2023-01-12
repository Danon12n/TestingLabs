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
        public List<User> GetUsers(); //��������� ���� �������������
        public void AddUser(User user); // ���������� ������������
        public User GetUserByLogin(string login); // ��������� ������������ �� ��� ������
        public void ChangeRole(int id, string role); // ����� ���� ������������
        public User GetUserById(int id); // ��������� ������������ �� ��� ID
        public void DeleteUserById(int id); // �������� ������������ �� ��� ID
        
    }

    public interface IPetMySQLRepository
    {
        public void AddPetInfo(PetInfo PI);
        public void AddPetPrice(Pet pet);
        public int GetPetNewId();
        public List<PetInfo> GetPets(); //��������� ���� �������������
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
