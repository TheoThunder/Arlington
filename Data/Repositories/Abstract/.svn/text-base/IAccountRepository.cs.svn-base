using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface IAccountRepository
    {
        IQueryable<Account> Accounts { get; }
        void SaveAccounts(Account lead);
        void DeleteAccounts(Account lead);
        IEnumerable<Account> GetAccountsByLeadId(int leadId);
        Account GetAccountByAccountId(int accountid);
    }
}
