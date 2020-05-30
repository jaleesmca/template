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
        IEnumerable<EmpInfo> getUsersEmployeeDetails();

        IEnumerable<User> GetUsersList();
        User GetUser(int id);

        void Add(User user);

        void Remove(int id);
        User getUserbyUsername(string u_name);
        void Update(User user);
        IEnumerable<string> getUsersNames(string name);
    }
}
