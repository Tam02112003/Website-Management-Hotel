using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS_QLKS.Models
{
    public class DichVu
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên dịch vụ là bắt buộc")]
        public string ServiceName { get; set; }
        
        public string? Description { get; set; }
        [Required(ErrorMessage = "Giá là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số dương")]
        public double Price { get; set; }
        
        public string? ImgUrl { get; set;}
       
    }
}
