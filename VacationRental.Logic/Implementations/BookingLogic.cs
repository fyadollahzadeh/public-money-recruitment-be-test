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


            var model = bookingEntity.Adapt<BookingEntity>();
            Func<BookingEntity, bool> findOverlappingBookingsOfRentalQuery = booking => booking.RentalId == bookingEntity.RentalId && DoesBookingsOverlap(model, booking, rental.PreparationTimeInDays);
            IEnumerable<BookingEntity> overlappingBookings = await _bookingDatabaseRepository.GetAllAsync(findOverlappingBookingsOfRentalQuery, ct);
            if (overlappingBookings is not null && overlappingBookings.Count() >= rental.Units)
                throw new NotAvailableForBookingException("Not available");



            var addedItemId = await _bookingDatabaseRepository.AddAsync(model,ct);
            return addedItemId;


            bool DoesBookingsOverlap(BookingEntity firstBooking, BookingEntity secondBooking, int preparationDays)
            {
                return ((secondBooking.Start < firstBooking.Start && secondBooking.EndDate.AddDays(preparationDays) > firstBooking.Start) || (firstBooking.Start < secondBooking.Start && firstBooking.EndDate.AddDays(preparationDays) > secondBooking.Start));
            }
        }

        public async Task<BookingEntity> GetBookingAsync(int bookingId, CancellationToken ct)
        {
            var item = await _bookingDatabaseRepository.GetAsync(bookingId, ct);
            if (item == null) throw new EntityNotFoundException();
            return item;
        }
    }
}
