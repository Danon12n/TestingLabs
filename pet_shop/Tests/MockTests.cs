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
using pet_shop.HelperClasses;

namespace MockTests.Tests
{
    [AllureNUnit]
    [AllureLink("https://github.com/Danon12n/TestingLabs")]
    public class MockTests
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
        [AllureFeature("CustomerController")]
        [AllureId(1)]
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


        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/Danon12n/TestingLabs")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("CustomerController")]
        [AllureId(2)]
        public void CheckFreeIdPetMockTest()
        {
            // Arrange
            var mock = new Mock<IPetMySQLRepository>();
            mock.Setup(mock => mock.GetPetById(25)).Returns(new Pet());
            PetHelper helper = new PetHelper(mock.Object);

            // Act
            var result = helper.CheckFreeId(25);

            // Assert
            Assert.AreEqual(result, "Такой питомец существует");
        }





    }
}
