using DACS_QLKS.Data;
using DACS_QLKS.Models;
using Microsoft.AspNetCore.Mvc;

namespace DACS_QLKS.ViewComponents
{
    public class LoaiPhongViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public LoaiPhongViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            IEnumerable<LoaiPhong> theloai = _db.LoaiPhongs.ToList();
            return View(theloai);
        }
    }
}
