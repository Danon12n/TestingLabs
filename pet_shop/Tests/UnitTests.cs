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
        







    }
}
