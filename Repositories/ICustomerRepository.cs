using System.Collections.Generic;
using System.Threading.Tasks;
using OnBoard.Models;

namespace OnBoard.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task<IEnumerable<Customer>> ListAsync();
        Task<Customer> FindByIdAsync(int customerId);
        Task<Customer> FindByNameAsync(string customerName);

        void Update(Customer customer);
        void Remove(Customer customer);
        Task SaveChangeAsync();
    }
}