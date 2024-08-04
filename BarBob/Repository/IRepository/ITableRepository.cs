using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface ITableRepository : IRepository<Table>
    {
        Task<IEnumerable<Table>> GetTablesByTableTypeIdAsync(int tableTypeId);
        void Update(Table obj);
    }
}
