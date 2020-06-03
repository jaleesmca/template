using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    public interface IRoleMenu
    {
        RoleMenu GetMenuRole(int id);

        IEnumerable<RoleMenu> GetRoleMenus { get; }

        void Add(RoleMenu roleMenu);

        void Remove(int id);

        void Update(RoleMenu roleMenu);
    }
}
