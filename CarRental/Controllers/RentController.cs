using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using CarRental.Repository;

namespace CarRental.Controllers
{
    public class RentController : Controller
    {
        private readonly IData data;
        public RentController(IData data) 
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            var list = data.GetAllRents();
            return View(list);
        }

        public IActionResult Add() { 
            return View();
        }
        [HttpPost]
        public IActionResult Add(Rent rent)
        {
            if(!ModelState.IsValid)
                return View(rent);
            ViewBag.isSaved = data.BookingNow(rent);
            ModelState.Clear();
            return View();
        }
        [HttpGet]
        public IActionResult GetBrand()
        {
            var list = data.GetBrand();
            return Json(list);
        }
        [HttpGet]
        public IActionResult GetModel(string brand) {
            var list = data.GetModel(brand);
            return Json(list);
        }
        [HttpGet]
        public IActionResult GetDriver()
        {
            var list = data.GetAllDrivers();
            return Json(list);
        }

	}
}
