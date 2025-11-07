using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Data; // <-- Replace with your actual DbContext namespace
using NorthWind.Repositories; // <-- where GenericRepository is
using NorthWind.Service;

var builder = WebApplication.CreateBuilder(args);

// ? Add DbContext
builder.Services.AddDbContext<NorthwindDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NorthWindConnection")));

// ? Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// ? Register Generic Repository (for all entities)
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// ? Register All Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IShipperService, ShipperService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));


// ? Add Controllers + Swagger + Auth
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? Add Authentication/Authorization
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Jwt:Authority"];
        options.Audience = builder.Configuration["Jwt:Audience"];
    });

builder.Services.AddAuthorization();
