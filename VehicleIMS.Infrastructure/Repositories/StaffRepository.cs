using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Domain.Models;
using VehicleIMS.Infrastructure.Persistence;

namespace VehicleIMS.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly AppDbContext _context;

        public StaffRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Staff> RegisterAsync(User user, Staff staff)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            staff.UserId = user.Id;
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            // Load navigation property before returning
            await _context.Entry(staff).Reference(s => s.User).LoadAsync();
            return staff;
        }

        public async Task<Staff?> GetByIdAsync(int id) =>
            await _context.Staff
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<Staff?> GetByEmailAsync(string email) =>
            await _context.Staff
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Email == email);

        public async Task<IEnumerable<Staff>> GetAllAsync() =>
            await _context.Staff
                .Include(s => s.User)
                .ToListAsync();

        public async Task UpdateAsync(Staff staff)
        {
            _context.Staff.Update(staff);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username) =>
            await _context.Users.AnyAsync(u => u.Username == username);
    }
}