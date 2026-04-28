using VehicleIMS.Domain.Models;
namespace VehicleIMS.Application.Interfaces;

public interface IAppointmentRepository
{
    Task<Appointment> AddAsync(Appointment appointment);
    Task<IEnumerable<Appointment>> GetByCustomerIdAsync(int customerId);
}

