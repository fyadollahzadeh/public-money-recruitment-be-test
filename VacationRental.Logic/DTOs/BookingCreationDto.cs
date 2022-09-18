namespace VacationRental.Logic.DTOs
{
    public class BookingCreationDto
    {
        public int RentalId { get; set; }
        public DateOnly Start { set; get; }

        public int Nights { get; set; }
    }
}
