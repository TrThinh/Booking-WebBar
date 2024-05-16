using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface IBillRepository : IRepository<Bill>
    {
        void Update(Bill obj);
    }
}
