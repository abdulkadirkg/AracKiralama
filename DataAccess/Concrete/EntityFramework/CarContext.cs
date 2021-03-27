using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class CarContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDb;Database=RentACar;Trusted_Connection=true");
        }

        public DbSet<Car> Cars { set; get; }
        public DbSet<Color> Colors { set; get; }
        public DbSet<Brand> Brands { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Rental> Rentals { set; get; }
    }
}
