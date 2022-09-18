using VacationRental.Infrastructure.Entities;

namespace VacationRental.Infrastructure.Repositories.Interfaces
{
    public interface IBookingDatabaseRepository : IDatabaseRepository<BookingEntity,int>
    {
    }
}
