using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Domain.Models;
using VehicleIMS.Infrastructure.Persistence;

namespace VehicleIMS.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;
        public ReviewRepository(AppDbContext context) { _context = context; }

        public async Task<Review> AddAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> HasCustomerAlreadyReviewedAsync(int customerId) =>
            await _context.Reviews.AnyAsync(r => r.CustomerId == customerId);
    }
}