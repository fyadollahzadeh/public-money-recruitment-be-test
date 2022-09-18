namespace VacationRental.Infrastructure.Entities
{
    public class BookingEntity : BaseEntity<int>
    {
        public int RentalId { get; set; }
        public BookingEntity() : base(0)
        {

        }
        public BookingEntity(int key) : base(key)
        {
        }

        public DateOnly Start { set; get; }

        public int Nights { get; set; }

        public DateOnly EndDate
        {
            get => Start.AddDays(Nights);
        }
    }
}
