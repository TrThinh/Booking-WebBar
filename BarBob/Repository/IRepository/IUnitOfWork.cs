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
        IBookingRepository Booking { get; }
        ITableRepository Table { get; }

        void Save();
        Task<int> SaveAsync();
    }
}
