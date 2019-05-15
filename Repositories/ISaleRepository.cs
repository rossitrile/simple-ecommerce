using System.Collections.Generic;
using System.Threading.Tasks;
using OnBoard.Models;

namespace OnBoard.Repositories
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale sale);
        Task<IEnumerable<Sale>> ListAsync();
        Task<Sale> FindByIdAsync(int saleId);

        void Update(Sale sale);
        void Remove(Sale sale);
        Task SaveChangeAsync();
    }
}