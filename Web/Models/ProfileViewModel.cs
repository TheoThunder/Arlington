using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain;
using Data.Repositories;
using Data.Repositories.Static;
using System.Web.Mvc;
using System.Web;

namespace Web.ViewModel
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {

        }
        public Lead lead { get; set; } 
        public int accounts;
        public User user { get; set; }
        public IEnumerable<Card> cards { get; set; }

        
    }
}