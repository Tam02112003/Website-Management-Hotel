using DACS_QLKS.Data;
using DACS_QLKS.Data.Migrations;
using DACS_QLKS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;

namespace DACS_QLKS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DichVuController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DichVuController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<DichVu> dichvu = _db.DichVus.ToList();
            return View(dichvu);
        }
        [HttpGet]
        public IActionResult Upsert(int id)
        {
            DichVu dichvu = new DichVu();
            if (id == 0)
                return View(dichvu);
            else
            {
                dichvu = _db.DichVus.Find(id);
                return View(dichvu);
            }
        }

        
        [HttpPost]
        public IActionResult Upsert(DichVu dv, IFormFile ImgUrl)
        {
            if (ModelState.IsValid)
            {
                
                if (ImgUrl != null)
                {
                    // Lưu hình ảnh và gán đường dẫn cho dv.ImgUrl
                    dv.ImgUrl = SaveImage(ImgUrl);
                }

                if (dv.Id == 0)
                {
                    // Nếu là đối tượng mới (Id = 0)
                    _db.DichVus.Add(dv);
                }
                else
                {
                    // Nếu là đối tượng cập nhật (Id khác 0)
                    _db.DichVus.Update(dv);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dv);
        }
        //Lưu File Hình ảnh
        private string SaveImage(IFormFile image)
        {
            
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            return "/images/" + image.FileName;
        }
        public IActionResult Delete(int id)
        {
            var dichvu = _db.DichVus.FirstOrDefault(sp => sp.Id == id);
            if (dichvu == null)
            {
                return NotFound();
            }


            _db.DichVus.Remove(dichvu);
            _db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult AddServicesToBooking(int bookingId)
        {
            var booking = _db.bookings.FirstOrDefault(b => b.Id == bookingId);
            var availableServices = _db.DichVus.ToList(); // Lấy danh sách các dịch vụ có sẵn

            var model = new BookingViewModel()
            {
                Booking = booking,
                AvailableServices = availableServices
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult AddServicesToBooking(int bookingId, Dictionary<int, int> selectedServiceQuantities)
        {
            var booking = _db.bookings
                     .Include(b => b.ServiceOrders) // Bao gồm ServiceOrders khi tải booking
                     .ThenInclude(so => so.dichVu)   // Bao gồm dichVu của từng ServiceOrder
                     .FirstOrDefault(b => b.Id == bookingId);
            if (booking == null)
            {
                return NotFound();
            }
            if (booking.ServiceOrders == null)
            {
                booking.ServiceOrders = new List<ServiceOrder>();
            }
            foreach (var dichvuId in selectedServiceQuantities.Keys)
            {
                var dichvu = _db.DichVus.FirstOrDefault(s => s.Id == dichvuId);
                if (dichvu != null)
                {
                    int quantity = selectedServiceQuantities[dichvuId];
                    booking.ServiceOrders.Add(new ServiceOrder
                    {
                        DichVuId = dichvuId, // Sử dụng ID của dịch vụ
                        dichVu = dichvu,     // Gán dịch vụ vào thuộc tính dichVu
                        Quantity = quantity,
                    });
                }
            }
           

            booking.ServicePrice = booking.ServiceOrders.Sum(so => (double)(so.Quantity * so.dichVu.Price));
            booking.TotalPrice = booking.RoomPrice + booking.ServicePrice;
            _db.SaveChanges();

            return RedirectToAction("BookDetails", new { bookingId = bookingId });
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

            //double totalServicePrice = booking.ServiceOrders.Sum(so => (so.Quantity * so.dichVu.Price));
            //double totalPrice = booking.RoomPrice + totalServicePrice;

            //booking.ServicePrice = totalServicePrice;
            //booking.TotalPrice = totalPrice;
            return View(booking);
        }



        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        [HttpPost]
        public IActionResult DeleteOrder(int id)
        {
            var serviceOrder = _db.serviceOrders.FirstOrDefault(so => so.Id == id);
            if (serviceOrder == null)
            {
                return NotFound();
            }

            _db.serviceOrders.Remove(serviceOrder);
            _db.SaveChanges();

            // Tính toán lại tổng giá tiền
            var bookingId = serviceOrder.BookingId;
            var totalServicePrice = _db.serviceOrders
                               .Where(so => so.BookingId == bookingId)
                               .Sum(so => so.Quantity * so.dichVu.Price);
           
           
            var booking = _db.bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            // Cập nhật tổng tiền dịch vụ và tổng tiền thanh toán
            booking.ServicePrice = totalServicePrice;
            booking.TotalPrice = booking.RoomPrice + totalServicePrice;
            _db.SaveChanges();
            return Json(new { success = true , totalServicePrice = totalServicePrice, totalPrice = booking.TotalPrice });
            
        }

      


    }

}
