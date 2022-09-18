﻿using FluentAssertions;
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
    public class BookingLogicTests
    {
        [Fact]
        public async void GetBooking_ValidId_ShouldReturnBookingModel()
        {
            //Arrange
            var givenData = new BookingEntity { Id = 1, RentalId = 1, Nights = 1, Start = new DateOnly(2002, 1, 1) };
            IBookingLogic bookingLogic = GetBookingLogic(fakeBookingsInDatabse : new List<BookingEntity> { givenData });

            //Act
            var bookingEntity = await bookingLogic.GetBookingAsync(1, CancellationToken.None);

            //Assert
            bookingEntity.Should().BeEquivalentTo(givenData);
        }

        [Fact]
        public async void GetBooking_NotExistingId_ShouldThrowExcpetion()
        {
            //Arrange
            var givenData = new BookingEntity { Id = 1, RentalId = 1, Nights = 1, Start = new DateOnly(2002, 1, 1) };
            IBookingLogic bookingLogic = GetBookingLogic(fakeBookingsInDatabse : new List<BookingEntity> { givenData });

            //Act
            var action =async ()=> await bookingLogic.GetBookingAsync(2, CancellationToken.None);

            //Assert
            await action.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async void AddBooking_NoOverlap_ShouldReturnId()
        {
            //Arrange
            var givenData = new BookingCreationDto { RentalId = 1, Nights = 1, Start = new DateOnly(2002, 1, 1) };
            var bookingLogic = GetBookingLogic(fakeRentalsInDatabase: new List<RentalEntity> { new RentalEntity(1) { Units = 1, PreparationTimeInDays = 0 } });

            //Act
            int addedItemId = await bookingLogic.AddBookingAsync(givenData, CancellationToken.None);

            //Assert
            addedItemId.Should().Be(1);

        }

        [Fact]
        public async void AddBooking_NotExistingRental_ShouldThrowException()
        {
            //Arrange
            var givenData = new BookingCreationDto { RentalId = 2, Nights = 1, Start = new DateOnly(2002, 1, 1) };
            IBookingLogic bookingLogic = GetBookingLogic(fakeRentalsInDatabase: new List<RentalEntity> { new RentalEntity(1) { Units = 1, PreparationTimeInDays = 1 } });

            //Act
            var action = async () => await bookingLogic.AddBookingAsync(givenData, CancellationToken.None);

            //Assert
            await action.Should().ThrowAsync<RentalNotFoundException>();
        }



        [Fact]
        public async void AddBooking_NoPreparationDays_HasOverlap_OfType_StartsBeforeEndsAfter_ShouldThrowException()
        {
            //Arrange
            var existingFakeBookings = new List<BookingEntity>()
            {
                new BookingEntity
                {
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 3),
                    Nights = 2
                }
            };
            var givenData = new BookingCreationDto { RentalId = 1, Nights = 6, Start = new DateOnly(2002, 1, 1) };
            IBookingLogic bookingLogic = GetBookingLogic(fakeRentalsInDatabase: new List<RentalEntity> { new RentalEntity(1) { Units = 1, PreparationTimeInDays = 0 } } ,fakeBookingsInDatabse : existingFakeBookings);

            //Act
            var action = async () => await bookingLogic.AddBookingAsync(givenData, CancellationToken.None);

            //Assert
            await action.Should().ThrowAsync<NotAvailableForBookingException>();
        }

        [Fact]
        public async void AddBooking_NoPreparationDays_HasOverlap_OfType_StartsBeforeEndsBefore_ShouldThrowException()
        {
            //Arrange
            var existingFakeBookings = new List<BookingEntity>()
            {
                new BookingEntity
                {
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 2),
                    Nights = 2
                }
            };
            var givenData = new BookingCreationDto { RentalId = 1, Nights = 2, Start = new DateOnly(2002, 1, 1) };
            IBookingLogic bookingLogic = GetBookingLogic(fakeRentalsInDatabase: new List<RentalEntity> { new RentalEntity(1) { Units = 1, PreparationTimeInDays = 0 } }, fakeBookingsInDatabse: existingFakeBookings);

            //Act
            var action = async () => await bookingLogic.AddBookingAsync(givenData, CancellationToken.None);

            //Assert
            await action.Should().ThrowAsync<NotAvailableForBookingException>();
        }



        [Fact]
        public async void AddBooking_NoPreparationDays_HasOverlap_OfType_StartsAfterEndsBefore_ShouldThrowException()
        {
            //Arrange
            var existingFakeBookings = new List<BookingEntity>()
            {
                new BookingEntity
                {
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 3),
                    Nights = 4
                }
            };
            var givenData = new BookingCreationDto { RentalId = 1, Nights = 2, Start = new DateOnly(2002, 1, 4) };
            IBookingLogic bookingLogic = GetBookingLogic(fakeRentalsInDatabase: new List<RentalEntity> { new RentalEntity(1) { Units = 1, PreparationTimeInDays = 0 } }, fakeBookingsInDatabse: existingFakeBookings);

            //Act
            var action = async () => await bookingLogic.AddBookingAsync(givenData, CancellationToken.None);

            //Assert
            await action.Should().ThrowAsync<NotAvailableForBookingException>();
        }
        [Fact]
        public async void AddBooking_NoPreparationDays_HasOverlap_OfType_StartsAfterEndsAfter_ShouldThrowException()
        {
            //Arrange
            var existingFakeBookings = new List<BookingEntity>()
            {
                new BookingEntity
                {
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 3),
                    Nights = 2
                }
            };
            var givenData = new BookingCreationDto { RentalId = 1, Nights = 4, Start = new DateOnly(2002, 1, 4) };
            IBookingLogic bookingLogic = GetBookingLogic(fakeRentalsInDatabase: new List<RentalEntity> { new RentalEntity(1) { Units = 1, PreparationTimeInDays = 0 } }, fakeBookingsInDatabse: existingFakeBookings);

            //Act
            var action = async () => await bookingLogic.AddBookingAsync(givenData, CancellationToken.None);

            //Assert
            await action.Should().ThrowAsync<NotAvailableForBookingException>();
        }


        [Fact]
        public async void AddBooking_RentalPreparationDaysAddsOverlapping_OfType_StartsAfterEndsAfter_ShouldThrowException()
        {
            //Arrange
            var existingFakeBookings = new List<BookingEntity>()
            {
                new BookingEntity
                {
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 3),
                    Nights = 2
                }
            };
            var givenData = new BookingCreationDto { RentalId = 1, Nights = 3, Start = new DateOnly(2002, 1, 4) };
            IBookingLogic bookingLogic = GetBookingLogic(fakeRentalsInDatabase: new List<RentalEntity> { new RentalEntity(1) { Units = 1, PreparationTimeInDays = 2 } }, fakeBookingsInDatabse: existingFakeBookings);

            //Act
            var action = async () => await bookingLogic.AddBookingAsync(givenData, CancellationToken.None);

            //Assert
            await action.Should().ThrowAsync<NotAvailableForBookingException>();
        }


        [Fact]
        public async void AddBooking_WithPreparationDays_NoOverlap_ShouldReturnId()
        {
            //Arrange
            var existingFakeBookings = new List<BookingEntity>()
            {
                new BookingEntity
                {
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 1),
                    Nights = 2
                }
            };
            var givenData = new BookingCreationDto { RentalId = 1, Nights = 1, Start = new DateOnly(2002, 1, 4) };
            var bookingLogic = GetBookingLogic(
                fakeBookingsInDatabse: existingFakeBookings,
                fakeRentalsInDatabase: new List<RentalEntity> { new RentalEntity(1) { Units = 1, PreparationTimeInDays = 1 } });

            //Act
            int addedItemId = await bookingLogic.AddBookingAsync(givenData, CancellationToken.None);

            //Assert
            addedItemId.Should().Be(1);

        }


        [Fact]
        public async void AddBooking_HasOverlap_OfType_StartsBeforeEndsAfter_RentalHas2Units_ShouldReturnId()
        {
            //Arrange
            var existingFakeBookings = new List<BookingEntity>()
            {
                new BookingEntity
                {
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 3),
                    Nights = 2
                }
            };
            var givenData = new BookingCreationDto { RentalId = 1, Nights = 3, Start = new DateOnly(2002, 1, 2) };
            var bookingLogic = GetBookingLogic(
                fakeBookingsInDatabse: existingFakeBookings,
                fakeRentalsInDatabase: new List<RentalEntity> { new RentalEntity(1) { Units = 2, PreparationTimeInDays = 1 } });

            //Act
            int addedItemId = await bookingLogic.AddBookingAsync(givenData, CancellationToken.None);

            //Assert
            addedItemId.Should().Be(1);

        }



        [Fact]
        public async void GetBookingsOccupiedOnDate_ThereAre2UnitsOccupied_ShouldReturn2UnitIds()
        {
            //Arrange

            var existingFakeBookings = new List<BookingEntity>()
            {

                new BookingEntity
                {
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 3),
                    Nights = 3
                },
                new BookingEntity
                {
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 5),
                    Nights = 2
                }
            };
            var bookingLogic = GetBookingLogic(
                fakeBookingsInDatabse: existingFakeBookings,
                fakeRentalsInDatabase: new List<RentalEntity> { new RentalEntity(1) { Units = 2, PreparationTimeInDays = 1 } });

            //Act
            var bookings = await bookingLogic.GetBookingsOfRentalOccupiedOnDate(1, new DateOnly(2002, 1, 5), CancellationToken.None);

            //Assert
            bookings.Should().BeEquivalentTo(existingFakeBookings);
        }



        [Fact]
        public async void GetBookingsOccupiedOnDate_ThereAre2UnitsThatNeedPreparation_ShouldReturn2PreparationItems()
        {
            //Arrange

            var existingFakeBookings = new List<BookingEntity>()
            {

                new BookingEntity
                {
                    Id = 1,
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 3),
                    Nights = 1
                },
                new BookingEntity
                {
                    Id = 2,
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 1),
                    Nights = 3
                },
                new BookingEntity
                {
                    Id = 3,
                    RentalId = 1,
                    Start = new DateOnly(2002, 1, 1),
                    Nights = 1
                }
            };
            var bookingLogic = GetBookingLogic(
                fakeBookingsInDatabse: existingFakeBookings,
                fakeRentalsInDatabase: new List<RentalEntity> { new RentalEntity(1) { Units = 2, PreparationTimeInDays = 3 } });
            var expectedResult = new List<PreparationTime>() { new PreparationTime { Unit = 1 }, new PreparationTime { Unit = 2 } };

            //Act
            List<PreparationTime> preparationTimes = await bookingLogic.GetUnitsOfRentalNeedsPreparationOnDate(1, new DateOnly(2002, 1, 5), CancellationToken.None);

            //Assert
            preparationTimes.Should().BeEquivalentTo(expectedResult);
        }

        private IBookingLogic GetBookingLogic(List<RentalEntity>? fakeRentalsInDatabase=null,List<BookingEntity>? fakeBookingsInDatabse = null)
        {

            var stubRentalRepository = new Mock<IRentalDatabaseRepository>();
            stubRentalRepository.Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int id, CancellationToken ct) => fakeRentalsInDatabase?.FirstOrDefault(X => X.Id == id));
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
            var bookingLogic = new BookingLogic(stubBookingRepository.Object,stubRentalRepository.Object);

            return bookingLogic;
        }
    }
}
