namespace VacationRental.Infrastructure.Entities
{
    public class BookingEntity : BaseEntity<int>
    {
        public int RentalId { get; set; }

        private DateTime _startIgnoreTime;
        public BookingEntity() : base(0)
        {

        }
        public BookingEntity(int key) : base(key)
        {
        }

        public DateTime Start
        {
            get => _startIgnoreTime;
            set => _startIgnoreTime = value.Date;
        }

        public int Nights { get; set; }

        public DateTime EndDate
        {
            get => Start.AddDays(Nights);
        }
    }
}
