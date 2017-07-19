using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface ITicketHistoryRepository
    {
        IQueryable<TicketHistory> TicketHistory { get; }
        void SaveHistory(TicketHistory ticket);
        void DeleteHistory(TicketHistory ticket);
        IEnumerable<TicketHistory> GetTicketHistoryByTicketID(string ticketid);
    }
}
