using DACS_QLKS.Data;
using DACS_QLKS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACS_QLKS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DuyetBookingController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DuyetBookingController(ApplicationDbContext db)
        {

            _db = db;
        }
        // action để hiển thị danh sách các đơn đặt phòng chưa được duyệt
        public IActionResult Index()
        {
            
            IEnumerable<Booking> booking = _db.bookings.Include(x => x.ApplicationUser).Include(x => x.Phong).Where(b=>b.BookingStatus==-1).ToList();
            return View(booking);
        }
        // Controller action để duyệt đơn đặt phòng
        [HttpPost]
        public ActionResult ApproveBooking(int bookingId)
        {
            var booking = _db.bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking != null)
            {
                booking.BookingStatus = 1;
                _db.SaveChanges();
                return RedirectToAction("Index","DuyetBooking");
            }
            return NotFound();
        }

        // Controller action để từ chối đơn đặt phòng
        [HttpPost]
        public ActionResult RejectBooking(int bookingId)
        {
            var booking = _db.bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking != null)
            {
                booking.BookingStatus = 0;
                _db.SaveChanges();
                return RedirectToAction("Index","DuyetBooking");
            }
            return NotFound();
        }

    }
}
