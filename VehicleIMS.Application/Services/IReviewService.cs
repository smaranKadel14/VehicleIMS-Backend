using VehicleIMS.Application.DTOs;

namespace VehicleIMS.Application.Services
{
    public interface IReviewService
    {
        Task<string> SubmitAsync(int customerId, ReviewDto dto);
    }
}