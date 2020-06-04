using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class MenuRepository : IMenu
    {
        private readonly DBContext db;

        public MenuRepository(DBContext _db)
        {
            db = _db;
        }

        public IEnumerable<Menu> GetMenus => db.Menus;

        public void Add(Menu menu)
        {
            db.Menus.Add(menu);
            db.SaveChanges();
        }

        public Menu GetMenu(int id)
        {
            Menu menu = db.Menus.Find(id);
            return menu;
        }

        public IEnumerable<Menu> getMenulistByRoleAndType(int role, string type)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
            db.SaveChanges();
        }

        public void Update(Menu menu)
        {
            db.Update(menu);
            db.SaveChanges();
        }
        
    }
}
