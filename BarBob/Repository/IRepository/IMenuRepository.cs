using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface IMenuRepository : IRepository<Menu>
    {
        void Update(Menu obj);
    }
}
