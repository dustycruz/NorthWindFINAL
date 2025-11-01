using AutoMapper;
using Northwind.DTO.Auth;
using Northwind.DTO.Customer;
using Northwind.DTO.Employee;
using Northwind.DTO.Order;
using Northwind.DTO.Product;
using Northwind.DTO.Shipper;
using Northwind.DTO.Supplier;
using Northwind.Model.Domain;
using NorthWind.Model.DTO.Customer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Northwind.BLL.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // 🧾 Customer
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CreateCustomerDto, Customer>();

            // 🧑‍💼 Employee
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<CreateEmployeeDto, Employee>();

            // 🏭 Supplier
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<CreateSupplierDto, Supplier>();

            // 📦 Product
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, Product>();

            // 🚚 Shipper
            CreateMap<Shipper, ShipperDto>().ReverseMap();
            CreateMap<CreateShipperDto, Shipper>();

            // 🧾 Order + OrderDetail
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<CreateOrderDetailDto, OrderDetail>();

            // 🔐 Auth (no direct domain, for Identity)
            CreateMap<RegisterDto, AuthResponseDto>().ReverseMap();
        }
    }
}
