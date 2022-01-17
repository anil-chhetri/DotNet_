using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinqExamples
{
    class LinqDbContext : DbContext
    {
        public LinqDbContext(DbContextOptions<LinqDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Department>().HasData(
                new Department { ID = 1, Location = "New York", Name = "IT" },
                new Department { ID = 2, Location = "London", Name = "HR" },
                new Department { ID = 3, Location = "Sydney", Name = "Payroll" }
                );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { ID = 1, FirstName = "Mark", LastName = "Hasting", Gender = "Male", Salary = 60000.0M, DepartmentID = 1 },
                new Employee { ID = 2, FirstName = "Steve", LastName = "Pound", Gender = "Male", Salary = 45000.0M, DepartmentID = 3 },
                new Employee { ID = 3, FirstName = "Ben", LastName = "Hoskins", Gender = "Male", Salary = 70000.0M, DepartmentID = 1 },
                new Employee { ID = 4, FirstName = "Philp", LastName = "Hastings", Gender = "Male", Salary = 45000.0M, DepartmentID = 2 },
                new Employee { ID = 5, FirstName = "Mary", LastName = "Lambeth", Gender = "Female", Salary = 30000.0M, DepartmentID = 2 },
                new Employee { ID = 6, FirstName = "Valarie", LastName = "Vikings", Gender = "Female", Salary = 35000.0M, DepartmentID = 3 },
                new Employee { ID = 7, FirstName = "John", LastName = "Stanmore", Gender = "Male", Salary = 80000.0M, DepartmentID = 1 }
                );

            base.OnModelCreating(modelBuilder);

            modelBuilder.
        }
    }


    class Department
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

    }

    class Employee
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Salary { get; set; }

        public int DepartmentID { get; set; }

        public Department Department { get; set; }

    }


}
