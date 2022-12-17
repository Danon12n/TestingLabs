using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using pet_shop.Models;

namespace pet_shop
{
    public static class Globals
    {
        /* public static User user { get; set; } = new User("null", "null", "null", "null", "null");*/

        public static List<DisplayInfo> DisplayInfoForTables { get; set; } = new List<DisplayInfo>();
        public static User user { get; set; } = new User();
        public static int error_code { get; set; } = 0;
    }
}
   

