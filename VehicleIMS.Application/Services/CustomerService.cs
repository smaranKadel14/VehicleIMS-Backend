using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<(bool Success, string Message, int? CustomerId)> RegisterAsync(RegisterCustomerDto dto)
        {
            var existingCustomer = await _repo.GetByEmailAsync(dto.Email);
            if (existingCustomer != null)
                return (false, "Email is already registered.", null);

            var usernameExists = await _repo.UsernameExistsAsync(dto.Username);
            if (usernameExists)
                return (false, "Username is already taken.", null);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Role = "Customer",
                CreatedAt = DateTime.UtcNow
            };

            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            var createdCustomer = await _repo.RegisterAsync(user, customer);
            return (true, "Customer registered successfully.", createdCustomer.Id);
        }

        public async Task<string> UpdateProfileAsync(int customerId, UpdateProfileDto dto)
        {
            var customer = await _repo.GetByIdAsync(customerId);
            if (customer == null)
                return "Customer not found.";

            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Phone = dto.Phone;
            customer.Address = dto.Address;

            await _repo.UpdateAsync(customer);
            return "Profile updated successfully.";
        }

        public async Task<string> AddVehicleAsync(int customerId, VehicleDto dto)
        {
            var customer = await _repo.GetByIdAsync(customerId);
            if (customer == null)
                return "Customer not found.";

            var vehicle = new Vehicle
            {
                CustomerId = customerId,
                Make = dto.Make,
                Model = dto.Model,
                Year = dto.Year,
                VIN = dto.VIN,
                LicensePlate = dto.LicensePlate
            };

            await _repo.AddVehicleAsync(vehicle);
            return "Vehicle added successfully.";
        }

        public async Task<string> UpdateVehicleAsync(int vehicleId, VehicleDto dto)
        {
            var vehicle = await _repo.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
                return "Vehicle not found.";

            vehicle.Make = dto.Make;
            vehicle.Model = dto.Model;
            vehicle.Year = dto.Year;
            vehicle.VIN = dto.VIN;
            vehicle.LicensePlate = dto.LicensePlate;

            await _repo.UpdateVehicleAsync(vehicle);
            return "Vehicle updated successfully.";
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync(int customerId)
        {
            return await _repo.GetVehiclesByCustomerIdAsync(customerId);
        }
    }
}
