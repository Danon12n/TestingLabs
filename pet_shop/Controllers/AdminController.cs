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
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Index()
        {
            if (Globals.user.login == null)
            {
                Globals.error_code = 1;
                return RedirectToAction("Index", "Home");
            }

            var rep = new UserMySQLRepository();
            string role = rep.GetUserByLogin(Globals.user.login).role;

            if (role == "admin") //проверка прав доступа (роль из бд)
            {
                var users = rep.GetUsers();
                return View(users);
            }
            Globals.error_code = 2;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult MakeVendor(IFormCollection collection)
        {
            var rep = new UserMySQLRepository();
            rep.ChangeRole(Convert.ToInt32(collection["userId"]), "vendor");
            //Console.WriteLine("MakeVendor(" + collection["userId"] + ");");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult MakeCustomer(IFormCollection collection)
        {
            var rep = new UserMySQLRepository();
            rep.ChangeRole(Convert.ToInt32(collection["userId"]),"customer");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteUser(IFormCollection collection)
        {
            var rep = new UserMySQLRepository();
            rep.DeleteUserById(Convert.ToInt32(collection["userId"]));
            //Console.WriteLine("DeleteUser(" + collection["userId"] + ");");
            return RedirectToAction("Index");
        }



        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
