using System.Collections.Generic;
using System.Threading.Tasks;
using OnBoard.DataAccess;
using OnBoard.Models;
using Microsoft.EntityFrameworkCore;
using OnBoard.Repositories;

namespace OnBoard.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) { _context = context; }
        public async Task AddAsync(Product product)
        {
            await _context.Product.AddAsync(product);
        }
        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Product.ToListAsync();
        }
        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.Product.FindAsync(id);
        }
        public void Update(Product product)
        {
            _context.Product.Update(product);
        }
        public void Remove(Product product)
        {
            _context.Product.Remove(product);
        }
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Product> FindByNameAsync(string productName)
        {
            return await _context.Product.FirstOrDefaultAsync(c => c.Name == productName);
        }
    }
}
