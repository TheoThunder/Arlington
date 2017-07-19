//Please see section on using prepared statements in npgsql user manual for explanation on params and query structure

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGAccountRepository : IAccountRepository // Account interface
    {
        //Query Strings for Postgresql
        #region Query Strings
        private string AccountSelectQuery = @"SELECT accountid, merchantid, accountname, aacreator, assignedsalesrep, salesrepnumber, officenumber, status, accountapprovaldate, annualfee, estimatedmonthlyvolume, ht, hmv, platform, vendor, vip, mbp, freesupplies, pcirefund, mailingstreet, mailingcity, mailingstate, mailingzipcode, dbastreet, dbacity, dbastate, dbazipcode, primaryphone, secondaryphone, faxnumber, primaryemail, secondaryemail, website, credit, debit, arb, cim, ip, giftcardprocessor, secur_chex, software, ecommerce, primaryterminal, primaryterminalowner, primaryterminalquantity, secondaryterminal, secondaryterminalowner, secondaryterminalquantity, checkequipment, checkequipmentowner, checkequipmentquantity, primarypinpad, primarypinpadowner, primarypinpadquantity, secondarypinpad, secondarypinpadowner, secondarypinpadquantity, printer, priterowner, description, uploadfiles, parentlead FROM account";
        private string AccountInsertQuery = @"INSERT INTO account (merchantid, accountname, aacreator, assignedsalesrep, salesrepnumber, officenumber, status, accountapprovaldate, annualfee, estimatedmonthlyvolume, ht, hmv, platform, vendor, vip, mbp, freesupplies, pcirefund, mailingstreet, mailingcity, mailingstate, mailingzipcode, dbastreet, dbacity, dbastate, dbazipcode, primaryphone, secondaryphone, faxnumber, primaryemail, secondaryemail, website, credit, debit, arb, cim, ip, giftcardprocessor, secur_chex, software, ecommerce, primaryterminal, primaryterminalowner, primaryterminalquantity, secondaryterminal, secondaryterminalowner, secondaryterminalquantity, checkequipment, checkequipmentowner, checkequipmentquantity, primarypinpad, primarypinpadowner, primarypinpadquantity, secondarypinpad, secondarypinpadowner, secondarypinpadquantity, printer, priterowner, description, uploadfiles, parentlead) VALUES (:merchantid, :accountname, :aacreator, :assignedsalesrep, :salesrepnumber, :officenumber, :status, :accountapprovaldate, :annualfee, :estimatedmonthlyvolume, :ht, :hmv, :platform, :vendor, :vip, :mbp, :freesupplies, :pcirefund, :mailingstreet, :mailingcity, :mailingstate, :mailingzipcode, :dbastreet, :dbacity, :dbastate, :dbazipcode, :primaryphone, :secondaryphone, :faxnumber, :primaryemail, :secondaryemail, :website, :credit, :debit, :arb, :cim, :ip, :giftcardprocessor, :secur_chex, :software, :ecommerce, :primaryterminal, :primaryterminalowner, :primaryterminalquantity, :secondaryterminal, :secondaryterminalowner, :secondaryterminalquantity, :checkequipment, :checkequipmentowner, :checkequipmentquantity, :primarypinpad, :primarypinpadowner, :primarypinpadquantity, :secondarypinpad, :secondarypinpadowner, :secondarypinpadquantity, :printer, :priterowner, :description, :uploadfiles, :parentlead)";

        private string AccountUpdateQuery = @"UPDATE account SET merchantid = :merchantid, accountname = :accountname, aacreator = :aacreator, assignedsalesrep = :assignedsalesrep, salesrepnumber = :salesrepnumber, officenumber = :officenumber, status = :status, accountapprovaldate = :accountapprovaldate, annualfee = :annualfee, estimatedmonthlyvolume = :estimatedmonthlyvolume, ht = :ht, hmv = :hmv, platform = :platform, vendor = :vendor, vip = :vip, mbp = :mbp, freesupplies = :freesupplies, pcirefund = :pcirefund, mailingstreet = :mailingstreet, mailingcity = :mailingcity, mailingstate = :mailingstate, mailingzipcode = :mailingzipcode, dbastreet = :dbastreet, dbacity = :dbacity, dbastate = :dbastate, dbazipcode = :dbazipcode, primaryphone = :primaryphone, secondaryphone = :secondaryphone, faxnumber = :faxnumber, primaryemail = :primaryemail, secondaryemail = :secondaryemail, website = :website, credit = :credit, debit = :debit, arb = :arb, cim = :cim, ip = :ip, giftcardprocessor = :giftcardprocessor, secur_chex = :secur_chex, software = :software, ecommerce = :ecommerce, primaryterminal = :primaryterminal, primaryterminalowner = :primaryterminalowner, primaryterminalquantity = :primaryterminalquantity, secondaryterminal = :secondaryterminal, secondaryterminalowner = :secondaryterminalowner, secondaryterminalquantity = :secondaryterminalquantity, checkequipment = :checkequipment, checkequipmentowner = :checkequipmentowner, checkequipmentquantity = :checkequipmentquantity, primarypinpad = :primarypinpad, primarypinpadowner = :primarypinpadowner, primarypinpadquantity = :primarypinpadquantity, secondarypinpad = :secondarypinpad, secondarypinpadowner = :secondarypinpadowner, secondarypinpadquantity = :secondarypinpadquantity, printer = :printer, priterowner = :priterowner, description = :description, uploadfiles = :uploadfiles, parentlead = :parentlead WHERE accountid = :accountid";

        private string AccountByAccountIDQuery = @"SELECT accountid, merchantid, accountname, aacreator, assignedsalesrep, salesrepnumber, officenumber, status, accountapprovaldate, annualfee, estimatedmonthlyvolume, ht, hmv, platform, vendor, vip, mbp, freesupplies, pcirefund, mailingstreet, mailingcity, mailingstate, mailingzipcode, dbastreet, dbacity, dbastate, dbazipcode, primaryphone, secondaryphone, faxnumber, primaryemail, secondaryemail, website, credit, debit, arb, cim, ip, giftcardprocessor, secur_chex, software, ecommerce, primaryterminal, primaryterminalowner, primaryterminalquantity, secondaryterminal, secondaryterminalowner, secondaryterminalquantity, checkequipment, checkequipmentowner, checkequipmentquantity, primarypinpad, primarypinpadowner, primarypinpadquantity, secondarypinpad, secondarypinpadowner, secondarypinpadquantity, printer, priterowner, description, uploadfiles, parentlead FROM account where accountid = :accountid";

        private string AccountDeleteQuery = @"DELETE FROM account WHERE accountid = :accountid";
        private string AccountSelectonLeadIDQuery = @"SELECT accountid, merchantid, accountname, aacreator, assignedsalesrep, salesrepnumber, officenumber, status, accountapprovaldate, annualfee, estimatedmonthlyvolume, ht, hmv, platform, vendor, vip, mbp, freesupplies, pcirefund, mailingstreet, mailingcity, mailingstate, mailingzipcode, dbastreet, dbacity, dbastate, dbazipcode, primaryphone, secondaryphone, faxnumber, primaryemail, secondaryemail, website, credit, debit, arb, cim, ip, giftcardprocessor, secur_chex, software, ecommerce, primaryterminal, primaryterminalowner, primaryterminalquantity, secondaryterminal, secondaryterminalowner, secondaryterminalquantity, checkequipment, checkequipmentowner, checkequipmentquantity, primarypinpad, primarypinpadowner, primarypinpadquantity, secondarypinpad, secondarypinpadowner, secondarypinpadquantity, printer, priterowner, description, uploadfiles, parentlead FROM account AS a Where parentlead = :leadid";
        #endregion

        //  private static int counter = 1;
        // Local Account object to store and retrieve db values
        public static IList<Domain.Account> fakeAccounts = new List<Domain.Account>();

        // Method to fetch all the Accounts from DB
        public IQueryable<Domain.Account> Accounts
        {

            get
            {
                fakeAccounts.Clear();
                //look for connection string in ConfigReader
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(AccountSelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Account newAccount = populateAccountFromDB(dr);
                                fakeAccounts.Add(newAccount);
                            }
                        }
                    }
                }
                return fakeAccounts.AsQueryable();

            }

        }

        //To save account(s) to DB
        public void SaveAccounts(Domain.Account account)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (account.AccountId > 0)
            {
                query = AccountUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = AccountInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("merchantid", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("accountname", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("aacreator", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("assignedsalesrep", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("salesrepnumber", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("officenumber", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("status", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("accountapprovaldate", NpgsqlTypes.NpgsqlDbType.Timestamp));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("annualfee", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("estimatedmonthlyvolume", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ht", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("hmv", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("platform", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("vendor", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("vip", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("mbp", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("freesupplies", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("pcirefund", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("mailingstreet", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("mailingcity", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("mailingstate", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("mailingzipcode", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("dbastreet", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("dbacity", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("dbastate", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("dbazipcode", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryphone", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("secondaryphone", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("faxnumber", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryemail", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("secondaryemail", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("website", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("credit", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("debit", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("arb", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("cim", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ip", NpgsqlTypes.NpgsqlDbType.Boolean));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("giftcardprocessor", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("secur_chex", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("software", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("ecommerce", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryterminal", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryterminalowner", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primaryterminalquantity", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("secondaryterminal", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("secondaryterminalowner", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("secondaryterminalquantity", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("checkequipment", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("checkequipmentowner", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("checkequipmentquantity", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primarypinpad", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primarypinpadowner", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("primarypinpadquantity", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("secondarypinpad", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("secondarypinpadowner", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("secondarypinpadquantity", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("printer", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("priterowner", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("description", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("uploadfiles", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("parentlead", NpgsqlTypes.NpgsqlDbType.Integer));




                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("accountid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = account.MerchantId;
                    command.Parameters[1].Value = account.AccountName;
                    command.Parameters[2].Value = account.AACreator;
                    command.Parameters[3].Value = account.AssignedSalesRep;
                    command.Parameters[4].Value = account.SalesRepNumber;
                    command.Parameters[5].Value = account.OfficeNumber;
                    command.Parameters[6].Value = account.Status;
                    command.Parameters[7].Value = account.AccountApprovalDate;
                    command.Parameters[8].Value = account.AnnualFee;
                    command.Parameters[9].Value = account.EstimatedMonthlyVolume;
                    command.Parameters[10].Value = account.HT;
                    command.Parameters[11].Value = account.HMV;
                    command.Parameters[12].Value = account.Platform;
                    command.Parameters[13].Value = account.Vendor;
                    command.Parameters[14].Value = account.VIP;
                    command.Parameters[15].Value = account.MBP;
                    command.Parameters[16].Value = account.FreeSupplies;
                    command.Parameters[17].Value = account.PCIRefund;
                    command.Parameters[18].Value = account.MailingStreet;
                    command.Parameters[19].Value = account.MailingCity;
                    command.Parameters[20].Value = account.MailingState;
                    command.Parameters[21].Value = account.MailingZipcode;
                    command.Parameters[22].Value = account.DBAStreet;
                    command.Parameters[23].Value = account.DBACity;
                    command.Parameters[24].Value = account.DBAState;
                    command.Parameters[25].Value = account.DBAZipcode;
                    command.Parameters[26].Value = account.PrimaryPhone;
                    command.Parameters[27].Value = account.SecondaryPhone;
                    command.Parameters[28].Value = account.FaxNumber;
                    command.Parameters[29].Value = account.PrimaryEmail;
                    command.Parameters[30].Value = account.SecondaryEmail;
                    command.Parameters[31].Value = account.Website;
                    command.Parameters[32].Value = account.Credit;
                    command.Parameters[33].Value = account.Debit;
                    command.Parameters[34].Value = account.ARB;
                    command.Parameters[35].Value = account.CIM;
                    command.Parameters[36].Value = account.IP;
                    command.Parameters[37].Value = account.GiftCardProcessor;
                    command.Parameters[38].Value = account.Secur_Chex;
                    command.Parameters[39].Value = account.Software;
                    command.Parameters[40].Value = account.ECommerace;
                    command.Parameters[41].Value = account.PrimaryTerminal;
                    command.Parameters[42].Value = account.PrimaryTerminalOwner;
                    command.Parameters[43].Value = account.PrimaryTerminalQuantity;
                    command.Parameters[44].Value = account.SecondaryTerminal;
                    command.Parameters[45].Value = account.SecondaryTerminalOwner;
                    command.Parameters[46].Value = account.SecondaryTerminalQuantity;
                    command.Parameters[47].Value = account.CheckEquipment;
                    command.Parameters[48].Value = account.CheckEquipmentOwner;
                    command.Parameters[49].Value = account.CheckEquipmentQuantity;
                    command.Parameters[50].Value = account.PrimaryPINpad;
                    command.Parameters[51].Value = account.PrimaryPINpadOwner;
                    command.Parameters[52].Value = account.PrimaryPINpadQuantity;
                    command.Parameters[53].Value = account.SecondaryPINpad;
                    command.Parameters[54].Value = account.SecondaryPINpadOwner;
                    command.Parameters[55].Value = account.SecondaryPINpadQuantity;
                    command.Parameters[56].Value = account.Printer;
                    command.Parameters[57].Value = account.PrinterOwner;
                    command.Parameters[58].Value = account.Description;
                    command.Parameters[59].Value = account.UploadedFiles;
                    command.Parameters[60].Value = account.ParentLead;
                   


                    if (isUpdate)
                    {
                        command.Parameters[61].Value = account.AccountId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
            if (account.NewFilePath != null && account.NewFilePath != string.Empty)
            {
                PGUploadedfileRepository upFile = new PGUploadedfileRepository();
                upFile.InsertAccountFile(account.AccountId, account.NewFilePath);
            }
        }

        public void DeleteAccounts(Domain.Account account)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(AccountDeleteQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("accountid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = account.AccountId;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Account> GetAccountsByLeadId(int leadId)
        {
            //Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
            IList<Domain.Account> leadAccounts = new List<Domain.Account>();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(AccountSelectonLeadIDQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("leadid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = leadId;

                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Account newAccount = populateAccountFromDB(dr);
                            leadAccounts.Add(newAccount);
                        }
                    }
                }
            }

            return leadAccounts;
        }
        public Account GetAccountByAccountId(int accountid)
        {
            //Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
            Domain.Account Account = new Domain.Account();
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(AccountByAccountIDQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("accountid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = accountid;

                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                             Account = populateAccountFromDB(dr);
                             
                        }
                    }
                }
            }

            return Account;
        }
        #region Helper Methods
        private static Account populateAccountFromDB(Npgsql.NpgsqlDataReader dr)
        {
            Account newUser = new Account();
            newUser.AccountId = Helper.ConvertFromDBVal<int>(dr[0]);
            newUser.MerchantId = dr[1].ToString();
            newUser.AccountName = dr[2].ToString();
            newUser.AACreator = Helper.ConvertFromDBVal<int>(dr[3]);
            newUser.AssignedSalesRep = Helper.ConvertFromDBVal<int>(dr[4]);
            newUser.SalesRepNumber = dr[5].ToString();
            newUser.OfficeNumber = dr[6].ToString();
            newUser.Status = dr[7].ToString();
            newUser.AccountApprovalDate = Helper.ConvertFromDBVal<DateTime>(dr[8]);
            newUser.AnnualFee = Helper.ConvertFromDBVal<Boolean>(dr[9]);
            newUser.EstimatedMonthlyVolume = dr[10].ToString();
            newUser.HT = dr[11].ToString();
            newUser.HMV = dr[12].ToString();
            newUser.Platform = dr[13].ToString();
            newUser.Vendor = dr[14].ToString();
            newUser.VIP = Helper.ConvertFromDBVal<Boolean>(dr[15]);
            newUser.MBP = Helper.ConvertFromDBVal<Boolean>(dr[16]);
            newUser.FreeSupplies = Helper.ConvertFromDBVal<Boolean>(dr[17]);
            newUser.PCIRefund = Helper.ConvertFromDBVal<Boolean>(dr[18]);
            newUser.MailingStreet = dr[19].ToString();
            newUser.MailingCity = dr[20].ToString();
            newUser.MailingState = dr[21].ToString();
            newUser.MailingZipcode = dr[22].ToString();
            newUser.DBAStreet = dr[23].ToString();
            newUser.DBACity = dr[24].ToString();
            newUser.DBAState = dr[25].ToString();
            newUser.DBAZipcode = dr[26].ToString();
            newUser.PrimaryPhone = dr[27].ToString();
            newUser.SecondaryPhone = dr[28].ToString();
            newUser.FaxNumber = dr[29].ToString();
            newUser.PrimaryEmail = dr[30].ToString();
            newUser.SecondaryEmail = dr[31].ToString();
            newUser.Website = dr[32].ToString();
            
            newUser.Credit = Helper.ConvertFromDBVal<Boolean>(dr[33]);
            newUser.Debit = Helper.ConvertFromDBVal<Boolean>(dr[34]);
            newUser.ARB = Helper.ConvertFromDBVal<Boolean>(dr[35]);
            newUser.CIM = Helper.ConvertFromDBVal<Boolean>(dr[36]);
            newUser.IP = Helper.ConvertFromDBVal<Boolean>(dr[37]);
            newUser.GiftCardProcessor = dr[38].ToString();
            newUser.Secur_Chex = dr[39].ToString();
            newUser.Software = dr[40].ToString();
            newUser.ECommerace = dr[41].ToString();
            newUser.PrimaryTerminal = Helper.ConvertFromDBVal<int>(dr[42]);
            newUser.PrimaryTerminalOwner = dr[43].ToString();
            newUser.PrimaryTerminalQuantity = Helper.ConvertFromDBVal<int>(dr[44]);
            newUser.SecondaryTerminal = Helper.ConvertFromDBVal<int>(dr[45]);
            newUser.SecondaryTerminalOwner = dr[46].ToString();
            newUser.SecondaryTerminalQuantity = Helper.ConvertFromDBVal<int>(dr[47]);
            newUser.CheckEquipment = Helper.ConvertFromDBVal<int>(dr[48]);
            newUser.CheckEquipmentOwner = dr[49].ToString();
            newUser.CheckEquipmentQuantity = Helper.ConvertFromDBVal<int>(dr[50]);
            newUser.PrimaryPINpad = Helper.ConvertFromDBVal<int>(dr[51]);
            newUser.PrimaryPINpadOwner = dr[52].ToString();
            newUser.PrimaryPINpadQuantity = Helper.ConvertFromDBVal<int>(dr[53]);
            newUser.SecondaryPINpad = Helper.ConvertFromDBVal<int>(dr[54]);
            newUser.SecondaryPINpadOwner = dr[55].ToString();
            newUser.SecondaryPINpadQuantity = Helper.ConvertFromDBVal<int>(dr[56]);
            newUser.Printer = ConvertFromDBVal<string>(dr[57]);
            newUser.PrinterOwner = ConvertFromDBVal<string>(dr[58]);
            newUser.Description = dr[59].ToString();
            newUser.UploadedFiles = Helper.ConvertFromDBVal<int>(dr[60]);
            newUser.ParentLead = Helper.ConvertFromDBVal<int>(dr[61]);

            PGUploadedfileRepository upFile = new PGUploadedfileRepository();
            
            newUser.StatementFiles.AddRange(upFile.GetFileByAccountId(newUser.AccountId));

            return newUser;
        }

        private static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T);
            }
            else
            {
                return (T)obj;
            }
        }
        #endregion
    }
}
