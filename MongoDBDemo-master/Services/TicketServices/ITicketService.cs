using Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Services.TicketServices
{
    public interface ITicketService
    {
        IEnumerable<Ticket> GetAllTickets();
        long CountThemAll();

        Ticket GetSingle(string id);

        Ticket GetSingleWherePredicate(Expression<Func<Ticket, bool>> predicate);

        void AddTicket(Ticket tick);

        void UpdateTicket(Ticket tick);

        void RemoveTicket(Ticket tick);
    }
}
