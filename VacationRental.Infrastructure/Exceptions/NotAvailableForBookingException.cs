namespace VacationRental.Infrastructure.Exceptions
{
    public class NotAvailableForBookingException : Exception
    {
        public NotAvailableForBookingException(string? message) : base(message)
        {
        }
    }
}
