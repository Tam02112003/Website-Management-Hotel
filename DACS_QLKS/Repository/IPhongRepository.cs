using DACS_QLKS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DACS_QLKS.Repository
{
    public interface IPhongRepository
    {
        IEnumerable <Phong> GetAll();
        Phong GetByID(int id);
        void Add(Phong phong);
        void Delete(int id);
        IEnumerable<SelectListItem> GetListLoaiPhong();
        void Save();
        void Update(Phong phong);
    }
}
