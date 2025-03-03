namespace CarBooking.Dtos
{
    public class BookingFilterDto
    {
        public required string CarId { get; set; }
        public DateOnly startBookingDate { get; set; }
        public DateOnly endBookingDate { get; set; }
    }
}
