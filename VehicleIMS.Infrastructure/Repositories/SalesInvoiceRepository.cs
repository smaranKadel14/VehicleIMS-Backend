using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Domain.Models;
using VehicleIMS.Infrastructure.Persistence;

namespace VehicleIMS.Infrastructure.Repositories
{
    public class SalesInvoiceRepository : ISalesInvoiceRepository
    {
        private readonly AppDbContext _context;

        public SalesInvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SalesInvoice> CreateAsync(SalesInvoice invoice, List<SalesInvoiceItem> items)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            _context.SalesInvoices.Add(invoice);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                item.SalesInvoiceId = invoice.Id;
                _context.SalesInvoiceItems.Add(item);
            }
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            // Reload with navigation properties
            return await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceItems)
                    .ThenInclude(i => i.Part)
                .FirstAsync(i => i.Id == invoice.Id);
        }

        public async Task<SalesInvoice?> GetByIdAsync(int id) =>
            await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceItems)
                    .ThenInclude(i => i.Part)
                .FirstOrDefaultAsync(i => i.Id == id);

        public async Task<IEnumerable<SalesInvoice>> GetAllAsync() =>
            await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceItems)
                    .ThenInclude(i => i.Part)
                .ToListAsync();

        public async Task<IEnumerable<SalesInvoice>> GetByCustomerIdAsync(int customerId) =>
            await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceItems)
                    .ThenInclude(i => i.Part)
                .Where(i => i.CustomerId == customerId)
                .ToListAsync();

        public async Task<Part?> GetPartByIdAsync(int partId) =>
            await _context.Parts.FirstOrDefaultAsync(p => p.Id == partId);

        public async Task UpdatePartStockAsync(Part part)
        {
            _context.Parts.Update(part);
            await _context.SaveChangesAsync();
        }
    }
}