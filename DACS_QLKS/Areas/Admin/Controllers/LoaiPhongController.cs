using DACS_QLKS.Data;
using DACS_QLKS.Models;
using DACS_QLKS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DACS_QLKS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class LoaiPhongController : Controller
    {
        private readonly ILoaiPhongRepository _loaiPhongRepository;
        private readonly ApplicationDbContext _db;
        public LoaiPhongController(ILoaiPhongRepository loaiPhongRepository, ApplicationDbContext db)
        {
            _loaiPhongRepository = loaiPhongRepository;
            _db = db;
        }
        public IActionResult Index()
        {
            var loaiPhong = _loaiPhongRepository.GetAll();
            ViewBag.LoaiPhong = loaiPhong;

            return View();
        }

   
      

        public IActionResult Delete(int id)
        {
            var loaiPhong = _db.LoaiPhongs.FirstOrDefault(sp => sp.Id == id);
            if (loaiPhong == null)
            {
                return NotFound();
            }


            _db.LoaiPhongs.Remove(loaiPhong);
            _db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Upsert(int id)
        {
          
                LoaiPhong loaiPhong = new LoaiPhong();
                if (id == 0)
                    return View(loaiPhong);
                else
                {
                    loaiPhong = _db.LoaiPhongs.Find(id);
                    return View(loaiPhong);
                }
            
            
        }
        [HttpPost]
        public IActionResult Upsert(LoaiPhong lp)
        {
            if (ModelState.IsValid)
            {
                if (lp.Id == 0)
                {
                    _db.LoaiPhongs.Add(lp);
                }
                else
                {
                    _db.LoaiPhongs.Update(lp);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
