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
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Add(Booking booking)
        {
            _db.Bookings.Add(booking);
        }

        public async Task<Booking> GetFirstOrDefaultAsync(Expression<Func<Booking, bool>> filter)
        {
            return await _db.Bookings.FirstOrDefaultAsync(filter);
        }

        public void Update(Booking obj)
        {
            _db.Bookings.Update(obj);
        }
    }
}
