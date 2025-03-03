namespace CarBooking.Dtos
{
    public class CarDto
    {
        public required string CarId { get; set; }
        public required string CarCompany { get; set; }
        public required string CarModel { get; set; }
        public int? Year { get; set; }
    }
}
