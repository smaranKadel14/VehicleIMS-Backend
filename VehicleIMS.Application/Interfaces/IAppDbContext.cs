using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Customers.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Vehicle> Vehicles { get; }
    DbSet<Appointment> Appointments { get; }
    DbSet<SalesInvoice> SalesInvoices { get; }
    DbSet<SalesInvoiceItem> SalesInvoiceItems { get; }
    DbSet<Vendor> Vendors { get; }
    DbSet<Part> Parts { get; }
    DbSet<Staff> Staff { get; } 
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
