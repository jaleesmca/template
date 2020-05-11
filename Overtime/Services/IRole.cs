using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    public interface IRole
    {
        IEnumerable<Role> GetRoles { get; }
        Role GetRole(int id);

        void Add(Role role);

        void Remove(int id);
    }
}
