﻿using BarBob.Models;
using System.Linq.Expressions;

namespace BarBob.Repository.IRepository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> GetFirstOrDefaultAsync(Expression<Func<Booking, bool>> filter, string includeProperties);
        Task<Booking> GetFirstOrDefaultAsync(Expression<Func<Booking, bool>> filter);
        void Update(Booking obj);
    }
}
