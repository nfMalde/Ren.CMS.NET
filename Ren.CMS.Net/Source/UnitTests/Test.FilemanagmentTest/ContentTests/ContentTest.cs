using System;
using Ren.CMS.Filemanagement;
using Ren.CMS.Persistence;
using NUnit.Framework;
using Ren.CMS.Persistence.Domain;
using System.Linq;
using System.Collections.Generic;
using Ren.CMS.Content;

namespace Test.ContentTests
{
    [TestFixture]
    public class ContentTest
    {
        [TestFixtureSetUp] 
        public void init()
        {
            new Integration().SetupDB();
        }

        [Test]
        public void AddContentTest()
        {
            List<nContentText> Texta = new List<nContentText>();
            Texta.Add(new nContentText() { LangCode = "de-DE" , LongText ="Test", MetaDescription = "test", MetaKeyWords = "test", PreviewText ="test" , SEOName ="TEST123455", Title ="TEST"} );

            ContentManagement CM = new ContentManagement();
            nContent c = new nContent(0, Texta, "eNews", null, "", null, "", false, 0, DateTime.Now);

            CM.InsertContent(ref c);
           
            Assert.IsTrue(c.ID > 0);
            Assert.IsNotNull(c.Texts);
            Assert.True(c.Texts.Count > 0);
            Assert.AreEqual(Texta.First().LangCode, c.Texts.First().LangCode);
            Assert.AreEqual(Texta.First().LongText, c.Texts.First().LongText);
            Assert.AreEqual(Texta.First().MetaDescription, c.Texts.First().MetaDescription);
            Assert.AreEqual(Texta.First().MetaKeyWords, c.Texts.First().MetaKeyWords);
            Assert.AreEqual(Texta.First().PreviewText, c.Texts.First().PreviewText);
            Assert.AreEqual(Texta.First().SEOName, c.Texts.First().SEOName);
            Assert.AreEqual(Texta.First().Title, c.Texts.First().Title);
            Assert.AreEqual("eNews", c.ContentType);

        }
        
        [Test]
        public void GetContent()
        {
            ContentManagement.GetContent GC = new ContentManagement.GetContent(new string[] { "eNews" });
            var list = GC.getList();
            if (list.Count == 0)
                AddContentTest();
            GC = new ContentManagement.GetContent(new string[] { "eNews" });
            list = GC.getList();

            Assert.Greater(list.Count, 0);
            var c = list.First();

            Assert.NotNull(c);
            List<nContentText> Texta = new List<nContentText>();
            Texta.Add(new nContentText() { LangCode = "de-DE", LongText = "Test", MetaDescription = "test", MetaKeyWords = "test", PreviewText = "test", SEOName = "TEST123455", Title = "TEST" });
            Assert.IsTrue(c.ID > 0);
            Assert.IsNotNull(c.Texts);
            Assert.True(c.Texts.Count > 0);
            Assert.AreEqual(Texta.First().LangCode, c.Texts.First().LangCode);
            Assert.AreEqual(Texta.First().LongText, c.Texts.First().LongText);
            Assert.AreEqual(Texta.First().MetaDescription, c.Texts.First().MetaDescription);
            Assert.AreEqual(Texta.First().MetaKeyWords, c.Texts.First().MetaKeyWords);
            Assert.AreEqual(Texta.First().PreviewText, c.Texts.First().PreviewText);
            Assert.AreEqual(Texta.First().SEOName, c.Texts.First().SEOName);
            Assert.AreEqual(Texta.First().Title, c.Texts.First().Title);
            Assert.AreEqual("eNews", c.ContentType);
        }

        [Test]
        public void UpdateContentTest()
        {
            ContentManagement.GetContent GC = new ContentManagement.GetContent(new string[] { "eNews" });
            var list = GC.getList();
            if (list.Count == 0)
                AddContentTest();
            GC = new ContentManagement.GetContent(new string[] { "eNews" });
            list = GC.getList();

            Assert.Greater(list.Count, 0);
            var c = list.First();


            ContentManagement Mng = new ContentManagement();

            List<nContentText> Texta = new List<nContentText>();
            Texta.Add(new nContentText() { LangCode = "en-US", LongText = "Test2", MetaDescription = "test2", MetaKeyWords = "test2", PreviewText = "test", SEOName = "TEST1234553", Title = "TEST5" });
            c.Texts.Clear();
            c.Texts.Add(Texta.First());
            c.ContentType = "eTest";
            bool success = Mng.UpdateContent(c);
            var list2 = new ContentManagement.GetContent(id: c.ID).getList();
            c = list2.First();
            Assert.True(success);
            Assert.IsTrue(c.ID > 0);
            Assert.IsNotNull(c.Texts);
            Assert.True(c.Texts.Count > 0);
            Assert.AreEqual(Texta.First().LangCode, c.Texts.First().LangCode);
            Assert.AreEqual(Texta.First().LongText, c.Texts.First().LongText);
            Assert.AreEqual(Texta.First().MetaDescription, c.Texts.First().MetaDescription);
            Assert.AreEqual(Texta.First().MetaKeyWords, c.Texts.First().MetaKeyWords);
            Assert.AreEqual(Texta.First().PreviewText, c.Texts.First().PreviewText);
            Assert.AreEqual(Texta.First().SEOName, c.Texts.First().SEOName);
            Assert.AreEqual(Texta.First().Title, c.Texts.First().Title);
            Assert.AreEqual("eTest", c.ContentType);
        }


