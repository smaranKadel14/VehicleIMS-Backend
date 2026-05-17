using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Domain.Models;
using VehicleIMS.Infrastructure.Persistence;

namespace VehicleIMS.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByEmailAsync(string email) =>
            await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);

        public async Task<bool> UsernameExistsAsync(string username) =>
            await _context.Users.AnyAsync(u => u.Username == username);

        public async Task<Customer?> GetByIdAsync(int id) =>
            await _context.Customers
                .Include(c => c.Vehicles)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Customer> RegisterAsync(User user, Customer customer)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            customer.UserId = user.Id;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            return customer;
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(int vehicleId) =>
            await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == vehicleId);

        public async Task<IEnumerable<Vehicle>> GetVehiclesByCustomerIdAsync(int customerId) =>
            await _context.Vehicles.Where(v => v.CustomerId == customerId).ToListAsync();
    }
}
