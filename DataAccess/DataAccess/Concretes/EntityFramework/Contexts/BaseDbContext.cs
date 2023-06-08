using Core.Entities.Concretes;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework.Contexts
{
    public class BaseDbContext : DbContext
    {
        private IConfiguration _configuration;

        public BaseDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<RentalLocation> RentalLocations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<OperationClaim> OperationClaims  { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Car>()
                .HasKey(i => i.Id);
          
            modelBuilder.Entity<Car>().Property(i => i.Plate).HasColumnName("Plate");

            modelBuilder.Entity<Brand>().HasKey(i => i.Id);

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.HasMany(i => i.Cars);
                entity.Property(i => i.Name).HasColumnName("Name");

                // seed data
                List<Color> colors = new List<Color>();
                int id = 0;
                colors.Add(new Color() { Id = ++id, Name = "Kırmızı" });
                colors.Add(new Color() { Id = ++id, Name = "Mavi" });
                entity.HasData(colors);
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                //entity.HasOne(i => i.RentalEndLocation);
                //entity.HasOne(i => i.RentalStartLocation);
            });

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TKI"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
