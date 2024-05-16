using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface IDepositRepository : IRepository<Deposit>
    {
        void Update(Deposit obj);
    }
}
