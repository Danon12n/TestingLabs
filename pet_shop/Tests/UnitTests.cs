using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using pet_shop.Models;
using System;
using pet_shop.MySQLRepository;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using pet_shop.Controllers;
using NUnit.Framework.Constraints;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace pet_shop.Tests
{
    [AllureNUnit]
    [AllureLink("https://github.com/Danon12n/TestingLabs")]
    public class UnitTests
    {
        [OneTimeSetUp]
        public void ClearResultsDir()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }

        //Allure.Steps required
        [AllureStep("This method is just saying hello")]
        private void SayHello()
        {
            Console.WriteLine("Hello!");
        }

        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/unickq/allure-nunit")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Core")]
        [AllureId(2)]
        public void AddPetLondon()
        {
            //Arrange
            SayHello();
            var formCol = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
                {
                    { "shop_id", "1" },
                    { "price", "200" },
                    { "can_swim", "Yes" },
                    {"reporoduce_ability", "Yes" }
                });
            var controllerMock = new Mock<VendorController>();
            controllerMock.Setup(x => x.AddNewPet(formCol));
            //Act
            controllerMock.Object.AddNewPet(formCol);
            //Assert
            AllureLifecycle.Instance.WrapInStep(() => controllerMock.Verify(
                x => x.RedirectToAction("Index", "Home"), Times.Once));
        }

        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/Danon12n/TestingLabs")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("UserMySQLRepository")]
        [AllureId(2)]
        public void GetUserByLoginTest()
        {
            //Arrange
            UserMySQLRepository rep = new UserMySQLRepository();
            //Act
            User newUser = rep.GetUserByLogin("admin");
            User expectedUser = new User(id: 1, login: "admin", password: "1234", name: "admin", surname: "admin", role: "admin");
            //Wrapping Step
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(newUser.Equals(expectedUser), $"Oh no"); },
                "Validate calculations");

        }

        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/Danon12n/TestingLabs")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("UserMySQLRepository")]
        [AllureId(3)]
        public void GetUserByIdTest()
        {
            //Arrange
            UserMySQLRepository rep = new UserMySQLRepository();
            //Act
            User newUser = rep.GetUserById(1);
            User expectedUser = new User(id:1,login: "admin", password: "1234", name: "admin", surname: "admin", role: "admin");
            //Wrapping Step
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(newUser.Equals(expectedUser), $"Oh no"); },
                "Validate calculations");

        }

        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/Danon12n/TestingLabs")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("UserMySQLRepository")]
        [AllureId(4)]
        public void AddUserTest()
        {
            //Arrange
            UserMySQLRepository rep = new UserMySQLRepository();
            User newUser = new User(login: "test", password: "test", name: "test", surname: "test", role: "admin");
            //Act
            rep.AddUser(newUser);
            User expectedUser = rep.GetUserByLogin("test");
            //Wrapping Step
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(newUser.Equals(expectedUser), $"Oh no"); },
                "Validate calculations");

        }

        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/Danon12n/TestingLabs")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("UserMySQLRepository")]
        [AllureId(5)]
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
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(newUser.Equals(expectedUser), $"Oh no! was:<{newUser.role}> expected<{expectedUser.role}>"); },
                "Validate calculations");

        }

        
         
        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/Danon12n/TestingLabs")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("UserMySQLRepository")]
        [AllureId(6)]
        public void DeleteUserByIdTest()
        {
            //Arrange
            UserMySQLRepository rep = new UserMySQLRepository();
            User expectedUser = rep.GetUserByLogin("test");
            //Act
            rep.DeleteUserById(expectedUser.id);

            expectedUser = rep.GetUserByLogin("test");
            
            //Wrapping Step
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(expectedUser == null, $"Oh no"); },
                "Validate calculations");

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


        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/Danon12n/TestingLabs")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("OrderMySQLRepository")]
        [AllureId(40)]
        public void GetOrderedPetsTest()
        {
            //Arrange
            OrderMySqlRepository rep = new OrderMySqlRepository();
            List<int> expected = new List<int> { 15, 20 };
            int userId = 1;
            //Act
            var newOrders = rep.GetOrderedPets(userId);
            //Wrapping Step
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(CheckOrders(newOrders, expected), $"Oh no! "); },
                "Validate calculations");

        }

        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/Danon12n/TestingLabs")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("OrderMySQLRepository")]
        [AllureId(41)]
        public void CreateOrderTest()
        {
            //Arrange
            OrderMySqlRepository rep = new OrderMySqlRepository();
            List<int> expectedOrders = new List<int> { -10};
            Order newOrder = new Order(-10, -10);
            //Act
            rep.CreateOrder(newOrder);
            var resultOrders = rep.GetOrderedPets(-10);
            //Wrapping Step
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(CheckOrders(resultOrders, expectedOrders), $"Oh no! "); },
                "Validate calculations");
        }
        

        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/Danon12n/TestingLabs")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("OrderMySQLRepository")]
        [AllureId(42)]
        public void DeleteOrderByNumberTest()
        {
            //Arrange
            OrderMySqlRepository rep = new OrderMySqlRepository();
            var expectedOrder = rep.GetOrderedPets(-10);
            int orderNumber = expectedOrder[0][1];
            //Act
            rep.DeleteOrderByNumber(orderNumber);

            //Wrapping Step
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(rep.GetOrderedPets(-10).Capacity == 0, $"Oh no! "); },
                "Validate calculations");
        }
        

        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/Danon12n/TestingLabs")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("OrderMySQLRepository")]
        [AllureId(43)]
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
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(flag == 2, $"Oh no! "); },
                "Validate calculations");
        }

        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/Danon12n/TestingLabs")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("CustomerController")]
        [AllureId(60)]
        public void ShowFilteredTableTest()
        {
            // Arrange
            var mock = new Mock<IShopMySQLRepository>();
            mock.Setup(a => a.GetShops()).Returns(new List<Shop>() { new Shop() });
            CustomerController controller = new CustomerController();
            List<string> expected = new List<string> { 
                "6275_Misty_Pines",
                "2690_Round_Elk_Ledge",
                "7091_Heather_Cider_Alley",
                "9817_Sunny_By-pass",
                "8007_Stony_Horse_Avenue",
                "6751_Quaking_Parkway",
                "388_Tawny_Road",
                "9015_Crystal_Field",
                "93_Hidden_Pony_Bend",
                "8152_Dusty_Barn_Lookout",
                "1600_Quiet_Villas",
                "6271_Noble_Spring_Street",
                "250_Cinder_Grounds",
                "9562_Easy_Walk",
                "1721_Velvet_Pike",
            };
            
            // Act
            ViewResult result = controller.ShowFilteredTable() as ViewResult;
            List<string> actual = controller.ViewBag.Shops as List<string>;

            // Assert
            Assert.AreEqual(expected, actual);
        }





    }
}
