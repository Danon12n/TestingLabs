using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pet_shop.Models
{
    public class DisplayInfo
    {
        public DisplayInfo()
        { }


        public DisplayInfo(PetInfo PI, int order_number, string adress, int price)
        {
            this.pet_id = PI.pet_id;
            this.pet_type = PI.pet_type;
            this.name = PI.name;
            this.age = PI.age;
            this.color = PI.color;
            this.can_swim = PI.can_swim;
            this.reproduce_ability = PI.reproduce_ability;
            this.gender = PI.gender;
            this.pet_breed = PI.pet_breed;
            this.order_number = order_number;
            this.adress = adress;
            this.price = price;
        }

        public DisplayInfo(PetInfo PI, string adress, int price)
        {
            this.pet_id = PI.pet_id;
            this.pet_type = PI.pet_type;
            this.name = PI.name;
            this.age = PI.age;
            this.color = PI.color;
            this.can_swim = PI.can_swim;
            this.reproduce_ability = PI.reproduce_ability;
            this.gender = PI.gender;
            this.pet_breed = PI.pet_breed;
            this.adress = adress;
            this.price = price;
        }
        public int pet_id { get; set; }
        public string pet_type { get; set; } //пока что cat, dog, racoon, fox, hedgehog
        public string name { get; set; } //бенедикт
        public int age { get; set; } //в пределах разумного
        public string color { get; set; } //да хоть буромалиновый
        public int can_swim { get; set; } //true/false
        public int reproduce_ability { get; set; } //true/false
        public string gender { get; set; } //male or female
        public string pet_breed { get; set; } //любой, это как приставка к имени
        public int order_number { get; set; } // номер заказа
        public string adress { get; set; } //адрес магазина
        public int price { get; set; }// цена животного

       
    }
}
