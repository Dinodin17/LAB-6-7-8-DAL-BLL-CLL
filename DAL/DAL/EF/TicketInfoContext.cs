using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL.EF
{
    public class TicketInfoContext
        : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public TicketInfoContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
