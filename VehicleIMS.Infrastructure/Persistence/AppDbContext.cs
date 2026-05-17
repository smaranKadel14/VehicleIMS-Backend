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
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<SalesInvoice> SalesInvoices { get; set; }
    public DbSet<SalesInvoiceItem> SalesInvoiceItems { get; set; }
    public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
    public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Part> Parts { get; set; }
    public DbSet<PartRequest> PartRequests { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Staff> Staff { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Vehicle>().ToTable("Vehicles");
        modelBuilder.Entity<Appointment>().ToTable("Appointments");
        modelBuilder.Entity<SalesInvoice>().ToTable("SalesInvoices");
        modelBuilder.Entity<SalesInvoiceItem>().ToTable("SalesInvoiceItems");
        modelBuilder.Entity<PurchaseInvoice>().ToTable("PurchaseInvoices");
        modelBuilder.Entity<PurchaseInvoiceItem>().ToTable("PurchaseInvoiceItems");
        modelBuilder.Entity<Vendor>().ToTable("Vendors");
        modelBuilder.Entity<Part>().ToTable("Parts");
        modelBuilder.Entity<PartRequest>().ToTable("PartRequests");
        modelBuilder.Entity<Review>().ToTable("Reviews");
        modelBuilder.Entity<Staff>().ToTable("Staff");
    }
}