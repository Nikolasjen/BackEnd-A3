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
    private readonly ILogger<CustomerController> _logger;

        public CustomerController(CustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var userName = (User.Identity?.Name ?? "Unknown").ToLower();
            _logger.LogInformation("Operation: {Operation}, User: {User}", "GET", userName);
            var customers = _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var userName = (User.Identity?.Name ?? "Unknown").ToLower();
            _logger.LogInformation("Operation: {Operation}, User: {User}", "GET", userName);
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "GET", id, userName);
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            var userName = (User.Identity?.Name ?? "Unknown").ToLower();
            _logger.LogInformation("Operation: {Operation}, User: {User}, Customer: {@Customer}", "POST", userName, customer);

            var createdCustomer = _customerService.AddCustomer(customer);
            return CreatedAtAction(nameof(GetAllCustomers), new { id = createdCustomer.CustomerId }, createdCustomer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomerById(int id, Customer customer)
        {
            var userName = (User.Identity?.Name ?? "Unknown").ToLower();
            _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}, Customer: {@Customer}", "PUT", id, userName, customer);
            if (!_customerService.UpdateCustomer(id, customer))
            {
                _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "PUT", id, userName);
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomerById(int id)
        {
            var userName = (User.Identity?.Name ?? "Unknown").ToLower();
            _logger.LogInformation("Operation: {Operation}, User: {User}", "DELETE", userName);
            if (!_customerService.DeleteCustomer(id))
            {
                _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "DELETE", id, userName);
                return NotFound();
            }
            return NoContent();
        }
    }
}
