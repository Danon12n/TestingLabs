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
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Order order = (Order)obj;
                return  (pet_id == order.pet_id) &&
                        (order_number == order.order_number) &&
                        (user_id == order.user_id);
            }
        }

    }
}
