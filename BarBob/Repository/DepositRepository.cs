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
    public class DepositRepository : Repository<Deposit>, IDepositRepository
    { 
        private ApplicationDbContext _db;

        public DepositRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Deposit obj)
        {
            _db.Deposits.Update(obj);
        }
    }
}
