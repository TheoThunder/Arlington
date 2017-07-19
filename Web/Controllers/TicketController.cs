using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories.Abstract;
using INF = Infrastructure;
using System;
using Newtonsoft.Json;
using Data.Domain;
using Web.ViewModel;

namespace Web.Controllers
{
    public class TicketController : Controller
    {
        public ITicketRepository _ticketRepository;
        public IUserRepository _UserRepository;
        public IZoneRepository _zoneRepos;
        public IGenericUsageRepositoryInterface _UsageRepos;
        public IAccountRepository _AccountRepository;
        public ITicketHistoryRepository _ticketHistoryRepository;

        public TicketController(ITicketHistoryRepository HistoryRepos, IAccountRepository AccountRepos, IGenericUsageRepositoryInterface URepository, ITicketRepository TicketRepository, IUserRepository UserRepos, IZoneRepository ZoneRepos)
        {
            _ticketRepository = TicketRepository;
            _UserRepository = UserRepos;
            _zoneRepos = ZoneRepos; 
            _UsageRepos = URepository;
            _AccountRepository = AccountRepos;
            _ticketHistoryRepository = HistoryRepos;
          
        }

        public ActionResult Index()
        {
            TicketViewModel tvm = new TicketViewModel();
            
            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);
            tvm.user = user;
            tvm.Tickets = _ticketRepository.Tickets;
            foreach (var ticket in tvm.Tickets)
            {
                if (ticket.CurrentOwner != 0)
                {
                    ticket.AssignedUser = _UserRepository.GetUserById(ticket.CurrentOwner);
                }
            }

            var aaUsersResult = _UserRepository.GetAllUsers();

            tvm.AAUsersDropdown = aaUsersResult.Select(row => new SelectListItem()
            {
                Text = row.UserName,
                Value = row.UserName.ToString()
            });
            IEnumerable<Zone> zones = _zoneRepos.Zones;
            IList<int> zonelist = new List<int>();
            foreach (var zone in zones)
            {
                zonelist.Add(zone.ZoneNumber);
            }

            tvm.ZoneDropdown = zonelist;
            return View(tvm);
        }

        //
        // GET: /Ticket/Details/5

        public ActionResult Details(int TicketId)
        {
            TicketViewModel tvm = new TicketViewModel();

            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);
            tvm.user = user;
            
            var aaUsersResult = _UserRepository.GetAllUsers();

           
            
            tvm.AAUsersDropdown = aaUsersResult.Select(row => new SelectListItem()
            {
                Text = row.LastName + "," + row.FirstName,
                Value = row.UserId.ToString()
            });

            tvm.ticket = _ticketRepository.GetTicketByTicketId(TicketId);
            tvm.ticket.AssignedUser = _UserRepository.GetUserById(tvm.ticket.CurrentOwner);
            tvm.ticket.CreatorName = _UserRepository.GetUserById(tvm.ticket.Creator);
            Account account  = _AccountRepository.GetAccountByAccountId(tvm.ticket.AccountId);
           
            tvm.ticket.CallBackNumber = account.PrimaryPhone;

            IEnumerable<TicketHistory> historyList = new List<TicketHistory>();
            IList<TicketHistory> newHistoryList = new List<TicketHistory>();
            historyList = _ticketHistoryRepository.GetTicketHistoryByTicketID(tvm.ticket.TicketHistoryID);
            foreach (var history in historyList)
            {
                history.AssignedUser = _UserRepository.GetUserById(history.UserWorked);
                newHistoryList.Add(history);
            }

