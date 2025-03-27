using DACS_QLKS.Models;

namespace DACS_QLKS.Repository
{
    public interface ILoaiPhongRepository
    {
        IEnumerable<LoaiPhong> GetAll();
        LoaiPhong GetByID(int id);
        void Add(LoaiPhong loaiPhong);
        void Delete(int id);
        void Update(LoaiPhong loaiPhong);
        void Save();
    }
}