        [Test]
        public void DeleteContentTest()
        {
            Ren.CMS.Persistence.Base.BaseRepository<TContent> Crepo = new Ren.CMS.Persistence.Base.BaseRepository<TContent>();
            Ren.CMS.Persistence.Base.BaseRepository<ContentText> Trepo = new Ren.CMS.Persistence.Base.BaseRepository<ContentText>();

            var many = Crepo.GetMany();

            if (many.Count() == 0)
                AddContentTest();

            many = Crepo.GetMany();


            var id = many.First();

            ContentManagement Mng = new ContentManagement();

            bool success = Mng.DeleteContent(id.Id);

            Assert.True(success);
            TContent Test1 = Crepo.GetOne(NHibernate.Criterion.Expression.Where<TContent>(e => e.Id == id.Id));
            Assert.Null(Test1);
            foreach(ContentText text in id.Texts)
            {
                ContentText Test2 = Trepo.GetOne(NHibernate.Criterion.Expression.Where<ContentText>(e => e.Id == text.Id));
                Assert.Null(Test2);
            }




        }

        [Test]
        public void AddAttachmentRoleTest()
        {
            nAttachmentRole Role = nAttachmentRoleManager.GetRoleByName("TestRole");
            if (Role != null)
            {
                nAttachmentRoleManager.DeleteRole(Role.Id);
            }

            var argument = new nAttachmentArgument() { Argumentlangline = "asd", Argumentlangpackage = "asdsdasd", ArgumentName = "test" };
            var attachmentRole = new nAttachmentRole() { Arguments = new List<nAttachmentArgument>() { argument }, Rolename = "TestRole", Rolelangline = "RoleLangTest", Rolelangpackage = "RoleLangPackageTest" };

            Role  = nAttachmentRoleManager.RegisterNewRole(attachmentRole);

            Assert.NotNull(Role);
            Assert.AreEqual(attachmentRole.Rolename, Role.Rolename);
            Assert.AreEqual(attachmentRole.Rolelangline, Role.Rolelangline);
            Assert.AreEqual(attachmentRole.Rolelangpackage, Role.Rolelangpackage);
            Assert.Greater(Role.Id, 0);
        }

        [Test]
        public void AddAttachmentTypeTest()
        {
            nContentAttachmenType t =  nContentAttachmenTypeManager.GetTypeByName("DEFAULT");
            if (t != null)
                nContentAttachmenTypeManager.DeleteAttachmenType(t.Id);
            var attachmentHandler = new nContentAttachmenType() { Handler = new Ren.CMS.Content.ContentAttachmentHandlers.ContentAttachmentHandlerBase(), Name = "DEFAULT", StoragePath = "Test" };
            t = nContentAttachmenTypeManager.RegisterAttachmentType(attachmentHandler);

            Assert.NotNull(t);
            Assert.AreEqual(attachmentHandler.Name, t.Name);
            Assert.AreEqual(attachmentHandler.StoragePath, t.StoragePath);
            Assert.Greater(t.Id, 0);
            
        }

        [Test]
        public void AddContentAttachmentTest()
        {
            ContentManagement.GetContent GC = new ContentManagement.GetContent(new string[] { "eNews" });
            var list = GC.getList();
            if (list.Count == 0)
                AddContentTest();
            GC = new ContentManagement.GetContent(new string[] { "eNews" });
            list = GC.getList();

            Assert.Greater(list.Count, 0);
            var c = list.First();

            Assert.NotNull(c.Attachments);
            nAttachmentRole Role = nAttachmentRoleManager.GetRoleByName("TestRole");
            if (Role == null)
            {
                AddAttachmentRoleTest();
                Role = nAttachmentRoleManager.GetRoleByName("TestRole");
           }

            nContentAttachmenType type = nContentAttachmenTypeManager.GetTypeByName("DEFAULT");
            if(type == null)
            {
                AddAttachmentTypeTest();
                type = nContentAttachmenTypeManager.GetTypeByName("DEFAULT");
            }

            nContentAttachment attach = c.Attachments.AddAttachment(
                url: "http://downloads.networkfreaks.de/unittests/test.txt",
                cType: type,
                Argument: Role.Arguments.First(),
                Role: Role);
            
          }


    }
}
