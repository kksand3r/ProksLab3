using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proks_API.Models;

namespace Proks_API.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FuelType>().HasData(
                new FuelType { Id = 1, Name = "Бензин" },
                new FuelType { Id = 2, Name = "Дизель" },
                new FuelType { Id = 3, Name = "Електричний" }
            );

            modelBuilder.Entity<TransmissionType>().HasData(
                new TransmissionType { Id = 1, Name = "Механіка" },
                new TransmissionType { Id = 2, Name = "Автомат" }
            );

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Audi" },
                new Brand { Id = 2, Name = "Mercedes-Benz" },
                new Brand { Id = 3, Name = "BMW" },
                new Brand { Id = 4, Name = "Volkswagen" }
            );

            modelBuilder.Entity<Car>()
                .HasOne(c => c.FuelType)
                .WithMany(f => f.Cars)
                .HasForeignKey(c => c.FuelTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.TransmissionType)
                .WithMany(t => t.Cars)
                .HasForeignKey(c => c.TransmissionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Brand)
                .WithMany(t => t.Cars)
                .HasForeignKey(c => c.BrandId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
