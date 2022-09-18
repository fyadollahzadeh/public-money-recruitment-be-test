using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VacationRental.Infrastructure.Entities;
using VacationRental.Infrastructure.Repositories.Interfaces;
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
            var stubBookingRepository = new Mock<IBookingDatabaseRepository>();
            stubBookingRepository.Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int id, CancellationToken ct) => fakeRentalsInDatabse?.FirstOrDefault(X => X.Id == id));
            stubBookingRepository.Setup(x => x.AddAsync(It.IsAny<BookingEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            var bookingLogic = new BookingLogic(stubBookingRepository.Object);

            return bookingLogic;
        }
    }
}
