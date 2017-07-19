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
    public class CallBackViewModel
    {
    public CallBackViewModel()
        {

        }
        public IEnumerable<Lead> Leads;
        public User user { get; set; }
        
    }
}