using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BarBob.Data;
using BarBob.Repository.IRepository;
using BarBob.Models;

namespace BarBob.Repository
{
    public class PaymentRepository : Repository<Bill>, IPaymentRepository
    { 
        private ApplicationDbContext _db;

        public PaymentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Add(Payment entity)
        {
            throw new NotImplementedException();
        }

        public Payment Get(Expression<Func<Payment, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> GetAll(Expression<Func<Payment, bool>>? filter = null, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(Payment entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Payment> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(Payment obj)
        {
            _db.Payment.Update(obj);
        }
    }
}
