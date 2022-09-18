using VacationRental.Infrastructure.Entities;
using VacationRental.Logic.DTOs;
using VacationRental.Logic.Interfaces;

namespace VacationRental.Logic.Implementations
{
    public class RentalLogic : IRentalLogic
    {
        public Task<int> AddRentalAsync(RentalCreationDto model, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<RentalEntity> GetRentalAsync(int rentalId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
