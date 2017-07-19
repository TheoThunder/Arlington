using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Data.Domain;
using Data.Repositories.Static;

namespace UnitTesting.DataTesting.StaticRepositories
{
    [TestFixture]
    public class StaticUploadedFileRepositoryTest
    {
        StaticUploadedFileRepository staticUploadedFileRepository;
        
        public StaticUploadedFileRepositoryTest()
        {
            staticUploadedFileRepository = new StaticUploadedFileRepository();
        }

        [SetUp]
        public void SetUp()
        {
            staticUploadedFileRepository.ClearRepo();
        }

        [Test]
        public void SaveNewUploadedFile()
        {
            var upldFile = new UploadedFile { FileName="Trinity", FileType="doc"  };
            staticUploadedFileRepository.SaveUploadedFile(upldFile);
            var result = staticUploadedFileRepository.uploadedFiles.Where(row => row.FileName == "Trinity").ToList();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].UploadedFileId != 0);

        }

        [Test]
        public void SaveUpdatedUploadedFile()
        {
            var upldFile = new UploadedFile { FileName = "Trinity", FileType = "doc" };
            staticUploadedFileRepository.SaveUploadedFile(upldFile);
            var result = staticUploadedFileRepository.uploadedFiles.Where(row => row.FileName == "Trinity").ToList()[0];
            result.FileName = "TrntyChngd";
            staticUploadedFileRepository.SaveUploadedFile(result);
            var result2 = staticUploadedFileRepository.uploadedFiles.Where(row => row.FileName == "TrntyChngd").ToList();
            Assert.IsTrue(result2.Count == 1, String.Format("result2 count was {0}", result2.Count));
            Assert.IsTrue(result2[0].UploadedFileId != 0);
            Assert.IsFalse(result2[0].FileName=="Trinity");
        }

        [Test]
        public void DeleteUploadedFile()
        {
            var upldFile = new UploadedFile { FileName = "Trinity", FileType = "doc" };
            staticUploadedFileRepository.SaveUploadedFile(upldFile);
            staticUploadedFileRepository.DeleteUploadedFile(upldFile);
            var result = staticUploadedFileRepository.uploadedFiles.Where(row => row.FileName == "Trinity").ToList();
            Assert.IsTrue(result.Count == 0);

        }
    }
}
