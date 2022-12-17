using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using pet_shop.Models;
using System;
using pet_shop.MySQLRepository;
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
            SayHello();
            PetMySQLRepository rep = new PetMySQLRepository();
            Pet newpet = rep.GetPetById(3);
            Console.WriteLine(newpet.availability);
            //Wrapping Step
            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(newpet.availability == "no", $"Oh no"); },
                "Validate calculations");
        }
    }
}
