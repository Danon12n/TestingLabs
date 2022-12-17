using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pet_shop.Models
{
    public class Pet
    {
        public Pet()
        { }
        public Pet(int pet_id, int shop_id, int price)
        {
            this.pet_id = pet_id;
            this.shop_id = shop_id;
            this.price = price;
            this.availability = "yes";
        }
        public int pet_id { get; set; }
        public int shop_id { get; set; }
        public int price { get; set; }
        public string availability { get; set; }
    }
}
