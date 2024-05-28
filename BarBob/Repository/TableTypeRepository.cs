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
    public class TableTypeRepository : Repository<TableType>, ITableTypeRepository
    { 
        private ApplicationDbContext _db;

        public TableTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TableType obj)
        {
            _db.TableTypes.Update(obj);
        }
    }
}
