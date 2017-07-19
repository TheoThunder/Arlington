using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;
using System.Data.Linq;

namespace Data.Repositories.Static
{
    public class StaticAccountRepository:IAccountRepository
    {
        private static IList<Account> fakeAccount = new List<Account>();
        private static int counter = 1;

        public IQueryable<Domain.Account> Accounts
        {
            get { return fakeAccount.AsQueryable(); }
        }
        public void SaveAccounts(Account account)
        {
            // If it's a new equipment, just add it to the list
            if (account.AccountId == 0)
            {
                account.AccountId = counter;
                counter += 1;
                fakeAccount.Add(account);
            }
            else if (fakeAccount.Count(row => row.AccountId == account.AccountId) == 1)
            {
                //This is an update. Remove old one, insert new one
                DeleteAccounts(account);
                fakeAccount.Add(account);
            }
        }

        public void DeleteAccounts(Account account)
        {
            var temp = fakeAccount.ToList();
            temp.RemoveAll(row => row.AccountId == account.AccountId);
            fakeAccount = temp;
        }
        //This method was implemented in PGAccountRepository after switching tp DB from Static

        public IEnumerable<Account> GetAccountsByLeadId(int leadId)
        {
            //Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
            IList<Domain.Account> leadAccounts = new List<Domain.Account>();
            
            return leadAccounts;
        }

        public Account GetAccountByAccountId(int accountid)
        {
            //Please see section on using prepared statements in npgsql user manual for explanation on params and query structure
            Domain.Account Accounts = new Domain.Account();
            
            return Accounts;
        }


        /// <summary>
        /// Only for Unit Testing. Clears repo of all data
        /// </summary>
        public void ClearRepo()
        {
            fakeAccount.Clear();
        }
    }
   

}
