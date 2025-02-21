using Microsoft.EntityFrameworkCore;
using Web.App.Dtos;
using Web.App.Entities;

namespace Web.App.Services;

public class BookingService(AppDbContext context) : IBookingService
{
    public async Task<List<BookingCarDto>> GetCalendarBookings(Guid? bookingId, Guid? carId, DateTime? from,
        DateTime? to)
    {
        var query = context.Bookings.Include(b => b.Car).AsQueryable();
        if (bookingId.HasValue) query = query.Where(booking => booking.Id == bookingId);
        if (carId.HasValue) query = query.Where(booking => booking.CarId == carId);
        if (from.HasValue) query = query.Where(booking => from <= booking.StartDateTime);
        if (to.HasValue) query = query.Where(booking => booking.EndDateTime <= to);
        var bookings = await query.ToListAsync();
        return bookings.Select(BookingToDto).ToList();
    }

    private static BookingCarDto BookingToDto(Booking booking)
    {
        var bookingCarDto = new BookingCarDto
        {
            Id = booking.Id,
            StartDateTime = booking.StartDateTime,
            EndDateTime = booking.EndDateTime,
            Car = booking.Car,
            BookedDates = new List<DateOnly>()
        };
        return bookingCarDto;
    }

    public async Task<CreateUpdateBookingDto> CreateBooking(CreateUpdateBookingDto bookingDto)
    {
        return new CreateUpdateBookingDto();
    }

    public async Task<IEnumerable<BookingCarDto>> GetSeedData()
    {
        await PopulateList(context, SeedCars);
        await PopulateList(context, SeedBooking);
        return new List<BookingCarDto>();
    }

    private async Task<List<T>> PopulateList<T>(DbContext dbContext, IEnumerable<T> defaultList) where T : class
    {
        var list = await dbContext.Set<T>().ToListAsync();
        if (list.Count != 0) return list;
        await dbContext.Set<T>().AddRangeAsync(defaultList);
        await dbContext.SaveChangesAsync();
        return list;
    }

    #region Sample Data

    private static IEnumerable<Car> SeedCars =>
    [
        new() { Id = Guid.NewGuid(), Make = "Toyota", Model = "Corolla" },
        new() { Id = Guid.NewGuid(), Make = "Honda", Model = "Civic" },
        new() { Id = Guid.NewGuid(), Make = "Ford", Model = "Focus" }
    ];

    private static IEnumerable<Booking> SeedBooking
    {
        get
        {
            var cars = SeedCars.ToArray();
            var bookings = new List<Booking>
            {
                Booking.CreateNow(
                    new DateTime(2025, 2, 5, 10, 0, 0), RepeatOption.DoesNotRepeat, null,
                    new DateTime(2025, 2, 5, 10, 0, 0).AddDays(1).AddHours(2), cars[0]
                ),
                Booking.CreateNow(
                    new DateTime(2025, 2, 10, 14, 0, 0), RepeatOption.Daily, null,
                    new DateTime(2025, 2, 10, 14, 0, 0).AddDays(10).AddHours(2), cars[1]
                ),
                Booking.CreateNow(
                    new DateTime(2025, 2, 15, 9, 0, 0), RepeatOption.Weekly, DaysOfWeek.Monday,
                    new DateTime(2025, 2, 15, 9, 0, 0).AddDays(16).AddHours(2), cars[2]
                ),
                Booking.CreateNow(
                    new DateTime(2025, 3, 1, 11, 0, 0), RepeatOption.DoesNotRepeat, null,
                    new DateTime(2025, 3, 1, 11, 0, 0).AddDays(1).AddHours(2), cars[0]
                ),
                Booking.CreateNow(
                    new DateTime(2025, 3, 7, 8, 0, 0), RepeatOption.Weekly, DaysOfWeek.Friday,
                    new DateTime(2025, 3, 7, 8, 0, 0).AddDays(21).AddHours(2), cars[1]
                ),
                Booking.CreateNow(
                    new DateTime(2025, 3, 15, 15, 0, 0), RepeatOption.Daily, null,
                    new DateTime(2025, 3, 15, 15, 0, 0).AddDays(5).AddHours(2), cars[2]
                )
            };
            return bookings;
        }
    }

    #endregion
}