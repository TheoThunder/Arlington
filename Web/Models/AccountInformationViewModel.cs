using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Domain;
using Data.Repositories;
using Data.Repositories.Static;
using System.Web.Mvc;
using System;

namespace Web.ViewModel
{
    public class AccountInformationViewModel
    {
        public AccountInformationViewModel()
        {

        }
        public Account account { get; set; }
        public IEnumerable<Account> Account;
        public User user { get; set; }
        public IEnumerable<string> estimatedmonthlyvolume = Data.Constants.VolumeList.Volume;//Update the list in constants after creating appointment sheet
        public IEnumerable<string> statuslist = Data.Constants.AccountStatus.accountstatus;//for account status
        public IEnumerable<string> states = Data.Constants.StateList.States;
        public IEnumerable<string> equipment = Data.Constants.CheckEquipment.equipments;
        public IEnumerable<string> platform = Data.Constants.platform.platforms;
        public IEnumerable<string> vender = Data.Constants.Vender.vender;
        public IEnumerable<string> GiftCard = Data.Constants.GiftCards.giftcards;
        public IEnumerable<string> securchex = Data.Constants.Securchex.securchex;
        public IEnumerable<string> software = Data.Constants.Software.software;
        public IEnumerable<string> ecommerce = Data.Constants.Ecommerce.ecommerce;
        public IEnumerable<string> pinpad = Data.Constants.PinPads.pinpads;
        public IEnumerable<string> terminals = Data.Constants.Terminals.terminals;
        public IEnumerable<string> ownership = Data.Constants.Ownership.ownership;
        public IEnumerable<Account> accounts;
        public IEnumerable<Ticket> tickets { get; set; }
        public int leadId {get; set;}

        public Lead lead { get; set; }
        public IEnumerable<SelectListItem> SAUsersDropdown { get; set; }

        public String AAName;
        public String SAName;
        public String EID;

        public User assignedaa { get; set; }
        public User assignedsa { get; set; }

        public Boolean Scored { get; set; }
        public int ScoredAppointmentId { get; set; }

        public int SelectEquipId { get; set; }
        public IEnumerable<SelectListItem> Equipments { get; set; }
    }
}