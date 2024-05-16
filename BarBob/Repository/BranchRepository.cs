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
    public class BranchRepository : Repository<Branch>, IBranchRepository
    { 
        private ApplicationDbContext _db;

        public BranchRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Branch obj)
        {
            _db.Branches.Update(obj);
        }
    }
}
