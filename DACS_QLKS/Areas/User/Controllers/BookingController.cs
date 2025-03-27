using DACS_QLKS.Data;
using DACS_QLKS.Data.Migrations;
using DACS_QLKS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DACS_QLKS.Areas.User.Controllers
{
    [Area("User")]
    public class BookingController: Controller
    {
        private readonly ApplicationDbContext _db;

        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Authorize]
        public IActionResult MyBookings()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            // Lấy tất cả các đơn đặt phòng của người dùng
            var bookings = _db.bookings
                              .Include(b => b.Phong)
                              .Where(b => b.ApplicationUserId == userId)
                              .ToList();

            return View(bookings);
        }
    

    }
}
