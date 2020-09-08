using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Helper
{
    public class TempHelper : IdentityDbContext
    {
        public TempHelper(DbContextOptions<TempHelper> options) : base(options)
        {
        }

        public DbSet<EmployeeModel> EmployeeInfo { get; set; }
        public DbSet<DepartmentModel> DepartmentInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
