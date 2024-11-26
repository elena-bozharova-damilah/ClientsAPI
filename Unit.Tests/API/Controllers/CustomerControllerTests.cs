using Application.Features.GetCustomers;
using ClientsAPI.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Unit.Tests.DataFaker;

namespace Unit.Tests.API.Controllers
{
    public class CustomerControllerTests
    {
        private readonly CustomerController _sut;
        private Mock<ILogger<CustomerController>> _loggerMock;
        private Mock<IMediator> _mediatorMock;

        public CustomerControllerTests()
        {
            _loggerMock = new();
            _mediatorMock = new();
            _sut = new CustomerController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllCustomers_Should_Return_Customers_When_ValidRequest()
        {
            // Arrange
            var request = new GetAllCustomersRequest();
            int numberOfCustomers = 5;
            var expectedCustomers = new GetCustomerResponseFaker().WithRandomParameters(numberOfCustomers);

            _mediatorMock.Setup(x => x.Send(request, default)).ReturnsAsync(new List<GetCustomerResponse>());

            // Act
            ActionResult<IEnumerable<GetCustomerResponse>> response = await _sut.GetAllCustomers();

            // Assert

            response.Result.Should().BeOfType<OkObjectResult>();
            var result = response.Result as OkObjectResult;
            result!.Value.Should().BeAssignableTo<IEnumerable<GetCustomerResponse>>();
            result.StatusCode.Should().Be(200);
        }
    }
}
