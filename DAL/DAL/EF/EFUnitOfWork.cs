using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Repositories.Impl;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
namespace DAL.EF
{
    public class EFUnitOfWork
        : IUnitOfWork
    {
        private TicketInfoContext db;
        private TicketRepository emplRepository;
        private CustomerRepository scheduleRepository;

        public EFUnitOfWork(TicketInfoContext context)
        {
            db = context;
        }
        public ITicketRepository Tickets
        {
            get
            {
                if (emplRepository == null)
                    emplRepository = new TicketRepository(db);
                return emplRepository;
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (scheduleRepository == null)
                    scheduleRepository = new CustomerRepository(db);
                return scheduleRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
