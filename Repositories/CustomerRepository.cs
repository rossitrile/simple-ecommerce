using System.Collections.Generic;
using System.Threading.Tasks;
using OnBoard.DataAccess;
using OnBoard.Models;
using Microsoft.EntityFrameworkCore;
using OnBoard.Repositories;

namespace OnBoard.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context) { _context = context; }
        public async Task AddAsync(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
        }
        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _context.Customer.ToListAsync();
        }
        public async Task<Customer> FindByIdAsync(int id)
        {
            return await _context.Customer.FindAsync(id);
        }
        public void Update(Customer customer)
        {
            _context.Customer.Update(customer);
        }
        public void Remove(Customer customer)
        {
            _context.Customer.Remove(customer);
        }
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> FindByNameAsync(string customerName)
        {
            return await _context.Customer.FirstOrDefaultAsync(c => c.Name == customerName);
        }
    }
}
