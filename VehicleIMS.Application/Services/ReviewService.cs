using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Application.Interfaces.IServices;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repo;
        public ReviewService(IReviewRepository repo) { _repo = repo; }

        public async Task<string> SubmitAsync(int customerId, ReviewDto dto)
        {
            var alreadyReviewed = await _repo.HasCustomerAlreadyReviewedAsync(customerId);
            if (alreadyReviewed)
                return "You have already submitted a review.";

            var review = new Review
            {
                CustomerId = customerId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                PartId = dto.PartId,
                Date = DateTime.UtcNow
            };
            await _repo.AddAsync(review);
            return "Review submitted. Thank you!";
        }
    }
}