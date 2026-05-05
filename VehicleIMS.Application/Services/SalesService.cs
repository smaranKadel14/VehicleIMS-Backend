using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Customers.Interfaces;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces.IServices;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services
{
    public class SalesService : ISalesService
    {
        private readonly IAppDbContext _context;
        private readonly IEmailService _emailService;

        public SalesService(IAppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<SalesInvoiceResponse> CreateSalesInvoiceAsync(CreateSalesInvoiceRequest request,
            CancellationToken cancellationToken = default)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);

            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            decimal subTotal = request.Items.Sum(i => i.Quantity * i.UnitPrice);
            decimal discountPercentage = subTotal > 5000 ? 10 : 0;
            decimal discountAmount = subTotal * (discountPercentage / 100);
            decimal finalTotal = subTotal - discountAmount;

            var invoice = new SalesInvoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMddHHmmss}",
                Date = DateTime.UtcNow,
                CustomerId = request.CustomerId,
                SubTotal = subTotal,
                DiscountPercentage = discountPercentage,
                DiscountAmount = discountAmount,
                FinalTotal = finalTotal,
                TotalAmount = finalTotal // For compatibility
            };

            foreach (var item in request.Items)
            {
                invoice.SalesInvoiceItems.Add(new SalesInvoiceItem
                {
                    PartId = item.PartId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            _context.SalesInvoices.Add(invoice);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToResponse(invoice);
        }

        public async Task<SalesInvoiceResponse?> GetSalesInvoiceByIdAsync(int id,
            CancellationToken cancellationToken = default)
        {
            var invoice = await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceItems)
                .ThenInclude(item => item.Part)
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

            return invoice == null ? null : MapToResponse(invoice);
        }

        public async Task<List<SalesInvoiceResponse>> GetAllSalesInvoicesAsync(
            CancellationToken cancellationToken = default)
        {
            var invoices = await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceItems)
                .ThenInclude(item => item.Part)
                .ToListAsync(cancellationToken);

            return invoices.Select(MapToResponse).ToList();
        }

        public async Task<SendInvoiceEmailResponse> SendInvoiceEmailAsync(int id, SendInvoiceEmailRequest request,
            CancellationToken cancellationToken = default)
        {
            var invoice = await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceItems)
                .ThenInclude(item => item.Part)
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

            if (invoice == null)
            {
                throw new KeyNotFoundException("Invoice not found.");
            }

            var recipientEmail = string.IsNullOrWhiteSpace(request.RecipientEmail)
                ? invoice.Customer?.Email
                : request.RecipientEmail.Trim();

            if (string.IsNullOrWhiteSpace(recipientEmail))
            {
                throw new InvalidOperationException("Customer email address is missing.");
            }

            var customerName = invoice.Customer != null
                ? $"{invoice.Customer.FirstName} {invoice.Customer.LastName}"
                : "Customer";

            var subject = string.IsNullOrWhiteSpace(request.Subject)
                ? $"Invoice {invoice.InvoiceNumber} from Vehicle IMS"
                : request.Subject.Trim();

            var introMessage = string.IsNullOrWhiteSpace(request.Message)
                ? "Thank you for your purchase. Your invoice details are below."
                : request.Message.Trim();

            var htmlBody = BuildInvoiceEmailBody(invoice, customerName, introMessage);

            await _emailService.SendAsync(recipientEmail, subject, htmlBody);

            return new SendInvoiceEmailResponse
            {
                Message = "Invoice email sent successfully.",
                SentTo = recipientEmail,
                InvoiceNumber = invoice.InvoiceNumber
            };
        }


        private static SalesInvoiceResponse MapToResponse(SalesInvoice invoice)
        {
            return new SalesInvoiceResponse
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                Date = invoice.Date,
                CustomerId = invoice.CustomerId,
                CustomerName = invoice.Customer != null
                    ? $"{invoice.Customer.FirstName} {invoice.Customer.LastName}"
                    : string.Empty,
                SubTotal = invoice.SubTotal,
                DiscountPercentage = invoice.DiscountPercentage,
                DiscountAmount = invoice.DiscountAmount,
                FinalTotal = invoice.FinalTotal,
                Items = invoice.SalesInvoiceItems.Select(item => new SalesInvoiceItemResponse
                {
                    Id = item.Id,
                    PartId = item.PartId,
                    PartName = item.Part?.Name ?? "Unknown Part",
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };
        }


        private static string BuildInvoiceEmailBody(SalesInvoice invoice, string customerName, string introMessage)
        {
            var rows = new StringBuilder();

            foreach (var item in invoice.SalesInvoiceItems)
            {
                rows.Append($@"
<tr>
    <td style='border:1px solid #ddd;padding:8px;'>{WebUtility.HtmlEncode(item.Part?.Name ?? "Unknown Part")}</td>
    <td style='border:1px solid #ddd;padding:8px;text-align:center;'>{item.Quantity}</td>
    <td style='border:1px solid #ddd;padding:8px;text-align:right;'>{item.UnitPrice:N2}</td>
    <td style='border:1px solid #ddd;padding:8px;text-align:right;'>{(item.Quantity * item.UnitPrice):N2}</td>
</tr>");
            }

            return $@"
<div style='font-family:Arial,sans-serif;color:#222;max-width:800px;margin:0 auto;'>
    <h2>Vehicle IMS Invoice</h2>
    <p>Hello {WebUtility.HtmlEncode(customerName)},</p>
    <p>{WebUtility.HtmlEncode(introMessage)}</p>

    <p>
        <strong>Invoice Number:</strong> {WebUtility.HtmlEncode(invoice.InvoiceNumber)}<br/>
        <strong>Date:</strong> {invoice.Date:dd MMM yyyy hh:mm tt}
    </p>

    <table style='border-collapse:collapse;width:100%;margin-top:16px;'>
        <thead>
            <tr style='background:#f4f4f4;'>
                <th style='border:1px solid #ddd;padding:8px;text-align:left;'>Part</th>
                <th style='border:1px solid #ddd;padding:8px;text-align:center;'>Qty</th>
                <th style='border:1px solid #ddd;padding:8px;text-align:right;'>Unit Price</th>
                <th style='border:1px solid #ddd;padding:8px;text-align:right;'>Total</th>
            </tr>
        </thead>
        <tbody>
            {rows}
        </tbody>
    </table>

    <div style='margin-top:20px;'>
        <p><strong>Sub Total:</strong> {invoice.SubTotal:N2}</p>
        <p><strong>Discount:</strong> {invoice.DiscountPercentage:N2}% ({invoice.DiscountAmount:N2})</p>
        <p><strong>Final Total:</strong> {invoice.FinalTotal:N2}</p>
    </div>

    <p style='margin-top:24px;'>Thank you for choosing Vehicle IMS.</p>
</div>";
        }
    }
}

