using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface ITicketRepository
    {
        IQueryable<Ticket> Tickets { get; }
        void SaveTickets(Ticket ticket);
        void DeleteTickets(Ticket ticket);
        IEnumerable<Ticket> GetTicketsByAccountID(int accountid);
        Ticket GetTicketByTicketId(int ticketid);
        int TicketCountByAccountId(int accountId);
    }
}
