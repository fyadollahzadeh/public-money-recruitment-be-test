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

        private IRentalLogic GetRentalLogic(List<RentalEntity> fakeRentalsInDatabse)
        {
            var stubRentalRepository = new Mock<IRentalDatabaseRepository>();
            stubRentalRepository.Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int id, CancellationToken ct) => fakeRentalsInDatabse.First(X => X.Id == id));
            var rentalLogic = new RentalLogic(stubRentalRepository.Object);

            return rentalLogic;
        }
    }
}
