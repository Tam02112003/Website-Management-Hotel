using DACS_QLKS.Data;
using DACS_QLKS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACS_QLKS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DSBookingController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DSBookingController(ApplicationDbContext db)
        {

            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Booking> booking = _db.bookings
                .Include(x => x.ApplicationUser)
                .Include(x => x.Phong)
                .Include(b => b.ServiceOrders)
                    .ThenInclude(so => so.dichVu)
                .OrderByDescending(b => b.Id)
                .ToList();

            return View(booking);
        }



        [HttpGet]
        public IActionResult BookDetails(int bookingId)
        {
            // Lấy thông tin đặt phòng từ cơ sở dữ liệu
            var booking = _db.bookings
                            .Include(x => x.Phong)
                            .Include(b => b.ServiceOrders)
                                .ThenInclude(so => so.dichVu)
                            .FirstOrDefault(b => b.Id == bookingId);
            
            if (booking == null)
            {
                // Xử lý khi không tìm thấy thông tin đặt phòng
                return NotFound();
            }

            double totalServicePrice = booking.ServiceOrders.Sum(so => (so.Quantity * so.dichVu.Price));
            double totalPrice = booking.RoomPrice + totalServicePrice;

            booking.ServicePrice = totalServicePrice;
            booking.TotalPrice = totalPrice;
            return View(booking);
        }
       
    }
}