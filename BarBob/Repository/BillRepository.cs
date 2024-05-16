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
    public class BillRepository : Repository<Bill>, IBillRepository
    { 
        private ApplicationDbContext _db;

        public BillRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Bill obj)
        {
            _db.Bills.Update(obj);
        }
    }
}
