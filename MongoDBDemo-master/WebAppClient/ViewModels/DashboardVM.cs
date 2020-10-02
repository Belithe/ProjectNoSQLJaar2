using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppClient.ViewModels
{
    public class DashboardVM
    {
        public DashboardVM()
        {
            ticketsOpen = new List<Ticket>();
            ticketsOverdue = new List<Ticket>();
            ticketsTotal = new List<Ticket>();
        }

        public List<Ticket> ticketsOpen { get; set; }
        public List<Ticket> ticketsOverdue { get; set; }
        public List<Ticket> ticketsTotal { get; set; }
    }
}
