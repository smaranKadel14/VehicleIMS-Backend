using System.Threading.Tasks;

namespace VehicleIMS.Application.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string htmlBody);
    }
}
