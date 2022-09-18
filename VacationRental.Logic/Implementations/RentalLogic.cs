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
        IBookingDatabaseRepository _bookingDatabaseRepository;
        public RentalLogic(IRentalDatabaseRepository rentalDatabaseRepository,IBookingDatabaseRepository bookingDatabaseRepository)
        {
            _rentalDatabaseRepository = rentalDatabaseRepository;
            _bookingDatabaseRepository = bookingDatabaseRepository;
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

        public async Task<int> UpdateRentalAsync(RentalUpdateDto rentalUpdateDto, CancellationToken ct)
        {
            var rentalEntity = await GetRentalAsync(rentalUpdateDto.Id,ct);

            var doesOverlap = await doesOverlapHappens();
            if (doesOverlap) throw new NotUpdatableException("can not update, due to existing bookings");

            var updatedItemId = await _rentalDatabaseRepository.UpdateAsync(rentalEntity, ct);

            return updatedItemId;
            async Task<bool> doesOverlapHappens()
            {
                var bookingsOfRental = await _bookingDatabaseRepository.GetAllAsync(x => x.RentalId == rentalEntity.Id, ct);
                foreach (var booking in bookingsOfRental)
                {
                    var overlappingBookings = await _bookingDatabaseRepository.GetAllAsync(otherBooking => otherBooking.Id != booking.Id && otherBooking.RentalId == booking.RentalId && booking.EndDate.AddDays(rentalUpdateDto.PreparationTimeInDays) > otherBooking.Start, ct);
                    int overlapCounts = overlappingBookings.Count();
                    if (overlapCounts >= rentalUpdateDto.Units)
                    {
                        return true;
                    }

                }
                return false;
            }
        }
    }
}
