using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Constants
{

	public class UploadedfileTable
	{
		public const string name = "uploadedfile";
		public const string uploadedfileid = "uploadedfileid";
		public const string filename = "filename";
		public const string filetype = "filetype";
		public const string filepath = "filepath";
		public const string accountid = "accountid";
		public const string leadid = "leadid";


        public const string InsertLeadFileQuery = @"INSERT INTO uploadedfile( filename, filetype, filepath, leadid) VALUES (:filename, :filetype, :filepath, :leadid )";
        public const string InsertAccountFileQuery = @"INSERT INTO uploadedfile( filename, filetype, filepath, accountid) VALUES (:filename, :filetype, :filepath, :accountid )";
        public const string SelectQuery = @"SELECT * FROM uploadedfile";
        public const string SelectByLeadIdQuery = @"SELECT * FROM uploadedfile WHERE leadid = :leadid";
        public const string SelectByAccountIdQuery = @"SELECT * FROM uploadedfile WHERE accountid = :accountid";
		
	}
	
}

