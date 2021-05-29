using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
