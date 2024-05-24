using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface ITableRepository : IRepository<Table>
    {
        void Update(Table obj);
    }
}
