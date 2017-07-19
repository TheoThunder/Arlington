using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;
using System.Data.Linq;

namespace Data.Repositories.Static
{
    public class StaticUploadedFileRepository:IUploadedFileRepository
    {
        private static IList<UploadedFile> fakeUpldFile = new List<UploadedFile>();
        private static int counter = 1;

        public IQueryable<Domain.UploadedFile> uploadedFiles
        {
            get { return fakeUpldFile.AsQueryable(); }
        }

        public void SaveUploadedFile(UploadedFile uploadedFile)
        {
            // If it's a new uploaded file, just add it to the list
            if (uploadedFile.UploadedFileId == 0)
            {
                uploadedFile.UploadedFileId = counter;
                counter += 1;
                fakeUpldFile.Add(uploadedFile);
            }
            else if (fakeUpldFile.Count(row => row.UploadedFileId == uploadedFile.UploadedFileId) == 1)
            {
                //This is an update. Remove old one, insert new one
                DeleteUploadedFile(uploadedFile);
                fakeUpldFile.Add(uploadedFile);
            }
        }
        public void DeleteUploadedFile(UploadedFile uploadedFile)
        {
            var temp = fakeUpldFile.ToList();
            temp.RemoveAll(row => row.UploadedFileId == uploadedFile.UploadedFileId);
            fakeUpldFile = temp;
        
        }
        /// <summary>
        /// Only for Unit Testing. Clears repo of all data
        /// </summary>
        public void ClearRepo()
        {
            fakeUpldFile.Clear();
        }
    }
}
