using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pet_shop.Models
{
    public class Shop
    {
        public Shop()
        { }

        public Shop(int id, string adress, string city, string owner)
        {
            this.id = id;
            this.adress = adress;
            this.city = city;
            this.owner = owner;
        }
        public int id { get; set; }
        public string adress { get; set; }
        public string city { get; set; }
        public string owner { get; set; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Shop shop= (Shop)obj;
                return 
                        (adress == shop.adress) &&
                        (owner == shop.owner) &&
                        (city == shop.city);
            }
        }
    }
}
