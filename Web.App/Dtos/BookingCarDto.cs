using Web.App.Entities;

namespace Web.App.Dtos
{
    public record BookingCarDto
    {
        public DateTime StartDateTime { get; set; }

        public RepeatOption RepeatOption { get; set; } = 0;

        public DaysOfWeek? DaysToRepeatOn { get; set; } = 0;

        public DateTime? EndDateTime { get; set; }
        public Car? Car { get; set; }

        public static BookingCarDto ToDto(Booking booking)
        {
            return new BookingCarDto
            {
                StartDateTime = booking.StartDateTime,
                RepeatOption = booking.RepeatOption,
                DaysToRepeatOn = booking.DaysToRepeatOn,
                EndDateTime = booking.EndDateTime,
                Car = booking.Car
            };
        }

        public static List<BookingCarDto> ToDots(List<Booking> bookings)
        {
            var dots = new List<BookingCarDto>(bookings.Count);
            foreach (var t in bookings) dots.Add(ToDto(t));
            return dots;
        }


        public override string ToString()
        {
            string daysOfWeekString = DaysToRepeatOn.HasValue ? DaysToRepeatOn.Value.ToString() : "None";

            return $$"""
                     BookingCalendarDto {
                         StartDateTime: {{StartDateTime}},
                         RepeatOption: {{RepeatOption}},
                         DaysToRepeatOn: {{daysOfWeekString}},
                         EndDateTime: {{EndDateTime}},
                         {{Car}}
                     }
                     """;
        }
    }
}