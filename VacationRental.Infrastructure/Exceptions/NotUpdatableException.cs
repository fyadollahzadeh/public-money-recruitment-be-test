namespace VacationRental.Infrastructure.Exceptions
{
    public class NotUpdatableException : Exception
    {
        public NotUpdatableException(string? message) : base(message)
        {
        }
    }
}
