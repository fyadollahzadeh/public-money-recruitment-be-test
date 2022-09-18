namespace VacationRental.Infrastructure.Exceptions
{
    public class RentalNotFoundException : Exception
    {
        public RentalNotFoundException()
        {
        }

        public RentalNotFoundException(string? message) : base(message)
        {
        }
    }
}
