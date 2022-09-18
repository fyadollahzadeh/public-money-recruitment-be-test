using VacationRental.Infrastructure.Entities;

namespace VacationRental.Infrastructure.Repositories.Interfaces
{
    public interface IRentalDatabaseRepository : IDatabaseRepository<RentalEntity,int>
    {
    }
}
