using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pet_shop.Models
{
    public class Order
    {
        public Order()
        { }
        public Order(int order_number, int user_id, int pet_id)
        {
            this.order_number = order_number;
            this.user_id = user_id;
            this.pet_id = pet_id;
        }

        public Order(int user_id, int pet_id)
        {
            this.user_id = user_id;
            this.pet_id = pet_id;
        }

        public int order_number { get; set; }
        public int user_id { get; set; }
        public int pet_id { get; set; }

    }
}
