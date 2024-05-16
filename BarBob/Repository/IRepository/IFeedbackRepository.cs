using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        void Update(Feedback obj);
    }
}
