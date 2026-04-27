using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentService(IAppointmentRepository repo) { _repo = repo; }

        public async Task<string> BookAsync(int customerId, AppointmentDto dto)
        {
            if (dto.AppointmentDate <= DateTime.UtcNow)
                return "Appointment date must be in the future.";

            var appointment = new Appointment
            {
                CustomerId = customerId,
                VehicleId = dto.VehicleId,
                AppointmentDate = dto.AppointmentDate,
                Notes = dto.Notes,
                Status = "Scheduled"
            };
            await _repo.AddAsync(appointment);
            return "Appointment booked successfully.";
        }

        public async Task<IEnumerable<Appointment>> GetByCustomerIdAsync(int customerId)
        {
            return await _repo.GetByCustomerIdAsync(customerId);
        }
    }
}