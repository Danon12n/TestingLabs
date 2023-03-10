using System;
using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using pet_shop.Builder;

namespace pet_shop.Tests
{


    public class BuilderTests
    {
       

        public bool checkFood(PetsFood food, string expectedState, string expectedAnimal)
        {
            if (food.state == expectedState && food.animal == expectedAnimal) { return true; }
            return false;
        }

        public void PetsFoodTest()
        {
            Console.WriteLine("Heyy");
            ICooker PurinaCooker = new Purina();

            Shop Pyaterochka = new Shop(PurinaCooker);

            Pyaterochka.CookFood();
            PetsFood catsFood = Pyaterochka.GetFood();

            ICooker PedigriCooker = new Pedigri();

            Pyaterochka.SetCooker(PedigriCooker);

            Pyaterochka.CookFood();
            PetsFood dogsFood = Pyaterochka.GetFood();

            AllureLifecycle.Instance.WrapInStep(
                () => { Assert.IsTrue(checkFood(dogsFood, "solid", "dogs") && checkFood(catsFood, "liquid", "cats"), $"Oh no {dogsFood.animal} {dogsFood.state} {catsFood.animal} {catsFood.state}"); },
                "Validate calculations");
        }
    }
}