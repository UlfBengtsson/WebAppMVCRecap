using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppMVCRecap.Models;

namespace WebAppMVCRecap.Controllers
{
    public class CarsController : Controller
    {

        // Don´t do this
        static List<Car> cars = new List<Car>();

        public IActionResult Index()
        {
            return View(cars);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Brand,Name")] Car car)
        {
            if (ModelState.IsValid)
            {
                cars.Add(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }
    }
}