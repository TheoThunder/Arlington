using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;
using Data.Repositories.Abstract;

namespace Data.Repositories.Postgres
{
    public class PGTicketHistoryRepository : ITicketHistoryRepository
    {

        #region Query Strings
        private string TicketHistorySelectQuery = @"SELECT historyid, hticketid, historydate, userworked, haction, comment FROM tickethistory";

        private string TicketHistoryTicketSelectQuery = @"SELECT historyid, hticketid, historydate, userworked, haction, comment FROM tickethistory AS th INNER JOIN ticket as t ON th.hticketid = t.tickethistoryid WHERE th.hticketid = :ticketid";

        private string TicketHistoryInsertQuery = @"INSERT INTO tickethistory (hticketid, historydate, userworked, haction, comment) VALUES (:hticketid, :historydate, :userworked, :haction, :comment)";

        private string TicketHistoryUpdateQuery = @"UPDATE tickethistory SET hticketid = :hticketid, historydate = :historydate, userworked = :userworked, haction = :haction, comment = :comment Where historyid = :historyid";

        private string TicketHistoryDeleteQuery = @"DELETE FROM tickethistory WHERE historyid = :historyid";
        #endregion

        //  private static int counter = 1;
        public static IList<Domain.TicketHistory> realTicketHistory = new List<Domain.TicketHistory>();
        public IQueryable<Domain.TicketHistory> TicketHistory
        {
            get
            {
                realTicketHistory.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(TicketHistorySelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                TicketHistory newTicketHistory = populateTicketHistoryFromDB(dr);
                                realTicketHistory.Add(newTicketHistory);
                            }
                        }
                    }
                }
                return realTicketHistory.AsQueryable();

            }

        }

        public IEnumerable<TicketHistory> GetTicketHistoryByTicketID(string ticketid)
        {
            realTicketHistory.Clear();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(TicketHistoryTicketSelectQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ticketid", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Prepare();
                    command.Parameters[0].Value = ticketid;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TicketHistory newTicketHistory = populateTicketHistoryFromDB(dr);
                            realTicketHistory.Add(newTicketHistory);
                        }
                    }
                }
            }
            return realTicketHistory.AsQueryable();
        }

        public void SaveHistory(TicketHistory TicketHistory)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (TicketHistory.HistoryId > 0)
            {
                query = TicketHistoryUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = TicketHistoryInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("hticketid", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("historydate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("userworked", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("haction", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("comment", NpgsqlTypes.NpgsqlDbType.Text));
                   
                   
                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("historyid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = TicketHistory.TicketId;
                    command.Parameters[1].Value = TicketHistory.HistoryDate;
                    command.Parameters[2].Value = TicketHistory.UserWorked;
                    command.Parameters[3].Value = TicketHistory.Action;
                    command.Parameters[4].Value = TicketHistory.Comment;
                   
                    if (isUpdate)
                    {
                        command.Parameters[5].Value = TicketHistory.HistoryId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteHistory(TicketHistory TicketHistory)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(TicketHistoryDeleteQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("historyid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = TicketHistory.HistoryId;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }



        #region Helper Methods
        private static TicketHistory populateTicketHistoryFromDB(Npgsql.NpgsqlDataReader dr)
        {
            TicketHistory newTicketHistory = new TicketHistory();
            newTicketHistory.HistoryId = Helper.ConvertFromDBVal<int>(dr[0]);
            newTicketHistory.TicketId = dr[1].ToString();
            newTicketHistory.HistoryDate = Helper.ConvertFromDBVal<DateTime>(dr[2]);
            newTicketHistory.UserWorked = Helper.ConvertFromDBVal<int>(dr[3]);
            newTicketHistory.Action= dr[4].ToString();
            newTicketHistory.Comment = dr[5].ToString();
            
            return newTicketHistory;
        }
        #endregion
    }
}
