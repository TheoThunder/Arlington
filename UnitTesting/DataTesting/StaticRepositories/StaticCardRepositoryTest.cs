using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Data.Domain;
using Data.Repositories.Static;

namespace UnitTesting.DataTesting.StaticRepositories
{
    [TestFixture]
    public class StaticCardRepositoryTest
    {
        StaticCardRepository staticCardRepository;
        public StaticCardRepositoryTest()
        {
            staticCardRepository = new StaticCardRepository();
        }
        [SetUp]
        public void SetUp()
        {
            staticCardRepository.ClearRepo();
        }
         [Test]
        public void SaveNewCard()
        {
            var card = new Card { CreatedOn = DateTime.Now.AddHours(-10.0), LastUpdated = DateTime.Now, NumberCalled = 1, CardType = Data.Constants.CardTypes.CALLBACK };
            staticCardRepository.SaveCard(card);
            var result = staticCardRepository.Cards.Where(row => row.NumberCalled== 1).ToList();
            Assert.IsTrue(result.Count == 1, String.Format("result count was {0}", result.Count));
            Assert.IsTrue(result[0].CardId != 0);

        }

        [Test]
        public void SaveUpdatedCard()
        {
             var card = new Card { CreatedOn = DateTime.Now.AddHours(-10.0), LastUpdated = DateTime.Now, NumberCalled = 1, CardType = Data.Constants.CardTypes.CALLBACK };
            staticCardRepository.SaveCard(card);
            var result = staticCardRepository.Cards.Where(row => row.NumberCalled == 1).ToList()[0];
            result.NumberCalled = 2;
            staticCardRepository.SaveCard(result);
            var result2 = staticCardRepository.Cards.Where(row => row.NumberCalled == 2).ToList();
            Assert.IsTrue(result2.Count == 1, String.Format("result2 count was {0}", result2.Count));
            Assert.IsTrue(result2[0].CardId != 0);
            Assert.IsFalse(result2[0].NumberCalled==1);
        }

        [Test]
        public void DeleteCard()
        {
            var card = new Card { CreatedOn = DateTime.Now.AddHours(-10.0), LastUpdated = DateTime.Now, NumberCalled = 1, CardType = Data.Constants.CardTypes.CALLBACK };
            staticCardRepository.SaveCard(card);
            staticCardRepository.DeleteCard(card);
            var result = staticCardRepository.Cards.Where(row => row.NumberCalled == 1).ToList();
            Assert.IsTrue(result.Count == 0);
 
        }

    }
}
