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
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    { 
        private ApplicationDbContext _db;

        public FeedbackRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Feedback> GetAll(Expression<Func<Feedback, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Feedback> query = _db.Feedbacks;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query;
        }

        public void Add(Feedback feedback)
        {
            _db.Feedbacks.Add(feedback);
        }

        public void Update(Feedback obj)
        {
            _db.Feedbacks.Update(obj);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
