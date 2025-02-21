using Web.App.Dtos;

namespace Web.App.Services;

public interface IBookingService
{
    Task<List<BookingCarDto>> GetCalendarBookings(Guid? bookingId, Guid? carId, DateTime? from, DateTime? to);
    Task<CreateUpdateBookingDto> CreateBooking(CreateUpdateBookingDto bookingDto);
    Task<IEnumerable<BookingCarDto>> GetSeedData();
}