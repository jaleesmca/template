using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    public  interface IMenu
    {
        Menu GetMenu(int id);

        IEnumerable<Menu> GetMenus { get; }

        void Add(Menu menu);

        void Remove(int id);

        void Update(Menu menu);
        IEnumerable<Menu> getMenulistByRoleAndType(int role,string type);
        IEnumerable<Menu> getMenuListNotMappedByRoleAndType(int role, string type);
        IEnumerable<Menu> GetMenuList(string type);
        IEnumerable<Menu> getMenulistByRoleAndTypeAndParrent(int u_role_id, string v, int m_id);
    }
}
