using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using DAL.Repositories.Impl;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Tests
{
    class TestEmployeeRepository
        : BaseRepository<Ticket>
    {
        public TestEmployeeRepository(DbContext context)
            : base(context)
        {
        }
    }
    public class BaseRepositoryUnitTests
    {

        [Fact]
        public void Create_InputStreetInstance_CalledAddMethodOfDBSetWithStreetInstance()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<TicketInfoContext>()
                .Options;
            var mockContext = new Mock<TicketInfoContext>(opt);
            var mockDbSet = new Mock<DbSet<Ticket>>();
            mockContext
                .Setup(context =>
                    context.Set<Ticket>(
                        ))
                .Returns(mockDbSet.Object);

            var repository = new TestEmployeeRepository(mockContext.Object);

            Ticket expectedTicket = new Mock<Ticket>().Object;

            //Act
            repository.Create(expectedTicket);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Add(
                    expectedTicket
                    ), Times.Once());
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<TicketInfoContext>()
                .Options;
            var mockContext = new Mock<TicketInfoContext>(opt);
            var mockDbSet = new Mock<DbSet<Ticket>>();
            mockContext
                .Setup(context =>
                    context.Set<Ticket>(
                        ))
                .Returns(mockDbSet.Object);

            var repository = new TestEmployeeRepository(mockContext.Object);

            Ticket expectedTicket = new Ticket() { TicketID = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedTicket.TicketID)).Returns(expectedTicket);

            //Act   
            repository.Delete(expectedTicket.TicketID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedTicket.TicketID
                    ), Times.Once());
            mockDbSet.Verify(
                dbSet => dbSet.Remove(
                    expectedTicket
                    ), Times.Once());
        }

        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<TicketInfoContext>()
                .Options;
            var mockContext = new Mock<TicketInfoContext>(opt);
            var mockDbSet = new Mock<DbSet<Ticket>>();
            mockContext
                .Setup(context =>
                    context.Set<Ticket>(
                        ))
                .Returns(mockDbSet.Object);

            Ticket expectedTicket = new Ticket() { TicketID = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedTicket.TicketID))
                    .Returns(expectedTicket);
            var repository = new TestEmployeeRepository(mockContext.Object);

            //Act
            var actualStreet = repository.Get(expectedTicket.TicketID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedTicket.TicketID
                    ), Times.Once());
            Assert.Equal(expectedTicket, actualStreet);
        }


    }
}
