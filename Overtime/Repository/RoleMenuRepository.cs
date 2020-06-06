using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class RoleMenuRepository : IRoleMenu
    {

        private readonly DBContext db;

        public RoleMenuRepository(DBContext _db)
        {
            db = _db;
        }

        public IEnumerable<RoleMenu> GetRoleMenus => db.RoleMenus;

        public void Add(RoleMenu roleMenu)
        {
            db.RoleMenus.Add(roleMenu);
            db.SaveChanges();
        }

        public int getCountOfRoleMenuByRoleAndType(int role, string type)
        {
            var count = (from u in db.RoleMenus
                              join d in db.Menus
                               on u.rm_menu_id equals d.m_id
                              where u.rm_role_id==role &&d.m_type==type
                              select u).Count();
            return count;
        }

        public RoleMenu GetMenuRole(int id)
        {
            
            return db.RoleMenus.Find(id);
        }

        public RoleMenu GetRoleMenusByRoleAndMenu(int role, int item)
        {
            RoleMenu roleMenu = (from u in db.RoleMenus
                                where u.rm_role_id== role && u.rm_menu_id==item 
                                select u).FirstOrDefault();
            return roleMenu;
        }


        public void Remove(int id)
        {
            RoleMenu roleMenu = db.RoleMenus.Find(id);
            db.RoleMenus.Remove(roleMenu);
            db.SaveChanges();
        }

        public void RemoveAllRoleMenu(int role, string type)
        {
            var x = (from u in db.RoleMenus
                     join d in db.Menus
                     on u.rm_menu_id equals d.m_id
                     where u.rm_role_id == role && d.m_type == type
                     select u);
            foreach (var item in x)
            {
                db.RoleMenus.Remove(item);
                
            }
            db.SaveChanges();
        }

        public void Update(RoleMenu roleMenu)
        {
            db.Update(roleMenu);
            db.SaveChanges();
        }
    }
}
