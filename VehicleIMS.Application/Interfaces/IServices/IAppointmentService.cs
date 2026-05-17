using VehicleIMS.Application.DTOs;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Interfaces.IServices
{
    public interface IAppointmentService
    {
        Task<string> BookAsync(int customerId, AppointmentDto dto);
        Task<IEnumerable<Appointment>> GetByCustomerIdAsync(int customerId);
    }
}
