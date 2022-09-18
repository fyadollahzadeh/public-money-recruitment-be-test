namespace VacationRental.Logic.DTOs
{
    public class CalendarBookingRequestDto
    {
        public int RentalId { set; get; }
        public DateTime Start { set; get; }
        public int Nights { set; get; }
    }
}
