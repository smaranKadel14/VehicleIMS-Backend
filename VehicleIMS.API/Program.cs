using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Customers.Interfaces;
using VehicleIMS.Application.Customers.Services;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Application.Interfaces.IServices;
using VehicleIMS.Application.Services;
using VehicleIMS.Infrastructure.Persistence;
using VehicleIMS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());

// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPartRequestRepository, PartRequestRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPartRequestService, PartRequestService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IPartService, PartService>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();