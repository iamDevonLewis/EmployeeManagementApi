using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Address)
            .WithOne()
            .HasForeignKey<Employee>(e => e.AddressId)
            .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Address>().HasData(
            //    new Address
            //    {
            //        Id = 1,
            //        City = "Saint Louis",
            //        State = "Missouri",
            //        Street = "8026 Martin Blvd",
            //        Zipcode = 63301
            //    },
            //    new Address
            //    {
            //        Id = 2,
            //        City = "Miami",
            //        State = "Florida",
            //        Street = "1916 Miami Gardens",
            //        Zipcode = 33169
            //    });

            //modelBuilder.Entity<Employee>().HasData(
            //    new Employee
            //    {
            //        Id = 1,
            //        FirstName = "Chris",
            //        LastName = "Davis",
            //        Department = "IT",
            //        AddressId = 1
            //    },
            //    new Employee
            //    {
            //        Id = 2,
            //        FirstName = "John",
            //        LastName = "Smith",
            //        Department = "Accounting",
            //        AddressId = 2
            //    });
        }
    }
}
