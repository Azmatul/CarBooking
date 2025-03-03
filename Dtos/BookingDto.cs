using CarBooking.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarBooking.Dtos
{
    public class BookingDto
    {
        public required int Id { get; set; }

        [Required]
        public DateOnly startBookingDate { get; set; }
        
        [Required]
        public DateOnly endBookingDate { get; set; }

        [Required]
        //Enum: DoesNotRepeat, Daily, Weekly
        public RepeatOption RepeatOption { get; set; }

        public DateOnly? EndRepeatDate { get; set; }

        //Enum: None,Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday
        public DaysOfWeek? DaysToRepeatOn { get; set; }

        public DateTime RequestedOn { get; set; }

        public required string CarId { get; set; }

    }
}
