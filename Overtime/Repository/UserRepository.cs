using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class UserRepository : IUser
    {
        private DBContext db;

        public IEnumerable<User> GetUsers => db.Users;

        public UserRepository(DBContext _db)
        {
            db = _db;
        }

       
        public IEnumerable<User> GetUsersList() {

           var query = from u in db.Users
                         join r in db.Roles
                           on u.u_role_id equals r.r_id
                         join d in db.Departments
                           on u.u_dep_id equals d.d_id
                         join k in db.Users
                          on u.u_cre_by equals k.u_id
                         where r.r_active_yn =="Y"
                         select new User{u_id=u.u_id,
                                    u_name= u.u_name,
                                    u_password= u.u_password,
                                    u_role_id= u.u_role_id,
                                    u_role_description=r.r_description,
                                    u_dep_id=u.u_dep_id,
                                    u_active_yn=u.u_active_yn,
                                    u_dep_description=d.d_description,
                                    u_cre_by= u.u_cre_by,
                                    u_cre_by_Name=k.u_name,
                                    u_cre_date= u.u_cre_date};
            
            return query;
            
        }



        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }


        public User GetUser(int id)
        {
            User user = db.Users.Find(id);
            return user;
        }

        public User getUserbyUsername(string u_name)
        {
            User user = db.Users.Where(s => s.u_name == u_name).FirstOrDefault();
            return user;
        }

        public void Remove(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public void Update(User user)
        {
            db.Users.Update(user);
            db.SaveChanges();
        }
    }
}
