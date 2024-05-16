using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarBob.Models;

namespace BarBob.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User obj);

        IEnumerable<User> GetUserByFacultyIdAndRole(string roleName);

    }
}
