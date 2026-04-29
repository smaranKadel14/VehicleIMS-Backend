using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Customers.Interfaces;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Part> Parts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
        modelBuilder.Entity<Vendor>().ToTable("Vendors");
        modelBuilder.Entity<Part>().ToTable("Parts");
    }
}
