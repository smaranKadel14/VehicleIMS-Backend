using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer?> GetByEmailAsync(string email);
    Task<bool> UsernameExistsAsync(string username);
    Task<Customer> RegisterAsync(User user, Customer customer);
    Task UpdateAsync(Customer customer);
    Task AddVehicleAsync(Vehicle vehicle);
    Task UpdateVehicleAsync(Vehicle vehicle);
    Task<Vehicle?> GetVehicleByIdAsync(int vehicleId);
    Task<IEnumerable<Vehicle>> GetVehiclesByCustomerIdAsync(int customerId);
}
