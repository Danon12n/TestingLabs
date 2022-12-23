using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pet_shop.Builder
{
    public class PetsFood
    {
        public string animal;
        public string state;

        public PetsFood()
        {
            animal = "";
            state = "";
        }

        public string AboutPetsFood()
        {
            return $"This food for {animal} in a {state} state.";
        }

        public void SetFoodState(string state) { this.state = state; }
        public void SetFoodAnimal(string animal) { this.animal = animal; }

    }

    public interface ICooker
    {
        void ChooseFoodState();
        void ChooseAnimal();
        PetsFood GetFood();
    }

    public class Pedigri : ICooker
    {
        private PetsFood food = new PetsFood();
        public void ChooseFoodState() { food.SetFoodState("solid"); }
        public void ChooseAnimal() { food.SetFoodAnimal("dogs"); }
        public PetsFood GetFood() { return food; }
    }

    public class Purina : ICooker
    {
        private PetsFood food = new PetsFood();
        public void ChooseFoodState() { food.SetFoodState("liquid"); }
        public void ChooseAnimal() { food.SetFoodAnimal("cats"); }
        public PetsFood GetFood() { return food; }
    }

    public class Shop
    {
        private ICooker cooker;
        public Shop(ICooker cooker) 
        {
            this.cooker = cooker;
           
        }
        public void CookFood()
        {
            cooker.ChooseFoodState();
            cooker.ChooseAnimal();
        }
        public void SetCooker(ICooker cooker) { this.cooker = cooker; }

        public string AboutShopFood()
        {
            return cooker.GetFood().AboutPetsFood();
        }

        public PetsFood GetFood() { return cooker.GetFood(); }
    }
}
