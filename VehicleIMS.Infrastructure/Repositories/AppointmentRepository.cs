using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Domain.Models;
using VehicleIMS.Infrastructure.Persistence;

namespace VehicleIMS.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context) { _context = context; }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<IEnumerable<Appointment>> GetByCustomerIdAsync(int customerId) =>
            await _context.Appointments
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();
    }
}