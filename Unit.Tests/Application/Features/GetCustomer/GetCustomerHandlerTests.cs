using Application.Features.GetCustomer;
using Application.Interfaces.HttpClients.Address;
using Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;
using Unit.Tests.DataFaker;

namespace Unit.Tests.Application.Features.GetCustomer
{
    public class GetCustomerHandlerTests
    {
        private readonly GetCustomerHandler _sut;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IAddressHttpClient> _addressHttpClientMock;

        public GetCustomerHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _addressHttpClientMock = new Mock<IAddressHttpClient>();
            _sut = new GetCustomerHandler(_customerRepositoryMock.Object, _addressHttpClientMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnCustomer_When_CustomerExists()
        {
            // Arrange
            var request = new GetCustomerRequest(1);
            var customer = new CustomerFaker().Generate();
            var address = new AddressResponseFaker().Generate();

            _customerRepositoryMock.Setup(x => x.GetCustomerByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(customer);

            _addressHttpClientMock.Setup(x => x.GetAddress(It.IsAny<Guid>()))
                .ReturnsAsync(address);

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Forename.Should().Be(customer.Forename);
            result.Surname.Should().Be(customer.Surname);
            result.PhoneNumber.Should().Be(customer.Phone);
            result.DateOfJoining.Should().Be(customer.DateOfJoining);
        }
    }
}
