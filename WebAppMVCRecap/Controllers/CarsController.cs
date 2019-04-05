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

        readonly ICarsRepository _carsRepository;

        public CarsController(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        public IActionResult Index()
        {
            return View(_carsRepository.AllCars());
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
                _carsRepository.Create(car.Brand , car.Name);
                return RedirectToAction("Index");
            }
            return View(car);
        }
    }
}