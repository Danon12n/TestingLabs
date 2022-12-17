using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        //тут нужен тип времени хотя можно попробовать пока без него, потом добавить когда найду тип хранения
    }
}
