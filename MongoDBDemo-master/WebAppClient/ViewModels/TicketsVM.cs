using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppClient.ViewModels
{
    public class TicketsVM
    {
        public IEnumerable<Ticket> lstTickets { get; set; }

        public string Date{get;set;}

        public bool FilterOn { get; set; }

    }
}
