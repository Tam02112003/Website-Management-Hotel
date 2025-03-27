using DACS_QLKS.Data;
using DACS_QLKS.Data.Migrations;
using DACS_QLKS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using TinTuc = DACS_QLKS.Models.TinTuc;

namespace DACS_QLKS.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db )
        {
            _logger = logger;
            _db = db;
           
        }

        public IActionResult Index()
        {
            IEnumerable<Phong> phong = _db.phongs.Include(x => x.loaiPhong).ToList();
            return View(phong);
        }
        
		public IActionResult rooms()
		{
            IEnumerable<Phong> phong = _db.phongs.Include(x=>x.loaiPhong).ToList();
            return View(phong);
            
		}
	
		public IActionResult about()
		{
			return View();
		}
        public IActionResult news()
        {
            IEnumerable<TinTuc> tintuc=_db.TinTucs.ToList();
            return View(tintuc);
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

        public IActionResult Details(int phongid)
        {
            Booking booking = new Booking()
            {
                PhongId = phongid,
                Phong = _db.phongs.Include("loaiPhong").FirstOrDefault(x => x.Id == phongid),

            };
            return View(booking);
        }
        [HttpGet]
        [Authorize]
        public IActionResult booknow(int phongid)
        {
            Booking booking = new Booking()
                {
                    PhongId = phongid,
                    Phong = _db.phongs.Include("loaiPhong").FirstOrDefault(x => x.Id == phongid),
                    CheckInDate = DateTime.Today, // Thêm ngày check-in mặc định (ví dụ: hôm nay)
                    CheckOutDate = DateTime.Today.AddDays(1), // Thêm ngày check-out mặc định (ví dụ: ngày mai)
                    BookingStatus = -1 // Thêm trạng thái booking mặc định
                };
                return View(booking);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult booknow(Booking booking)
        {
          
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            booking.ApplicationUserId = claim.Value;
            booking.BookingStatus = -1;
            if (booking.CheckInDate < DateTime.Today)
            {
                ModelState.AddModelError(string.Empty, "Ngày đến không thể là ngày đã qua.");
                // Xử lý khi ngày đến đã qua
                var referrerUrl = Request.Headers["Referer"].ToString();
                return Redirect(referrerUrl); // Chuyển hướng về trang trước đó

            }
            if (booking.CheckInDate >= booking.CheckOutDate)
            {
                //// Xử lý khi ngày đến không hợp lệ
                var referrerUrl = Request.Headers["Referer"].ToString();
                return Redirect(referrerUrl); // Chuyển hướng về trang trước đó

            }
            // Kiểm tra tính khả dụng của phòng trong khoảng thời gian đã chọn
            var existingBooking = _db.bookings.FirstOrDefault(b =>
                b.PhongId == booking.PhongId &&
                ((booking.CheckInDate >= b.CheckInDate && booking.CheckInDate < b.CheckOutDate) ||
                (booking.CheckOutDate > b.CheckInDate && booking.CheckOutDate <= b.CheckOutDate) ||
                (booking.CheckInDate <= b.CheckInDate && booking.CheckOutDate >= b.CheckOutDate)) && b.BookingStatus != 0
            );

            if (existingBooking != null)
            {
                // Phòng đã được đặt trong khoảng thời gian đã chọn, xử lý logic phù hợp (ví dụ: hiển thị thông báo cho người dùng)

                var referrerUrl = Request.Headers["Referer"].ToString();
                return Redirect(referrerUrl); // Chuyển hướng về trang trước đó

            }
            // Tính số ngày đặt phòng
            int numberOfDays = (int)(booking.CheckOutDate - booking.CheckInDate).TotalDays;
            // Lấy thông tin về phòng để tính tổng tiền
            var room = _db.phongs.FirstOrDefault(p => p.Id == booking.PhongId);
            if (room == null)
            {
                // Xử lý khi không tìm thấy thông tin về phòng
                return NotFound();
            }
            //else
            //{
            //    room.Availability = "Non-available";
            //    _db.SaveChanges();
            //}
            booking.RoomPrice = (numberOfDays * room.Price);
            booking.TotalPrice = booking.RoomPrice;
            _db.bookings.Add(booking);
            _db.SaveChanges();
            return RedirectToAction("BookDetails", new { bookingId = booking.Id });
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
            /*    IEnumerable<Booking> CTHD = _db.bookings.Include("Booking").Include("Phong").Where(x =>x.Id == bookingId).ToList();*/
            if (booking == null)
            {
                // Xử lý khi không tìm thấy thông tin đặt phòng
                return NotFound();
            }
            double totalServicePrice = booking.ServiceOrders.Sum(so => (so.Quantity * so.dichVu.Price));
            double totalPrice = booking.RoomPrice + totalServicePrice;

            booking.ServicePrice= totalServicePrice;
            booking.TotalPrice = totalPrice;
            return View(booking);
            
        }

        [HttpPost]
        public ActionResult CheckRoomAvailability(DateTime checkInDate, DateTime checkOutDate, int phongId)
        {
            var existingBooking = _db.bookings.FirstOrDefault(b =>
                b.PhongId == phongId &&
                ((checkInDate >= b.CheckInDate && checkInDate < b.CheckOutDate) ||
                (checkOutDate > b.CheckInDate && checkOutDate <= b.CheckOutDate) ||
                (checkInDate <= b.CheckInDate && checkOutDate >= b.CheckOutDate)) && b.BookingStatus!=0
            );

            if (existingBooking != null)
            {
                return Json(new { available = false });
            }
            else
            {
                return Json(new { available = true });
            }
        }
   

        //Tìm kiếm phòng --- TD
        public IActionResult TimKiem(string lp)
        {
            var listPhong=_db.phongs.Include(t=>t.loaiPhong).ToList();
            ViewBag.SanPhamTimKiem = listPhong;
            if (!String.IsNullOrEmpty(lp))
            {
                var phong = _db.LoaiPhongs.FirstOrDefault(s => s.TypeName == lp);
                listPhong= _db.phongs.Where(t => t.LoaiPhongId == phong.Id).ToList();
                ViewBag.SanPhamTimKiem=listPhong;
            }
                return View();
        }
        

    


    }
}