            tvm.TicketHistory = newHistoryList;
            return View(tvm);
        }

        [HttpPost]
        public ActionResult Details(TicketCreateViewModel tvm)
        {
            try
            {
                Ticket ticket = new Ticket();
                TicketHistory ticket_history = new TicketHistory();

                ticket = _ticketRepository.GetTicketByTicketId(tvm.ticket.TicketId);
                ticket.DateOpened = tvm.ticket.DateOpened;
                ticket.LastUpdated = DateTime.Now;
                _ticketRepository.SaveTickets(ticket);
                ///////////////////////////////////////////////
                //Getting and Saving Ticket History
                Ticket newTicket = new Ticket();
                newTicket = _ticketRepository.Tickets.Single(row => row.TicketHistoryID == ticket.TicketHistoryID);

                ticket_history.TicketId = newTicket.TicketHistoryID;
                ticket_history.HistoryDate = DateTime.Now;
                ticket_history.UserWorked = ticket.CurrentOwner;
                ticket_history.Action = ticket.Description;
                ticket_history.Comment = ticket.Comments;
                _ticketHistoryRepository.SaveHistory(ticket_history);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Ticket/Create

        public ActionResult Create(Int32 AccountId)
        {
            TicketViewModel tvm = new TicketViewModel();
            Ticket newTicket = new Ticket();

            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);
            tvm.user = user;

            tvm.account = _AccountRepository.GetAccountByAccountId(AccountId);
            var aaUsersResult = _UserRepository.GetAllUsers();
            tvm.AAUsersDropdown = aaUsersResult.Select(row => new SelectListItem()
            {
                Text = row.LastName + "," + row.FirstName,
                Value = row.UserId.ToString()
            });
            newTicket.DateOpened = DateTime.Now;
            newTicket.LastUpdated = DateTime.Now;
            newTicket.AccountName = tvm.account.AccountName;
            tvm.ticket = newTicket;
            return View(tvm);
        } 

        //
        // POST: /Ticket/Create

        [HttpPost]
        public ActionResult Create(TicketCreateViewModel tvm)
        {
            try
            {
                var username = HttpContext.User.Identity.Name;
                var userworked = _UserRepository.GetUserByUsername(username);

                Ticket ticket = new Ticket();
                TicketHistory ticket_history = new TicketHistory();
                
                // Getting and Saving Ticket Information
                ticket = tvm.ticket;
                ticket.AccountId = tvm.account.AccountId;
                ticket.AccountName = tvm.account.AccountName;
                ticket.DateOpened = DateTime.Now;
                ticket.LastUpdated = DateTime.Now;
                
                if (tvm.ticket.ReceivedFrom != null)
                {
                    ticket.CustomerName = tvm.ticket.ReceivedFrom;
                }
                else
                {
                    ticket.CustomerName = tvm.account.AccountName;
                }
                //ticket.CurrentOwner = _UserRepository.GetUserById(tvm.user.UserId).UserId;
                ticket.CurrentOwner = 0;
                ticket.Creator = userworked.UserId;
                int numOfTicketsForAccount = _ticketRepository.GetTicketsByAccountID(ticket.AccountId).Count();
                numOfTicketsForAccount++;
                string newTicketHistoryID = numOfTicketsForAccount.ToString();
                string newAccountID = ticket.AccountId.ToString();
                ticket.TicketHistoryID = newAccountID+newTicketHistoryID;
                _ticketRepository.SaveTickets(ticket);
                ///////////////////////////////////////////////
                //Getting and Saving Ticket History
                Ticket newTicket = new Ticket();
                newTicket = _ticketRepository.Tickets.Single(row => row.TicketHistoryID == newAccountID+newTicketHistoryID);

                ticket_history.TicketId = newTicket.TicketHistoryID;
                ticket_history.HistoryDate = DateTime.Now;
                ticket_history.UserWorked = ticket.CurrentOwner;
                ticket_history.Action = ticket.Description;
                ticket_history.Comment = ticket.Comments;
                _ticketHistoryRepository.SaveHistory(ticket_history);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateNew()
        {
            TicketViewModel tvm = new TicketViewModel();
            Ticket newTicket = new Ticket();

            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);
            tvm.user = user;

            var aaUsersResult = _UserRepository.GetAllUsers();
            tvm.AAUsersDropdown = aaUsersResult.Select(row => new SelectListItem()
            {
                Text = row.LastName + "," + row.FirstName,
                Value = row.UserId.ToString()
            });
            newTicket.DateOpened = DateTime.Now;
            newTicket.LastUpdated = DateTime.Now;

            tvm.ticket = newTicket;
            return View(tvm);
        }


        [HttpPost]
        public ActionResult CreateNew(TicketCreateViewModel tvm)
        {
            try
            {
                var username = HttpContext.User.Identity.Name;
                var userworked = _UserRepository.GetUserByUsername(username);

                Ticket ticket = new Ticket();
                TicketHistory ticket_history = new TicketHistory();

                // Getting and Saving Ticket Information
                ticket = tvm.ticket;
                ticket.AccountId = 0;

                ticket.DateOpened = DateTime.Now;
                ticket.LastUpdated = DateTime.Now;

                if (tvm.ticket.ReceivedFrom != null)
                {
                    ticket.CustomerName = tvm.ticket.ReceivedFrom;
                }
                if (tvm.ticket.AccountName == null)
                {
                    ticket.AccountName = "No associated Account";
                }
                //ticket.CurrentOwner = _UserRepository.GetUserById(tvm.user.UserId).UserId;
                ticket.CurrentOwner = 0;
                ticket.Creator = userworked.UserId;
                int numOfTicketsForAccount = _ticketRepository.GetTicketsByAccountID(ticket.AccountId).Count();
                numOfTicketsForAccount++;
                string newTicketHistoryID = numOfTicketsForAccount.ToString();
                string newAccountID = ticket.AccountId.ToString();
                ticket.TicketHistoryID = newAccountID + newTicketHistoryID;
                _ticketRepository.SaveTickets(ticket);
                ///////////////////////////////////////////////
                //Getting and Saving Ticket History
                Ticket newTicket = new Ticket();
                newTicket = _ticketRepository.Tickets.Single(row => row.TicketHistoryID == newAccountID + newTicketHistoryID);

                ticket_history.TicketId = newTicket.TicketHistoryID;
                ticket_history.HistoryDate = DateTime.Now;
                ticket_history.UserWorked = ticket.CurrentOwner;
                ticket_history.Action = ticket.Description;
                ticket_history.Comment = ticket.Comments;
                _ticketHistoryRepository.SaveHistory(ticket_history);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Assign(int ticketid, string text)
        {
            var username = HttpContext.User.Identity.Name;
            var userworked = _UserRepository.GetUserByUsername(username);

            if (text != "undefined")
            {

                //Get the Assigned User Username
                if (text != "Not Assigned")
                {
                    string[] names = text.Split(',');
                    User user = _UserRepository.GetUserByUsername(text);

                    //Assign the ticket to this user
                    Ticket ticket = new Ticket();
                    TicketHistory ticket_history = new TicketHistory();
                    //save ticket
                    ticket = _ticketRepository.GetTicketByTicketId(ticketid);
                    ticket.CurrentOwner = user.UserId;
                    ticket.Status = "Assigned";
                    _ticketRepository.SaveTickets(ticket);

                    //save history
                    ticket_history.TicketId = ticket.TicketHistoryID;
                    ticket_history.HistoryDate = DateTime.Now;
                    ticket_history.UserWorked = userworked.UserId;
                    ticket_history.Action = "Ticket was assigned to "+ticket.CurrentOwner+" by " +userworked.FirstName;
                    ticket_history.Comment = ticket.Comments;
                    _ticketHistoryRepository.SaveHistory(ticket_history);


                }
                return RedirectToAction("Index");
            }
            else
            {
                return Content("Please click on the checkbox to assign the ticket to user.");
            }

        }
        
        public ActionResult Release(int ticketid)
        {
            var username = HttpContext.User.Identity.Name;
            var userworked = _UserRepository.GetUserByUsername(username);

                    //Assign the ticket to this user
                    Ticket ticket = new Ticket();
                    TicketHistory ticket_history = new TicketHistory();

                    ticket = _ticketRepository.GetTicketByTicketId(ticketid);
                    ticket.CurrentOwner = 0;
                    ticket.Status = "Un-Assigned";
                    _ticketRepository.SaveTickets(ticket);


                    //save history
                    ticket_history.TicketId = ticket.TicketHistoryID;
                    ticket_history.HistoryDate = DateTime.Now;
                    ticket_history.UserWorked = userworked.UserId;
                    ticket_history.Action = "Ticket was assigned to " + ticket.CurrentOwner + " by " + userworked.FirstName;
                    ticket_history.Comment = ticket.Comments;
                    _ticketHistoryRepository.SaveHistory(ticket_history);
                
                return RedirectToAction("Index");
           

        }
        [HttpPost]
        public ActionResult AssignCSR(int ticketid)
        {
            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);

            Ticket newTicket = new Ticket();
            TicketHistory ticket_history = new TicketHistory();

            newTicket = _ticketRepository.GetTicketByTicketId(ticketid);
            newTicket.CurrentOwner = user.UserId;
            newTicket.Status = "Assigned";
            _ticketRepository.SaveTickets(newTicket);

            //save history
            ticket_history.TicketId = newTicket.TicketHistoryID;
            ticket_history.HistoryDate = DateTime.Now;
            ticket_history.UserWorked = user.UserId;
            ticket_history.Action = "Ticket is now assigned to "+ user.FirstName;
            ticket_history.Comment = newTicket.Comments;
            _ticketHistoryRepository.SaveHistory(ticket_history);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ReleaseCSR(int ticketid)
        {
            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);

            Ticket newTicket = new Ticket();
            TicketHistory ticket_history = new TicketHistory();

            newTicket = _ticketRepository.GetTicketByTicketId(ticketid);
            newTicket.CurrentOwner = 0;
            newTicket.Status = "Un-Assigned";
            _ticketRepository.SaveTickets(newTicket);

            ticket_history.TicketId = newTicket.TicketHistoryID;
            ticket_history.HistoryDate = DateTime.Now;
            ticket_history.UserWorked = user.UserId;
            ticket_history.Action = "Ticket was released by " + user.FirstName;
            ticket_history.Comment = newTicket.Comments;
            _ticketHistoryRepository.SaveHistory(ticket_history);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AssignOrReleaseCSR(int ticketid)
        {
            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);

            Ticket newTicket = new Ticket();
            TicketHistory ticket_history = new TicketHistory();
            newTicket = _ticketRepository.GetTicketByTicketId(ticketid);
            if (newTicket.CurrentOwner == 0)
            {
                newTicket.CurrentOwner = user.UserId;
                newTicket.Status = "Assigned";
                ticket_history.TicketId = newTicket.TicketHistoryID;
                ticket_history.HistoryDate = DateTime.Now;
                ticket_history.UserWorked = user.UserId;
                ticket_history.Action = "Ticket is now assigned to " + user.FirstName;
                ticket_history.Comment = newTicket.Comments;
                _ticketHistoryRepository.SaveHistory(ticket_history);
            }
            else
            {
                newTicket.CurrentOwner = 0;
                newTicket.Status = "Un-Assigned";
                ticket_history.TicketId = newTicket.TicketHistoryID;
                ticket_history.HistoryDate = DateTime.Now;
                ticket_history.UserWorked = user.UserId;
                ticket_history.Action = "Ticket was released by " + user.FirstName;
                ticket_history.Comment = newTicket.Comments;
            _ticketHistoryRepository.SaveHistory(ticket_history);
            }
            
            _ticketRepository.SaveTickets(newTicket);

            return Content("Ticket has been updated");
        }
        //
        // GET: /Ticket/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Ticket/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
 
        public ActionResult Delete(int id)
        {
            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);

            //Delete the ticket
            Ticket deleteTicket = new Ticket();
            TicketHistory closeHistory = new TicketHistory();

            deleteTicket = _ticketRepository.Tickets.Single(row => row.TicketId == id);
            _ticketRepository.DeleteTickets(deleteTicket);

            //Save history
            closeHistory.TicketId = deleteTicket.TicketHistoryID;
            closeHistory.UserWorked = user.UserId;
            closeHistory.Action = "Customer Service Agent " + user.FirstName + " closed the ticket on " + DateTime.Now;
            closeHistory.AccountId = deleteTicket.AccountId;
            closeHistory.Comment = deleteTicket.Comments;
            closeHistory.HistoryDate = DateTime.Now;
            _ticketHistoryRepository.SaveHistory(closeHistory);
            return RedirectToAction("Index");
        }

        public ActionResult Close(int id)
        {
            var username = HttpContext.User.Identity.Name;
            var user = _UserRepository.GetUserByUsername(username);

            Ticket closeTicket = new Ticket();
            TicketHistory closeHistory = new TicketHistory();
//Save ticket
            closeTicket = _ticketRepository.Tickets.Single(row => row.TicketId == id);
            closeTicket.Status = "Closed";
            _ticketRepository.SaveTickets(closeTicket);

//Save history
            closeHistory.TicketId = closeTicket.TicketHistoryID;
            closeHistory.UserWorked = user.UserId;
            closeHistory.Action = "Customer Service Agent " + user.FirstName + " deleted the ticket on" + DateTime.Now;
            closeHistory.AccountId = closeTicket.AccountId;
            closeHistory.Comment = closeTicket.Comments;
            closeHistory.HistoryDate = DateTime.Now;
            _ticketHistoryRepository.SaveHistory(closeHistory);
            

            
            return RedirectToAction("Index");
        }


        public ActionResult AccountList()
        {
            TicketAccountListViewModel avm = new TicketAccountListViewModel();

            var username = HttpContext.User.Identity.Name;
            avm.user = _UserRepository.GetUserByUsername(username);

            var results = GetAllAccounts();
            foreach (var result in results)
            {
                var salesagentid = result.AssignedSalesRep;
                User newUser = _UserRepository.GetUserById(salesagentid);
                if (newUser == null)
                {

                    result.AssignedUser.UserName = "Not Assigned";
                }
                else
                {
                    result.AssignedUser = newUser;
                }
                int number = _ticketRepository.GetTicketsByAccountID(result.AccountId).Count();
                result.NumberOfTickets = number;
            }

            
            avm.Account = results;
            return View(avm);
        }
        public IEnumerable<Data.Domain.Account> GetAllAccounts()
        {
            return _AccountRepository.Accounts;
        }
    }
}
