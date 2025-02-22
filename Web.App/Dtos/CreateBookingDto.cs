using Web.App.Entities;

namespace Web.App.Dtos
{
    public record CreateBookingDto
    {

        public DateTime StartDateTime { get; set; }

        //Enum: DoesNotRepeat, Daily, Weekly
        public RepeatOption RepeatOption { get; set; }

        //Enum: None,Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday
        public DaysOfWeek? DaysToRepeatOn { get; set; }
        public DateTime EndDateTime { get; set; }
        public Guid CarId { get; set; }
    }
}