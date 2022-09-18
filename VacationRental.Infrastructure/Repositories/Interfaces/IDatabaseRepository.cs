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
        Task<T> GetAsync(TKey itemId, CancellationToken ct);
    }
}
