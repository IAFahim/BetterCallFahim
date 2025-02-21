﻿using Web.App.Entities;

namespace Web.App.Dtos
{
    public record BookingCarDto
    {
        public Guid Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public List<DateOnly> BookedDates { get; set; }
        public DateTime EndDateTime { get; set; }
        public Car Car { get; set; }
    }
}