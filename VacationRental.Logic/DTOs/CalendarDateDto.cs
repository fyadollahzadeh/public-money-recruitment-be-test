namespace VacationRental.Logic.DTOs
{
    public class CalendarDateDto
    {
        public DateOnly Date { get; set; }
        public List<CalendarBookingViewDto> Bookings { get; set; }
        public List<PreparationTime> PreparationTimes { set; get; }
    }
}
