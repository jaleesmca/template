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

        public RoleMenu GetMenuRole(int id)
        {
            
            return db.RoleMenus.Find(id);
        }

        public void Remove(int id)
        {
            RoleMenu roleMenu = db.RoleMenus.Find(id);
            db.RoleMenus.Remove(roleMenu);
            db.SaveChanges();
        }

        public void Update(RoleMenu roleMenu)
        {
            db.Update(roleMenu);
            db.SaveChanges();
        }
    }
}
