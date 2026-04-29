using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Application.Interfaces.IServices;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ISalesInvoiceRepository _repo;

        public SalesInvoiceService(ISalesInvoiceRepository repo)
        {
            _repo = repo;
        }

        public async Task<SalesInvoiceResponse> CreateInvoiceAsync(CreateSalesInvoiceDto dto)
        {
            var invoiceItems = new List<SalesInvoiceItem>();
            decimal totalAmount = 0;

            // Validate parts and calculate total
            foreach (var itemDto in dto.Items)
            {
                var part = await _repo.GetPartByIdAsync(itemDto.PartId);
                if (part == null)
                    throw new InvalidOperationException($"Part with ID {itemDto.PartId} not found.");

                if (part.StockQuantity < itemDto.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for part '{part.Name}'. Available: {part.StockQuantity}");

                invoiceItems.Add(new SalesInvoiceItem
                {
                    PartId = part.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = part.Price
                });

                totalAmount += part.Price * itemDto.Quantity;

                // Deduct stock
                part.StockQuantity -= itemDto.Quantity;
                await _repo.UpdatePartStockAsync(part);
            }

            var invoice = new SalesInvoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMddHHmmss}",
                Date = DateTime.UtcNow,
                TotalAmount = totalAmount,
                CustomerId = dto.CustomerId
            };

            var created = await _repo.CreateAsync(invoice, invoiceItems);
            return MapToResponse(created);
        }

        public async Task<SalesInvoiceResponse?> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _repo.GetByIdAsync(id);
            if (invoice == null) return null;
            return MapToResponse(invoice);
        }

        public async Task<IEnumerable<SalesInvoiceResponse>> GetAllInvoicesAsync()
        {
            var invoices = await _repo.GetAllAsync();
            return invoices.Select(MapToResponse);
        }

        public async Task<IEnumerable<SalesInvoiceResponse>> GetInvoicesByCustomerIdAsync(int customerId)
        {
            var invoices = await _repo.GetByCustomerIdAsync(customerId);
            return invoices.Select(MapToResponse);
        }

        private static SalesInvoiceResponse MapToResponse(SalesInvoice invoice)
        {
            var items = invoice.SalesInvoiceItems.Select(i => new SalesInvoiceItemResponse
            {
                Id = i.Id,
                PartId = i.PartId,
                PartName = i.Part?.Name ?? string.Empty,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();

            var subTotal = items.Sum(i => i.TotalPrice);

            return new SalesInvoiceResponse
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                Date = invoice.Date,
                CustomerId = invoice.CustomerId,
                CustomerName = invoice.Customer?.FirstName + " " + invoice.Customer?.LastName,
                SubTotal = subTotal,
                DiscountPercentage = 0,
                DiscountAmount = 0,
                FinalTotal = subTotal,
                Items = items
            };
        }
    }
}