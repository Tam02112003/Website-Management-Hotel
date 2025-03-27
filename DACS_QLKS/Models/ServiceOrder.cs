using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DACS_QLKS.Models
{
    public class ServiceOrder
    {
        [Key]
        public int Id { get; set; }
        public int DichVuId { get; set; }
        [ForeignKey("DichVuId")]
        [ValidateNever]
        public DichVu dichVu { get; set; }

        public int Quantity { get; set; }
        public int BookingId { get; set; }
        [ForeignKey("BookingId")]
        [ValidateNever]
        public Booking booking { get; set; }
        
    }
}
