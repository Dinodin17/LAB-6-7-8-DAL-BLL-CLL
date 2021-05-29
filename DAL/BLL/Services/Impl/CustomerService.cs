using BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using BLL.DTO;
using DAL.Repositories.Interfaces;
using AutoMapper;
using DAL.UnitOfWork;
using CLL.Security;
using CLL.Security.Identity;
namespace BLL.Services.Impl
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _database;
        private int pageSize = 10;

        public CustomerService(
            IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(
                    nameof(unitOfWork));
            }
            _database = unitOfWork;
        }

        /// <exception cref="MethodAccessException"></exception>
        public IEnumerable<CustomerDTO> GetCustomers(int pageNumber)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }
            var userId = user.UserId;
            var customersEntities =
                _database
                    .Customers
                    .Find(z => z.CustomerID == userId, pageNumber, pageSize);
            var mapper =
                new MapperConfiguration(
                    cfg => cfg.CreateMap<Customer, CustomerDTO>()
                    ).CreateMapper();
            var customersDto =
                mapper
                    .Map<IEnumerable<Customer>, List<CustomerDTO>>(
                        customersEntities);
            return customersDto;
        }

        public void AddStreet(CustomerDTO customer)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            validate(customer);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, Customer>()).CreateMapper();
            var CustomersEntity = mapper.Map<CustomerDTO, Customer>(customer);
            _database.Customers.Create(CustomersEntity);
        }

        private void validate(CustomerDTO customer)
        {
            if (string.IsNullOrEmpty(customer.Name))
            {
                throw new ArgumentException("Name повинне містити значення!");
            }
        }
    }
}
