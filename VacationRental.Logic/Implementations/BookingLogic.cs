using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationRental.Infrastructure.Entities;
using VacationRental.Infrastructure.Exceptions;
using VacationRental.Infrastructure.Repositories.Interfaces;
using VacationRental.Logic.DTOs;
using VacationRental.Logic.Interfaces;

namespace VacationRental.Logic.Implementations
{
    public class BookingLogic : IBookingLogic
    {
        private readonly IBookingDatabaseRepository _bookingDatabaseRepository;
        private readonly IRentalDatabaseRepository _rentalDatabaseRepository;
        public BookingLogic(IBookingDatabaseRepository bookingDatabaseRepository,IRentalDatabaseRepository rentalDatabaseRepository)
        {
            _bookingDatabaseRepository = bookingDatabaseRepository;
            _rentalDatabaseRepository = rentalDatabaseRepository;
        }

        public async Task<int> AddBookingAsync(BookingCreationDto bookingEntity, CancellationToken ct)
        {
            var rental = await _rentalDatabaseRepository.GetAsync(bookingEntity.RentalId, ct);
            if (rental == null) throw new RentalNotFoundException();

            var addedItemId = await _bookingDatabaseRepository.AddAsync(bookingEntity.Adapt<BookingEntity>(),ct);
            return addedItemId;
        }

        public async Task<BookingEntity> GetBookingAsync(int bookingId, CancellationToken ct)
        {
            var item = await _bookingDatabaseRepository.GetAsync(bookingId, ct);
            if (item == null) throw new EntityNotFoundException();
            return item;
        }
    }
}
