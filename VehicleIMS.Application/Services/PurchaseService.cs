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
    public class PurchaseService : IPurchaseService
    {
        private readonly IAppDbContext _context;

        public PurchaseService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<PurchaseInvoiceResponse> CreatePurchaseInvoiceAsync(CreatePurchaseInvoiceRequest request, CancellationToken cancellationToken = default)
        {
            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(v => v.Id == request.VendorId, cancellationToken);

            if (vendor == null)
            {
                throw new Exception("Vendor not found.");
            }

            decimal subTotal = request.Items.Sum(i => i.Quantity * i.UnitPrice);
            decimal discountPercentage = subTotal > 5000 ? 10 : 0;
            decimal discountAmount = subTotal * (discountPercentage / 100);
            decimal finalTotal = subTotal - discountAmount;

            var invoice = new PurchaseInvoice
            {
                InvoiceNumber = $"PUR-{DateTime.UtcNow:yyyyMMddHHmmss}",
                Date = DateTime.UtcNow,
                VendorId = request.VendorId,
                SubTotal = subTotal,
                DiscountPercentage = discountPercentage,
                DiscountAmount = discountAmount,
                FinalTotal = finalTotal,
                TotalAmount = finalTotal // For compatibility
            };

            foreach (var item in request.Items)
            {
                invoice.PurchaseInvoiceItems.Add(new PurchaseInvoiceItem
                {
                    PartId = item.PartId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            _context.PurchaseInvoices.Add(invoice);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToResponse(invoice);
        }

        public async Task<PurchaseInvoiceResponse?> GetPurchaseInvoiceByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var invoice = await _context.PurchaseInvoices
                .Include(i => i.Vendor)
                .Include(i => i.PurchaseInvoiceItems)
                    .ThenInclude(item => item.Part)
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

            return invoice == null ? null : MapToResponse(invoice);
        }

        public async Task<List<PurchaseInvoiceResponse>> GetAllPurchaseInvoicesAsync(CancellationToken cancellationToken = default)
        {
            var invoices = await _context.PurchaseInvoices
                .Include(i => i.Vendor)
                .Include(i => i.PurchaseInvoiceItems)
                    .ThenInclude(item => item.Part)
                .ToListAsync(cancellationToken);

            return invoices.Select(MapToResponse).ToList();
        }

        private static PurchaseInvoiceResponse MapToResponse(PurchaseInvoice invoice)
        {
            return new PurchaseInvoiceResponse
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                Date = invoice.Date,
                VendorId = invoice.VendorId,
                VendorName = invoice.Vendor?.Name ?? "Unknown Vendor",
                SubTotal = invoice.SubTotal,
                DiscountPercentage = invoice.DiscountPercentage,
                DiscountAmount = invoice.DiscountAmount,
                FinalTotal = invoice.FinalTotal,
                Items = invoice.PurchaseInvoiceItems.Select(item => new PurchaseInvoiceItemResponse
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
