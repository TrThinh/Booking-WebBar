using BarBob.Data;
using BarBob.Repository.IRepository;
using BarBob.Models;

namespace BarBob.Repository
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        private ApplicationDbContext _db;

        public MenuRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Menu obj)
        {
            _db.Menus.Update(obj);
        }
    }
}