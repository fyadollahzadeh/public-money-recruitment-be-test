using VacationRental.Infrastructure.Entities;
using VacationRental.Logic.DTOs;

namespace VacationRental.Logic.Interfaces
{
    public interface IBookingLogic
    {
        Task<BookingEntity> GetBookingAsync(int bookingId, CancellationToken ct);
        Task<int> AddBookingAsync(BookingCreationDto bookingEntity, CancellationToken ct);
        Task<IEnumerable<BookingEntity>> GetBookingsOfRentalOccupiedOnDate(int rentalId, DateOnly startDate, CancellationToken ct);
        Task<List<PreparationTime>> GetUnitsOfRentalNeedsPreparationOnDate(int rentalId, DateOnly dateOnly, CancellationToken none);
    }
}
