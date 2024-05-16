using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface IBranchRepository : IRepository<Branch>
    {
        void Update(Branch obj);
    }
}
