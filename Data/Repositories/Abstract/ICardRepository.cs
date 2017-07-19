using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface ICardRepository
    {
        IQueryable<Card> Cards { get; }
        void SaveCard(Card lead);
        void DeleteCard(Card lead);
        IEnumerable<Card> GetCardByLeadId(int leadId);
    }
}
