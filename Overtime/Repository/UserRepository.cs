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

        public UserRepository(DBContext _db)
        {
            db = _db;
        }

        public IEnumerable<User> GetUsers => db.Users;

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
    }
}
