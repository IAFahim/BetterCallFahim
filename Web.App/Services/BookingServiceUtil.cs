using Web.App.Dtos;
using Web.App.Entities;

namespace Web.App.Services;

public static class BookingServiceUtil
{
    public static Booking ToBooking(this CreateBookingDto createBookingDto)
    {
        return new Booking
        {
            Id = Guid.NewGuid(),
            RequestedOn = DateTime.UtcNow,
            StartDateTime = createBookingDto.StartDateTime,
            RepeatOption = createBookingDto.RepeatOption,
            DaysToRepeatOn = createBookingDto.DaysToRepeatOn,
            EndDateTime = createBookingDto.EndDateTime,
            CarId = createBookingDto.CarId
        };
    }
    
    public static ReturnBookingDto ToReturnBookingDto(this Booking booking)
    {
        return new ReturnBookingDto
        {
            Id = Guid.NewGuid(),
            RequestedOn = DateTime.UtcNow,
            StartDateTime = booking.StartDateTime,
            RepeatOption = booking.RepeatOption,
            DaysToRepeatOn = booking.DaysToRepeatOn,
            EndDateTime = booking.EndDateTime,
            CarId = booking.CarId
        };
    }
    public static BookingCarDto ToDto(this Booking booking, DateTime? from, DateTime? to)
    {
        return new BookingCarDto
        {
            Id = booking.Id,
            RequestedOn = booking.RequestedOn,
            StartDateTime = booking.StartDateTime,
            EndDateTime = booking.EndDateTime,
            Car = booking.Car,
            RenewDates = BuildRenewDates(booking, from, to)
        };
    }

    private static List<DateOnly> BuildRenewDates(Booking booking, DateTime? from, DateTime? to
    )
    {
        var start = from ?? booking.StartDateTime;
        var end = from ?? booking.EndDateTime;
        return booking.RepeatOption switch
        {
            RepeatOption.DoesNotRepeat => [],
            RepeatOption.Daily => ListDailyDays(start.AddDays(1), end),
            RepeatOption.Weekly => ListWeekDays(booking.DaysToRepeatOn!.Value, start.AddDays(1), end),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static List<DateOnly> ListDailyDays(DateTime star, DateTime end)
    {
        var days = new List<DateOnly>();
        var current = DateOnly.FromDateTime(star);
        var endDate = DateOnly.FromDateTime(end);

        while (current < endDate)
        {
            days.Add(current);
            current = current.AddDays(1);
        }

        return days;
    }

    private static List<DateOnly> ListWeekDays(DaysOfWeek daysToRepeatOn, DateTime start, DateTime end)
    {
        var days = new List<DateOnly>();
        var current = DateOnly.FromDateTime(start);
        var endDate = DateOnly.FromDateTime(end);

        while (current < endDate)
        {
            var flag = (DaysOfWeek)(1 << (int)current.DayOfWeek);
            if (daysToRepeatOn.HasFlag(flag)) days.Add(current);
            current = current.AddDays(1);
        }

        return days;
    }
}