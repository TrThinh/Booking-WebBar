using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Update(Payment obj);
    }
}
