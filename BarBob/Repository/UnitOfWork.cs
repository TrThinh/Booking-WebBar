using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarBob.Data;
using BarBob.Repository.IRepository;

namespace BarBob.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IUserRepository User { get; private set; }
        public IFeedbackRepository Feedback { get; private set; }
        public IBillRepository Bill { get; private set; }
        public IBookingRequestRepository BookingRequest { get; private set; }
        public IDepositRepository Deposit { get; private set; }
        public IPaymentRepository Payment { get; private set; }
        public IServiceRepository Service { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            Feedback = new FeedbackRepository(_db);
            Bill = new BillRepository(_db);
            BookingRequest = new BookingRequestRepository(_db);
            Deposit = new DepositRepository(_db);
            Payment = new PaymentRepository(_db);
            Service = new ServiceRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
