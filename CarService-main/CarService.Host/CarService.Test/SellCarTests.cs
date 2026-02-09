using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Dto;
using Moq;
using FluentAssertions;

namespace CarService.Test
{
    public class SellCarServiceTests
    {
        private Mock<ICarCrudService> carServiceMock;
        private Mock<ICustomerRepository> customerRepoMock;

        [Fact]
        public void SellingCar_ShouldReturnCorrectPrice()
        {
            // Arrange
            carServiceMock = new Mock<ICarCrudService>();
            customerRepoMock = new Mock<ICustomerRepository>();

            var expectedPrice = 23000m;

            carServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Car
            {
                Id = Guid.NewGuid(),
                Model = "Honda Civic",
                Year = 2021,
                BasePrice = 25000m
            });

            customerRepoMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Jane Doe",
                Email = "jane@domain.com",
                Discount = 2000
            });

            var sellCarService = new BL.Services.SellCar(carServiceMock.Object, customerRepoMock.Object);

            // Act
            var result = sellCarService.Sell(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            result.Should().NotBeNull();
            result.Price.Should().Be(expectedPrice);
        }

        [Fact]
        public void SellingCar_WhenCustomerMissing_ShouldThrowException()
        {
            // Arrange
            carServiceMock = new Mock<ICarCrudService>();
            customerRepoMock = new Mock<ICustomerRepository>();

            carServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Car
            {
                Id = Guid.NewGuid(),
                Model = "Honda Civic",
                Year = 2021,
                BasePrice = 25000m
            });

            customerRepoMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Customer?)null);

            var sellCarService = new BL.Services.SellCar(carServiceMock.Object, customerRepoMock.Object);

            // Act & Assert
            Action act = () => sellCarService.Sell(Guid.NewGuid(), Guid.NewGuid());
            act.Should().Throw<ArgumentException>()
                .WithMessage("*not found*");
        }
    }
}
