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
    public class TableRepository : Repository<Table>, ITableRepository
    { 
        private ApplicationDbContext _db;

        public TableRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Table obj)
        {
            _db.Tables.Update(obj);
        }
    }
}
