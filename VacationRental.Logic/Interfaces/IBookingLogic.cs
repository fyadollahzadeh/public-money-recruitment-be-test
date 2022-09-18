using VacationRental.Infrastructure.Entities;
using VacationRental.Logic.DTOs;

namespace VacationRental.Logic.Interfaces
{
    public interface IBookingLogic
    {
        Task<BookingEntity> GetBookingAsync(int bookingId, CancellationToken ct);
    }
}
