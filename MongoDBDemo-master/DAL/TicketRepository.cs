using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class TicketRepository : EntityBaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository() : base()
        {

        }
    }
}
