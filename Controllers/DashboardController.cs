using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;

namespace Hotels.Controllers
{
	//izomlevtaplocitb
	public class DashboardController : Controller
    {
		private readonly AppDbContext _context;
        public DashboardController(AppDbContext context)
        {
            _context = context;

		}
		public async Task<string> SendEmail()
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Test Massage","Soomm831@gmail.com"));
			message.To.Add(MailboxAddress.Parse("SamiaAlammari1@gmail.com"));
			message.Subject = "Test email from My ASP.net Project";
			message.Body = new TextPart("Plain")
			{
				Text = "Wellcome"
			};
			using(var client=new SmtpClient())
			{
				try
				{
					client.Connect("smtp.gmail.com", 587);
					client.Authenticate("Soomm831@gmail.com", "izomlevtaplocitb");
					await client.SendAsync(message);
					client.Disconnect(true);
				}
				catch(Exception e)
				{
					return e.Message.ToString();
				}
			}
			return "Ok";
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
		public IActionResult DeleteRooms(int id)
		{
			var RoomsDelete = _context.rooms.SingleOrDefault(x => x.Id == id);
			if (RoomsDelete != null)
			{
				_context.rooms.Remove(RoomsDelete);
				_context.SaveChanges();
				TempData["Del"] = "ok";
			}

			return RedirectToAction("Rooms");
		}
		public IActionResult DeleteRoomDetails(int id)
		{
			var RoomDetailsDelete = _context.roomDetails.SingleOrDefault(x => x.Id == id);
			if (RoomDetailsDelete != null)
			{
				_context.roomDetails.Remove(RoomDetailsDelete);
				_context.SaveChanges();
				TempData["Del"] = "ok";
			}

			return RedirectToAction("RoomDetails");
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
			var currentuser = HttpContext.User.Identity.Name;
			ViewBag.currentuser = currentuser;
			//CookieOptions option = new CookieOptions();
			//option.Expires = DateTime.Now.AddMinutes(20);
			//Response.Cookies.Append("UserName", currentuser, option);
			HttpContext.Session.SetString("UserName", currentuser);
			var hotel = _context.hotel.ToList();
			return View(hotel);
			
		}
		public IActionResult Rooms()
		{

			var hotel = _context.hotel.ToList();
			ViewBag.hotel = hotel;
			//ViewBag.currentuser = Request.Cookies["UserName"];
			ViewBag.currentuser = HttpContext.Session.GetString("UserName");
			var rooms = _context.rooms.ToList();
			return View(rooms);

		}
		[HttpPost]
		public IActionResult Rooms(int hotel)
		{

			var Rooms = _context.rooms.Where(h => h.IdHotel.Equals(hotel));
			return View(Rooms);
		}
		public IActionResult RoomDetails()
		{

			var hotel = _context.hotel.ToList();
			ViewBag.hotel = hotel;
			//ViewBag.currentuser = Request.Cookies["UserName"];
			var rooms = _context.rooms.ToList();
			ViewBag.rooms = rooms;
			var roomDetails = _context.roomDetails.ToList();
			ViewBag.currentuser = HttpContext.Session.GetString("UserName");
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
