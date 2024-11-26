using Application.Features.AddCustomer;
using Application.Features.GetCustomer;
using Application.Features.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly IMediator _mediator;

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("all-customers")]
        public async Task<ActionResult<IEnumerable<GetCustomerResponse>>> GetAllCustomers()
        {
            return Ok(await _mediator.Send(new GetAllCustomersRequest()));
        }

        [HttpPost("add-customer")]
        public async Task<ActionResult> AddCustomer(AddCustomerRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet("get-customer/{customerId}")]
        public async Task<ActionResult<GetCustomerResponse>> GetCustomer([FromRoute] int customerId)
        {
            return Ok(await _mediator.Send(new GetCustomerRequest(customerId)));
        }
    }
}
