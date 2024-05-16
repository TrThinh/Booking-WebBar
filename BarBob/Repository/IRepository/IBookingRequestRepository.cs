using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface IBookingRequestRepository : IRepository<BookingRequest>
    {
        void Update(BookingRequest obj);
    }
}
