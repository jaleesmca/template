using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    public interface IUserDepartment
    {
        IEnumerable<UserDepartment> GetUserDepartments { get; }
        UserDepartment GetUserDepartment(int id);

        void Add(UserDepartment department);

        void Remove(int id);

        void Update(UserDepartment department);
        bool getIsExistOrNot(int ud_id, int ud_depart_id);
    }
}
