using System.Collections.Generic;
using System.Threading.Tasks;
using OnBoard.Models;

namespace OnBoard.Repositories
{
    public interface IStoreRepository
    {
        Task AddAsync(Store store);
        Task<IEnumerable<Store>> ListAsync();
        Task<Store> FindByIdAsync(int storeId);
        Task<Store> FindByNameAsync(string storeName);

        void Update(Store store);
        void Remove(Store store);
        Task SaveChangeAsync();
    }
}