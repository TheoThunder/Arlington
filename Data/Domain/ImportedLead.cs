using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;

namespace Data.Domain
{
    /// <summary>
    /// Class to contain data read from imported csv file.
    /// If the csv file format changes then this class must be changed.
    /// </summary>
    [DelimitedRecord(",")]
    [IgnoreFirst] //ignores the first header line
    class ImportedLead
    {
        [FieldQuoted()]
        public string CompanyName;
        [FieldQuoted()]
        public string ContactFirstName;
        [FieldQuoted()]
        public string ContactLastName;
        [FieldQuoted()]
        public string MiddleInit;
        [FieldQuoted()]
        public string Gender;
        [FieldQuoted()]
        public string Title;
        [FieldQuoted()]
        public string Address;
        [FieldQuoted()]
        public string City;
        [FieldQuoted()]
        public string State;
        [FieldQuoted()]
        public string ZipCode;
        [FieldQuoted()]
        public string MailingAddress;
        [FieldQuoted()]
        public string MailingCity;
        [FieldQuoted()]
        public string MailingState;
        [FieldQuoted()]
        public string MailingZipCode;
        [FieldQuoted()]
        public string MailingCarrierRoute;
        [FieldQuoted()]
        public string MailingDeliveryPointBarCode;
        [FieldQuoted()]
        public string PrimaryPhoneNumber;
        [FieldQuoted()]
        public string AdditionalPhoneNumber;
        [FieldQuoted()]
        public string FaxNumber;
        [FieldQuoted()]
        public string MobileNumber;
        [FieldQuoted()]
        public string EmailAddress;
        [FieldQuoted()]
        public string ContactManagerGroup;
        [FieldQuoted()]
        public string DoNotCall;
        [FieldQuoted()]
        public string FollowUp;
        [FieldQuoted()]
        public string ContactType;
        [FieldQuoted()]
        public string Source;
        [FieldQuoted()]
        public string AddedOn;
        [FieldQuoted()]
        public string Notes;
    }
}
