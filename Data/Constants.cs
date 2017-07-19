using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Constants
{
    /// <summary>
    /// A place to store the names for permissions. This should help prevent the problems 
    /// that tend to occur with string comparisons. 
    /// The Permission strings should be of the form of NAME_ACTION
    /// </summary>
    public static class Permissions
    {
        public static readonly string LEAD_CREATE = "Lead_Create";
        public static readonly string LEAD_VIEW = "Lead_View";
        public static readonly string CALENDAR_VIEW = "Calendar_View";
        public static readonly string PHONE_DIRECTCALL = "Phone_DirectCall";
        public static readonly string USER_MANAGE = "User_Manage";
        public static readonly string ACCOUNTS_VIEW = "Accounts_View";
        public static readonly string ZONES_MANAGE = "Zone_Manage";
        public static readonly string SETTINGS_EDIT = "Setting_Edit";
        /// <summary>
        /// Role can be assigned Leads
        /// </summary>
        public static readonly string LEAD_ASSIGNABLE = "Lead_Assignable";
        public static readonly string LEAD_APPOINTED = "Lead_Appointed";
        public static readonly string LEAD_IMPORT = "Lead_Import";
        public static readonly string LEAD_QUEUE = "Lead_Queue";
        public static readonly string APPOINTMENTS_QUEUE = "Appointments_Queue";
    }

    public static class EquipmentTypes
    {
        public static readonly string PINPAD = "Pinpad";
        public static readonly string TERMINAL = "Terminal";
        public static readonly string PRINTER = "Printer";
        public static readonly string CHECKEQUIPMENT = "CheckEquipment";
        
    }
    public static class CardTypes
    {
        public static readonly string DNC = "DNC";
        public static readonly string CALLBACK= "Call Back";
        public static readonly string WRONG = "Wrong#";
        public static readonly string NOTLEAD = "Not Lead";
        public static readonly string NOANSWER= "No Answer";
        public static readonly string NOINTEREST = "No Interest";
        public static readonly string SETAPPOINTMENT= "Set Appointment";
        public static readonly string RESCHEDULEAPPOINTMENT = "Reschedule Appointment";
    }
    public static class CallTypeList
    {
        public static IEnumerable<string> CallTypes = new List<string>
        {
              
             "Call Back", 
             "DNC",
             "Set Appointment",
             "Reschedule Appointment"
        };

    }
    
    public static class VolumeList
    {
        public static IEnumerable<string> Volume = new List<string>
        {
             "New Setup",
             "Under 2k",
             "2k - 5k", 
             "5k - 10k", 
             "10k - 20k",
             "20k - 30k",
             "30k - 50k",
             "50k+", 
             
        };

    }
    public static class LeadStatus
    {
        public static readonly string COLD = "cold";
    }
    public static class DecisionMakerList
    {
        public static IEnumerable<string> DecisionMakers = new List<string>
        {
            "CEO",
            "CFO",
            "Controller",
            "Office Manager",
            "Accounting Manager",
            "Owner",
            "Other",
            "President",
            "Vice President"
        };
    }

    public static class UserType
    {
        public static IEnumerable<string> type = new List<string>()
        {
            "Administrator", "Manager", "AA", "SA", "Customer Service Agent"
        };
    }
  

    public static class StatusList
    {
        public static IEnumerable<string> status = new List<string>()
        {
              "UnAssigned", "Ignored"
        };
    }
    public static class StatusListLeadQueue
    {
        public static IEnumerable<string> status = new List<string>()
        {
             "All", "Wrong#", "Not Lead", "DNC", "Left VM", "No Interest"
        };
    }
    public static class SourceDropDown
    {
        public static IEnumerable<string> source = new List<string>()
        {
             "SalesG"
        };
    }
    public static class ForCurrentProcessor
    {
        public static IEnumerable<string> Processors = new List <string>()
        {
            "None",
            "Wells Fargo",
            "First Data",
            "Merchant Services",
            "Elliot Management Group",
            "TransTech",
            "TransOne",
            "Heartland",
            "Paymentech",
            "RBS Lynk",
            "Trilogy",
            "Bank of America",
            "First American Payment Systems",
            "Compass Bank",
            "Fifth Third",
            "Liberty Merchant Services",
            "Retriever",
            "Elavon",
            "Encore"
        };
    }
    public static class Location
    {
        public static IEnumerable<string> Locations = new List<string>()
        {
             "Storefront","At TMG","Home","Restaurant/Coffee shop"
        };
    }
    public static class StateList
    {
        public static IEnumerable<string> States = new List<string>() {
            "AL",
            "AK",
            "AZ",
            "AR",
            "CA",
            "CO",
            "CT",
            "DE",
            "FL",
            "GA",
            "HI",
            "ID",
            "IL",
            "IN",
            "IA",
            "KS",
            "KY",
            "LA",
            "ME",
            "MD",
            "MA",
            "MI",
            "MN",
            "MS",
            "MO",
            "MT",
            "NE",
            "NV",
            "NH",
            "NJ",
            "NM",
            "NY",
            "NC",
            "ND",
            "OH",
            "OK",
            "OR",
            "PA",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VT",
            "VI",
            "VA",
            "WA",
            "WV",
            "WI",
            "WY"
        };
    }

    public static class Score
    {
        public static IEnumerable<string> Scores = new List<string>()
        {
            "Select", "Good", "Bad"
        }; 
    }
    public static class NotInterestedList
    {
        public static IEnumerable<string> NotInterested = new List<string>()
        {
            "Just recently switched", "Happy with current provider", "Don't currently accept cards", "No one can beat the current rates", "Too expensive so accept cards"
        };
    }

    public static class TimeList
    {
        public static IEnumerable<DateTime> AppointmentTimeSlots = new List<DateTime>()
        {
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("10:00"),
            Convert.ToDateTime("11:00"),
            Convert.ToDateTime("12:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00"),
            Convert.ToDateTime("9:00")
         

        };
    }
    public static class platform
    {
        public static IEnumerable<string> platforms = new List<string>()
        {
            "Fifth Third", "Paymentech", "VITAL(TSYS)"
        };
    }
    public static class Vender
    {
        public static IEnumerable<string> vender = new List<string>()
        {
            "FAPS", "MeritCard"
        };
    }

    public static class Terminals
    {
        public static IEnumerable<string> terminals = new List<string>()
        {
            "Momentum 4000", "Momentum 4000X", "Vx510/3730", "Vx510LE/3730LE", "Vx570", "Vx570DC", "Omni 3200", "Omni 3740", "Omni 3740E", "Omni 3750" ,"Omni 3750E", "Hypercom T7 Plus 35 Key", "Hypercom T4100", "Hypercom T4205", "Hypercom T4210", "Hypercom T4220", "Hypercom M4100 GPRS (Blade)", "Hypercom M4230 GPRS", "Hypercom T7P", "Hypercom T7P-T", "Hypercom T77-F", "Hypercom T77-T", "Hypercom T7 Plus 19 Key", "Nurit 2085", "Nurit 8020 GPRS", "Nurit 8400", "Nurit 8400E", "Nurit 8400L", "Nurit 8010 GPRS", "Nurit 8320", "Nurit 8320E", "Nurit 8320L", "Way 5000", "Orion"
        };
    }

    public static class CheckEquipment
    {
        public static IEnumerable<string> equipments = new List<string>()
        {
            "Mini Magtek Reader", "RDM EC 6014F Imager"
        };
    }
    public static class GiftCards
    {
        public static IEnumerable<string> giftcards = new List<string>()
        {
            "First Advantage", "Valuetec"
        };
    }

    public static class Software
    {
        public static IEnumerable<string> software = new List<string>()
        {
            "PC Charge", "DinerWare", "Future POS", "Micros", "Aloha", "Fresh Books", "QuickBooks", "Authorize.net Virtual Terminal", "FirstPay.net VPOS", "FirstPay.net Virtual Terminal"
        };
    }

    public static class Ecommerce
    {
        public static IEnumerable<string> ecommerce = new List<string>()
        {
            "FirstPay.net Moto Virtual Terminal","FirstPay.net Virtual POS","FirstPay.net Payment Gateway","FirstPay.net Total Package","Authorize.net eCommerce","Authorize.net MOTO","Authorize.net Retail"
        
        };
    }

    public static class Securchex
    {
        public static IEnumerable<string> securchex = new List<string>()
        {
            "Guaranteed Conversion Plus (Image)", "Guaranteed Conversion (No Image)", "Check Guarantee (Paper-Based)", "Corporate Check Guarantee (Paper-Based)", "Check Conversion With Verification", "Check Verification Only (Paper-Based)"
        };
    }

    public static class Ownership
    {
        public static IEnumerable<string> ownership = new List<string>()
        {
            "Free", "Merimac Lease", "In-House Lease", "Cash Sale", "Reprogrammed"
        };
    }

    public static class TeamNumbers
    {
        public static IEnumerable<int> teams = new List<int>()
        {
            0,1,2,3,4,5,6,7,8,9,10
        };
    }
        
    public static class PinPads
    {
        public static IEnumerable<string> pinpads = new List<string>()
        {
            "Internal Pin Pad", "VeriFone 1000SE", "VeriFone Mx830", "Hypercom 1300", "Hypercom S9", "Hypercom S9C", "Nurit292"
        };
    }

    public static class AccountStatus
    {
        public static IEnumerable<string> accountstatus = new List<string>()
        {
            "Active","Inactive","Closed","Pending","Seasonal", "Withdrawn"
        };
    }

    public static class LeadStatusList
    {
        public static IEnumerable<string> leadstatus = new List<string>()
        {
            "Cold Lead","Warm Lead", "Customer"
        };
    }

    public static class Priority
    {
        public static IEnumerable<string> ticketPriority = new List<string>()
        {
            "Low", "Medium" ,"High"
        };
    }

    public static class TicketStatus
    {
        public static IEnumerable<string> ticketStatus = new List<string>()
        {
            "New", "Assigned", "Un-Assigned", "Closed"
        };
    }

    public static class TicketType
    {
        public static IEnumerable<string> tickettype = new List<string>()
        {
            "Customer Service", "Installs"
        };
    }
    
    public static class TicketReason
    {
        public static IEnumerable<string> reason = new List<string>()
        {
            "Account Maintenance", "Technical/Terminal Assistance", "Pricing", "Suuplies"
        };
    }

    public static class TicketOrigin
    {
        public static IEnumerable<string> origin = new List<string>()
        {
            "Phone", "Email", "Web"
        };
    }

    public static class MonthList
    {
        public static IEnumerable<string> Months = new List<string>
        {
             "January", 
             "February", 
             "March",
             "April", 
             "May", 
             "June", 
             "July",
             "August",
             "September",
             "October",
             "November",
             "December"
        };

    }
    public static class YearList
    {
        public static IEnumerable<int> Year = new List<int>
        {
             2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022
             
        };

    }

    public static class UserStatus
    {
        public static IEnumerable<string> user_status = new List<string>
        {
             "Active", "In-Active"
             
        };

    }

    public static class RoleList
    {
        public static IEnumerable<string> role_list = new List<string>()
        {
            "Administrator", "Manager", "Appointment Agent", "Sales Agent", "Customer Service Agent"
        };
    }
    //public static class VolumeList
    //{
    //    public static IEnumerable<string> Volumes = new List<string>()
    //    {
    //        "1-5", "5-10", "10+"
    //    };
    //}
}
