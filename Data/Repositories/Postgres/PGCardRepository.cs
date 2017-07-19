using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
   public class PGCardRepository : ICardRepository
    {
        #region Query Strings
       private string CardSelectQuery = @"SELECT cardid, cardtype, comment, createdon, lastupdated, leftvm, numbercalled, talkedtodm, talkedtooffm, talkedtoother, talkedtoperson, creatorid, parentleadid, assignedaaid, appointmentsheetid, reassigned, creatorname, cardcallbackdate, nointerestchk, nointerestrea, acquireddm FROM card ORDER BY createdon DESC";
       private string CardSelectonLeadIDQuery = @"SELECT cardid, cardtype, comment, createdon, lastupdated, leftvm, numbercalled, talkedtodm, talkedtooffm, talkedtoother, talkedtoperson, creatorid, parentleadid, assignedaaid, appointmentsheetid, c.reassigned, creatorname, cardcallbackdate, nointerestchk, nointerestrea, acquireddm FROM card AS c Where c.parentleadid = :leadid ORDER BY createdon";
       private string CardInsertQuery = @"INSERT INTO card (cardtype, comment, createdon, lastupdated, leftvm, numbercalled, talkedtodm, talkedtooffm, talkedtoother, talkedtoperson, creatorid, parentleadid, assignedaaid, appointmentsheetid, reassigned, creatorname, cardcallbackdate, nointerestchk, nointerestrea, acquireddm) VALUES (:cardtype, :comment, :createdon, :lastupdated, :leftvm, :numbercalled, :talkedtodm, :talkedtooffm, :talkedtoother, :talkedtoperson, :creatorid, :parentleadid, :assignedaaid, :appointmentsheetid, :reassigned, :creatorname, :callbackdate, :nointerestchk, :nointerestrea, :acquireddm )";

       private string CardUpdateQuery = @"UPDATE card SET cardtype = :cardtype, comment = :comment, createdon = :createdon, lastupdated = :lastupdated, leftvm = :leftvm, numbercalled = :numbercalled, talkedtodm = :talkedtodm, talkedtooffm = :talkedtooffm, talkedtoother = :talkedtoother, talkedtoperson = :talkedtoperson, creatorid = :creatorid, parentleadid = :parentleadid, assignedaaid = :assignedaaid, appointmentsheetid = :appointmentsheetid, reassigned = :reassigned, creatorname = :creatorname, cardcallbackdate = :callbackdate, nointerestchk = :nointerestchk, nointerestrea = :nointerestrea, acquireddm = :acquireddm WHERE cardid = :cardid";

        private string CardDeleteQuery = @"DELETE FROM card WHERE cardid = :cardid";
        #endregion

        //  private static int counter = 1;
        public static IList<Domain.Card> fakeCards = new List<Domain.Card>();
        public IQueryable<Domain.Card> Cards
        {
            get
            {
                fakeCards.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(CardSelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Card newCard = populateCardFromDB(dr);
                                fakeCards.Add(newCard);
                            }
                        }
                    }
                }
                return fakeCards.AsQueryable();

            }

        }

        public void SaveCard(Card card)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (card.CardId > 0)
            {
                query = CardUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = CardInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("cardtype", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("comment", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("createdon", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("lastupdated", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("leftvm", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("numbercalled", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("talkedtodm", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("talkedtooffm", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("talkedtoother", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("talkedtoperson", NpgsqlTypes.NpgsqlDbType.Boolean));  
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("creatorid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("parentleadid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("assignedaaid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("appointmentsheetid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("reassigned", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("creatorname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("callbackdate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("nointerestchk", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("nointerestrea", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("acquireddm", NpgsqlTypes.NpgsqlDbType.Boolean));
                    
                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("cardid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = card.CardType;
                    command.Parameters[1].Value = card.Comment;
                    command.Parameters[2].Value = card.CreatedOn;
                    command.Parameters[3].Value = card.LastUpdated;
                    command.Parameters[4].Value = card.LeftVM;
                    command.Parameters[5].Value = card.NumberCalled;
                    command.Parameters[6].Value = card.TalkedToDM;
                    command.Parameters[7].Value = card.TalkedToOfficeManager;
                    command.Parameters[8].Value = card.TalkedToOther;
                    command.Parameters[9].Value = card.TalkedToPerson;
                    command.Parameters[10].Value = card.CreatorId;
                    command.Parameters[11].Value = card.ParentLeadId;
                    command.Parameters[12].Value = card.AssignedAAId;
                    command.Parameters[13].Value = card.AppointmentSheetId;
                    command.Parameters[14].Value = card.Reassigned;
                    command.Parameters[15].Value = card.CreatorName;
                    command.Parameters[16].Value = card.CallBackDate;
                    command.Parameters[17].Value = card.NoInterestChk;
                    command.Parameters[18].Value = card.NoInterestRea;
                    command.Parameters[19].Value = card.AcquiredDMName;

                    //command.Parameters[10].Value = card.Creator;
              




                    if (isUpdate)
                    {
                        command.Parameters[20].Value = card.CardId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCard(Card card)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(CardDeleteQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("cardid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = card.CardId;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Card> GetCardByLeadId(int leadId)
        { 
            //Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
            IList<Domain.Card> leadCards = new List<Domain.Card>();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(CardSelectonLeadIDQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("leadid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = leadId;

                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Card newCard = populateCardFromDB(dr);
                            leadCards.Add(newCard);
                           
                        }
                    }
                }
            }

            return leadCards;
        }

        #region Helper Methods
        private static Card populateCardFromDB(Npgsql.NpgsqlDataReader dr)
        {
            Card newUser = new Card();
            newUser.CardId = Helper.ConvertFromDBVal<int>(dr[0]);
            newUser.CardType = dr[1].ToString();
            newUser.Comment = dr[2].ToString();
            newUser.CreatedOn = Helper.ConvertFromDBVal<DateTime>(dr[3]);
            newUser.LastUpdated = Helper.ConvertFromDBVal<DateTime>(dr[4]);
            newUser.LeftVM = Helper.ConvertFromDBVal<Boolean>(dr[5]);
            newUser.NumberCalled = Helper.ConvertFromDBVal<int>(dr[6]);
            newUser.TalkedToDM = Helper.ConvertFromDBVal<Boolean>(dr[7]);
            newUser.TalkedToOfficeManager = Helper.ConvertFromDBVal<Boolean>(dr[8]);
            newUser.TalkedToOther = Helper.ConvertFromDBVal<Boolean>(dr[9]);
            newUser.TalkedToPerson = Helper.ConvertFromDBVal<Boolean>(dr[10]);
            newUser.CreatorId = Helper.ConvertFromDBVal<int>(dr[11]);
            newUser.ParentLeadId = Helper.ConvertFromDBVal<int>(dr[12]);
            newUser.AssignedAAId = Helper.ConvertFromDBVal<int>(dr[13]);
            newUser.AppointmentSheetId = Helper.ConvertFromDBVal<int>(dr[14]);
            newUser.Reassigned = Helper.ConvertFromDBVal<Boolean>(dr[15]);
            newUser.CreatorName = dr[16].ToString();
            newUser.CallBackDate = Helper.ConvertFromDBVal<DateTime>(dr[17]);
            newUser.NoInterestChk = Helper.ConvertFromDBVal<Boolean>(dr[18]);
            newUser.NoInterestRea = dr[19].ToString();
            newUser.AcquiredDMName = Helper.ConvertFromDBVal<Boolean>(dr[20]);
            
             return newUser;
        }
        #endregion
    }
}
