using DACS_QLKS.Data;
using DACS_QLKS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACS_QLKS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class TaiKhoanController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TaiKhoanController(ApplicationDbContext db)
        {

            _db = db;
        }
        public IActionResult Index()
        {
            var users = _db.Users.OfType<ApplicationUser>().ToList();

            return View(users);
        }
    
}
}
