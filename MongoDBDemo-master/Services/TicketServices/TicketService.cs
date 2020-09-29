using System;
using System.Collections.Generic;
using Models;
using DAL;
using System.Text;
using System.Linq.Expressions;

namespace Services.TicketServices
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repo;
        private string collection = "Tickets";

        public TicketService(ITicketRepository repo)
        {
            _repo = repo;
        }
        public void AddTicket(Ticket tick)
        {
            _repo.Add(tick, collection);
        }

        public long CountThemAll()
        {
            return _repo.Count(collection);
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return _repo.GetAll(collection);
        }

        public Ticket GetSingle(string id)
        {
            return _repo.GetSingle(id, collection);
        }

        public Ticket GetSingleWherePredicate(Expression<Func<Ticket, bool>> predicate)
        {
            return _repo.GetSingleItemPredicate(predicate, collection);
        }

        public void RemoveTicket(Ticket tick)
        {
            _repo.Delete(tick, collection);
        }

        public void UpdateTicket(Ticket tick)
        {
            _repo.Update(tick, collection);
        }
    }
}
