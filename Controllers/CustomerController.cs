using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
    private readonly ILogger<Assign1QueryController> _logger;

        public CustomerController(CustomerService customerService, ILogger<Assign1QueryController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            _logger.LogInformation("Customer called AddCustomer (POST) with Customer:{@Customer} ", customer);
            var createdCustomer = _customerService.AddCustomer(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.CustomerId }, createdCustomer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer customer)
        {
            _logger.LogInformation("Customer called UpdateCustomer (PUT) with ID:{Id} and Customer:{@Customer} ", id, customer);
            if (!_customerService.UpdateCustomer(id, customer))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Customer called DeleteCustomer (DELETE) with ID:{Id} ", id);
            if (!_customerService.DeleteCustomer(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
