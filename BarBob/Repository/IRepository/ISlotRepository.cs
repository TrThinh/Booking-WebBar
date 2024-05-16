using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface ISlotRepository : IRepository<Slot>
    {
        void Update(Slot obj);
    }
}
