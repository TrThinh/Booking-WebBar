using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking obj);
    }
}
