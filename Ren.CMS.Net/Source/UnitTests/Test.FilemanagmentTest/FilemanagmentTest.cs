using System;
using Ren.CMS.Filemanagement;
using Ren.CMS.Persistence;
using NUnit.Framework;
using Ren.CMS.Persistence.Domain;
using System.Linq;
namespace Test.FilemanagmentTest
{
     [TestFixture]
    public class FilemanagmentTest
    {
        [Test]
        public void AddExternalFileTest()
        {
            nFile file = Filemanager.CreateFile(new Uri("http://downloads.networkfreaks.de/unittests/test.txt"), true, 0);
            Assert.NotNull(file);
            Assert.True(file.isActive);
            Assert.AreEqual(file.FilePath, "http://downloads.networkfreaks.de/unittests/test.txt");
        }

        [Test]
        public void GetFile()
        {
            nFile file = Filemanager.GetFile("test.txt");
            if(file == null)
            {
                AddExternalFileTest();
            }

            file = Filemanager.GetFile("test.txt");

            Assert.NotNull(file);
            Assert.False(file.Physical);
            Assert.True(file.isActive);
            Assert.AreEqual(file.AliasName, "test.txt");

        }

         [Test]
         public void SaveFileWithoutReferences()
         {
             nFile file = Filemanager.CreateFile(new Uri("http://downloads.networkfreaks.de/unittests/test.txt"), true, 0);
             string oldName = file.AliasName;
             file.AliasName = ("myNewName.txt");
             file.isActive = false;

             file = Filemanager.SaveFile(file);
             Assert.AreEqual( ("myNewName.txt"), file.AliasName);
             Assert.False(file.isActive);
             file.AliasName = oldName;
             file.isActive = true;
             file = Filemanager.SaveFile(file);
             Assert.AreEqual(file.AliasName, oldName);
             Assert.True(file.isActive);
         }

         [Test]
         public void AddNewReference()
         {
             nFile file = Filemanager.GetFile("test.txt");
             if (file == null)
             {
                 AddExternalFileTest();
             }

             file = Filemanager.GetFile("test.txt");
             int count = file.ReferencedFiles.Count;

             nFile fileRef = Filemanager.CreateFile(new Uri("http://downloads.networkfreaks.de/unittests/test.txt"), true, 0);
             fileRef.AliasName = "ReferenceToTest.txt";
             file.ReferencedFiles.Add(fileRef);
             file = Filemanager.SaveFile(file);
             Assert.AreEqual((count + 1), file.ReferencedFiles.Count);
         }


         [Test]
         public void DeleteReference()
         {
             nFile file = Filemanager.GetFile("test.txt");
             if (file == null)
             {
                 AddExternalFileTest();
             }

             file = Filemanager.GetFile("test.txt");
             if(file.ReferencedFiles.Count == 0)
             {
                 AddNewReference();
                 file = Filemanager.GetFile("test.txt");
             }
             int id = file.ReferencedFiles[0].Id;

             file.ReferencedFiles.Remove(file.ReferencedFiles[0]);

             Assert.False(file.ReferencedFiles.Any(e => e.Id == id));

             file = Filemanager.SaveFile(file);
             Assert.False(file.ReferencedFiles.Any(e => e.Id == id));

         }


         [Test]
         public void DeleteFile()
         {
             nFile file = Filemanager.GetFile("test.txt");
             if (file == null)
             {
                 AddExternalFileTest();
             }

             file = Filemanager.GetFile("test.txt");
             if (file.ReferencedFiles.Count == 0)
             {
                 AddNewReference();
                 file = Filemanager.GetFile("test.txt");
             }

             bool delete = Filemanager.DeleteFile(file);
             Assert.True(delete);
         }
    }
}
