using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Constants;

namespace Data.Repositories.Postgres
{
    public class PGUploadedfileRepository
    {
        private Npgsql.NpgsqlConnection _conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString());
        public PGUploadedfileRepository()
        {
        }
        #region Public Methods
        private List<Domain.UploadedFile> _files = new List<Domain.UploadedFile>();
        public IQueryable<Domain.UploadedFile> UploadedFiles
        {
            get
            {
                //IList<Domain.UploadedFile> Files = new List<Domain.UploadedFile>();
                _conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(UploadedfileTable.SelectQuery, _conn))
                {
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Domain.UploadedFile upFile = PopulateFromDB(dr);
                            _files.Add(upFile);
                        }
                    }
                }
                return _files.AsQueryable();
            }
        }

        public List<Domain.UploadedFile> GetFileByLeadId(int leadId)
        {
            Domain.UploadedFile upFile  = null;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {

                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(UploadedfileTable.SelectByLeadIdQuery, conn))
                {
                    command.Parameters.AddWithValue(UploadedfileTable.leadid, leadId);
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            upFile = PopulateFromDB(dr);
                            _files.Add(upFile);
                        }
                    }
                }
            }
            return _files;
        }

        public List<Domain.UploadedFile> GetFileByAccountId(int accountId)
        {
            Domain.UploadedFile upFile = null;
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {

                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(UploadedfileTable.SelectByAccountIdQuery, conn))
                {
                    command.Parameters.AddWithValue(UploadedfileTable.accountid, accountId);
                    using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            upFile = PopulateFromDB(dr);
                            _files.Add(upFile);
                        }
                    }
                }
            }
            return _files;
        }
        public void InsertLeadFile(int leadID, string path)
        {
            string query = UploadedfileTable.InsertLeadFileQuery;
            InsertUploadedFile(leadID, path, query);

        }

        public void InsertAccountFile(int accountID, string path)
        {
            string query = UploadedfileTable.InsertAccountFileQuery;
            InsertAccountUploadedFile(accountID, path, query);

        }
        #endregion

        #region Private Methods
        private void InsertUploadedFile(int ID, string path, string query)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {

                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue(UploadedfileTable.filename, System.IO.Path.GetFileName(path));
                    //todo: determine specific mime type
                    command.Parameters.AddWithValue(UploadedfileTable.filetype, "image");
                    command.Parameters.AddWithValue(UploadedfileTable.filepath, path);
                    command.Parameters.AddWithValue(UploadedfileTable.leadid, ID);

                    //command.Prepare();

                    ////command.Parameters["filename"].Value = System.IO.Path.GetFileName(lead.NewFilePath);
                    //command.Parameters["filetype"].Value = "application";
                    //command.Parameters["filepath"].Value = lead.NewFilePath;
                    //command.Parameters["leadid"].Value = lead.LeadId;
                    command.ExecuteNonQuery();
                }
            }

        }

        private void InsertAccountUploadedFile(int ID, string path, string query)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {

                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue(UploadedfileTable.filename, System.IO.Path.GetFileName(path));
                    //todo: determine specific mime type
                    command.Parameters.AddWithValue(UploadedfileTable.filetype, "image");
                    command.Parameters.AddWithValue(UploadedfileTable.filepath, path);
                    command.Parameters.AddWithValue(UploadedfileTable.accountid, ID);

                    
                    command.ExecuteNonQuery();
                }
            }

        }
        
        private Domain.UploadedFile PopulateFromDB(Npgsql.NpgsqlDataReader dr)
        {
            Domain.UploadedFile file = new Domain.UploadedFile();
            file.UploadedFileId = Helper.ConvertFromDBVal<int>(dr[UploadedfileTable.uploadedfileid]);
            file.FileName = Helper.ConvertFromDBVal<string>(dr[UploadedfileTable.filename]);
            file.FilePath = Helper.ConvertFromDBVal<string>(dr[UploadedfileTable.filepath]);
            file.FileType = Helper.ConvertFromDBVal<string>(dr[UploadedfileTable.filetype]);
            file.accountID = Helper.ConvertFromDBVal<int>(dr[UploadedfileTable.accountid]);
            file.leadID = Helper.ConvertFromDBVal<int>(dr[UploadedfileTable.leadid]);

            return file;
        }
        #endregion
    }
}
