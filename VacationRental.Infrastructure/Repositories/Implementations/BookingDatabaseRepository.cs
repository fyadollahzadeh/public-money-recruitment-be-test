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
        Dictionary<int, BookingEntity> _bookings;
        public BookingDatabaseRepository(IDictionary<int, BookingEntity> bookings)
        {
            _bookings = (Dictionary<int, BookingEntity>)bookings;
        }
        public Task<int> AddAsync(BookingEntity item, CancellationToken ct)
        {
            var itemId = _bookings.Keys.Count + 1;
            item.Id = itemId;
            _bookings.Add(itemId, item);
            return Task.FromResult(itemId);
        }

        public async Task<IEnumerable<BookingEntity>> GetAllAsync(Func<BookingEntity, bool> searchQuery, CancellationToken ct)
        {
            var items = _bookings.Values.Where(searchQuery);
            return items;
        }

        public async Task<BookingEntity?> GetAsync(int rentalId, CancellationToken ct)
        {
            return _bookings.GetValueOrDefault(rentalId);
        }

        public Task<BookingEntity> UpdateAsync(BookingEntity item, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
