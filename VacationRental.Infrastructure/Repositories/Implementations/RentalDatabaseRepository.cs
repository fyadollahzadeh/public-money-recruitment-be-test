using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationRental.Infrastructure.Entities;
using VacationRental.Infrastructure.Repositories.Interfaces;

namespace VacationRental.Infrastructure.Repositories.Implementations
{
    public class RentalDatabaseRepository : IRentalDatabaseRepository
    {
        Dictionary<int, RentalEntity> _rentals;
        public RentalDatabaseRepository(IDictionary<int, RentalEntity> rentals)
        {
            _rentals = (Dictionary<int, RentalEntity>?)rentals;
        }
        public Task<int> AddAsync(RentalEntity item, CancellationToken ct)
        {
            var itemId = _rentals.Keys.Count + 1;
            item.Id = itemId;
            _rentals.Add(itemId, item);
            return Task.FromResult(itemId);
        }

        public async Task<IEnumerable<RentalEntity>> GetAllAsync(Func<RentalEntity, bool> searchQuery, CancellationToken ct)
        {
            return _rentals.Values.Where(searchQuery);
        }

        public async Task<RentalEntity> GetAsync(int itemId, CancellationToken ct)
        {
            return _rentals.GetValueOrDefault(itemId);
        }

        public async Task<RentalEntity> UpdateAsync(RentalEntity item, CancellationToken ct)
        {
            _rentals[item.Id] = item;
            return _rentals[item.Id];
        }
    }
}
