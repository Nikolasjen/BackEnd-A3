using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAppG4.Services
{
    public class CustomerService
    {
        private readonly FoodAppG4Context _context;

        public CustomerService(FoodAppG4Context context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer? GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }

        public Customer AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public bool UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return false;
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Customers.Any(e => e.CustomerId == id))
                {
                    return false;
                }
                throw;
            }
        }

        public bool DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return true;
        }
    }
}
