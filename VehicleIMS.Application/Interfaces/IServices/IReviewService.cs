using VehicleIMS.Application.DTOs;

namespace VehicleIMS.Application.Interfaces.IServices
{
    public interface IReviewService
    {
        Task<string> SubmitAsync(int customerId, ReviewDto dto);
    }
}