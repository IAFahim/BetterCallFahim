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
            [FromQuery] Guid? id, [FromQuery] Guid? carId,
            [FromQuery] DateTime? from, [FromQuery] DateTime? to
        )
        {
            var bookings = await bookingService.GetCalendarBookings(id, carId, from, to);
            return Ok(bookings);
        }


        // POST: api/Bookings
        [HttpPost("Booking")]
        [ProducesResponseType(typeof(List<BookingCarDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ReturnBookingDto>> PostBooking(CreateBookingDto bookingDto)
        {
            var returnBookingDto = await bookingService.CreateBooking(bookingDto);
            return CreatedAtAction(nameof(GetCalendarBookings), new { id = returnBookingDto.Id }, returnBookingDto);
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