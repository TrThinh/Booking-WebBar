﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarBob.Data;
using BarBob.Repository.IRepository;

namespace BarBob.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IUserRepository User { get; private set; }
        public IFeedbackRepository Feedback { get; private set; }
        public IBookingRepository Booking { get; private set; }
        public IPaymentRepository Payment { get; private set; }
        public ITableRepository Table { get; private set; }
        public ITableTypeRepository TableType { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            Feedback = new FeedbackRepository(_db);
            Booking = new BookingRepository(_db);
            Payment = new PaymentRepository(_db);
            Table = new TableRepository(_db);
            TableType = new TableTypeRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}