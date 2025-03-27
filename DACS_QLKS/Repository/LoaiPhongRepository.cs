using DACS_QLKS.Data;
using DACS_QLKS.Models;

namespace DACS_QLKS.Repository
{
    public class LoaiPhongRepository: ILoaiPhongRepository
    {
        private readonly ApplicationDbContext _db;
        public LoaiPhongRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Delete(int id)
        {
            var theloai = _db.LoaiPhongs.Find(id);
            if (theloai != null)
            {
                _db.Remove(theloai);
            }

        }
        public IEnumerable<LoaiPhong> GetAll()
        {
            var loaiPhong = _db.LoaiPhongs.ToList();
            return loaiPhong;
        }

        public LoaiPhong GetByID(int id)
        {
            var loaiPhong = _db.LoaiPhongs.Find(id);
            return loaiPhong;
        }

        public void Add(LoaiPhong loaiPhong)
        {
            _db.LoaiPhongs.Add(loaiPhong);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(LoaiPhong loaiPhong)
        {
            _db.LoaiPhongs.Update(loaiPhong);
        }
    }
}
