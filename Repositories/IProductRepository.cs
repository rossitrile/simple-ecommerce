using System.Collections.Generic;
using System.Threading.Tasks;
using OnBoard.Models;

namespace OnBoard.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<IEnumerable<Product>> ListAsync();
        Task<Product> FindByIdAsync(int productId);
        Task<Product> FindByNameAsync(string productName);

        void Update(Product product);
        void Remove(Product product);
        Task SaveChangeAsync();
    }
}