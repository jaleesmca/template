using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class DBContext : DBContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
    }
}
