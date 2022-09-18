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
        public Task<int> AddAsync(RentalEntity item, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RentalEntity>> GetAllAsync(Func<RentalEntity, bool> searchQuery, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<RentalEntity> GetAsync(int itemId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(RentalEntity item, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
