using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VacationRental.Infrastructure.Entities;
using VacationRental.Infrastructure.Exceptions;
using VacationRental.Infrastructure.Repositories.Interfaces;
using VacationRental.Logic.DTOs;
using VacationRental.Logic.Implementations;
using VacationRental.Logic.Interfaces;
using Xunit;

namespace VacationRental.Logic.Tests
{
    public class RentalLogicTests
    {

        [Fact]
        public async void GetRental_ValidId_ShouldReturnRentalModel()
        {
            //Arrange
            var givenRental = new RentalEntity(1) { Units = 1, PreparationTimeInDays = 1 };
            var fakeRentalsInDatabse = new List<RentalEntity> { givenRental };
            IRentalLogic rentalLogic = GetRentalLogic(fakeRentalsInDatabse);

            //Act
            var rental = await rentalLogic.GetRentalAsync(1,CancellationToken.None);

            //Assert
            rental.Should().BeEquivalentTo(givenRental);
        }

        [Fact]
        public async void GetRental_NotExistingId_ShouldThrowExcpetion()
        {
            //Arrange
            var fakeRentalsInDatabse = new List<RentalEntity> { new RentalEntity(1) { Units = 1, PreparationTimeInDays = 1 } };
            IRentalLogic rentalLogic = GetRentalLogic(fakeRentalsInDatabse);

            //Act
            var action = async()=>await rentalLogic.GetRentalAsync(2, CancellationToken.None);

            //Assert
            await action.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async void AddRental_ValidInput_ShouldReturnRentalId()
        {
            //Arrange
            IRentalLogic rentalLogic = GetRentalLogic();
            var givenRental = new RentalCreationDto { Units = 1,PreparationTimeInDays = 1 };

            //Act
            var rentalId = await rentalLogic.AddRentalAsync(givenRental, CancellationToken.None);

            //Assert
            rentalId.Should().Be(1);

        }

        [Fact]
        public async void UpdateRental_UpdatePreparationTime_HasOverlap_ShouldThrowException()
        {
            //Arrange

            var fakeBookingsInDatabse = new List<BookingEntity>()
            {

                new BookingEntity
                {
                    Id = 1,
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 1),
                    Nights = 1
                },
                new BookingEntity
                {
                    Id = 2,
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 3),
                    Nights = 3
                }
            };
            IRentalLogic rentalLogic = GetRentalLogic(fakeBookingsInDatabse : fakeBookingsInDatabse);

            //Act
            var action = async () => await rentalLogic.UpdateRentalAsync(new RentalEntity { Id = 1, PreparationTimeInDays = 2, Units = 1 }, CancellationToken.None);

            //Assert
            await action.Should().ThrowAsync<NotUpdatableException>();

        }

        private IRentalLogic GetRentalLogic(List<RentalEntity>? fakeRentalsInDatabse = null, List<BookingEntity>? fakeBookingsInDatabse = null)
        {
            var stubRentalRepository = new Mock<IRentalDatabaseRepository>();
            stubRentalRepository.Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int id, CancellationToken ct) => fakeRentalsInDatabse?.FirstOrDefault(X => X.Id == id));
            stubRentalRepository.Setup(x => x.AddAsync(It.IsAny<RentalEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var stubBookingRepository = new Mock<IBookingDatabaseRepository>();
            stubBookingRepository
                .Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int id, CancellationToken ct) => fakeBookingsInDatabse?.FirstOrDefault(X => X.Id == id));
            stubBookingRepository
                .Setup(x => x.GetAllAsync(It.IsAny<Func<BookingEntity, bool>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Func<BookingEntity, bool> func, CancellationToken ct) => fakeBookingsInDatabse?.Where(func));
            stubBookingRepository.Setup(x => x.AddAsync(It.IsAny<BookingEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            var rentalLogic = new RentalLogic(stubRentalRepository.Object, stubBookingRepository.Object);

            return rentalLogic;
        }
    }
}
