using Microsoft.EntityFrameworkCore;
using System.Numerics;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Part> Parts { get; set; }
}