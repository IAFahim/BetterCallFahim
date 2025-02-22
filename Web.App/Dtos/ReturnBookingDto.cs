using Web.App.Entities;

namespace Web.App.Dtos;

public record ReturnBookingDto
{
    public Guid Id { get; set; }

    public DateTime RequestedOn { get; set; }
    public DateTime StartDateTime { get; set; }
    
    public RepeatOption RepeatOption { get; set; }
    public DaysOfWeek? DaysToRepeatOn { get; set; }
    public DateTime EndDateTime { get; set; }
    public Guid CarId { get; set; }
}