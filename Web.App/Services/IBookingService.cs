using Web.App.Dtos;

namespace Web.App.Services;

public interface IBookingService
{
    Task<List<BookingCarDto>> GetCalendarBookings(Guid? carId, DateOnly? start);
    Task<CreateUpdateBookingDto> CreateBooking(CreateUpdateBookingDto bookingDto);
    Task<IEnumerable<BookingCarDto>> GetSeedData();
}