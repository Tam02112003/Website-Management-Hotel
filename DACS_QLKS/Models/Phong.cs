using DACS_QLKS.Data.Migrations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS_QLKS.Models
{
    public class Phong
    {
        [Key] public int Id { get; set; }
        [Required(ErrorMessage = "Tên phòng là bắt buộc")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Giá phòng là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá không thể là số âm.")]
        public double Price { get; set; }
        
        public string? Description { get; set; }
        public string? Size { get; set; }
       
        public string? ImgUrl { get; set; }="/path/to/default-image.jpg";
        [Required(ErrorMessage = "Số người ở phòng là bắt buộc.")]
        [Range(1, double.MaxValue, ErrorMessage = "Số người phải từ 1 trở lên.")]
        public int MaxOccupancy { get; set; }
        [Required] public int LoaiPhongId { get; set; }
        [ForeignKey("LoaiPhongId")]
        [ValidateNever]
        public LoaiPhong loaiPhong { get; set; }
       
    }
}
