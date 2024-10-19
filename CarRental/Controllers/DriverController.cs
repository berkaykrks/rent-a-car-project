using Microsoft.AspNetCore.Mvc;
using CarRental.Models;
using CarRental.Repository;

namespace CarRental.Controllers
{
    public class DriverController : Controller
    {
        private readonly IData data;
        public DriverController(IData _data)
        {
            data = _data;
        }
        public IActionResult Index()
        {
            var list = data.GetAllDrivers();
			return View(list);
        }
        public IActionResult Add() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Driver newdriver)
        {
            if (!ModelState.IsValid)
                return View(newdriver);
			bool isSaved = data.AddDriver(newdriver);
			ViewBag.isSaved = data.AddDriver(newdriver);
            ModelState.Clear();
                
            return View();

			
		}

    }
}
