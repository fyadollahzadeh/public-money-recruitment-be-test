using VacationRental.Infrastructure.Entities;
using VacationRental.Logic.DTOs;

namespace VacationRental.Logic.Interfaces
{
    public interface IRentalLogic
    {
        Task<int> AddRentalAsync(RentalCreationDto model, CancellationToken ct);
        Task<RentalEntity> GetRentalAsync(int rentalId, CancellationToken ct);

        Task<int> UpdateRentalAsync(RentalEntity rentalEntity, CancellationToken none);
    }
}
