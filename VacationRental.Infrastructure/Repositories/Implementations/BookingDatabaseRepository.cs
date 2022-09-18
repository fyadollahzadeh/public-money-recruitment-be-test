using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationRental.Infrastructure.Entities;
using VacationRental.Infrastructure.Repositories.Interfaces;

namespace VacationRental.Infrastructure.Repositories.Implementations
{
    public class BookingDatabaseRepository : IBookingDatabaseRepository
    {
        public Task<int> AddAsync(BookingEntity item, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingEntity>> GetAllAsync(Func<BookingEntity, bool> searchQuery, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<BookingEntity> GetAsync(int itemId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
