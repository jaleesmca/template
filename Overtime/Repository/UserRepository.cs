using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NHibernate.Linq;
using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class UserRepository : IUser
    {
        private DBContext db;

        public IEnumerable<User> GetUsers => db.Users;

        public IEnumerable<EmpInfo> getUsersEmployeeDetails()
        {
            var quary=from u in db.Users
            join r in db.Roles
              on u.u_role_id equals r.r_id
            join d in db.Departments
              on u.u_dep_id equals d.d_id
            join k in db.Users
             on u.u_cre_by equals k.u_id
            where r.r_active_yn == "Y"
            select new EmpInfo
            {
                id = u.u_id,
                code = u.u_name,
                name = u.u_full_name
            };
            return quary;
        }

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
                                    u_full_name=u.u_full_name,
                                    u_password= u.u_password,
                                    u_role_id= u.u_role_id,
                                    u_role_description=r.r_description,
                                    u_dep_id=u.u_dep_id,
                                    u_active_yn=u.u_active_yn,
                                    u_is_admin=u.u_is_admin,
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
            var query = from u in db.Users
                        join r in db.Roles
                          on u.u_role_id equals r.r_id
                        join d in db.Departments
                           on u.u_dep_id equals d.d_id
                        join e in db.Users
                        on u.u_cre_by equals e.u_id
                        where u.u_name== u_name
                        select new User
                        {
                           u_id=u.u_id,
                           u_full_name=u.u_full_name,
                           u_name=u.u_name,
                           u_password=u.u_password,
                           u_is_admin=u.u_is_admin,
                           u_role_id=u.u_role_id,
                           u_role_description=r.r_description,
                           u_dep_id=u.u_dep_id,
                           u_active_yn=u.u_active_yn,
                           u_cre_by=u.u_cre_by,
                           u_cre_by_Name=e.u_name,
                           u_cre_date=u.u_cre_date,
                           u_dep_description=d.d_description

                        };

            return query.FirstOrDefault<User>();
           
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

        public IEnumerable<string> getUsersNames(string name)
        {
            /*  var query = from u in db.Users
                          where SqlMethods.Like(u.u_full_name, "%" + name + "%")
                          select u.u_full_name;*/


            var query = from u in db.Users
                        where u.u_full_name.Contains(name)
                        select u.u_name+"-"+u.u_full_name;
          


            return query;
        }
    }
}
