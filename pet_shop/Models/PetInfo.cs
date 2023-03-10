using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace pet_shop.Models
{
    public class PetInfo
    {
        public PetInfo()
        { }
        public PetInfo(int pet_id, string pet_type, string name, int age, string color,
            int can_swim, int reproduce_ability, string gender, string pet_breed)
        {
            this.pet_id = pet_id;
            this.pet_type = pet_type;
            this.name = name;
            this.age = age;
            this.color = color;
            this.can_swim = can_swim;
            this.reproduce_ability = reproduce_ability;
            this.gender = gender;
            this.pet_breed = pet_breed;
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

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                PetInfo PI = (PetInfo)obj;
                return 
                        (pet_type == PI.pet_type) &&
                        (pet_breed == PI.pet_breed) &&
                        (age == PI.age) &&
                        (name == PI.name) &&
                        (color == PI.color) &&
                        (gender == PI.gender) && 
                        (can_swim== PI.can_swim) && 
                        (reproduce_ability == PI.reproduce_ability) ;
            }
        }
    }
}
