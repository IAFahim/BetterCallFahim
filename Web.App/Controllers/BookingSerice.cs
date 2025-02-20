using Microsoft.AspNetCore.Mvc;
using Web.App.Dtos;
using Web.App.Services;

namespace Web.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController(IBookingService bookingService) : ControllerBase
    {
        // GET: api/Bookings
        [HttpGet("Booking")]
        [ProducesResponseType(typeof(List<BookingCarDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BookingCarDto>>> GetCalendarBookings(
            [FromQuery] Guid? carId,[FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate
        )
        {
            var bookings = await bookingService.GetCalendarBookings(carId, startDate);
            return Ok(bookings);
        }


        // POST: api/Bookings
        [HttpPost("Booking")]
        [ProducesResponseType(typeof(CreateUpdateBookingDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateUpdateBookingDto>> PostBooking(CreateUpdateBookingDto bookingDto)
        {
            var booking = await bookingService.CreateBooking(bookingDto);
            return CreatedAtAction(nameof(GetCalendarBookings), new { id = booking.Id }, booking);
        }

        // GET: api/SeedData
        // For test purpose
        [HttpGet("SeedData")]
        public async Task<ActionResult<IEnumerable<BookingCarDto>>> GetSeedData()
        {
            var seedData = await bookingService.GetSeedData();
            return Ok(seedData);
        }
    }
}