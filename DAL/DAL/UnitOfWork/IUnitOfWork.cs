using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories.Interfaces;
namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITicketRepository Tickets { get; }
        ICustomerRepository Customers { get; }
        void Save();
    }
}
