using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using DAL.EF;
namespace DAL.Repositories.Impl
{

    public class CustomerRepository
        : BaseRepository<Customer>, ICustomerRepository
    {

        internal CustomerRepository(TicketInfoContext context)
            : base(context)
        {
        }
    }
}
