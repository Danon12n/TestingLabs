using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

using pet_shop.MySQLRepository;
using pet_shop.Models;




namespace pet_shop.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: RegistrationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RegistrationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegistrationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistrationController/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string login = collection["login"];
                string password = collection["password"];
                string name = collection["name"];
                string surname = collection["surname"];/*
                Console.WriteLine("login:" + collection["login"]);
                Console.WriteLine("password:" + collection["password"]);
                Console.WriteLine("name:" + collection["name"]);
                Console.WriteLine("surname:" + collection["surname"]);*/

                var user = new User(login,password,name,surname,"customer");
                var rep = new UserMySQLRepository();

                rep.AddUser(user);
                Console.WriteLine("Пользователь добавлен!");
                return RedirectToAction("Index", "Home");
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: RegistrationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegistrationController/Edit/5
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

        // GET: RegistrationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegistrationController/Delete/5
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
