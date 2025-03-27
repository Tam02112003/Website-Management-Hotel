using System.ComponentModel.DataAnnotations;

namespace DACS_QLKS.Models
{
    public class TinTuc
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Chủ đề là bắt buộc")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        public string Title { get; set; }

        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
    }
}
