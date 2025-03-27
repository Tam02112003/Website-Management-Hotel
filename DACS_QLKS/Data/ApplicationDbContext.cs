using DACS_QLKS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DACS_QLKS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LoaiPhong> LoaiPhongs { get; set; }

        public DbSet<Phong> phongs { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }

        public DbSet<Booking> bookings { get; set; }
         
        public DbSet<ServiceOrder> serviceOrders { get; set; }

        public DbSet<TinTuc> TinTucs { get; set; }


    }
}