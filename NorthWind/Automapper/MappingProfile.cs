using AutoMapper;
using Northwind.Model.Domain;
using Northwind.DTO.Customer;
using Northwind.DTO.Employee;
using Northwind.DTO.Order;
using Northwind.DTO.Product;
using Northwind.DTO.Shipper;
using Northwind.DTO.Supplier;

namespace NorthWind.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 🔹 Customer
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();

            // 🔹 Employee
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();

            // 🔹 Order
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();

            // 🔹 OrderDetail
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, CreateOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, UpdateOrderDetailDto>().ReverseMap();

            // 🔹 Product
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();

            // 🔹 Shipper
            CreateMap<Shipper, ShipperDto>().ReverseMap();
            CreateMap<Shipper, CreateShipperDto>().ReverseMap();
            CreateMap<Shipper, UpdateShipperDto>().ReverseMap();

            // 🔹 Supplier
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Supplier, CreateSupplierDto>().ReverseMap();
            CreateMap<Supplier, UpdateSupplierDto>().ReverseMap();
        }
    }
}
