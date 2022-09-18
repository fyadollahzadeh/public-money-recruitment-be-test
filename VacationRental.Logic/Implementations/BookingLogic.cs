﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationRental.Infrastructure.Entities;
using VacationRental.Infrastructure.Repositories.Interfaces;
using VacationRental.Logic.Interfaces;

namespace VacationRental.Logic.Implementations
{
    public class BookingLogic : IBookingLogic
    {
        IBookingDatabaseRepository _bookingDatabaseRepository;
        public BookingLogic(IBookingDatabaseRepository bookingDatabaseRepository)
        {
            _bookingDatabaseRepository = bookingDatabaseRepository;
        }
        public async Task<BookingEntity> GetBookingAsync(int bookingId, CancellationToken ct)
        {
            var item = await _bookingDatabaseRepository.GetAsync(bookingId, ct);
            return item;
        }
    }
}
