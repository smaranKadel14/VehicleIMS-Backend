using VehicleIMS.Domain.Models;
namespace VehicleIMS.Application.Interfaces;

public interface IReviewRepository
{
    Task<Review> AddAsync(Review review);
    Task<bool> HasCustomerAlreadyReviewedAsync(int customerId);
}
