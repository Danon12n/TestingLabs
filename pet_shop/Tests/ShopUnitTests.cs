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
    public class ShopUnitTests
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
            User expectedUser = new User(id: 1, login: "admin", password: "1234", name: "admin", surname: "admin", role: "admin");
            //Wrapping Step
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(newUser.Equals(expectedUser), $"Oh no"); },
                "Validate calculations");

        }





    }
}
