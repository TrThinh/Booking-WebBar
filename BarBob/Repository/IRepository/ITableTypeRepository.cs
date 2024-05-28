using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface ITableTypeRepository : IRepository<TableType>
    {
        void Update(TableType obj);
    }
}
