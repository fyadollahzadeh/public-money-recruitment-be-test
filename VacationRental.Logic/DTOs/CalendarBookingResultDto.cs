namespace VacationRental.Logic.DTOs
{
    public class CalendarBookingResultDto
    {
        public int RentalId { get; set; }
        public List<CalendarDateDto> Dates { get; set; }
    }
}
