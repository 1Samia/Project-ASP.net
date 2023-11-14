using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Controllers
{
    public class DashboardController : Controller
    {
		private readonly AppDbContext _context;
        public DashboardController(AppDbContext context)
        {
            _context = context;

		}
		public IActionResult Update(Hotel hotel)
		{
			_context.hotel.Update(hotel);
			_context.SaveChanges();
            TempData["Edit"] = "ok";
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
			if (hotelDelete != null)
			{
				_context.hotel.Remove(hotelDelete);
				_context.SaveChanges();
				TempData["Del"] = "ok";
			}

			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult Index(string city)
		{
		

			var hotels = _context.hotel.Where(h => h.City.Equals(city));
		
			return View(hotels);
		}
		[Authorize]
		public IActionResult Index()
        {
			var hotel = _context.hotel.ToList();

			return View(hotel);
			
		}
		public IActionResult Rooms()
		{

			var hotel = _context.hotel.ToList();
			ViewBag.hotel = hotel;
			var rooms = _context.rooms.ToList();
			
			return View(rooms);

		}
		public IActionResult RoomDetails()
		{

			var hotel = _context.hotel.ToList();
			ViewBag.hotel = hotel;
			var rooms = _context.rooms.ToList();
			ViewBag.rooms = rooms;
			var roomDetails = _context.roomDetails.ToList();
			return View(roomDetails);

		}
		public IActionResult CreateNewRoomDetails(RoomDetails roomDetails)
		{
			
			_context.roomDetails.Add(roomDetails);
			_context.SaveChanges();
			return RedirectToAction("RoomDetails");
		}
		public IActionResult CreateNewRooms(Rooms rooms)
		{
			_context.rooms.Add(rooms);
			_context.SaveChanges();
			return RedirectToAction("Rooms");
		}
		public IActionResult CreateNewHotel(Hotel hotels)
        {
			if (ModelState.IsValid)
			{
				_context.hotel.Add(hotels);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			var hotel = _context.hotel.ToList();
			return View ("Index", hotel);

		}
    }
}
