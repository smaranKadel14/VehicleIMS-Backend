using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Customers.Interfaces;
using VehicleIMS.Application.Customers.Services;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Application.Interfaces.IServices;
using VehicleIMS.Application.Services;
using VehicleIMS.Infrastructure.Persistence;
using VehicleIMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VehicleIMS.Application.Interfaces.IServices;
using VehicleIMS.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
    });
});

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IPartRequestRepository, PartRequestRepository>();
builder.Services.AddScoped<IPartRequestService, PartRequestService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ISalesInvoiceRepository, SalesInvoiceRepository>();
builder.Services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IPartService, PartService>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Health Check
app.MapGet("/api/health", () => Results.Ok(new { Status = "Healthy", Time = DateTime.UtcNow }));

// Seed Admin User
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // This ensures the database and tables are created
        context.Database.EnsureCreated();
        
        var adminEmail = "admin@gmail.com";
        var existingAdmin = context.Users.FirstOrDefault(u => u.Email == adminEmail);
        
        if (existingAdmin == null)
        {
            var admin = new VehicleIMS.Domain.Models.User
            {
                Username = "Admin",
                Email = adminEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            };
            context.Users.Add(admin);
            Console.WriteLine("Admin user created.");
        }
        else
        {
            existingAdmin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123");
            context.Users.Update(existingAdmin);
            Console.WriteLine("Admin password reset to 'password123'.");
        }
        context.SaveChanges();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during seeding: {ex.Message}");
    }
}
app.MapControllers();

app.Run();