namespace VacationRental.Infrastructure.Exceptions
{
    public class RentalNotFoundException : Exception
    {
        public RentalNotFoundException(string? message) : base(message)
        {
        }
    }
}
