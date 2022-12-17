using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using pet_shop.MySQLRepository;
using pet_shop.Models;

namespace pet_shop.Controllers
{
    public class VendorController : Controller
    {
        // GET: VendorController
        public ActionResult Index()
        {
            if (Globals.user.login == null)
            {
                Globals.error_code = 1;
                return RedirectToAction("Index", "Home");
            }

            var rep = new UserMySQLRepository();
            string role = rep.GetUserByLogin(Globals.user.login).role;

            if (role == "vendor" || role == "admin")
            {
                return View();
            }

            Globals.error_code = 2;
            return RedirectToAction("Index", "Home");

        }

        public ActionResult ShowAllOrders()
        {
            var rep = new OrderMySqlRepository();
            var orders = rep.GetOrders();
            return View(orders);
        }

        public ActionResult PetCreator()
        {
            var shopRep = new ShopMySQLRepository();
            var shops = shopRep.GetShops();
            var adresses = new List<string>();
            for (int i = 0; i < shops.Count(); i++)
            {
                adresses.Add(shops[i].adress);
            }
            ViewBag.Shops = adresses;
            return View();
        }

       

        // POST: VendorController/Create
        [HttpPost]
        public ActionResult AddNewPet(IFormCollection collection)
        {
            try
            { 
                var petRep = new PetMySQLRepository();
                var shopRep = new ShopMySQLRepository();

                int shopId = shopRep.GetShopIdByAdress(collection["shop_id"]);
                if (shopId == -10)
                {
                    Console.WriteLine("AddNewPet: There is no such shop");
                    return RedirectToAction("Index");
                }

                int petId = petRep.GetPetNewId();
                Console.WriteLine("PetId = " + petId + "\nshopId = " + shopId);

                var pet = new Pet(petId, shopId, Convert.ToInt32(collection["price"]));
                petRep.AddPetPrice(pet);

                int canSwinBit = 0;
                int canReproduceBit = 0;

                if (collection["can_swim"] == "Yes")
                    canSwinBit = 1;
                if (collection["reproduce_ability"] == "Yes")
                    canReproduceBit = 1;

                var PI = new PetInfo(pet.pet_id, collection["pet_type"], collection["name"],
                    Convert.ToInt32(collection["age"]), collection["color"],canSwinBit, canReproduceBit, collection["gender"], collection["pet_breed"]);
                petRep.AddPetInfo(PI);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult RefuseOrder(IFormCollection collection)
        {
            var orderRep = new OrderMySqlRepository();
            var petRep = new PetMySQLRepository();
            var orderNum = Convert.ToInt32(collection["orderNumber"]);
            var petId = Convert.ToInt32(collection["petId"]);

            orderRep.DeleteOrderByNumber(orderNum);

            petRep.ChangePetAvailability(petId, "yes");

            return RedirectToAction("ShowAllOrders");
        }

        [HttpPost]
        public ActionResult CloseOrder(IFormCollection collection)
        {
            var petRep = new PetMySQLRepository();
            var orderRep = new OrderMySqlRepository();

            orderRep.DeleteOrderByNumber(Convert.ToInt32(collection["orderNumber"]));
            petRep.DeletePetById(Convert.ToInt32(collection["petId"]));
            return RedirectToAction("ShowAllOrders");
        }
    }
}
