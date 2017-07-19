using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
using System.Web.Mvc;
namespace Data.Domain
{
    public class Account
    {
        public Account()
        {
            StatementFiles = new List<UploadedFile>();
            AssignedUser = new User();
            AssignedSA = new User();
        }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int AccountId { get; set; } //Database ID
        public List<UploadedFile> StatementFiles { get; set; }
        public string NewFilePath { get; set; }
       

        [Required]
        [StringLength(20, MinimumLength=20, ErrorMessage = "Exactly 20 characters")]
        public string MerchantId { get; set; }

        [Required]
        public string AccountName { get; set; }
        //public User AACreator { get; set; }
        public User AssignedSA { get; set; }
        public int AACreator { get; set; }
        public int AssignedSalesRep { get; set; }
        public string SalesRepNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string Status { get; set; }
        public DateTime AccountApprovalDate { get; set; }
        public Boolean AnnualFee { get; set; }
        public string EstimatedMonthlyVolume { get; set; }
        public string HT { get; set; }
        public string HMV { get; set; }
        public string Platform { get; set; }
        public string Vendor { get; set; }
        public Boolean VIP { get; set; }
        public Boolean MBP { get; set; }
        public Boolean FreeSupplies { get; set; }
        public Boolean PCIRefund { get; set; }
        public string MailingStreet { get; set; }
        public string MailingCity { get; set; }
        public string MailingState { get; set; }
        public string MailingZipcode { get; set; }
        public string DBAStreet { get; set; }
        public string DBACity { get; set; }
        public string DBAState { get; set; }
        public string DBAZipcode { get; set; }

        [Required]
        public string PrimaryPhone { get; set; }

        [Required]
        public string SecondaryPhone { get; set; }

        
        public string FaxNumber { get; set; }

        [DataType(DataType.EmailAddress)] 
        public string PrimaryEmail { get; set; }

        [DataType(DataType.EmailAddress)] 
        public string SecondaryEmail { get; set; }

        [DataType(DataType.Url)] 
        public string Website { get; set; }
       
        public Boolean Credit { get; set; }
        public Boolean Debit { get; set; }
        public Boolean ARB { get; set; }
        public Boolean CIM { get; set; }
        public Boolean IP { get; set; }
        public string GiftCardProcessor { get; set; }
        public string Secur_Chex { get; set; }
        public string Software { get; set; }
        public string ECommerace { get; set; }
       // public Equipment PrimaryTerminal { get; set; }
        public int PrimaryTerminal { get; set; }
        public string PrimaryTerminalOwner { get; set; }

       
        public int PrimaryTerminalQuantity { get; set; }
        //public Equipment SecondaryTerminal { get; set; }
        public int SecondaryTerminal { get; set; } 
        public string SecondaryTerminalOwner { get; set; }

       
        public int SecondaryTerminalQuantity { get; set; }

       //public Equipment SecondaryTerminal { get; set; }
       public int CheckEquipment { get; set; } 
        public string CheckEquipmentOwner { get; set; }

    
        public int CheckEquipmentQuantity { get; set; }

        //public Equipment PrimaryPINpad { get; set; }
        public int PrimaryPINpad { get; set; }
        public string PrimaryPINpadOwner { get; set; }

        public int PrimaryPINpadQuantity { get; set; }

        //public Equipment SecondaryPINpad { get; set; }
        public int SecondaryPINpad { get; set; }
        public string SecondaryPINpadOwner { get; set; }

    
        public int SecondaryPINpadQuantity { get; set; }

       // public Equipment Printer { get; set; }
        public string Printer { get; set; }
        public string PrinterOwner { get; set; }
        public string Description { get; set; }
        //public IList<UploadedFile> UploadedFiles { get; set; }
        public int UploadedFiles { get; set; }
        //public UploadedFile UploadedFiles { get; set; }
        public int ParentLead { get; set; }
       
        
        
        // This user is to populate username etc for a user. AssignedUser.Userid id referenced using AssignedSAUserID
        public User AssignedUser { get; set; }


        //To diplay number of tickets for account
        public int NumberOfTickets { get; set; }

        public int Zone { get; set; }

        public IEnumerable<Equipment> equipments { get; set; }
    }
}
