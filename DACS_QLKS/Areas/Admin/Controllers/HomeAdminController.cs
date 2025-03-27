using DACS_QLKS.Areas.User.Controllers;
using DACS_QLKS.Data;
using DACS_QLKS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DACS_QLKS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class HomeAdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeAdminController( ApplicationDbContext db)
        {
            
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
