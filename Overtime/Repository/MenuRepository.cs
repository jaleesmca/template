using Microsoft.EntityFrameworkCore;
using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

        public IEnumerable<Menu> GetMenuList(string type)
        {
            var quary = from u in db.Menus
                        where u.m_type == type
                        select u;
            return quary;
        }

        public IEnumerable<Menu> getMenulistByRoleAndType(int role, string type)
        {
           
            var quary = from u in db.Menus
                        join r in db.RoleMenus
                          on u.m_id equals r.rm_menu_id
                        join d in db.Roles
                        on r.rm_role_id equals d.r_id
                        where r.rm_role_id == role && u.m_type == type
                        select u;
           
            return quary;
        }

        public IEnumerable<Menu> getMenulistByRoleAndTypeAndParrent(int role, string type, int m_id)
        {
            var query = from u in db.Menus
                        join r in db.RoleMenus
                          on u.m_id equals r.rm_menu_id
                        join d in db.Roles
                        on r.rm_role_id equals d.r_id
                        where r.rm_role_id == role && u.m_type == type && u.m_parrent_id== m_id
                        select u;
            return query;
        }

        public IEnumerable<Menu> getMenuListNotMappedByRoleAndType(int role, string type)
        {

            List<Menu> result = new List<Menu>();
            var conn = db.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string query = @"select * from Menus 
                    where m_id not in (select rm_menu_id from RoleMenus where rm_role_id='"+ role + @"') and m_type='"+ type + @"'";
                    command.CommandText = query;
                    DbDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Menu menu = new Menu();
                            menu.m_id = int.Parse(reader["m_id"].ToString());
                            menu.m_description = reader["m_description"].ToString();
                            menu.m_desc_to_show = reader["m_desc_to_show"].ToString();
                            menu.m_link = reader["m_link"].ToString();
                            if(!reader["m_parrent_id"].ToString().Equals(""))  menu.m_parrent_id = int.Parse(reader["m_parrent_id"].ToString());
                            menu.m_type = reader["m_type"].ToString();
                            menu.m_cre_by = int.Parse(reader["m_cre_by"].ToString());
                            menu.m_cre_date = DateTime.Parse(reader["m_cre_date"].ToString());
                            result.Add(menu);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
           
            return result;
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
