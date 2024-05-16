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
    public class BookingRequestRepository : Repository<BookingRequest>, IBookingRequestRepository
    { 
        private ApplicationDbContext _db;

        public BookingRequestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(BookingRequest obj)
        {
            _db.BookingRequests.Update(obj);
        }
    }
}
