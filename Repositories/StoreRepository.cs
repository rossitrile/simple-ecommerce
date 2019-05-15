using System.Collections.Generic;
using System.Threading.Tasks;
using OnBoard.DataAccess;
using OnBoard.Models;
using Microsoft.EntityFrameworkCore;
using OnBoard.Repositories;

namespace OnBoard.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;
        public StoreRepository(AppDbContext context) { _context = context; }
        public async Task AddAsync(Store store)
        {
            await _context.Store.AddAsync(store);
        }
        public async Task<IEnumerable<Store>> ListAsync()
        {
            return await _context.Store.ToListAsync();
        }
        public async Task<Store> FindByIdAsync(int id)
        {
            return await _context.Store.FindAsync(id);
        }
        public void Update(Store store)
        {
            _context.Store.Update(store);
        }
        public void Remove(Store store)
        {
            _context.Store.Remove(store);
        }
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Store> FindByNameAsync(string storeName)
        {
            return await _context.Store.FirstOrDefaultAsync(c => c.Name == storeName);
        }
    }
}
