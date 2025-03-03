namespace CarBooking.Dtos
{
    public class BookingCalenderDto
    {
        public DateOnly startBookingDate { get; set; }
        public DateOnly endBookingDate { get; set; }
        public required string CarModel { get; set; }
        public required string CarId { get; set; }
    }
}
