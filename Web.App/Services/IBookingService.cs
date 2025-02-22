using Web.App.Dtos;
using Web.App.Entities;

namespace Web.App.Services;

public interface IBookingService
{
    Task<List<BookingCarDto>> GetCalendarBookings(Guid? bookingId, Guid? carId, DateTime? from, DateTime? to);
    Task<ReturnBookingDto> CreateBooking(CreateBookingDto bookingDto);
    Task<IEnumerable<BookingCarDto>> GetSeedData();
}