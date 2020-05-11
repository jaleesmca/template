using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    public interface IUser
    {
        IEnumerable<User> GetUsers { get;}
        User GetUser(int id);

        void Add(User user);

        void Remove(int id);
    }
}
