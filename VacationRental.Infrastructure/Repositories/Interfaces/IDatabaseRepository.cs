using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationRental.Infrastructure.Entities;

namespace VacationRental.Infrastructure.Repositories.Interfaces
{
    public interface IDatabaseRepository<T, TKey> where T : BaseEntity<TKey>
    {
        Task<int> AddAsync(T item, CancellationToken ct);
        Task<T> GetAsync(TKey itemId, CancellationToken ct);

        Task<IEnumerable<T>> GetAllAsync(Func<T, bool> searchQuery, CancellationToken ct);
        Task<int> UpdateAsync(T item, CancellationToken ct);
    }
}
