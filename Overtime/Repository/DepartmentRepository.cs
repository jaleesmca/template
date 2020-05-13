using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class DepartmentRepository : IDepartment
    {
        private DBContext db;

        public DepartmentRepository(DBContext _db)
        {
            db = _db;
        }

        public IEnumerable<Department> GetDepartments => db.Departments;

        public void Add(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
        }

        public Department GetDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            return department;
        }

        public void Remove(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
           
        }

        public void Update(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
