using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS_QLKS.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        [Required]
        public string CustomerEmail { get; set; }

        public int PhongId { get; set; }
        [ForeignKey("PhongId")]
        [ValidateNever]
        public Phong Phong { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Giá không thể là số âm.")]
        public double RoomPrice { get; set; }
        [Required]
        public int BookingStatus { get; set; }
        //public bool IsDeleted { get; set; } // Thêm cột IsDeleted
        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public List<ServiceOrder> ServiceOrders { get; set; }
        
        public double ServicePrice { get; set; }
        
        public double TotalPrice { get; set; }
    }
}
