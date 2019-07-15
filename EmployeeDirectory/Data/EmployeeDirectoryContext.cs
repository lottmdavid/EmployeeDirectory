using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Data
{
    public class EmployeeDirectoryContext : DbContext
    {
        public EmployeeDirectoryContext (DbContextOptions<EmployeeDirectoryContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeDirectory.Models.Department> Department { get; set; }

        public DbSet<EmployeeDirectory.Models.Employee> Employee { get; set; }
    }
}
