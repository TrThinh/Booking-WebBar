using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBob.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IFeedbackRepository Feedback { get; }
        IBillRepository Bill { get; }
        IBookingRequestRepository BookingRequest { get; }
        IDepositRepository Deposit { get; }
        IPaymentRepository Payment { get; }
        IServiceRepository Service { get; }

        void Save();
        Task<int> SaveAsync();
    }
}
