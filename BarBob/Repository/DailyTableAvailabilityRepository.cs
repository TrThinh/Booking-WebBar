using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BarBob.Data;
using BarBob.Repository.IRepository;
using BarBob.Models;
using Microsoft.EntityFrameworkCore;

namespace BarBob.Repository
{
    public class DailyTableAvailabilityRepository : Repository<DailyTableAvailability>, IDailyTableAvailabilityRepository
    { 
        private ApplicationDbContext _db;

        public DailyTableAvailabilityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DailyTableAvailability obj)
        {
            _db.DailyTableAvailabilitys.Update(obj);
        }
    }
}
