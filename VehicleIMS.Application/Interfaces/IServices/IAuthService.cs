using VehicleIMS.Application.DTOs;

namespace VehicleIMS.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginRequest request);
        Task<AuthResponse> Register(RegisterRequest request);
    }
}
