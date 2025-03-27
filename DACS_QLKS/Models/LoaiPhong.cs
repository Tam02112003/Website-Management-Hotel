using System.ComponentModel.DataAnnotations;

namespace DACS_QLKS.Models
{
    public class LoaiPhong
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Loại phòng là bắt buộc")]
        [Display(Name ="Thể Loại")]
        public string TypeName { get; set; }
        
        [Required(ErrorMessage = "Ngày giờ là bắt buộc")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
