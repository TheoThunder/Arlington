using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;
using Data.Repositories.Abstract;

namespace Data.Repositories.Postgres
{
    public class PGTicketRepository : ITicketRepository
    {

        #region Query Strings
        private string TicketSelectQuery = @"SELECT ticketid, ticket_number, customer_name, subject, tstatus, creator, current_owner, zone, priority, date_opened, account_name, last_updated, ticket_type, reason, ticket_origin, received_from, tdescription, effective_date, comments, action, taccountid, date_closed, callback_number, tickethistoryid, closedby FROM ticket ";

        private string TicketByTicketIdSelectQuery = @"SELECT ticketid, ticket_number, customer_name, subject, tstatus, creator, current_owner, zone, priority, date_opened, account_name, last_updated, ticket_type, reason, ticket_origin, received_from, tdescription, effective_date, comments, action, taccountid, date_closed, callback_number, tickethistoryid, closedby FROM ticket where ticketid = :ticketid";

        private string TicketAccountSelectQuery = @"SELECT ticketid, ticket_number, customer_name, subject, tstatus, creator, current_owner, zone, priority, date_opened, account_name, last_updated, ticket_type, reason,  ticket_origin, received_from, tdescription, effective_date, comments, action, taccountid, date_closed, callback_number, tickethistoryid, closedby FROM ticket AS t WHERE t.taccountid = :accountid";

        private string TicketInsertQuery = @"INSERT INTO ticket (ticket_number, customer_name, subject, tstatus, creator, current_owner, zone, priority, date_opened, account_name, last_updated, ticket_type, reason, ticket_origin, received_from, tdescription, effective_date, comments, action, taccountid, date_closed, callback_number, tickethistoryid , closedby) VALUES (:ticket_number, :customer_name, :subject, :status, :creator, :current_owner, :zone, :priority, :date_opened, :account_name, :last_updated, :ticket_type, :reason, :ticket_origin, :received_from, :description, :effective_date, :comments, :action, :accountid, :date_closed, :callback_number, :tickethistoryid, :closedby)";

        private string TicketUpdateQuery = @"UPDATE Ticket SET  ticket_number = :ticket_number, customer_name = :customer_name, subject = :subject, tstatus = :status, creator = :creator, current_owner = :current_owner, zone = :zone, priority = :priority, date_opened = :date_opened, account_name= :account_name, last_updated = :last_updated, ticket_type = :ticket_type, reason = :reason, ticket_origin = :ticket_origin, received_from = :received_from, tdescription = :description, effective_date = :effective_date, comments = :comments, action= :action, taccountid = :accountid, date_closed = :date_closed, callback_number = :callback_number, tickethistoryid = :tickethistoryid, closedby = :closedby WHERE ticketid = :ticketid";

        private string TicketDeleteQuery = @"DELETE FROM ticket WHERE ticketid = :ticketid";

        private string TicketCountQuery = @"Select Count(*) from ticket where taccountid = :accountid ";
        #endregion

