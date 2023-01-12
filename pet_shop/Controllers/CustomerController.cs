using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using pet_shop.Models;
using pet_shop.MySQLRepository;

namespace pet_shop.Controllers
{
    public class CustomerController : Controller
    {
       
        public ActionResult Index()
        {

            if (Globals.user.login == null)
            {
                Globals.error_code = 1;
                return RedirectToAction("Index", "Home");
            }

            var rep = new PetMySQLRepository();
            var shopRep = new ShopMySQLRepository();

            var pets = rep.GetPets();
            
            var DisplayInfo = new List<DisplayInfo>();
            var petsInfo = new List<int>(); // 0 = shop_id, 1 = price, 2 = pet availability

            for (int i = 0; i < pets.Count(); i++)
            {
                petsInfo = rep.GetShopIdByPetId(pets[i].pet_id);
                if (petsInfo[2] == 1)
                {
                    DisplayInfo.Add(new DisplayInfo(pets[i], shopRep.GetShopAdressByShopId(petsInfo[0]), petsInfo[1]));
                }
            }

            return View(DisplayInfo);
        }


        [HttpPost]
        public ActionResult CreateOrder(IFormCollection collection)
        {
            var rep = new OrderMySqlRepository();
            var petRep = new PetMySQLRepository();
            var petId = Convert.ToInt32(collection["petId"]);
            var order = new Order(Globals.user.id, petId);

            rep.CreateOrder(order);

            petRep.ChangePetAvailability(petId, "no");

            return RedirectToAction("Index");
        }

        // POST: CustomerController/DeletePet
        [HttpPost]
        public ActionResult DeletePet(IFormCollection collection)
        {
            var rep = new PetMySQLRepository();
            Console.WriteLine("DeletePetById(" + collection["petId"] + ");");
            rep.DeletePetById(Convert.ToInt32(collection["petId"]));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult AnotherPage(int num)
        {
            Console.WriteLine(num); //таким образом передаются квери параметры
            var rep = new UserMySQLRepository();
            User user = rep.GetUserByLogin("Danon");
            
            return Json(user);
        }

        public ActionResult ShowOrders()
        {
            var petRep = new PetMySQLRepository();
            var orderRep = new OrderMySqlRepository();
            var shopRep = new ShopMySQLRepository();

            var orderedPetsIds = orderRep.GetOrderedPets(Globals.user.id);

            var DisplayInfo = new List<DisplayInfo>();
            var petsInfo = new List<int>();

            for (int i = 0; i < orderedPetsIds.Count(); i++)
            {
                petsInfo = petRep.GetShopIdByPetId(orderedPetsIds[i][0]);
                DisplayInfo.Add(new DisplayInfo(petRep.GetPetInfoById(orderedPetsIds[i][0]), orderedPetsIds[i][1], shopRep.GetShopAdressByShopId(petsInfo[0]), petsInfo[1]));
            }
            return View(DisplayInfo);
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

            return RedirectToAction("ShowOrders");
        }

        public ActionResult ShowFilteredTable()
        {
            var shopRep = new ShopMySQLRepository();
            var shops = shopRep.GetShops();
            var adresses = new List<string>();
            for (int i = 0; i < shops.Count(); i++)
            {
                adresses.Add(shops[i].adress);
            }
            ViewBag.Shops = adresses;

            return View(Globals.DisplayInfoForTables);
        }

        [HttpPost]
        public ActionResult Filter(IFormCollection collection)
        {
            var rep = new PetMySQLRepository();
            int canSwim = 0, canReproduce = 0;
            if (collection["canSwim"] == "Yes")
                canSwim = 1;
            else if (collection["canSwim"] == "Any")
                canSwim = 2;

            if (collection["reproduceAbility"] == "Yes")
                canReproduce = 1;
            else if (collection["reproduceAbility"] == "Any")
                canReproduce = 2;
            Globals.DisplayInfoForTables = rep.FilterResult(collection["petType"], collection["gender"],
                canSwim, canReproduce,
                Convert.ToInt32(collection["priceFrom"]), Convert.ToInt32(collection["priceTo"]),
                collection["adress"]);
          

            return RedirectToAction("ShowFilteredTable");
        }

    }
}
