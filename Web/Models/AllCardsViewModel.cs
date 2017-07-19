using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Domain;
using Data.Repositories;
using Data.Repositories.Abstract;

namespace Web.ViewModel
{
    public class AllCardsViewModel 
    {
        //
        // GET: /AllCardsViewModel/
        public User user { get; set; }
        public IEnumerable<string> callTypes = Data.Constants.CallTypeList.CallTypes;
        public IEnumerable<string> noInterest = Data.Constants.NotInterestedList.NotInterested;
        public IEnumerable<Card> cards { get; set; }
        public string UserName { get; set; }
        public User AssignedUser { get; set; } 
    }
}
