
using System.ComponentModel.DataAnnotations;

namespace CarBooking.Model.Entities

{
    public class Car
    {
        [Key]
        public required string CarId { get; set; }
        public required string CarCompany { get; set; }
        public required string CarModel { get; set; }
        public int? Year { get; set; }

        // Navigation Property
        public ICollection<Booking>? Bookings { get; set; }
    }
}
