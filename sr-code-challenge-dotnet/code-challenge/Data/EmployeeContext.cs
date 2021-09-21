using challenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Compensation> Compensations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set the Primary key for Compensation, We can achieve the same with [Key] Attribute
            modelBuilder.Entity<Compensation>(entity =>
            {
                entity.HasKey(z => z.EmployeeId);
            });

            //Establish a one-to-one relationship, We can use this, for EF to throw error when we add multiple compensations
            //TODO: for some reason, this is not working as expected, its creating the Compensation even there is no employee existed
            //Need to look for the solution
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne<Compensation>(x => x.Compensation)
                    .WithOne(s => s.Employee)
                    .HasForeignKey<Compensation>(s => s.EmployeeId);
            });



        }
    }
}
