using BLL.Services.Impl;
using BLL.Services.Interfaces;
using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using CLL.Security;
using CLL.Security.Identity;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
namespace BLL.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            // Arrange
            IUnitOfWork nullUnitOfWork = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new CustomerService(nullUnitOfWork));
        }

        [Fact]
        public void GetCustomers_UserIsAdmin_ThrowMethodAccessException()
        {
            // Arrange
            User user = new Client(1, "test");
            SecurityContext.SetUser(user);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            ICustomerService customersService = new CustomerService(mockUnitOfWork.Object);

            // Act
            // Assert
            Assert.Throws<MethodAccessException>(() => customersService.GetCustomers(0));
        }

        [Fact]
        public void GetCustomers_CustomersFromDAL_CorrectMappingToStreetDTO()
        {
            // Arrange
            User user = new Admin(1, "test");
            SecurityContext.SetUser(user);
            var customersService = GetCustomersService();

            // Act
            var actualStreetDto = customersService.GetCustomers(0).First();

            // Assert
            Assert.True(
                actualStreetDto.CustomerID == 1
                && actualStreetDto.Name == "testN"
                && actualStreetDto.Surname == "testD"
                );
        }

        ICustomerService GetCustomersService()
        {
            var mockContext = new Mock<IUnitOfWork>();
            var expectedCustomer = new Customer() { CustomerID = 1, Name = "testN", Surname = "testD" };
            var mockDbSet = new Mock<ICustomerRepository>();
            mockDbSet.Setup(z =>
                z.Find(
                    It.IsAny<Func<Customer, bool>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                  .Returns(
                    new List<Customer>() { expectedCustomer }
                    );
            mockContext
                .Setup(context =>
                    context.Customers)
                .Returns(mockDbSet.Object);


            ICustomerService customerService = new CustomerService(mockContext.Object);

            return customerService;
        }
    }
}
