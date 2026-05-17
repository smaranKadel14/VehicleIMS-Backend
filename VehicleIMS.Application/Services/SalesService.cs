using System;
using System.Collections.Generic;
using System.Linq;
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

        public SalesService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<SalesInvoiceResponse> CreateSalesInvoiceAsync(CreateSalesInvoiceRequest request, CancellationToken cancellationToken = default)
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

        public async Task<SalesInvoiceResponse?> GetSalesInvoiceByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var invoice = await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceItems)
                    .ThenInclude(item => item.Part)
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

            return invoice == null ? null : MapToResponse(invoice);
        }

        public async Task<List<SalesInvoiceResponse>> GetAllSalesInvoicesAsync(CancellationToken cancellationToken = default)
        {
            var invoices = await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceItems)
                    .ThenInclude(item => item.Part)
                .ToListAsync(cancellationToken);

            return invoices.Select(MapToResponse).ToList();
        }

        private static SalesInvoiceResponse MapToResponse(SalesInvoice invoice)
        {
            return new SalesInvoiceResponse
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                Date = invoice.Date,
                CustomerId = invoice.CustomerId,
                CustomerName = invoice.Customer != null ? $"{invoice.Customer.FirstName} {invoice.Customer.LastName}" : string.Empty,
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
    }
}
