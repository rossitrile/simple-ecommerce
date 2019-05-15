using System.Collections.Generic;
using System.Threading.Tasks;
using OnBoard.DataAccess;
using OnBoard.Models;
using Microsoft.EntityFrameworkCore;
using OnBoard.Repositories;

namespace OnBoard.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;
        public SaleRepository(AppDbContext context) { _context = context; }
        public async Task AddAsync(Sale sale)
        {
            await _context.Sale.AddAsync(sale);
        }
        public async Task<IEnumerable<Sale>> ListAsync()
        {
            return await _context.Sale
                                .Include(s => s.store)
                                .Include(c => c.customer)
                                .Include(p => p.product)
                                .ToListAsync();
        }
        public async Task<Sale> FindByIdAsync(int id)
        {
            return await _context.Sale.FindAsync(id);
        }
        public void Update(Sale sale)
        {
            _context.Sale.Update(sale);
        }
        public void Remove(Sale sale)
        {
            _context.Sale.Remove(sale);
        }
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
