using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;
using System.Data.Linq;

namespace Data.Repositories.Static
{
    public class StaticCardRepository:ICardRepository
    {
        private static IList<Card> fakeCard = new List<Card>();
        private static int counter = 1;

        public IQueryable<Domain.Card> Cards
        {
            get { return fakeCard.AsQueryable(); }
        }

        public void SaveCard(Card card)
        {
            // If it's a new equipment, just add it to the list
            if (card.CardId == 0)
            {
                card.CardId = counter;
                counter += 1;
                fakeCard.Add(card);
            }
            else if (fakeCard.Count(row => row.CardId == card.CardId) == 1)
            {
                //This is an update. Remove old one, insert new one
                DeleteCard(card);
                fakeCard.Add(card);
            }
        }

        public void DeleteCard(Card card)
        {
            var temp = fakeCard.ToList();
            temp.RemoveAll(row => row.CardId == card.CardId);
            fakeCard = temp;
        }

        /// <summary>
        /// Only for Unit Testing. Clears repo of all data
        /// </summary>
        public void ClearRepo()
        {
            fakeCard.Clear();
        }


        public IEnumerable<Card> GetCardByLeadId(int leadId)
        {
            return fakeCard.Where(row => row.ParentLeadId == leadId).ToList();
            //return fakeCard.SingleOrDefault(row => row.ParentLeadId == leadId);
        }

    }
}
