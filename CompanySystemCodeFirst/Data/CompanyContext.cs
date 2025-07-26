using CompanySystemCodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanySystemCodeFirst.Data
{
    public class CompanyContext : DbContext
    {
       

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<EmployeeProject> EmployeeProjects => Set<EmployeeProject>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=F:\ITI\DAY_14_EF\CompanySystemCodeFirst\Company.db");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });
        }
    }
}
