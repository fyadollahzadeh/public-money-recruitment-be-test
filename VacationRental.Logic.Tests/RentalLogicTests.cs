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
            var givenRental = new RentalEntity(1) { Units = 1 };
            var fakeRentalsInDatabse = new List<RentalEntity> { givenRental };
            IRentalLogic rentalLogic = GetRentalLogic(fakeRentalsInDatabse);

            //Act
            var rental = await rentalLogic.GetRentalAsync(1,CancellationToken.None);

            //Assert
            rental.Should().BeEquivalentTo(givenRental);
        }

        [Fact]
        public async void GetRental_NotExistingId_ShouldReturnRentalModel()
        {
            //Arrange
            var fakeRentalsInDatabse = new List<RentalEntity> { new RentalEntity(1) { Units = 1 } };
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
            var givenRental = new RentalCreationDto { Units = 1 };

            //Act
            var rentalId = await rentalLogic.AddRentalAsync(givenRental, CancellationToken.None);

            //Assert
            rentalId.Should().Be(1);

        }

        private IRentalLogic GetRentalLogic(List<RentalEntity>? fakeRentalsInDatabse = null)
        {
            var stubRentalRepository = new Mock<IRentalDatabaseRepository>();
            stubRentalRepository.Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int id, CancellationToken ct) => fakeRentalsInDatabse?.FirstOrDefault(X => X.Id == id));
            stubRentalRepository.Setup(x => x.AddAsync(It.IsAny<RentalEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            var rentalLogic = new RentalLogic(stubRentalRepository.Object);

            return rentalLogic;
        }
    }
}
