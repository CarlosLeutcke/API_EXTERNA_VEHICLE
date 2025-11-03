using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using VehicleApi.Models;

namespace VehicleApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>(b =>
            {
                b.HasKey(v => v.Id);
                b.Property(v => v.Plate).IsRequired();
                b.HasIndex(v => v.Plate).IsUnique();
                b.Property(v => v.Make).IsRequired();
                b.Property(v => v.Model).IsRequired();
                b.Property(v => v.Year).IsRequired();
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
