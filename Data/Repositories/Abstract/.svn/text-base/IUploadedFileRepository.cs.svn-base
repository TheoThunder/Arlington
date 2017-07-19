using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface IUploadedFileRepository
    {
        IQueryable<UploadedFile> uploadedFiles { get; }
        void SaveUploadedFile(UploadedFile uploadedFile);
        void DeleteUploadedFile(UploadedFile uploadedFile);
    }
}