        //  private static int counter = 1;
        public static IList<Domain.Ticket> realTickets = new List<Domain.Ticket>();
        public IQueryable<Domain.Ticket> Tickets
        {
            get
            {
                realTickets.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(TicketSelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Ticket newTicket = populateTicketsFromDB(dr);
                                realTickets.Add(newTicket);
                            }
                        }
                    }
                }
                return realTickets.AsQueryable();

            }

        }

        public IEnumerable<Ticket> GetTicketsByAccountID(int accountid)
        {
            realTickets.Clear();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(TicketAccountSelectQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("accountid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = accountid;
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Ticket newTicket = populateTicketsFromDB(dr);
                            realTickets.Add(newTicket);
                        }
                    }
                }
            }
            return realTickets.AsQueryable();
        }

        public void SaveTickets(Ticket Ticket)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (Ticket.TicketId > 0)
            {
                query = TicketUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = TicketInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ticket_number", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("customer_name", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("subject", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("status", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("creator", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("current_owner", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("zone", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("priority", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("date_opened", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("account_name", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("last_updated", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ticket_type", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("reason", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ticket_origin", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("received_from", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("description", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("effective_date", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("comments", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("action", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("accountid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("date_closed", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("callback_number", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("tickethistoryid", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("closedby", NpgsqlTypes.NpgsqlDbType.Integer));

                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("ticketid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = Ticket.TicketNumber;
                    command.Parameters[1].Value = Ticket.CustomerName;
                    command.Parameters[2].Value = Ticket.Subject;
                    command.Parameters[3].Value = Ticket.Status;
                    command.Parameters[4].Value = Ticket.Creator;
                    command.Parameters[5].Value = Ticket.CurrentOwner;
                    command.Parameters[6].Value = Ticket.Zone;
                    command.Parameters[7].Value = Ticket.Priority;
                    command.Parameters[8].Value = Ticket.DateOpened;
                    command.Parameters[9].Value = Ticket.AccountName;
                    command.Parameters[10].Value = Ticket.LastUpdated;
                    command.Parameters[11].Value = Ticket.TicketType;
                    command.Parameters[12].Value = Ticket.Reason;
                    command.Parameters[13].Value = Ticket.TicketOrigin;
                    command.Parameters[14].Value = Ticket.ReceivedFrom;
                    command.Parameters[15].Value = Ticket.Description;
                    command.Parameters[16].Value = Ticket.EffectiveDate;
                    command.Parameters[17].Value = Ticket.Comments;
                    command.Parameters[18].Value = Ticket.Action;
                    command.Parameters[19].Value = Ticket.AccountId;
                    command.Parameters[20].Value = Ticket.DateClosed;
                    command.Parameters[21].Value = Ticket.CallBackNumber;
                    command.Parameters[22].Value = Ticket.TicketHistoryID;
                    command.Parameters[23].Value = Ticket.ClosedBy;

                    if (isUpdate)
                    {
                        command.Parameters[24].Value = Ticket.TicketId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTickets(Ticket Ticket)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(TicketDeleteQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ticketid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = Ticket.TicketId;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public Ticket GetTicketByTicketId(int ticketid)
        {
            //Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
            Ticket newTicket = new Ticket();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(TicketByTicketIdSelectQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ticketid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = ticketid;

                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            newTicket = populateTicketsFromDB(dr);

                        }
                    }
                }
            }

            return newTicket;
        }

        public int TicketCountByAccountId(int accountId)
        {
            int count = 0;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {

                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(TicketCountQuery, conn))
                {
                    command.Parameters.AddWithValue("accountId", accountId);

                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                           count = Helper.ConvertFromDBVal<int>(dr[0]);
                    }
                }
            }
            return count;
        }

        #region Helper Methods
        private static Ticket populateTicketsFromDB(Npgsql.NpgsqlDataReader dr)
        {
            Ticket newTicket = new Ticket();
            newTicket.TicketId = Helper.ConvertFromDBVal<int>(dr[0]);
            newTicket.TicketNumber = Helper.ConvertFromDBVal<int>(dr[1]);
            newTicket.CustomerName= dr[2].ToString();
            newTicket.Subject = dr[3].ToString();
            newTicket.Status = dr[4].ToString();
            newTicket.Creator= Helper.ConvertFromDBVal<int>(dr[5]);
            newTicket.CurrentOwner = Helper.ConvertFromDBVal<int>(dr[6]);
            newTicket.Zone = Helper.ConvertFromDBVal<int>(dr[7]);
            newTicket.Priority = dr[8].ToString();
            newTicket.DateOpened = Helper.ConvertFromDBVal <DateTime>(dr[9]);
            newTicket.AccountName = dr[10].ToString();
            newTicket.LastUpdated = Helper.ConvertFromDBVal <DateTime>(dr[11]);
            newTicket.TicketType = dr[12].ToString();
            newTicket.Reason = dr[13].ToString();
            newTicket.TicketOrigin = dr[14].ToString();
            newTicket.ReceivedFrom = dr[15].ToString();
            newTicket.Description = dr[16].ToString();
            newTicket.EffectiveDate = Helper.ConvertFromDBVal<DateTime>(dr[17]);
            newTicket.Comments = dr[18].ToString();
            newTicket.Action = dr[19].ToString();
            newTicket.AccountId = Helper.ConvertFromDBVal<int>(dr[20]);
            newTicket.DateClosed = Helper.ConvertFromDBVal<DateTime>(dr[21]);
            newTicket.CallBackNumber = dr[22].ToString();
            newTicket.TicketHistoryID = dr[23].ToString();
            newTicket.ClosedBy = Helper.ConvertFromDBVal<int>(dr[24]);

            return newTicket;
        }
        #endregion
    }
}
