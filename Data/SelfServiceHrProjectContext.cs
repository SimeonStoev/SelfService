using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SelfServiceHrProject.Models;

namespace SelfServiceHrProject.Data
{
    public class SelfServiceHrProjectContext : DbContext
    {
        public SelfServiceHrProjectContext (DbContextOptions<SelfServiceHrProjectContext> options)
            : base(options)
        {
        }

        public DbSet<SelfServiceHrProject.Models.Employees> Employees { get; set; }
        public DbSet<SelfServiceHrProject.Models.Address> Address { get; set; }
        public DbSet<SelfServiceHrProject.Models.Organisations> Organisations { get; set; }
        public DbSet<SelfServiceHrProject.Models.Positions> Positions { get; set; }
        public DbSet<SelfServiceHrProject.Models.Salary> Salary { get; set; }
        public DbSet<SelfServiceHrProject.Models.SystemUsers> SystemUsers { get; set; }

        public DbSet<SelfServiceHrProject.Models.LeaveReasons> LeaveReasons { get; set; }

        public DbSet<SelfServiceHrProject.Models.RemainingLeaves> RemainingLeaves { get; set; }

        public DbSet<SelfServiceHrProject.Models.Leaves> Leaves { get; set; }

    }
}
