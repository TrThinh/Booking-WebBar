using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface IServiceRepository : IRepository<Service>
    {
        void Update(Service obj);
    }
}
