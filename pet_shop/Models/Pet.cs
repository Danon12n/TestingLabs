using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

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


        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Pet pet = (Pet)obj;
                return (pet_id == pet.pet_id) &&
                        (shop_id == pet.shop_id) &&
                        (price == pet.price) &&
                        (availability == pet.availability);
            }
        }
    }
}
