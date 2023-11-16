using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hotels.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly AppDbContext _context;
        public ShoppingController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var hotel = _context.hotel.ToList();
            return View(hotel);
        }
        public IActionResult Rooms (int id)
        {
            var rooms = _context.rooms.Where(p=>p.IdHotel==id).ToList();
            return View(rooms);
        }
        public IActionResult Invoice(int id)
        {
            Decimal Tax = 15 / 100;
            var rooms = _context.rooms.SingleOrDefault(p => p.Id == id);
            var Invoice = new Invoice()
            {
                RoomNo = rooms.Id,
                IdHotel = rooms.IdHotel,
                IdRoomDetails = rooms.Id,
                Price = rooms.Price,
                Total = rooms.Price * 1,
                Discount = 0,
                Tax=(15/100),
                Net=Tax* rooms.Price*1,
                DateFrom = DateTime.Now.Date,
                DateInvoice= DateTime.Now.Date,
                DateTo= DateTime.Now.Date,
                UserId=1
            };
            _context.invoices.Add(Invoice);
            _context.SaveChanges();

            return View(Invoice);
        }
    }
}
