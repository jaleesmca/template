using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    interface Role
    {
        IEnumerable<Role> GetRoles { get; set; }
        Role GetRole(int id);

        void Add(Role role);

        void Remove(int id);
    }
}
