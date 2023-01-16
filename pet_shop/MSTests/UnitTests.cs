using Allure.Net.Commons;
using pet_shop.Models;
using pet_shop.MySQLRepository;
using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using pet_shop.Controllers;
using NUnit.Framework.Constraints;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using pet_shop.Buider;

namespace pet_shop.MSTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void GetUserByLoginTest()
        {
            //Arrange
            UserMySQLRepository rep = new UserMySQLRepository();
            //Act
            User newUser = rep.GetUserByLogin("admin");
            User expectedUser = new User(id: 1, login: "admin", password: "1234", name: "admin", surname: "admin", role: "admin");
            //Wrapping Step
            Assert.IsTrue(newUser.Equals(expectedUser), $"Oh no");

        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            //Arrange
            UserMySQLRepository rep = new UserMySQLRepository();
            //Act
            User newUser = rep.GetUserById(1);
            User expectedUser = new User(id: 1, login: "admin", password: "1234", name: "admin", surname: "admin", role: "admin");
            //Wrapping Step
            Assert.IsTrue(newUser.Equals(expectedUser), $"Oh no");

        }

        [TestMethod]
        public void AddUserTest()
        {
            //Arrange
            UserMySQLRepository rep = new UserMySQLRepository();
            User newUser = new User(login: "test", password: "test", name: "test", surname: "test", role: "admin");
            //Act
            rep.AddUser(newUser);
            User expectedUser = rep.GetUserByLogin("test");
            //Wrapping Step
            Assert.IsTrue(newUser.Equals(expectedUser), $"Oh no");

        }

        [TestMethod]
        public void ChangeRoleTest()
        {
            //Arrange
            UserMySQLRepository rep = new UserMySQLRepository();
            User expectedUser = new User(login: "test", password: "test", name: "test", surname: "test", role: "vendor");
            //Act
            User newUser = rep.GetUserByLogin("test");
            rep.ChangeRole(newUser.id, "vendor");
            newUser = rep.GetUserByLogin("test");
            //Wrapping Step
            Assert.IsTrue(newUser.Equals(expectedUser), $"Oh no! was:<{newUser.role}> expected<{expectedUser.role}>");

        }

        [TestMethod]
        public void DeleteUserByIdTest()
        {
            //Arrange
            UserMySQLRepository rep = new UserMySQLRepository();
            User expectedUser = rep.GetUserByLogin("test");
            //Act
            rep.DeleteUserById(expectedUser.id);

            expectedUser = rep.GetUserByLogin("test");

            //Wrapping Step
             Assert.IsTrue(expectedUser == null, $"Oh no");

        }

        private bool CheckOrders(List<List<int>> orders, List<int> expected)
        {
            bool isEveryEqual = true;
            int i = 0;
            foreach (var order in orders)
            {
                if (order[0] != expected[i])
                {
                    isEveryEqual = false;
                    break;
                }
                i++;
            }
            return isEveryEqual;
        }



        [TestMethod]
        public void GetOrderedPetsTest()
        {
            //Arrange
            OrderMySqlRepository rep = new OrderMySqlRepository();
            List<int> expected = new List<int> { 15, 20 };
            int userId = 1;
            //Act
            var newOrders = rep.GetOrderedPets(userId);
            //Wrapping Step
            Assert.IsTrue(CheckOrders(newOrders, expected), $"Oh no! ");

        }

        [TestMethod]
        public void CreateOrderTest()
        {
            //Arrange
            OrderMySqlRepository rep = new OrderMySqlRepository();
            List<int> expectedOrders = new List<int> { -10 };
            Order newOrder = new Order(-10, -10);
            //Act
            rep.CreateOrder(newOrder);
            var resultOrders = rep.GetOrderedPets(-10);
            //Wrapping Step
            Assert.IsTrue(CheckOrders(resultOrders, expectedOrders), $"Oh no! ");
        }


        [TestMethod]
        public void DeleteOrderByNumberTest()
        {
            //Arrange
            OrderMySqlRepository rep = new OrderMySqlRepository();
            var expectedOrder = rep.GetOrderedPets(-10);
            int orderNumber = expectedOrder[0][1];
            //Act
            rep.DeleteOrderByNumber(orderNumber);

            //Wrapping Step
            Assert.IsTrue(rep.GetOrderedPets(-10).Capacity == 0, $"Oh no! ");
        }


        [TestMethod]
        public void GetOrdersTest()
        {
            //Arrange
            OrderMySqlRepository rep = new OrderMySqlRepository();
            Order newOrder1 = new Order(-10, -1);
            Order newOrder2 = new Order(-10, -2);

            rep.CreateOrder(newOrder1);
            rep.CreateOrder(newOrder2);

            Order[] expectedOrders = new[] { newOrder1, newOrder2 };
            //Act
            var resultOrders = rep.GetOrders();

            var flag = 0;
            foreach (Order order in resultOrders)
            {
                if (order.Equals(expectedOrders[0]) || order.Equals(expectedOrders[1]))
                {
                    flag++;
                }
            }
            var orderedPets = rep.GetOrderedPets(-10);
            rep.DeleteOrderByNumber(orderedPets[0][1]);
            rep.DeleteOrderByNumber(orderedPets[1][1]);


            //Wrapping Step
            Assert.IsTrue(flag == 2, $"Oh no! ");
        }
        [TestMethod]
        public void FilterResult_HandlesInvalidInput()
        {
            // Arrange
            var petRepo = new PetMySQLRepository();
            string petType = "invalid_input";
            string gender = "invalid_input";
            int canSwim = 3;
            int reproduceAbility = 3;
            int priceFrom = -1;
            int priceTo = -1;
            string adress = "invalid_input";

            // Act
            List<DisplayInfo> petInfos = petRepo.FilterResult(petType, gender, canSwim, reproduceAbility, priceFrom, priceTo, adress);

            // Assert
            Assert.IsInstanceOfType<List<DisplayInfo>>(petInfos, $"Oh no! ");
        }


        [TestMethod]
        public void GetPetById_Test()
        {
            // Arrange
            var petRepo = new PetMySQLRepository();
            int id = 1;

            // Act
            Pet testpet = petRepo.GetPetById(id);

            // Assert
            Assert.IsTrue(testpet.availability == "yes", $"Oh no! ");
        }


        [TestMethod]
        public void DeleteShop_Test()
        {
            try
            {

                // Arrange
                var shopRepo = new ShopMySQLRepository();
                Shop shop = new Shop(556, "Волгоградский проспект", "Москва", "Власов");
                shopRepo.AddNewShop(shop);
                var result = shopRepo.GetShopIdByAdress("Волгоградский проспект");
                if (result != 556) throw new Exception("Database Error");

                // Act
                shopRepo.DeleteShopById(556);
                var result2 = shopRepo.GetShopIdByAdress("Волгоградский проспект");

                // Assert
                Assert.IsTrue(result2 == -10, $"Oh no! ");

            }
            finally
            {
                var shopRepo = new ShopMySQLRepository();
                shopRepo.DeleteShopById(556);
            }

        }


        [TestMethod]
        public void AddGetShop_Test()
        {
            try
            {

                // Arrange
                var shopRepo = new ShopMySQLRepository();
                Shop shop = new Shop(556, "Волгоградский проспект", "Москва", "Тасов");
                shopRepo.AddNewShop(shop);

                // Act
                var result = shopRepo.GetShopIdByAdress("Волгоградский проспект");

                // Assert
                Assert.IsTrue(result == 556, $"Oh no! ");

            }
            finally
            {
                var shopRepo = new ShopMySQLRepository();
                shopRepo.DeleteShopById(556);
            }

        }


        [TestMethod]
        public void AddOrder_Test()
        {
            try
            {

                // Arrange
                var orderRepo = new OrderMySqlRepository();
                orderRepo.DeleteOrderByNumber(-30);
                Order order = new Order(-30, -30, -30);
                var ExpectedList = new List<int>() { -30 };
                orderRepo.CreateOrderWithId(order);

                // Act
                var result = orderRepo.GetOrderedPets(-10);
                Console.WriteLine(result);

                // Assert
                Assert.IsTrue(CheckOrders(result, ExpectedList), $"Oh no! ");

            }
            finally
            {
                var orderRepo = new OrderMySqlRepository();
                orderRepo.DeleteOrderByNumber(-30);
            }

        }
    }
}

