using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
namespace Data.Domain
{
    /// <summary>
    /// UploadedFile contains all information need to store a file in a database
    /// </summary>
    public class UploadedFile
    {
        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int UploadedFileId { get; set; } //Database Id

        public string FileName { get; set; }
        public string FileType { get; set; } //This is the MIME Type of the file uploaded
        //public string Category { get; set; } //What type of file for an account/etc? Statement? Contract?
        private string _filepath;
        public string FilePath 
        {
            get
            {
                return _filepath;
            }
            set
            {
                if (value == string.Empty || value == null)
                {
                    _filepath = "#";
                }
                else
                {
                    _filepath = value;
                }
            }
        }
        public int accountID { get; set; }
        public int leadID { get; set; }
    }
}
