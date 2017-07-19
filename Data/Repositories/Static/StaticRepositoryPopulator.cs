using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Static
{
    /// <summary>
    /// The Sole Purpose of this class is to be able to populate the static repositories with data
    /// </summary>
    public class StaticRepositoryPopulator
    {
        private static bool populated = false;

        public static StaticLeadRepository leadRep = new StaticLeadRepository();
        public static StaticEquipmentRepository equipRep = new StaticEquipmentRepository();
        public static StaticAccountRepository accRep = new StaticAccountRepository();
        public static StaticZoneRepository zoneRep = new StaticZoneRepository();
        public static StaticAppointmentSheetRepository appRep = new StaticAppointmentSheetRepository();
        public static StaticCardRepository cardRep = new StaticCardRepository();
        public static StaticUserRepository userRep = new StaticUserRepository();
        public static StaticPermissionRepository permissionRep = new StaticPermissionRepository();
        public static StaticRoleRepository roleRep = new StaticRoleRepository();

        public static List<Account> fakeAccount;
        public static List<Zone> fakeZone;
        public static List<AppointmentSheet> fakeAppSheet;
        public static List<User> fakeUser;
        public static List<Card> fakeCards;
        public static List<Permission> fakePermissions;
        

        public static void Populate()
        {
            if (!populated)
            {
                populated = true;
                PopulateLead();
                PopulateEquipment();
                PopulateAccount();
                PopulateAppointmentSheet();
                PopulateCard();
                PopulateZone();
                PopulatePermission();
                PopulateRole();
                PopulateUser();
                CreateRelations();
            }
        }

        private static void CreateRelations()
        {
            #region Loading Rows
            //**Leads
            var abcLead = leadRep.Leads.Single(row => row.CompanyName == "abc");
            var abnLead = leadRep.Leads.Single(row => row.CompanyName == "abn");
            var ascLead = leadRep.Leads.Single(row => row.CompanyName == "asc");
            var llbcLead = leadRep.Leads.Single(row => row.CompanyName == "llbc");
            var atcLead = leadRep.Leads.Single(row => row.CompanyName == "atc");

            //**Equipment
            var printEquip = equipRep.Equipments.Single(row => row.Name == "printer");
            var primaryTerminal = equipRep.Equipments.Single(row => row.Name == "terminal");
            var secondaryTerminal = equipRep.Equipments.Single(row => row.Name == "terminal");
            var chkEquip = equipRep.Equipments.Single(row => row.Name == "chkEqp");
            var primaryPinpad = equipRep.Equipments.Single(row => row.Name == "pinpad");
            var secondaryPinpad = equipRep.Equipments.Single(row => row.Name == "pinpad");

            //**Zone
            var zone1 = zoneRep.Zones.Single(row => row.ZoneNumber == 1);
            var zone2 = zoneRep.Zones.Single(row => row.ZoneNumber == 2);
            var zone3 = zoneRep.Zones.Single(row => row.ZoneNumber == 0);

            //**Roles
            var AdminRole = roleRep.GetRoleByRoleName("Admin");
            var ManagerRole = roleRep.GetRoleByRoleName("Manager");
            var SARole = roleRep.GetRoleByRoleName("SA");
            var AARole = roleRep.GetRoleByRoleName("AA");

            #endregion Loading Rows

            #region Creating Relationships

            #region Accounts
            //Leads for the Account            
            fakeAccount[0].ParentLead = abcLead.LeadId;
            fakeAccount[1].ParentLead = abcLead.LeadId;
            fakeAccount[2].ParentLead = abcLead.LeadId;
            fakeAccount[3].ParentLead = abnLead.LeadId;
            fakeAccount[4].ParentLead = abnLead.LeadId;
            //Equipment for Account            
            //fakeAccount[0].Printer = printEquip.EquipmentId;

            fakeAccount[0].PrimaryTerminal = primaryTerminal.EquipmentId;

            fakeAccount[0].SecondaryTerminal = secondaryTerminal.EquipmentId;

            fakeAccount[0].CheckEquipment = secondaryTerminal.EquipmentId;

            fakeAccount[0].PrimaryPINpad = primaryPinpad.EquipmentId;

            fakeAccount[0].SecondaryPINpad = secondaryPinpad.EquipmentId;

            #endregion Accounts

            #region Appointment Sheets
            //Leads for Appointment Sheets
            fakeAppSheet[0].ParentLeadId = abcLead.LeadId;
            fakeAppSheet[1].ParentLeadId = abnLead.LeadId;
            fakeAppSheet[2].ParentLeadId = llbcLead.LeadId;
            //fakeAppSheet[3].ParentLeadId = abcLead.LeadId;
            //fakeAppSheet[4].ParentLeadId = llbcLead.LeadId;
            //fakeAppSheet[5].ParentLeadId = abnLead.LeadId;

            #endregion Appointment Sheets

            #region User
            
            #endregion User

            #region Cards
            //Loading leads for cards
            //fakeCards[0].ParentLead = abcLead;           
            //fakeCards[1].ParentLead = abcLead;            
            //fakeCards[2].ParentLead = abnLead;           
            //fakeCards[3].ParentLead = ascLead;            
            //fakeCards[4].ParentLead = llbcLead;            
            //fakeCards[5].ParentLead = llbcLead;            
            //fakeCards[6].ParentLead = atcLead;            
            //fakeCards[7].ParentLead = atcLead;

            fakeCards[0].ParentLeadId = abcLead.LeadId;
            fakeCards[1].ParentLeadId = abcLead.LeadId;
            fakeCards[2].ParentLeadId = abnLead.LeadId;
            fakeCards[3].ParentLeadId = ascLead.LeadId;
            fakeCards[4].ParentLeadId = llbcLead.LeadId;
            fakeCards[5].ParentLeadId = llbcLead.LeadId;
            fakeCards[6].ParentLeadId = atcLead.LeadId;
            fakeCards[7].ParentLeadId = abcLead.LeadId;

            #endregion Cards

            #region Leads
            //Zones
            #endregion

            #endregion Creating Relationships

            //****Saving Data
            foreach (var row in fakeAccount)
                accRep.SaveAccounts(row);

            foreach (var row in fakeCards)
                cardRep.SaveCard(row);

            foreach (var row in fakeUser)
                userRep.SaveUser(row);

            foreach (var row in fakeAppSheet)
                appRep.SaveAppointmentSheet(row);

            leadRep.SaveLead(ascLead);
            leadRep.SaveLead(atcLead);
            leadRep.SaveLead(llbcLead);
        }

        private static void PopulateAccount()
        {


            fakeAccount = new List<Account> {
                new Account {MerchantId="a1", AccountName="Bank", SalesRepNumber="srn1", OfficeNumber="ofn1", Status="sts1", 
                AccountApprovalDate=DateTime.Now.AddHours(-5.0), AnnualFee=true, EstimatedMonthlyVolume="estvol", HT="ht", HMV="hmv",
                Platform = "pltform", Vendor="vndr", VIP=true, MBP=true, FreeSupplies= true, PCIRefund=true, MailingStreet="cooper",
                MailingCity="Arlington", MailingState="TX", MailingZipcode= "3221", DBAStreet="abram", DBAState="TX", DBAZipcode="76010",
                PrimaryPhone="871222222", SecondaryPhone="214432654", FaxNumber="123455555", PrimaryEmail="tt@hotmail.com", 
                SecondaryEmail="bb@gmail.com", Website="trinity.com", Credit=true, Debit= true, ARB=true, CIM=true, IP= true, 
                GiftCardProcessor="GiftPro", Secur_Chex="schx", Software="sftware", ECommerace="ecomrce", PrimaryTerminalOwner="ptowner",
                PrimaryTerminalQuantity=1, SecondaryTerminalOwner="sto", SecondaryTerminalQuantity=2, CheckEquipmentOwner= "ckheqo",
                CheckEquipmentQuantity=3, PrimaryPINpadOwner="ppO", PrimaryPINpadQuantity=4, SecondaryPINpadOwner="sPo", 
                SecondaryPINpadQuantity=5, PrinterOwner="prtOnr", Description="desc"},

                new Account {MerchantId="b1", AccountName="GAS station", SalesRepNumber="srn2", OfficeNumber="ofn2", Status="sts2", 
                AccountApprovalDate=DateTime.Now.AddHours(-3.0), AnnualFee=true, EstimatedMonthlyVolume="estvol2", HT="ht2", HMV="hmv2",
                Platform = "pltform2", Vendor="vndr2", VIP=true, MBP=true, FreeSupplies= true, PCIRefund=true, MailingStreet="mitchell",
                MailingCity="Arlington", MailingState="TX", MailingZipcode= "2221", DBAStreet="abram", DBAState="TX", DBAZipcode="76020",
                PrimaryPhone="871234222", SecondaryPhone="214572654", FaxNumber="123879555", PrimaryEmail="at@hotmail.com", 
                SecondaryEmail="gb@gmail.com", Website="trinity.com", Credit=true, Debit= true, ARB=true, CIM=true, IP= true, 
                GiftCardProcessor="GiftPro2", Secur_Chex="schx2", Software="sftware2", ECommerace="ecomrce2", PrimaryTerminalOwner="ptowner2",
                PrimaryTerminalQuantity=5, SecondaryTerminalOwner="sto2", SecondaryTerminalQuantity=3, CheckEquipmentOwner= "ckheqo2",
                CheckEquipmentQuantity=6, PrimaryPINpadOwner="ppO2", PrimaryPINpadQuantity=4, SecondaryPINpadOwner="sPo", 
                SecondaryPINpadQuantity=5, PrinterOwner="prtOnr", Description="desc"}, 
        
                new Account {MerchantId="id1", AccountName="Grocery Store", SalesRepNumber="srn1", OfficeNumber="ofn1", Status="sts1", 
                AccountApprovalDate=DateTime.Now.AddHours(-5.0), AnnualFee=true, EstimatedMonthlyVolume="estvol", HT="ht", HMV="hmv",
                Platform = "pltform", Vendor="vndr", VIP=true, MBP=true, FreeSupplies= true, PCIRefund=true, MailingStreet="cooper",
                MailingCity="Arlington", MailingState="TX", MailingZipcode= "3221", DBAStreet="abram", DBAState="TX", DBAZipcode="76010",
                PrimaryPhone="871222222", SecondaryPhone="214432654", FaxNumber="123455555", PrimaryEmail="tt@hotmail.com", 
                SecondaryEmail="bb@gmail.com", Website="trinity.com", Credit=true, Debit= true, ARB=true, CIM=true, IP= true, 
                GiftCardProcessor="GiftPro", Secur_Chex="schx", Software="sftware", ECommerace="ecomrce", PrimaryTerminalOwner="ptowner",
                PrimaryTerminalQuantity=1, SecondaryTerminalOwner="sto", SecondaryTerminalQuantity=2, CheckEquipmentOwner= "ckheqo",
                CheckEquipmentQuantity=3, PrimaryPINpadOwner="ppO", PrimaryPINpadQuantity=4, SecondaryPINpadOwner="sPo", 
                SecondaryPINpadQuantity=5, PrinterOwner="prttnr", Description="desc"},

                new Account {MerchantId="be1", AccountName="Mall", SalesRepNumber="srn2", OfficeNumber="ofn2", Status="sts2", 
                AccountApprovalDate=DateTime.Now.AddHours(-3.0), AnnualFee=true, EstimatedMonthlyVolume="estvol2", HT="ht2", HMV="hmv2",
                Platform = "pltform2", Vendor="vndr2", VIP=true, MBP=true, FreeSupplies= true, PCIRefund=true, MailingStreet="mitchell",
                MailingCity="Arlington", MailingState="TX", MailingZipcode= "2221", DBAStreet="abram", DBAState="TX", DBAZipcode="76020",
                PrimaryPhone="871234222", SecondaryPhone="214572654", FaxNumber="123879555", PrimaryEmail="at@hotmail.com", 
                SecondaryEmail="gb@gmail.com", Website="trinity.com", Credit=true, Debit= true, ARB=true, CIM=true, IP= true, 
                GiftCardProcessor="GiftPro2", Secur_Chex="schx2", Software="sftware2", ECommerace="ecomrce2", PrimaryTerminalOwner="ptowner2",
                PrimaryTerminalQuantity=5, SecondaryTerminalOwner="sto2", SecondaryTerminalQuantity=3, CheckEquipmentOwner= "ckheqo2",
                CheckEquipmentQuantity=6, PrimaryPINpadOwner="ppO2", PrimaryPINpadQuantity=4, SecondaryPINpadOwner="sPo", 
                SecondaryPINpadQuantity=5, PrinterOwner="p34Onr", Description="desc"},   
                
                new Account {MerchantId="al1", AccountName="School", SalesRepNumber="srn1", OfficeNumber="ofn1", Status="sts1", 
                AccountApprovalDate=DateTime.Now.AddHours(-5.0), AnnualFee=true, EstimatedMonthlyVolume="estvol", HT="ht", HMV="hmv",
                Platform = "pltform", Vendor="vndr", VIP=true, MBP=true, FreeSupplies= true, PCIRefund=true, MailingStreet="cooper",
                MailingCity="Arlington", MailingState="TX", MailingZipcode= "3221", DBAStreet="abram", DBAState="TX", DBAZipcode="76010",
                PrimaryPhone="871222222", SecondaryPhone="214432654", FaxNumber="123455555", PrimaryEmail="tt@hotmail.com", 
                SecondaryEmail="bb@gmail.com", Website="trinity.com", Credit=true, Debit= true, ARB=true, CIM=true, IP= true, 
                GiftCardProcessor="GiftPro", Secur_Chex="schx", Software="sftware", ECommerace="ecomrce", PrimaryTerminalOwner="ptowner",
                PrimaryTerminalQuantity=1, SecondaryTerminalOwner="sto", SecondaryTerminalQuantity=2, CheckEquipmentOwner= "ckheqo",
                CheckEquipmentQuantity=3, PrimaryPINpadOwner="ppO", PrimaryPINpadQuantity=4, SecondaryPINpadOwner="sPo", 
                SecondaryPINpadQuantity=5, PrinterOwner="prssnr", Description="desc"}
                };


            foreach (var row in fakeAccount)
                accRep.SaveAccounts(row);


        }
        private static void PopulateZone()
        {

            fakeZone = new List<Zone>{
                new Zone { ZoneNumber = 0,  ZipCodesCovered = new List<String> {"75052","76010","71040"}},
                new Zone { ZoneNumber = 1,  ZipCodesCovered = new List<String> {"85052","86010"}},
                new Zone { ZoneNumber = 2,  ZipCodesCovered = new List<String> {"95052","96010"}}
            };
            foreach (var row in fakeZone)
                zoneRep.SaveZone(row);

        }
        private static void PopulateAppointmentSheet()
        {

            fakeAppSheet = new List<AppointmentSheet>{
                new AppointmentSheet{ CreatedAt=DateTime.Now.AddHours(-10.0), LastUpdated=DateTime.Now, DayOfAppointment=DateTime.Now.AddHours(-10.0), 
                 AppointmentLocation = "abc", Street="cooper", City="Arlington", State="TX", ZipCode=76010, CurrentlyAcceptingCards=true,
                 NewSetUp=true, Price=false, NewEquipment=false, AddingServices=false, Unhappy=true,
                 CurrentProcessor="Intel", HowManyLocations=2, Volume="third", Swipe=true, Moto=false, Internet=true, Comment="hohoho", Score="this is score"},
                new AppointmentSheet{ CreatedAt=DateTime.Now.AddHours(-5.0), LastUpdated=DateTime.Now, DayOfAppointment=DateTime.Now.AddHours(-11.0), 
                 AppointmentLocation = "xyz", Street="abram", City="Arlington", State="TX", ZipCode=76020, CurrentlyAcceptingCards=true,
                 NewSetUp=false, Price=true, NewEquipment=false, AddingServices=true, Unhappy=true,
                 CurrentProcessor="Celeron", HowManyLocations=2, Volume="third", Swipe=false, Moto=true, Internet=true, Comment="wowasd o", Score="of making "},
                new AppointmentSheet{ CreatedAt=DateTime.Now.AddHours(-1.0), LastUpdated=DateTime.Now, DayOfAppointment=DateTime.Now.AddHours(-10.0), 
                 AppointmentLocation = "abfc", Street="cooper", City="Chicago", State="TX", ZipCode=76010, CurrentlyAcceptingCards=true,
                 NewSetUp=true, Price=false, NewEquipment=false, AddingServices=false, Unhappy=true,
                 CurrentProcessor="Intel", HowManyLocations=2, Volume="third", Swipe=true, Moto=false, Internet=false, Comment="You're confused", Score="we dream"},
                new AppointmentSheet{ CreatedAt=DateTime.Now.AddHours(-3.0), LastUpdated=DateTime.Now, DayOfAppointment=DateTime.Now.AddHours(-11.0), 
                 AppointmentLocation = "xyfz", Street="abram", City="Boston", State="MA", ZipCode=76020, CurrentlyAcceptingCards=true,
                 NewSetUp=false, Price=true, NewEquipment=false, AddingServices=true, Unhappy=true,
                 CurrentProcessor="Proce", HowManyLocations=2, Volume="third", Swipe=true, Moto=true, Internet=true, Comment="wowo sdf", Score="awake and"},
                 new AppointmentSheet{ CreatedAt=DateTime.Now.AddHours(-4.0), LastUpdated=DateTime.Now, DayOfAppointment=DateTime.Now.AddHours(-10.0), 
                 AppointmentLocation = "abssc", Street="cooper", City="Arlington", State="TX", ZipCode=76010, CurrentlyAcceptingCards=true,
                 NewSetUp=true, Price=false, NewEquipment=false, AddingServices=false, Unhappy=true,
                 CurrentProcessor="Dual", HowManyLocations=2, Volume="third", Swipe=true, Moto=false, Internet=false, Comment="You know it", Score="we stay"},
                new AppointmentSheet{ CreatedAt=DateTime.Now.AddHours(-3.0), LastUpdated=DateTime.Now, DayOfAppointment=DateTime.Now.AddHours(-11.0), 
                 AppointmentLocation = "Heere", Street="abram", City="NYC", State="NY", ZipCode=76020, CurrentlyAcceptingCards=true,
                 NewSetUp=false, Price=true, NewEquipment=false, AddingServices=true, Unhappy=true,
                 CurrentProcessor="Ce3", HowManyLocations=2, Volume="dummy", Swipe=true, Moto=false, Internet=false, Comment="To the street", Score="In the end"},

            
            
            };


            foreach (var row in fakeAppSheet)
                appRep.SaveAppointmentSheet(row);
        }

        private static void PopulateUser()
        {

            fakeUser = new List<User> { 
            
                new User { UserName="TestAdmin", newPassword="Trinity123", FirstName="Brettest", LastName="LopezLastName", Address1="222 Cooper st", Address2="suite#321", State="TX",
                          City="Arlington", ZipCode ="75643", OfficeNumber= 817214567, SalesRepNumber = 2124, CalendarColor="yellow", AssignedRoleId = 1},                     
            
                new User { UserName="TestAA", newPassword="Trinity123", FirstName="Marktest", LastName="Martinez", Address1="222 Cooper st", Address2="suite#321", State="TX",
                          City="Arlington", ZipCode ="75643", OfficeNumber= 817214567, SalesRepNumber = 2124, CalendarColor="yellow", AssignedRoleId = 2},                                          
            
                new User { UserName="TestSA", newPassword="Trinity123", FirstName="Nimbotest", LastName="Rodriguez", Address1="222 Cooper st", Address2="suite#321", State="TX",
                          City="Arlington", ZipCode ="75643", OfficeNumber= 817214567, SalesRepNumber = 2124, CalendarColor="yellow", AssignedRoleId = 3},             
            
            };


            foreach (var row in fakeUser)
                userRep.SaveUser(row);
        }

        private static void PopulateRole()
        {

            var fakeRoles = new List<Role>
            {
                new Role {Name = "Manager", Permissions = fakePermissions },
                new Role {Name = "AA", Permissions = fakePermissions},
                new Role {Name = "SA", Permissions = fakePermissions},
                new Role {Name = "Admin", Permissions = fakePermissions}
            };
            foreach (var row in fakeRoles)
                roleRep.SaveRole(row);
        }

        private static void PopulatePermission()
        {

            fakePermissions = new List<Permission>
            {
                new Permission(Data.Constants.Permissions.LEAD_VIEW),
                new Permission(Data.Constants.Permissions.USER_MANAGE),
                new Permission(Data.Constants.Permissions.ZONES_MANAGE),
                new Permission(Data.Constants.Permissions.LEAD_CREATE),
                new Permission(Data.Constants.Permissions.LEAD_ASSIGNABLE)
            };
            foreach (var row in fakePermissions)
                permissionRep.SavePermission(row);
        }
        private static void PopulateCard()
        {

            fakeCards = new List<Card>
           {
               new Card { CreatedOn=DateTime.Now.AddHours(-5.5), LastUpdated=DateTime.Today, NumberCalled= 1, CardType=Data.Constants.CardTypes.CALLBACK, 
                         Comment="This is a very nice comment, Good Customer", TalkedToPerson=false, TalkedToOfficeManager=true, TalkedToOther=true, LeftVM=false},
               new Card { CreatedOn=DateTime.Now.AddHours(-4.3), LastUpdated=DateTime.Now.AddHours(2.2), NumberCalled= 1, CardType=Data.Constants.CardTypes.DNC, 
                         Comment="Now that we are testing a comment, we can appreciate this", TalkedToPerson=false, TalkedToOfficeManager=false, TalkedToOther=true, LeftVM=false},
               new Card { CreatedOn=DateTime.Now.AddHours(-1.0), LastUpdated=DateTime.Now.AddHours(5.0), NumberCalled= 1, CardType=Data.Constants.CardTypes.NOANSWER, 
                         Comment="comment to a comment, what??", TalkedToPerson=false, TalkedToOfficeManager=false, TalkedToOther=false , LeftVM=false},
               new Card { CreatedOn=DateTime.Now.AddHours(-11.0), LastUpdated=DateTime.Now.AddHours(1.1), NumberCalled= 1, CardType=Data.Constants.CardTypes.NOINTEREST, 
                         Comment="this", TalkedToPerson=true, TalkedToOfficeManager=true, TalkedToOther=true, LeftVM=true},
               new Card { CreatedOn=DateTime.Now.AddHours(-8.0), LastUpdated=DateTime.Now, NumberCalled= 1, CardType=Data.Constants.CardTypes.NOTLEAD, 
                         Comment="good customer", TalkedToPerson=true, TalkedToOfficeManager=false, TalkedToOther=true, LeftVM=false},
               new Card { CreatedOn=DateTime.Now.AddHours(-3.0), LastUpdated=DateTime.Now.AddHours(11.2), NumberCalled= 1, CardType=Data.Constants.CardTypes.RESCHEDULEAPPOINTMENT, 
                         Comment="this is a customer", TalkedToPerson=true, TalkedToOfficeManager=false, TalkedToOther=true, LeftVM=true},
               new Card { CreatedOn=DateTime.Now.AddHours(-7.0), LastUpdated=DateTime.Now, NumberCalled= 1, CardType=Data.Constants.CardTypes.SETAPPOINTMENT, 
                         Comment="hopeless client", TalkedToPerson=false, TalkedToOfficeManager=true, TalkedToOther=false, LeftVM=true},
               new Card { CreatedOn=DateTime.Now.AddHours(-10.0), LastUpdated=DateTime.Now.AddHours(2.1), NumberCalled= 1, CardType=Data.Constants.CardTypes.WRONG, 
                         Comment="Dont't even call this number, They are mean girls", TalkedToPerson=false , TalkedToOfficeManager=false, TalkedToOther=true, LeftVM=true},
           };


            foreach (var row in fakeCards)
                cardRep.SaveCard(row);
        }
        private static void PopulateLead()
        {

            var fakeLeads = new List<Lead>
            {
              new Lead {CompanyName="abc", Contact1Title="mng1", Contact1FirstName="xy", Contact1LastName="yy", Contact2Title="mng2",
                        Contact2FirstName="uv", Contact2LastName="vv", PrimaryPhoneNumber="817-234-7757", AddtionalPhoneNumber="682-998-1234", 
                        FaxNumber="1-800-900-1234", PrimaryEmailAddress="tr@hotmail.com", AdditonalEmailAddress="br@hotmail.com", WebsiteLink="tr.com",
                        StreetAddress1="233 bn st", StreetAddress2 = "19 cs st",City ="arlington", State="tx", ZipCode="76090"},
              new Lead {CompanyName="abn", Contact1Title="mng3", Contact1FirstName="jy",Contact1LastName="yy", Contact2Title="mng4", AssignedAAUserId = 1, AssignedSAUserId = 1,
                        Contact2FirstName="ghh",Contact2LastName="vv", PrimaryPhoneNumber="817-234-7752", AddtionalPhoneNumber="692-998-1234", 
                        FaxNumber="1-800-900-2234", PrimaryEmailAddress="ar@hotmail.com", AdditonalEmailAddress="cr@hotmail.com", WebsiteLink="tr.com",
                        StreetAddress1="234 bn st", StreetAddress2 = "11 cs st",City ="arlington", State="tx", ZipCode="76091"},
              new Lead {CompanyName="asc", Contact1Title="mng5", Contact1FirstName="xy",Contact1LastName="yy", Contact2Title="mng6",AssignedAAUserId = 2, AssignedSAUserId = 2,
                        Contact2FirstName="uq",Contact2LastName="vv", PrimaryPhoneNumber="817-232-6757", AddtionalPhoneNumber="682-998-1254",
                        FaxNumber="1-800-900-1233", PrimaryEmailAddress="dr@hotmail.com", AdditonalEmailAddress="fr@hotmail.com", WebsiteLink="tr.com",
                        StreetAddress1="223 bn st", StreetAddress2 = "15 cs st",City ="arlington", State="tx", ZipCode="76092"},
              new Lead {CompanyName="llbc", Contact1Title="mng7", Contact1FirstName="xy",Contact1LastName="yy", Contact2Title="mng8", AssignedAAUserId = 1, AssignedSAUserId = 2,
                        Contact2FirstName="uf",Contact2LastName="vv", PrimaryPhoneNumber="817-234-7457", AddtionalPhoneNumber="682-998-2234",
                        FaxNumber="1-800-900-1734", PrimaryEmailAddress="er@hotmail.com", AdditonalEmailAddress="ur@hotmail.com", WebsiteLink="tr.com",
                        StreetAddress1="213 bn st", StreetAddress2 = "16 cs st",City ="arlington", State="tx", ZipCode="76290"},
              new Lead {CompanyName="atc", Contact1Title="mng9", Contact1FirstName="xy",Contact1LastName="yy", Contact2Title="mng10", AssignedAAUserId = 2, AssignedSAUserId = 1,
                        Contact2FirstName="pl",Contact2LastName="vv", PrimaryPhoneNumber="817-234-7797", AddtionalPhoneNumber="682-937-1234",  
                        FaxNumber="1-800-700-1634", PrimaryEmailAddress="sr@hotmail.com", AdditonalEmailAddress="zr@hotmail.com", WebsiteLink="tr.com",
                        StreetAddress1="203 bn st", StreetAddress2 = "14 cs st",City ="arlington", State="tx", ZipCode="76490"},

            };
            foreach (var row in fakeLeads)
                leadRep.SaveLead(row);
        }
        private static void PopulateEquipment()
        {

            var fakeEquipment = new List<Equipment> { 
                new Equipment {Name = "printer", Type=Data.Constants.EquipmentTypes.PRINTER, Active= true}, 
                new Equipment {Name = "pinpad", Type=Data.Constants.EquipmentTypes.PINPAD, Active= true }, 
                new Equipment {Name = "terminal", Type=Data.Constants.EquipmentTypes.TERMINAL, Active= true},
                new Equipment {Name = "chkEqp", Type=Data.Constants.EquipmentTypes.CHECKEQUIPMENT, Active= true},
            };

            foreach (var row in fakeEquipment)
                equipRep.SaveEquipment(row);
        }
    }
}
