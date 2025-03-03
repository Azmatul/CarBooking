using CarBooking.Data;
using CarBooking.Dtos;
using CarBooking.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBookingController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CarBookingController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await context.Bookings.ToListAsync();

            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] BookingDto bookingDto)
        {
            if (bookingDto == null)
            {
                return BadRequest("Invalid booking data.");
            }

            //check for duplicate bookings
            bool isDuplicate = await context.Bookings.AnyAsync(b =>
                b.CarId == bookingDto.CarId &&
                ((b.startBookingDate < bookingDto.endBookingDate && b.endBookingDate > bookingDto.startBookingDate))
            );

            if (isDuplicate)
            {
                return Conflict("This car is already booked for the selected time slot.");
            }

            int newId = await context.Bookings.AnyAsync() ? await context.Bookings.MaxAsync(b => b.Id) + 1 : 1;

            var booking = new Booking
            {
                Id = newId,
                startBookingDate = bookingDto.startBookingDate,
                endBookingDate = bookingDto.endBookingDate,
                RepeatOption = bookingDto.RepeatOption,
                EndRepeatDate = bookingDto.EndRepeatDate,
                DaysToRepeatOn = bookingDto.DaysToRepeatOn,
                RequestedOn = DateTime.UtcNow,
                CarId = bookingDto.CarId
            };

            context.Bookings.Add(booking);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingDto bookingDto)
        {
            if (bookingDto == null || id <= 0)
            {
                return BadRequest("Invalid booking data.");
            }

            var existingBooking = await context.Bookings.FindAsync(id);
            if (existingBooking == null)
            {
                return NotFound($"Booking with ID {id} not found.");
            }

            //check for duplicate booking
            bool isDuplicate = await context.Bookings.AnyAsync(b =>
               b.CarId == bookingDto.CarId &&
               ((b.startBookingDate < bookingDto.endBookingDate && b.endBookingDate > bookingDto.startBookingDate))
           );

            if (isDuplicate)
            {
                return Conflict("This car is already booked for the selected time slot.");
            }

            existingBooking.startBookingDate = bookingDto.startBookingDate;
            existingBooking.endBookingDate = bookingDto.endBookingDate;
            existingBooking.RepeatOption = bookingDto.RepeatOption;
            existingBooking.EndRepeatDate = bookingDto.EndRepeatDate;
            existingBooking.DaysToRepeatOn = bookingDto.DaysToRepeatOn;
            existingBooking.RequestedOn = DateTime.UtcNow;
            existingBooking.CarId = bookingDto.CarId;

            await context.SaveChangesAsync();

            return Ok(existingBooking);
        }



        //Filter by CarId, start Booking & End booking Date

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBookingById(int Id)
        {
            var book = await context.Bookings.FindAsync(Id);
            if (book == null) return NotFound();
            return Ok(book);
        }


        [HttpGet("FilterBycar/{carId}")]
        public async Task<IActionResult> GetBookingsByCarId(string carId)
        {
            if (string.IsNullOrEmpty(carId))
            {
                return BadRequest("CarId is required.");
            }

            var bookings = await context.Bookings
                .Where(b => b.CarId == carId)
                .ToListAsync();

            return Ok(bookings);
        }


        [HttpGet("FilterByStartBookingDate/{carId}")]
        public async Task<IActionResult> GetBookingsByCarId(string carId)
        {
            if (string.IsNullOrEmpty(carId))
            {
                return BadRequest("CarId is required.");
            }

            var bookings = await context.Bookings
                .Where(b => b.CarId == carId)
                .ToListAsync();

            return Ok(bookings);
        }
    }
}
