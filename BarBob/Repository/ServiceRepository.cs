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
    public class ServiceRepository : Repository<Service>, IServiceRepository
    { 
        private ApplicationDbContext _db;

        public ServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Service obj)
        {
            _db.Services.Update(obj);
        }
    }
}
