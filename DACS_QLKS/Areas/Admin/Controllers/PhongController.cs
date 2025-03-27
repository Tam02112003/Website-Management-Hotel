using DACS_QLKS.Data;
using DACS_QLKS.Models;
using DACS_QLKS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DACS_QLKS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class PhongController : Controller
    {
        private readonly IPhongRepository _phongRepository;
        private readonly ApplicationDbContext _db;
        public PhongController(IPhongRepository phongRepository, ApplicationDbContext db)
        {
            _phongRepository = phongRepository;
            _db = db;   
        }
        public IActionResult Index()
        {
            IEnumerable<Phong> phong = _db.phongs.Include(p => p.loaiPhong).ToList();
            return View(phong);
        }
        [HttpGet]
        public IActionResult Upsert(int id)
        {
            Phong phong = new Phong();
            IEnumerable<SelectListItem> dstheloai = _phongRepository.GetListLoaiPhong();
            ViewBag.DSTheLoai = dstheloai;
            if (id == 0)

                return View(phong);
            else
            {
                phong = _db.phongs.Find(id); 
                return View(phong);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Phong phong, IFormFile ImgUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImgUrl != null)
                {
                    // Lưu hình ảnh và gán đường dẫn cho dv.ImgUrl
                    phong.ImgUrl = SaveImage(ImgUrl);
                }
                
               
                if (phong.Id == 0)
                {
                    _db.phongs.Add(phong);
                }
                else
                {
                    _db.phongs.Update(phong);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            IEnumerable<SelectListItem> dstheloai = _phongRepository.GetListLoaiPhong();
            ViewBag.DSTheLoai = dstheloai;
            return View(phong);
        }
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
            var phong = _db.phongs.FirstOrDefault(sp => sp.Id == id);
            if (phong == null)
            {
                return NotFound();
            }


            _db.phongs.Remove(phong);
            _db.SaveChanges();

            return Json(new { success = true });
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
    }
}
