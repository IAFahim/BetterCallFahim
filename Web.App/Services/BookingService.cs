using Web.App.Dtos;
using Web.App.Entities;

namespace Web.App.Services;

public class BookingService(AppDbContext context) : IBookingService
{
    public async Task<List<BookingCarDto>> GetCalendarBookings(Guid? carId, DateOnly? bookingDate)
    {
        
        return new List<BookingCarDto>();
    }

    public async Task<CreateUpdateBookingDto> CreateBooking(CreateUpdateBookingDto bookingDto)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BookingCarDto>> GetSeedData()
    {
        
        return new List<BookingCarDto>();
    }

    #region Sample Data

    private IList<Car> GetCars()
    {
        var cars = new List<Car>
        {
            new Car { Id = Guid.NewGuid(), Make = "Toyota", Model = "Corolla" },
            new Car { Id = Guid.NewGuid(), Make = "Honda", Model = "Civic" },
            new Car { Id = Guid.NewGuid(), Make = "Ford", Model = "Focus" }
        };

        return cars;
    }
    

    #endregion
}