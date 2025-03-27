using DACS_QLKS.Data;
using DACS_QLKS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DACS_QLKS.Repository
{
    public class PhongRepository: IPhongRepository
    {
        private readonly ApplicationDbContext _db;
        public PhongRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Delete(int id)
        {
            var theloai = _db.phongs.Find(id);
            if (theloai != null)
            {
                _db.Remove(theloai);
            }

        }
        public IEnumerable<Phong> GetAll()
        {
            IEnumerable<Phong> Phong = _db.phongs.Include(p => p.loaiPhong).ToList();
            return Phong;
        }
        public IEnumerable<SelectListItem> GetListLoaiPhong()
        {

            IEnumerable<SelectListItem> dsloaiphong = _db.LoaiPhongs.Select(
                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.TypeName
                });
            return dsloaiphong;
        }
        public Phong GetByID(int id)
        {
            var phong = _db.phongs.Find(id);
            return phong;
        }

        public void Add(Phong phong)
        {
            _db.phongs.Add(phong);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(Phong phong)
        {
            _db.phongs.Update(phong);
        }
    }
}
