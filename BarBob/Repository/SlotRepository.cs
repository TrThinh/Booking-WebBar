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
    public class SlotRepository : Repository<Slot>, ISlotRepository
    { 
        private ApplicationDbContext _db;

        public SlotRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Slot obj)
        {
            _db.Slots.Update(obj);
        }
    }
}
