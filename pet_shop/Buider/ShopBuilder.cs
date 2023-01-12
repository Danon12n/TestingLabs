using Microsoft.Extensions.Hosting;
using pet_shop.Models;
using System;

namespace pet_shop.Buider
{
    public static class ShopBuilder
    {
        public static Shop addId(int id, Shop shop)
        {
            shop.id = id;
            return shop;
        }
        public static Shop addAdress(string adress, Shop shop)
        {
            shop.adress = adress;
            return shop;
        }
        public static Shop addCity(string city, Shop shop)
        {
            shop.city = city;
            return shop;
        }
        public static Shop addOwner(string owner, Shop shop)
        {
            shop.owner = owner;
            return shop;
        }

        //Return the finally consrcuted User object
        public static Shop build()
        {
            Shop shop = new Shop();
            return shop;
        }

    }
}
