namespace VacationRental.Logic.DTOs
{
    public class CalendarBookingRequestDto
    {
        public int RentalId { set; get; }
        public DateOnly Start { set; get; }
        public int Nights { set; get; }
    }
}
