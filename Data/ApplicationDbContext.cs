using CarBooking.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Car)
                .WithMany(c => c.Bookings)  // A car can have many bookings
                .HasForeignKey(b => b.CarId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
