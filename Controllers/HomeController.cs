using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotels.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly AppDbContext _context;
        List<Hotel> hotel;

        public HomeController(AppDbContext context)
        {

            _context = context;
            hotel = new List<Hotel>();
        }
        public IActionResult CreateNewReord(Hotel hotels)
        {
            if (ModelState.IsValid)
            {
				_context.hotel.Add(hotels);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			var hotel = _context.hotel.ToList();

			return View("Index", hotel);
		}
        public IActionResult Update (Hotel hotel)
        {
            _context.hotel.Update(hotel);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var hotelEdit = _context.hotel.SingleOrDefault(x => x.Id == id);

            return View(hotelEdit);
        }
            public IActionResult Delete(int id)
        {
            var hotelDelete = _context.hotel.SingleOrDefault(x => x.Id == id); 
            _context.hotel.Remove(hotelDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        /*  
         *  [HttpPost]
          public IActionResult Index (string city)
          {

              var model = _context.hotel.Where(a => a.City.Contains(city)).ToList();
              ViewBag.hotels = model;
              return View(model);
          }*/
        [HttpGet]
        public IActionResult Index(string city)
        {
            List<Hotel> hotels;

            if (string.IsNullOrWhiteSpace(city))
            {
                // If the city is not provided, display all hotels
                hotels = _context.hotel.ToList();
            }
            else
            {
                // If a city is provided, search for hotels in that city
                hotels = _context.hotel.Where(h => h.City.Contains(city)).ToList();
               
            }
            ViewBag.h = hotels;
            return View();
        }
        public IActionResult Index()
        {
            var hotel = _context.hotel.ToList();
          
            return View(hotel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}