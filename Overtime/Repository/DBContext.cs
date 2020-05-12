using Microsoft.EntityFrameworkCore;
using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<User> Users {get;set;}
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<WorkflowDetail> WorkflowDetails { get; set; }
        public DbSet<WorkflowTracker> workflowTrackers { get; set; }
        public DbSet<OverTimeRequest> OverTimeRequest { get; set; }
    }
}
