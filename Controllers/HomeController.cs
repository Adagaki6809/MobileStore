using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobileStore.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MobileStore.Controllers
{
    public class HomeController : Controller
    {
        MobileContext db;
        public HomeController(MobileContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Phones.ToList());
        }

        [HttpGet]
        public IActionResult Buy(int? id)
        {
            
            if (id == null)
            {
                Debug.WriteLine("id == null");
                return RedirectToAction("Index");
            }
            ViewBag.PhoneId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Order order)
        {
            db.Orders.Add(order);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, " + order.User + ", за покупку!";
        }

        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Login(string login, string password, int age, string comment)
        {
            string authData = $"Login: {login}   Password: {password}   Age: {age}  Comment: {comment}";
            return Content(authData);
        }

        public ContentResult Area(Geometry geometry)
        {
            return Content("area");
        }
    }
    public class Geometry
    {
        
        public int Altitude { get; set; } // основание
        public int Height { get; set; } // высота

        public double GetArea() // вычисление площади треугольника
        {
            return Altitude * Height / 2;
        }
    }
}
