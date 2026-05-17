using System.ComponentModel.DataAnnotations;

namespace VehicleIMS.Application.DTOs
{
    public class SendInvoiceEmailRequest
    {
        [EmailAddress]
        public string? RecipientEmail { get; set; }

        [MaxLength(200)]
        public string? Subject { get; set; }

        [MaxLength(1000)]
        public string? Message { get; set; }
    }

    public class SendInvoiceEmailResponse
    {
        public string Message { get; set; } = string.Empty;
        public string SentTo { get; set; } = string.Empty;
        public string InvoiceNumber { get; set; } = string.Empty;
    }
}