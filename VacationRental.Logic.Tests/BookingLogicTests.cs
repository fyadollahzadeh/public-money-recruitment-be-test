using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VacationRental.Infrastructure.Entities;
using VacationRental.Logic.Implementations;
using VacationRental.Logic.Interfaces;
using Xunit;

namespace VacationRental.Logic.Tests
{
    public class BookingLogicTests
    {
        [Fact]
        public async void GetBooking_ValidId_ShouldReturnBookingModel()
        {
            //Arrange
            var givenData = new BookingEntity { Id = 1, RentalId = 1, Nights = 1, Start = new DateTime(2002, 1, 1) };
            IBookingLogic bookingLogic = GetBookingLogic(new List<BookingEntity> { givenData });

            //Act
            var bookingEntity = await bookingLogic.GetBookingAsync(1, CancellationToken.None);

            //Assert
            bookingEntity.Should().BeEquivalentTo(givenData);
        }


        private IBookingLogic GetBookingLogic(List<BookingEntity>? fakeRentalsInDatabse = null)
        {
            var bookingLogic = new BookingLogic();

            return bookingLogic;
        }
    }
}
