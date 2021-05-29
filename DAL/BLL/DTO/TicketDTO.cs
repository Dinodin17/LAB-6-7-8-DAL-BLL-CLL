using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class TicketDTO
    {
        public int TicketID { get; set; }
        public string Seat { get; set; }
        public string DepatureDate { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
