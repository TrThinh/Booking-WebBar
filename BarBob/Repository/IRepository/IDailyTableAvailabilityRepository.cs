using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface IDailyTableAvailabilityRepository : IRepository<DailyTableAvailability>
    {
        void Update(DailyTableAvailability obj);
    }
}
