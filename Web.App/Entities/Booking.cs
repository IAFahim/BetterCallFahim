using System.ComponentModel.DataAnnotations;

namespace Web.App.Entities
{
    public class Booking
    {
        public static Booking CreateNow(DateTime startDateTime,
            RepeatOption repeatOption,
            DaysOfWeek? daysToRepeatOn,
            DateTime endDateTime,
            Car car
        )
        {
            return new Booking
            {
                Id = Guid.NewGuid(),
                RequestedOn = DateTime.UtcNow,
                StartDateTime = startDateTime,
                RepeatOption = repeatOption,
                DaysToRepeatOn = daysToRepeatOn,
                EndDateTime = endDateTime,
                CarId = car.Id,
                Car = car,
            };
        }

        [Key] public Guid Id { get; set; }

        public DateTime RequestedOn { get; set; }
        [Required] public DateTime StartDateTime { get; set; }

        [Required]
        //Enum: DoesNotRepeat, Daily, Weekly
        public RepeatOption RepeatOption { get; set; }

        //Enum: None,Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday
        public DaysOfWeek? DaysToRepeatOn { get; set; }
        public DateTime EndDateTime { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }

    [Flags]
    public enum DaysOfWeek
    {
        None = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64
    }

    public enum RepeatOption
    {
        DoesNotRepeat = 1,
        Daily = 2,
        Weekly = 3
    }
}