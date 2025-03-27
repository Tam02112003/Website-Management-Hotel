using DACS_QLKS.Data;
using DACS_QLKS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DACS_QLKS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class TinTucController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TinTucController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<TinTuc> tintuc = _db.TinTucs.ToList();
            return View(tintuc);
        }

        [HttpGet]
        public IActionResult Upsert(int id)
        {
            TinTuc tintuc = new TinTuc();
            if (id == 0)
                return View(tintuc);
            else
            {
                tintuc = _db.TinTucs.Find(id);
                return View(tintuc);
            }
        }


        [HttpPost]
        public IActionResult Upsert(TinTuc tt, IFormFile ImgUrl)
        {
            if (ModelState.IsValid)
            {

                if (ImgUrl != null)
                {
                    // Lưu hình ảnh và gán đường dẫn cho dv.ImgUrl
                    tt.ImgUrl = SaveImage(ImgUrl);
                }

                if (tt.Id == 0)
                {
                    // Nếu là đối tượng mới (Id = 0)
                    _db.TinTucs.Add(tt);
                }
                else
                {
                    // Nếu là đối tượng cập nhật (Id khác 0)
                    _db.TinTucs.Update(tt);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tt);
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
            var tintuc = _db.TinTucs.FirstOrDefault(sp => sp.Id == id);
            if (tintuc == null)
            {
                return NotFound();
            }


            _db.TinTucs.Remove(tintuc);
            _db.SaveChanges();

            return Json(new { success = true });
        }
    }
}
