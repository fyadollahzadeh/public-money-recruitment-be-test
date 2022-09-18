using Mapster;
using VacationRental.Infrastructure.Entities;
using VacationRental.Infrastructure.Exceptions;
using VacationRental.Infrastructure.Repositories.Interfaces;
using VacationRental.Logic.DTOs;
using VacationRental.Logic.Interfaces;

namespace VacationRental.Logic.Implementations
{
    public class RentalLogic : IRentalLogic
    {
        IRentalDatabaseRepository _rentalDatabaseRepository;
        public RentalLogic(IRentalDatabaseRepository rentalDatabaseRepository)
        {
            _rentalDatabaseRepository = rentalDatabaseRepository;
        }
        public async Task<int> AddRentalAsync(RentalCreationDto model, CancellationToken ct)
        {
            var addedRentalId = await _rentalDatabaseRepository.AddAsync(model.Adapt<RentalEntity>(),ct);
            return addedRentalId;
        }

        public async Task<RentalEntity> GetRentalAsync(int rentalId, CancellationToken ct)
        {
            var item =  await _rentalDatabaseRepository.GetAsync(rentalId,ct);
            if (item == null) throw new EntityNotFoundException();

            return item;
        }
    }
}
