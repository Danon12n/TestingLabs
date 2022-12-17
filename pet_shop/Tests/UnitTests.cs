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
    [AllureLink("https://github.com/unickq/allure-nunit")]
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
        [AllureId(123)]
        public void EvenTest()
    {
            //Arrange
            SayHello();
            PetMySQLRepository rep = new PetMySQLRepository();
            //Act
            Pet newpet = rep.GetPetById(3);
            Console.WriteLine(newpet.availability);
            //Assert
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(newpet.availability == "no", $"Oh no"); },
                "Validate calculations");
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

    }
}
