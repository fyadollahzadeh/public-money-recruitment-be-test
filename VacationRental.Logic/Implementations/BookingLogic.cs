using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationRental.Infrastructure.Entities;
using VacationRental.Logic.Interfaces;

namespace VacationRental.Logic.Implementations
{
    public class BookingLogic : IBookingLogic
    {
        public BookingLogic()
        {
        }
        public async Task<BookingEntity> GetBookingAsync(int bookingId, CancellationToken ct)
        {
            return new BookingEntity { Id = 1, RentalId = 1, Nights = 1, Start = new DateTime(2002, 1, 1) };
        }
    }
}
