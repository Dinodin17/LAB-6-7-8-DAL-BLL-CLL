using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public string Seat { get; set; }
        public string DepatureDate { get; set; }
        public string AdditionalInfo { get; set; }
        public Customer Owner { get; set; }
    }
}
