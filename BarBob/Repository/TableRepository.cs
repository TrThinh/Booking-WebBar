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

        public async Task<IEnumerable<Table>> GetTablesByTableTypeIdAsync(int tableTypeId)
        {
            return await dbSet.Where(t => t.TableTypeId == tableTypeId)
                              .Include(t => t.TableType)
                              .ToListAsync();
        }
    }
}
